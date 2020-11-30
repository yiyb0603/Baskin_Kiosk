using Baskin_Kiosk.Util;
using Baskin_Kiosk.ViewModel;
using System.Collections.Generic;

namespace Baskin_Kiosk.Model.DAO
{
    public class CsvOrderDAO
    {
        private readonly OrderViewModel viewModel = App.orderViewModel;

        public void InsertOrderDB()
        {
            CsvCommunication connection = new CsvCommunication();
            connection.GetConnection(Constants.CSV_PATH, "w");

            connection.csvWriter.Configuration.Delimiter = ", ";
            connection.csvWriter.Configuration.HasHeaderRecord = true;

            foreach (OrderModel order in viewModel.orderMenuList)
            {
                connection.csvWriter.WriteRecord(order);
                connection.csvWriter.NextRecord();
            }

            connection.CloseConnection();
        }

        public IEnumerable<OrderModel> ReadOrderDB()
        {
            CsvCommunication connection = new CsvCommunication();
            connection.GetConnection(Constants.CSV_PATH);

            connection.csvReader.Configuration.Delimiter = ", ";
            connection.csvReader.Configuration.HasHeaderRecord = true;

            IEnumerable<OrderModel> orderModel = connection.csvReader.GetRecords<OrderModel>();

            connection.CloseConnection();

            return orderModel;
        }

        public string GetTotalPrice()
        {
            int totalPrice = 0;
            CsvCommunication connection = new CsvCommunication();
            connection.GetConnection(Constants.CSV_PATH);

            connection.csvReader.Configuration.Delimiter = ", ";
            connection.csvReader.Configuration.HasHeaderRecord = true;

            while (connection.csvReader.Read())
            {
                totalPrice += int.Parse(connection.csvReader.GetField(4));
            }

            connection.CloseConnection();

            return totalPrice.ToString();
        }

    }
}
