# PROJ_NET_lib_cad
.NET Wrapper for OSGeo GDAL PROJ library and original library for using as external library for CAD-software (NanoCAD and etc, later)
# What it is?
Small .NET class with Platform Invoke method to run process of reproject points data (from CPP library using GDAL PROJ methods).  There are also **proj.db** in Releasws with Russian coordinate systems (as part of "PROJ_lib_ver-\*\*\*_\*\*\*.zip")

Also there is a project for Autodesk dynamo - look [that ReadMe file](dyn_ReadMe.md);
## Structure of catalog
- ".NET access" -- contains .NET class (wrapper);
- "include" -- contains one header-file that declare external function of my lib;
- "src" -- Visual Studio's solution directory - there are three VS projects (main lib and debug versions: cpp console app and c# console app);
- "dyn_proj_library" code for Autodesk Dynamo Core package ```dyn_proj_library```
# How use?
## Firstly!
Install proj.db to tour OS: unpack last ```PROJ_lib_ver-***_***.zip``` from [releases](https://github.com/GeorgGrebenyuk/PROJ_NET_lib_cad/releases) to folder ```%LOCALAPPDATA%\proj```
## From C++
Most comfortable way - do not use my "lib" and use original GDAL PROJ with [**these official instructions**](https://proj.org/development/quickstart.html).
## From C#
1. Install proj.db to your system. Read [**these instructions**](https://proj.org/resource_files.html#where-are-proj-resource-files-looked-for).
2. Unzip my cpp proj's lib (watch "proj_functions_ver-\*\*\*.zip" in Releases)
3. Add class from **.NET access\LibraryImport.cs** to your application and set directory path to unzipped cpp library (watch "proj_functions_ver-\*\*\*.zip" in Releases) in DllImport argument. "point" structure is simply double-structure for simply process to return data from cpp lib.
4. Use it!

### Small video guide (Russian)
https://zen.yandex.ru/video/watch/628d3a06c0c8630b7c954c9a

## Articles about that (Russian only)
1. https://zen.yandex.ru/media/id/5d0dba97ecd5cf00afaf2938/627a2666e71a825dbd453337
2. https://zen.yandex.ru/media/id/5d0dba97ecd5cf00afaf2938/628c917fa70c7938bb24560f
