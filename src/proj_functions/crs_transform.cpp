#include "pch.h"
#include "declare_functions.h"
#include <iostream>

PROJ_LIB_FUNCTIONS_API point __stdcall crs2crs_tranform(char* source_cs_name, char* target_cs_name, point source_point)
{
	//std::cout << "source_cs " << source_cs_name << std::endl;
	//std::cout << "target_cs " << target_cs_name << std::endl;
	PJ_CONTEXT* C;
	PJ* P;
	//PJ* norm;
	PJ_COORD a, b;

	C = proj_context_create();
	P = proj_create_crs_to_crs(C,
		source_cs_name,
		target_cs_name,
		NULL);

	if (0 == P) {
		fprintf(stderr, "Failed to create transformation object.\n");
		//return 1;
	}
	a = proj_coord(source_point.x, source_point.y, source_point.z, 0);
	b = proj_trans(P, PJ_FWD, a);

	point p_export(b.enu.e, b.enu.n, b.enu.u);
	proj_destroy(P);
	proj_context_destroy(C);
	return p_export;
}