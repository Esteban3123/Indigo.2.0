

/*
Nombre: [Report].[[ViewIngresosAbiertos]]
Tipo:Vista
Observacion: Ingresos Abiertos
Profesional:
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Nelly Patricia Morales Capera
Fecha: 02-05-2023
Ovservaciones: Se incluye el numero de la boleta de salida y el usuario que lo registra
_____________________________________________________________________________
Version 3
Persona que modifico: Nilsson Miguel Galindo
Fecha: 08-08-2023
Ovservaciones: Se agrega el campo de fecha de la orden de salida
--------------------------------------
*/

CREATE VIEW [Report].[ViewIngresosAbiertos] AS

---------********* SCRIPS PARA IDENTIFICAR LOS INGRESOS ABIERTOS PENDIENTES DE LIQUIDAR*********---------
WITH CTE_INGRESOS_ABIERTOS AS
 (
  SELECT 
   ING.NUMINGRES 'INGRESO',
   ING.IPCODPACI 'IDENTIFICACION',
   CAST(ING.IFECHAING AS DATE) 'FECHA INGRESO',
   CAST(ING.IFECHAING AS TIME) 'HORA INGRESO',
   CASE ING.TIPOINGRE WHEN 1 THEN 'AMBULATORIO' ELSE 'HOSPITALARIO' END 'TIPO INGRESO',
   ING.IAUTORIZA ,ING.UFUINGMED ,ING.UFUEGRMED ,ING.UFUINGHOS ,ING.UFUEGRHOS ,ING.CODPROING ,ING.CODPROEGR ,ING.CODESPTRA ,
  ING.UFUAACTMED ,ING.UFUAACTHOS ,ING.CODCAMACT ,ING.CODDIAING ,ING.CODDIAEGR ,ING.UFUACTPAC ,ING.CODICAMHO ,ING.FECHOSPIT ,ING.GENCONENTITY ,ING.GENCAREGROUP ,
  ING.FECHEGRESO 'FECHA EGRESO CAMA', CASE ING.IESTADOIN WHEN '' THEN 'ABIERTO' ELSE 'PARCIAL' END 'ESTADO' ,USU.NOMUSUARI 'USUARIO CREO',USUM.NOMUSUARI 'USUARIO ANULO',
  ING.UFUCODIGO, CEN.NOMCENATE 'CENTRO ATENCION',ING.IOBSERVAC
   FROM DBO.ADINGRESO ING 
   INNER JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE =ING.CODCENATE 
   LEFT JOIN DBO.SEGusuaru AS USU ON USU.CODUSUARI =ING.CODUSUCRE 
   LEFT JOIN DBO.SEGusuaru AS USUM ON USUM.CODUSUARI =ING.CODUSUANU
   WHERE ING.IESTADOIN IN ('' , 'P')
  ),

CTE_CARGOS_INGRESOS AS
 (
  SELECT 
   RC.AdmissionNumber 'INGRESO',
   SUM(RCD.TotalFolio) 'TOTAL FOLIO',
   boleta.*
  FROM 
   BILLING.REVENUECONTROL RC 
   INNER JOIN CTE_INGRESOS_ABIERTOS AS ING ON RC.AdmissionNumber = ING.INGRESO  
   INNER JOIN Billing.RevenueControlDetail AS RCD  ON RC.Id =RCD.RevenueControlId AND RCD.Status NOT IN (2,4) 
   LEFT JOIN (SELECT MAX (B.[Id]) [Id], PER.Fullname [USUARIO REGISTRO], B.[AdmissionNumber],B.CreationDate--V3
			  FROM 
			   Billing.SlipOut B
			   LEFT JOIN Security .[User] AS USU  ON USU.UserCode = B.[CreationUser] 
			   LEFT JOIN Security .Person AS PER  ON PER.Id = USU.IdPerson 
			  GROUP BY PER.Fullname, B.[AdmissionNumber],B.CreationDate) boleta ON boleta.AdmissionNumber = RC.AdmissionNumber
  GROUP BY RC.AdmissionNumber, boleta.[Id], boleta.[USUARIO REGISTRO], boleta.[AdmissionNumber],boleta.CreationDate
),

CTE_ALTA_MEDICA
AS
(
  SELECT EGR.NUMINGRES 'INGRESO' ,EGR.IPCODPACI 'IDENTIFICACION' ,MAX(EGR.FECALTPAC) 'FECHA ALTA MEDICA'  FROM DBO.HCREGEGRE EGR
  INNER JOIN CTE_INGRESOS_ABIERTOS AS ING  ON EGR.NUMINGRES  =ING.INGRESO 
  GROUP BY EGR.NUMINGRES,EGR.IPCODPACI
),

CTE_HISTORIAS_AMBULATORIAS
AS
(
  SELECT HIS.NUMINGRES ,HIS.IPCODPACI ,MAX(FECHISPAC) AS 'FECHA ALTA'    FROM  DBO.HCHISPACA HIS
  INNER JOIN CTE_INGRESOS_ABIERTOS  AS PEN ON PEN.INGRESO =HIS.NUMINGRES 
  WHERE HIS.GENCONEXT =1
  GROUP BY HIS.NUMINGRES ,HIS.IPCODPACI
)

SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 ING.[CENTRO ATENCION],
 ING.INGRESO,
 ING.[FECHA INGRESO],
 ING.[HORA INGRESO],
 ING.ESTADO 'ESTADO INGRESO',
 ING.[TIPO INGRESO],
 CASE PAC.IPTIPODOC 
  WHEN '1' THEN 'CEDULA DE CIUDADANIA' 
  WHEN '2' THEN 'CEDULA DE EXTRANJERIA' 
  WHEN '3' THEN 'TARJETA DE IDENTIDAD' 
  WHEN '4' THEN 'REGISTRO CIVIL' 
  WHEN '5' THEN 'PASAPORTE' 
  WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' 
  WHEN '7' THEN 'MENOR SIN IDENTIFICACION' 
  WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACION' 
  WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' 
  WHEN '10' THEN 'CARNET DIPLOMATICO'
  WHEN '11' THEN 'SALVOCONDUCTO' 
  WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' ELSE 'N/A' END AS 'TIPO IDENTIFICACION',
 CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS 'SEXO',
 CAST(PAC.IPFECNACI AS DATE) 'FECHA NACIMIENTO',
 FLOOR((CAST(CONVERT(VARCHAR(8), ING.[FECHA INGRESO]  , 112) AS INT) - CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS 'EDAD',
 rtrim(PAC.IPDIRECCI) AS DIRECCION, 
 PAC.IPTELEFON AS TELEFONO, 
 PAC.IPTELMOVI AS MOVIL,
 ING.IDENTIFICACION,
 rtrim(PAC.IPNOMCOMP) 'PACIENTE',
 UNI.UFUCODIGO 'COD U FUNCIONAL',
 rtrim(UNI.UFUDESCRI) 'UNIDAD FUNCIONAL',
 IIF(ING.[TIPO INGRESO]='HOSPITALARIO', ALT.[FECHA ALTA MEDICA], AMB.[FECHA ALTA]) 'FECHA ALTA MEDICA',
 ING.[FECHA EGRESO CAMA],
 CAM.DESCCAMAS 'CAMA',EA.Code 'CODIGO ENTIDAD',
 EA.Name 'ENTIDAD', GA.Code 'CODIGO GRUPO ATENCION',
 GA.Name AS 'GRUPO DE ATENCION',
 ISNULL(VAL.[TOTAL FOLIO],'0') [TOTAL FOLIO], 
 ING.IAUTORIZA AS [AUTORIZACION INGRESO],
 ING.IOBSERVAC AS OBSERVACIONES,
 ING.[USUARIO CREO],
 ING.[USUARIO ANULO],
 VAL.CreationDate AS [FECHA ORDEN SALIDA],--V3
 VAL.[ID] [ORDEN SALIDA],
 VAL.[USUARIO REGISTRO],
 1 as 'CANTIDAD',
 CAST([FECHA INGRESO] AS date) AS 'FECHA BUSQUEDA',
  YEAR([FECHA INGRESO]) AS 'AÑO BUSQUEDA',
  MONTH([FECHA INGRESO]) AS 'MES BUSQUEDA',
  CONCAT(FORMAT(MONTH([FECHA INGRESO]), '00') ,' - ', 
	   CASE MONTH([FECHA INGRESO]) 
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
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 CTE_INGRESOS_ABIERTOS AS ING 
 INNER JOIN CONTRACT.CAREGROUP AS GA  ON GA.ID = ING.GENCAREGROUP
 INNER JOIN CONTRACT.HEALTHADMINISTRATOR AS EA  ON EA.ID = ING.GENCONENTITY
 INNER JOIN DBO.INPACIENT AS PAC  ON PAC.IPCODPACI = ING.IDENTIFICACION 
 INNER JOIN DBO.INUNIFUNC AS UNI  ON UNI.UFUCODIGO = ISNULL(ING.UFUACTPAC, ING.UFUCODIGO) 
 LEFT JOIN CTE_ALTA_MEDICA AS ALT  ON ALT.INGRESO  =ING.INGRESO 
 LEFT JOIN DBO.CHCAMASHO AS CAM ON CAM.CODICAMAS  =ING.CODCAMACT 
 LEFT JOIN CTE_HISTORIAS_AMBULATORIAS AS AMB  ON AMB.NUMINGRES =ING.INGRESO 
 LEFT JOIN CTE_CARGOS_INGRESOS AS VAL ON VAL.INGRESO =ING.INGRESO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a gestión de cuentas hospitalarias pendientes de liquidar. Consolida los ingresos con estado abierto o parcial (ambulatorios y hospitalarios), incorporando datos demográficos del paciente, unidad funcional activa, cama asignada, entidad contratante, grupo de atención, total de cargos facturados y la boleta/orden de salida asociada. Incluye fecha de alta médica, fechas de egreso de cama y campos de búsqueda por período, facilitando seguimiento de ingresos no cerrados administrativamente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos hospitalarios y ambulatorios en estado abierto o parcialmente liquidado, enriquecidos con datos del paciente, entidad/grupo de atención, cama, alta médica, totales facturados y orden de salida, para reportería de pendientes de liquidación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen ingresos en ADINGRESO con IESTADOIN en ('''',''P'') (abiertos o parciales); El ingreso debe tener centro de atención válido en ADCENATEN, grupo de atención en CONTRACT.CAREGROUP y entidad administradora en CONTRACT.HEALTHADMINISTRATOR; El paciente referenciado por IPCODPACI debe existir en INPACIENT; La unidad funcional (UFUACTPAC o UFUCODIGO) debe existir en INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos con IESTADOIN en ('''',''P''); los liquidados/cerrados quedan excluidos; Los detalles de RevenueControl con Status 2 o 4 nunca aportan al TOTAL FOLIO; La fecha de alta ambulatoria solo considera historias con GENCONEXT=1; La edad se calcula como años completos a partir de IPFECNACI vs FECHA INGRESO usando aritmética yyyymmdd/10000; El ID_COMPANY se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres; ULT_ACTUAL se entrega convertido a la zona horaria ''Pakistan Standard Time''; Se toma el MAX(Id) de SlipOut por ingreso para representar la última orden de salida', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso abierto; Ingreso parcial; Ingreso hospitalario; Ingreso ambulatorio; Alta médica; Historia clínica ambulatoria; Paciente; Tipo de identificación; Unidad funcional; Cama; Entidad administradora de salud; Grupo de atención; Autorización de ingreso; Folio de facturación; Boleta/orden de salida; Centro de atención; Egreso de cama', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un conjunto de filas con un registro por ingreso abierto/parcial; CANTIDAD=1 fija por fila para conteos en reportería', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.TIPOINGRE = 1 → Clasifica el ingreso como ''AMBULATORIO'' else Clasifica como ''HOSPITALARIO''; si ING.IESTADOIN = '''' → Marca el ingreso como ''ABIERTO'' else Marca como ''PARCIAL'' (aplica solo a IESTADOIN=''P'' por el filtro IN ('''',''P'')); si TIPO INGRESO = ''HOSPITALARIO'' → Toma FECHA ALTA MEDICA desde HCREGEGRE (CTE_ALTA_MEDICA) else Toma FECHA ALTA desde HCHISPACA con GENCONEXT=1 (CTE_HISTORIAS_AMBULATORIAS); si RCD.Status NOT IN (2,4) → Suma TotalFolio del detalle al TOTAL FOLIO del ingreso else Excluye los detalles con estado 2 o 4 del cálculo; si ING.UFUACTPAC IS NULL → Usa ING.UFUCODIGO para resolver la unidad funcional else Usa UFUACTPAC (unidad funcional actual del paciente); si PAC.IPSEXOPAC = 1 → Sexo = ''MASCULINO'' else Sexo = ''FEMENINO''; si VAL.[TOTAL FOLIO] IS NULL → Reporta TOTAL FOLIO = ''0'' else Reporta el total calculado', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.ADINGRESO; DBO.ADCENATEN; DBO.SEGusuaru; BILLING.REVENUECONTROL; Billing.RevenueControlDetail; Billing.SlipOut; Security.User; Security.Person; DBO.HCREGEGRE; DBO.HCHISPACA; CONTRACT.CAREGROUP; CONTRACT.HEALTHADMINISTRATOR; DBO.INPACIENT; DBO.INUNIFUNC; DBO.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosAbiertos';
GO
