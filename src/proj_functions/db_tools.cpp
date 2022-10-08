#include "pch.h"
#include "declare_functions.h"
//#include "string"
#include <vector>
#include <iostream>
#include <fstream>
#include <sstream>

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
(char* file_path)
{
	PJ_CONTEXT* C = proj_context_create();
	/*
	Поскольку функция proj_get_crs_info_list_from_database НЕ ВОВЗРАЩАЕТ добавленные 
	Пользователем параметры (почему, НЕ ПОНИМАЮ), действуем через получение всех кодов СК
	и уже по ним получаем из БД информацию о проекции. Тратится некоторое время (до 5 секунд) для парсинга всей базы.
	Видится оптимальным ввод ограничений на категорию запроса - т.к. нам железно "хватит" только EPSG.
	
	*/
	PROJ_STRING_LIST auths_names = proj_get_authorities_from_database(C);
	std::vector<PJ_TYPE> types{ PJ_TYPE_PROJECTED_CRS, PJ_TYPE_GEODETIC_CRS };
	std::ofstream out;
	out.open(file_path);

	if (auths_names)
	{
		const char** auths_names_temp = const_cast<const char**>(auths_names);
		//Это как раз "спефификации" определений - типа EPSG и прочих
		while (*auths_names_temp)
		{
			//Тут мы получаем определения из projected_crs, geodetic_crs
			for (PJ_TYPE type : types)
			{
				PROJ_STRING_LIST info_codes = proj_get_codes_from_database(C, *auths_names_temp, type, 0);
				if (info_codes != NULL)
				{
					const char** pItem = const_cast<const char**>(info_codes);
					while (*pItem)
					{
						PJ* existed_proj = proj_create_from_database(C, *auths_names_temp, *pItem, PJ_CATEGORY_CRS, 0, NULL);
						if (existed_proj)
						{
							//std::cout << proj_get_name(existed_proj) << std::endl;
							if (out.is_open()) { out << proj_get_name(existed_proj) << std::endl; }
						}
						pItem++;
						proj_destroy(existed_proj);
					}
				}
				proj_string_list_destroy(info_codes);
			}
			auths_names_temp++;
		}
	}
	proj_string_list_destroy(auths_names);
	out.close();
	proj_context_destroy(C);
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