

CREATE PROCEDURE [dbo].[SPCH_ListarControlTriage]
(
@CentroAtencion Char(10), 
@UnidadFuncional Char(10), 
@Usuario char(20) 
)
AS
BEGIN
	SET NOCOUNT ON;

	--necesitamos saber si la unidad que esta haciendo la consulta es de atención prioritaria o urgencias
declare @AtencionPrioritaria bit = (select case UFUTIPUNI when 31 then 1 else 0 end from INUNIFUNC where UFUCODIGO = @UnidadFuncional)

if @AtencionPrioritaria = 1 begin
	SELECT A.CODTIPPAC as Tipo, A.NUMINGRES, CAST(A.IFECHAING AS DATETIME) as 'Fecha Hora Llegada',RTRIM(A.IPCODPACI) as Identificacion,  RTRIM(C.IPNOMCOMP) as 'Nombre Paciente',
	 RTRIM(B.NOMENTIDA) as Entidad,0 AS [Minutos],0 AS 'Barra','' AS TipoEntidad,'' AS Consecutivo,CAST(0 AS BIT) AS Ausencia,IPFECNACI AS 'Fecha Nacimiento' ,
	A.PRIMERLLA AS LlamadoUno,A.SEGUNDLLA AS LlamadoDos,A.TERCERLLA AS LlamadoTres
	, CASE WHEN C.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
	, C.ZONAPARTADA,
	D.RIESGOAGRE,
	(SELECT count(*) FROM dbo.ADPOBESPEPAC Z with(nolock) INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE  WHERE IPCODPACI = C.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
	CONVERT(BIT,0) AS Riesgo,
	CAST('' AS CHAR(50)) AS Edad
	FROM dbo.ADINGRESO A with(nolock)
	INNER JOIN dbo.INEntidad B with(nolock) ON A.CODENTIDA=b.Codentida 
	LEFT OUTER JOIN dbo.INPacient C with(nolock) ON A.IPCODPACI=C.IPCODPACI 
	LEFT OUTER JOIN dbo.ADACTIVID D with(nolock) ON C.CODACTIVI = D.codactivi
	left outer join dbo.ADTRIAGEU T with(nolock) on T.NUMINGRES = A.NUMINGRES
	WHERE A.CODCENATE = @CentroAtencion AND A.UFUCODIGO = @UnidadFuncional AND A.REQTRIAGE = 1 AND A.IESTADOIN = ' ' AND T.TRIANUMER IS NULL
end
else begin
	SELECT  A.CODTIPPAC as Tipo, CAST(A.IPFECLLEGA AS DATETIME) as 'Fecha Hora Llegada',RTRIM(A.IPCODPACI) as Identificacion, ISNULL(Rtrim(C.IPNOMCOMP),Rtrim(A.IPNOMCOMP)) as 'Nombre Paciente', RTRIM(B.NOMENTIDA) as Entidad,0 AS [Minutos],0 AS 'Barra','' AS TipoEntidad,A.CODCONCEC AS Consecutivo,CAST(0 AS BIT) AS Ausencia,IPFECNACI AS 'Fecha Nacimiento' ,
	A.PRIMERLLA AS LlamadoUno,A.SEGUNDLLA AS LlamadoDos,A.TERCERLLA AS LlamadoTres
	, CASE WHEN C.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
	, C.ZONAPARTADA,
	D.RIESGOAGRE,
	(SELECT count(*) FROM dbo.ADPOBESPEPAC Z with(nolock) INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE  WHERE IPCODPACI = C.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
	CONVERT(BIT,0) AS Riesgo,
	CAST('' AS CHAR(50)) AS Edad
	FROM dbo.ADCONTURG A with(nolock)
	INNER JOIN dbo.INEntidad B with(nolock) ON A.CODENTIDA=b.Codentida 
	LEFT OUTER JOIN dbo.INPacient C with(nolock) ON A.IPCODPACI=C.IPCODPACI 
	LEFT OUTER JOIN dbo.ADACTIVID D with(nolock) ON C.CODACTIVI = D.codactivi
	WHERE CONESTADO = '1' AND CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional AND A.CODCONCEC NOT IN 
	(SELECT NUMDOCUME FROM dbo.INDOCUMEN with(nolock) WHERE CODDOCUME='1' AND CODUSUARI<>@Usuario) 
end

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes pendientes de clasificación de triage en una unidad funcional de urgencias o atención prioritaria de un centro de atención. Según el tipo de unidad (prioritaria o urgencias convencionales), consulta los ingresos activos sin triage registrado o los pacientes en espera del control de urgencias, enriqueciendo la información con datos del paciente (nombre, documento, fecha de nacimiento, zona apartada, tipo de documento para alertas ASMS), la entidad aseguradora o EPS, el riesgo agregado de la actividad de admisión y si el paciente pertenece a alguna población especial de interés. Se usa en el módulo de triage para que el personal de urgencias identifique y priorice a los pacientes que aún no han sido valorados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarControlTriage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarControlTriage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes pendientes de triage en una unidad funcional, diferenciando entre unidades de atención prioritaria y unidades de urgencias para determinar el origen de los datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La unidad funcional debe existir en INUNIFUNC para determinar si es de atención prioritaria (UFUTIPUNI=31); Las entidades referenciadas por los ingresos/contactos deben existir en INEntidad (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan pacientes que aún no tienen triage realizado; La clasificación de la unidad como ''atención prioritaria'' se determina exclusivamente por UFUTIPUNI=31; En la rama de urgencias se respeta exclusividad por usuario: un contacto ya tomado/documentado por otro usuario (INDOCUMEN.CODDOCUME=''1'') no se muestra; El conteo de población especial se limita a TIPOPOESPERIES=1; Los campos Minutos, Barra, TipoEntidad, Ausencia, Riesgo y Edad se devuelven con valores fijos/placeholder, calculándose en otra capa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage; Atención prioritaria; Urgencias; Unidad funcional; Centro de atención; Paciente; Entidad (aseguradora); Población especial; Riesgo de agresión; Zona apartada; Llamados a paciente; Ingreso/Admisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ADINGRESO: Cuando la unidad es de atención prioritaria (UFUTIPUNI=31) se retornan ingresos del centro y unidad indicados con REQTRIAGE=1, IESTADOIN='' '' y sin registro asociado en ADTRIAGEU (TRIANUMER IS NULL); [RETURN_RESULT] ADCONTURG: Cuando la unidad NO es de atención prioritaria se retornan contactos de urgencias con CONESTADO=''1'' del centro y unidad indicados, excluyendo aquellos cuyo CODCONCEC esté en INDOCUMEN con CODDOCUME=''1'' registrado por un usuario distinto al actual', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La unidad funcional tiene UFUTIPUNI = 31 (atención prioritaria) → Consulta ingresos en ADINGRESO pendientes de triage (sin registro en ADTRIAGEU) else Consulta contactos de urgencias en ADCONTURG activos (CONESTADO=''1'') excluyendo los ya documentados por otro usuario; si IPTIPODOC del paciente IN (6,7) → Marca el campo ASMS = 1 (de lo contrario 0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC; dbo.ADINGRESO; dbo.INEntidad; dbo.INPacient; dbo.ADACTIVID; dbo.ADTRIAGEU; dbo.ADCONTURG; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.INDOCUMEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlTriage';
-- GO
