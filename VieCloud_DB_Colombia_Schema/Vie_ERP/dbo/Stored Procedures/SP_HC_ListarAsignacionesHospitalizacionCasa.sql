

CREATE PROCEDURE [dbo].[SP_HC_ListarAsignacionesHospitalizacionCasa](
@Centro varchar(20),
@Unidad varchar(1000) --Vienen varias unidades funcionales!!
)
AS

BEGIN	
	SET NOCOUNT ON;
		SELECT 
		I.NUMINGRES 
		,PA.ID
		,I.CODTIPPAC AS TipoPoblacion		
		,I.IPCODPACI	
		,PAC.IPNOMCOMP
		,IPNOMCOMP NOMPACIENTE
		,PAC.IPFECNACI AS 'FECHANACIMIENTO'
		, CAST('' AS CHAR(50)) AS EDAD
		,ENT.NOMENTIDA 
		,PAC.IPDIRECCI +' ' + UB.UBINOMBRE  AS Ubicacion
		,DG.NOMDIAGNO AS DiagPrincipal
		,(SELECT NOMMEDICO FROM INPROFSAL WHERE  CODPROSAL= (SELECT CODPROSAL FROM
			(
			SELECT ROW_NUMBER() OVER (ORDER BY CAST(NUMEFOLIO AS INT) DESC) AUTO_ID
			, *
			FROM HCHISPACA
			WHERE NUMINGRES = I.NUMINGRES
			) T
			WHERE AUTO_ID = 1) ) AS NOMMEDICO

			,(CASE  (SELECT INDICAPAC FROM
			(
			SELECT ROW_NUMBER() OVER (ORDER BY CAST(NUMEFOLIO AS INT) DESC) AUTO_ID
			, *
			FROM HCHISPACA
			WHERE NUMINGRES = I.NUMINGRES
			) T
			WHERE AUTO_ID = 1) WHEN 12 THEN 1 ELSE 2 END ) AS TIENEORDENALTA
			,CONVERT(VARCHAR(20),PA.FECHAREGISTRO,120) AS FECHAREGISTRO
		    ,DATEDIFF(DAY, PA.FECHAREGISTRO, [Common].[GETDATE]()) DIAS
		,CASE WHEN PAC.IPTIPODOC IN (6, 7) THEN 1
			ELSE 0
		 END AS ASMS
		,PAC.ZONAPARTADA
		,L.RIESGOAGRE
		,(SELECT COUNT(1) FROM ADPOBESPEPAC Z WITH (NOLOCK) INNER JOIN ADPOBESPE X WITH (NOLOCK) ON X.ID = Z.IDADPOBESPE WHERE Z.IPCODPACI = PAC.IPCODPACI AND X.TIPOPOESPERIES = 1) AS POBESPECIAL
		,I.VIVESOLO
		,(SELECT COUNT(1) FROM ADACOMPAN A WITH (NOLOCK) WHERE A.NUMINGRES = I.NUMINGRES) AS ACOMPANANTES
		,CONVERT(BIT, 0) AS RIESGO			
		FROM PADASIGNACION PA
		inner join ADINGRESO I  with (nolock) on PA.NUMEROINGRESO =I.NUMINGRES					
		INNER JOIN INUNIFUNC UF with (nolock) ON UF.UFUCODIGO =I.UFUCODIGO
		INNER JOIN INENTIDAD ENT with (nolock) ON I.CODENTIDA=ENT.CODENTIDA 
	    INNER JOIN INPACIENT PAC with (nolock) ON I.IPCODPACI =PAC.IPCODPACI  
		INNER JOIN INUBICACI AS UB with (nolock) ON PAC.AUUBICACI = UB.AUUBICACI 
		INNER JOIN ADACTIVID L WITH (NOLOCK) ON PAC.CODACTIVI = L.CODACTIVI
		LEFT JOIN INDIAGNOP DP with (nolock) ON PAC.IPCODPACI =I.IPCODPACI AND I.NUMINGRES=DP.NUMINGRES AND CODDIAPRI =1
		LEFT JOIN INDIAGNOS DG with (nolock) ON DP.CODDIAGNO =DG.CODDIAGNO 		 						
		WHERE PA.TIPOATENCION =2
		and PA.ESTADO = 1		
		AND I.IESTADOIN IN('', 'P')
		and UF.UFUTIPUNI = 32 
		AND I.CODCENATE =@Centro AND I.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@Unidad))			
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes activos en el programa de Hospitalización en Casa (PAD tipo atención 2), filtrando por centro de atención y una o más unidades funcionales de tipo hospitalización domiciliaria (tipo 32). Combina datos del ingreso hospitalario (ADINGRESO), la asignación PAD (PADASIGNACION), la información demográfica del paciente (INPACIENT), la entidad aseguradora (INENTIDAD), el diagnóstico principal CIE-10 (INDIAGNOP/INDIAGNOS) y el médico tratante más reciente según la historia clínica (HCHISPACA/INPROFSAL). Devuelve por cada paciente: número de ingreso, cédula, nombre completo, fecha de nacimiento, edad, dirección, aseguradora, diagnóstico principal, médico responsable, si tiene orden de alta, días en el programa, indicadores de riesgo agregado, población especial, si vive solo y cantidad de acompañantes; información utilizada para el seguimiento y gestión operativa del censo de pacientes en hospitalización domiciliaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes asignados al programa de hospitalización en casa de un centro y unidades funcionales específicas, con datos clínicos, sociodemográficos y de riesgo para gestión del censo domiciliario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro debe existir en ADINGRESO (CODCENATE).; Las unidades funcionales recibidas deben corresponder a unidades de tipo 32 (UFUTIPUNI=32) — típicamente hospitalización en casa.; Debe existir la función dbo.splitstring para descomponer la lista de unidades.; El paciente debe tener actividad (CODACTIVI) registrada en ADACTIVID.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran asignaciones activas (ESTADO=1) del tipo de atención hospitalización en casa (TIPOATENCION=2).; Solo se incluyen ingresos vigentes o pendientes (IESTADOIN en '''' o ''P'').; El diagnóstico mostrado siempre corresponde al diagnóstico principal (CODDIAPRI=1).; El médico tratante mostrado es el de la última evolución registrada en HCHISPACA (mayor NUMEFOLIO).; La unidad funcional siempre debe ser de tipo 32 para aparecer en el listado.; Los días de estancia se calculan desde FECHAREGISTRO de la asignación hasta la fecha actual del sistema.; El campo RIESGO siempre se devuelve como BIT 0 (placeholder).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hospitalización en casa; Asignación de paciente; Ingreso hospitalario; Diagnóstico principal; Médico tratante; Orden de alta; Población especial; Riesgo de agresión; Zona apartada; Acompañantes del paciente; Paciente que vive solo; Unidad funcional; Entidad responsable de pago', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente asignaciones con TIPOATENCION=2 y ESTADO=1 cuyo ingreso esté en estado IESTADOIN '''' o ''P'', en unidades de tipo 32, del centro indicado y unidades filtradas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Última nota de HCHISPACA (mayor NUMEFOLIO) tiene INDICAPAC = 12 → Marca TIENEORDENALTA = 1 (paciente con orden de alta) else Marca TIENEORDENALTA = 2; si PAC.IPTIPODOC IN (6,7) → Marca ASMS = 1 (población especial por tipo de documento) else ASMS = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.PADASIGNACION; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.INPACIENT; dbo.INUBICACI; dbo.ADACTIVID; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCHISPACA; dbo.INPROFSAL; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAsignacionesHospitalizacionCasa';
-- GO
