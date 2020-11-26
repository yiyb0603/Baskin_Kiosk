using Baskin_Kiosk.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace Baskin_Kiosk.Network
{
    public class ServerConnection
    {
        private bool isSend = false;
        private const int MAX_LEN = 4096;

        private byte[] sendData = new byte[MAX_LEN];
        private byte[] receiveData = new byte[MAX_LEN];
        private Thread networkThread = null;

        TcpClient client = null;
        NetworkStream networkStream = null;
        
        public string sendMessage()
        {
            MsgPacket packet = new MsgPacket
            {
                MSGType = "0",
                Id = "2205"
            };

            String JsonStr = JsonConvert.SerializeObject(packet);
            this.sendData = Encoding.UTF8.GetBytes(JsonStr);

            try
            {
                if (client == null)
                {
                    client = new TcpClient(Constants.SERVER_ADDRESS, Constants.SERVER_PORT); // (ip주소 , 포트 번호)
                    networkStream = client.GetStream();
                }

                networkStream.Write(sendData, 0, sendData.Length);
                byte[] response = new byte[MAX_LEN];
                Int32 readData = networkStream.Read(response, 0, response.Length);

                String getResponse = Encoding.UTF8.GetString(response, 0, readData);
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                return getResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                threadEnd();
            }

            return null;
        }

        public async void receiveMessage()
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
                            MessageBox.Show(response);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        break;
                    }
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void threadStart()
        {
            this.networkThread = new Thread(new ThreadStart(receiveMessage));
            networkThread.Start();
        }

        public void threadEnd()
        {
            if (networkThread != null)
            {
                networkThread.Abort();
            }
        }

        public void sendMessage(string message, bool? isGroup)
        {
            MsgPacket packet = new MsgPacket
            {
                MSGType = "1",
                Id = "2205",
                Content = message,
                Group = (isGroup == true)
            };

            string JsonStr = JsonConvert.SerializeObject(packet);
            this.sendData = Encoding.UTF8.GetBytes(JsonStr);

            try
            {
                if (client == null)
                {
                    client = new TcpClient(Constants.SERVER_ADDRESS, Constants.SERVER_PORT); // (ip주소 , 포트 번호)
                    networkStream = client.GetStream();
                }

                networkStream.Write(sendData, 0, sendData.Length);
                isSend = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public String sendMessage(List<MsgOrderInfo> orderInfo, String orderNum)
        {
            MsgPacket packet = new MsgPacket
            {
                MSGType = "2",
                Id = "2205",
                ShopName = "배스킨라빈스 구지점",
                Menus = orderInfo,
                OrderNumber = orderNum.ToString()
            };

            String JsonStr = JsonConvert.SerializeObject(packet);
            this.sendData = Encoding.UTF8.GetBytes(JsonStr);

            try
            {
                if (client == null)
                {
                    client = new TcpClient(Constants.SERVER_ADDRESS, Constants.SERVER_PORT); // (ip주소 , 포트 번호)
                    networkStream = client.GetStream();
                }

                networkStream.Write(sendData, 0, sendData.Length);
                byte[] response = new byte[MAX_LEN];
                Int32 readData = networkStream.Read(response, 0, response.Length);

                String getResponse = Encoding.UTF8.GetString(response, 0, readData);
                return getResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return null;
        }
    }
}
