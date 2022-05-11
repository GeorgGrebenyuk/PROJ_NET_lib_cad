// debug_app.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "declare_functions.h"
int main()
{
    crs2crs_tranform("EPSG:32636", "EPSG:4326", 346744.4475, 6648123.4632, 0.0);

    std::cout << "End!\n";
    return 0;
}
