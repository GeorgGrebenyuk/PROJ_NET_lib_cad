using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dr = Autodesk.DesignScript.Runtime;

namespace dyn_proj_library
{
    public class About
    {
        private About() { }
        /// <summary>
        /// Getting info about package as link to GitHub repository
        /// </summary>
        /// <returns></returns>
        public static string AboutPackage()
        {
            return "Look extended info for package at https://github.com/GeorgGrebenyuk/PROJ_NET_lib_cad/blob/main/dyn_ReadMe.md";
        }
        /// <summary>
        /// Checking, is proj crs database is installed in that PC
        /// </summary>
        /// <returns></returns>
        public static bool CkeckDatabaseInstalled()
        {
            if (System.IO.File.Exists(ProjDB_resources.db_path + "\\proj.db")) return true;
            else return false;
        }

    }
}
