-- Get a list of tables and their sizes on disk 
CREATE PROCEDURE [dbo].[sp_Table_Sizes] 
AS 
BEGIN 
    -- SET NOCOUNT ON added to prevent extra result sets from 
    -- interfering with SELECT statements. 
    SET NOCOUNT ON; 
DECLARE @table_name VARCHAR(500) 
DECLARE @schema_name VARCHAR(500) 
DECLARE @tab1 TABLE( 
     tablename VARCHAR (500) collate database_default 
     ,schemaname VARCHAR(500) collate database_default 
) 

CREATE TABLE #temp_Table ( 
     tablename sysname 
     ,row_count INT 
     ,reserved VARCHAR(50) collate database_default 
     ,data VARCHAR(50) collate database_default 
     ,index_size VARCHAR(50) collate database_default 
     ,unused VARCHAR(50) collate database_default 
) 

INSERT INTO @tab1 
SELECT Table_Name, Table_Schema 
FROM information_schema.tables 
WHERE TABLE_TYPE = 'BASE TABLE' 

DECLARE c1 CURSOR FOR 
SELECT Table_Schema + '.' + Table_Name 
FROM information_schema.tables t1 
WHERE TABLE_TYPE = 'BASE TABLE' 

OPEN c1 
FETCH NEXT FROM c1 INTO @table_name 
WHILE @@FETCH_STATUS = 0 
BEGIN 
     SET @table_name = REPLACE(@table_name, '[',''); 
     SET @table_name = REPLACE(@table_name, ']',''); 

     -- make sure the object exists before calling sp_spacedused 
     IF EXISTS(SELECT id FROM sysobjects WHERE id = OBJECT_ID(@table_name)) 
     BEGIN 
       INSERT INTO #temp_Table EXEC sp_spaceused @table_name, false; 
     END 

     FETCH NEXT FROM c1 INTO @table_name 
END 
CLOSE c1 
DEALLOCATE c1 

SELECT t1.* 
     ,t2.schemaname 
FROM #temp_Table t1 
INNER JOIN @tab1 t2 ON (t1.tablename = t2.tablename) 
ORDER BY schemaname,t1.tablename; 

DROP TABLE #temp_Table 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de administración técnica de base de datos que lista todas las tablas del sistema junto con su tamaño en disco. Para cada tabla obtiene la cantidad de filas, espacio reservado, espacio de datos, tamaño de índices y espacio no utilizado, usando el procedimiento del sistema sp_spaceused. No toca entidades de negocio directamente; su uso es operativo y de monitoreo, permitiendo identificar qué tablas consumen más almacenamiento en la base de datos del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_Table_Sizes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_Table_Sizes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista todas las tablas base de la base de datos con su tamaño en disco (filas, espacio reservado, datos, índices y no usado) para monitoreo de almacenamiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_Table_Sizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Permisos para leer information_schema.tables y sysobjects; Permisos para ejecutar sp_spaceused sobre cada tabla', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_Table_Sizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa objetos con TABLE_TYPE = ''BASE TABLE'' (excluye vistas); Limpia corchetes ''['' y '']'' del nombre antes de validar el objeto; Solo invoca sp_spaceused si el objeto existe, evitando errores en tiempo de ejecución; El resultado final está siempre ordenado por esquema y nombre de tabla', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_Table_Sizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #temp_Table: Para cada tabla BASE TABLE existente en sysobjects, se inserta el resultado de EXEC sp_spaceused con detalle de filas y espacio.; [RETURN_RESULT] RESULTSET: Retorna el contenido de #temp_Table unido con el esquema, ordenado por schemaname y tablename.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_Table_Sizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS(SELECT id FROM sysobjects WHERE id = OBJECT_ID(@table_name)) → Ejecuta sp_spaceused e inserta el resultado en la tabla temporal else Omite la tabla y continúa con la siguiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_Table_Sizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_spaceused', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_Table_Sizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'information_schema.tables; sys.sysobjects', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_Table_Sizes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_Table_Sizes';
-- GO
