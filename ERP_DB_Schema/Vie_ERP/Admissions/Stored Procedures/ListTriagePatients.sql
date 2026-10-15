CREATE PROCEDURE [Admissions].[ListTriagePatients]
@Tipo integer, --1:Clasificacion Triage 2:Pretriage 
@Estado as integer,
@CentroAtencion varchar(10),
@UnidadFuncional varchar(10)
AS
BEGIN
	SET NOCOUNT ON;

if @Tipo = 1 begin --Clasificacion Triage

	SELECT  CAST('' AS varchar) AS Edad,c.IPFECNACI,CAST(A.IPFECLLEGA AS DATETIME) as 'Fecha Hora Llegada', RTRIM(A.IPCODPACI) as Identificacion, isnull(Rtrim(c.IPNOMCOMP), Rtrim(a.IPNOMCOMP)) as 'Nombre Paciente', RTRIM(B.Name) as Entidad,DATEDIFF(mi,A.IPFECLLEGA,Common.GETDATE()) AS [Minutos],DATEDIFF(mi,A.IPFECLLEGA,Common.GETDATE()) AS 'Barra',A.CODCONCEC AS Consecutivo 
		,iif(PRIMERLLA = 1, '2. Pacientes sin respuesta al llamado de clasificación triage', '1. Pacientes pendientes de llamados a clasificación triage' )  as Agrupacion
		,Contract.[fnCareGroupEntityType] (b.EntityType) as 'Tipo Entidad'
		,case Prioritization when 1 then 'Alto' when 2 then 'Medio' else 'Bajo' end Priorizacion
		,isnull(z.PopulationGroup,A.CODTIPPAC) as TipoPoblacion
	FROM dbo.ADCONTURG A with(nolock)
			LEFT OUTER JOIN Contract.HealthAdministrator B with(nolock) ON A.CODENTIDA= b.code
			LEFT OUTER JOIN dbo.INPACIENT C with(nolock) ON A.IPCODPACI=C.IPCODPACI
			Left Join  [Admissions].[TrazabilidadPreTriage] z with(nolock) on z.CODCONCEC = a.CODCONCEC and z.IPCODPACI = a.IPCODPACI 
	WHERE CONESTADO = @Estado AND CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional
	ORDER BY IPFECLLEGA
	
end else if @Tipo = 2 begin --Pre-Triage
		
	SELECT  CAST('' AS varchar) AS Edad,A.CODTIPPAC as TipoPoblacion,c.IPFECNACI,CAST(A.IPFECLLEGA AS DATETIME) as 'Fecha Hora Llegada',RTRIM(A.IPCODPACI) as Identificacion,  isnull(Rtrim(c.IPNOMCOMP), Rtrim(a.IPNOMCOMP)) as 'Nombre Paciente', RTRIM(B.Name) as Entidad,DATEDIFF(mi,A.IPFECLLEGA,Common.GETDATE()) AS [Minutos],DATEDIFF(mi,A.IPFECLLEGA,Common.GETDATE()) AS 'Barra',A.CODCONCEC AS Consecutivo 
		,iif(DateCallOnePreTriage is not null, '2. Pacientes sin respuesta al llamado de clasificación Pre-triage', '1. Pacientes pendientes de llamados a clasificación Pre-triage' )  as Agrupacion
		,Contract.[fnCareGroupEntityType] (b.EntityType) as 'Tipo Entidad'
		FROM dbo.ADCONTURG A with(nolock)
			LEFT OUTER JOIN Contract.HealthAdministrator B with(nolock) ON A.CODENTIDA= b.code
			LEFT OUTER JOIN dbo.INPACIENT C with(nolock) ON A.IPCODPACI=C.IPCODPACI
	WHERE CONESTADO = @Estado AND CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional and 
		  not exists (select * from [Admissions].[TrazabilidadPreTriage] z where z.CODCONCEC = a.CODCONCEC and z.IPCODPACI = a.IPCODPACI)
	ORDER BY IPFECLLEGA

end

END