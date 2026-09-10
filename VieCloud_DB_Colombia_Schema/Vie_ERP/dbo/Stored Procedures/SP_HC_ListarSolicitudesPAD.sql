CREATE PROCEDURE [dbo].[SP_HC_ListarSolicitudesPAD](
@Centro varchar(20),
@Unidad varchar(1000) --Vienen varias unidades funcionales!!
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		SELECT  DISTINCT		 
		'Solicitudes desde hospitalización' AS TipoSolicitud		
		,I.NUMINGRES 
		,UF.UFUDESCRI 
		,P.IPCODPACI	
		,PAC.IPNOMCOMP AS NOMPACIENTE
		,ENT.NOMENTIDA 
		,P.FECHAREGISTRO AS FECHAREGINGRESO
		,I.CODTIPPAC AS TipoPoblacion
		,DG.NOMDIAGNO AS DiagPrincipal
		,O.DESCCAMAS AS cama
		,CASE WHEN p.TIPOATENCION = 1 THEN 'Atención domiciliaria' WHEN p.TIPOATENCION = 2 THEN 'Hospitalización en casa' END AS TipoAtencion
		,PAC.IPFECNACI AS 'Fecha Nacimiento'
		, CAST('' AS CHAR(50)) AS Edad
		,I.ESTADO
		,P.ID AS PADCONTROLID
		FROM ADINGRESO I
		INNER JOIN PADCONTROL P  ON  P.NUMINGRES=I.NUMINGRES  AND P.IPCODPACI =I.IPCODPACI	
		INNER JOIN INENTIDAD ENT with (nolock) ON I.CODENTIDA=ENT.CODENTIDA 
	    INNER JOIN INPACIENT PAC with (nolock) ON I.IPCODPACI =PAC.IPCODPACI  
		INNER JOIN INDIAGNOP DP with (nolock) ON PAC.IPCODPACI =I.IPCODPACI AND I.NUMINGRES=DP.NUMINGRES AND CODDIAPRI =1
		INNER JOIN INDIAGNOS DG with (nolock) ON DP.CODDIAGNO =DG.CODDIAGNO 
		left  JOIN CHREGESTA CH  with (nolock) on CH.NUMINGRES = I.NUMINGRES AND CH.REGESTADO = 1 	
        left JOIN CHCAMASHO O with (nolock)  ON  O.CODICAMAS =CH.CODICAMAS	AND CH.REGESTADO = 1 	
		INNER JOIN INUNIFUNC UF with (nolock) ON UF.UFUCODIGO = i.UFUACTPAC 
		AND I.IINGREPOR  IN(1,3,4,5)
		AND I.IESTADOIN IN('','C', 'P')
		AND P.ESTADO =1		
		AND I.CODCENATE =@Centro AND I.UFUACTPAC IN (SELECT Value FROM dbo.splitstring(@Unidad))
		--AND P.NUMEFOLIO =(select max(cast(NUMEFOLIO as int) )  from hchispaca where NUMINGRES =I.NUMINGRES )
		AND NOT EXISTS(SELECT A.NUMEROINGRESO  FROM PADASIGNACION A WHERE A.NUMEROINGRESOORG = I.NUMINGRES )
		UNION ALL
	   	SELECT DISTINCT 'Solicitudes externas' AS TipoSolicitud
		,I.NUMINGRES 
		,NULL AS UFUDESCRI 
		,I.IPCODPACI 
		,PAC.IPNOMCOMP AS NOMPACIENTE
		,ENT.NOMENTIDA 
		,I.IFECHAING  AS FECHAREGINGRESO	
		,I.CODTIPPAC AS TipoPoblacion
		,NULL AS DiagPrincipal
		,NULL AS cama		
		,'Sin Información' AS TipoAtencion
		,PAC.IPFECNACI AS 'Fecha Nacimiento'
		,CAST('' AS CHAR(50)) AS Edad
		,I.ESTADO
		,NULL  AS PADCONTROLID
		FROM ADINGRESO I		
		INNER JOIN INUNIFUNC UF with (nolock) ON UF.UFUCODIGO =I.UFUACTPAC 
		INNER JOIN INENTIDAD ENT with (nolock) ON I.CODENTIDA=ENT.CODENTIDA 
		INNER JOIN INPACIENT PAC with (nolock) ON I.IPCODPACI =PAC.IPCODPACI 		
		WHERE I.IINGREPOR  =2
		AND I.IESTADOIN in('', 'P')
		AND UF.UFUTIPUNI =32		
		AND I.CODCENATE = @Centro AND I.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@Unidad))
		AND NOT EXISTS(SELECT A.NUMEROINGRESO  FROM PADASIGNACION A WHERE A.NUMEROINGRESO = I.NUMINGRES)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de ingreso al Programa de Atención Domiciliaria (PAD) pendientes de asignación, combinando dos fuentes: pacientes hospitalizados que han sido referidos al PAD desde una unidad funcional del hospital, y solicitudes externas de atención domiciliaria que ingresaron directamente sin hospitalización previa. Para cada solicitud muestra el número de ingreso, la cédula y nombre del paciente, la entidad aseguradora (EPS/pagador), la fecha de registro, el tipo de población, el diagnóstico principal (CIE-10), la cama asignada si aplica, el tipo de atención domiciliaria y la fecha de nacimiento. Filtra por centro de atención y una o varias unidades funcionales recibidas como parámetro, excluyendo los casos que ya tienen asignación PAD activa. Se usa en el módulo de gestión domiciliaria para que el equipo coordinador identifique y priorice los pacientes que aún no han sido asignados a un profesional o equipo de atención en casa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSolicitudesPAD';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSolicitudesPAD';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista solicitudes de atención domiciliaria/hospitalización en casa (PAD), combinando solicitudes generadas desde hospitalización y solicitudes externas pendientes de asignación, filtradas por centro y unidades funcionales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de unidades debe ser parseable por dbo.splitstring (lista delimitada).; Deben existir registros de paciente, entidad, unidad funcional y diagnóstico principal asociados al ingreso para que aparezca en el bloque de hospitalización.; El ingreso no debe estar ya asignado en PADASIGNACION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna ingresos ya asignados en PADASIGNACION.; Solo considera ingresos del centro de atención solicitado.; Solo considera ingresos cuya unidad funcional actual esté en la lista recibida.; Para el bloque de hospitalización solo se toma el diagnóstico marcado como principal (CODDIAPRI=1).; Para solicitudes externas la unidad funcional debe ser de tipo 32 (PAD).; La cama mostrada corresponde al registro de estado activo (CHREGESTA.REGESTADO=1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Programa de Atención Domiciliaria (PAD); Atención domiciliaria; Hospitalización en casa; Ingreso hospitalario; Paciente; Entidad responsable de pago; Diagnóstico principal; Unidad funcional; Cama hospitalaria; Asignación de solicitud; Tipo de población; Solicitud externa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Retorna ingresos hospitalarios con PADCONTROL.ESTADO=1, IINGREPOR IN (1,3,4,5), IESTADOIN IN ('''',''C'',''P''), del centro y unidades indicadas, sin asignación previa en PADASIGNACION, etiquetados como ''Solicitudes desde hospitalización''.; [RETURN_RESULT] RESULTSET: Retorna ingresos externos con IINGREPOR=2, IESTADOIN IN ('''',''P''), unidad funcional de tipo UFUTIPUNI=32, del centro y unidades indicadas, sin asignación en PADASIGNACION, etiquetados como ''Solicitudes externas''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PADCONTROL.TIPOATENCION = 1 → Se etiqueta como ''Atención domiciliaria'' else Si TIPOATENCION = 2 se etiqueta ''Hospitalización en casa''; en otros casos NULL; si Bloque 1: ADINGRESO.IINGREPOR IN (1,3,4,5) y IESTADOIN IN ('''',''C'',''P'') y PADCONTROL.ESTADO=1 → Incluye el ingreso como ''Solicitudes desde hospitalización'' con datos de cama, diagnóstico y unidad; si Bloque 2: ADINGRESO.IINGREPOR=2 y IESTADOIN IN ('''',''P'') y UFUTIPUNI=32 → Incluye el ingreso como ''Solicitudes externas'' sin diagnóstico ni cama; si EXISTS en PADASIGNACION para el ingreso → Se excluye del resultado (no se considera solicitud pendiente)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.PADCONTROL; dbo.INENTIDAD; dbo.INPACIENT; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.PADASIGNACION', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesPAD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesPAD';
-- GO
