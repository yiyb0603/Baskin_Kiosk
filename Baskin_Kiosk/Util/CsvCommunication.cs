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
            FileStream fs = new FileStream(address, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        }

        public void GetConnection(string address, string openMode)
        {
            if (openMode.Equals("w"))
            {
                FileStream fs = new FileStream(address, FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs);
                csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            }
            else if (openMode.Equals("r"))
            {
                FileStream fs = new FileStream(address, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fs);
                csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            }
        }

        public void CloseConnection()
        {
            if (csvReader != null)
            {
                csvReader.Dispose();
            }
            else if (csvWriter != null)
            {
                csvWriter.Dispose();
            }
        }
    }
}
