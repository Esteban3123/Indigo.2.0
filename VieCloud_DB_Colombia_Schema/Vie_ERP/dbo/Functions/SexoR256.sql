
CREATE FUNCTION [dbo].[SexoR256] (@Sexo as int)
RETURNS varchar (9)
AS
BEGIN

declare @grupo varchar(9)
SET @grupo=
     CASE WHEN @Sexo=1 THEN 'H' 
          WHEN @Sexo=2 THEN 'M' 
	 END 
RETURN @grupo
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte el código numérico de sexo de un paciente en su abreviatura textual: 1 devuelve ''H'' (hombre) y 2 devuelve ''M'' (mujer). Se usa para normalizar y presentar el género del paciente en reportes, listados y documentos clínicos. Permite estandarizar la visualización del sexo biológico en todo el sistema sin repetir la lógica de conversión en cada consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'SexoR256';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'SexoR256';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de sexo a su carácter representativo (H/M) usado por el formato de reporte R256.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SexoR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El valor de entrada debe ser numérico entero correspondiente a un código de sexo (1 o 2) para obtener resultado significativo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SexoR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado solo puede ser ''H'', ''M'' o NULL.; No se contemplan códigos de sexo distintos de 1 y 2 (no hay mapeo para indeterminado/otro).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SexoR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sexo del paciente; Codificación para reporte R256', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SexoR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Cuando el código de sexo = 1 retorna ''H''; cuando = 2 retorna ''M''; cualquier otro valor retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SexoR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Sexo = 1 → Devuelve ''H'' (hombre); si Sexo = 2 → Devuelve ''M'' (mujer) else Devuelve NULL al no existir rama ELSE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SexoR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SexoR256';
GO
