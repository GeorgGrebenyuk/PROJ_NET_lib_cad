#pragma once
#include <vector>
class aux_functions
{
public:
	static std::vector<char*>get_authorities();
	static std::vector<char*> get_all_crs_names_sqlite(const char* db_path);
};

