
    /*******************************************************************************************************************
Nombre: [Report].[ViewReferanciaRecepcion]
Tipo: Vista
Observacion:Oportunidad de la recepción de la referencia
Profesional: Nilsson Galindo
Fecha:13-10-2023
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

CREATE VIEW [Report].[ViewReferenciaRecepcion] AS

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CA.NOMCENATE AS CENTRO_ATENCION,
DOC.Nombre as [TIPO DOCUMENTO],
RR.IPCODPACI AS IDENTIFICACION,
PAC.IPNOMCOMP AS [PACIENTE],
CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS[GENERO DEL PACIENTE],
CAST(PAC.IPFECNACI AS DATE) AS [FECHA DE NACIMIENTO],
CASE RR.CODTIPPAC WHEN '1' THEN 'Maternas' 
				  WHEN '2' THEN 'Pediatrico'
				  WHEN '3' THEN 'Población general' 
				  WHEN '4' THEN 'Adulto mayor' 
				  WHEN '5' THEN 'Población general' END AS [TIPO DE PACIENTE], 
DATEDIFF(YEAR,IPFECNACI ,GETDATE()) -(CASE WHEN DATEADD(YY,DATEDIFF(YEAR,IPFECNACI,GETDATE()),IPFECNACI)>GETDATE() THEN 1 ELSE 0 END) AS EDAD,
RR.FECREGIS AS [FECHA REGISTRO REFERENCIA], 
CASE RR.CODTIPOENTSOLI WHEN '1' THEN 'IPS' 
					   WHEN '2' THEN 'EAPB' 
					   WHEN '3' THEN 'CRUE' 
					   WHEN '4' THEN 'OTRA' END AS [TIPO ENTIDAD SOLICITUD],
EN.NOMENTIDA AS [EAPB PAGADORA], 
ME.Nombre AS [MEDIO DE ENVIO REFERENCIA],
RR.FECMEDENV AS [FECHA DE ENVIO],
IPS.DSCRIPIPS AS [IPS QUE REMITE],
RS.Nombre AS [SERVICIO QUE REMITE],
RR.MEDREMITE AS [MEDICO QUE REMITE],
E.DESESPECI AS [ESPECIALIDAD QUE REMITE],
UF.UFUDESCRI AS [SERVICIO AL QUE SE REMITE], 
E1.DESESPECI AS [ESPECIALIDAD A LA QUE REMITE],
MODS.Nombre AS [MODALIDAD DE LA SOLICITUD],
MR.Nombre AS [MOTIVO DE LA REFERENCIA], 
CASE RR.CODCOMPLEJ WHEN '1' THEN 'Baja' 
				   WHEN '2' THEN 'Media' 
				   WHEN '3' THEN 'Alta' END AS COMPLEJIDAD,
CASE RR.CODURGVITAL WHEN 0 THEN 'No' ELSE 'Si' END AS [URGENCIA VITAL],

DIAG.CODDIAGNO AS [CODIGO DIAGNOSTICO],
DIAG.NOMDIAGNO AS DIAGNOSTICO, 
CASE RR.ESTADO WHEN '1' THEN 'Registrado' 
			   WHEN '2' THEN 'Aceptado sin ingreso' 
			   WHEN '3' THEN 'Aceptado coon ingreso' 
			   WHEN '4' THEN 'Rechazado' 
			   WHEN '5' THEN 'Cancelado' 
			   WHEN '6' THEN 'Egresado' END AS ESTADO,
U.NOMUSUARI AS [USUARIO RECEPCION],
RR.CONSEC AS CONSECUTIVO, RR.OBSERVACION,
RR.NUMINGRES AS [NUMERO INGRESO], 
I.IFECHAING AS [FECHA INGRESO], 
DATEDIFF(DAY, RR.FECREGIS,I.IFECHAING)/24 AS [DIAS REGISTRO VS INGRESO],
DATEDIFF(HOUR, RR.FECREGIS,I.IFECHAING) AS [HORAS REGISTRO VS INGRESO],
IIF(RR.ESTADO IN (4,5),DE.OBSERVACION,NULL) AS [OBSERVACION RECHAZO],
IIF(RR.ESTADO IN (4,5),REC.CODIGO+'-'+REC.DESCRIPCION,NULL) AS [MOTIVO RECHAZO],
IIF(RR.ESTADO IN (4,5),DE.FECREGIS,NULL) AS [FECHA RECHAZO],
IIF(RR.ESTADO IN (4,5),DATEDIFF(DAY, RR.FECREGIS,DE.FECREGIS),NULL) AS [DIAS REGISTRO VS RECHAZO],
IIF(RR.ESTADO IN (4,5),DATEDIFF(HOUR, RR.FECREGIS,DE.FECREGIS),NULL) AS [HORAS REGISTRO VS RECHAZO],
IIF(RR.ESTADO IN (4,5),USU.NOMUSUARI,NULL) AS [USUARIO RECHAZO],
1 as 'CANTIDAD',
CAST(RR.FECREGIS AS date) AS 'FECHA BUSQUEDA',
YEAR(RR.FECREGIS) AS 'AÑO FECHA BUSQUEDA',
MONTH(RR.FECREGIS) AS 'MES FECHA BUSQUEDA',
CASE MONTH(RR.FECREGIS)
WHEN 1 THEN 'ENERO'
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
WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM            
dbo.HCREFRECEP RR INNER JOIN
dbo.ADCENATEN CA ON RR.ADCENATENID = CA.CODCENATE LEFT JOIN
dbo.INPACIENT PAC ON RR.IPCODPACI=PAC.IPCODPACI  LEFT JOIN
dbo.ADTIPOIDENTIFICA DOC ON PAC.IPTIPODOC=DOC.CODIGO LEFT JOIN
dbo.INENTIDAD AS EN ON RR.INENTADMID=EN.CODENTIDA LEFT JOIN
dbo.ADCONTIPS IPS ON RR.ADCONTIPSID=IPS.CODIGOIPS LEFT JOIN
dbo.RCMEDIOSENVIO AS ME ON RR.RCMEDIOSENVIOID = ME.ID LEFT JOIN
dbo.RCSERVICIOS AS RS ON RR.RCSERVICIOSID = RS.Id LEFT JOIN
dbo.INUNIFUNC AS UF ON RR.INUNIFUNCID = UF.UFUCODIGO LEFT JOIN
dbo.INESPECIA AS E ON RR.INESPECIAIDREMITE = E.CODESPECI LEFT JOIN
dbo.INESPECIA AS E1 ON RR.INESPECIAIDAREMITIR = E1.CODESPECI LEFT JOIN
dbo.SEGusuaru AS U ON RR.USERCREA = U.CODUSUARI LEFT JOIN
dbo.RCMOTREF AS MR ON MR.Id = RR.RCMOTREFID LEFT JOIN
dbo.HCREFREDIAG AS REFD ON REFD.HCREFRECEPID = RR.ID AND REFD.PRINCIPAL = 1 LEFT JOIN
dbo.INDIAGNOS AS DIAG ON REFD.INDIAGNOSCOD = DIAG.CODDIAGNO LEFT JOIN
dbo.ADINGRESO AS I ON I.NUMINGRES = RR.NUMINGRES LEFT JOIN
dbo.RCMODSOLIC MODS ON RR.RCMODSOLICID=MODS.Id LEFT JOIN
dbo.HCREFRECDE DE ON RR.ID=DE.HCREFRECEPID AND DE.ID=(SELECT MAX(A.ID) FROM dbo.HCREFRECDE A WHERE RR.ID=A.HCREFRECEPID) LEFT JOIN
dbo.HCMOTREFREC REC ON DE.HCMOTREFRECID=REC.ID LEFT JOIN
dbo.SEGusuaru AS USU ON DE.FUNCREG=USU.CODUSUARI
--WHERE RR.IPCODPACI='80129656'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a analizar la **oportunidad en la recepción de referencias médicas**. Consolida, para cada solicitud de referencia/remisión registrada en `HCREFRECEP`, los datos demográficos del paciente, la IPS remitente, especialidades origen y destino, diagnóstico principal CIE-10, estado del proceso (registrado, aceptado, rechazado, cancelado, egresado) y —cuando aplica— motivo, usuario y fechas de rechazo. Calcula brechas temporales en días y horas entre el registro de la referencia y el ingreso efectivo o el rechazo, facilitando el seguimiento del indicador de oportunidad en la red de referencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reportería que consolida la oportunidad y trazabilidad de la recepción de referencias de pacientes, integrando datos del paciente, entidad solicitante, IPS remitente, servicio/especialidad, diagnóstico principal, estado de la referencia y tiempos de gestión (registro, ingreso y rechazo).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe al menos un registro en dbo.HCREFRECEP (referencias) que se cruza con su centro de atención en dbo.ADCENATEN.; Para enriquecer datos de paciente, entidad, IPS, medio de envío, servicio, unidad funcional, especialidades, motivo, modalidad, diagnóstico, ingreso, recepción y usuarios, las claves foráneas deben coincidir; de lo contrario los campos quedan en NULL por los LEFT JOIN.; El diagnóstico mostrado proviene exclusivamente del registro marcado como principal (HCREFREDIAG.PRINCIPAL = 1).; La información de rechazo se obtiene del último registro de recepción por referencia (MAX(ID) en HCREFRECDE para cada HCREFRECEPID).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El diagnóstico reportado siempre corresponde al diagnóstico principal de la referencia (HCREFREDIAG.PRINCIPAL = 1).; Los datos de rechazo provienen siempre del registro de recepción más reciente por referencia (MAX(ID) en HCREFRECDE por HCREFRECEPID).; El identificador de compañía corresponde al nombre actual de la base de datos (DB_NAME()) truncado a 9 caracteres.; ULT_ACTUAL siempre se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El campo CANTIDAD siempre vale 1 (cada fila cuenta como una referencia para conteos).; Los días de oportunidad entre registro e ingreso se calculan como DATEDIFF(DAY)/24 (división entera, no como días reales).; Los campos de rechazo solo se diligencian cuando el estado de la referencia es 4 (Rechazado) o 5 (Cancelado).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Referencia y contrarreferencia; Recepción de referencia; Paciente; Tipo de paciente (maternas, pediátrico, adulto mayor, población general); EAPB pagadora; IPS remitente; Entidad solicitante (IPS, EAPB, CRUE); Especialidad médica; Unidad funcional / servicio; Modalidad de solicitud; Motivo de referencia; Complejidad de atención (baja, media, alta); Urgencia vital; Diagnóstico principal (CIE-10); Estado de la referencia (registrado, aceptado, rechazado, cancelado, egresado); Ingreso/admisión del paciente; Motivo y observación de rechazo; Oportunidad (tiempos entre registro, ingreso y rechazo)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewReferenciaRecepcion: Devuelve una fila por referencia de HCREFRECEP enriquecida con catálogos y tiempos calculados; no realiza cambios en datos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPSEXOPAC = 1 → Género del paciente = ''MASCULINO'' else Género del paciente = ''FEMENINO''; si RR.CODTIPPAC en {''1'',''2'',''3'',''4'',''5''} → Mapea a tipo de paciente: 1=Maternas, 2=Pediátrico, 3=Población general, 4=Adulto mayor, 5=Población general; si RR.CODTIPOENTSOLI en {''1'',''2'',''3'',''4''} → Mapea tipo de entidad solicitante: 1=IPS, 2=EAPB, 3=CRUE, 4=OTRA; si RR.CODCOMPLEJ en {''1'',''2'',''3''} → Mapea complejidad: 1=Baja, 2=Media, 3=Alta; si RR.CODURGVITAL = 0 → Urgencia vital = ''No'' else Urgencia vital = ''Si''; si RR.ESTADO en {''1''..''6''} → Mapea estado: 1=Registrado, 2=Aceptado sin ingreso, 3=Aceptado con ingreso, 4=Rechazado, 5=Cancelado, 6=Egresado; si RR.ESTADO IN (4,5) → Expone observación, motivo (código-descripción), fecha, días/horas desde registro y usuario del rechazo tomados del último HCREFRECDE else Estos campos de rechazo se devuelven como NULL; si DATEADD(YY, DATEDIFF(YEAR, IPFECNACI, GETDATE()), IPFECNACI) > GETDATE() → Resta 1 al cálculo de la edad (aún no ha cumplido años en el periodo) else Edad = diferencia simple en años; si MONTH(RR.FECREGIS) entre 1 y 12 → Traduce el número del mes a su nombre en español (ENERO..DICIEMBRE)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFRECEP; dbo.ADCENATEN; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.INENTIDAD; dbo.ADCONTIPS; dbo.RCMEDIOSENVIO; dbo.RCSERVICIOS; dbo.INUNIFUNC; dbo.INESPECIA; dbo.SEGusuaru; dbo.RCMOTREF; dbo.HCREFREDIAG; dbo.INDIAGNOS; dbo.ADINGRESO; dbo.RCMODSOLIC; dbo.HCREFRECDE; dbo.HCMOTREFREC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReferenciaRecepcion';
GO
