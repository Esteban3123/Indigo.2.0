
CREATE view [Report].[ViewConsultaTamano] as
SELECT CAST(DB_NAME() AS VARCHAR(16)) AS ID_COMPANY, 
       YEAR(GETDATE()) AS 'AÑO',
       CONCAT(FORMAT(MONTH(GETDATE()), '00') ,' - ', 
	   CASE MONTH(GETDATE()) 
	    WHEN 1 THEN 'ENERO'
   	    WHEN 2 THEN 'FEBRERO'
	    WHEN 3 THEN 'MARZO'
	    WHEN 4 THEN 'ABRIL'
	    WHEN 5 THEN 'MAYO'
	    WHEN 6 THEN 'JUNIO'
	    WHEN 7 THEN 'JULIO'
	    WHEN 8 THEN 'AGOSTO'
	    WHEN 9 THEN 'SEPTIEMBRE'
	    WHEN 10 THEN 'OCTUBRE'
	    WHEN 11 THEN 'NOVIEMBRE'
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE',
	  --OBJECT_NAME(t.object_id) AS ObjectName,
       SUM(u.total_pages) * 8 AS Total_Reserved_kb,
       (SUM(u.total_pages) * 8) * 0.000001 AS Total_Reserved_GB,
       SUM(u.used_pages) * 8 AS Used_Space_kb,
       (SUM(u.used_pages) * 8) * 0.000001  AS Used_Space_GB,
       u.type_desc AS TypeDesc,
       MAX(p.rows) AS RowsCount
FROM 
 sys.allocation_units AS u
 JOIN sys.partitions AS p ON u.container_id = p.hobt_id
 JOIN sys.tables AS t ON p.object_id = t.object_id
GROUP BY u.type_desc--, OBJECT_NAME(t.object_id)
HAVING SUM(u.used_pages) * 8 <> 0
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida métricas de tamaño y uso de almacenamiento de la base de datos actual en el momento de la consulta. Agrupa el espacio reservado y utilizado (en KB y GB) por tipo de unidad de asignación, junto con el conteo máximo de filas, enriqueciendo el resultado con el nombre de la compañía (base de datos), el año y el mes en curso. Está orientada a monitoreo o reportes periódicos de capacidad de almacenamiento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewConsultaTamano';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewConsultaTamano';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el tamaño total reservado y usado (en KB y GB) y la cantidad de filas de las tablas de la base de datos actual, agrupado por tipo de unidad de asignación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewConsultaTamano';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se ejecuta en el contexto de una base de datos accesible vía DB_NAME() y con permisos de lectura sobre las vistas de catálogo sys.allocation_units, sys.partitions y sys.tables.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewConsultaTamano';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen filas cuyo espacio usado total sea distinto de cero (HAVING SUM(u.used_pages) * 8 <> 0).; La conversión a KB se realiza multiplicando páginas por 8; la conversión a GB multiplica los KB por 0.000001.; ID_COMPANY se trunca a VARCHAR(16) a partir de DB_NAME().; Se agrupa exclusivamente por u.type_desc (la línea de OBJECT_NAME está comentada).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewConsultaTamano';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve métricas de espacio (Total_Reserved_kb/GB, Used_Space_kb/GB) y filas máximas (RowsCount) por type_desc, junto con identificación de la base (DB_NAME), año y mes con nombre en español del momento de consulta.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewConsultaTamano';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MONTH(GETDATE()) entre 1 y 12 → Traduce el número de mes a su nombre en español (ENERO..DICIEMBRE) concatenado con el número formateado a 2 dígitos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewConsultaTamano';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'sys.allocation_units; sys.partitions; sys.tables', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewConsultaTamano';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewConsultaTamano';
GO
