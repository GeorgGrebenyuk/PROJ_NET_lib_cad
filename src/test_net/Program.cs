using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proj_wrapper;
using System.Globalization;

namespace test_net
{
    class Program
    {
        static LibraryImport lib;
        static void Main(string[] args)
        {
            DateTime t1 = DateTime.Now;
            lib = new LibraryImport();

            test_recalc();
            //test_get_info();
            //create_new();

            lib.Dispose();
            DateTime t2 = DateTime.Now;

            Console.WriteLine($"End!, time = {(t2-t1).TotalSeconds} s.");
            Console.ReadKey();

        }
        static void test_recalc()
        {
            DateTime start = DateTime.Now;
            string source_cs = "Russia-MSK1964";
            string target_cs = "WGS 84 / UTM zone 36N";//WGS 84    WGS 84 / UTM zone 36N
            string[] cs_1 = File.ReadAllLines(@"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\examples\points_4326-nerovnosti_945dc078-03c5-4990-be2f-b27ce73b1f36.txt");

            List<double[]> source_points = new List<double[]>();
            foreach (string cs_row in cs_1)
            {
                double[] coords_row = cs_row.Split(',').Select(a => Double.Parse(a, CultureInfo.InvariantCulture)).ToArray();

                source_points.Add(coords_row);
            }
            List<double[]> target_points = lib.transform_coords(source_cs, target_cs, source_points);
            StringBuilder SB = new StringBuilder();
            DateTime end = DateTime.Now;
            Console.WriteLine($"time = {(end-start).TotalSeconds} s.");
            foreach (double[] p in target_points)
            {
                //SB.AppendLine($"{p.x};{p.y}");
                Console.WriteLine($"x = {p[0]}\ty = {p[1]}\tz = {p[2]}");
            }
            //File.WriteAllText(@"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\examples\points_1964_2.csv", SB.ToString());

            //Console.WriteLine($"x = {test_1.x}\ty = {test_1.y}\tz = {test_1.z}");
        }
        static void test_get_info()
        {

            string source_cs = "Ukraine-SK63-X_Zona1";
            string info_wkt = lib.get_proj_as_wkt(source_cs, "3");
            Console.WriteLine("wkt= " + info_wkt);

            //string info_proj = lib.get_proj_as_proj(source_cs, "0");
            //Console.WriteLine("proj= " + info_proj);


            //List<string> crs_all = lib.get_crs_names(15);
            //foreach (string cs in crs_all)
            //{
            //    if (cs.Contains("Russia"))
            //    {

            //    }
            //    Console.WriteLine(cs);

            //}

        }
        static void create_new()
        {
            //string wkt_new_path = @"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\examples\test_wkt_new.txt";
            //string wkt_row = File.ReadAllText(wkt_new_path);
            //string errors = "";
            //bool success_trigger = lib.create_crs_by_wkt(wkt_row, ref errors);

            //Console.WriteLine(success_trigger + " " + errors);

            //string wkt_path = @"C:\Users\Georg\Documents\GitHub\MSK_for-Autodesk-Civil-3D\OtherData\Русские МСК_WKT.txt";
            //string[] all_wkts = File.ReadAllLines(wkt_path);
            //foreach (string str in all_wkts)
            //{
            //    string[] arr = str.Split('|');
            //    string errors = "";
            //    bool success_trigger = lib.create_crs_by_wkt(arr[2], ref errors);
            //    if (success_trigger == true)
            //    {
            //        Console.WriteLine($"ok created " + arr[1]);
            //    }
            //    else
            //    {
            //        Console.WriteLine($"crash for " + arr[1]);
            //        Console.WriteLine(errors + "\n\n");
            //    }
            //}

        }
    }
}
