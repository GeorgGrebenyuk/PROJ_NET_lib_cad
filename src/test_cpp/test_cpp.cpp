// test_cpp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "..\\proj_functions\\declare_functions.h"

int main()
{
    //const char* source_cs = "Ukraine-SK63-X_Zona1";
    //OutString res{};
    //int info_wkt = get_proj_as_proj((char* )source_cs, res, 0);
    //std::cout << "code = " << info_wkt << " data = " << info_wkt;
   
    //double x = 114761.9900359072;
    //double y = 84173.3800804345;

    //point p = crs2crs_tranform(source_cs, target_cs, x, y, 0);
    //std::cout << "x= " << p.x << " y= " << p.y << " z= " << p.z;
    //std::cout << "End\n";
    //const char* source = "Russia-MSK1964";
    //const char* target = "WGS 84"; //EPSG:4326
    //const char* f_path = "C:\\Users\\Georg\\Documents\\GitHub\\PROJ_NET_lib_cad\\examples\\points_4326-nerovnosti_945dc078-03c5-4990-be2f-b27ce73b1f36.txt";
    //int wait_1 = crs2crs_tranform(source, target, f_path);

    int wait_test = test_transform();
}

