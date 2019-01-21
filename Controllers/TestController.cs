using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace test0122.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [Route("select")]
        [HttpGet]
        public ActionResult<ArrayList> Get()
        {
            DataBase db = new DataBase();
            string sql ="select n.nNo,m.mName,n.nTitle,n.nContents,n.regDate,n.modDate from Notice as n left join Member as m on (n.mNo=m.mNo) where n.delYn='N';";
            MySqlDataReader sdr = db.Reader(sql);
            ArrayList list = new ArrayList();
            while(sdr.Read()){
                list.Add(new string[]{sdr["nNo"].ToString(),sdr["nTitle"].ToString(),sdr["nContents"].ToString(),sdr["mName"].ToString(),sdr["regDate"].ToString(),sdr["modDate"].ToString()});
            }
            db.ReaderClose(sdr);
            db.Close();
            return list;
        }

        [Route("insert")]
        [HttpPost]
        public void Insert([FromForm] string nTitle,[FromForm] string nContents,[FromForm] string mName)
        {

        }
        [Route("update")]
        [HttpPost]
        public void Update([FromForm] string nNo,[FromForm] string nTitle,[FromForm] string nContents)
        {
            
        }
        [Route("delete")]
        [HttpPost]
        public void Delete([FromForm] string nNo)
        {
            
        }
    }
}
