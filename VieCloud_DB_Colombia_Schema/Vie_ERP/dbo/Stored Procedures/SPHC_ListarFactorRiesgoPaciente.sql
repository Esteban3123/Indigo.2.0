CREATE PROC [dbo].[SPHC_ListarFactorRiesgoPaciente]
(
@Identificacion as varchar(25)

)

AS
BEGIN 

	SELECT
		R.DateCreation AS FechaReporte,
		R.IPCODPACI AS Identificacion,
		RTRIM(I.IPNOMCOMP) AS NombrePaciente,
		F.[Description] AS FactorRiesgo,
		r.Observation AS Observacion,
		RTRIM(S.NOMUSUARI) AS FuncionarioReporta,
		R.Observation,
		R.Id as Id,
		r.Intervention,
		R.Status AS Estado,
		CASE R.Status WHEN 1 THEN 'Activo' WHEN 2 THEN 'Inactivo' WHEN 3 THEN 'Anulado' END AS EstadoLabel,
		RTRIM(UA.NOMUSUARI) As FuncionarioActiva,
		R.LastActivationDate As FechaUltimaActivacion,
		RTRIM(M.DESMOTANU) As MotivoAnulacion,
		RT.Name AS TypeOfRisk, CAST(0 AS BIT) AS NewRegister, R.IdRiskFactor
	FROM ReportRiskFactors R WITH (NOLOCK)
	INNER JOIN INPACIENT I WITH (NOLOCK) ON I.IPCODPACI = R.IPCODPACI 
	INNER JOIN RiskFactor F WITH (NOLOCK) ON F.Id = R.IdRiskFactor
	INNER JOIN SEGusuaru S WITH (NOLOCK) ON S.CODUSUARI = R.UserCreation
	LEFT JOIN Admissions.RisksType RT WITH (NOLOCK) ON F.TypeOfRisk = RT.Id
	LEFT JOIN SEGusuaru UA WITH (NOLOCK) ON UA.CODUSUARI = R.LastActivationProfessional
	LEFT JOIN HCMOANULB M WITH (NOLOCK) ON R.CancellationReasonCode = M.CODMOTANU
	WHERE R.IPCODPACI = @Identificacion

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los factores de riesgo clínico registrados para un paciente, identificado por su cédula o documento. Para cada factor de riesgo retorna la fecha de reporte, el nombre del paciente, el tipo de riesgo (por ejemplo caída, úlcera, desnutrición), la observación clínica, la intervención aplicada, el estado (activo, inactivo o anulado) y el motivo de anulación cuando aplica. Combina los reportes de riesgo con el catálogo de factores de riesgo, los datos del paciente y los nombres de los funcionarios que reportaron o activaron el riesgo. Se usa en historia clínica para consultar y hacer seguimiento al perfil de riesgo de un paciente específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarFactorRiesgoPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarFactorRiesgoPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los factores de riesgo reportados para un paciente, con datos de quien lo registró/activó, motivo de anulación y estado legible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarFactorRiesgoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT con la identificación recibida; Cada reporte debe tener un factor de riesgo válido en RiskFactor y un usuario creador en SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarFactorRiesgoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado del reporte solo se interpreta para los valores 1, 2 y 3; otros valores quedan sin etiqueta; Tipo de riesgo, funcionario que activó y motivo de anulación son opcionales (LEFT JOIN); El campo NewRegister siempre se devuelve en 0 (falso), indicando que son registros existentes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarFactorRiesgoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Factor de riesgo; Tipo de riesgo; Anulación de reporte; Activación de factor de riesgo; Funcionario que reporta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarFactorRiesgoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ReportRiskFactors: Devuelve los factores de riesgo donde R.IPCODPACI = identificación recibida, enriquecidos con paciente, factor, tipo de riesgo, funcionarios y motivo de anulación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarFactorRiesgoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si R.Status = 1 → Etiqueta el estado como ''Activo''; si R.Status = 2 → Etiqueta el estado como ''Inactivo''; si R.Status = 3 → Etiqueta el estado como ''Anulado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarFactorRiesgoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ReportRiskFactors; dbo.INPACIENT; dbo.RiskFactor; dbo.SEGusuaru; Admissions.RisksType; dbo.HCMOANULB', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarFactorRiesgoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarFactorRiesgoPaciente';
-- GO
