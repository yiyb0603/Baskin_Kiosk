using Baskin_Kiosk.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Baskin_Kiosk.Network
{
    public class ServerConnection
    {
        private const int MAX_LEN = 4096;
        private byte[] sendData = new byte[MAX_LEN];

        NetworkStream networkStream = null;
        TcpClient client = null;

        public string sendMessage()
        {
            MsgPacket packet = new MsgPacket();
            packet.MSGType = "0";
            packet.Id = "2205";

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
                byte[] response = new byte[MAX_LEN];
                Int32 readData = networkStream.Read(response, 0, response.Length);

                String getResponse = Encoding.UTF8.GetString(response, 0, readData);
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                return getResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return null;
        }


        public string sendMessage(String message)
        {
            MsgPacket packet = new MsgPacket();
            packet.MSGType = "1";
            packet.Id = "2205";
            packet.Content = message;

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


        public string sendMessage(List<MsgOrderInfo> orderInfo, string orderNum)
        {
            MsgPacket packet = new MsgPacket();
            packet.MSGType = "2";
            packet.Id = "2205";
            packet.ShopName = "배스킨라빈스 구지점";
            packet.Menus = orderInfo;
            packet.OrderNumber = orderNum.ToString();

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
