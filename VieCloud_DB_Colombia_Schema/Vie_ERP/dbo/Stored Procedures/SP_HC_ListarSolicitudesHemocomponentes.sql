-- =============================================
-- Author:		Rafael E Patiño
-- Create date: 3/12/2019
-- Description:	se pasa select a SP listar solicitudes de hemocomponentes
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarSolicitudesHemocomponentes]
@CentroAtencion varchar(10),
@Estados  varchar(100),
@Extramural bit,
@Interfaz bit
AS
BEGIN

--hospitalario
 if @Extramural = 0
 begin
	SELECT
		HEMCO.ID
	   ,HEMBOL.ESTADO
	   ,COMSAM.CODCOMSAM
	   ,COMSAM.DESCOMSAM
	   ,RTRIM(PAC.IPNOMCOMP) AS NOMPACIENTE
	   ,RTRIM(HEMCO.FECORDMED) AS ORDEN
	   ,COUNT(1) AS Cantidad
	   ,RTRIM(PAC.IPCODPACI) AS CODPACIENTE
	   ,PAC.IPGRUPSAN + '' + PAC.IPRHSANGR RH
	   ,UFUNCIONAL =
		CASE
			WHEN RTRIM(UF.UFUDESCRI) IS NOT NULL THEN RTRIM(UF.UFUDESCRI)
			ELSE ING.UFUACTPAC
		END
	   ,PRIORIDAD =
		CASE HEMCO.PRIOTRAN
			WHEN 1 THEN 'Emergencia (hasta 15 min)'
			WHEN 2 THEN 'Urgencia (hasta 1 hora)'
			WHEN 3 THEN 'Urgencia Diferida (hasta 3 horas)'
			WHEN 4 THEN 'Normal (hasta 24 horas)'
		END
	   ,PENDIENTECONFRESERVA = ISNULL((SELECT TOP 1
				PENCONRES
			FROM HCORHEMBOL
			WHERE HCORHEMCOID = HEMCO.ID
			AND PENCONRES = 1)
		, 0)
	   ,HEMCO.NUMINGRES
	   ,RTRIM(HEMCO.NUMEFOLIO) AS FOLIO
	   ,COMSAMID
	   ,HEMBOL.PENCONRES AS PENDIENTECONFRESERVA2
	   ,CAMA.DESCCAMAS
	   ,PAC.IPTIPODOC AS 'TipoDocumento'
	   ,dbo.[TipoDocumentoNombreCompleto](PAC.IPTIPODOC) AS 'NameTypeDocument'
	   ,CONVERT(DATE, PAC.IPFECNACI) AS 'FechaNacimiento'
	   ,CASE PAC.IPSEXOPAC
			WHEN 1 THEN 'M'
			ELSE 'F'
		END AS 'Sexo'
	   ,HEMCO.CODDIAGNO AS 'CodigoDiagnostico'
	   ,DIAG.NOMDIAGNO AS 'DescripcionDiagnostico'
	   ,HEMCO.OBSERVACI AS 'Observaciones'
	   ,UF.UFUCODIGO AS 'CodigoServicio'
	   ,COMSAM.CODCOMSAM AS 'Codigo Sanguineo'
	   ,RTRIM(PROFSALRES.CODPROSAL) AS CodigoMedico
	   ,RTRIM(PROFSALRES.NOMMEDICO) DescricionMedico
	   ,RTRIM(PROFSALRES.CODPROSAL) + ' - ' + RTRIM(PROFSALRES.NOMMEDICO) AS CodigoDrescripcionMedico
	   ,RTRIM(ENTI.CODENTIDA) AS CodigoEntidad
	   ,RTRIM(ENTI.NOMENTIDA) AS NombreEntidad
	   ,CASE HEMBOL.ESTADO
			WHEN 1 THEN '2. Solicitud de Reserva'
			WHEN 2 THEN '1. Solicitud de Transfusión'
			WHEN 12 THEN '3. Solicitud de reserva recibida sin procesar'
		END AS TIPO,
		HEMBOL.ESTADO
	   ,'Autorización' AS Autorizacion
	   ,Null as  EntityId, 
	   CAST('' as bit) as Sel,
	   dbo.[RiskFactorAlert](PAC.IPCODPACI, HEMCO.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
	   dbo.[RiskFactorAlert](PAC.IPCODPACI, HEMCO.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM dbo.HCORHEMBOL AS HEMBOL  
	INNER JOIN dbo.INPROFSAL AS PROFSALRES ON HEMBOL.PROFSOLRES = PROFSALRES.CODPROSAL   
	LEFT OUTER JOIN dbo.INPROFSAL AS PROFSOLTRA  ON HEMBOL.PROFSOLTRA = PROFSOLTRA.CODPROSAL   
	INNER JOIN dbo.HCORHEMCO AS HEMCO ON HEMBOL.HCORHEMCOID = HEMCO.ID   
	LEFT OUTER JOIN dbo.INDIAGNOS AS DIAGNO ON HEMCO.CODDIAGNO = DIAGNO.CODDIAGNO   
	INNER JOIN dbo.HCCOMSAN AS COMSAM ON HEMBOL.COMSAMID = COMSAM.ID   
	INNER JOIN dbo.INPACIENT AS PAC ON PAC.IPCODPACI = HEMCO.IPCODPACI   
	INNER JOIN dbo.ADINGRESO AS ING ON ING.NUMINGRES = HEMCO.NUMINGRES   
	LEFT JOIN dbo.INUNIFUNC as UF ON UF.UFUCODIGO = ING.UFUACTPAC  
	LEFT JOIN dbo.CHCAMASHO as CAMA ON CAMA.CODICAMAS = ING.CODCAMACT   
	LEFT JOIN dbo.INDIAGNOS as DIAG ON DIAG.CODDIAGNO = HEMCO.CODDIAGNO   
	LEFT JOIN dbo.INENTIDAD as ENTI ON ENTI.CODENTIDA = ING.CODENTIDA
	--LEFT OUTER JOIN dbo.HCORHEMSER HD ON HEMCO.ID = HD.HCORHEMCOID  AND  HD.HCORHEMBOLID = HEMBOL.ID
 	WHERE HEMBOL.ESTADO IN(SELECT Value FROM dbo.splitstring(@Estados))  and (HEMBOL.BOLRECIBIDA IS NULL OR HEMBOL.BOLRECIBIDA = 0) and HEMCO.SOLEXTRAMU = @Extramural AND HEMCO.CODCENATE = @CentroAtencion and HEMCO.INTERFAZ = @Interfaz
	GROUP BY HEMCO.ID,HEMBOL.ESTADO,COMSAM.CODCOMSAM,COMSAM.DESCOMSAM , PAC.IPNOMCOMP,HEMCO.FECORDMED, PAC.IPCODPACI, PAC.IPGRUPSAN,PAC.IPRHSANGR,UF.UFUDESCRI,HEMCO.PRIOTRAN,HEMCO.NUMINGRES ,HEMCO.NUMEFOLIO,ING.UFUAACTMED
	,COMSAMID,UFUACTPAC, PENCONRES,CAMA.DESCCAMAS,PAC.IPTIPODOC ,convert(date,PAC.IPFECNACI ) ,PAC.IPSEXOPAC,HEMCO.CODDIAGNO,DIAG.NOMDIAGNO,HEMCO.OBSERVACI,UF.UFUCODIGO,COMSAM.CODCOMSAM,PROFSALRES.CODPROSAL
	,PROFSALRES.NOMMEDICO,  ENTI.CODENTIDA,ENTI.NOMENTIDA--, HD.ID  
	ORDER BY PRIOTRAN ASC
 END
 ELSE
 begin
	--ambulatorio
	SELECT HEMCO.ID,HEMBOL.ESTADO,COMSAM.CODCOMSAM,COMSAM.DESCOMSAM , RTRIM(PAC.IPNOMCOMP) AS NOMPACIENTE,RTRIM(HEMCO.FECORDMED) AS ORDEN,count(*) as Cantidad,RTRIM(PAC.IPCODPACI) as CODPACIENTE,  PAC.IPGRUPSAN+''+PAC.IPRHSANGR RH, 
	UFUNCIONAL = CASE WHEN  RTRIM(UF.UFUDESCRI) IS NOT NULL THEN RTRIM(UF.UFUDESCRI) ELSE ING.UFUACTPAC END, PRIORIDAD = CASE HEMCO.PRIOTRAN 	 WHEN 1 THEN 'Emergencia (hasta 15 min)' 	 WHEN 2 THEN 'Urgencia (hasta 1 hora)' WHEN 3 THEN 'Urgencia Diferida (hasta 3 horas)' 	 WHEN 4 THEN 'Normal (hasta 24 horas)' END,  PENDIENTECONFRESERVA = ISNULL((Select top 1 PENCONRES from HCORHEMBOL WHERE HCORHEMCOID = HEMCO.ID AND PENCONRES = 1),0)   ,
	HEMCO.NUMINGRES ,RTRIM(HEMCO.NUMEFOLIO) AS FOLIO,COMSAMID, HEMBOL.PENCONRES AS PENDIENTECONFRESERVA2,CAMA.DESCCAMAS  ,
	PAC.IPTIPODOC As 'TipoDocumento',dbo.[TipoDocumentoNombreCompleto](PAC.IPTIPODOC) AS 'NameTypeDocument',convert(date,PAC.IPFECNACI ) as 'FechaNacimiento',
	CASE PAC.IPSEXOPAC WHEN 1 THEN 'M' ELSE 'F' END AS 'Sexo',HEMCO.CODDIAGNO as 'CodigoDiagnostico', DIAG.NOMDIAGNO AS 'DescripcionDiagnostico',HEMCO.OBSERVACI AS 'Observaciones',UF.UFUCODIGO AS 'CodigoServicio',COMSAM.CODCOMSAM AS 'Codigo Sanguineo',
	rtrim(PROFSALRES.CODPROSAL) as CodigoMedico,rtrim(PROFSALRES.NOMMEDICO) DescricionMedico,rtrim(PROFSALRES.CODPROSAL) + ' - ' + rtrim(PROFSALRES.NOMMEDICO) as CodigoDrescripcionMedico  ,
	rtrim(ENTI.CODENTIDA) as CodigoEntidad,rtrim(ENTI.NOMENTIDA) as NombreEntidad,
	case HEMBOL.ESTADO when 1 then '2. Solicitud de Reserva' when 2 then '1. Solicitud de Transfusión' when 12 then '3. Solicitud de reserva recibida sin procesar' end as TIPO 
	, '' AS Autorizacion, RTRIM(CITA.IPFECHCIT) AS CITA
	,NULL AS EntityId,
	CAST('' as bit) as Sel,
	dbo.[RiskFactorAlert](PAC.IPCODPACI, HEMCO.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
	dbo.[RiskFactorAlert](PAC.IPCODPACI, HEMCO.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM dbo.HCORHEMBOL AS HEMBOL  
	INNER JOIN dbo.INPROFSAL AS PROFSALRES ON HEMBOL.PROFSOLRES = PROFSALRES.CODPROSAL   
	LEFT OUTER JOIN dbo.INPROFSAL AS PROFSOLTRA  ON HEMBOL.PROFSOLTRA = PROFSOLTRA.CODPROSAL   
	INNER JOIN dbo.HCORHEMCO AS HEMCO ON HEMBOL.HCORHEMCOID = HEMCO.ID   
	LEFT OUTER JOIN dbo.INDIAGNOS AS DIAGNO ON HEMCO.CODDIAGNO = DIAGNO.CODDIAGNO   
	INNER JOIN dbo.HCCOMSAN AS COMSAM ON HEMBOL.COMSAMID = COMSAM.ID   
	INNER JOIN dbo.INPACIENT AS PAC ON PAC.IPCODPACI = HEMCO.IPCODPACI   
	INNER JOIN dbo.ADINGRESO AS ING ON ING.NUMINGRES = HEMCO.NUMINGRES   
	LEFT JOIN dbo.INUNIFUNC as UF ON UF.UFUCODIGO = ING.UFUACTPAC  
	LEFT JOIN dbo.CHCAMASHO as CAMA ON CAMA.CODICAMAS = ING.CODCAMACT   
	LEFT JOIN dbo.INDIAGNOS as DIAG ON DIAG.CODDIAGNO = HEMCO.CODDIAGNO   
	LEFT JOIN dbo.INENTIDAD as ENTI ON ENTI.CODENTIDA = ING.CODENTIDA  
	LEFT JOIN dbo.ADCONCOEX as CITA ON CAST(CITA.NUMCONCIT AS INT) = HEMCO.idAGASICITA
	WHERE HEMBOL.ESTADO IN(SELECT Value FROM dbo.splitstring(@Estados))  and (HEMBOL.BOLRECIBIDA IS NULL OR HEMBOL.BOLRECIBIDA = 0) and HEMCO.SOLEXTRAMU = @Extramural AND HEMCO.CODCENATE = @CentroAtencion and HEMCO.INTERFAZ = @Interfaz
	GROUP BY HEMCO.ID,HEMBOL.ESTADO,COMSAM.CODCOMSAM,COMSAM.DESCOMSAM , PAC.IPNOMCOMP,HEMCO.FECORDMED, PAC.IPCODPACI, PAC.IPGRUPSAN,PAC.IPRHSANGR,UF.UFUDESCRI,HEMCO.PRIOTRAN,HEMCO.NUMINGRES ,NUMEFOLIO,ING.UFUAACTMED,COMSAMID,UFUACTPAC, PENCONRES,CAMA.DESCCAMAS,PAC.IPTIPODOC ,convert(date,PAC.IPFECNACI ) ,PAC.IPSEXOPAC,HEMCO.CODDIAGNO,DIAG.NOMDIAGNO,HEMCO.OBSERVACI,UF.UFUCODIGO,COMSAM.CODCOMSAM,PROFSALRES.CODPROSAL,PROFSALRES.NOMMEDICO,  ENTI.CODENTIDA,ENTI.NOMENTIDA, CITA.IPFECHCIT 
	ORDER BY PRIOTRAN ASC
 end
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de hemocomponentes (bolsas de sangre, glóbulos rojos, plaquetas y otros hemoderivados) pendientes de reserva o transfusión para un centro de atención específico. Combina datos del paciente (cédula, nombre, grupo sanguíneo, RH, fecha de nacimiento, sexo), la orden de hemoterapia (diagnóstico CIE-10, prioridad de transfusión —emergencia, urgencia, urgencia diferida o normal—, folio, observaciones), las bolsas solicitadas (tipo de componente sanguíneo, estado, confirmación de reserva), el ingreso hospitalario (número de ingreso, unidad funcional, cama), el médico solicitante y la entidad aseguradora. Opera en dos modalidades según el parámetro @Extramural: modo hospitalario (pacientes ingresados con cama asignada) y modo ambulatorio/extramural (pacientes con cita programada sin ingreso formal), filtrando además por estados de bolsa y por si la solicitud proviene de interfaz externa. Es el procedimiento central que alimenta el módulo de banco de sangre y hemoterapia para que el personal de enfermería y el banco de sangre gestionen y prioricen las transfusiones pendientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de hemocomponentes (reserva/transfusión) por centro de atención, estados y modalidad (hospitalaria o ambulatoria/extramural), enriqueciendo con datos del paciente, médico, diagnóstico, ubicación y alertas de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de estados debe ser una cadena delimitada interpretable por dbo.splitstring.; Debe existir el centro de atención indicado y registros de bolsas/órdenes asociadas.; Las funciones escalares dbo.TipoDocumentoNombreCompleto, dbo.RiskFactorAlert y dbo.splitstring deben existir y ser ejecutables.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna bolsas marcadas como recibidas (BOLRECIBIDA = 1).; El filtro por SOLEXTRAMU garantiza separación estricta entre flujo hospitalario y ambulatorio.; El resultado siempre va ordenado ascendente por prioridad de transfusión (PRIOTRAN).; Las alertas de factores de riesgo y escalas se calculan siempre por paciente e ingreso vía dbo.RiskFactorAlert.; En el flujo hospitalario la columna Autorizacion se entrega como literal ''Autorización'' y en el ambulatorio como cadena vacía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de hemocomponentes; Reserva de sangre; Solicitud de transfusión; Hemocomponente; Grupo sanguíneo y RH; Prioridad de transfusión (emergencia/urgencia/normal); Pendiente de confirmación de reserva; Diagnóstico (CIE); Unidad funcional; Cama hospitalaria; Entidad/aseguradora; Cita ambulatoria; Alerta de factores de riesgo; Alerta de escalas; Atención extramural vs hospitalaria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @Extramural=0 retorna el listado hospitalario incluyendo cama, autorización y alertas; cuando @Extramural=1 retorna listado ambulatorio incluyendo fecha de cita (ADCONCOEX).; [RETURN_RESULT] resultset: Solo se incluyen filas donde HEMBOL.ESTADO esté en la lista de @Estados, BOLRECIBIDA sea NULL o 0, HEMCO.SOLEXTRAMU=@Extramural, HEMCO.CODCENATE=@CentroAtencion y HEMCO.INTERFAZ=@Interfaz.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Extramural = 0 → Ejecuta consulta hospitalaria: incluye columna ''Autorización'' fija y NO une con citas externas (ADCONCOEX). else Ejecuta consulta ambulatoria: agrega LEFT JOIN con ADCONCOEX para traer fecha de cita (IPFECHCIT) y deja Autorizacion vacío.; si HEMBOL.ESTADO = 1 / 2 / 12 → Clasifica TIPO como ''2. Solicitud de Reserva'', ''1. Solicitud de Transfusión'' o ''3. Solicitud de reserva recibida sin procesar'' respectivamente.; si HEMCO.PRIOTRAN = 1/2/3/4 → Asigna PRIORIDAD: Emergencia (15 min), Urgencia (1 h), Urgencia Diferida (3 h) o Normal (24 h).; si UF.UFUDESCRI IS NOT NULL → Usa la descripción de la unidad funcional como UFUNCIONAL. else Usa ING.UFUACTPAC como UFUNCIONAL.; si PAC.IPSEXOPAC = 1 → Reporta sexo ''M''. else Reporta sexo ''F''.; si Existe en HCORHEMBOL alguna fila con HCORHEMCOID = HEMCO.ID y PENCONRES = 1 → PENDIENTECONFRESERVA = 1. else PENDIENTECONFRESERVA = 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.TipoDocumentoNombreCompleto; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORHEMBOL; dbo.INPROFSAL; dbo.HCORHEMCO; dbo.INDIAGNOS; dbo.HCCOMSAN; dbo.INPACIENT; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INENTIDAD; dbo.ADCONCOEX', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentes';
-- GO
