#include "pch.h"
#include "declare_functions.h"
#include <iostream>
//#include <string.h>
#include <string>
int32_t init_app()
{
	return 1;
}
point crs2crs_tranform(int source_cs_code, int target_cs_code,
	double source_x, double source_y, double source_z)
{
	std::cout << "source_cs" << source_cs_code << std::endl;
	std::cout << "target_cs" << target_cs_code << std::endl;
	PJ_CONTEXT* C;
	PJ* P;
	PJ* norm;
	PJ_COORD a, b;

	std::string epsg = "EPSG:";
	std::string source_cs = "EPSG:" + std::to_string(source_cs_code);
	std::string target_cs = "EPSG:" + std::to_string(target_cs_code);
	//const char *source_cs = epsg.c_str() + std::to_string(source_cs_code);
	//const char* target_cs = "EPSG:" + target_cs_code;

	std::cout << "source_cs" << source_cs << std::endl;
	std::cout << "target_cs" << target_cs << std::endl;

	C = proj_context_create();
	P = proj_create_crs_to_crs(C,
		source_cs.c_str(),
		target_cs.c_str(), /* or EPSG:32632 */
		NULL);

	if (0 == P) {
		fprintf(stderr, "Failed to create transformation object.\n");
		//return 1;
	}
	a = proj_coord(source_x, source_y, source_z, 0);
	b = proj_trans(P, PJ_FWD, a);
	//printf("easting: %.7f, northing: %.7f\n", b.enu.e, b.enu.n);

	point p_export(b.enu.e, b.enu.n, b.enu.u);
	return p_export;
}