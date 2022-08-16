// test_cpp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "..\\proj_functions\\declare_functions.h"

int main()
{
    int source_cs = 100564;
    int target_cs = 4326;
   
    double x = 114761.9900359072;
    double y = 84173.3800804345;

    point p = crs2crs_tranform(source_cs, target_cs, x, y, 0);
    std::cout << "x= " << p.x << " y= " << p.y << " z= " << p.z;
    std::cout << "End\n";
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
