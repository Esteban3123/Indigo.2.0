/*******************************************************************************************************************
Nombre: [Report].[ViewOpportunityBloodComponents]
Tipo:vista
Observacion:Oportunidad de solicitud y despacho de hemocomponentes
Profesional: Nilsson Galindo
Fecha:12-12-2012
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

CREATE VIEW [Report].[ViewOpportunityBloodComponents] AS

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
TIP.NOMBRE AS [TIPO IDENTIFICACIÓN],
HMCO.IPCODPACI AS [IDENTIFICACIÓN],
PAC.IPNOMCOMP AS [NOMBRE],
CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS[GENERO DEL PACIENTE],
CAST(PAC.IPFECNACI AS DATE) AS [FECHA DE NACIMIENTO],
CASE ING.CODTIPPAC WHEN '1' THEN 'Maternas' 
				   WHEN '2' THEN 'Pediatrico'
				   WHEN '3' THEN 'Población general' 
				   WHEN '4' THEN 'Adulto mayor' 
				   WHEN '5' THEN 'Población general' END AS [TIPO DE PACIENTE], 
DATEDIFF(YEAR,IPFECNACI ,GETDATE()) -(CASE WHEN DATEADD(YY,DATEDIFF(YEAR,IPFECNACI,GETDATE()),IPFECNACI)>GETDATE() THEN 1 ELSE 0 END) AS EDAD,
HEA.Code AS [CODIGO ENTIDAD],
HEA.Name AS ENTIDAD,
CG.Name AS [GRUPO DE ATENCION],
HMCO.NUMINGRES AS INGRESO,
HMCO.NUMEFOLIO AS FOLIO,
HMCO.FECORDMED AS [FECHA SOLICITUD],
FUN.UFUDESCRI AS [UNIDAD FUNCIONAL SOLICITUD],
HSAN.DESCOMSAM AS HEMOCOMPONENTE,
HBOL.VolumeTransfuse AS [VOLUMEN A TRANSFUNDIR],
CASE HBOL.ESTADO WHEN 0 THEN 'RECHAZADO'
				 WHEN 1 THEN 'Solicitud Reserva'
				 WHEN 2 THEN 'Solicitud de Transfusión'
				 WHEN 3 THEN 'Reserva sin Solicitud de Transfusión'
				 WHEN 4 THEN 'Reserva con Solicitud de Transfusión'
				 WHEN 5 THEN 'Liberado'
				 WHEN 6 THEN 'Registro no realizado'
				 WHEN 7 THEN 'Aplicado - registro realizado'
				 WHEN 8 THEN 'No Aplicado - registro descartado'
				 WHEN 9 THEN 'Anulado'
				 WHEN 10 THEN 'Descartado por salida de paciente'
				 WHEN 11 THEN 'Extramural' ELSE CAST(HBOL.ESTADO AS VARCHAR) END AS [ESTADO],
HBOL.FECSOLRES AS [FECHA SOLICITU RESERVA],
HBOL.FECSOLTRA AS [FECHA SOLICITUD TRANSFUSIÓN],
HBOL.FECRESERVA AS [FECHA RESERVA],
HBOL.FECENTREGA AS [FECHA ENTREGA BOLSA],
HBOL.FECRECBOLSA AS [FECHA RECIBE BOLSA],
HBOL.NUMBOLSA AS [NUMERO BOLSA],
HBOL.FECHINITRA AS [FECHA INICIO TRANSFUSIÓN],
HBOL.FECHFINTRA AS [FECHA FINAL TRANSFUSIÓN],
DATEDIFF(MINUTE,HBOL.FECHINITRA,HBOL.FECHFINTRA) AS [DURACION TRANSFUSIÓN MIN],
HBOL.VOLTRANSF AS [VOLUMEN TRANSFUNDIDO],
REC.DESCRIPCION AS [MOTIVO RECHAZO],
PRO.CODPROSAL+'-'+PRO.NOMMEDICO AS [ENFERMERA RECHAZO],
1 as 'CANTIDAD',
CAST(HMCO.FECORDMED AS date) AS [FECHA BUSQUEDA],
YEAR(HMCO.FECORDMED) AS [AÑO FECHA BUSQUEDA],
MONTH(HMCO.FECORDMED) AS [MES FECHA BUSQUEDA],
CASE MONTH(HMCO.FECORDMED) WHEN 1 THEN 'ENERO'
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
dbo.HCORHEMCO HMCO
INNER JOIN dbo.HCORHEMBOL HBOL ON HMCO.ID=HBOL.HCORHEMCOID
INNER JOIN dbo.INPACIENT PAC ON HMCO.IPCODPACI=PAC.IPCODPACI
INNER JOIN dbo.ADTIPOIDENTIFICA TIP ON PAC.IPTIPODOC=TIP.CODIGO
INNER JOIN dbo.ADINGRESO ING ON HMCO.NUMINGRES=ING.NUMINGRES
INNER JOIN CONTRACT.HEALTHADMINISTRATOR HEA ON ING.GENCONENTITY=HEA.Id
INNER JOIN Contract.CareGroup CG ON ING.GENCAREGROUP=CG.Id 
INNER JOIN dbo.HCCOMSAN HSAN ON HBOL.COMSAMID=HSAN.ID
INNER JOIN dbo.INUNIFUNC FUN ON HBOL.UFUSOLRES=FUN.UFUCODIGO
LEFT JOIN dbo.HCRECHEMO REC ON HBOL.HCRECHEMOID=REC.ID
LEFT JOIN dbo.INPROFSAL PRO ON HBOL.ENFRECHAZ=PRO.CODPROSAL
--WHERE HMCO.IPCODPACI='23114610729214' AND NUMEFOLIO='225'

--SELECT * FROM dbo.HCORHEMCO WHERE IPCODPACI='23114610729214' AND NUMEFOLIO='225'
--SELECT * FROM dbo.HCORHEMBOL WHERE HCORHEMCOID =25523
--
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a analizar la oportunidad en la solicitud y despacho de hemocomponentes por paciente e ingreso. Consolida el ciclo completo de cada bolsa de hemoderivado —desde la orden médica hasta la aplicación o rechazo— incluyendo fechas clave (solicitud, reserva, entrega, inicio y fin de transfusión) y su duración en minutos. Cruza datos demográficos del paciente, entidad aseguradora, grupo de atención, unidad funcional solicitante y motivo de rechazo, exponiendo el estado de cada bolsa en sus posibles etapas. Está diseñada para consumo en herramientas de reporting con dimensiones temporales (año, mes) sobre la fecha de solicitud.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la trazabilidad y oportunidad del ciclo de solicitud, reserva, entrega y transfusión de hemocomponentes por paciente, ingreso y bolsa, con tiempos y estados decodificados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada orden de hemoterapia (HCORHEMCO) debe tener al menos una bolsa asociada en HCORHEMBOL (INNER JOIN por HCORHEMCOID).; El paciente del ingreso debe existir en INPACIENT y su tipo de documento en ADTIPOIDENTIFICA.; El ingreso (NUMINGRES) debe existir en ADINGRESO y referenciar una entidad (GENCONENTITY) presente en CONTRACT.HEALTHADMINISTRATOR y un grupo de atención (GENCAREGROUP) en Contract.CareGroup.; El hemocomponente (COMSAMID) debe existir en HCCOMSAN y la unidad funcional solicitante (UFUSOLRES) en INUNIFUNC.; El motivo de rechazo (HCRECHEMOID) y la enfermera que rechaza (ENFRECHAZ) son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY se obtiene de DB_NAME() truncado a 9 caracteres, identificando la base/empresa actual.; La cantidad reportada por bolsa siempre es 1 (constante para conteos en el reporte).; ULT_ACTUAL siempre se entrega convertido a la zona horaria ''Pakistan Standard Time''.; La duración de la transfusión se calcula en minutos entre FECHINITRA y FECHFINTRA.; Solo se incluyen bolsas cuya orden, paciente, ingreso, entidad, grupo de atención, hemocomponente y unidad funcional existan (filtros INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hemocomponentes; Transfusión; Reserva de bolsa; Orden de hemoterapia; Paciente; Ingreso/Admisión; Entidad administradora de salud; Grupo de atención; Unidad funcional; Motivo de rechazo de transfusión; Profesional de enfermería; Tipo de paciente (maternas, pediátrico, adulto mayor, población general); Oportunidad (tiempos del proceso transfusional)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Result set: Devuelve una fila por bolsa de hemocomponente cruzada con su orden, paciente, ingreso, entidad, grupo de atención y unidad funcional solicitante.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPSEXOPAC = 1 → Género se reporta como ''MASCULINO'' else Cualquier otro valor se reporta como ''FEMENINO''; si ING.CODTIPPAC IN (''1'',''2'',''3'',''4'',''5'') → Mapea a tipo de paciente: 1=Maternas, 2=Pediatrico, 3=Población general, 4=Adulto mayor, 5=Población general else NULL (sin etiqueta); si HBOL.ESTADO entre 0 y 11 → Decodifica el estado de la bolsa (RECHAZADO, Solicitud Reserva, Solicitud de Transfusión, Reserva sin/con Solicitud, Liberado, Registro no realizado, Aplicado, No Aplicado, Anulado, Descartado por salida de paciente, Extramural) else Convierte el valor numérico a VARCHAR sin etiqueta; si DATEADD(YY,DATEDIFF(YEAR,IPFECNACI,GETDATE()),IPFECNACI) > GETDATE() → Resta 1 a la edad calculada (aún no ha cumplido años en el año actual) else Mantiene la diferencia de años como edad', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORHEMCO; dbo.HCORHEMBOL; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.ADINGRESO; CONTRACT.HEALTHADMINISTRATOR; Contract.CareGroup; dbo.HCCOMSAN; dbo.INUNIFUNC; dbo.HCRECHEMO; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityBloodComponents';
GO
