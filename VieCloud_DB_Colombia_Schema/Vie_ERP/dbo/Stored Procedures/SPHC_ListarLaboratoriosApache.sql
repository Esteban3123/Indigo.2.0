
CREATE PROCEDURE [dbo].[SPHC_ListarLaboratoriosApache]
(
@CodigoPaciente varchar(25),
@NumeroIngreso char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	---Consecutivo orden de laboratorio con resultado, estado 3 
	DECLARE @CODIGOORDEN AS INT
	SELECT
			TOP 1
			@CODIGOORDEN = AUTO 
		FROM
			HCORDLABO
		WHERE 
			IPCODPACI =@CodigoPaciente AND NUMINGRES = @NumeroIngreso
		ORDER BY
			AUTO DESC

	--Obtener los resultados de laboratorio por el consecutivo
	SELECT ANALITO, VALOR FROM INTERLABD WHERE AUTOLABOR =3837
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera los resultados de laboratorio clínico más recientes de un paciente durante un ingreso específico, orientado al cálculo del score Apache (índice de gravedad de pacientes críticos). Primero identifica la última orden de laboratorio registrada para el paciente (cédula o código de paciente) y número de ingreso consultando las órdenes de laboratorio de la historia clínica. Luego obtiene el detalle de analitos y sus valores resultado desde el detalle de resultados de laboratorio, información que alimenta la evaluación de gravedad clínica en unidades de cuidados intensivos o urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarLaboratoriosApache';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarLaboratoriosApache';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna los analitos y valores de resultados de laboratorio asociados a una orden, en el contexto de cálculo de score APACHE para un paciente e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosApache';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una orden de laboratorio para el paciente y número de ingreso si se desea obtener el consecutivo más reciente (aunque luego no se utilice en la consulta final).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosApache';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la orden de laboratorio más reciente (mayor consecutivo AUTO) del paciente para el ingreso indicado.; Los resultados retornados se filtran por un identificador de orden hardcodeado (AUTOLABOR = 3837), por lo que el resultado no depende realmente del paciente ni del ingreso recibidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosApache';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; orden de laboratorio; resultados de laboratorio; analito; score APACHE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosApache';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INTERLABD: Devuelve ANALITO y VALOR donde AUTOLABOR = 3837 (valor fijo en el código, no se usa el consecutivo calculado de la orden del paciente).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosApache';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INTERLABD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosApache';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosApache';
-- GO
