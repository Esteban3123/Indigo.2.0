
CREATE FUNCTION [dbo].[MedicosEsp] (@Medico as nvarchar(20))
RETURNS nvarchar (60)
AS
BEGIN

declare @Especialidad nvarchar(60)

SELECT @Especialidad=CODESPEC1 FROM INPROFSAL WHERE CODPROSAL=@Medico

RETURN @Especialidad

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado el código de un profesional de la salud (médico), devuelve su especialidad principal registrada en el maestro de profesionales. Consulta la tabla INPROFSAL usando el código del médico (CODPROSAL) para recuperar el código de la primera especialidad (CODESPEC1). Se utiliza para mostrar la especialidad del médico en reportes, órdenes médicas, agendamiento y cualquier contexto donde se necesite identificar rápidamente a qué especialidad pertenece un profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MedicosEsp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MedicosEsp';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener el código de especialidad principal asociado a un profesional de la salud a partir de su identificador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosEsp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe suministrarse el identificador del profesional de la salud para la búsqueda en INPROFSAL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosEsp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve únicamente la especialidad principal (primera) registrada del profesional.; Si el profesional no existe en INPROFSAL, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosEsp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Profesional de la salud; Especialidad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosEsp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INPROFSAL: Cuando CODPROSAL coincide con el identificador recibido, retorna el valor de CODESPEC1; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosEsp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosEsp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosEsp';
GO
