CREATE PROC [dbo].[BuscaValorEnBBDD]
(
@StrValorBusqueda nvarchar(100)
)
AS
BEGIN

CREATE TABLE #Resultado (NombreColumna nvarchar(370), ValorColumna nvarchar(3630))
SET NOCOUNT ON

DECLARE @NombreTabla nvarchar(256),
@NombreColumna nvarchar(128) = 'IPCODPACI',
@StrValorBusqueda2 nvarchar(110) = '1717'

SET  @NombreTabla = ''
SET @StrValorBusqueda2 = QUOTENAME('%' + @StrValorBusqueda + '%','''')

WHILE @NombreTabla IS NOT NULL
     BEGIN
     SET @NombreColumna = ''
     SET @NombreTabla =
     (SELECT MIN(QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME))
     FROM INFORMATION_SCHEMA.TABLES
     WHERE TABLE_TYPE = 'BASE TABLE'
     AND QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) > @NombreTabla
     AND OBJECTPROPERTY(
     OBJECT_ID(QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME)), 'IsMSShipped') = 0)

     WHILE (@NombreTabla IS NOT NULL) AND (@NombreColumna IS NOT NULL)
         BEGIN
         SET @NombreColumna =
         (SELECT MIN(QUOTENAME(COLUMN_NAME))
         FROM INFORMATION_SCHEMA.COLUMNS
         WHERE TABLE_SCHEMA = PARSENAME(@NombreTabla, 2)
         AND TABLE_NAME = PARSENAME(@NombreTabla, 1)
         AND DATA_TYPE IN ('char', 'varchar', 'nchar', 'nvarchar')
         AND QUOTENAME(COLUMN_NAME) > @NombreColumna)

         IF @NombreColumna IS NOT NULL
              BEGIN
              INSERT INTO #Resultado
              EXEC
              ('SELECT ''' + @NombreTabla + '.' + @NombreColumna + ''', LEFT(' + @NombreColumna + ', 3630)
              FROM ' + @NombreTabla + ' (NOLOCK) ' + ' WHERE ' + @NombreColumna + ' LIKE ' + @StrValorBusqueda2)
              END 
         END
     END
     SELECT NombreColumna, ValorColumna FROM #Resultado
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de búsqueda global en toda la base de datos: recorre dinámicamente todas las tablas y columnas de texto del sistema buscando un valor libre ingresado por el usuario (por ejemplo, una cédula, un código de paciente o cualquier texto). Devuelve el nombre de cada tabla y columna donde ese valor fue encontrado, junto con el contenido del campo. Se usa como herramienta de diagnóstico y localización de datos, útil para rastrear en qué partes del sistema aparece un paciente, un documento de identidad u otro dato específico cuando no se conoce la tabla exacta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'BuscaValorEnBBDD';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'BuscaValorEnBBDD';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Realiza una búsqueda exhaustiva de un valor de texto en todas las columnas de tipo carácter de todas las tablas de usuario de la base de datos, devolviendo las coincidencias encontradas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'BuscaValorEnBBDD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario ejecutor debe tener permisos de SELECT sobre todas las tablas base no de sistema; Debe existir tempdb disponible para crear la tabla temporal de resultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'BuscaValorEnBBDD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se inspeccionan tablas base (BASE TABLE) no marcadas como IsMSShipped; Solo se inspeccionan columnas de tipos carácter (char, varchar, nchar, nvarchar); Los valores devueltos se truncan a los primeros 3630 caracteres mediante LEFT; Las consultas dinámicas se ejecutan con NOLOCK (lecturas sucias permitidas); El recorrido de tablas y columnas es alfabético ascendente usando MIN > anterior; El valor de búsqueda se envuelve con comodines ''%...%'' y se entrecomilla con QUOTENAME para uso en SQL dinámico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'BuscaValorEnBBDD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #Resultado: Por cada columna de tipo char/varchar/nchar/nvarchar de cada tabla base de usuario, se inserta una fila por cada registro cuyo valor cumpla LIKE ''%@StrValorBusqueda%''; [RETURN_RESULT] #Resultado: Al finalizar el barrido se retorna el contenido completo de la tabla temporal con NombreColumna y ValorColumna (truncado a 3630 caracteres)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'BuscaValorEnBBDD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TABLE_TYPE = ''BASE TABLE'' AND OBJECTPROPERTY(...,''IsMSShipped'') = 0 → Se incluye la tabla en el barrido else Se omiten vistas y tablas de sistema/MS; si DATA_TYPE IN (''char'',''varchar'',''nchar'',''nvarchar'') → Se ejecuta búsqueda LIKE sobre la columna else Se omiten columnas numéricas, fechas, binarios u otros tipos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'BuscaValorEnBBDD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'INFORMATION_SCHEMA.TABLES; INFORMATION_SCHEMA.COLUMNS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'BuscaValorEnBBDD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'BuscaValorEnBBDD';
-- GO
