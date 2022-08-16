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
        static void Main(string[] args)
        {
            DateTime t1 = DateTime.Now;
            LibraryImport lib = new LibraryImport();
            int source_cs = 100564;
            int target_cs = 4326;
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
            lib.Dispose();
            DateTime t2 = DateTime.Now;

            Console.WriteLine($"End!, time = {(t2-t1).TotalSeconds} s.");
            Console.ReadKey();

        }
    }
}
