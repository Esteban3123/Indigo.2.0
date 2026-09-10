

CREATE VIEW [Report].[ViewResolucionTriages] as

WITH CTE_MUERTOS AS
(
  SELECT
   HIS.IPCODPACI, HIS.NUMINGRES 'INGRESO',
   HIS.NUMEFOLIO, HIS.FECHISPAC 'FECHA ATENCION', 
   HIS.INDICAPAC,HIS.ID, EGR.FECMUEPAC 'FECHA MUERTE',
   EGR.UFUCODIGO 'CODIGO UNIDAD FUNCIONAL', FUN.UFUDESCRI 'UNIDAD FUNCIONAL'
  FROM DBO.HCHISPACA HIS
  INNER JOIN DBO.HCREGEGRE AS EGR ON EGR.NUMINGRES =HIS.NUMINGRES AND EGR.NUMEFOLIO =HIS.NUMEFOLIO 
  INNER JOIN DBO.INUNIFUNC AS FUN ON FUN.UFUCODIGO =EGR.UFUCODIGO 
  WHERE HIS.INDICAPAC =11 AND FUN.UFUTIPUNI ='1'
)

SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 CASE U.IPTIPODOC 
  WHEN 1 THEN 'CC'  
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
 END AS [TIPO DOCUMENTO], 
 C.IPCODPACI AS [IDENTIFICACIÓN],
 YEAR(GETDATE()) - YEAR(U.IPFECNACI) AS EDAD, 
 U.IPTELEFON AS TELEFONO, 
 U.IPTELMOVI AS MOVIL,
 CASE C.CODTIPPAC 
  WHEN 1 THEN 'Maternas' 
  WHEN 2 THEN 'Menores de 5 Años' 
  WHEN 3 THEN 'Adultos Mayores' 
  WHEN 4 THEN 'Prepagadas o Particulares' 
  WHEN 5 THEN 'Poblacion General' 
 END AS [TIPO PACIENTE],
 cast(U.IPFECNACI as date) AS [FECHA NACIMIENTO],
 CASE WHEN U.IPSEXOPAC = '1' THEN 'M' WHEN U.IPSEXOPAC = '2' THEN 'F' END AS SEXO, 
 u.IPPRIAPEL AS [PRIMER APELLIDO],
 u.IPSEGAPEL AS [SEGUNDO APELLIDO],
 u.IPPRINOMB AS [PRIMER NOMBRE],
 u.IPSEGNOMB AS [SEGUNDO NOMBRE], 
 F.CODENTIDA AS [CODIGO EAPB],
 F.NOMENTIDA AS [NOMBRE EAPB],
 CASE e.EntityType 
  WHEN '1' THEN 'EPS Contributivo' 
  WHEN '2' THEN 'EPS Subsidiado' 
  WHEN '3' THEN 'ET Vinculados Municipios' 
  WHEN '4' THEN 'ET Vinculados Departamentos' 
  WHEN '5' THEN 'ARL' 
  WHEN '6' THEN 'Prepagada'
  WHEN '7' THEN 'IPS' 
  WHEN '8' THEN 'IPS' 
  WHEN '9' THEN 'Regimen Especial' 
  WHEN '10' THEN 'Accidentes Transito' 
  WHEN '11' THEN 'Fosyga' 
  WHEN '12' THEN 'Otros'
  WHEN '99' THEN 'Particulares' 
 END AS [REGIMEN],
 C.IPFECLLEGA AS [HORA DE INGRESO A ADMISIONES],
 DATEDIFF(minute, C.IPFECLLEGA, A.TRIAFECHA) AS [MINUTOS ESPERA ADMISIONES A TRIAGE],
 CASE 
  WHEN C.CONESTADO = '1' THEN 'Sin Atender' 
  WHEN C.CONESTADO = '2' THEN 'Ausente en Clasificacion TRIAGE' 
  WHEN C.CONESTADO = '3' THEN 'Clasificado sin Ingreso' 
  WHEN C.CONESTADO = '4' THEN 'Clasificado con Ingreso'
  WHEN C.CONESTADO = '5' THEN 'Atendido' 
  WHEN C.CONESTADO = '6' THEN 'Ausente en Atencion Inicial Urgencias' 
  WHEN C.CONESTADO = '7' THEN 'Anulado por error de Parametrizacion' 
  WHEN C.CONESTADO = '8' THEN 'No atendido por Clasificacion sin Autorizacion' 
 END AS [ESTADO PACIENTE], 
 ds.TRIANOMCA AS [CAUSA DE INGRESO],
 cast(A.TRIAFECHA as date) AS [FECHA TRIAGE],
 rtrim(convert(char(8), A.TRIAFECHA, 108)) [HORA TRIAGE], 
 C.OBVAUSENT AS [OBSERVACION AUSENCIA TRIAGE],
 P.NOMMEDICO AS [PROFESIONAL REALIZA TRIAGE], 
 ES.DESESPECI AS [ESPECIALIDAD],
 A.TRIAGECLA AS [CLASIFICACION TRIAGE],
 IIF(A.TRIAFECHA IS NULL,'NO','SI') 'ATENDIDO TRIAGE',
 A.NUMINGRES AS [INGRESO],
 CASE I.IESTADOIN WHEN ' ' THEN 'Abierto' WHEN 'F' THEN 'Facturado' WHEN 'C' THEN 'Cerrado' WHEN 'A' THEN 'Anulado' END AS [ESTADO INGRESO],
 DATEDIFF(minute, A.TRIAFECHA, k.FECHINIHI) AS [MINUTOS ESPERA TRIAGE A CONSULTA],
 cast(k.FECHINIHI as date) AS [FECHA CONSULTA],
 rtrim(convert(char(5), k.FECHINIHI, 108)) AS [HORA CONSULTA], 
 PAIU.NOMMEDICO AS [PROFESIONAL QUE ATIENDE CONSULTA],
 DX.CODDIAGNO AS [CODIGO DIAG INGRESO],
 DX.NOMDIAGNO AS [DIAGNOSTICO INGRESO],
 DXA.CODDIAGNO AS [CODIGO EGRESO],
 DXA.NOMDIAGNO AS [DIAGNOSTICO EGRESO],
 IIF(k.FECHFINH  IS NULL,'NO','SI') 'ATENDIDO CONSULTA',
 k.FECHFINH AS [FECHA HORA FINAL CONSULTA], 
 DATEDIFF(minute, k.FECHINIHI, k.FECHFINH) AS [MINUTOS DE CONSULTA],
 CASE K.INDICAPAC 
  WHEN 1 THEN 'Trasladar a Urgencias'
  WHEN 2 THEN 'Trasladar a Observación Urgencias'
  WHEN 3 THEN 'Trasladar a Hospitalización'
  WHEN 7 THEN 'Trasladar a Consulta Externa'
  WHEN 9 THEN 'Hospitalización en Casa'
  WHEN 10 THEN 'Referencia'
  WHEN 11 THEN 'Morgue'
  WHEN 12 THEN 'Salida'
  WHEN 13 THEN 'Continúa en la Unidad'
  WHEN 15 THEN 'Retiro Voluntario'
  WHEN 16 THEN 'Fuga'
 END [DESTINO PACIENTE], 
 CASE WHEN TRA.MEDIOTRASPORTE = 1 THEN 'Básica' WHEN TRA.MEDIOTRASPORTE = 2 THEN 'Medicalizada' END 'TIPO DE AMBULACIA',
 IIF(MUE.ID IS NULL,'NO','SI') 'FALLECIDO',
 MUE.[FECHA MUERTE]  ,
 IIF(MUE.ID IS NOT NULL AND DATEDIFF (MINUTE,c.IPFECLLEGA, c.IPFECLLEGA) < 1440,'SI','NO' ) 'MORTALIDAD URGENCIAS < 24',
 IIF(MUE.ID IS NOT NULL AND DATEDIFF (MINUTE,c.IPFECLLEGA, c.IPFECLLEGA) > 2880,'SI','NO' ) 'MORTALIDAD URGENCIAS > 48',
 1 as 'CANTIDAD',
 CAST(c.IPFECLLEGA AS date) AS 'FECHA BUSQUEDA',
 CONCAT(FORMAT(MONTH(c.IPFECLLEGA), '00') ,' - ', 
	   CASE MONTH(c.IPFECLLEGA) 
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
		END) 'MES NOMBRE BUSQUEDA',
 YEAR(c.IPFECLLEGA) AS 'AÑO BUSQUEDA',					
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 dbo.ADCONTURG AS C 
 INNER JOIN .dbo.ADTRIAGEU AS A ON A.TRIANUMER = C.CODCONCEC
 INNER JOIN .dbo.INPACIENT AS U ON U.IPCODPACI = C.IPCODPACI 
 INNER JOIN .Contract.CareGroup AS E  ON E.Id = U.GENCAREGROUP
 LEFT JOIN .dbo.INPROFSAL AS P ON P.CODPROSAL = A.CODPROSAL 
 LEFT JOIN .dbo.INPROFSAL AS Pa ON Pa.CODPROSAL = C.PROAUSENT 
 LEFT JOIN .dbo.INESPECIA AS ES ON ES.CODESPECI = P.CODESPEC1 
 LEFT JOIN .dbo.ADCATTRIU AS ds ON ds.TRIACATEG = A.TRIACATEG 
 LEFT JOIN .dbo.HCURGING1 AS k ON k.IPCODPACI = A.IPCODPACI AND k.NUMINGRES = A.NUMINGRES AND k.UFUCODIGO = C.UFUCODIGO 
 LEFT JOIN .dbo.INPROFSAL AS PAIU  ON PAIU.CODPROSAL = k.CODPROSAL 
 LEFT JOIN .dbo.ADINGRESO AS I ON I.NUMINGRES = A.NUMINGRES 
 LEFT JOIN dbo.INENTIDAD AS F  ON A.CODENTIDA = F.CODENTIDA 
 LEFT JOIN .dbo.INDIAGNOS AS DX  ON DX.CODDIAGNO = I.CODDIAING 
 LEFT JOIN .dbo.INDIAGNOS AS DXA  ON DXA.CODDIAGNO =I.CODDIAEGR
 LEFT JOIN (SELECT MAX(AUTO) AUTO, NUMINGRES FROM .dbo.HCREFCONP GROUP BY NUMINGRES) AS OTRA ON OTRA.NUMINGRES=I.NUMINGRES
 LEFT JOIN CTE_MUERTOS AS MUE ON MUE.INGRESO = I.NUMINGRES
 LEFT JOIN .dbo.HCTRASLADOS AS TRA ON TRA.IDHCREFCONP=OTRA.AUTO
 
--where a.TRIAGECLA in (4,5)
--where i.numingres='7497'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada al análisis del proceso de atención en urgencias, desde el registro en admisiones hasta la resolución clínica. Consolida datos demográficos del paciente, aseguradora/régimen, tiempos de espera entre admisiones y triage, clasificación de triage, atención médica inicial y destino final (incluida mortalidad). Permite evaluar indicadores de oportunidad y calidad en urgencias por empresa, mes y año.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la trazabilidad de pacientes en urgencias desde su llegada a admisiones, pasando por triage y consulta de urgencias, hasta su destino final (incluyendo mortalidad y traslado).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en ADCONTURG vinculados a un triage en ADTRIAGEU vía CODCONCEC=TRIANUMER.; El paciente debe existir en INPACIENT y tener un grupo de cuidado en Contract.CareGroup.; Para detectar fallecidos, debe existir egreso en HCREGEGRE con INDICAPAC=11 y unidad funcional con UFUTIPUNI=''1'' (hospitalaria).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El ID_COMPANY se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.; La columna CANTIDAD siempre vale 1 (para conteos agregados en el reporte).; La fecha/hora ULT_ACTUAL se entrega convertida a la zona horaria ''Pakistan Standard Time''.; La edad se calcula como diferencia de años calendario (YEAR(GETDATE())-YEAR(IPFECNACI)), no como edad cumplida exacta.; Solo se incluyen pacientes que tengan al menos un registro de triage (INNER JOIN con ADTRIAGEU).; Para cada ingreso solo se considera la última remisión/traslado (MAX(AUTO) por NUMINGRES en HCREFCONP).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage de urgencias; Clasificación de triage; Admisión de urgencias; Ingreso hospitalario; Diagnóstico de ingreso y egreso; EAPB / aseguradora; Régimen de afiliación; Tipo de paciente (maternas, menores, adultos mayores, prepagada, general); Mortalidad en urgencias (<24h, >48h); Destino del paciente; Traslado en ambulancia (básica/medicalizada); Tiempos de espera (admisión a triage, triage a consulta, duración de consulta); Profesional de salud; Especialidad médica; Causa de ingreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewResolucionTriages: Devuelve un registro por cada contacto de urgencias con su triage asociado (INNER JOIN ADCONTURG-ADTRIAGEU), enriquecido con datos del paciente, EAPB, consulta de urgencias, diagnóstico, traslado y mortalidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HIS.INDICAPAC = 11 y FUN.UFUTIPUNI = ''1'' (CTE_MUERTOS) → El ingreso se considera fallecido en unidad hospitalaria y se cruza con la vista para marcar ''FALLECIDO''=''SI'' y exponer la fecha de muerte.; si MUE.ID IS NOT NULL AND DATEDIFF(MINUTE, c.IPFECLLEGA, c.IPFECLLEGA) < 1440 → Marca ''MORTALIDAD URGENCIAS < 24'' = ''SI'' (nota: la diferencia se calcula sobre la misma columna, por lo que siempre da 0 y la condición <1440 se cumple cuando hay fallecido).; si MUE.ID IS NOT NULL AND DATEDIFF(MINUTE, c.IPFECLLEGA, c.IPFECLLEGA) > 2880 → Marca ''MORTALIDAD URGENCIAS > 48'' = ''SI'' (la diferencia es siempre 0, por lo que la condición >2880 nunca se cumple: posible bug).; si A.TRIAFECHA IS NULL → ''ATENDIDO TRIAGE'' = ''NO'' else ''ATENDIDO TRIAGE'' = ''SI''; si k.FECHFINH IS NULL → ''ATENDIDO CONSULTA'' = ''NO'' else ''ATENDIDO CONSULTA'' = ''SI''; si C.CONESTADO entre 1 y 8 → Traduce el código a estado del paciente (Sin Atender, Ausente Triage, Clasificado, Atendido, Anulado, etc.).; si K.INDICAPAC en {1,2,3,7,9,10,11,12,13,15,16} → Determina el destino del paciente (Urgencias, Observación, Hospitalización, Consulta Externa, Morgue, Salida, Retiro Voluntario, Fuga, etc.).; si I.IESTADOIN en {'' '',''F'',''C'',''A''} → Mapea el estado del ingreso a Abierto/Facturado/Cerrado/Anulado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.HCREGEGRE; dbo.INUNIFUNC; dbo.ADCONTURG; dbo.ADTRIAGEU; dbo.INPACIENT; Contract.CareGroup; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADCATTRIU; dbo.HCURGING1; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.HCREFCONP; dbo.HCTRASLADOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucionTriages';
GO
