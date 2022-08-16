using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proj_lib;

namespace test_net
{
    class Program
    {
        static LibraryImport lib;
        static void Main(string[] args)
        {
            DateTime t1 = DateTime.Now;
            lib = new LibraryImport();

            //test_recalc();
            //test_get_info();
            create_new();

            lib.Dispose();
            DateTime t2 = DateTime.Now;

            Console.WriteLine($"End!, time = {(t2-t1).TotalSeconds} s.");
            Console.ReadKey();

        }
        static void test_recalc()
        {
            string source_cs = "Russia-MSK1964";
            string target_cs = "WGS 84";
            string[] cs_1 = File.ReadAllLines(@"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\examples\points_1964_1.csv");

            List<point> source_points = new List<point>();
            foreach (string cs_row in cs_1)
            {
                double[] coords_row = cs_row.Split(',').Select(a => Double.Parse(a)).ToArray();
                source_points.Add(new point(coords_row[0], coords_row[1], 0));
            }
            List<point> target_points = lib.transform_coords(source_cs, target_cs, source_points);
            StringBuilder SB = new StringBuilder();
            foreach (point p in target_points)
            {
                //SB.AppendLine($"{p.x};{p.y}");
                Console.WriteLine($"x = {p.x}\ty = {p.y}\tz = {p.z}");
            }
            //File.WriteAllText(@"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\examples\points_1964_2.csv", SB.ToString());

            //Console.WriteLine($"x = {test_1.x}\ty = {test_1.y}\tz = {test_1.z}");
        }
        static void test_get_info()
        {
            string source_cs = "Russia-MSK1964";
            string info = lib.get_proj_as_wkt(source_cs, 1);
            Console.WriteLine(info);


            List<string> crs_all = lib.get_crs_names(9);
            foreach (string cs in crs_all)
            {
                Console.WriteLine(cs);
            }
        }
        static void create_new()
        {
            string wkt_new_path = @"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\examples\test_wkt_new.txt";
            string wkt_row = File.ReadAllText(wkt_new_path);
            //string errors = lib.create_crs_by_wkt(wkt_row);
            
            //Console.WriteLine(errors);
        }
    }
}
