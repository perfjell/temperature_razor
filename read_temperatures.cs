using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace temperature_razor_2 {
  public struct TemperaturData {
    public int farsund;
    public int smeaheia;
    public string tid;
    public string timestamp;
    public int farsund_id;
    public int smeaheia_id;
  }
}

namespace temperature_razor_2 {
  public class Temperatures {
    
    public static List<TemperaturData> load_temperatures() {
      var cb = new SqlConnectionStringBuilder();
      cb.DataSource = "farsund.database.windows.net";
      cb.UserID = "perfjell";
      cb.Password = "ZrNgqLCwS8II";
      cb.InitialCatalog = "temperatur";
      List<TemperaturData> temperature_collection = new List<TemperaturData>();
      using(var connection = new SqlConnection(cb.ConnectionString)) {
        connection.Open();
        
        SqlCommand  cmd = new SqlCommand("select * from farsund_smeaheia order by timestamp desc");
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = connection;
        SqlDataReader reader = cmd.ExecuteReader();
        TemperaturData tmp = new TemperaturData();
        while(reader.Read()) {
          tmp.farsund = Int32.Parse(reader["Farsund"].ToString());
          tmp.smeaheia = Int32.Parse(reader["Smeaheia"].ToString());
          tmp.tid =reader["Tid"].ToString();
          tmp.timestamp = reader["Timestamp"].ToString() ;
          tmp.farsund_id = Int32.Parse(reader["farsund_id"].ToString());
          tmp.smeaheia_id = Int32.Parse(reader["smeaheia_id"].ToString());
          temperature_collection.Add(tmp);
        }
      }
      // test
      foreach (TemperaturData data in temperature_collection) {
        System.Console.WriteLine( data.farsund);
      }
       
      //
      return temperature_collection;
    }
  }
}