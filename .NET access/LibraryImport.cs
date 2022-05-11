using System;
using System.Runtime.InteropServices;

namespace test_net
{
    public static class LibraryImport
    {
        public static string lib_path = @"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\src\x64\Debug\proj_functions_x64";
        public static ILibraryImport Select()
        {
            if (IntPtr.Size == 4) // 32-bit application
            {
                return new LibraryImport_x86();
            }
            else // 64-bit application
            {
                return new LibraryImport_x64();
            }
        }
    }
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void LogMessage(string value);

    public interface ILibraryImport
    {
        point crs2crs_tranform(char[] source_cs, char[] target_cs, double source_x, double source_y, double source_z);

    }
    internal class LibraryImport_x64 : ILibraryImport
    {
        [DllImport(@"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\src\x64\Debug\proj_functions_x64",
            CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "crs2crs_tranform")]
        private static extern point crs2crs_tranform_internal(char[] source_cs, char[] target_cs, double source_x, double source_y, double source_z);


        public point crs2crs_tranform(char[] source_cs, char[] target_cs, double source_x, double source_y, double source_z)
        {
            return crs2crs_tranform_internal(source_cs, target_cs, source_x, source_y, source_z);
        }
    }
    internal class LibraryImport_x86 : ILibraryImport
    {
        [DllImport(@"C:\Users\Georg\Documents\GitHub\PROJ_NET_lib_cad\src\x64\Debug\proj_functions_x86",
            CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "crs2crs_tranform")]
        private static extern point crs2crs_tranform_internal(char[] source_cs, char[] target_cs, double source_x, double source_y, double source_z);

        public point crs2crs_tranform(char[] source_cs, char[] target_cs, double source_x, double source_y, double source_z)
        {
            return crs2crs_tranform_internal(source_cs, target_cs, source_x, source_y, source_z);
        }
    }
}
