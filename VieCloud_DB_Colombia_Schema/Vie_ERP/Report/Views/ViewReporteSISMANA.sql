

CREATE VIEW [Report].[ViewReporteSISMANA] as

WITH
CTE_FACTURACION_ECOGRAFIA
    AS
	(
	  SELECT F.AdmissionNumber ,F.PatientCode ,CUPS.Code ,CUPS.Description, DF.ServiceDate  FROM   BILLING.INVOICE AS F WITH (NOLOCK) 
		   INNER JOIN BILLING.INVOICEDETAIL AS DF WITH (NOLOCK) ON DF.INVOICEID = F.ID 
		   INNER JOIN BILLING.REVENUECONTROLDETAIL RCD WITH (NOLOCK) ON RCD.ID = F.REVENUECONTROLDETAILID 
		   INNER JOIN DBO.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES = F.ADMISSIONNUMBER 
		   INNER JOIN DBO.ADCENATEN AS CEN WITH (NOLOCK) ON CEN.CODCENATE = ING.CODCENATE 
		   INNER JOIN BILLING.SERVICEORDERDETAIL AS DOS WITH (NOLOCK) ON DOS.ID = DF.SERVICEORDERDETAILID
		   LEFT OUTER JOIN CONTRACT.CUPSENTITY AS CUPS WITH (NOLOCK) ON CUPS.ID = DOS.CUPSENTITYID
		   WHERE CUPS.Code IN ('881401','881402','881403','881410','881432','881435','881436','881437') AND F.Status =1 		
		   group by F.AdmissionNumber ,F.PatientCode ,CUPS.Code ,CUPS.Description, DF.ServiceDate
	)

SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)<13,'INFANTE',
 IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)>12 AND PAC.IPSEXOPAC=2 AND HC.IDMODELOHC=11,'MATERNA','OTRA POBLACION')) AS 'POBLACION',

	HC.FECHISPAC 'FECHA ATENCION',

		IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)>=10 AND PAC.IPSEXOPAC=2 AND HC.IDMODELOHC=11, GIN.GESTACION, 
		 IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)<10 OR PAC.IPSEXOPAC=1 OR HC.IDMODELOHC IS NULL OR GIN.GESTACION IS NULL, '0','0')) AS 'NUMERO DE GESTACION',
				
	IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)>=10 AND PAC.IPSEXOPAC=2 AND HC.IDMODELOHC=11,GIN.NOMSEMGES,
	 IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)<10 OR PAC.IPSEXOPAC=1 OR HC.IDMODELOHC IS NULL OR GIN.NOMSEMGES IS NULL,'0','0')) AS 'EDAD GESTACIONAL',

	IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)>=10 AND PAC.IPSEXOPAC=2 AND HC.IDMODELOHC=11,GIN.FECULTMEN, 
	 IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)<10 OR PAC.IPSEXOPAC=1 OR HC.IDMODELOHC IS NULL OR GIN.FECULTMEN IS NULL, '1845-01-01', '1800-01-01')) AS 'FECHA ULTIMA MESTRUACION',

		IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)>=10 AND PAC.IPSEXOPAC=2 AND HC.IDMODELOHC=11,GIN.FECPROPAR, 
		 IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)<10 OR PAC.IPSEXOPAC=1 OR HC.IDMODELOHC IS NULL OR GIN.FECPROPAR IS NULL,'1845-01-01', '1800-01-01')) AS 'FECHA PROBABLE DE PARTO',

	IIF(PAC.IPTIPOPAC IN ('6','7','8'),'SI','NO') AS 'DESPLAZADO',
 CASE PAC.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA' 
				 WHEN '2' THEN 'CEDULA DE EXTRANJERIA' 
				 WHEN '3' THEN 'TARJETA DE IDENTIDAD' 
				 WHEN '4' THEN 'REGISTRO CIVIL' 
				 WHEN '5' THEN 'PASAPORTE'
				 WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' 
				 WHEN '7' THEN 'MENOR SIN IDENTIFICACION' 
				 WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACIÒN' 
				 WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' 
				 WHEN '10' THEN 'CARNET DIPLOMATICO'
				 WHEN '11' THEN 'SALVOCONDUCTO' 
				 WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' END AS [TIPO DOCUMENTO],
				 HC.IPCODPACI 'IDENTIFICACION', PAC.IPNOMCOMP 'NOMBRE PACIENTE',PAC.IPPRINOMB AS 'PRIMER NOMBRE', PAC.IPSEGNOMB AS 'SEGUNDO NOMBRE', 
				 PAC. IPPRIAPEL AS 'PRIMER APELLIDO', PAC.IPSEGAPEL AS 'SEGUNDO APELLIDO', CAST(PAC.IPFECNACI AS DATE) AS 'FECHA NAC.',
				 CASE PAC.IPSEXO WHEN 'M' THEN 'FEMENINO' WHEN 'H' THEN 'MASCULINO' ELSE 'NO REGISTRA' END 'SEXO',DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC) AS 'EDAD',
				 PAC.IPTELEFON AS 'TEL. FIJO',PAC.IPTELMOVI AS 'TEL. MOVIL', 
				 MUN.MUNNOMBRE AS 'MUNICIPIO PACIENTE',UB.UBINOMBRE AS 'BARRIO',PAC.IPDIRECCI AS 'DIRECCION PACIENTE', ISNULL(PAC.GRUPCODIGO,'NO REGISTRA') AS 'GRUPO POBLACIONAL', 
				 CASE PAC.CODGRUPOE WHEN '000' THEN 'OTRO'
									WHEN '001' THEN 'INDIGENAS'
									WHEN '002' THEN 'AFROCOLOMBIANOS NEGROS MULATOS O AFRODESCENDIENTES'
									WHEN '003' THEN 'RAIZALES SAN ANDRES Y PROVIDENCIA'
									WHEN '004' THEN 'PUEBLO ROM GITANOS'
									WHEN '999' THEN 'NO SABE  NO INFORMA  NO APLICA' ELSE 'NO REGISTRA' END 'ETNIA PACIENTE', 

				CASE PAC.NIVECODIGO WHEN '01'  THEN 'NO DEFINIDO'
									WHEN '02'  THEN 'PREESCOLAR'
									WHEN '03'  THEN 'BASICA PRIMARIA'
									WHEN '04'  THEN 'BASICA SECUNDARIA'
									WHEN '05'  THEN 'MEDIA ACADEMICA'
									WHEN '06'  THEN 'MEDIA TECNICA'
									WHEN '07'  THEN 'NORMALISTA'
									WHEN '08'  THEN 'TECNICA PROFESIONAL'
									WHEN '09'  THEN 'TECNOLOGICA'
									WHEN '10'  THEN 'PROFESIONAL'
									WHEN '11'  THEN 'ESPECIALIZACION'
									WHEN '12'  THEN 'MAESTRIA'
									WHEN '13'  THEN 'DOCTORADO' ELSE 'NO REGISTRA' END 'NIVEL EDUCATIVO', ISNULL(ANT.ANTESCOLARES,'NO REGISTRA') AS 'ESCOLARES',

				 CASE VAC.IDPLAVACU WHEN '0001' THEN 'Antituberculosa - BCG'
									WHEN '0002' THEN 'Hepatitis B'
									WHEN '0003' THEN 'PENTAVALENTE - Difteria, Tos Ferina, Tetanos, Haemophilus influenzae Tipo B, Hepatitis B'
									WHEN '0004' THEN 'PENTAVALENTE - Haemophilus influenzae Tipo B'
									WHEN '0005' THEN 'Neumococo'
									WHEN '0006' THEN 'Polio (Oral - IM)'
									WHEN '0007' THEN 'Rotavirus (Oral)'
									WHEN '0008' THEN 'PENTAVALENTE - Difteria, Tos Ferina, Tetanos, Haemophilus influenzae Tipo B, Hepatitis B'
									WHEN '0009' THEN 'PENTAVALENTE - Haemophilus influenzae Tipo B'
									WHEN '0010' THEN 'Neumococo'
									WHEN '0011' THEN 'Polio (Oral - IM)'
									WHEN '0012' THEN 'Rotavirus (Oral)'
									WHEN '0013' THEN 'PENTAVALENTE - Difteria, Tos Ferina, Tetanos, Haemophilus influenzae Tipo B, Hepatitis B '
									WHEN '0014' THEN 'PENTAVALENTE - Haemophilus influenzae Tipo B'
									WHEN '0015' THEN 'PENTAVALENTE - Hepatitis B'
									WHEN '0016' THEN 'Polio (Oral - IM)'
									WHEN '0017' THEN 'Influenza estacional'
									WHEN '0018' THEN 'Influenza estacional'
									WHEN '0019' THEN 'Triple Viral - Sarampion, Rubeola, Paperas (SRP)'
									WHEN '0020' THEN 'Varicela'
									WHEN '0021' THEN 'Difteria - Tos Ferina - Tetanos (DPT)'
									WHEN '0022' THEN 'Polio (Oral -IM)'
									WHEN '0023' THEN 'Difteria - Tos Ferina - Tetanos (DPT)'
									WHEN '0024' THEN 'Polio (Oral -IM)'
									WHEN '0025' THEN 'Triple Viral - Sarampion, Rubeola, Paperas (SRP)'
									WHEN '0026' THEN 'Toxoide Tetanico Difterico del Adulto (Td)'
									WHEN '0027' THEN 'Toxoide Tetanico Difterico del Adulto (Td)'
									WHEN '0028' THEN 'Toxoide Tetanico Difterico del Adulto (Td)'
									WHEN '0029' THEN 'Toxoide Tetanico Difterico del Adulto (Td)'
									WHEN '0030' THEN 'Toxoide Tetanico Difterico del Adulto (Td)'
									WHEN '0031' THEN 'Toxoide Tetanico Difterico del Adulto (Td)'
									WHEN '0032' THEN 'Sarampion Rubeola (SR)'
									WHEN '0033' THEN 'Neumococo'
									WHEN '0034' THEN 'Neumococo'
									WHEN '0035' THEN 'Neumococo'
									WHEN '0036' THEN 'Virus de Papiloma Humano (VPH)'
									WHEN '0037' THEN 'Virus de Papiloma Humano (VPH)'
									WHEN '0038' THEN 'Virus de Papiloma Humano (VPH)'
									WHEN '0039' THEN 'Hepatitis A'
									WHEN '0040' THEN 'Neuomococo'
									WHEN '0041' THEN 'Fiebre Amarilla'
									WHEN '0042' THEN 'Influenza'
									WHEN '0043' THEN 'TdaP (Tetanos - Difteria - Tosferina acelular)'
									WHEN '0044' THEN 'Influenza' ELSE 'NO REGISTRA' 
									END 'VACUNACION',
ISNULL(ANT.ANTMEDPAC,'NO REGISTRA') AS 'DISCAPACIDAD', ISNULL(ANT.ANTFAMPAC,'NO REGISTRA') AS 'FAMILIAR/MADRE', 
IIF(EX.PESOPACIE IS NULL, '0',
CASE WHEN (FLOOR((CAST(CONVERT(VARCHAR(8), HC.FECHISPAC,112) AS INT)-CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT))/ 10000)) <1 THEN CAST(EX.PESOPACIE AS FLOAT) ELSE (EX.PESOPACIE)/1000 END) AS 'PESO',
ISNULL(EX.TALLAPACI,'0') AS 'TALLA ', ISNULL(EX.PB,'0') AS 'PERIMETRO BRAQUIAL',ISNULL(EX.NEOPERCEF,'0') AS 'PERIMETRO CEFALICO' ,ISNULL(THC.NOMBRE,'Urgencias/Hospitalizacion') AS 'TIPO HISTORIA', 
CASE PAC.IPTIPOPAC WHEN '1' THEN 'Contributivo'
				   WHEN '2' THEN 'Subsidiado'
				   WHEN '3' THEN 'Vinculado'
				   WHEN '4' THEN 'Particular'
				   WHEN '5' THEN 'Otro'
				   WHEN '6' THEN 'Desplazado Reg. Contributivo'
				   WHEN '7' THEN 'Desplazado Reg. Subsidiado'
				   WHEN '8' THEN 'Desplazado No Asegurado' END 'REGIMEN DE SALUD', ISNULL(ANT.ANTNUTRICION,'NO REGISTRA') AS 'ANT. NUTRICIONALES', ISNULL(ECO.ServiceDate,'1800-01-01') AS 'FECHA ECOGRAFIA', ISNULL(GIN.HEMOGLO,'0') AS 'HEMOGLOBINA', 
				   ISNULL(GIN.RESCUAHEM,'0') AS 'HEMATOCRITO', ISNULL(EX.INTERALTURAUTERINA,'0') AS 'ALTURA UTERINA' ,
				   
				  IIF(DATEDIFF(YEAR,PAC.IPFECNACI,HC.FECHISPAC)<13, 
				   IIF(THC.ID IN ('7','8'),'SI','NO'),'NO APLICA') AS 'ASISTE PROGRAMA CYD'   ,                                                                  
				    DIA.NOMDIAGNO AS 'DIAGNOSTICO',PROF.NOMMEDICO AS 'PROFESIONAL QUE ATIENDE', 
1 as 'CANTIDAD',
CAST(HC.FECHISPAC AS date) AS 'FECHA BUSQUEDA',
YEAR(HC.FECHISPAC) AS 'AÑO FECHA BUSQUEDA',
MONTH(HC.FECHISPAC) AS 'MES AÑO FECHA BUSQUEDA',
CASE MONTH(HC.FECHISPAC) 
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
 FORMAT(DAY(HC.FECHISPAC), '00') AS 'DIA FECHA BUSQUEDA',
 CONCAT(FORMAT(MONTH(HC.FECHISPAC), '00') ,' - ', 
	   CASE MONTH(HC.FECHISPAC) 
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
FROM ..HCHISPACA AS HC WITH(NOLOCK)
LEFT JOIN ..INPACIENT AS PAC WITH(NOLOCK) ON PAC.IPCODPACI = HC.IPCODPACI
LEFT JOIN ..INPROFSAL AS PROF WITH(NOLOCK) ON PROF.CODPROSAL = HC.CODPROSAL
LEFT JOIN ..INUBICACI AS UB WITH(NOLOCK) ON UB.AUUBICACI = PAC.AUUBICACI
LEFT JOIN ..INMUNICIP AS MUN WITH(NOLOCK) ON MUN.DEPMUNCOD = UB.DEPMUNCOD
LEFT JOIN ..HCANTPACH AS ANT WITH(NOLOCK) ON ANT.IPCODPACI=PAC.IPCODPACI 
LEFT JOIN ..HCPLANVAC AS VAC WITH(NOLOCK) ON VAC.IPCODPACI=HC.IPCODPACI AND HC.NUMEFOLIO = VAC.NUMFOLIO AND HC.NUMINGRES=VAC.NUMINGRES
LEFT JOIN ..HCEXFISIC AS EX WITH(NOLOCK) ON EX.IPCODPACI=HC.IPCODPACI AND HC.NUMEFOLIO = EX.NUMEFOLIO AND HC.NUMINGRES=EX.NUMINGRES
LEFT JOIN ..PRMODELOHC AS THC WITH(NOLOCK) ON THC.ID=HC.IDMODELOHC
LEFT JOIN ..ADINGRESO AS ING WITH(NOLOCK) ON  ING.NUMINGRES =HC.NUMINGRES AND ING.IPCODPACI=HC.IPCODPACI
LEFT JOIN .Contract.HealthAdministrator AS EAPB WITH(NOLOCK) ON EAPB.Id=ING.GENCONENTITY
LEFT JOIN ..INDIAGNOS AS DIA WITH(NOLOCK) ON DIA.CODDIAGNO=HC.CODDIAGNO
LEFT JOIN ..HCANTGINE AS GIN WITH(NOLOCK) ON GIN.IPCODPACI=HC.IPCODPACI AND GIN.NUMINGRES=HC.NUMINGRES
LEFT JOIN CTE_FACTURACION_ECOGRAFIA AS ECO WITH(NOLOCK) ON ECO.PatientCode=HC.IPCODPACI
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting para el sistema SISMANÁ que consolida atenciones en salud de pacientes, clasificándolos en tres poblaciones: infantes (menores de 13 años), maternas (mujeres ≥13 años con historia clínica modelo 11) y otra población. Integra datos demográficos, gineco-obstétricos, vacunación, examen físico, antecedentes, diagnóstico, régimen de salud y fecha de ecografía (CUPS obstétricos). Está orientada a reportes de seguimiento y control de programas de crecimiento y desarrollo (CYD) y atención materna.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte SISMANA que consolida atenciones de historia clínica con datos demográficos, antecedentes, examen físico, ginecoobstétricos, vacunación, diagnóstico y ecografías facturadas, clasificando la población en infante, materna u otra.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en HCHISPACA (historia clínica) como tabla base; los demás cruces son LEFT JOIN.; La identificación de ecografías depende de que existan facturas en BILLING.INVOICE con Status=1 y CUPS dentro del listado de ecografías obstétricas/abdominales.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La población ''MATERNA'' requiere simultáneamente edad ≥13 años, sexo=2 y modelo de historia clínica =11.; Las fechas ginecoobstétricas inválidas o no aplicables se materializan con centinelas ''1845-01-01'' o ''1800-01-01'' nunca como NULL.; Solo se consideran ecografías con factura activa (Status=1) y CUPS dentro del listado fijo de 8 códigos (881401, 881402, 881403, 881410, 881432, 881435, 881436, 881437).; El peso para menores de 1 año se reporta en gramos (sin dividir); para el resto se asume almacenado en gramos y se convierte a kilogramos dividiendo entre 1000.; ULT_ACTUAL siempre se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El ID_COMPANY se obtiene del nombre de la base de datos actual (DB_NAME), truncado a 9 caracteres.; Cuando no hay tipo de historia (PRMODELOHC) asociado, se etiqueta por defecto como ''Urgencias/Hospitalizacion''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia clínica; Atención; Antecedentes (médicos, familiares, escolares, nutricionales); Examen físico (peso, talla, perímetro braquial, perímetro cefálico, altura uterina); Antecedentes ginecoobstétricos (gestación, semanas, FUM, FPP, hemoglobina, hematocrito); Vacunación (PAI: BCG, Hepatitis B, Pentavalente, Polio, Rotavirus, Neumococo, SRP, DPT, Td, VPH, Fiebre Amarilla, Influenza, TdaP); Régimen de salud (Contributivo, Subsidiado, Vinculado, Particular, Desplazado); Población desplazada; Etnia / grupo poblacional; Nivel educativo; Tipo de documento de identidad; Diagnóstico (CIE); Profesional de salud; Programa Crecimiento y Desarrollo (CYD); Ecografía obstétrica (CUPS); EAPB / Administradora de salud; Facturación; Ingreso/Admisión; Centro de atención', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewReporteSISMANA: Devuelve un dataset por atención (HCHISPACA) enriquecido con paciente, ubicación, antecedentes, exámenes, ginecoobstétricos, vacunación, diagnóstico, profesional y datos de ecografía facturada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DATEDIFF(YEAR, IPFECNACI, FECHISPAC) < 13 → POBLACION=''INFANTE'' else Si edad>12 AND IPSEXOPAC=2 AND IDMODELOHC=11 → ''MATERNA''; en otro caso → ''OTRA POBLACION''; si Edad >= 10 AND sexo=2 (femenino) AND IDMODELOHC=11 → Se exponen datos ginecoobstétricos reales (GESTACION, NOMSEMGES, FECULTMEN, FECPROPAR) else Se sustituyen por ''0'' o por fechas centinela (''1845-01-01'' o ''1800-01-01''); si PAC.IPTIPOPAC IN (''6'',''7'',''8'') → DESPLAZADO=''SI'' else DESPLAZADO=''NO''; si Edad < 1 año (calculada por diferencia AAAAMMDD/10000) y existe peso → PESO se devuelve tal cual (CAST FLOAT) else PESO se divide entre 1000 (conversión gramos→kg); si EX.PESOPACIE IS NULL → PESO=''0'' else Aplica regla de conversión por edad; si Edad < 13 años → ASISTE PROGRAMA CYD = ''SI'' si tipo de historia (THC.ID) IN (''7'',''8''), si no ''NO'' else ASISTE PROGRAMA CYD = ''NO APLICA''; si CUPS.Code IN (''881401'',''881402'',''881403'',''881410'',''881432'',''881435'',''881436'',''881437'') AND F.Status=1 en BILLING.INVOICE → El registro entra al CTE de ecografías y aporta la FECHA ECOGRAFIA del paciente else FECHA ECOGRAFIA se rellena con ''1800-01-01''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'BILLING.INVOICE; BILLING.INVOICEDETAIL; BILLING.REVENUECONTROLDETAIL; BILLING.SERVICEORDERDETAIL; CONTRACT.CUPSENTITY; dbo.ADINGRESO; dbo.ADCENATEN; dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUBICACI; dbo.INMUNICIP; dbo.HCANTPACH; dbo.HCPLANVAC; dbo.HCEXFISIC; dbo.PRMODELOHC; Contract.HealthAdministrator; dbo.INDIAGNOS; dbo.HCANTGINE', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteSISMANA';
GO
