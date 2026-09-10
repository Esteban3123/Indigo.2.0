create
 
FUNCTION [dbo].[RemoverTildes] (@Cadena VARCHAR (100))
RETURNS VARCHAR (100)
AS
 
BEGIN
 
 
--Reemplazamos las vocales acentuadas
    
RETURN
 
REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@Cadena,'á','A'),'é','E'),'í','I'),'ó','O'),'ú','U')
 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función utilitaria que recibe un texto y devuelve el mismo texto sin tildes, reemplazando las vocales acentuadas minúsculas (á, é, í, ó, ú) por sus equivalentes en mayúscula sin tilde (A, E, I, O, U). Se usa para normalizar y estandarizar cadenas de texto antes de comparaciones, búsquedas o integraciones donde las tildes pueden causar inconsistencias, como nombres de pacientes, diagnósticos o descripciones de servicios. Es una función de apoyo transversal que no toca una entidad de negocio específica, sino que es invocada por otros objetos del sistema para limpiar texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'RemoverTildes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'RemoverTildes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Normaliza una cadena reemplazando vocales minúsculas acentuadas por sus equivalentes mayúsculas sin tilde.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RemoverTildes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se transforman vocales minúsculas acentuadas (á,é,í,ó,ú); el resto de caracteres se conserva.; La longitud máxima resultante está limitada a 100 caracteres por el tipo de retorno.; No se manejan vocales mayúsculas acentuadas (Á,É,Í,Ó,Ú) ni la diéresis (ü) ni la ñ.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RemoverTildes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve la cadena de entrada sustituyendo ''á'',''é'',''í'',''ó'',''ú'' por ''A'',''E'',''I'',''O'',''U'' respectivamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RemoverTildes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RemoverTildes';
GO
