#include "pch.h"
#include "declare_functions.h"
#include "aux_functions.hpp"
#include <iostream>


std::vector<char*> aux_functions::get_authorities() 
{
    PJ_CONTEXT* C = proj_context_create();
    std::vector<char*> names;
    auto infoList = proj_get_authorities_from_database(C);

    const char** pItem = const_cast<const char**>(infoList);
    int count = 0;

    while (*pItem)
    {
        count++;
        pItem++;
    }

    for (int i = 0; i < count; i++)
    {
        //std::cout << infoList[i] << std::endl;
        names.push_back(infoList[i]);
    }
    proj_string_list_destroy(infoList);
    proj_context_destroy(C);
    return names;
}
