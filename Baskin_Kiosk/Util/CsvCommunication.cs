using CsvHelper;
using System.Globalization;
using System.IO;

namespace Baskin_Kiosk.Util
{
    class CsvCommunication : IDB
    {
        public CsvReader csvReader = null;
        public CsvWriter csvWriter = null;

        public void GetConnection(string address)
        {
            using var reader = new StreamReader(address);
            csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            using var writer = new StreamWriter(address);
            csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
        }

        public void CloseConnection()
        {
            csvReader.Dispose();
            csvWriter.Dispose();
        }
    }
}
