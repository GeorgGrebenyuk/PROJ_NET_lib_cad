--Удаление всех ранних добавленных вещей
delete from "conversion_table" where code = 200501;
delete from "projected_crs" where code = 100501;
--delete from "geodetic_crs" where code = 21000;
--изменение системной таблицы для геод. преобразований

--для формулировки перехода из СК-42 в WGS-84
--update helmert_transformation_table set deprecated = 1 where target_crs_code = 4326 and source_crs_code = 4284 and code != 1267;
--перевод датума для СК-95 в число для СК-42
update helmert_transformation_table set source_crs_code = 4284 where target_crs_code = 4326 and (code = 1281 or code = 5043);
--update helmert_transformation_table set deprecated = 1 where target_crs_code = 4326 and source_crs_code = 4200 and code != 5043;

--добавление системы координат
--датум для СК-95
--формулировка системы координат как проекция Поперечная Меркатора
INSERT INTO "conversion_table" (
	auth_name, code, name, description, 
	method_auth_name, method_code, 
	param1_auth_name, param1_code, param1_value, param1_uom_auth_name, param1_uom_code, 
	param2_auth_name, param2_code, param2_value, param2_uom_auth_name, param2_uom_code, 
	param3_auth_name, param3_code, param3_value, param3_uom_auth_name, param3_uom_code, 
	param4_auth_name, param4_code, param4_value, param4_uom_auth_name, param4_uom_code, 
	param5_auth_name, param5_code, param5_value, param5_uom_auth_name, param5_uom_code, 
	param6_auth_name, param6_code, param6_value, param6_uom_auth_name, param6_uom_code, 
	param7_auth_name, param7_code, param7_value, param7_uom_auth_name, param7_uom_code, 
	deprecated) VALUES(
	'EPSG', 200501, 'RussiaMSK-1964', 'CS Definition for Russia_MSK-1964', 
	'EPSG', 9807, -- Transverse Mercator
	'EPSG', 8801, 0.0, 'EPSG', 9102, -- Latitude of natural origin
	'EPSG', 8802, 30, 'EPSG', 9102, -- Longitude of natural origin
	'EPSG', 8805, 1.0, 'EPSG', 9201, -- Scale factor at natural origin
	'EPSG', 8806, 95942.17, 'EPSG', 9001, -- False easting
	'EPSG', 8807, -6552810.0, 'EPSG', 9001, -- False northing,
	NULL, NULL, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL,
	0);	
INSERT INTO "projected_crs" (
	auth_name,
	code, 
	name, 
	description, 
	coordinate_system_auth_name, 
	coordinate_system_code, 
	geodetic_crs_auth_name, 
	geodetic_crs_code, 
	conversion_auth_name, 
	conversion_code, 
	text_definition,
	deprecated) VALUES(
	'EPSG',
	100501, 
	'Russia_MSK-1964', 
	'Городская СК для Санкт-Петербурга',
	'EPSG',
	4530, -- const for our systems
	'EPSG',
	4284, --universal datum for 
	'EPSG', 
	200501,
	NULL,
	0);