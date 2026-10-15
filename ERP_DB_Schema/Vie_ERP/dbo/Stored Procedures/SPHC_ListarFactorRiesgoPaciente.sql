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
		RTRIM(M.DESMOTANU) As MotivoAnulacion
	FROM ReportRiskFactors R WITH (NOLOCK)
	INNER JOIN INPACIENT I WITH (NOLOCK) ON I.IPCODPACI = R.IPCODPACI 
	INNER JOIN RiskFactor F WITH (NOLOCK) ON F.Id = R.IdRiskFactor
	INNER JOIN SEGusuaru S WITH (NOLOCK) ON S.CODUSUARI = R.UserCreation
	LEFT JOIN SEGusuaru UA WITH (NOLOCK) ON UA.CODUSUARI = R.LastActivationProfessional
	LEFT JOIN HCMOANULB M WITH (NOLOCK) ON R.CancellationReasonCode = M.CODMOTANU
	WHERE R.IPCODPACI = @Identificacion

END