using Baskin_Kiosk.Common;
using Baskin_Kiosk.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public String connectionLogin()
        {
            MsgPacket packet = new MsgPacket();
            packet.MSGType = "0";
            packet.Id = "2205";
            packet.Content = string.Empty;
            packet.ShopName = "Baskin Robbins";

            //packet.OrderNumber = "001";
            //packet.Menus = new List<MsgOrderInfo>();
            //MsgOrderInfo orderInfo = new MsgOrderInfo();
            //orderInfo.Name = "싸이버거";
            //orderInfo.Count = "2";
            //orderInfo.Price = "1000";
            //packet.Menus.Add(orderInfo);

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
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return null;
        }


        public void sendMessage(String message)
        {
            JObject json = new JObject();
            json.Add("MSGType", 1);
            json.Add("id", "2205");
            json.Add("Content", message);

            String sendStr = JsonConvert.SerializeObject(json);
            this.sendData = Encoding.UTF8.GetBytes(sendStr);

            try
            {
                client = new TcpClient(Constants.SERVER_ADDRESS, Constants.SERVER_PORT); // (ip주소 , 포트 번호)
                networkStream = client.GetStream();

                networkStream.Write(sendData, 0, sendData.Length);
                byte[] response = new byte[MAX_LEN];
                //Int32 readData = networkStream.Read(response, 0, response.Length);

                //String getResponse = Encoding.UTF8.GetString(response, 0, readData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버와 연결이 실패된거같음");
                MessageBox.Show(ex.ToString());
            }
        }


        public void sendOrderMessage(ObservableCollection<Food> foodList, int orderNum)
        {
            String orderNumber = orderNum < 10 ? "00" + orderNum.ToString() : orderNum < 100 ? "0" + orderNum.ToString() : orderNum.ToString();
            int menuLength = foodList.Count;
            JArray menus = new JArray();

            JObject json = new JObject();
            json.Add("MSGType", 2);
            json.Add("id", "2205");
            json.Add("Content", "");
            json.Add("ShopName", "베스킨라빈스 구지점");
            json.Add("OrderNumber", orderNumber);

            for (int i = 0; i < foodList.Count; i++)
            {
                String menuName = foodList[i].menuName;
                int count = foodList[i].count;
                int price = foodList[i].price;

                JObject menuData = new JObject();
                menuData.Add("Name", menuName);
                menuData.Add("Count", count);
                menuData.Add("Price", price);

                menus.Add(menuData);
            }
            json.Add("Menus", menus);

            String sendStr = JsonConvert.SerializeObject(json);
            this.sendData = Encoding.UTF8.GetBytes(sendStr);
            
            try
            {
                client = new TcpClient(Constants.SERVER_ADDRESS, Constants.SERVER_PORT); // (ip주소 , 포트 번호)
                networkStream = client.GetStream();

                networkStream.Write(sendData, 0, sendData.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버와 연결이 실패된거같음");
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
