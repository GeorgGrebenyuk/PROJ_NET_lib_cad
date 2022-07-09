using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dr = Autodesk.DesignScript.Runtime;
using dg = Autodesk.DesignScript.Geometry;
namespace dyn_proj_library
{
    /// <summary>
    /// Class for data reproject
    /// </summary>
    public class Reprojecting
    {
        public point recalced;
        /// <summary>
        /// Reproject data (point) by codes of start and target coordinate system (EPSG code)
        /// and coordinates of point
        /// </summary>
        /// <param name="cs_source_code">integer value of EPSG code for source coordinate system</param>
        /// <param name="cs_target_code">integer value of EPSG code for target coordinate system</param>
        /// <param name="coord_x">double-value of X-coordinate (too latitude)</param>
        /// <param name="coord_y">double-value of Y-coordinate (too longitude)</param>
        /// <param name="coord_z">double-value of Z-coordinate (too altitude)</param>
        public Reprojecting (int cs_source_code, int cs_target_code, double coord_x, double coord_y, double coord_z)
        {
            ILibraryImport lib_wrapper = LibraryImport.Select();
            this.recalced = lib_wrapper.crs2crs_tranform(cs_source_code, cs_target_code, coord_x, coord_y, coord_z);
        }
        /// <summary>
        /// Getting reproject point as three coordinates (double-values)
        /// </summary>
        /// <returns></returns>
        [dr.MultiReturn(new[] {"X","Y","Z"})]
        public Dictionary<string,double> get_XYZ()
        {
            return new Dictionary<string, double>()
            {
                {"X",this.recalced.x },
                {"Y",this.recalced.y },
                {"Z",this.recalced.z }
            };
        }
        /// <summary>
        /// Getting reproject point as Dynamo geometry (Point)
        /// </summary>
        /// <returns></returns>
        public dg.Point get_DynamoPoint()
        {
            return dg.Point.ByCoordinates(this.recalced.x, this.recalced.y, this.recalced.z);
        }
    }
}
