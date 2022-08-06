using System;
using System.Runtime.InteropServices;

namespace test_net
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
        public LibraryImport() { }
        [DllImport("proj_functions_x64", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "crs2crs_tranform")]
        private static extern point crs2crs_tranform_internal(int source_cs, int target_cs,
            double source_x, double source_y, double source_z);

        public point crs2crs_tranform(int source_cs, int target_cs,
            double source_x, double source_y, double source_z)
        {
            return crs2crs_tranform_internal(source_cs, target_cs, source_x, source_y, source_z);
        }
        private bool disposed = false;

        // реализация интерфейса IDisposable.
        public void Dispose()
        {
            // освобождаем неуправляемые ресурсы
            Dispose(true);
            // подавляем финализацию
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                // Освобождаем управляемые ресурсы
            }
            // освобождаем неуправляемые объекты
            disposed = true;
        }

        // Деструктор
        ~LibraryImport()
        {
            Dispose(false);
        }

    }
}