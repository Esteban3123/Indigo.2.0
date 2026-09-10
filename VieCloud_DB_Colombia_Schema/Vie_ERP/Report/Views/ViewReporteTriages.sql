

/*******************************************************************************************************************
Nombre: [Report].[ViewReporteTriages]
Tipo:Procedimiento Vista
Observacion:Informe sobre la oportunidad y clasificación del triage.
Profesional:
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:09-06-2023
Observaciones: Se quitan las clasificaciones en blanco o igual a 0
--------------------------------------
Version 3
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:09-06-2023
Observaciones: Se agrega la fecha de egreso o alta medica.
--------------------------------------
Version 4
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:28-06-2023
Observaciones:Se cambia la logica para los campos de codigo EAPb, nombre EAPB y se agrega el centro de atención, segun el ticket 10628
--------------------------------------
Version 5
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:19-06-203
Observaciones: Se agrega la tabla Admissions.TypesPopulationGroups, para agregar el tipo de población esto afecta el campo "TIPO PACIENTE"
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewReporteTriages] as

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

SELECT DISTINCT
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 CEN.NOMCENATE AS [CENTRO DE ATENCIÓN],
 CASE U.IPTIPODOC WHEN 1 THEN 'CC'  
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
				  WHEN 20 THEN 'OT' END AS [TIPO DOCUMENTO], 
 C.IPCODPACI AS [IDENTIFICACIÓN],
 YEAR(GETDATE()) - YEAR(U.IPFECNACI) AS EDAD, 
 U.IPTELEFON AS TELEFONO, 
 U.IPTELMOVI AS MOVIL,
 GRU.NAME AS [TIPO PACIENTE],
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
  WHEN C.CONESTADO = '8' THEN 'No atendido por Clasificacion sin Autorizacion' END AS [ESTADO PACIENTE], 
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
 ISNULL(I.FECHEGRESO,EGR.FECALTPAC) AS [FECHA EGRESO],
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
 FUNEA.UFUCODIGO [COD. UNIDAD DE EGRESO], 
 FUNEA.UFUDESCRI [UNIDAD DE EGRESO],
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
 INNER JOIN dbo.ADTRIAGEU  AS A ON A.TRIANUMER = C.CODCONCEC
 INNER JOIN dbo.INPACIENT AS U ON U.IPCODPACI = C.IPCODPACI 
 INNER JOIN Contract.CareGroup AS E  ON E.Id = U.GENCAREGROUP 
 INNER JOIN dbo.ADCENATEN CEN ON C.CODCENATE=CEN.CODCENATE 
 LEFT JOIN  dbo.INPROFSAL AS P ON P.CODPROSAL = A.CODPROSAL 
 LEFT JOIN  dbo.INPROFSAL AS Pa ON Pa.CODPROSAL = C.PROAUSENT 
 LEFT JOIN  dbo.INESPECIA AS ES ON ES.CODESPECI = P.CODESPEC1 
 LEFT JOIN  dbo.ADCATTRIU AS ds ON ds.TRIACATEG = A.TRIACATEG 
 LEFT JOIN  dbo.HCURGING1 AS k ON k.IPCODPACI = A.IPCODPACI AND k.NUMINGRES = A.NUMINGRES AND k.UFUCODIGO = C.UFUCODIGO 
 LEFT JOIN  dbo.INPROFSAL AS PAIU  ON PAIU.CODPROSAL = k.CODPROSAL 
 LEFT JOIN  dbo.ADINGRESO AS I ON I.NUMINGRES = A.NUMINGRES 
 LEFT JOIN  dbo.INENTIDAD AS F  ON /*IN V4*/ISNULL(C.CODENTIDA,A.CODENTIDA)/*FN V4*/ = F.CODENTIDA 
 LEFT JOIN  dbo.INDIAGNOS AS DX  ON DX.CODDIAGNO = I.CODDIAING 
 LEFT JOIN  dbo.INDIAGNOS AS DXA  ON DXA.CODDIAGNO =I.CODDIAEGR
 LEFT JOIN  (SELECT MAX(AUTO) AUTO, NUMINGRES FROM .dbo.HCREFCONP GROUP BY NUMINGRES) AS OTRA ON OTRA.NUMINGRES=I.NUMINGRES
 LEFT JOIN CTE_MUERTOS AS MUE ON MUE.INGRESO = I.NUMINGRES
 LEFT JOIN  dbo.HCTRASLADOS AS TRA ON TRA.IDHCREFCONP=OTRA.AUTO /*IN V3*/
 LEFT JOIN  DBO.HCREGEGRE EGR ON I.NUMINGRES=EGR.NUMINGRES AND EGR.NUMEFOLIO=(SELECT MAX(E.NUMEFOLIO) FROM DBO.HCREGEGRE E WHERE I.NUMINGRES=E.NUMINGRES) 
 LEFT JOIN  INUNIFUNC AS FUNEA ON EGR.UFUCODIGO = FUNEA.UFUCODIGO    
 LEFT JOIN /*FN V3*/ Admissions.TypesPopulationGroups GRU ON C.CODTIPPAC=GRU.CODE
where  
 (A.TRIAGECLA<=5/*in v2*/ AND A.TRIAGECLA!=0 /*fn v2*/) AND 
 (C.IPFECLLEGA BETWEEN GETDATE()-390 AND GETDATE()) 
 --and u.IPCODPACI='1000135627'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a auditoría de oportunidad y clasificación de triage en urgencias. Consolida, para cada contacto de urgencias de los últimos 390 días, datos demográficos del paciente, entidad aseguradora y régimen, tiempos de espera (admisiones→triage y triage→consulta), clasificación de triage (1-5), estado del paciente, diagnósticos de ingreso y egreso, destino, unidad funcional de egreso, tipo de ambulancia y condición de fallecimiento, incluyendo indicadores de mortalidad en urgencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteTriages';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la información de triage de urgencias, oportunidad (tiempos de espera), clasificación, datos del paciente, profesional, ingreso, egreso, diagnóstico, traslado y mortalidad para reportes operativos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El triage debe tener clasificación válida: TRIAGECLA <= 5 y distinta de 0 (excluye blancos/cero).; La fecha de llegada a admisiones (IPFECLLEGA) debe estar en los últimos 390 días respecto a GETDATE().; Para identificar fallecidos se requiere que en HCHISPACA INDICAPAC=11 (Morgue) y la unidad funcional asociada sea de tipo ''1'' (UFUTIPUNI=''1'').; Cada contacto de urgencias (ADCONTURG) debe tener un triage asociado en ADTRIAGEU vía TRIANUMER = CODCONCEC (INNER JOIN).; El paciente debe existir en INPACIENT y estar asociado a un grupo de cuidado (Contract.CareGroup) y a un centro de atención (ADCENATEN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran fallecidos cuando el destino del paciente en HCHISPACA es Morgue (INDICAPAC=11) y la unidad funcional es de tipo ''1''.; Las clasificaciones de triage en blanco o iguales a 0 nunca aparecen en el reporte.; El reporte siempre devuelve registros con triage existente (INNER JOIN entre ADCONTURG y ADTRIAGEU).; El campo ID_COMPANY siempre corresponde al nombre de la base de datos actual (DB_NAME), truncado a 9 caracteres.; Para el egreso se toma siempre el último folio de HCREGEGRE por NUMINGRES (MAX(NUMEFOLIO)).; Para el traslado se toma el último HCREFCONP por NUMINGRES (MAX(AUTO)).; ULT_ACTUAL siempre se reporta convertido a la zona horaria ''Pakistan Standard Time''.; Los indicadores ''MORTALIDAD URGENCIAS < 24'' y ''> 48'' usan DATEDIFF entre IPFECLLEGA y la misma IPFECLLEGA (siempre 0), por lo que < 24 es siempre ''SI'' y > 48 siempre ''NO'' cuando existe registro de muerte (posible defecto de implementación).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewReporteTriages: Devuelve filas DISTINCT con el reporte de triages dentro de la ventana de los últimos 390 días y con clasificación 1..5 (excluye 0).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.TRIAGECLA <= 5 AND A.TRIAGECLA != 0 → Se incluye el registro en el reporte (excluye clasificaciones en blanco o iguales a 0, según versión 2).; si C.IPFECLLEGA BETWEEN GETDATE()-390 AND GETDATE() → Solo se reportan atenciones con llegada a admisiones en los últimos 390 días.; si ISNULL(C.CODENTIDA, A.CODENTIDA) → Para la EAPB se toma la entidad del contacto de urgencias; si es nula, se usa la del triage (ajuste versión 4).; si MUE.ID IS NOT NULL AND DATEDIFF(MINUTE, c.IPFECLLEGA, c.IPFECLLEGA) < 1440 → Marca ''MORTALIDAD URGENCIAS < 24'' = ''SI''. else ''NO''; si MUE.ID IS NOT NULL AND DATEDIFF(MINUTE, c.IPFECLLEGA, c.IPFECLLEGA) > 2880 → Marca ''MORTALIDAD URGENCIAS > 48'' = ''SI''. else ''NO''; si A.TRIAFECHA IS NULL → ATENDIDO TRIAGE = ''NO'' else ''SI''; si k.FECHFINH IS NULL → ATENDIDO CONSULTA = ''NO'' else ''SI''; si I.FECHEGRESO IS NULL → FECHA EGRESO toma EGR.FECALTPAC (fecha de alta médica desde HCREGEGRE). else Se usa I.FECHEGRESO de ADINGRESO.; si C.CONESTADO ∈ {1..8} → Traduce el código a la descripción del estado del paciente (Sin Atender, Ausente Triage, Clasificado sin/con Ingreso, Atendido, Ausente Atención Inicial, Anulado, No atendido sin Autorización).; si K.INDICAPAC ∈ {1,2,3,7,9,10,11,12,13,15,16} → Traduce a destino del paciente (Urgencias, Observación, Hospitalización, Consulta Externa, Hosp. Casa, Referencia, Morgue, Salida, Continúa, Retiro Voluntario, Fuga).; si e.EntityType ∈ {1..12,99} → Traduce a régimen (EPS Contributivo/Subsidiado, ET, ARL, Prepagada, IPS, Régimen Especial, SOAT, Fosyga, Otros, Particulares).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.HCREGEGRE; dbo.INUNIFUNC; dbo.ADCONTURG; dbo.ADTRIAGEU; dbo.INPACIENT; Contract.CareGroup; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADCATTRIU; dbo.HCURGING1; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.HCREFCONP; dbo.HCTRASLADOS; Admissions.TypesPopulationGroups', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteTriages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteTriages';
GO
