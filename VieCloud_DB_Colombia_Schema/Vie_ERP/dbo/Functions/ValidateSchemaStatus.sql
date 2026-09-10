
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[ValidateSchemaStatus] (@TIPOCONSULTA AS integer, @IDHCORDQUIMIo as varchar(25), @CODPRODUC AS Varchar(50), @CICLO AS INT, @DIA AS INT)
RETURNS varchar (300)
AS
BEGIN

DECLARE @ESTADOMODIFICADO INT = 0
	
IF @TIPOCONSULTA = 1
BEGIN
	SET @ESTADOMODIFICADO = (SELECT TOP 1 State FROM EHR.HCORDCICLOS WHERE IDHCORDQUIMIo = @IDHCORDQUIMIo AND State <> 1)
END
ELSE
BEGIN	
	SET @ESTADOMODIFICADO = (SELECT TOP 1 State FROM EHR.HCORDMEDICAM WHERE IDHCORDQUIMIo = @IDHCORDQUIMIo AND State <> 1 AND CODPRODUC = @CODPRODUC AND (@CICLO = 0 OR CICLO = @CICLO) AND (@DIA = 0 OR DIA = @DIA))
END

/*
Función que me indica si el medicamento o el esquema fue modificado 1 - sin modificar, 2 - medicamento añadido, 3 - modificado, 4 - suspendido 
*/

RETURN @ESTADOMODIFICADO

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que verifica el estado actual de un esquema de quimioterapia o de un medicamento específico dentro de un ciclo oncológico, retornando un código numérico que indica si fue modificado. Los posibles estados son: 1 = sin modificar, 2 = medicamento añadido, 3 = modificado, 4 = suspendido. Según el tipo de consulta (@TIPOCONSULTA), evalúa el estado a nivel del ciclo completo (consultando HCORDCICLOS) o a nivel del medicamento puntual dentro del ciclo (consultando HCORDMEDICAM, filtrando por producto, número de ciclo y día de administración). Se usa para determinar si una orden de quimioterapia o un medicamento oncológico ha sido alterado respecto a su versión original antes de ser dispensado o administrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ValidateSchemaStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ValidateSchemaStatus';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un esquema de quimioterapia o un medicamento específico de un ciclo/día fue modificado, devolviendo el estado distinto de ''sin modificar''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateSchemaStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el identificador del esquema de quimioterapia consultado en EHR.HCORDCICLOS o EHR.HCORDMEDICAM según el tipo de consulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateSchemaStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'State=1 representa ''sin modificar'' y se excluye explícitamente de la búsqueda; Si no hay registros con State<>1, el resultado por defecto es 0; Los códigos de estado posibles documentados son: 1 sin modificar, 2 medicamento añadido, 3 modificado, 4 suspendido; Solo se retorna un único estado (TOP 1) aunque existan varios registros modificados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateSchemaStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Esquema de quimioterapia; Ciclo de tratamiento oncológico; Medicamento ordenado; Modificación de orden médica; Suspensión de medicamento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateSchemaStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Si TIPOCONSULTA=1 retorna el primer State<>1 encontrado en EHR.HCORDCICLOS para el esquema; en otro caso retorna el primer State<>1 en EHR.HCORDMEDICAM filtrando por producto y, si se indican, ciclo y día', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateSchemaStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOCONSULTA = 1 → Consulta el estado modificado a nivel de ciclo del esquema en EHR.HCORDCICLOS else Consulta el estado modificado a nivel de medicamento (producto, ciclo opcional, día opcional) en EHR.HCORDMEDICAM; si CICLO = 0 → No se filtra por ciclo (cualquier ciclo aplica) else Se filtra por el ciclo indicado; si DIA = 0 → No se filtra por día (cualquier día aplica) else Se filtra por el día indicado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateSchemaStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDCICLOS; EHR.HCORDMEDICAM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateSchemaStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateSchemaStatus';
GO
