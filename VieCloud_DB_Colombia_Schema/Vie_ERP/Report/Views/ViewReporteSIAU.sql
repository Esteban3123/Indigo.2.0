

/*******************************************************************************************************************
Nombre: [Report].[ViewReporteSIAU]
Tipo:Vista
Observacion:Informe de todas las solicitudes realizadas extramuralmente.
Profesional: Nelly Morales Capera
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 1
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 15-02-2023
Ovservaciones: Se suprime CTE_DatosPaciente, para mejorar la velocidad de la consulta, ademas .
--------------------------------------
Version 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewReporteSIAU] as

WITH CTE_SERVICIOS AS
 (
  SELECT NUMINGRES,NUMEFOLIO, CODPROSAL, CODSERIPS, CANSERIPS, OBSSERIPS, FECORDMED FROM dbo.HCORDIMAG WHERE MANEXTPRO='1'--IMAGENES dx
  UNION ALL
  SELECT NUMINGRES,NUMEFOLIO, CODPROSAL, CODSERIPS, CANSERIPS, OBSSERIPS,FECORDMED FROM dbo.HCORDINTE WHERE MANEXTPRO='1'--INTERCONSULTAS
  UNION ALL
  SELECT NUMINGRES,NUMEFOLIO, CODPROSAL, CODSERIPS, CANSERIPS, OBSSERIPS,FECORDMED FROM dbo.HCORDLABO WHERE MANEXTPRO='1' --LABORATORIOS
  UNION ALL
  SELECT NUMINGRES,NUMEFOLIO, CODPROSAL, CODSERIPS, CANSERIPS, OBSSERIPS,FECORDMED FROM dbo.HCORDPATO WHERE MANEXTPRO='1'--PATOLOGIAS
  UNION ALL
  SELECT NUMINGRES,NUMEFOLIO, CODPROSAL, CODSERIPS, CANSERIPS, OBSSERIPS,FECORDMED FROM dbo.HCORDPRON WHERE MANEXTPRO='1'--PROCEDIMIENTOS NO QX
  UNION ALL
  SELECT NUMINGRES,NUMEFOLIO, CODPROSAL, CODSERIPS, CANSERIPS, OBSSERIPS,FECORDMED FROM dbo.HCORDPROQ WHERE MANEXTPRO='1'--PROCEDIMIENTOS QX
  UNION ALL
  SELECT CON.NUMINGRES,CON.NUMEFOLIO,HC.CODPROSAL,CON.CODSERIPS,'1' AS CANSERIPS,'' AS OBSSERIPS,HC.FECHISPAC AS FECORDMED 
  FROM dbo.HCDESCOEX CON INNER JOIN dbo.HCHISPACA HC ON CON.NUMINGRES=HC.NUMINGRES AND CON.NUMEFOLIO=HC.NUMEFOLIO
)

  SELECT
   CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
   HIS.NUMINGRES,
   ROW_NUMBER() OVER (ORDER BY HIS.IPCODPACI ) AS [CONSECUTIVO],
   EA.Name AS [ENTIDAD ADMINISTRADORA],
   CASE PAC.IPTIPODOC WHEN '1' THEN 'CC' 
					  WHEN '2' THEN 'CE' 
					  WHEN '3' THEN 'TI' 
					  WHEN '4' THEN 'RC' 
					  WHEN '5' THEN 'PA'
					  WHEN '6' THEN 'AS' 
					  WHEN '7' THEN 'MS' 
					  WHEN '8' THEN 'NU' 
					  WHEN '9' THEN 'NV' 
					  WHEN '10' THEN 'CD' 
					  WHEN '11' THEN 'SC' 
					  WHEN '12' THEN 'PE' 
					  WHEN '13' THEN 'PT'
					  WHEN '14' THEN 'DE'
					  WHEN '15' THEN 'SI' END AS [TIPO DOCUMENTO],
   HIS.IPCODPACI AS [NUMERO DE DOCUMENTO],
   PAC.IPTELEFON AS [TELEFONO],
   PAC.IPTELMOVI AS [CELULAR],
   CASE HIS.GENCONEXT WHEN 1 THEN 'A' ELSE 'H' END [AMBITO],
   UNI.UFUDESCRI AS [SERVICIO], 
   HIS.FECHISPAC AS [FECHA ORDEN MEDICA],
   CEN.CODIPSSEC AS [CODIGO DE HABILITACION], 
    CASE PER.IdentificationType WHEN 1 THEN 'CC'
								WHEN 2 THEN 'CE' ELSE 'PA' END AS [TIPO DOCUMENTO PROFESIONAL],
   HIS.CODPROSAL AS [NUMERO DOCUMENTO PROFESIONAL],
   HIS.CODESPTRA [ESPECIALIDAD PROFESIONAL],
   CASE ING.ICAUSAING WHEN '3'  THEN '1' 
					  WHEN '10' THEN '2' 
					  WHEN '6'  THEN '3'
					  WHEN '7'  THEN '4' 
					  WHEN '1'  THEN '4'
					  WHEN '2'  THEN '5' ELSE '1' END AS [ORIGEN DE LA ATENCION],
   '1' AS [PRIORIDAD DE LA ATENCION],
   IIF(HIS.GENCONEXT=1,'1',IIF(ING.IINGREPOR=4,'5','2')) AS [TIPO SERVICIO SOLICITADO],
    CASE UNI.UFUTIPUNI WHEN 1  THEN '2' 
					   WHEN 15 THEN '1' 
					   WHEN 24 THEN '1' 
					   WHEN 3  THEN '1' 
					   WHEN 4  THEN '1'
					   WHEN 20 THEN '1' 
					   WHEN 30 THEN '1' ELSE '3' END AS [UBICACION DEL PACIENTE],
    HIS.CODDIAGNO AS [CODIGO DIAGNOSTICO],
	CASE DIA.TIPDIAGNO WHEN 'I' THEN '102'
					   WHEN 'C' THEN '100'
					   WHEN 'R' THEN '101' ELSE '100' END AS [TIPO DIAGNOSTICO],
   CASE WHEN DIA.CODDIAPRI='1' THEN '1' ELSE '0' END [DIAGNOTICO PRINCIPAL],
   '1' AS [TIPO TECNOLOGIA],
   SER.CODSERIPS AS [CODIGO TECNOLOGIA],
   B.DESSERIPS AS [NOMBRE SERVICIO],
   SER.CANSERIPS AS [CANTIDAD],
   '1' AS [DURACION],
   '' AS [CODIGO SERVICIO],
   '0' AS [DOSIS],
   '0' AS [FRECUENCIA],
   '0' AS [TIPO FRECUENCIA],
   '' AS [VIA ADMINISTRACION],
   CAST(HIS.FECHISPAC AS DATE) AS [FECHA ATENCION],
   SUBSTRING(ISNULL(URG.ANALISISP, ISNULL(EVOR.ANALISISP, EVO.ANALISISP)), 0, 3000) AS [JUSTIFICACION CLINICA], 
   CAST(HIS.FECHISPAC AS date) AS [FECHA BUSQUEDA], 
   YEAR(HIS.FECHISPAC) AS 'AÑO FECHA BUSQUEDA',
   MONTH(HIS.FECHISPAC) AS 'MES AÑO FECHA BUSQUEDA',
   CONCAT(FORMAT(MONTH(HIS.FECHISPAC), '00') ,' - ', 
     	  CASE MONTH(HIS.FECHISPAC) 
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
		   WHEN 12 THEN 'DICIEMBRE' END) AS 'MES NOMBRE FECHA BUSQUEDA',
   FORMAT(DAY(HIS.FECHISPAC), '00') AS 'DIA FECHA BUSQUEDA',
   CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
 FROM
	CTE_SERVICIOS SER INNER JOIN
	dbo.ADINGRESO ING ON SER.NUMINGRES=ING.NUMINGRES INNER JOIN
	dbo.HCHISPACA HIS ON ING.NUMINGRES=HIS.NUMINGRES AND SER.NUMEFOLIO=HIS.NUMEFOLIO INNER JOIN
	dbo.INPACIENT PAC ON ING.IPCODPACI=PAC.IPCODPACI INNER JOIN
	dbo.INUNIFUNC UNI ON HIS.UFUCODIGO=UNI.UFUCODIGO INNER JOIN
	Security.Person PER ON HIS.CODPROSAL=PER.Identification INNER JOIN
	Contract.HealthAdministrator EA ON ING.CODENTIDA=EA.Code INNER JOIN
	dbo.ADCENATEN AS CEN ON ING.CODCENATE = CEN.CODCENATE INNER JOIN
	dbo.INCUPSIPS B ON SER.CODSERIPS=B.CODSERIPS LEFT JOIN
	dbo.INDIAGNOH AS DIA ON DIA.CODDIAGNO=HIS.CODDIAGNO AND DIA.NUMINGRES=HIS.NUMINGRES AND HIS.NUMEFOLIO=DIA.NUMEFOLIO AND DIA.CODDIAPRI=1 LEFT JOIN
	dbo.HCURGING1 AS URG ON URG.IDETIPHIS=HIS.IDETIPHIS AND URG.NUMINGRES=HIS.NUMINGRES AND HIS.NUMEFOLIO=URG.NUMEFOLIO LEFT JOIN
	dbo.HCURGEVO1 AS EVO ON EVO.IDETIPHIS=HIS.IDETIPHIS AND EVO.NUMINGRES=HIS.NUMINGRES AND HIS.NUMEFOLIO=EVO.NUMEFOLIO LEFT JOIN
	dbo.HCNOTEVO1 AS EVOR ON EVOR.IDETIPHIS=HIS.IDETIPHIS AND EVOR.NUMINGRES=HIS.NUMINGRES AND HIS.NUMEFOLIO=EVOR.NUMEFOLIO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para el sistema SIAU que consolida todas las órdenes médicas extramurales (imágenes diagnósticas, interconsultas, laboratorios, patologías, procedimientos quirúrgicos y no quirúrgicos) generadas en historia clínica. Cruza datos del ingreso, paciente, profesional, entidad administradora, diagnóstico CIE-10 y justificación clínica para producir un informe plano orientado a reporting regulatorio, con campos de fecha desagregados y consecutivo por atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte SIAU las solicitudes de servicios (imágenes, laboratorios, interconsultas, patologías, procedimientos y consultas externas) ordenadas extramuralmente, con datos clínicos, administrativos y demográficos asociados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de servicios deben tener marca extramural MANEXTPRO=''1'' para ser incluidas (excepto las provenientes de HCDESCOEX/HCHISPACA por consulta externa).; Cada servicio debe estar vinculado a un ingreso (ADINGRESO), una historia clínica (HCHISPACA), un paciente (INPACIENT), una unidad funcional (INUNIFUNC), un profesional registrado en Security.Person, una administradora (Contract.HealthAdministrator), un centro de atención (ADCENATEN) y un código CUPS/IPS (INCUPSIPS).; El diagnóstico se considera principal solo si DIA.CODDIAPRI=1.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La prioridad de atención y el tipo de tecnología siempre se reportan como ''1'' (constantes).; Duración se reporta como ''1'' y dosis/frecuencia/tipo frecuencia como ''0'' (constantes; no se calculan).; El campo ID_COMPANY se obtiene siempre de DB_NAME() truncado a 9 caracteres.; ULT_ACTUAL siempre refleja la hora actual convertida a la zona horaria ''Pakistan Standard Time''.; Las consultas externas (HCDESCOEX) siempre se incluyen con cantidad fija ''1'' y sin observación.; Solo se considera un diagnóstico por historia: el principal (CODDIAPRI=1), por el JOIN LEFT a INDIAGNOH.; El consecutivo se asigna por ROW_NUMBER ordenado por IPCODPACI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitudes extramurales (SIAU); Órdenes médicas (imágenes, laboratorios, interconsultas, patologías, procedimientos no quirúrgicos y quirúrgicos); Consulta externa; Ingreso/admisión de paciente; Historia clínica; Diagnóstico CIE-10 (principal/impresión/confirmado/relacionado); Entidad administradora de salud (EPS/ARS); Centro de atención y código de habilitación; Unidad funcional; Profesional de salud y especialidad; Tipo de documento de identificación (paciente y profesional); Origen de atención; Ubicación del paciente (ámbito ambulatorio/hospitalario); Justificación clínica (notas de urgencias y evolución); Código CUPS/IPS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewReporteSIAU: Devuelve una fila por cada servicio extramural (UNION ALL de HCORDIMAG, HCORDINTE, HCORDLABO, HCORDPATO, HCORDPRON, HCORDPROQ con MANEXTPRO=''1'', más consultas externas vía HCDESCOEX⨝HCHISPACA) cruzada con sus datos administrativos, clínicos y de paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del servicio en CTE_SERVICIOS → Para HCORDIMAG/HCORDINTE/HCORDLABO/HCORDPATO/HCORDPRON/HCORDPROQ se filtra por MANEXTPRO=''1''; para HCDESCOEX se asume cantidad ''1'' y observación vacía, tomando FECHISPAC de HCHISPACA como fecha de orden.; si PAC.IPTIPODOC → Se mapea a sigla de tipo documento (1→CC, 2→CE, 3→TI, 4→RC, 5→PA, 6→AS, 7→MS, 8→NU, 9→NV, 10→CD, 11→SC, 12→PE, 13→PT, 14→DE, 15→SI).; si HIS.GENCONEXT=1 → Ámbito=''A'' (ambulatorio) y TIPO SERVICIO SOLICITADO=''1''. else Ámbito=''H'' (hospitalario); si ING.IINGREPOR=4 entonces TIPO SERVICIO=''5'', de lo contrario ''2''.; si PER.IdentificationType del profesional → 1→CC, 2→CE, cualquier otro→PA.; si ING.ICAUSAING (causa de ingreso) → Se traduce a Origen de Atención: 3→1, 10→2, 6→3, 7→4, 1→4, 2→5; cualquier otro valor→1.; si UNI.UFUTIPUNI (tipo de unidad funcional) → Ubicación del paciente: 1→2; 15,24,3,4,20,30→1; cualquier otro→3.; si DIA.TIPDIAGNO → Tipo diagnóstico: ''I''→102 (impresión), ''C''→100 (confirmado), ''R''→101 (relacionado); cualquier otro o NULL→100.; si DIA.CODDIAPRI=''1'' → Diagnóstico principal=1. else Diagnóstico principal=0.; si Disponibilidad de justificación clínica → Se toma URG.ANALISISP; si es NULL, EVOR.ANALISISP; si también NULL, EVO.ANALISISP, truncado a 3000 caracteres.; si MONTH(HIS.FECHISPAC) → Se convierte el número de mes al nombre en español (ENERO..DICIEMBRE) concatenado al número formateado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.HCORDINTE; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCDESCOEX; dbo.HCHISPACA; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUNIFUNC; Security.Person; Contract.HealthAdministrator; dbo.ADCENATEN; dbo.INCUPSIPS; dbo.INDIAGNOH; dbo.HCURGING1; dbo.HCURGEVO1; dbo.HCNOTEVO1', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSIAU';
GO
