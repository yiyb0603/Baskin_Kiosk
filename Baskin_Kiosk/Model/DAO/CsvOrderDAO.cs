using Baskin_Kiosk.Util;
using Baskin_Kiosk.ViewModel;
using System.Collections.Generic;

namespace Baskin_Kiosk.Model.DAO
{
    public class CsvOrderDAO : IOrderDB
    {
        private readonly OrderViewModel viewModel = App.orderViewModel;
        private readonly CsvCommunication connection = new CsvCommunication();

        public void InsertOrderDB()
        {
            connection.GetConnection(Constants.CSV_PATH);

            foreach (OrderModel order in viewModel.orderMenuList)
            {
                connection.GetConnection(Constants.CSV_PATH);

                connection.csvWriter.Configuration.Delimiter = ",";
                connection.csvWriter.Configuration.HasHeaderRecord = true;
                connection.csvWriter.Configuration.AutoMap<OrderModel>();

                connection.csvWriter.WriteHeader<OrderModel>();
                connection.csvWriter.WriteRecords((System.Collections.IEnumerable)order);
            }

            connection.CloseConnection();
        }

        public List<OrderModel> ReadOrderDB()
        {
            connection.GetConnection(Constants.CSV_PATH);

            List<OrderModel> orderModel = (List<OrderModel>)connection.csvReader.GetRecords<OrderModel>();

            return orderModel;
        }

        public string GetTotalPrice()
        {
            int totalPrice = 0;
            connection.GetConnection(Constants.CSV_PATH);

            while (connection.csvReader.Read())
            {
                totalPrice += int.Parse(connection.csvReader.GetField(4));
            }

            return totalPrice.ToString();
        }

    }
}
