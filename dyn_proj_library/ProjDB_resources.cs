using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using dr = Autodesk.DesignScript.Runtime;


namespace dyn_proj_library
{
    /// <summary>
    /// List of cs in PROJ database
    /// </summary>
    public class ProjDB_resources
    {
        private ProjDB_resources() { }
        [dr.IsVisibleInDynamoLibrary(false)]
        public static string db_path = $@"C:\Users\{Environment.UserName}\AppData\Local\proj";

        /// <summary>
        /// Getting info about all projected coordinate systems
        /// </summary>
        /// <returns>Dictionary with code and name of CS</returns>
        [dr.MultiReturn(new[] { "EPSG codes", "CS names" })]
        public static Dictionary<string,object > get_projected_crs_list()
        {
            List<int> codes = new List<int>();
            List<string> names = new List<string>();
            get_data(db_path + "\\projected_crs_list.txt", ref codes, ref names);
            return new Dictionary<string, object>()
            {
                {"EPSG codes",codes },
                {"CS names", names }
            };
        }
        /// <summary>
        /// Getting info about all projected (Russian) coordinate systems
        /// </summary>
        /// <returns>Dictionary with code and name of CS</returns>
        [dr.MultiReturn(new[] { "EPSG codes", "CS names" })]
        public static Dictionary<string, object> get_projected_russian_crs_list()
        {
            List<int> codes = new List<int>();
            List<string> names = new List<string>();
            get_data(db_path + "\\projected_russian_crs_list.txt", ref codes, ref names);
            return new Dictionary<string, object>()
            {
                {"EPSG codes",codes },
                {"CS names", names }
            };
        }
        /// <summary>
        /// Getting info about all geodetic coordinate systems
        /// </summary>
        /// <returns>Dictionary with code and name of CS</returns>
        [dr.MultiReturn(new[] { "EPSG codes", "CS names" })]
        public static Dictionary<string, object> get_geodetic_crs_list()
        {
            List<int> codes = new List<int>();
            List<string> names = new List<string>();
            get_data(db_path + "\\geodetic_crs_list.txt", ref codes, ref names);
            return new Dictionary<string, object>()
            {
                {"EPSG codes",codes },
                {"CS names", names }
            };
        }
        private static void get_data(string path_file, ref List<int> codes, ref List<string> names)
        {
            List<string> row_data = File.ReadAllLines(path_file).ToList();
            foreach (string str in row_data)
            {
                string[] data_array = str.Split('\t');
                codes.Add(Convert.ToInt32(data_array[1]));
                names.Add(Convert.ToString(data_array[0]));
            }
        }


    }
}
