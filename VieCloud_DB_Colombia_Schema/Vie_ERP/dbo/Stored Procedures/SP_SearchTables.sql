CREATE PROCEDURE [dbo].[SP_SearchTables] @Tablenames      VARCHAR(500), 
                                        @SearchStr       NVARCHAR(60), 
                                        @GenerateSQLOnly BIT          = 0
AS

/*
	Parameters and usage

	@Tablenames		-- Provide a single table name or multiple table name with comma seperated. 
						If left blank , it will check for all the tables in the database
	@SearchStr		-- Provide the search string. Use the '%' to coin the search. 
						EX : X%--- will give data staring with X
							 %X--- will give data ending with X
							 %X%--- will give data containig  X
	@GenerateSQLOnly -- Provide 1 if you only want to generate the SQL statements without seraching the database. 
						By default it is 0 and it will search.

	Samples :

	1. To search data in a table

		EXEC SP_SearchTables @Tablenames = 'T1'
						 ,@SearchStr  = '%TEST%'

		The above sample searches in table T1 with string containing TEST.

	2. To search in a multiple table

		EXEC SP_SearchTables @Tablenames = 'T2'
						 ,@SearchStr  = '%TEST%'

		The above sample searches in tables T1 & T2 with string containing TEST.
	
	3. To search in a all table

		EXEC SP_SearchTables @Tablenames = '%'
						 ,@SearchStr  = '%TEST%'

		The above sample searches in all table with string containing TEST.

	4. Generate the SQL for the Select statements

		EXEC SP_SearchTables @Tablenames		= 'T1'
						 ,@SearchStr		= '%TEST%'
						 ,@GenerateSQLOnly	= 1

*/

     SET NOCOUNT ON;
     DECLARE @MatchFound BIT;
     SELECT @MatchFound = 0;
     DECLARE @CheckTableNames TABLE(Tablename SYSNAME);
     DECLARE @SQLTbl TABLE
     (Tablename    SYSNAME, 
      WHEREClause  VARCHAR(MAX), 
      SQLStatement VARCHAR(MAX), 
      Execstatus   BIT
     );
     DECLARE @SQL VARCHAR(MAX);
     DECLARE @tmpTblname SYSNAME;
     DECLARE @ErrMsg VARCHAR(100);
     IF LTRIM(RTRIM(@Tablenames)) IN('', '%')
         BEGIN
             INSERT INTO @CheckTableNames
                    SELECT Name
                    FROM sys.tables;
     END;
         ELSE
         BEGIN
             SELECT @SQL = 'SELECT ''' + REPLACE(@Tablenames, ',', ''' UNION SELECT ''') + '''';
             INSERT INTO @CheckTableNames
             EXEC (@SQL);
     END;
     IF NOT EXISTS
     (
         SELECT 1
         FROM @CheckTableNames
     )
         BEGIN
             SELECT @ErrMsg = 'No tables are found in this database ' + DB_NAME() + ' for the specified filter';
             PRINT @ErrMsg;
             RETURN;
     END;
     INSERT INTO @SQLTbl
     (Tablename, 
      WHEREClause
     )
            SELECT QUOTENAME(SCh.name) + '.' + QUOTENAME(ST.NAME), 
            (
                SELECT '[' + SC.Name + ']' + ' LIKE ''' + @SearchStr + ''' OR ' + CHAR(10)
                FROM SYS.columns SC
                     JOIN SYS.types STy ON STy.system_type_id = SC.system_type_id
                                           AND STy.user_type_id = SC.user_type_id
                WHERE STY.name IN('varchar', 'char', 'nvarchar', 'nchar', 'text')
                     AND SC.object_id = ST.object_id
                ORDER BY SC.name FOR XML PATH('')
            )
            FROM SYS.tables ST
                 JOIN @CheckTableNames chktbls ON chktbls.Tablename COLLATE Modern_Spanish_CI_AS = ST.name COLLATE Modern_Spanish_CI_AS
                 JOIN SYS.schemas SCh ON ST.schema_id = SCh.schema_id
            WHERE ST.name <> 'SearchTMP'
            GROUP BY ST.object_id, 
                     QUOTENAME(SCh.name) + '.' + QUOTENAME(ST.NAME);
     UPDATE @SQLTbl
       SET 
           SQLStatement = 'SELECT * INTO SearchTMP FROM ' + Tablename + ' WHERE ' + SUBSTRING(WHEREClause, 1, LEN(WHEREClause) - 5);
     DELETE FROM @SQLTbl
     WHERE WHEREClause IS NULL;
     WHILE EXISTS
     (
         SELECT 1
         FROM @SQLTbl
         WHERE ISNULL(Execstatus, 0) = 0
     )
         BEGIN
             SELECT TOP 1 @tmpTblname = Tablename, 
                          @SQL = SQLStatement
             FROM @SQLTbl
             WHERE ISNULL(Execstatus, 0) = 0;
             IF @GenerateSQLOnly = 0
                 BEGIN
                     IF OBJECT_ID('SearchTMP', 'U') IS NOT NULL
                         DROP TABLE SearchTMP;
                     EXEC (@SQL);
                     IF EXISTS
                     (
                         SELECT 1
                         FROM SearchTMP
                     )
                         BEGIN
                             SELECT Tablename = @tmpTblname, 
                                    *
                             FROM SearchTMP;
                             SELECT @MatchFound = 1;
                     END;
             END;
                 ELSE
                 BEGIN
                     PRINT REPLICATE('-', 100);
                     PRINT @tmpTblname;
                     PRINT REPLICATE('-', 100);
                     PRINT replace(@SQL, 'INTO SearchTMP', '');
             END;
             UPDATE @SQLTbl
               SET 
                   Execstatus = 1
             WHERE Tablename = @tmpTblname;
         END;
     IF @MatchFound = 0
         BEGIN
             SELECT @ErrMsg = 'No Matches are found in this database ' + DB_NAME() + ' for the specified filter';
             PRINT @ErrMsg;
             RETURN;
     END;
     SET NOCOUNT OFF;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento utilitario de búsqueda de texto libre que recorre una o varias tablas de la base de datos actual, construyendo dinámicamente sentencias SELECT sobre todas las columnas de tipo carácter (`varchar`, `nvarchar`, `char`, `nchar`, `text`) y filtrando por un patrón LIKE. Permite operar sobre tablas específicas (lista separada por comas) o sobre la totalidad de la base de datos. Cuando `@GenerateSQLOnly = 1`, solo imprime el SQL generado sin ejecutarlo; en caso contrario devuelve los registros coincidentes usando una tabla temporal auxiliar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SearchTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SearchTables';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Utilidad administrativa que busca un patrón de texto en columnas de tipo carácter de una o varias tablas de la base de datos, devolviendo coincidencias o generando los SELECT equivalentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SearchTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere permiso para leer catálogos del sistema (sys.tables, sys.columns, sys.types, sys.schemas); Si se ejecuta la búsqueda (no solo generación de SQL), el usuario debe poder crear/eliminar la tabla SearchTMP en el esquema actual; El patrón de búsqueda debe expresarse con comodines de LIKE (% / _) según se requiera', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SearchTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se buscan coincidencias en columnas de tipo varchar, char, nvarchar, nchar o text; La tabla auxiliar SearchTMP nunca se incluye como objetivo de búsqueda; SearchTMP se recrea (DROP+SELECT INTO) en cada iteración para evitar residuos de búsquedas previas; Cada tabla candidata se procesa una sola vez (Execstatus se marca en 1 tras procesarla); Las tablas sin columnas de texto se descartan antes de iterar (DELETE WHERE WHEREClause IS NULL); El procedimiento nunca modifica datos de las tablas de negocio; solo lee y escribe en la tabla temporal SearchTMP; La comparación de nombres de tabla se hace con collation Modern_Spanish_CI_AS para evitar conflictos de intercalación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SearchTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] SearchTMP: Antes de cada ejecución dinámica, si OBJECT_ID(''SearchTMP'',''U'') IS NOT NULL se hace DROP TABLE SearchTMP; [INSERT] SearchTMP: Por cada tabla candidata se ejecuta ''SELECT * INTO SearchTMP FROM <tabla> WHERE <col> LIKE @SearchStr OR ...'' creando y poblando la tabla temporal con los registros coincidentes; [RETURN_RESULT] (resultset): Si SearchTMP tiene filas tras la inserción, se devuelve un result set con la columna Tablename y todas las columnas de la tabla buscada, y se marca @MatchFound=1; [RETURN_RESULT] (mensajes PRINT): Si @GenerateSQLOnly=1, en lugar de ejecutar, se imprime el nombre de la tabla y el SELECT equivalente (sin ''INTO SearchTMP''); [RETURN_RESULT] (mensajes PRINT): Si @CheckTableNames queda vacío tras el filtro, se imprime ''No tables are found in this database <db> for the specified filter'' y se hace RETURN; [RETURN_RESULT] (mensajes PRINT): Si tras procesar todas las tablas @MatchFound sigue en 0, se imprime ''No Matches are found in this database <db> for the specified filter'' y se hace RETURN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SearchTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LTRIM(RTRIM(@Tablenames)) IN ('''',''%'') → Se cargan TODAS las tablas de sys.tables en la lista de candidatas else Se interpreta @Tablenames como lista separada por comas y se construye dinámicamente un SELECT...UNION para poblar las candidatas; si @GenerateSQLOnly = 0 → Se ejecuta el SELECT INTO SearchTMP y, si hay filas, se devuelven al cliente else Solo se imprime el SQL equivalente sin ejecutar la búsqueda; si ST.name <> ''SearchTMP'' → Se excluye la tabla auxiliar SearchTMP del conjunto de tablas a inspeccionar; si STy.name IN (''varchar'',''char'',''nvarchar'',''nchar'',''text'') → Solo se generan predicados LIKE para columnas de tipos de cadena; las tablas sin columnas de texto quedan con WHEREClause NULL y se eliminan', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SearchTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'sys.tables; sys.columns; sys.types; sys.schemas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SearchTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SearchTables';
-- GO
