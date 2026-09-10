

    /*******************************************************************************************************************
Nombre: [Report].[ViewOpportunitLaboratory]
Tipo:Vista
Observacion: Oportunidad de laboratorio
Profesional: Nilsson Miguel Galindo Lopez
Fecha:03-05-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 20-06-2023
Ovservaciones:En el estado del servicio se agrega el estado 9 para HOMI
--------------------------------------
Version 3
Persona que modifico: Nilsson Miguel Galindo Lopez
Observacion: Se agrega el cte CTE_CANCELACION_ORDEN para tener trazabilidad de la cancelacion desde la orden de laboratorio y semejora la velocidad un 50%
Fecha:29-08-2023
----------------------------------------------------------------------------------------------------------------------------------
Version 4
Persona que modifico:Nilsson Miguel Galindo Lopez 
Observacion: Se agrega el CTE_INTERPRETA para la interpretación del medico
Fecha:26-09-2023
----------------------------------------------------------------------------------------------------------------------------------
Version 5
Persona que modifico:Nilsson Miguel Galindo Lopez 
Observacion: Se cambia la logica de la union entre la tabla dbo.INTERCTRL y DBO.INTERDETA para que no se muestre cuando una muestra
			 se recha, este cambio fue realizado por el ticket 13038 HOMI
Fecha:1-11-2023
--***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewOpportunitLaboratory] AS

WITH

CTE_CANCELACION AS
(
SELECT
CAN.ID,
CAN.CancellationDate,
USU.NOMUSUARI,
CAN.CancellationReasonsObservations
FROM
[Authorization].TraceabilityPaperwork CAN INNER JOIN
dbo.SEGusuaru USU ON CAN.CancellationUserCode=USU.CODUSUARI
WHERE 
CAN.CancellationDate BETWEEN GETDATE()-370 AND GETDATE()
),

--CANCELACIÓN DESDE LA ORDEN DE LABORATORIO
CTE_CANCELACION_ORDEN AS
(
SELECT 
CAN.NUMINGRES,
CAN.NUMEFOLIO,
CAN.CODSERIPS,
USU.NOMUSUARI,
CAN.FECANULAB,
MOT.DESMOTANU
FROM
dbo.HCLABANUL CAN INNER JOIN
dbo.SEGusuaru USU ON CAN.USUANULAB=USU.CODUSUARI LEFT JOIN
dbo.HCMOANULB MOT ON CAN.CODMOTANU=MOT.CODMOTANU
WHERE 
CAN.FECANULAB BETWEEN GETDATE()-370 AND GETDATE()
),

CTE_LABORATORIO AS
(
SELECT 
AUTO,
IPCODPACI,NUMINGRES,NUMEFOLIO,UFUCODIGO,CODSERIPS,CANSERIPS,ESTSERIPS,FECORDMED,FECRECMUE,CODPROSAL,USURECMUE,FECHARESULT,
TraceabilityPaperworkId,IDDESCRIPCIONRELACIONADA,PROFRESULT,NUMFOLINT
FROM
dbo.HCORDLABO
WHERE FECORDMED BETWEEN GETDATE()-370 AND GETDATE()
UNION ALL
SELECT 
AUTO,
IPCODPACI,'' AS NUMINGRES,NUMFOLINT,UFUCODIGO,CODSERIPS,CANSERIPS,ESTSERIPS,FECORDMED,FECRECMUE,CODPROSAL,USURECMUE,FECHARESULT,
'' AS TraceabilityPaperworkId,IDDESCRIPCIONRELACIONADA,PROFRESULT,NUMFOLINT
FROM
dbo.AMBORDLAB
WHERE FECORDMED BETWEEN GETDATE()-370 AND GETDATE()
),
--IN V4 interpretación del medico 
CTE_INTERPRETA AS 
(
SELECT 
NUMINGRES,NUMEFOLIO,FECHISPAC
FROM
dbo.HCHISPACA
WHERE
FECHISPAC BETWEEN GETDATE()-370 AND GETDATE()
)
--FN V4

SELECT 
--CAST(ORD.[AUTO] as varchar) + '- HCORDLABO' ID,
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
CASE PAC.IPTIPODOC WHEN 1 THEN 'CC' 
				   WHEN 2 THEN 'CE' 
				   WHEN 3 THEN 'TI' 
				   WHEN 4 THEN 'RC' 
				   WHEN 5 THEN 'PA' 
				   WHEN 6 THEN 'AS'
                   WHEN 7 THEN 'MS' 
				   WHEN 8 THEN 'NU' 
				   WHEN 9 THEN 'CN' 
				   WHEN 10 THEN 'CD' 
				   WHEN 11 THEN 'SC'
                   WHEN 12 THEN 'PE' 
				   WHEN 13 THEN 'PT' 
				   WHEN 14 THEN 'DE' 
				   WHEN 15 THEN 'SI' 
				   END [TIPO DOCUMENTO]
	,ORD.IPCODPACI [# IDENTIFICACIÓN]
	,PAC.IPNOMCOMP [NOMBRE DEL PACIENTE]
	,CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS[GENERO DEL PACIENTE]
	,CG.CODE COD_GRUPO_A
	,CG.NAME [GRUPO DE ATENCION]
	,HA.CODE COD_ENTIDAD
	,HA.NAME ENTIDAD
	,ORD.NUMINGRES [INGRESO]
	,ORD.NUMEFOLIO [FOLIO]
	,UNF.UFUDESCRI as [UNIDAD FUNCIONAL SOLICITUD]
	,CASE WHEN SUBSTRING(ORD.UFUCODIGO,1,4)='1111' THEN 'URGENCIAS'
			WHEN SUBSTRING(ORD.UFUCODIGO,1,4)='1112' THEN 'CONSULTA EXTERNA'
			WHEN SUBSTRING(ORD.UFUCODIGO,1,4)='1113' THEN 'HOSPITALIZACION'
			WHEN SUBSTRING(ORD.UFUCODIGO,1,4)='1114' THEN 'QUIROFANOS'
			WHEN SUBSTRING(ORD.UFUCODIGO,1,4)='1115' THEN 'APOYO DIAGNOSTICO'
			WHEN SUBSTRING(ORD.UFUCODIGO,1,4)='1116' THEN 'APOYO TERAPEUTICO' END AS AMBITO_SOLICITUD
	,PRO.NOMMEDICO [MEDICO]
	,RTRIM(ESP.DESESPECI) AS ESPECIALIDAD
	,ORD.CODSERIPS as [CUPS]
	,cups.Description as [DESCRIPCIÓN CUPS]
	,GCUP.Description AS [GRUPO DE CUPS]
	,CON.Name AS [CONCEPTO DE FACTURACION]
	,DCUP.[Name] AS [SUBGRUPO CUPS]
	,CD.Code+' - '+CD.Name AS [CUPS RELACIONADO]
	,UNF.UFUDESCRI AS 'CENTRO DE COSTO'
	,[INT].CODCONCEC AS [NUMERO DE ORDEN]
	,ORD.CANSERIPS as [CANTIDAD]
	,IIF(ORD.ESTSERIPS=6 AND CAN.CancellationDate IS NULL,'Anulado',CASE ORD.ESTSERIPS WHEN 1 THEN 'Solicitado'
																					   WHEN 2 THEN 'Muestra Recolectada'
																					   WHEN 3 THEN 'Resultado Entregado'
																					   WHEN 4 THEN 'Interpretado'
																					   WHEN 5 THEN 'Remitido'
																					   WHEN 6 THEN 'Anulado'
																					   WHEN 7 THEN 'Extramural'
																					   WHEN 8 THEN 'Muetra recolectada parcialmente' 
																					   WHEN 9 THEN 'Muestra no recolectada'ELSE ORD.ESTSERIPS END)  as [ESTADO]
	,ORD.FECORDMED as [FECHA SOLICITUD]
	,ORD.FECRECMUE as [FECHA MUESTRA]
	,IIF(ORD.FECORDMED>ORD.FECRECMUE,0,DATEDIFF(MINUTE,ORD.FECORDMED,ORD.FECRECMUE)) AS[MINUTOS ENTRE S.Y.M.]
	,PROF.NOMMEDICO as [USUARIO MUESTRA]
	,ISNULL(ORD.FECHARESULT,RES.FECREGIST) AS [FECHA RESULTADO]
	,ISNULL(FE.NOMMEDICO,RES.CODPROSAL) AS [USUARIO DE RESULTADO]
	--IN V4,RES.FECREGIST AS [FECHA INTERPRETACION]
	--,IIF(ORD.FECRECMUE>RES.FECREGIST,0,DATEDIFF(MINUTE,ORD.FECRECMUE,RES.FECREGIST)) AS[MINUTOS ENTRE M.Y.R]
	--,IIF(ORD.FECORDMED>RES.FECREGIST,0,DATEDIFF(MINUTE,ORD.FECORDMED,RES.FECREGIST)) AS[MINUTOS ENTRE S.Y.R]
	,INTE.FECHISPAC AS [FECHA INTERPRETACION]
	,ISNULL(DATEDIFF(MINUTE,ORD.FECRECMUE,ORD.FECHARESULT),DATEDIFF(MINUTE,ORD.FECRECMUE,RES.FECREGIST)) AS[MINUTOS ENTRE M.Y.R]--MUESTRA Y RESULTADO
	,ISNULL(DATEDIFF(MINUTE,ORD.FECORDMED,ORD.FECHARESULT),DATEDIFF(MINUTE,ORD.FECORDMED,RES.FECREGIST)) AS[MINUTOS ENTRE S.Y.R]--SOLICITADO A RESULTADO FN V4
	,FUN2.UFUDESCRI 'UNIDAD ACTUAL'
	,IIF(ORD.ESTSERIPS=6,NULL,ISNULL(CAST(CANO.FECANULAB AS DATE),CAST(CAN.CancellationDate AS DATE))) AS [FECHA ANULACION]
	,IIF(ORD.ESTSERIPS=6,NULL,ISNULL(CAST(CANO.FECANULAB AS TIME),CAST(CAN.CancellationDate as time))) AS [HORA ANULACION]
	,IIF(ORD.ESTSERIPS=6,NULL,ISNULL(CANO.NOMUSUARI,CAN.NOMUSUARI)) AS [USUARIO ANULACION]
	,IIF(ORD.ESTSERIPS=6,NULL,ISNULL(CANO.DESMOTANU,CAN.CancellationReasonsObservations)) AS [OBSERVACION DE LA CANCELACIÓN]
	,CAST(ORD.FECORDMED AS date) AS 'FECHA BUSQUEDA'
	,YEAR(ORD.FECORDMED) AS 'AÑO FECHA BUSQUEDA'
	,MONTH(ORD.FECORDMED) AS 'MES AÑO FECHA BUSQUEDA'
	, CASE MONTH(ORD.FECORDMED) WHEN 1 THEN 'ENERO'
								WHEN 2 THEN 'FEBRERO'
								WHEN 3 THEN 'MARZO'
								WHEN 4 THEN 'ABRIL'
								WHEN 5 THEN 'MAYO'
								WHEN 6 THEN 'JUNIO'
								WHEN 7 THEN 'JULIO'
								WHEN 8 THEN 'AGOSTO'
								WHEN 9 THEN 'SEPTIEMBRE'
								WHEN 10 THEN 'OCTUBRE'
								WHEN 11 THEN 'NOVIEMBRE'
								WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA'
	, FORMAT(DAY(ORD.FECORDMED), '00') AS 'DIA FECHA BUSQUEDA'
	,CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
 FROM CTE_LABORATORIO ORD
LEFT JOIN CTE_INTERPRETA INTE ON ORD.NUMINGRES=INTE.NUMINGRES AND ORD.NUMFOLINT=INTE.NUMEFOLIO
LEFT JOIN dbo.INPACIENT PAC ON PAC.IPCODPACI=ORD.IPCODPACI
LEFT JOIN dbo.INUNIFUNC UNF ON ORD.UFUCODIGO=UNF.UFUCODIGO 
LEFT JOIN dbo.INPROFSAL PRO ON PRO.CODPROSAL=ORD.CODPROSAL
LEFT JOIN dbo.INPROFSAL FE ON ORD.PROFRESULT=FE.CODPROSAL
LEFT JOIN INESPECIA AS ESP  ON PRO.CODESPEC1 = ESP.CODESPECI
LEFT JOIN Contract.CUPSEntity CUPS ON CUPS.Code=ORD.CODSERIPS
LEFT JOIN Contract.CupsSubgroup DCUP ON CUPS.CUPSSubGroupId=DCUP.ID
LEFT JOIN Contract.CupsGroup GCUP ON GCUP.ID=DCUP.CUPSGroupId 
LEFT JOIN Billing.BillingConcept CON ON CON.Id=CUPS.BillingConceptId 
LEFT JOIN dbo.INPROFSAL PROF on ORD.USURECMUE=prof.CODUSUARI 
LEFT JOIN dbo.INTERCTRL RES ON ORD.AUTO=RES.AUTOLABOR AND RES.AUTO=(SELECT MAX(RE.AUTO) FROM INTERCTRL RE WHERE ORD.AUTO=RE.AUTOLABOR)--RES.ESTADOINT='1'
LEFT JOIN DBO.INTERDETA [INT] ON RES.ORDEN_INDIGO=[INT].CODCONCEC AND ORD.CODSERIPS=INT.CODSERIPS  --ORD.[AUTO]=[INT].AUTOLABOR v5
LEFT JOIN DBO.ADINGRESO ING ON ING.NUMINGRES=ORD.NUMINGRES AND ING.IPCODPACI=ORD.IPCODPACI
LEFT JOIN INUNIFUNC FUN2 ON FUN2.UFUCODIGO=ING.UFUAACTMED
LEFT JOIN Contract.CareGroup CG ON CG.Id=PAC.GENCAREGROUP
LEFT JOIN Contract.HealthAdministrator HA ON HA.Id=PAC.GENCONENTITY
LEFT JOIN CTE_CANCELACION CAN ON ORD.TraceabilityPaperworkId=CAN.Id
LEFT JOIN Contract.ContractDescriptions CD ON  ORD.IDDESCRIPCIONRELACIONADA=CD.Id 
LEFT JOIN CTE_CANCELACION_ORDEN CANO ON ORD.NUMINGRES=CANO.NUMINGRES AND ORD.NUMEFOLIO=CANO.NUMEFOLIO AND ORD.CODSERIPS=CANO.CODSERIPS
--where ord.ipcodpaci='1013158945' AND ORD.CODSERIPS='901107'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting para el indicador de **oportunidad de laboratorio clínico**, que consolida órdenes hospitalarias y ambulatorias de los últimos 370 días junto con datos del paciente, entidad pagadora, CUPS, profesionales y estados del servicio. Calcula tiempos en minutos entre solicitud, recolección de muestra y entrega de resultado, e incorpora trazabilidad de cancelaciones/anulaciones (desde autorización y desde la orden). Está orientada a consumo en herramientas de BI para medir tiempos de respuesta del laboratorio por ámbito, unidad funcional y período.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunitLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunitLaboratory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye un dataset de oportunidad del servicio de laboratorio clínico, consolidando órdenes hospitalarias y ambulatorias con sus tiempos entre solicitud, toma de muestra, resultado e interpretación, y la trazabilidad de anulaciones.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunitLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas dbo.HCORDLABO, dbo.AMBORDLAB, dbo.HCHISPACA, dbo.HCLABANUL y Authorization.TraceabilityPaperwork deben tener fechas dentro de los últimos 370 días para aparecer; Los códigos de UFUCODIGO deben seguir la convención de prefijos 1111-1116 para que se clasifique el ámbito de solicitud; La base de datos debe soportar la zona horaria ''Pakistan Standard Time'' (AT TIME ZONE); Los catálogos relacionados (INPACIENT, INPROFSAL, INUNIFUNC, INESPECIA, CUPSEntity, CupsSubgroup, CupsGroup, BillingConcept, CareGroup, HealthAdministrator, ContractDescriptions) deben existir para poder describir códigos; los LEFT JOIN permiten ausencias', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunitLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan órdenes con FECORDMED dentro de los últimos 370 días (ventana móvil sobre GETDATE()); Las cancelaciones consideradas (tanto desde autorización como desde la orden) están limitadas a los últimos 370 días; Los tiempos en minutos entre fechas nunca son negativos: si la fecha posterior es menor que la anterior, se devuelve 0; Para cada orden de laboratorio (HCORDLABO/AMBORDLAB) se toma solo el último registro de INTERCTRL (MAX(AUTO)) como interpretación vigente; El INT (INTERDETA) se vincula por ORDEN_INDIGO=CODCONCEC y CODSERIPS, no por AUTOLABOR (cambio v5 para no mostrar muestras rechazadas); Si ESTSERIPS=6 los datos de anulación se ocultan (NULL) en la salida, salvo el estado etiquetado ''Anulado''; El identificador de la compañía proviene del nombre de la base de datos actual (DB_NAME); La hora de actualización se reporta convertida a la zona ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunitLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; orden de laboratorio; examen de laboratorio; muestra; interpretación médica; anulación de orden; autorización; trazabilidad de trámite; CUPS; grupo de atención; administradora de salud (EPS); unidad funcional; ámbito de atención (urgencias, consulta externa, hospitalización, quirófanos, apoyo diagnóstico/terapéutico); especialidad médica; concepto de facturación; tiempos de oportunidad (solicitud-muestra-resultado)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunitLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = 6 AND CancellationDate IS NULL (en TraceabilityPaperwork) → El estado se reporta como ''Anulado'' aunque no exista registro de cancelación en autorización else Se mapea ESTSERIPS a etiqueta: 1=Solicitado, 2=Muestra Recolectada, 3=Resultado Entregado, 4=Interpretado, 5=Remitido, 6=Anulado, 7=Extramural, 8=Muestra recolectada parcialmente, 9=Muestra no recolectada; si SUBSTRING(UFUCODIGO,1,4) coincide con prefijos 1111-1116 → Se clasifica el ámbito de solicitud como URGENCIAS, CONSULTA EXTERNA, HOSPITALIZACION, QUIROFANOS, APOYO DIAGNOSTICO o APOYO TERAPEUTICO según prefijo; si ORD.ESTSERIPS = 6 (Anulado) → Se anulan (NULL) los campos FECHA/HORA/USUARIO/OBSERVACION de anulación en la salida else Se toman los datos de anulación desde HCLABANUL (CTE_CANCELACION_ORDEN) y, si no existe, desde Authorization.TraceabilityPaperwork (CTE_CANCELACION) vía COALESCE; si ORD.FECORDMED > ORD.FECRECMUE → Los minutos entre solicitud y muestra se reportan como 0 (evita valores negativos) else Se calcula DATEDIFF(MINUTE, FECORDMED, FECRECMUE); si ORD.FECHARESULT IS NOT NULL → Se usa FECHARESULT y NOMMEDICO del profesional de resultado para fecha y usuario de resultado y para los DATEDIFF M.Y.R y S.Y.R else Se cae a INTERCTRL.FECREGIST y INTERCTRL.CODPROSAL; si Origen del registro de laboratorio → Se unen ambulatorio (AMBORDLAB) y hospitalario (HCORDLABO) vía UNION ALL; en AMBORDLAB se asigna NUMINGRES='''' y TraceabilityPaperworkId=''''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunitLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.TraceabilityPaperwork; dbo.SEGusuaru; dbo.HCLABANUL; dbo.HCMOANULB; dbo.HCORDLABO; dbo.AMBORDLAB; dbo.HCHISPACA; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; Contract.CUPSEntity; Contract.CupsSubgroup; Contract.CupsGroup; Billing.BillingConcept; dbo.INTERCTRL; dbo.INTERDETA; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunitLaboratory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunitLaboratory';
GO
