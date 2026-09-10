

CREATE VIEW [Report].[ViewMorbilidad] as 

WITH CTE_DATOSHIS_UNICOS as
 (
  SELECT
   HIS.IPCODPACI,
   MAX(HIS.FECHISPAC) [FECHA_ATENCION],
   (SELECT TOP 1 NUMINGRES FROM HCHISPACA H WHERE H.IPCODPACI = HIS.IPCODPACI AND H.FECHISPAC = MAX(HIS.FECHISPAC)) NUMINGRES
  FROM 
   HCHISPACA HIS
  WHERE
   HIS.GENCONEXT IN (1, 0) AND
   HIS.IPCODPACI <> '000000000000000'
  GROUP BY
   HIS.IPCODPACI
)

SELECT --COUNT(*)
   CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
   CASE PAC.IPTIPODOC 
      WHEN 1  THEN	'CC - CEDULA DE CIUDADANIA' 
	  WHEN 2  THEN	'CE - CEDULA DE EXTRANJERIA' 
	  WHEN 3  THEN	'TI - TARJETA DE IDENTIDAD' 
	  WHEN 4  THEN	'RC - REGISTRO CIVIL' 
	  WHEN 5  THEN	'PA - PASAPORTE' 
	  WHEN 6  THEN	'AS - ADULTO SIN IDENTIFICACION' 
	  WHEN 7  THEN	'MS - MENOR SIN IDENTIFICACION' 
	  WHEN 8  THEN	'NU - NUMERO UNICO DE IDENTIFICACIÒN' 
	  WHEN 9  THEN	'NV - CERTIFICADO NACIDO VIVO' 
	  WHEN 10 THEN	'CD - CARNET DIPLOMATICO' 
	  WHEN 11 THEN	'SC - SALVOCONDUCTO' 
	  WHEN 12 THEN	'PE - PERMISO ESPECIAL DE PERMANENCIA' ELSE 'NO REGISTRA'
	  END 'TIPO IDENTIFICACION',
   HIS.IPCODPACI 'ID. PACIENTE',
   PAC.IPNOMCOMP 'NOMBRE PACIENTE',
   DATEDIFF(YEAR,PAC.IPFECNACI,  CAST(CTE.[FECHA_ATENCION] AS DATE)) 'EDAD AÑOS', 
   DATEDIFF(MONTH,PAC.IPFECNACI, CAST(CTE.[FECHA_ATENCION] AS DATE)) 'EDAD MESES',
   CASE WHEN PAC.IPSEXOPAC =1 THEN 'M' ELSE 'F' END 'SEXO',
   DEP.nomdepart 'DEPARTAMENTO', 
   MUN.MUNNOMBRE 'MUNICIPIO',
   HA.Code 'COD. EAPB', 
   HA.Name 'NOMBRE EAPB',
   CASE WHEN HIS.GENCONEXT = 1 THEN 'Ambulatorio' ELSE 'Hospitalario' END 'AMBITO', 
   HIS.NUMINGRES AS 'INGRESO', 
   CAST(CTE.[FECHA_ATENCION] AS DATE) 'FECHA ATENCION',
   HIS.UFUCODIGO 'COD. UF', 
   FUN.UFUDESCRI 'UNIDAD FUNCIONAL',
   PRO.NOMMEDICO AS 'NOMBRE PROFESIONAL',
   ESP.DESESPECI AS 'ESPECIALIDAD TRATANTE', 
   TI1.CODDIAGNO 'COD. DX CIE-10', 
   TI1.NOMDIAGNO 'DX CIE-10', 
   TI2.CODDIAGNO 'COD CIE-10 RELACIONADO', 
   TI2.NOMDIAGNO 'DX CIE-10 RELACIONADO',
   CASE HIS.INDICAPAC WHEN '11' THEN 'SI' ELSE 'NO' END 'MORTALIDAD',
   CASE WHEN DATEDIFF(MONTH,PAC.IPFECNACI,CTE.[FECHA_ATENCION]) <12 THEN 1 ELSE 0 END '< 1 AÑO',
   CASE WHEN DATEDIFF(MONTH,PAC.IPFECNACI,CTE.[FECHA_ATENCION]) BETWEEN 12 AND 23 THEN 1 ELSE 0 END '1 AÑO',
   CASE WHEN DATEDIFF(MONTH,PAC.IPFECNACI,CTE.[FECHA_ATENCION]) BETWEEN 24 AND 59 THEN 1 ELSE 0 END '2 a 4 AÑOS',
   CASE WHEN DATEDIFF(MONTH,PAC.IPFECNACI,CTE.[FECHA_ATENCION]) BETWEEN 60 AND 239 THEN 1 ELSE 0 END '5 A 19 AÑOS',
   CASE WHEN DATEDIFF(MONTH,PAC.IPFECNACI,CTE.[FECHA_ATENCION]) BETWEEN 240 AND 479 THEN 1 ELSE 0 END '20 A 39 AÑOS',
   CASE WHEN DATEDIFF(MONTH,PAC.IPFECNACI,CTE.[FECHA_ATENCION]) BETWEEN 480 AND 719 THEN 1 ELSE 0 END '40 A 59 AÑOS',
   CASE WHEN DATEDIFF(MONTH,PAC.IPFECNACI,CTE.[FECHA_ATENCION]) > 719 THEN 1 ELSE 0 END '>60 AÑOS',
   /*FAC.CUPS AS 'COD. CUPS', 
   FAC.DESCRIPCION AS 'DESCRIPCION CUPS', */
   1 as 'CANTIDAD',
   CAST(CTE.[FECHA_ATENCION] AS DATE) AS 'FECHA BUSQUEDA',
   YEAR(CTE.[FECHA_ATENCION]) AS 'AÑO FECHA BUSQUEDA',
   MONTH(CTE.[FECHA_ATENCION]) AS 'MES AÑO FECHA BUSQUEDA',
   CASE MONTH(CTE.[FECHA_ATENCION]) 
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
		WHEN 12 THEN 'DICIEMBRE'
   END AS 'MES NOMBRE FECHA BUSQUEDA', 
   FORMAT(DAY(CTE.[FECHA_ATENCION]), '00') AS 'DIA FECHA BUSQUEDA',
   CONCAT(FORMAT(MONTH(CTE.[FECHA_ATENCION]), '00') ,' - ', 
         CASE MONTH(CTE.[FECHA_ATENCION]) 
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
			WHEN 12 THEN 'DICIEMBRE'
		END) MES_LABEL_BUSQUEDA,
   CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
  FROM 
   CTE_DATOSHIS_UNICOS CTE --14.638
   INNER JOIN HCHISPACA HIS ON CTE.IPCODPACI = HIS.IPCODPACI AND CTE.[FECHA_ATENCION] = HIS.FECHISPAC --14.638
   INNER JOIN INPACIENT PAC ON PAC.IPCODPACI=HIS.IPCODPACI --14.638
   INNER JOIN INPROFSAL AS PRO  ON PRO.CODPROSAL=HIS.CODPROSAL
   INNER JOIN INESPECIA AS ESP  ON ESP.CODESPECI=HIS.CODESPTRA
   INNER JOIN INUBICACI AS UB ON UB.AUUBICACI = PAC.AUUBICACI
   INNER JOIN INMUNICIP AS MUN ON MUN.DEPMUNCOD = UB.DEPMUNCOD
   INNER JOIN INDEPARTA AS DEP ON DEP.depcodigo=MUN.DEPCODIGO
   INNER JOIN Contract.HealthAdministrator HA ON HA.Code=PAC.CODENTIDA  --14.639
   INNER JOIN DBO.INUNIFUNC FUN ON FUN.UFUCODIGO = HIS.UFUCODIGO --14.640
   INNER JOIN INDIAGNOP DIAGT1 ON DIAGT1.NUMINGRES = HIS.NUMINGRES AND DIAGT1.IPCODPACI = HIS.IPCODPACI AND DIAGT1.NUMEFOLIO = HIS.NUMEFOLIO  AND DIAGT1.CODDIAPRI = 1 --14-547
   INNER JOIN INDIAGNOS TI1 ON TI1.CODDIAGNO = DIAGT1.CODDIAGNO --14.547
   LEFT JOIN INDIAGNOP DIAGT2 ON DIAGT2.NUMINGRES = HIS.NUMINGRES AND DIAGT2.IPCODPACI = HIS.IPCODPACI AND DIAGT2.NUMEFOLIO = HIS.NUMEFOLIO  AND DIAGT2.CODDIAPRI = 0
   LEFT JOIN INDIAGNOS TI2 ON DIAGT2.CODDIAGNO = TI2.CODDIAGNO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting para análisis de morbilidad institucional. Consolida la última atención por paciente (ambulatoria u hospitalaria) cruzando historia clínica, datos demográficos, ubicación geográfica, EAPB/EPS contratante, unidad funcional, profesional, especialidad y diagnósticos CIE-10 principal y relacionado. Genera bandas de edad por grupos etarios, indicador de mortalidad y desagregación temporal (año, mes, día) para facilitar el consumo en herramientas de Business Intelligence o reportes epidemiológicos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte de morbilidad que consolida la última atención por paciente con su diagnóstico principal y relacionado (CIE-10), datos demográficos, ámbito y agrupaciones por rangos de edad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en HCHISPACA con GENCONEXT IN (1,0) e IPCODPACI distinto de ''000000000000000''; Cada paciente debe tener un diagnóstico principal (CODDIAPRI=1) en INDIAGNOP para aparecer en el resultado; El paciente debe estar afiliado a una HealthAdministrator existente (Code = PAC.CODENTIDA); El paciente debe tener ubicación válida (AUUBICACI) con municipio y departamento referenciables', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye siempre pacientes con IPCODPACI = ''000000000000000''; Solo considera atenciones con GENCONEXT IN (1,0) (ambulatorio u hospitalario); Por cada paciente se selecciona únicamente la fecha máxima de atención (MAX(FECHISPAC)) y el NUMINGRES asociado a esa fecha; Solo aparecen pacientes con diagnóstico principal definido (CODDIAPRI=1) por el INNER JOIN a INDIAGNOP; La columna CANTIDAD siempre vale 1 (conteo unitario por fila); ULT_ACTUAL se calcula con GETDATE() convertido a zona ''Pakistan Standard Time''; ID_COMPANY se obtiene de DB_NAME() truncado a VARCHAR(9)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Morbilidad; Paciente; Diagnóstico CIE-10 principal y relacionado; EAPB / Administradora de salud; Unidad funcional; Especialidad tratante; Profesional de salud; Ámbito de atención (ambulatorio/hospitalario); Mortalidad hospitalaria; Tipo de documento de identificación; Ubicación geográfica (departamento/municipio); Grupos etarios', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewMorbilidad: Devuelve una fila por paciente y diagnóstico principal con su última fecha de atención (MAX(FECHISPAC)); si tiene varios diagnósticos relacionados (CODDIAPRI=0) se multiplican filas por LEFT JOIN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HIS.GENCONEXT = 1 → Se etiqueta el ámbito como ''Ambulatorio'' else Se etiqueta como ''Hospitalario'' (cuando GENCONEXT = 0); si HIS.INDICAPAC = ''11'' → Marca MORTALIDAD = ''SI'' else MORTALIDAD = ''NO''; si PAC.IPSEXOPAC = 1 → Sexo = ''M'' else Sexo = ''F''; si DIAGT1.CODDIAPRI = 1 (INNER JOIN) → Trae el diagnóstico principal CIE-10 (obligatorio); si DIAGT2.CODDIAPRI = 0 (LEFT JOIN) → Trae diagnóstico relacionado opcional else Si no existe, columnas relacionadas quedan en NULL; si DATEDIFF(MONTH, IPFECNACI, FECHA_ATENCION) según rangos: <12, 12-23, 24-59, 60-239, 240-479, 480-719, >719 → Marca con 1 la columna del rango etario correspondiente (<1 año, 1 año, 2-4, 5-19, 20-39, 40-59, >60) else 0 en las demás; si PAC.IPTIPODOC entre 1 y 12 → Mapea a etiqueta CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE else ''NO REGISTRA''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; Contract.HealthAdministrator; dbo.INUNIFUNC; dbo.INDIAGNOP; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMorbilidad';
GO
