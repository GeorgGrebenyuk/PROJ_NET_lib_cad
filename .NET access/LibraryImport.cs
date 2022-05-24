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
    public static class LibraryImport
    {
        
        public static ILibraryImport Select()
        {
            if (IntPtr.Size != 4) // 32-bit application
            {
                return new LibraryImport_x64();
            }
            else // 64-bit application
            {
                return null;
            }
        }
    }
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void LogMessage(string value);

    public interface ILibraryImport
    {
        point crs2crs_tranform(int source_cs, int target_cs, 
            double source_x, double source_y, double source_z);

    }
    internal class LibraryImport_x64 : ILibraryImport
    {
        [DllImport(@"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\src\x64\Debug\proj_functions_x64",
            CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "crs2crs_tranform")]
        private static extern point crs2crs_tranform_internal(int source_cs, int target_cs, 
            double source_x, double source_y, double source_z);

        public point crs2crs_tranform(int source_cs, int target_cs, 
            double source_x, double source_y, double source_z)
        {
            return crs2crs_tranform_internal(source_cs, target_cs, source_x, source_y, source_z);
        }
    }
}