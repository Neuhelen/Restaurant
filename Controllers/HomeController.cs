using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Resturant.Models;
using System.Data;
using System.Diagnostics;

using System.Linq;
using System.Xml.Linq;

namespace Resturant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            getdata();
            return View();
        }
        public IActionResult BookingForm()
        {
            return View();
        }
        public IActionResult BookingDone()
        {
            return View();
        }
        public IActionResult Menu()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private class Test
        {
            public int Idtest { get; set; }
            public string Test_ { get; set; }
            public int Number { get; set; }
            public bool Truefalse { get; set; }

            public Test(int idtest, string test, int number, bool truefalse)
            {
                Idtest = idtest;
                Test_ = test;
                Number = number;
                Truefalse = truefalse;
            }
        }

        private List<Test> getdata()
        {
            List<Test> tests = new List<Test>();
            MySqlConnection connect = new MySqlConnection("SERVER=sql.anders-jensen.dk; uid=G9WT; password=7niJwCe#XsyH!y2M4Fgw; database=resturant;");
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM resturant.test;");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            connect.Open();
            try
            {
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tests.Add(new Test(
                        dr.GetInt32("idtest"),
                        dr.GetString("test"),
                        dr.GetInt32("number"),
                        dr.GetBoolean("truefalse")
                    ));

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
            return tests;
        }
    }
}