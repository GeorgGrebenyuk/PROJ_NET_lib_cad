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
        [DllImport("proj_lib\\proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false,
            EntryPoint = "crs2crs_tranform")]
        private static extern point crs2crs_tranform(int source_cs, int target_cs,
            double source_x, double source_y, double source_z);
        public List<point> transform_coords(int source_cs, int target_cs, List<point> points)
        {
            List<point> recalced = new List<point>();
            foreach (point p in points)
            {
                recalced.Add(crs2crs_tranform(source_cs, target_cs, p.x, p.y, p.z));
            }
            return recalced;
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