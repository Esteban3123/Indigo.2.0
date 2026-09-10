CREATE PROCEDURE [dbo].[GetAllTableSizes]
AS
/*
    Obtains spaced used data for ALL user tables in the database
*/
DECLARE @TableName VARCHAR(100)    --For storing values in the cursor
 
--Cursor to get the name of all user tables from the sysobjects listing
DECLARE tableCursor CURSOR
FOR
select [name]
from dbo.sysobjects 
where  OBJECTPROPERTY(id, N'IsUserTable') = 1
FOR READ ONLY
 
--A procedure level temp table to store the results
CREATE TABLE #TempTable
(
    tableName varchar(100),
    numberofRows varchar(100),
    reservedSize varchar(50),
    dataSize varchar(50),
    indexSize varchar(50),
    unusedSize varchar(50)
)
 
--Open the cursor
OPEN tableCursor
 
--Get the first table name from the cursor
FETCH NEXT FROM tableCursor INTO @TableName
 
--Loop until the cursor was not able to fetch
WHILE (@@Fetch_Status >= 0)
BEGIN
    --Dump the results of the sp_spaceused query to the temp table
    INSERT  #TempTable
        EXEC sp_spaceused @TableName
 
    --Get the next table name
    FETCH NEXT FROM tableCursor INTO @TableName
END
 
--Get rid of the cursor
CLOSE tableCursor
DEALLOCATE tableCursor
 
--Select all records so we can use the reults
SELECT * 
FROM #TempTable
 
--Final cleanup!
DROP TABLE #TempTable
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de administración técnica que recorre todas las tablas del usuario en la base de datos y devuelve el uso de espacio en disco de cada una: número de filas, tamaño reservado, tamaño de datos, tamaño de índices y espacio sin utilizar. No toca entidades de negocio del sistema Indigo Vie Cloud directamente; su propósito es de monitoreo y diagnóstico de capacidad de la base de datos. Útil para el equipo de infraestructura o DBA cuando necesitan identificar las tablas más grandes o controlar el crecimiento del almacenamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'GetAllTableSizes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'GetAllTableSizes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el espacio utilizado (filas, reservado, datos, índices y no usado) de todas las tablas de usuario de la base de datos para monitoreo y diagnóstico de capacidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAllTableSizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ejecutor debe tener permisos para leer dbo.sysobjects y ejecutar sp_spaceused sobre las tablas de usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAllTableSizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa objetos con OBJECTPROPERTY IsUserTable = 1, excluyendo tablas de sistema; La tabla temporal se crea y se elimina dentro del mismo alcance de ejecución; El cursor se cierra y libera tras recorrer todas las tablas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAllTableSizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #TempTable: Por cada tabla de usuario obtenida del cursor, se inserta en la tabla temporal el resultado de EXEC sp_spaceused @TableName; [RETURN_RESULT] #TempTable: Al finalizar el recorrido, se devuelve un result set con todas las filas acumuladas en la tabla temporal (SELECT * FROM #TempTable)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAllTableSizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_spaceused', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAllTableSizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.sysobjects', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAllTableSizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAllTableSizes';
-- GO
