#include "pch.h"
#include "declare_functions.h"
//#include "string"
#include <vector>
#include <iostream>
#include <fstream>
#include <sstream>
//char** get_all_geodetic_crs()
//{
//
//}
PROJ_LIB_FUNCTIONS_API int __stdcall get_proj_as_wkt(char* cs_name, OutString result, int type)
{
	int result_status = 0;
	std::string err = "error";
	PJ_CONTEXT* C;
	PJ* P;
	std::cout << "input code= " << cs_name << std::endl;

	C = proj_context_create();
	P = proj_create(C, cs_name);
	if (0 == P) return result_status;
	result_status = 1;

	result(proj_as_wkt(C, P, (PJ_WKT_TYPE)type, NULL));

	proj_destroy(P);
	proj_context_destroy(C);

	return result_status;
}
PROJ_LIB_FUNCTIONS_API int __stdcall get_proj_as_proj(char* cs_name, OutString result, int type)
{
	PJ_CONTEXT* C;
	PJ* P;

	C = proj_context_create();
	P = proj_create(C, cs_name);
	if (0 == P) return 0;

	result(proj_as_proj_string (C, P, (PJ_PROJ_STRING_TYPE)type, NULL));
	proj_destroy(P);
	proj_context_destroy(C);
	return 1;
}

PROJ_LIB_FUNCTIONS_API int __stdcall get_all_crs_names
(int include_mode, char* file_path)
{
	PJ_CONTEXT* C = proj_context_create();
	PROJ_CRS_LIST_PARAMETERS* parameters = proj_get_crs_list_parameters_create();
	PJ_TYPE type = (PJ_TYPE)include_mode; //(PJ_TYPE)include_mode;
	//PJ_TYPE types[1] = { (PJ_TYPE)include_mode };
	parameters->typesCount = 1;
	parameters->types = &type;
	int crs_counter = 0;
	PROJ_CRS_INFO** info_crs = proj_get_crs_info_list_from_database(C, NULL, parameters, &crs_counter);
	//crs_counter += 100;
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

	proj_context_destroy(C);
	proj_get_crs_list_parameters_destroy(parameters);
	proj_crs_info_list_destroy(info_crs);
	return 1;
}
PROJ_LIB_FUNCTIONS_API int __stdcall create_crs_by_wkt(char* wkt, OutString errors)
{
	PJ_CONTEXT* C = proj_context_create();
	PROJ_STRING_LIST out_warnings = nullptr;
	PROJ_STRING_LIST out_grammar_errors = nullptr;
	int code_out = 0;

	PJ* p = proj_create_from_wkt(C, wkt, NULL, &out_warnings, &out_grammar_errors);
	
	std::stringstream ss;
	if (p == NULL)
	{
		//ss << "warnings" << std::endl;

		for (auto iter = out_warnings; iter && *iter; ++iter)
			ss << (*iter);
		//ss << "wkt grammar errors" << std::endl;
		for (auto iter = out_grammar_errors; iter && *iter; ++iter)
			ss << (*iter);
		code_out = 0; //false
	}
	else 
	{
		ss << "no errors" << std::endl;
		code_out = 1; //true
	}
	errors(ss.str().c_str());
	ss.clear();
	std::cout << errors << std::endl;
	
	
	proj_context_destroy(C);
	proj_string_list_destroy(out_warnings);
	proj_string_list_destroy(out_grammar_errors);
	
	return code_out;
	
}