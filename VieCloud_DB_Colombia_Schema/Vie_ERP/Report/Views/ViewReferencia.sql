

    /*******************************************************************************************************************
Nombre: [Report].[ViewReferancia]
Tipo: Vista
Observacion:Oportunidad de la referencia
Profesional: Nilsson Galindo
Fecha:23-10-2023
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico:
Fecha:
Observaciones:
-------------------------------------------------------------------------------------------------------------------------------------
Version 3
Persona que modifico:
Fecha:
Observaciones:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewReferencia] AS

WITH

CTE_SOLICITUDES AS
(
SELECT 
RR.AUTO,RR.NUMINGRES,RR.CODCENATE,RR.UFUCODIGO,RR.CODPROSAL,RR.CODESPECI,RR.IDHCREFCONP,RR.MOTREMISI,RR.SERVIDOREM,
RR.NIVELREMIT,RR.FECSOLICIT
FROM dbo.HCREFCONT RR
WHERE CAST(RR.[AUTO] AS INT)=(SELECT MIN(CAST(A.AUTO AS INT)) FROM dbo.HCREFCONT A WHERE RR.NUMINGRES=A.NUMINGRES)
AND RR.MOTREMISI NOT LIKE '%A%'
)

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CA.NOMCENATE AS [CENTRO DE ATENCION],
DOC.Nombre as [TIPO DOCUMENTO],
PAC.IPCODPACI AS IDENTIFICACION,
PAC.IPNOMCOMP AS [PACIENTE],
CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS[GENERO DEL PACIENTE],
CAST(PAC.IPFECNACI AS DATE) AS [FECHA DE NACIMIENTO],
CASE ING.CODTIPPAC WHEN '1' THEN 'Maternas' 
				   WHEN '2' THEN 'Pediatrico'
				   WHEN '3' THEN 'Población general' 
				   WHEN '4' THEN 'Adulto mayor' 
				   WHEN '5' THEN 'Población general' END AS [TIPO DE PACIENTE], 
DATEDIFF(YEAR,IPFECNACI ,GETDATE()) -(CASE WHEN DATEADD(YY,DATEDIFF(YEAR,IPFECNACI,GETDATE()),IPFECNACI)>GETDATE() THEN 1 ELSE 0 END) AS EDAD,
HEA.Code AS [CODIGO ENTIDAD],
HEA.Name AS [ENTIDAD],
CG.Name AS [GRUPO DE ATENCION],
RR.NUMINGRES AS [INGRESO],
FUN.UFUDESCRI AS [UNIDAD FUNCIONAL SOLICITUD],
PRO.CODPROSAL AS [IDENTIFICACION PROFESIONAL],
PRO.NOMMEDICO AS [PROFESIONAL DE SOLICITUD],
ESP.DESESPECI AS [ESPECIALIDAD PROFESIONAL],
RRP.CODCONCECG AS CONSECUTIVO,
MOTR.Nombre AS [MOTIVO DE REMISION],
DIA.CODDIAGNO+' - '+DIA.NOMDIAGNO AS [DIAGNOSTICO REFERENCIA],
RR.NIVELREMIT AS [NIVEL A QUE SE REMITE],
ESPR.DESESPECI AS [ESPECIALIDAD A REMITIR],
SER.Nombre AS [SERVICIO REMISION],
CASE RRP.ESTADO WHEN 1 THEN 'Solicitado'
				WHEN 2 THEN 'Pendinte o Gestionando'
				WHEN 3 THEN 'Aceptado con pediente de salida'
				WHEN 4 THEN 'Suspendido'
				WHEN 5 THEN 'Ya salio'
				WHEN 6 THEN 'Solicitud con Pertinencia' END AS ESTADO,
CASE RRD.TIPENTIDAD WHEN 1 THEN 'IPS' 
					WHEN 2 THEN 'EAPB'
					WHEN 3 THEN 'CRUE'
					WHEN 4 THEN 'OTRAS' END AS [TIPO ENTIDAD],
ENT.NOMENTIDA AS [ENTIDAD REFERENCIA],
MODR.Nombre AS [MODALIDAD GESTION],
USU.NOMUSUARI AS [USUARIO SEGUIMIENTO],
IPS.DSCRIPIPS AS [INSTITUCION],
RR.FECSOLICIT AS [FECHA SOLICITUD],
RRD.FECSEGUIMIENTO AS [FECHA GUARDADO],
RRD.FECHCREREG AS [FECHA REGISTRO],
RRD.FECHCONFIR AS [FECHA CONFIRMACION],
ING.FECHEGRESO AS [FECHA EGRESO],
RRP.FECHAVISADO AS [FECHA VISADO],
DATEDIFF(HOUR,RR.FECSOLICIT,ING.FECHEGRESO) AS [SOLICITUD VS EGRESO (H)],
MOTS.Nombre AS [MOTIVO SUSPENCION],
RRP.FECHASUSPEN AS [FECHA SUSPENCION],
USUS.NOMUSUARI AS [USUARIO SUSPENCION],
DATEDIFF(HOUR,RR.FECSOLICIT,RRP.FECHASUSPEN) AS [SOLICITUD VS SUPSPENCION (H)],
1 as 'CANTIDAD',
CAST(RR.FECSOLICIT AS date) AS [FECHA BUSQUEDA],
YEAR(RR.FECSOLICIT) AS [AÑO FECHA BUSQUEDA],
MONTH(RR.FECSOLICIT) AS [MES FECHA BUSQUEDA],
CASE MONTH(RR.FECSOLICIT) WHEN 1 THEN 'ENERO'
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
						  WHEN 12 THEN 'DICIEMBRE' END AS [MES NOMBRE FECHA BUSQUEDA],
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM
CTE_SOLICITUDES RR INNER JOIN
dbo.ADCENATEN CA ON RR.CODCENATE = CA.CODCENATE INNER JOIN
dbo.INUNIFUNC FUN ON RR.UFUCODIGO=FUN.UFUCODIGO INNER JOIN
dbo.ADINGRESO ING ON RR.NUMINGRES=ING.NUMINGRES INNER JOIN
dbo.INPACIENT PAC ON ING.IPCODPACI=PAC.IPCODPACI INNER JOIN
dbo.ADTIPOIDENTIFICA DOC ON PAC.IPTIPODOC=DOC.CODIGO LEFT JOIN
CONTRACT.HEALTHADMINISTRATOR HEA ON CAST(ING.GENCONENTITY as varchar)=HEA.Code LEFT JOIN
Contract.CareGroup CG ON CAST(ING.GENCAREGROUP as varchar)=CG.Code INNER JOIN
dbo.INPROFSAL PRO ON RR.CODPROSAL=PRO.CODPROSAL INNER JOIN
dbo.INESPECIA ESP ON PRO.CODESPEC1=ESP.CODESPECI INNER JOIN
dbo.HCREFCONTDET DX ON RR.AUTO=DX.HCREFCONTAUTO INNER JOIN
dbo.INDIAGNOS DIA ON DX.CODDIAGNO=DIA.CODDIAGNO INNER JOIN
dbo.INESPECIA ESPR ON RR.CODESPECI=ESPR.CODESPECI INNER JOIN
dbo.HCREFCONP RRP ON RR.IDHCREFCONP=RRP.AUTO INNER JOIN
dbo.RCMOTREF MOTR ON RR.MOTREMISI=MOTR.Id INNER JOIN
dbo.RCSERVICIOS SER ON RR.SERVIDOREM=SER.Codigo LEFT JOIN
dbo.HCREFCONTD RRD ON RRP.AUTO=RRD.HCREFCONPID AND RRD.ID=(SELECT MAX(A.ID) FROM dbo.HCREFCONTD A WHERE RRD.HCREFCONPID=A.HCREFCONPID) LEFT JOIN
dbo.INENTIDAD ENT ON RRD.CODENTIDA=ENT.CODENTIDA LEFT JOIN
dbo.RCMODSOLIC MODR ON RRD.RCMODSOLICID=MODR.Id LEFT JOIN
dbo.SEGusuaru USU ON RRD.CODUSUAREG=USU.CODUSUARI LEFT JOIN
dbo.ADCONTIPS IPS ON RRD.IPSACEPTADO=IPS.CODIGOIPS LEFT JOIN
DBO.RCMOTNOREF MOTS ON RRP.RCMOTNOREFID=MOTS.Id LEFT JOIN
dbo.SEGusuaru USUS ON RRP.USUARSUSPEN=USUS.CODUSUARI
--WHERE ING.IPCODPACI='33448680'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a análisis de oportunidad en el proceso de referencia y contrarreferencia de pacientes. Consolida, por ingreso, la primera solicitud de remisión (excluyendo motivos con ''A''), enriquecida con datos demográficos del paciente, profesional solicitante, especialidad destino, entidad aseguradora, IPS receptora y estado de gestión. Calcula tiempos en horas entre la solicitud y el egreso, y entre la solicitud y la suspensión, junto con dimensiones temporales para análisis mensual/anual.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la información clínica y administrativa de la primera solicitud de referencia/remisión de cada ingreso, junto con su seguimiento, estado, tiempos y entidades involucradas, para medir oportunidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso (NUMINGRES) debe tener al menos un registro en HCREFCONT cuyo MOTREMISI no contenga la letra ''A''; HCREFCONT.AUTO debe ser convertible a INT para poder seleccionar el mínimo por ingreso; Deben existir maestros consistentes: centro de atención, unidad funcional, paciente, tipo de documento, profesional, especialidad, diagnóstico, motivo de referencia y servicio de remisión (joins INNER); El AUTO de HCREFCONT debe enlazar con HCREFCONP vía IDHCREFCONP; ING.GENCONENTITY e ING.GENCAREGROUP deben ser convertibles a varchar para emparejar con HEALTHADMINISTRATOR.Code y CareGroup.Code', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reporta la primera referencia (AUTO mínimo) por cada ingreso; Se excluyen referencias cuyo motivo de remisión contiene la letra ''A''; Para cada referencia incluida se toma únicamente el último detalle de seguimiento (MAX(ID) en HCREFCONTD); La cantidad siempre se reporta como 1 por fila (columna CANTIDAD); ID_COMPANY se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres; ULT_ACTUAL refleja la hora actual convertida a la zona horaria ''Pakistan Standard Time''; Los tiempos de oportunidad se miden en HORAS: solicitud vs egreso y solicitud vs suspensión; La especialidad del profesional proviene de su especialidad principal (CODESPEC1), mientras que la especialidad a remitir proviene de CODESPECI de la referencia', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Referencia y contrarreferencia; Oportunidad de la referencia; Remisión de pacientes; Motivo de remisión; Diagnóstico CIE-10; Especialidad médica; Nivel de atención al que se remite; Estado de la solicitud (solicitado, gestionando, aceptado, suspendido, salida, pertinencia); Tipo de entidad receptora (IPS, EAPB, CRUE); Modalidad de gestión; Visado de la solicitud; Suspensión de la remisión; Tipo de paciente (Maternas, Pediátrico, Población general, Adulto mayor); Centro de atención; Unidad funcional; Grupo de atención; Entidad/aseguradora; IPS aceptante; Ingreso/episodio del paciente', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewReferencia: Devuelve una fila por la PRIMERA referencia (MIN(AUTO)) de cada ingreso cuyo motivo de remisión NO contenga ''A'', enriquecida con datos del paciente, profesional, diagnóstico, entidad, IPS aceptante y tiempos de gestión.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RR.MOTREMISI NOT LIKE ''%A%'' y AUTO = MIN(AUTO) por NUMINGRES → Se incluye la referencia en el reporte (CTE_SOLICITUDES) else Se excluye la referencia; si PAC.IPSEXOPAC = 1 → GENERO = ''MASCULINO'' else GENERO = ''FEMENINO''; si ING.CODTIPPAC IN (''1'',''2'',''3'',''4'',''5'') → Clasifica TIPO DE PACIENTE como Maternas / Pediátrico / Población general / Adulto mayor / Población general respectivamente else TIPO DE PACIENTE queda NULL; si RRP.ESTADO entre 1 y 6 → Traduce a etiqueta: 1=Solicitado, 2=Pendiente o Gestionando, 3=Aceptado con pendiente de salida, 4=Suspendido, 5=Ya salió, 6=Solicitud con Pertinencia else ESTADO queda NULL; si RRD.TIPENTIDAD IN (1,2,3,4) → TIPO ENTIDAD = IPS / EAPB / CRUE / OTRAS respectivamente else TIPO ENTIDAD queda NULL; si Cálculo de edad con DATEADD(YY,DATEDIFF(YEAR,IPFECNACI,GETDATE()),IPFECNACI) > GETDATE() → Resta 1 año a la edad calculada (aún no ha cumplido años en el año actual) else Mantiene la diferencia de años; si Existe registro en HCREFCONTD para el HCREFCONP → Toma el detalle con MAX(ID) por HCREFCONPID (último seguimiento) else Campos de seguimiento (entidad, modalidad, usuario, IPS aceptado, fechas) quedan NULL por LEFT JOIN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFCONT; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; CONTRACT.HEALTHADMINISTRATOR; Contract.CareGroup; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCREFCONTDET; dbo.INDIAGNOS; dbo.HCREFCONP; dbo.RCMOTREF; dbo.RCSERVICIOS; dbo.HCREFCONTD; dbo.INENTIDAD; dbo.RCMODSOLIC; dbo.SEGusuaru; dbo.ADCONTIPS; dbo.RCMOTNOREF', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferencia';
GO
