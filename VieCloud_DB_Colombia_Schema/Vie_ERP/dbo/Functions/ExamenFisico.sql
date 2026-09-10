

CREATE FUNCTION [dbo].[ExamenFisico] (@Cedula varchar(25))
RETURNS datetime
AS
BEGIN

declare 
@fechamax as datetime

SELECT  top 1 @fechamax =max(FECREGITE)
FROM         HCEXFISIC
WHERE IPCODPACI = @Cedula 
group by FECREGITE

RETURN @fechamax

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que recibe la cédula o identificación de un paciente y retorna la fecha más reciente en que se registró un examen físico para ese paciente. Consulta la tabla de exámenes físicos de historia clínica (HCEXFISIC) buscando el último registro por fecha. Es útil para saber cuándo fue el último examen físico documentado de un paciente, por ejemplo en controles de evolución clínica o auditorías de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ExamenFisico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ExamenFisico';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha del último registro de examen físico realizado a un paciente identificado por su cédula.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse la cédula/identificación del paciente para filtrar los exámenes físicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera registros del paciente identificado por su cédula; Devuelve únicamente la fecha más reciente entre los registros existentes; Si el paciente no tiene registros de examen físico, retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Examen físico; Paciente; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCEXFISIC: Cuando existen registros en HCEXFISIC para el paciente, retorna la fecha máxima (más reciente) de FECREGITE; en caso contrario retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisico';
GO
