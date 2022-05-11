#pragma once
//structs
struct point {
	double x;
	double y;
	double z;
};
#ifdef PROJ_LIB_FUNCTIONS
#define PROJ_LIB_FUNCTIONS_API __declspec(dllexport)
#else
#define PROJ_LIB_FUNCTIONS_API __declspec(dllimport)
#endif
#include <cstdint>
//typedef void(__stdcall* LogMessage)(const char*);

//extern "C" int32_t PROJ_LIB_FUNCTIONS_API __stdcall crs2crs_tranform(const char* source_cs, const char* target_cs,
//	double source_x, double source_y, double source_z);
extern "C" point PROJ_LIB_FUNCTIONS_API __stdcall crs2crs_tranform(const char* source_cs, const char* target_cs,
	double source_x, double source_y, double source_z);