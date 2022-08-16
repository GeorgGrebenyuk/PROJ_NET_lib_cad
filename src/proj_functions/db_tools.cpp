#include "pch.h"
#include "declare_functions.h"
#include "string"
//char** get_all_geodetic_crs()
//{
//
//}
PROJ_LIB_FUNCTIONS_API char* __stdcall get_proj_as_wkt(char* cs_name)
{
	PJ_CONTEXT* C;
	PJ* P;

	C = proj_context_create();
	P = proj_create(C, cs_name);

	const char* wkt_code = proj_as_wkt(C, P, PJ_WKT2_2019, NULL);
	proj_destroy(P);
	proj_context_destroy(C);
	return (char*)wkt_code;


}
PROJ_LIB_FUNCTIONS_API char* __stdcall get_proj_as_proj(char* cs_name)
{
	PJ_CONTEXT* C;
	PJ* P;

	C = proj_context_create();
	P = proj_create(C, cs_name);

	const char* proj_code = proj_as_proj_string (C, P, PJ_PROJ_4, NULL);
	proj_destroy(P);
	proj_context_destroy(C);
	return (char*)proj_code;
}

PROJ_LIB_FUNCTIONS_API int __stdcall get_all_crs_count(int include_mode)
{
	PJ_CONTEXT* C = proj_context_create();
	PROJ_CRS_LIST_PARAMETERS* parameters = proj_get_crs_list_parameters_create();
	const PJ_TYPE types[1] = { (PJ_TYPE)include_mode };
	parameters->types = types;
	int crs_counter = 0;
	PROJ_CRS_INFO** info_crs = proj_get_crs_info_list_from_database(C, NULL, parameters, &crs_counter);
	
	proj_get_crs_list_parameters_destroy(parameters);
	proj_crs_info_list_destroy(info_crs);
	proj_context_destroy(C);
	return crs_counter;
}
PROJ_LIB_FUNCTIONS_API void __stdcall get_crs_name
(int include_mode, int counter, OutString data)
{
	PJ_CONTEXT* C = proj_context_create();
	PROJ_CRS_LIST_PARAMETERS* parameters = proj_get_crs_list_parameters_create();
	const PJ_TYPE types[1] = { (PJ_TYPE)include_mode };

	parameters->types = types;
	int crs_counter = 0;
	PROJ_CRS_INFO** info_crs = proj_get_crs_info_list_from_database(C, NULL, parameters, &crs_counter);
	char* name = (info_crs[counter]->name);
	data(name);
	
	proj_get_crs_list_parameters_destroy(parameters);
	proj_crs_info_list_destroy(info_crs);
	proj_context_destroy(C);
	//return name;
}