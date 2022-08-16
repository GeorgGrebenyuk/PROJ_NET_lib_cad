#include "pch.h"
#include "declare_functions.h"
//#include "string"
#include <vector>
#include <iostream>
#include <fstream>
//char** get_all_geodetic_crs()
//{
//
//}
PROJ_LIB_FUNCTIONS_API char* __stdcall get_proj_as_wkt(char* cs_name, int type)
{
	PJ_CONTEXT* C;
	PJ* P;

	C = proj_context_create();
	P = proj_create(C, cs_name);

	const char* wkt_code = proj_as_wkt(C, P, (PJ_WKT_TYPE)type, NULL);
	proj_destroy(P);
	proj_context_destroy(C);
	return (char*)wkt_code;


}
PROJ_LIB_FUNCTIONS_API char* __stdcall get_proj_as_proj(char* cs_name, int type)
{
	PJ_CONTEXT* C;
	PJ* P;

	C = proj_context_create();
	P = proj_create(C, cs_name);

	const char* proj_code = proj_as_proj_string (C, P, (PJ_PROJ_STRING_TYPE)type, NULL);
	proj_destroy(P);
	proj_context_destroy(C);
	return (char*)proj_code;
}

PROJ_LIB_FUNCTIONS_API int __stdcall get_all_crs_names
(int include_mode, char* file_path)
{
	PJ_CONTEXT* C = proj_context_create();
	PROJ_CRS_LIST_PARAMETERS* parameters = proj_get_crs_list_parameters_create();
	const PJ_TYPE types[1] = { (PJ_TYPE)include_mode };

	parameters->types = types;
	int crs_counter = 0;
	PROJ_CRS_INFO** info_crs = proj_get_crs_info_list_from_database(C, NULL, parameters, &crs_counter);
	std::vector<char*> names;
	for (int i = 0; i < crs_counter; i++)
	{
		names.push_back(info_crs[i]->name);
	}
	std::ofstream out;
	out.open(file_path);
	if (out.is_open())
	{
		for (char* name : names) { out << name << std::endl; }
	}
	out.close();
	return 1;
}