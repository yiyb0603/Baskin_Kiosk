using Baskin_Kiosk.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Baskin_Kiosk.Util
{
    public class ServerConnection
    {
        public void sendOrderMessage(ObservableCollection<Food> foodList)
        {
            int menuLength = foodList.Count;
            JArray menus = new JArray();

            JObject json = new JObject();
            json.Add("MSGType", 2);
            json.Add("id", "2205");
            json.Add("Content", "");
            json.Add("ShopName", "베스킨라빈스 구지점");
            json.Add("OrderNumber", "001");

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

            const int MAX_LEN = 4096;
            byte[] sendData = new byte[MAX_LEN];


            String sendStr = JsonConvert.SerializeObject(json);
            sendData = Encoding.UTF8.GetBytes(sendStr);
            NetworkStream networkStream = null;
            
            try
            {
                TcpClient client = new TcpClient(Constants.SERVER_ADDRESS, Constants.SERVER_PORT);    // (ip주소 , 포트 번호)
                networkStream = client.GetStream();

                networkStream.Write(sendData, 0, sendData.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버와 연결이 실패된거같음");
            } finally
            {
                if (networkStream != null)
                {
                    networkStream.Close();
                }
            }
        }
    }
}
