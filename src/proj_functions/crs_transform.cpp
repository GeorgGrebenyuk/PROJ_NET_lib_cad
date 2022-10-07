#include "pch.h"
#include "declare_functions.h"

#include <iostream>
#include <string>
#include <fstream>
#include <vector>
#include <sstream>
//Для точности
#include <iomanip>
//#include <limits>
//#include <numbers>
//#include <iostream>

PROJ_LIB_FUNCTIONS_API int __stdcall crs2crs_tranform(const char* source_cs_name, const char* target_cs_name, const char* file_path)
{
	std::cout << "Start convert from " << source_cs_name << " to " << target_cs_name << std::endl;

	std::vector<std::vector<double>> points_row;
	std::string line;
	std::ifstream points_file(file_path);
	
	while (getline(points_file, line))
	{
		std::vector<double> elems;
		std::stringstream ss;
		char delim = ',';
		ss.str(line);
		std::string item;
		while (std::getline(ss, item, delim)) 
		{
			//std::cout << item << std::endl;
			elems.push_back(atof(item.c_str()));
		}
		points_row.push_back(elems);
		//std::cout << "row" << elems[0] << " " << elems[1] << " " << elems[2] << std::endl;
	}
	std::cout <<"end  read" << std::endl;
	PJ_CONTEXT* C;
	
	PJ_COORD a, b;

	C = proj_context_create();
	PJ* P_source = proj_create(C, source_cs_name);
	std::cout << "P_source = " << P_source << std::endl;
	PJ* P_target = proj_create(C, target_cs_name);
	std::cout << "P_target = " << P_target << std::endl;

	//const char* const options[] = { "ACCURACY= 1.0", nullptr };
	//PJ_AREA* area = proj_area_create();
	
	PJ* P_convert = proj_create_crs_to_crs_from_pj(C, P_source, P_target, NULL, NULL);
	
	//PJ* P_convert = proj_create_crs_to_crs(C, source_cs_name, target_cs_name, NULL);
	if (0 == P_convert) {
		fprintf(stderr, "Failed to create transformation object.\n");
	}
	//std::vector<const char*> points_end;
	std::ofstream out;
	out.open(file_path);
	//std::cout << "start recalc" << std::endl;


	for (auto source_point : points_row)
	{
		a = proj_coord(source_point[0], source_point[1], source_point[2], 0); //инвертированный x,y
		//std::cout << "coord row (geodetic xy)= " << source_point[1] << " " << source_point[0] << " " << std::endl;
		b = proj_trans(P_convert, PJ_FWD, a);

		
		std::stringstream ss;
		ss << std::fixed << std::setprecision(14) << b.xyz.x << "," << b.xyz.y << "," << b.xyz.z;
		out << ss.str().c_str() << std::endl;
		std::cout << "coord = " << ss.str().c_str() << std::endl;
		//points_end.push_back(ss.str().c_str());
		ss.clear();
	}

	//for (auto target_point : points_end)
	//{
	//	std::cout << "calced " << target_point << std::endl;
	//	out << target_point << std::endl;
	//}
	out.close();

	proj_destroy(P_convert);
	proj_destroy(P_source);
	proj_destroy(P_target);
	proj_context_destroy(C);
	return 1;
}
PROJ_LIB_FUNCTIONS_API int __stdcall test_transform()
{
	PJ_CONTEXT* C = proj_context_create();
	PJ* P_source = proj_create(C, "WGS 84 / UTM zone 36N");
	PJ* P_target = proj_create(C, "EPSG:4326");
	const char* const options[] = { "ACCURACY=8", nullptr };
	PJ* P_convert = proj_create_crs_to_crs_from_pj(C, P_source, P_target, NULL, options);
	if (0 == P_convert) {
		fprintf(stderr, "Failed to create transformation object.\n");
	}

	PJ_COORD a, b;
	//353803,6638030,0.0
	a = proj_coord(353803, 6638030, 0.0, 0.0);
	std::cout << "source " << a.enu.e << " " << a.enu.n << std::endl;
	b = proj_trans(P_convert, PJ_FWD, a);
	std::cout << "recalced " << b.enu.e << " " << b.enu.n << std::endl;

	proj_destroy(P_convert);
	proj_destroy(P_source);
	proj_destroy(P_target);
	proj_context_destroy(C);
	return 1;
}