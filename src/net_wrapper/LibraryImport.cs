using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace proj_wrapper
{
    /// <summary>
    /// Sctruct only for exporting data from extern cpp-function
    /// </summary>
    public struct point
    {
        /// <summary>
        /// X, meters = latitude in degrees for geodetic coordinates
        /// </summary>
        public double x;
        /// <summary>
        /// Y, meters = longitude in degrees for geodetic coordinates
        /// </summary>
        public double y;
        /// <summary>
        /// Z, meters - altitude
        /// </summary>
        public double z;
        public point(double x_input, double y_input, double z_input)
        {
            this.x = x_input;
            this.y = y_input;
            this.z = z_input;
        }
    }
    public class LibraryImport : IDisposable
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate void OutString(string value);

        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
            EntryPoint = "crs2crs_tranform")]
        private static extern point crs2crs_tranform(string source_cs, string target_cs, point p);
        /// <summary>
        /// Получение списка пересчитанных координат из одной системы в другую
        /// </summary>
        /// <param name="source_cs_name">Наименование исходной СК</param>
        /// <param name="target_cs_name">Наименование целевой СК</param>
        /// <param name="points">Список с точками (структура point этой библиотеки)</param>
        /// <returns></returns>
        public List<point> transform_coords(string source_cs_name, string target_cs_name, List<point> points)
        {
            List<point> recalced = new List<point>();
            foreach (point p in points)
            {
                recalced.Add(crs2crs_tranform(source_cs_name, target_cs_name, p));
            }
            return recalced;
        }
        //getting cs info
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "get_proj_as_wkt")]
        private static extern int getting_proj_as_wkt(char[] cs_name, OutString result, int type); //, int type
        /// <summary>
        /// Получение WKT-кода для данного строчного наименования СК
        /// </summary>
        /// <param name="cs_name">Строчное наименование СК</param>
        /// <param name="type">Тип вывода WKT-кода: WKT2_2015 = 0; WKT2_2015_SIMPLIFIED = 1;
        /// WKT2_2019, WKT2_2018 = 2; WKT2_2019_SIMPLIFIED, WKT2_2018_SIMPLIFIED = 3;
        /// WKT1_GDAL = 4; WKT1_ESRI = 5</param>
        /// <returns></returns>
        public string get_proj_as_wkt(string cs_name, string type_str) //, int type
        {
            int type = 0;
            if (type_str == "WKT1_GDAL") type = 4;
            else if (type_str == "WKT1_ESRI") type = 5;
            else if (type_str == "WKT2_2015") type = 0;
            else if (type_str == "WKT2_2015_SIMPLIFIED") type = 1;
            else if (type_str == "WKT2_2019") type = 2;
            else if (type_str == "WKT2_2018") type = 2;
            else if (type_str == "WKT2_2019_SIMPLIFIED") type = 3;
            else if (type_str == "WKT2_2018_SIMPLIFIED") type = 3;

            string result = "-";
            int responce = getting_proj_as_wkt(cs_name.ToCharArray(), a=> result = a, type);
            return result;
        }
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "get_proj_as_proj")]
        private static extern int getting_proj_as_proj(string cs_name, OutString result, int type); //
        /// <summary>
        /// Получение PROJ4-кода для данного строчного наименования СК
        /// </summary>
        /// <param name="cs_name">Строчное наименование СК</param>
        /// <param name="type">Тип вывода PROJ-кода: PROJ_5 = 0; PROJ_4 = 1</param>
        /// <returns></returns>
        public string get_proj_as_proj(string cs_name, string type_str) //, int type
        {
            int type = 0;
            if (type_str == "PROJ_5") type = 0;
            else if (type_str == "PROJ_4") type = 1;
            string result = "-";
            int responce = getting_proj_as_proj(cs_name, a => result = a, type);
            return result;
        }
        //for getting crs_names
        
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "get_all_crs_names")]
        private static extern int geting_all_crs_names(int include_mode, string file_path);
        /// <summary>
        /// Получение наименований всех систем координат в базе данных
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public List<string> get_crs_names(int mode)
        {
            string temp_path = Path.GetTempFileName();
            int wait_process = geting_all_crs_names(mode, temp_path);
            List<string> names = File.ReadAllLines(temp_path).ToList();
            //File.Delete(temp_path);
            return names;
        }
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "create_crs_by_wkt")]
        private static extern int creation_crs_by_wkt(string wkt, OutString errors);
        /// <summary>
        /// Позволяет создать новое определение системы координат по строчному WKT-представлению.
        /// </summary>
        /// <param name="wkt">Строчное WKT-представление</param>
        /// <param name="errors">Список с ошибками (опциональный)</param>
        /// <returns>Список с ошибками если они были, или "-" если всё удачно</returns>
        public bool create_crs_by_wkt (string wkt, ref string errors_out)
        {
            string temp_path = Path.GetTempFileName();
            string errors = "";
            int result = creation_crs_by_wkt(wkt, a => errors = a);
            errors_out = errors;
            if (result == 1) return true;
            else return false;
        }










        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                // Освобождаем управляемые ресурсы
            }
            disposed = true;
        }
        ~LibraryImport()
        {
            Dispose(false);
        }

    }
}