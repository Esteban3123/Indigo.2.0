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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes en espera de clasificación de triage o pre-triage en urgencias, según el tipo de proceso solicitado. Para el triage formal (Tipo=1), combina los contactos de urgencias (ADCONTURG) con los datos del paciente (INPACIENT), la aseguradora o EPS (HealthAdministrator) y la trazabilidad del pre-triage (TrazabilidadPreTriage), mostrando nombre, identificación/cédula, entidad pagadora, priorización (alto/medio/bajo), grupo poblacional, tiempo de espera en minutos y si el paciente ya fue llamado o no. Para el pre-triage (Tipo=2), lista únicamente los pacientes que aún no tienen registro en TrazabilidadPreTriage, indicando también si ya recibieron un primer llamado. Filtra por centro de atención, unidad funcional y estado del contacto, ordenando los resultados por hora de llegada para apoyar la gestión operativa de urgencias.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'ListTriagePatients';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'ListTriagePatients';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la lista de pacientes en urgencias pendientes o sin respuesta al llamado, según se requiera para clasificación de Triage o de Pre-Triage, con tiempos de espera, priorización y datos del pagador.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListTriagePatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@Tipo debe ser 1 (Clasificación Triage) o 2 (Pre-triage); otros valores no producen resultados.; @Estado, @CentroAtencion y @UnidadFuncional deben corresponder a valores válidos de CONESTADO, CODCENATE y UFUCODIGO en ADCONTURG.; La función Common.GETDATE() y Contract.fnCareGroupEntityType deben estar disponibles.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListTriagePatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El listado siempre se restringe a un único centro de atención, unidad funcional y estado del contacto.; Los resultados se ordenan cronológicamente por fecha/hora de llegada (IPFECLLEGA ascendente).; Minutos de espera y ''Barra'' se calculan como DATEDIFF en minutos entre la llegada y Common.GETDATE() (hora del sistema/dominio).; En la rama Pre-Triage se excluyen pacientes que ya tienen registro en Admissions.TrazabilidadPreTriage (mutuamente excluyente con flujo de triage clasificado).; El tipo de entidad se resuelve mediante la función Contract.fnCareGroupEntityType sobre EntityType de la administradora.; La columna Edad siempre se devuelve como cadena vacía (cálculo delegado al consumidor).', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListTriagePatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage; Pre-triage; Clasificación de urgencias; Llamado a paciente; Priorización; Grupo poblacional; Entidad administradora de salud (EPS); Centro de atención; Unidad funcional; Trazabilidad de pre-triage', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListTriagePatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @Tipo=1: retorna pacientes de ADCONTURG filtrados por CONESTADO=@Estado, CODCENATE=@CentroAtencion y UFUCODIGO=@UnidadFuncional, con agrupación dependiente de PRIMERLLA y priorización mapeada de Prioritization.; [RETURN_RESULT] resultset: Cuando @Tipo=2: retorna pacientes de ADCONTURG con los mismos filtros pero solo aquellos que NO existen en Admissions.TrazabilidadPreTriage (NOT EXISTS por CODCONCEC e IPCODPACI), agrupados según DateCallOnePreTriage.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListTriagePatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Tipo = 1 (Clasificación Triage) → Lista pacientes en urgencias filtrados por estado, centro de atención y unidad funcional, agrupándolos según PRIMERLLA: si PRIMERLLA=1 quedan en ''Pacientes sin respuesta al llamado de clasificación triage'', en caso contrario en ''pendientes de llamados a clasificación triage''. Incluye priorización y tipo de población tomado de TrazabilidadPreTriage cuando exista. else Si @Tipo = 2 ejecuta la rama Pre-Triage.; si @Tipo = 2 (Pre-Triage) → Lista los pacientes que aún NO tienen registro en Admissions.TrazabilidadPreTriage para el consecutivo y paciente; los agrupa según DateCallOnePreTriage: si ya tiene fecha de primer llamado quedan como ''sin respuesta al llamado de clasificación Pre-triage'', si no, ''pendientes de llamados a clasificación Pre-triage''.; si Coalesce de nombre del paciente: ISNULL(c.IPNOMCOMP, a.IPNOMCOMP) → Se prioriza el nombre del maestro de pacientes (INPACIENT); si no existe, se usa el nombre registrado en el contacto de urgencias.; si CASE Prioritization (solo Tipo=1) → 1 → ''Alto'', 2 → ''Medio'', cualquier otro valor → ''Bajo''.; si TipoPoblacion en Tipo=1: ISNULL(z.PopulationGroup, A.CODTIPPAC) → Toma el grupo poblacional desde la trazabilidad de pre-triage si existe; si no, usa el tipo de paciente del contacto de urgencias.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListTriagePatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONTURG; Contract.HealthAdministrator; dbo.INPACIENT; Admissions.TrazabilidadPreTriage', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListTriagePatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListTriagePatients';
-- GO
