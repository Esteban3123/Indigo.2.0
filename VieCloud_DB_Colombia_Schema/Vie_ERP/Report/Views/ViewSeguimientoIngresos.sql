

CREATE VIEW [Report].[ViewSeguimientoIngresos] AS
SELECT  
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 CASE PAC.IPTIPODOC 	
  WHEN '1' THEN 'CC' WHEN '2' THEN 'CE' WHEN '3' THEN 'TI' WHEN '4' THEN 'RC'	WHEN '5' THEN 'PA'	WHEN '6' THEN 'AS'
 WHEN '7' THEN 'MS'	WHEN '8' THEN 'NU'	WHEN '9' THEN 'CN'	WHEN '10' THEN 'CD'	WHEN '11' THEN 'SC' WHEN '12' THEN 'PE' 
 ELSE 'N/A'END 'TIPO DOCUMENTO',ING.IPCODPACI 'ID PACIENTE' ,PAC.IPNOMCOMP 'NOMBRE PACIENTE',PAC.IPTELEFON 'TEL. FIJO', PAC.IPTELMOVI 'TEL. CELULAR',
 PAC.CORELEPAC 'CORREO ELECTRONICO',
 PAC.CODIGONIT 'NIT',GRU.NAME 'GRUPO DE ATENCION',HA.NAME 'ENTIDAD EAPB',
 ING.NUMINGRES,CASE WHEN ING.TIPOINGRE='1' THEN 'Ambulatorio' ELSE 'Hospitalario' END 'TIPO DE INGRESO', ING.UFUCODIGO 'COD. UF INGRESO',
 FUN1.UFUDESCRI 'UNIDAD FUNCIONAL DE INGRESO',ING.IFECHAING 'FECHA DE INGRESO', 
 ING.UFUACTPAC 'COD UF EGRESO',FUN2.UFUDESCRI 'UNIDAD FUNCIONAL DE EGRESO',ING.FECREGCRE 'FECHA DE EGRESO',
 ING.IFECHAING 'FECHA DE BUSQUEDA',
 1 as 'CANTIDAD',
 CAST(ING.IFECHAING AS date) AS 'FECHA BUSQUEDA',
 YEAR(ING.IFECHAING) AS 'AÑO BUSQUEDA',
 MONTH(ING.IFECHAING) AS 'MES BUSQUEDA',
 CONCAT(FORMAT(MONTH(ING.IFECHAING), '00') ,' - ', 
        CASE MONTH(ING.IFECHAING) 
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
 ADINGRESO ING
 INNER JOIN INPACIENT PAC ON PAC.IPCODPACI= ING.IPCODPACI
 INNER JOIN INUNIFUNC FUN1 ON FUN1.UFUCODIGO=ING.UFUCODIGO
 INNER JOIN CONTRACT.CareGroup GRU ON GRU.ID=PAC.GENCAREGROUP
 INNER JOIN CONTRACT.HEALTHADMINISTRATOR HA ON HA.ID=PAC.GENCONENTITY
 LEFT JOIN INUNIFUNC FUN2 ON FUN2.UFUCODIGO=ING.UFUACTPAC
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que aplana los ingresos hospitalarios y ambulatorios de pacientes, combinando datos demográficos del paciente (tipo de documento, contacto, NIT), la entidad EAPB y grupo de atención, con las unidades funcionales de ingreso y egreso. Incluye campos derivados de fecha (año, mes con nombre en español) para facilitar el análisis temporal de ingresos por período en herramientas de BI o dashboards de seguimiento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los ingresos de pacientes con datos demográficos, contacto, entidad EAPB, grupo de atención y unidades funcionales de ingreso/egreso, normalizando fechas para análisis temporal.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente del ingreso debe existir en INPACIENT (INNER JOIN por IPCODPACI).; La unidad funcional de ingreso (UFUCODIGO) debe existir en INUNIFUNC (INNER JOIN).; El paciente debe tener un grupo de atención válido en CONTRACT.CareGroup (INNER JOIN GENCAREGROUP).; El paciente debe tener una entidad administradora de salud válida en CONTRACT.HEALTHADMINISTRATOR (INNER JOIN GENCONENTITY).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El ID_COMPANY se deriva siempre del nombre de la base de datos actual (DB_NAME()), truncado a 9 caracteres.; La columna CANTIDAD siempre vale 1 (cada fila representa un ingreso).; ULT_ACTUAL siempre se calcula con la hora actual convertida a zona horaria ''Pakistan Standard Time''.; Solo se incluyen ingresos cuyo paciente tenga grupo de atención y entidad EAPB asignados (INNER JOIN).; Los tipos de ingreso se reducen a dos categorías: Ambulatorio (TIPOINGRE=''1'') u Hospitalario (cualquier otro valor).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Ingreso ambulatorio; Unidad funcional de ingreso; Unidad funcional de egreso; Grupo de atención; Entidad EAPB (administradora de salud); Tipo de documento de identidad; NIT', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewSeguimientoIngresos: Devuelve una fila por cada ingreso (ADINGRESO) que tenga paciente, unidad funcional de ingreso, grupo de atención y entidad EAPB válidos; la unidad funcional de egreso es opcional (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPTIPODOC IN (''1''..''12'') → Mapea el código numérico al código alfabético de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE) else Asigna ''N/A'' como tipo de documento; si ING.TIPOINGRE=''1'' → Clasifica el ingreso como ''Ambulatorio'' else Clasifica el ingreso como ''Hospitalario''; si MONTH(ING.IFECHAING) entre 1 y 12 → Concatena el número de mes con su nombre en español (ENERO..DICIEMBRE) para la dimensión ''MES NOMBRE BUSQUEDA''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INUNIFUNC; CONTRACT.CareGroup; CONTRACT.HEALTHADMINISTRATOR', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewSeguimientoIngresos';
GO
