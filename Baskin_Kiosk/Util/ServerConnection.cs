using Baskin_Kiosk.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Baskin_Kiosk.Util
{
    public class ServerConnection
    {
        private const int MAX_LEN = 4096;
        private byte[] sendData = new byte[MAX_LEN];

        public String connectionLogin()
        {
            JObject json = new JObject();
            json.Add("MSGType", 0);
            json.Add("id", "2205");
            json.Add("Content", "로그인 되었습니다.");
            json.Add("OrderNumber", "");
            json.Add("Menus", "");

            String sendStr = JsonConvert.SerializeObject(json);
            this.sendData = Encoding.UTF8.GetBytes(sendStr);

            NetworkStream networkStream = null;
            TcpClient client = null;

            try
            {
                client = new TcpClient(Constants.SERVER_ADDRESS, Constants.SERVER_PORT); // (ip주소 , 포트 번호)
                networkStream = client.GetStream();

                networkStream.Write(sendData, 0, sendData.Length);
                byte[] response = new byte[MAX_LEN];
                Int32 readData = networkStream.Read(response, 0, response.Length);

                String getResponse = Encoding.UTF8.GetString(response, 0, readData);
                return getResponse;

            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return null;
        }


        public String sendMessage(String message)
        {
            JObject json = new JObject();
            json.Add("MSGType", 1);
            json.Add("id", "2205");
            json.Add("Content", message);

            String sendStr = JsonConvert.SerializeObject(json);
            this.sendData = Encoding.UTF8.GetBytes(sendStr);

            NetworkStream networkStream = null;
            TcpClient client = null;

            try
            {
                client = new TcpClient(Constants.SERVER_ADDRESS, Constants.SERVER_PORT); // (ip주소 , 포트 번호)
                networkStream = client.GetStream();

                networkStream.Write(sendData, 0, sendData.Length);
                byte[] response = new byte[MAX_LEN];
                Int32 readData = networkStream.Read(response, 0, response.Length);

                String getResponse = Encoding.UTF8.GetString(response, 0, readData);
                return getResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버와 연결이 실패된거같음");
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (networkStream != null)
                {
                    networkStream.Close();
                }
            }

            return null;
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

            NetworkStream networkStream = null;
            TcpClient client = null;
            
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
