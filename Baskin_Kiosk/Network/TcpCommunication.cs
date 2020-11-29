using Baskin_Kiosk.Model.DAO;
using Baskin_Kiosk.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace Baskin_Kiosk.Network
{
    public class TcpCommunication
    {
        private bool isSend = false;
        private const int MAX_LEN = 4096;
        public string serverResponse = string.Empty;

        private byte[] sendData = new byte[MAX_LEN];
        private byte[] receiveData = new byte[MAX_LEN];
        private byte[] response = new byte[MAX_LEN];
        private Thread networkThread = null;

        private readonly OrderDAO orderDAO = new OrderDAO();
        public static bool isConnected = false;

        TcpClient client = null;
        NetworkStream networkStream = null;

        public void MessageSettings()
        {
            client = new TcpClient(); // (ip주소 , 포트 번호)

            const int THIRD_SECOND = 3000;
            var result = client.BeginConnect(Constants.SERVER_ADDRESS, Constants.SERVER_PORT, null, null);
            result.AsyncWaitHandle.WaitOne(THIRD_SECOND, true);

            networkStream = client.GetStream();
            networkStream.Write(sendData, 0, sendData.Length);
        }

        private void GetResponse()
        {
            int readData = networkStream.Read(response, 0, response.Length);
            serverResponse = Encoding.UTF8.GetString(response, 0, readData);

            if (serverResponse == "200")
            {
                isConnected = true;
            }
        }

        public async void ReceiveMessage()
        {
            try
            {
                NetworkStream receiveStream = client.GetStream();
                int readData = 0;
                string response = "";

                while (true)
                {
                    try
                    {
                        isSend = false;
                        readData = await receiveStream.ReadAsync(receiveData, 0, receiveData.Length);
                        response = Encoding.UTF8.GetString(receiveData, 0, readData);

                        if (!isSend)
                        {
                            if (response.Length > 0)
                            {
                                if (response.IndexOf("총매출액") > -1)
                                {
                                    string totalPrice = "현재 총 매출액은 " + orderDAO.GetTotalPrice() + "원 입니다.";
                                    SendMessage(totalPrice, true);
                                    break;
                                }
                                MessageBox.Show(response);
                            }
                            else
                            {
                                isConnected = false;
                                MessageBox.Show("서버와 연결이 종료되었습니다.");
                                client.Close();
                                networkStream.Close();
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("서버와 연결이 닫혔습니다.");
                        isConnected = false;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void ThreadStart()
        {
            if (isConnected)
            {
                networkThread = new Thread(new ThreadStart(ReceiveMessage));
                networkThread.Start();
            }
        }

        public void ThreadEnd()
        {
            if (networkThread != null)
            {
                networkThread.Abort();
            }
        }

        public string SendMessage()
        {
            MsgPacket packet = new MsgPacket
            {
                MSGType = "0",
                Id = "2205"
            };

            string JsonStr = JsonConvert.SerializeObject(packet);
            sendData = Encoding.UTF8.GetBytes(JsonStr);

            try
            {
                MessageSettings();
                GetResponse();
                return serverResponse;
            }
            catch (Exception)
            {
                isConnected = false;
                MessageBoxResult confirm = MessageBox.Show("서버가 꺼진상태로 로그인 하시겠습니까?", "잠시만요", MessageBoxButton.YesNo);

                if (confirm == MessageBoxResult.Yes)
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
        }

        public void SendMessage(string message, bool? isGroup)
        {
            MsgPacket packet = new MsgPacket
            {
                MSGType = "1",
                Id = "2205",
                Content = message,
                Group = (isGroup == true)
            };

            string JsonStr = JsonConvert.SerializeObject(packet);
            sendData = Encoding.UTF8.GetBytes(JsonStr);

            try
            {
                MessageSettings();
                isSend = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string SendMessage(List<MsgOrderInfo> orderInfo, string orderNum)
        {
            MsgPacket packet = new MsgPacket
            {
                MSGType = "2",
                Id = "2205",
                Group = true,
                ShopName = "베스킨라빈스 구지점",
                Menus = orderInfo,
                OrderNumber = orderNum.ToString(),
            };

            string JsonStr = JsonConvert.SerializeObject(packet);
            sendData = Encoding.UTF8.GetBytes(JsonStr);

            try
            {
                MessageSettings();
                GetResponse();
                return serverResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return null;
        }
    }
}
