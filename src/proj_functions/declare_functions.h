#pragma once
/// <summary>
/// Sctruct only for exporting data from extern-function
/// </summary>
struct point {
public:
	/// <summary>
	/// X, meters = latitude in degrees for geodetic coordinates
	/// </summary>
	double x;
	/// <summary>
	/// Y, meters = longitude in degrees for geodetic coordinates
	/// </summary>
	double y;
	/// <summary>
	/// Z, meters - altitude
	/// </summary>
	double z;
	point(double x, double y, double z) {
		this->x = x;
		this->y = y;
		this->z = z;
	}
};
#ifdef PROJ_LIB_FUNCTIONS
#define PROJ_LIB_FUNCTIONS_API __declspec(dllexport)
#else
#define PROJ_LIB_FUNCTIONS_API __declspec(dllimport)
#endif
//typedef void(__stdcall* LogMessage)(const char*); -- for some log message .. or not
extern "C" point PROJ_LIB_FUNCTIONS_API __stdcall crs2crs_tranform
(int source_cs_code, int target_cs_code,
	double source_x, double source_y, double source_z);