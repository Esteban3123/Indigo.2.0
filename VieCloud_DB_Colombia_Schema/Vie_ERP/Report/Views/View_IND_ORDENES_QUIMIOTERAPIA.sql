

CREATE VIEW [Report].[View_IND_ORDENES_QUIMIOTERAPIA]
AS

WITH CTE_ORDENS_QUIMIO
AS
(
  SELECT CAST(QUI.FECHAREGISTRO AS DATE) 'FECHA ORDEN',CAST(ISNULL(QUI.FECHAFINALIZA,ISNULL(QUI.FECHAANULACION,QUI.FECHAREGISTRO )) AS DATE) 'FECHA ANULACION',
  ESQ.Code 'CODIGO ESQUEMA' ,ESQ.Description 'ESQUEMA', QUI.IPCODPACI IDENTIFICACION,PAC.IPNOMCOMP 'PACIENTE', QUI.NUMINGRES 'INGRESO',
  CASE QUI.ESTADO WHEN 1 THEN 'Orden Solicitada' WHEN 2 THEN 'Esquema iniciado' WHEN 3 THEN 'Esquema finalizado completo'
  WHEN 4 THEN 'Suspendido - Finalización prematura' WHEN 5 THEN 'Anulado' END 'ESTADO',
  IIF(MOTIVOFINALIZAR IS NOT NULL,CASE MOTIVOFINALIZAR WHEN 1 THEN 'TOXICIDAD' WHEN 2 THEN 'MOTIVO MEDICO' WHEN 3 THEN 'MUERTE DEL PACIENTE' WHEN 4 THEN 'CAMBIO EPS'
       WHEN 5 THEN 'USUARIO' WHEN 6 THEN 'NO DISPONIBILIDAD MEDICAMENTO' WHEN 7 THEN 'ADMINISTRATIVOS' WHEN 8 THEN 'OTROS MOTIVOS' ELSE 'OTROS MOTIVOS' END,
	     IIF(QUI.IDHCMOANULB IS NOT NULL,ANU1.DESMOTANU,ANU2.DESMOTANU)) 'MOTIVO DE ANULACION/SUSPENSION',QUI.[46] 'NRO FASES',CASE FASEQUIMIOTERAPIA 
		 WHEN '1' THEN 'Prefase o citorreducción inicial' WHEN '2' THEN 'Inducción' WHEN '3' THEN 'Intensificación' WHEN '4' THEN 'Consolidación'
         WHEN '5' THEN 'Reinducción' WHEN '6' THEN 'Mantenimiento' WHEN '7' THEN 'Mantenimiento largo o final' WHEN '8' THEN 'Otra Fase' END 'FASE ACTUAL',
		 CASE QUI.[48]  WHEN '1' THEN 'Neoadyuvancia (manejo  inicial prequirúrgico)'WHEN '2' THEN 'Tratamiento inicial curativo sin cirugía sugerida'
        WHEN '3' THEN 'Adyuvancia(manejo inicial postquirúrgico)'WHEN '4' THEN 'Manejo paliativo inicial'WHEN '5' THEN 'Manejo curativo de primera recaída'
        WHEN '6' THEN 'Manejo paliativo de primera recaída'WHEN '7' THEN 'Manejo curativo de segunda recaída'WHEN '8' THEN 'Manejo paliativo de segunda recaída'
        WHEN '9' THEN 'Manejo curativo de tercera recaída o posterior'WHEN '10' THEN 'Manejo paliativo de tercera recaída o posterior' END  'UBICACION TEMPORAL',
		QUI.CODDIAGNO 'CIE10', DIA.NOMDIAGNO 'DIAGNOSTICO',FUN.UFUCODIGO 'CODIGO UNIDAD FUNCIONAL',FUN.UFUDESCRI 'UNIDAD FUNCIONAL',HA.Name 'ENTIDAD',
		CG.Code 'CODIGO GRUPO ATENCION', CG.Name 'GRUPO DE ATENCION',CASE HA.EntityType WHEN 1  THEN 'EPS Contributivo'
WHEN '2' THEN 'EPS Subsidiado'WHEN '3' THEN 'ET Vinculados Municipios'WHEN '4' THEN 'ET Vinculados Departamentos'WHEN '5' THEN 'ARL Riesgos Laborales'
WHEN '6' THEN 'MP Medicina Prepagada'WHEN '7' THEN 'IPS Privada'WHEN '8' THEN 'IPS Publica'WHEN '9' THEN 'Regimen Especial'WHEN '10'THEN 'Accidentes de transito'
WHEN '11'THEN 'Fosyga'WHEN '12'THEN 'Otros' END 'REGIMEN'
  FROM EHR.HCORDQUIMIO QUI
  INNER JOIN EHR.Schemes ESQ ON ESQ.Id=QUI.SchemesId
  INNER JOIN DBO.INPACIENT AS PAC ON PAC.IPCODPACI =QUI.IPCODPACI
  INNER JOIN DBO.ADINGRESO AS ING ON QUI.NUMINGRES =ING.NUMINGRES 
  INNER JOIN Contract .HealthAdministrator AS HA ON HA.Id =ING.GENCONENTITY
  INNER JOIN Contract .CareGroup CG ON CG.Id =ING.GENCAREGROUP 
  LEFT JOIN HCMOANULB ANU1 ON ANU1.CODMOTANU=QUI.IDHCMOANULB
  LEFT JOIN HCMOANULB ANU2 ON ANU2.CODMOTANU=QUI.IDHCMOANULB_SUSP 
  LEFT JOIN DBO.INDIAGNOS AS DIA ON DIA.CODDIAGNO =QUI.CODDIAGNO 
  LEFT JOIN DBO.INUNIFUNC AS FUN ON FUN.UFUCODIGO =QUI.UFUCODIGO 
)

SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, *,
 1 as 'CANTIDAD',
 CAST([FECHA ORDEN] AS date) AS 'FECHA BUSQUEDA',
 YEAR([FECHA ORDEN]) AS 'AÑO BUSQUEDA',
 MONTH([FECHA ORDEN]) AS 'MES BUSQUEDA',
 CONCAT(FORMAT(MONTH([FECHA ORDEN]), '00') ,' - ', 
	   CASE MONTH([FECHA ORDEN]) 
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
 
FROM CTE_ORDENS_QUIMIO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que aplana las órdenes de quimioterapia registradas en el sistema EHR, combinando datos del esquema terapéutico, paciente, ingreso, entidad aseguradora, grupo de atención, diagnóstico CIE-10 y unidad funcional. Expone el estado de cada orden, la fase del ciclo, la ubicación temporal del tratamiento y el motivo de anulación o suspensión. Agrega columnas de apoyo para análisis por fecha (año, mes, nombre del mes) y marca la empresa con el nombre de la base de datos activa, orientada a indicadores de gestión oncológica.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida las órdenes de quimioterapia con datos del paciente, esquema, diagnóstico, unidad funcional, entidad responsable y régimen, agregando dimensiones temporales para indicadores.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de quimioterapia debe tener un esquema válido en EHR.Schemes (INNER JOIN); El paciente referenciado debe existir en DBO.INPACIENT (INNER JOIN); El ingreso (NUMINGRES) debe existir en DBO.ADINGRESO (INNER JOIN); El ingreso debe tener entidad de salud (GENCONENTITY) y grupo de atención (GENCAREGROUP) válidos en Contract.HealthAdministrator y Contract.CareGroup (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'FECHA ORDEN siempre se deriva de QUI.FECHAREGISTRO truncado a DATE; FECHA ANULACION usa COALESCE: FECHAFINALIZA → FECHAANULACION → FECHAREGISTRO (truncado a DATE), nunca queda nula; CANTIDAD siempre vale 1 (cada fila representa una orden); ID_COMPANY corresponde al nombre de la base de datos truncado a 9 caracteres; ULT_ACTUAL refleja la hora actual convertida a zona horaria ''Pakistan Standard Time''; Solo aparecen órdenes cuyo paciente, ingreso, entidad y grupo de atención están registrados (INNER JOIN obligatorios); FECHA BUSQUEDA = FECHA ORDEN (ambas derivadas de FECHAREGISTRO)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de quimioterapia; Esquema de tratamiento oncológico; Paciente; Ingreso hospitalario; Diagnóstico CIE-10; Unidad funcional; Entidad responsable de pago (EPS/ARL/MP/IPS); Grupo de atención; Régimen de afiliación; Fase de quimioterapia (Inducción, Consolidación, Mantenimiento, etc.); Ubicación temporal del tratamiento (Neoadyuvancia/Adyuvancia/Paliativo/Curativo); Motivo de finalización/anulación/suspensión; Toxicidad; Muerte del paciente; Cambio de EPS; Fosyga', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.View_IND_ORDENES_QUIMIOTERAPIA: Devuelve una fila por orden de quimioterapia uniendo HCORDQUIMIO con esquema, paciente, ingreso, entidad, grupo, diagnóstico y unidad funcional, agregando ID_COMPANY=DB_NAME() y ULT_ACTUAL en zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si QUI.ESTADO IN (1..5) → Traduce a textos: 1=Orden Solicitada, 2=Esquema iniciado, 3=Esquema finalizado completo, 4=Suspendido - Finalización prematura, 5=Anulado; si MOTIVOFINALIZAR IS NOT NULL → Mapea código de motivo de finalización (1=TOXICIDAD, 2=MOTIVO MEDICO, 3=MUERTE DEL PACIENTE, 4=CAMBIO EPS, 5=USUARIO, 6=NO DISPONIBILIDAD MEDICAMENTO, 7=ADMINISTRATIVOS, 8/otros=OTROS MOTIVOS) else Si QUI.IDHCMOANULB IS NOT NULL toma descripción de ANU1 (HCMOANULB por IDHCMOANULB), de lo contrario toma ANU2 (por IDHCMOANULB_SUSP); si FASEQUIMIOTERAPIA entre ''1'' y ''8'' → Traduce la fase del tratamiento (Prefase, Inducción, Intensificación, Consolidación, Reinducción, Mantenimiento, Mantenimiento largo o final, Otra Fase); si QUI.[48] entre ''1'' y ''10'' → Traduce ubicación temporal del tratamiento (Neoadyuvancia, Tratamiento curativo sin cirugía, Adyuvancia, Manejos paliativos/curativos por recaídas); si HA.EntityType entre 1 y 12 → Clasifica el régimen de la entidad (EPS Contributivo/Subsidiado, ET Municipios/Departamentos, ARL, MP, IPS Privada/Pública, Régimen Especial, Accidentes de tránsito, Fosyga, Otros); si MONTH([FECHA ORDEN]) entre 1 y 12 → Genera ''MES NOMBRE BUSQUEDA'' concatenando número con cero a la izquierda y nombre en español del mes', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDQUIMIO; EHR.Schemes; DBO.INPACIENT; DBO.ADINGRESO; Contract.HealthAdministrator; Contract.CareGroup; DBO.HCMOANULB; DBO.INDIAGNOS; DBO.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_IND_ORDENES_QUIMIOTERAPIA';
GO
