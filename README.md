# PROJ_NET_lib_cad
.NET Wrapper for OSGeo GDAL PROJ library and original library for using as external library for CAD-software (NanoCAD and etc, later)
# What it is?
Small .NET class in .NET Framework (4.5.2) dll with Platform Invoke methods to work with GDAL PROJ library.  There are also **proj.db** in Releasws with Russian coordinate systems (as part of "PROJ_lib_ver-\*\*\*_\*\*\*.zip")

Also there is a project for Autodesk dynamo - look [that ReadMe file](dyn_ReadMe.md);
## Update PROJ.db (auxiliary info)

Because of PROJ us sqlite-base library, there is way to multiplue-adding new CS to it via running local sql-file.
1. Open sqlite3.exe application (download that [here](https://www.sqlite.org/download.html) as "Precompiled Binaries for Windows" needing version)
2. Press ```open C:/Users/USER/AppData/Local/proj/proj.db``` where USER is your username
3. Press ```.read path_to_file.sql``` where ```path_to_file.sql``` is your file absolute path to sql file with new definitions.

Running sql-file: there is a some rows with single ```INSERT``` commands:
Below example for adding new CS by it's WKT2 code:
```
INSERT INTO "projected_crs" VALUES('EPSG',100500,'Russia-MGGT','',NULL,NULL,NULL,NULL,NULL,NULL,'BOUNDCRS[SOURCECRS[PROJCRS["Russia-MGGT",BASEGEOGCRS["Unknown datum based upon the BESSEL ellipsoid",DATUM["MGGT",ELLIPSOID["Bessel, 1841",6377397.155,299.15281535,LENGTHUNIT["metre",1,ID["EPSG",9001]]]],PRIMEM["Greenwich",0,ANGLEUNIT["degree",0.0174532925199433],ID["EPSG",8901]]],CONVERSION["Russia-MGGT",METHOD["Transverse Mercator",ID["EPSG",9807]],PARAMETER["Latitude of natural origin",55.66666666667,ANGLEUNIT["degree",0.0174532925199433],ID["EPSG",8801]],PARAMETER["Longitude of natural origin",37.5,ANGLEUNIT["degree",0.0174532925199433],ID["EPSG",8802]],PARAMETER["Scale factor at natural origin",1,SCALEUNIT["unity",1],ID["EPSG",8805]],PARAMETER["False easting",16.098,LENGTHUNIT["metre",1],ID["EPSG",8806]],PARAMETER["False northing",14.512,LENGTHUNIT["metre",1],ID["EPSG",8807]]],CS[Cartesian,2],AXIS["(E)",east,ORDER[1],LENGTHUNIT["metre",1,ID["EPSG",9001]]],AXIS["(N)",north,ORDER[2],LENGTHUNIT["metre",1,ID["EPSG",9001]]],USAGE[BBOX[54,34,58,41]]]],TARGETCRS[GEOGCRS["WGS 84",DATUM["World Geodetic System 1984",ELLIPSOID["WGS 84",6378137,298.257223563,LENGTHUNIT["metre",1]]],PRIMEM["Greenwich",0,ANGLEUNIT["degree",0.0174532925199433]],CS[ellipsoidal,2],AXIS["geodetic latitude(Lat)",north,ORDER[1],ANGLEUNIT["degree",0.0174532925199433]],AXIS["geodetic longitude(Lon)",east,ORDER[2],ANGLEUNIT["degree",0.0174532925199433]],ID["EPSG",4326]]],ABRIDGEDTRANSFORMATION["Moscow MGGT Datum",METHOD["Coordinate Frame rotation",ID["EPSG",9607]],PARAMETER["X-axis translation",316.151,ID["EPSG",8605]],PARAMETER["Y-axis translation",78.924,ID["EPSG",8606]],PARAMETER["Z-axis translation",589.65,ID["EPSG",8607]],PARAMETER["X-axis rotation",1.57273000,ID["EPSG",8608]],PARAMETER["Y-axis rotation",-2.69209000,ID["EPSG",8609]],PARAMETER["Z-axis rotation",-2.34693000,ID["EPSG",8610]],PARAMETER["Scale difference",1.0000084507,ID["EPSG",8611]]]]',0);
```
More info you can find at [official PROJ docs](https://proj.org/resource_files.html).

## Structure of catalog
- "src" -- Visual Studio's solution directory - there are three VS projects (main lib and debug versions: cpp console app and c# console app);
- "examples" -- source files with coordinates to reproject and other auxiliary test-data;
- "dyn_proj_library" code for Autodesk Dynamo Core package ```dyn_proj_library```
# How use?
## Firstly!
Install proj.db to tour OS: unpack last ```PROJ_lib_ver-***_***.zip``` from [releases](https://github.com/GeorgGrebenyuk/PROJ_NET_lib_cad/releases) to folder ```%LOCALAPPDATA%\proj```

**Last version is that file ([archive](https://github.com/GeorgGrebenyuk/PROJ_NET_lib_cad/releases/download/v1.1.0/PROJ_lib_ver-7.2.1_1.1.0.zip))**.
## From C++
Most comfortable way - do not use my "lib" and use original GDAL PROJ with [**these official instructions**](https://proj.org/development/quickstart.html).
## From C#
1. Upload stable version from Releases and connect to "proj_wrapper.dll";
2. Use it!

## About functions (from C#):
- crs2crs_tranform: transform point in one CS to another system;
- getting_proj_as_wkt: transform CS or it's part to WKT-representation as string-output data;
- getting_proj_as_proj: transform CS or it's part to PROJ-representation as string-output data;
- get_crs_names: getting ass string-names of CS in  database;
- create_crs_by_wkt: create new CS definition by input WKT-string code and return boolean value with status of adding (and also errors-list it exists);


### Small video guide (Russian)
https://zen.yandex.ru/video/watch/628d3a06c0c8630b7c954c9a

## Articles about that (Russian only)
1. https://zen.yandex.ru/media/id/5d0dba97ecd5cf00afaf2938/627a2666e71a825dbd453337
2. https://zen.yandex.ru/media/id/5d0dba97ecd5cf00afaf2938/628c917fa70c7938bb24560f
