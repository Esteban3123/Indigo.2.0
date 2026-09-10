
-- =============================================
-- Author:		<Juan Montealegre>
-- Create date: <28/03/2014>
-- Description:	<Valor GlasGow>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarUltimoGlasGowPaciente]
(
@CodigoPaciente varchar(25),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT TOP 1  VALGLASGO  FROM HCCTRNEUR WHERE IPCODPACI = @CodigoPaciente AND NUMINGRES = @NumeroIngreso ORDER BY FECREGIST DESC 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve el último valor registrado en la Escala de Glasgow para un paciente en un ingreso hospitalario específico. Recibe como parámetros la cédula del paciente y el número de ingreso, y busca en el registro de evaluaciones neurológicas (HCCTRNEUR) el puntaje de Glasgow más reciente según la fecha de registro. Es utilizado para conocer rápidamente el estado de consciencia neurológica del paciente durante su hospitalización, mostrando únicamente el dato más actual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarUltimoGlasGowPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarUltimoGlasGowPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el valor de Glasgow más reciente registrado en el control neurológico de un paciente para un ingreso específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarUltimoGlasGowPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente y el ingreso indicados con al menos un registro neurológico para retornar valor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarUltimoGlasGowPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna un único valor (TOP 1), correspondiente al último registro por fecha de registro; El filtro siempre exige coincidencia simultánea de paciente e ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarUltimoGlasGowPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Control neurológico; Escala de Glasgow', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarUltimoGlasGowPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCCTRNEUR: Cuando existen registros con IPCODPACI y NUMINGRES coincidentes, devuelve el VALGLASGO del registro con FECREGIST más reciente (TOP 1 ORDER BY FECREGIST DESC)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarUltimoGlasGowPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCTRNEUR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarUltimoGlasGowPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarUltimoGlasGowPaciente';
-- GO
