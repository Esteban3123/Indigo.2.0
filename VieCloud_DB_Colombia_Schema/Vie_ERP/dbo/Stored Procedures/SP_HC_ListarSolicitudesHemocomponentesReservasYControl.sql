CREATE PROCEDURE [dbo].[SP_HC_ListarSolicitudesHemocomponentesReservasYControl]
@CentroAtencion varchar(10),
@Estados  varchar(100),
@Extramural bit
WITH RECOMPILE
AS
BEGIN
	SELECT DISTINCT HEMCO.ID, RTRIM(HEMCO.FECORDMED) AS ORDEN, MEDICO = CASE WHEN PROFSOLTRA.NOMMEDICO IS NOT NULL THEN RTRIM(PROFSOLTRA.NOMMEDICO) 	ELSE RTRIM(PROFSALRES.NOMMEDICO) END, HEMCO.PRIOTRAN,
	PRIORIDAD = CASE HEMCO.PRIOTRAN 	WHEN 1 THEN 'Emergencia (hasta 15 min)' 	WHEN 2 THEN 'Urgencia (hasta 1 hora)' WHEN 3 THEN 'Urgencia Diferida (hasta 3 horas)' 	WHEN 4 THEN 'Normal (hasta 24 horas)' END,
	RTRIM(DIAGNO.NOMDIAGNO)	AS DIAGNOSTICO, RTRIM(HEMCO.NUMEFOLIO) AS FOLIO,RTRIM(PAC.IPCODPACI) AS CODPACIENTE,RTRIM(PAC.IPNOMCOMP) AS NOMPACIENTE,
	UFUNCIONAL =  CASE 	WHEN UFUSOLTRA.UFUDESCRI IS NOT NULL THEN UFUSOLTRA.UFUDESCRI 	ELSE UFUSOLRES.UFUDESCRI END, PENDIENTECONFRESERVA = ISNULL((Select top 1 PENCONRES from HCORHEMBOL WHERE HCORHEMCOID = HEMCO.ID AND PENCONRES = 1),0)  ,
	HEMCO.NUMINGRES, UFUCODIGO =  CASE 	WHEN UFUSOLTRA.UFUCODIGO IS NOT NULL THEN UFUSOLTRA.UFUCODIGO 	ELSE UFUSOLRES.UFUCODIGO END  , REARASANT, HEMCO.HB, HEMCO.HTO, HEMCO.RCTOPLAQ, HEMCO.FIBRINOG,
	HEMCO.OTROSLAB, HEMCO.ITRESTAU, HEMCO.ITVOLCIR, HEMCO.ITRESCIR, HEMCO.ITCAPTRA, HEMCO.ITEXATRA, HEMCO.ITHEMOST  , HEMCO.TRANPREV, HEMCO.RTHEMOLI, HEMCO.RTFEBRIL, HEMCO.RTURTICA, HEMCO.OTRASRET, HEMCO.AOEMBPRE, HEMCO.AOEMBAC, HEMCO.AOERIFE,
	HEMCO.AOERIFE, HEMCO.OBSERVACI, HEMCO.COOMBSDTO, HEMCO.RAI,HEMCO.AUTOCTRL,CAMA.DESCCAMAS, HEMBOL.ENTRANSFU,
	case HEMBOL.ESTADO when 3 then 'Reserva sin solicitud de transfusión' when 4 then 'Reserva con solicitud de transfusión' when 10 then 'Descartado por Médico' when 6 then 'Registro no Realizado' when 7 then 'Registro Realizado' when 8 then 'Registro descartado' end as TIPO, 
	HEMBOL.ESTADO,
	COMSAM.DESCOMSAM As 'Componente', xd.Cantidad, RTRIM(CITA.IPFECHCIT) AS CITA,
	dbo.[RiskFactorAlert](PAC.IPCODPACI, HEMCO.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
	dbo.[RiskFactorAlert](PAC.IPCODPACI, HEMCO.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM dbo.HCORHEMBOL AS HEMBOL INNER JOIN 
	dbo.INPROFSAL AS PROFSALRES ON HEMBOL.PROFSOLRES = PROFSALRES.CODPROSAL LEFT OUTER JOIN 
	dbo.INPROFSAL AS PROFSOLTRA  ON HEMBOL.PROFSOLTRA = PROFSOLTRA.CODPROSAL INNER JOIN 
	dbo.HCORHEMCO AS HEMCO ON HEMBOL.HCORHEMCOID = HEMCO.ID LEFT OUTER JOIN 
	dbo.INDIAGNOS AS DIAGNO ON HEMCO.CODDIAGNO = DIAGNO.CODDIAGNO INNER JOIN 
	dbo.HCCOMSAN AS COMSAM ON HEMBOL.COMSAMID = COMSAM.ID INNER JOIN 
	dbo.INPACIENT AS PAC ON PAC.IPCODPACI = HEMCO.IPCODPACI INNER JOIN 
	dbo.INUNIFUNC AS UFUSOLRES ON HEMBOL.UFUSOLRES = UFUSOLRES.UFUCODIGO LEFT OUTER JOIN 
	dbo.INUNIFUNC AS UFUSOLTRA  ON HEMBOL.UFUSOLTRA = UFUSOLTRA.UFUCODIGO INNER JOIN 
	dbo.ADINGRESO AS ING ON ING.NUMINGRES = HEMCO.NUMINGRES LEFT OUTER JOIN 
	dbo.CHCAMASHO as CAMA ON CAMA.CODICAMAS = ING.CODCAMACT 
	LEFT JOIN dbo.ADCONCOEX as CITA ON CAST(CITA.NUMCONCIT AS INT) = HEMCO.idAGASICITA
	OUTER APPLY
	(
	SELECT COUNT(X.HCORHEMCOID) as 'Cantidad' FROM HCORHEMBOL x WHERE HCORHEMCOID = HEMBOL.HCORHEMCOID GROUP BY x.HCORHEMCOID
	)xd
WHERE HEMBOL.ESTADO IN(SELECT Value FROM dbo.splitstring(@Estados))  and (HEMBOL.BOLRECIBIDA IS NULL OR HEMBOL.BOLRECIBIDA = 0) and HEMCO.SOLEXTRAMU = @Extramural  AND HEMCO.CODCENATE = @CentroAtencion 
ORDER BY PRIOTRAN ASC

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de hemocomponentes (transfusiones de sangre y componentes sanguíneos) en estado de reserva y control para un centro de atención específico, filtrando por estados configurables y si la atención es extramural o no. Integra datos del paciente (cédula, nombre), el ingreso hospitalario, la cama asignada, el diagnóstico CIE-10, el médico solicitante o tratante, la unidad funcional de origen, el componente sanguíneo solicitado, la prioridad de transfusión (emergencia, urgencia, normal), los valores de laboratorio previos a la transfusión (hemoglobina, hematocrito, plaquetas, fibrinógeno), antecedentes transfusionales, indicaciones clínicas, la cita de consulta externa asociada, y alertas de factores de riesgo y escalas clínicas del paciente. Se usa en el módulo de hemoterapia para que el banco de sangre y el personal clínico hagan seguimiento y control de las bolsas pendientes de entrega o transfusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de hemocomponentes con sus reservas y control de transfusión, filtrando por centro de atención, estados de bolsa y origen extramural, incluyendo datos clínicos, médico solicitante, paciente, cama, cita y alertas de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de estados debe ser una cadena parseable por dbo.splitstring (lista separada por delimitador).; Debe existir la función escalar dbo.RiskFactorAlert para evaluar alertas de factores de riesgo y escalas.; El centro de atención y la bandera extramural deben corresponder a valores existentes en HCORHEMCO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye bolsas ya recibidas en banco (BOLRECIBIDA = 1).; Siempre filtra por un único centro de atención y por la condición extramural exacta.; La prioridad de transfusión está acotada a cuatro niveles (1 a 4) con tiempos máximos definidos: 15 min, 1 h, 3 h y 24 h.; El médico mostrado prioriza al solicitante de transfusión sobre el de reserva (igual criterio para la unidad funcional).; El listado es DISTINCT, evitando duplicar la misma bolsa-orden.; La cantidad expuesta corresponde al total de bolsas asociadas a la misma orden de hemocomponente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de hemocomponentes; Reserva de bolsa de sangre; Transfusión; Prioridad de transfusión (emergencia/urgencia/normal); Hemocomponente; Diagnóstico CIE; Paciente; Ingreso hospitalario; Cama hospitalaria; Unidad funcional; Cita; Médico solicitante; Coombs directo / RAI / Autocontrol; Antecedentes obstétricos y reacciones transfusionales previas; Solicitud extramural; Alerta de factores de riesgo y escalas clínicas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo bolsas cuyo ESTADO esté en la lista @Estados, no recibidas (BOLRECIBIDA IS NULL o 0), del centro indicado y con SOLEXTRAMU igual al parámetro; ordena por PRIOTRAN ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PROFSOLTRA.NOMMEDICO IS NOT NULL → Toma como médico al profesional solicitante de transfusión else Toma como médico al profesional solicitante de reserva; si UFUSOLTRA.UFUDESCRI/UFUCODIGO IS NOT NULL → Reporta la unidad funcional del solicitante de transfusión else Reporta la unidad funcional del solicitante de reserva; si HEMCO.PRIOTRAN = 1/2/3/4 → Etiqueta prioridad como Emergencia (15 min)/Urgencia (1 h)/Urgencia Diferida (3 h)/Normal (24 h) respectivamente; si HEMBOL.ESTADO en {3,4,6,7,8,10} → Mapea a etiquetas: 3=Reserva sin solicitud de transfusión, 4=Reserva con solicitud de transfusión, 10=Descartado por Médico, 6=Registro no Realizado, 7=Registro Realizado, 8=Registro descartado; si Existe alguna bolsa con PENCONRES=1 para la misma orden → Marca PENDIENTECONFRESERVA=1 (pendiente de confirmar reserva) else PENDIENTECONFRESERVA=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORHEMBOL; dbo.INPROFSAL; dbo.HCORHEMCO; dbo.INDIAGNOS; dbo.HCCOMSAN; dbo.INPACIENT; dbo.INUNIFUNC; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADCONCOEX', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesHemocomponentesReservasYControl';
-- GO
