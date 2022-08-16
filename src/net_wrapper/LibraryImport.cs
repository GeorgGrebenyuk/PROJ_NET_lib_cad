﻿using System;
using System.Linq;
using System.IO;
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
        private static extern string getting_proj_as_wkt(string cs_name, int type);
        /// <summary>
        /// Получение WKT-кода для данного строчного наименования СК
        /// </summary>
        /// <param name="cs_name">Строчное наименование СК</param>
        /// <param name="type">Тип вывода WKT-кода: WKT2_2015 = 0; WKT2_2015_SIMPLIFIED = 1;
        /// WKT2_2019, WKT2_2018 = 2; WKT2_2019_SIMPLIFIED, PJ_WKT2_2019_SIMPLIFIED = 3;
        /// WKT1_GDAL = 4; WKT1_ESRI = 5</param>
        /// <returns></returns>
        public string get_proj_as_wkt(string cs_name, int type)
        {
            return getting_proj_as_wkt(cs_name, type);
        }
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "get_proj_as_proj")]
        private static extern string getting_proj_as_proj(string cs_name, int type);
        /// <summary>
        /// Получение PROJ4-кода для данного строчного наименования СК
        /// </summary>
        /// <param name="cs_name">Строчное наименование СК</param>
        /// <param name="type">Тип вывода PROJ-кода: PROJ_5 = 0; PROJ_4 = 1</param>
        /// <returns></returns>
        public string get_proj_as_proj(string cs_name, int type)
        {
            return getting_proj_as_proj(cs_name, type);
        }
        //for getting crs_names
        
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "get_all_crs_names")]
        private static extern int geting_all_crs_names(int include_mode, string file_path);
        public List<string> get_crs_names(int mode)
        {
            string temp_path = Path.GetTempFileName();
            int wait_process = geting_all_crs_names(mode, temp_path);
            List<string> names = File.ReadAllLines(temp_path).ToList();
            File.Delete(temp_path);
            return names;
        }
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
        EntryPoint = "create_crs_by_wkt")]
        private static extern string creation_crs_by_wkt(string wkt);
        /// <summary>
        /// Позволяет создать новое определение системы координат по строчному WKT-представлению.
        /// </summary>
        /// <param name="wkt">Строчное WKT-представление</param>
        /// <param name="errors">Список с ошибками (опциональный)</param>
        /// <returns>Список с ошибками если они были, или "-" если всё удачно</returns>
        private string create_crs_by_wkt (string wkt)
        {
            string temp_path = Path.GetTempFileName();
            string result = creation_crs_by_wkt(wkt);

            //bool result_status;
            //if (result == 0) 
            //{
            //    result_status = false;
            //    //errors = errors_output;
            //}
            //else result_status = true;
            return result;
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