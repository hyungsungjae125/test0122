using System;
using System.Collections;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace test0122
{
    public class DataBase
    {
        private MySqlConnection connection;
        private bool status;

        public DataBase()
        {
            status = Connection();
        }

        private bool Connection()
        {
            string path = "/public/DBinfo.json";
            string result = new StreamReader(File.OpenRead(path)).ReadToEnd();
            JObject jo = JsonConvert.DeserializeObject<JObject>(result);
            Hashtable map = new Hashtable();
            foreach (JProperty jp in jo.Properties())
            {
                map.Add(jp.Name, jp.Value);
                Console.WriteLine("{0} , {1}", jp.Name, jp.Value);
            }
            string connStr = string.Format("server={0};user={1};password={2};database={3}", map["server"].ToString(), map["user"].ToString(), map["password"].ToString(), map["database"].ToString());//strConnection1;
            Console.WriteLine("DB접속정보 ==> "+connStr);
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                this.connection = conn;
                Console.WriteLine("--------------------연결성공----------------------");
                return true;

            }
            catch
            {
                conn.Close();
                this.connection = null;
                Console.WriteLine("XXXXXXXXXXXXXXXXXXX연결실패XXXXXXXXXXXXXXXXXXXX");
                return false;
            }
        }

        public void Close()
        {
            if (status)
            {
                connection.Close();
            }
        }

        public MySqlDataReader Reader(string sql)
        {
            if (status)
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText = sql;
                    comm.Connection = connection;

                    return comm.ExecuteReader();
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        public bool ReaderClose(MySqlDataReader sdr)
        {
            try
            {
                sdr.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool NonQuery(string sql, Hashtable ht)
        {
            if (status)
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText = sql;
                    comm.Connection = connection;

                    foreach (DictionaryEntry data in ht)
                    {
                        comm.Parameters.AddWithValue(data.Key.ToString(), data.Value);
                    }
                    int cnt = comm.ExecuteNonQuery();
                    Console.WriteLine("------------------>>>>>>>>" + cnt);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}