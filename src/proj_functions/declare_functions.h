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
#define PROJ_LIB_FUNCTIONS_API __declspec(dllexport)
//#ifdef PROJ_LIB_FUNCTIONS
//#define PROJ_LIB_FUNCTIONS_API __declspec(dllexport)
//#else
//#define PROJ_LIB_FUNCTIONS_API __declspec(dllimport)
//#endif
//typedef void(__stdcall* LogMessage)(const char*); -- for some log message .. or not

//extern "C" char** PROJ_LIB_FUNCTIONS_API __stdcall get_all_projected_crs();
//extern "C" char** PROJ_LIB_FUNCTIONS_API __stdcall get_all_geodetic_crs();

typedef void(__stdcall* OutString)(const char*);
extern "C"
{
	//proj_create_from реализовать потом для выборки повторяющихся имен в БД
	PROJ_LIB_FUNCTIONS_API point __stdcall crs2crs_tranform
	(char* source_cs_name, char* target_cs_name, point source_point);

	PROJ_LIB_FUNCTIONS_API char* __stdcall get_proj_as_wkt(char* cs_name, int type);
	PROJ_LIB_FUNCTIONS_API char* __stdcall get_proj_as_proj(char* cs_name, int type);

	PROJ_LIB_FUNCTIONS_API int __stdcall get_all_crs_names
	(int include_mode, char* file_path);
	PROJ_LIB_FUNCTIONS_API char* __stdcall create_crs_by_wkt(char* wkt);

}

	
