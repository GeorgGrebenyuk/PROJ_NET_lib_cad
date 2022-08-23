#include "pch.h"
#include "declare_functions.h"

#include <iostream>
#include <string>
#include <fstream>
#include <vector>
#include <sstream>


PROJ_LIB_FUNCTIONS_API int __stdcall crs2crs_tranform(char* source_cs_name, char* target_cs_name, char* file_path)
{
	std::cout << "start" << std::endl;

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
	PJ_CONTEXT* C;
	PJ* P;
	PJ_COORD a, b;

	C = proj_context_create();
	P = proj_create_crs_to_crs(C,
		source_cs_name,
		target_cs_name,
		NULL);

	if (0 == P) {
		fprintf(stderr, "Failed to create transformation object.\n");
	}
	//std::vector<const char*> points_end;
	std::ofstream out;
	out.open(file_path);
	for (auto source_point : points_row)
	{
		a = proj_coord(source_point[0], source_point[1], source_point[2], 0);
		b = proj_trans(P, PJ_FWD, a);
		std::stringstream ss;
		ss << b.enu.e << "," << b.enu.n << "," << b.enu.u;
		out << ss.str().c_str() << std::endl;
		//std::cout << "calced " << ss.str().c_str() << std::endl;
		//points_end.push_back(ss.str().c_str());
		ss.clear();
	}

	//for (auto target_point : points_end)
	//{
	//	std::cout << "calced " << target_point << std::endl;
	//	out << target_point << std::endl;
	//}
	out.close();

	proj_destroy(P);
	proj_context_destroy(C);
	return 1;
}