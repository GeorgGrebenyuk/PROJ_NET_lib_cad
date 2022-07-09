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
            LibraryImport lib_wrapper = new LibraryImport();
            this.recalced = lib_wrapper.crs2crs_tranform(cs_source_code, cs_target_code, coord_x, coord_y, coord_z);
            lib_wrapper.Dispose();
        }
        /// <summary>
        /// Recalc dynapo point by local offset-point and rotation angle (f.e. for Revit base point)
        /// </summary>
        /// <param name="dx">Offset for x-coordinate</param>
        /// <param name="dy">Offset for y-coordinate</param>
        /// <param name="dz">Offset for z-coordinate</param>
        /// <param name="scale">Coefficient to coordinate (for unit's translations)</param>
        /// <param name="angle_value"></param>
        /// <param name="source_point"></param>
        public Reprojecting (double dx, double dy, double dz, double angle_value, double scale, dg.Point source_point)
        {
            double x = source_point.X;
            double y = source_point.Y;
            double z = source_point.Z;
            x -= dx;
            y -= dy;
            z -= dz;

            x = x * Math.Cos(angle_value) - y * Math.Sin(angle_value);
            y = x * Math.Sin(angle_value) + y * Math.Cos(angle_value);
            this.recalced = new point(x * scale, y * scale, z * scale);
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
        //For reproject Dynamo geometry
        /// <summary>
        /// Reproject dynamo point
        /// </summary>
        /// <param name="cs_source_code">integer value of EPSG code for source coordinate system<</param>
        /// <param name="cs_target_code">integer value of EPSG code for target coordinate system<</param>
        /// <param name="source_point"></param>
        /// <returns></returns>
        public static dg.Point Reproject_dynamo_point(int cs_source_code, int cs_target_code, dg.Point source_point)
        {
            LibraryImport lib_wrapper = new LibraryImport();
            point recalced = lib_wrapper.crs2crs_tranform(cs_source_code, cs_target_code, source_point.X, source_point.Y, source_point.Z);
            lib_wrapper.Dispose();
            return dg.Point.ByCoordinates(recalced.x, recalced.y, recalced.z);
        }
        /// <summary>
        /// Too as Reproject_dynamo_point
        /// </summary>
        public static dg.Point Reproject_point_as_double_array(int cs_source_code, int cs_target_code, List<double> source_point)
        {
            using (LibraryImport lib_wrapper = new LibraryImport())
            {
                point recalced = lib_wrapper.crs2crs_tranform(cs_source_code, cs_target_code, source_point[0], source_point[1], source_point[2]);
                lib_wrapper.Dispose();
                return dg.Point.ByCoordinates(recalced.x, recalced.y, recalced.z);
            }
        }
        /// <summary>
        /// Reproject dynamo polycurves and polygons
        /// </summary>
        /// <param name="cs_source_code">integer value of EPSG code for source coordinate system<</param>
        /// <param name="cs_target_code">integer value of EPSG code for target coordinate system<</param>
        /// <param name="source_polycurve">Dynamo geometry - polycurve, polygons</param>
        /// <returns></returns>
        public static dg.PolyCurve Reproject_dynamo_polycurve (int cs_source_code, int cs_target_code, dg.PolyCurve source_polycurve)
        {
            List<dg.Point> points = new List<dg.Point>();
            foreach (dg.Curve curve in source_polycurve.Curves())
            {

                dg.Point p1 = Reproject_dynamo_point(cs_source_code, cs_target_code, curve.StartPoint);
                dg.Point p2 = Reproject_dynamo_point(cs_source_code, cs_target_code, curve.EndPoint);
                if (!points.Contains(p1)) points.Add(p1);
                if (!points.Contains(p2)) points.Add(p2);
            }
            return dg.PolyCurve.ByPoints(points);
        }

        
    }
}
