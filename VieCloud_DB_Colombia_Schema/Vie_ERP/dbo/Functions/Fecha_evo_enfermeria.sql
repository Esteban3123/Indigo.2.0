

CREATE FUNCTION [dbo].[Fecha_evo_enfermeria] (
@IPCODPACI as varchar(25),
@NUMINGRES AS char(10)

)
RETURNS nvarchar(200)
AS
BEGIN

declare @FECREGIST nvarchar(200)

/*SET @grupo= (
SELECT EQUI.CODPROSAL from dbo.HCQXEQUIP AS EQUI WHERE ((EQUI.NUMEFOLIO=@NFOLIO) AND (EQUI.IPCODPACI=@COD)AND (EQUI.CODPROSAL LIKE 'MI%' )))
*/
set @FECREGIST=(select  MIN(HC.FECREGIST)
from HCCTRNOTE AS HC where HC.IPCODPACI=@IPCODPACI AND HC.NUMINGRES=@NUMINGRES)
RETURN (@FECREGIST)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado el código o cédula del paciente y el número de ingreso hospitalario, retorna la fecha más antigua (mínima) en que se registró una nota de enfermería para ese ingreso. Consulta el historial de notas de enfermería de la historia clínica para determinar cuándo comenzaron las evoluciones de enfermería durante una hospitalización. Se usa para conocer la primera fecha de evolución de enfermería de un paciente en un ingreso específico, útil en reportes clínicos, auditorías y seguimiento de atención de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Fecha_evo_enfermeria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Fecha_evo_enfermeria';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener la fecha del primer registro (más temprano) de notas/evolución de enfermería para un paciente en un ingreso específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Fecha_evo_enfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en HCCTRNOTE asociados al paciente e ingreso indicados para obtener una fecha; en caso contrario, el resultado es NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Fecha_evo_enfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran notas asociadas al mismo paciente e ingreso suministrados.; Si no existen notas para el paciente/ingreso, retorna NULL.; Siempre se devuelve la fecha más temprana (MIN) de registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Fecha_evo_enfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; notas/evolución de enfermería', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Fecha_evo_enfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCCTRNOTE: Retorna MIN(FECREGIST) de HCCTRNOTE filtrando por paciente e ingreso coincidentes; NULL si no hay registros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Fecha_evo_enfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCTRNOTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Fecha_evo_enfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Fecha_evo_enfermeria';
GO
