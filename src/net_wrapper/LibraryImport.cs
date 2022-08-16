using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace proj_lib
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
        private static extern string getting_proj_as_wkt(string cs_name);
        /// <summary>
        /// Получение WKT-кода для данного строчного наименования СК
        /// </summary>
        /// <param name="cs_name">Строчное наименование СК</param>
        /// <returns></returns>
        public string get_proj_as_wkt(string cs_name)
        {
            return getting_proj_as_wkt(cs_name);
        }
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "get_proj_as_proj")]
        private static extern string getting_proj_as_proj(string cs_name);
        /// <summary>
        /// Получение PROJ4-кода для данного строчного наименования СК
        /// </summary>
        /// <param name="cs_name">Строчное наименование СК</param>
        /// <returns></returns>
        public string get_proj_as_proj(string cs_name)
        {
            return getting_proj_as_proj(cs_name);
        }
        //for getting crs_names
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "get_all_crs_count")]
        private static extern int geting_all_crs_count(int include_mode);
        /// <summary>
        /// Получение числа определений СК в базе данного типа (таблицы в proj.db)
        /// </summary>
        /// <param name="include_mode"></param>
        /// <returns></returns>
        private int get_all_crs_count (int include_mode)
        {
            return geting_all_crs_count(include_mode);
        }
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "get_crs_name")]
        private static extern void getting_crs_name(int include_mode, int counter, OutString data);
        /// <summary>
        /// Получение наименований всех СК входящих в базу данных из определенной таблицы
        /// </summary>
        /// <param name="mode">15 = projected_crs; 9 = geodetic_crs</param>
        /// <returns></returns>
        public List<string> get_crs_names (int mode)
        {
            //PJ_TYPE_PROJECTED_CRS = 15
            //PJ_TYPE_GEODETIC_CRS = 9
            List<string> crs = new List<string>();
            int count_of_crs = this.get_all_crs_count(mode);
            for (int i = 0; i < 50; i ++)
            {
                string name = "-";
                getting_crs_name(mode, i, data=> name = data);
                crs.Add(name);
            }
            return crs;
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