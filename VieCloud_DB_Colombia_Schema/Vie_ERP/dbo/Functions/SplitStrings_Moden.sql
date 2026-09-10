CREATE FUNCTION [dbo].[SplitStrings_Moden]
(
   @List NVARCHAR(MAX),
   @Delimiter NVARCHAR(255)
)
RETURNS TABLE
WITH SCHEMABINDING AS
RETURN
  WITH E1(N)        AS ( SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1 
                         UNION ALL SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1 
                         UNION ALL SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1),
       E2(N)        AS (SELECT 1 FROM E1 a, E1 b),
       E4(N)        AS (SELECT 1 FROM E2 a, E2 b),
       E42(N)       AS (SELECT 1 FROM E4 a, E2 b),
       cteTally(N)  AS (SELECT 0 UNION ALL SELECT TOP (DATALENGTH(ISNULL(@List,1))) 
                         ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) FROM E42),
       cteStart(N1) AS (SELECT t.N+1 FROM cteTally t
                         WHERE (SUBSTRING(@List,t.N,1) = @Delimiter OR t.N = 0))
  SELECT Item = SUBSTRING(@List, s.N1, ISNULL(NULLIF(CHARINDEX(@Delimiter,@List,s.N1),0)-s.N1,8000))
    FROM cteStart s;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de utilidad que divide una cadena de texto en múltiples filas usando un delimitador especificado. Recibe una lista de valores separados (por ejemplo, códigos de diagnóstico, servicios o identificadores separados por coma, punto y coma u otro carácter) y devuelve cada elemento como una fila individual en una tabla. Es una función auxiliar de alto rendimiento usada internamente por otros procesos del sistema para descomponer listas de valores y procesarlos de forma tabular, sin tocar directamente una entidad de negocio específica pero siendo clave para filtros y búsquedas que reciben múltiples parámetros como listas de cédulas, códigos de servicio, contratos o diagnósticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'SplitStrings_Moden';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'SplitStrings_Moden';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Función de tabla utilitaria que divide una cadena delimitada en filas, devolviendo cada fragmento como un ítem independiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitStrings_Moden';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El delimitador debe ser de un solo carácter para que el cálculo de posiciones con SUBSTRING/CHARINDEX funcione correctamente.; La cadena de entrada no debe exceder los límites de NVARCHAR(MAX) procesables por la tabla de tally generada (~10000 posiciones).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitStrings_Moden';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Si la entrada es NULL, se trata como longitud mínima vía ISNULL(@List,1) evitando fallo en DATALENGTH.; Cada posición de inicio se calcula a partir de donde aparece el delimitador o desde el inicio de la cadena (N=0).; El tamaño máximo de cada ítem extraído está acotado a 8000 caracteres cuando no se encuentra el siguiente delimitador.; La función es SCHEMABINDING, por lo que no depende de objetos externos modificables.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitStrings_Moden';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve un conjunto de filas con la columna Item, una por cada segmento de la cadena separado por el delimitador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitStrings_Moden';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitStrings_Moden';
GO
