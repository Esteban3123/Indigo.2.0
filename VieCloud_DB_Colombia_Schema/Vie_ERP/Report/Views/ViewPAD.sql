

/*******************************************************************************************************************
Nombre: [Report].[ViewPAD]
Tipo:Vistas
Observacion:Vista sobre la oportunidad atencion domiciliaria
Profesional: Nilsson Miguel Galindo Lopez
Fecha:27-09-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 2
Persona que modifico: Amira Esperanza Gil 
Fecha: 19-05-2023
Ovservaciones: 
--------------------------------------
Vercion 3
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewPAD] as

WITH 
CTE_SOLICITUD AS
(
	SELECT 
	ROW_NUMBER ( )   
		OVER ( PARTITION BY PAD.IPCODPACI  order by PAD.IPCODPACI,PAD.NUMINGRES) 'NUMERO',
	PAD.ID,
	PAD.NUMINGRES,
	PAD.NUMEFOLIO,
	PAD.IPCODPACI,
	PAD.FECHAREGISTRO,
	PAD.ESTADO,
	PAD.TIPOATENCION,
	PAD.FECHASUSP,
	EGR.FECALTPAC AS [FECHA ALTA MEDICA],
  	EGR.ESTPACEGR, 
	EGR.FECMUEPAC, 
	EGR.NUMCERDEF,
	EGR.CODCAUMUE,
	ING.FECHEGRESO AS [FECHA EGRESO]
	FROM 
	dbo.PADCONTROL PAD INNER JOIN
	dbo.ADINGRESO ING ON PAD.NUMINGRES=ING.NUMINGRES AND PAD.NUMEFOLIO=(SELECT MAX(CON.NUMEFOLIO) FROM dbo.PADCONTROL CON WHERE PAD.NUMINGRES=CON.NUMINGRES) LEFT JOIN
	dbo.HCREGEGRE EGR ON PAD.NUMINGRES=EGR.NUMINGRES 
	--WHERE PAD.IPCODPACI='6491466'
),
CTE_INGRESO_PAD AS
(
	SELECT 
	ROW_NUMBER ( )   
		OVER ( PARTITION BY ING.IPCODPACI  order by ING.IPCODPACI,ING.IFECHAING) 'NUMERO',
	ING.NUMINGRES,
	ING.IPCODPACI,
	ING.IFECHAING,
	EGR.FECALTPAC AS [FECHA EGRESO PAD],
	ING.UFUCODIGO,
	APAD.FECHAEGRESO AS [FECHA ALTA MEDICA PAD]
	FROM 
	dbo.ADINGRESO ING INNER JOIN
	dbo.INUNIFUNC FUN ON ING.UFUCODIGO=FUN.UFUCODIGO AND FUN.UFUDESCRI LIKE '%DOMICILIARIA%' LEFT JOIN
	dbo.HCREGEGRE EGR ON ING.NUMINGREI=EGR.NUMINGRES
	INNER JOIN dbo.PADASIGNACION  APAD ON ING.NUMINGRES=APAD.NUMEROINGRESO
	--WHERE ING.IPCODPACI='6491466'

),

CTE_CONTROL AS
(
	SELECT 
	SOL.ID,
	SOL.IPCODPACI,
	SOL.NUMINGRES AS NUMINGRES_SOL,
	SOL.NUMEFOLIO,
	SOL.FECHAREGISTRO,
	CASE WHEN PAD.NUMINGRES IS NOT NULL THEN 'ATENDIDO' 
	     WHEN SOL.ESTADO=1 THEN 'REGISTRADO' ELSE 'SUSPENDIDO' END AS ESTADO,
	CASE SOL.TIPOATENCION  WHEN 1 THEN 'ATENCIÓN DOMICILIARIA' 
						   WHEN 2 THEN 'HOSPITALIZACIÓN EN CASA' END AS [TIPO DE SOLICITUD],						   							 
	CASE WHEN PAD.NUMINGRES IS NOT NULL THEN NULL ELSE SOL.FECHASUSP END AS FECHASUSP,
	SOL.[FECHA ALTA MEDICA],
	PAD.NUMINGRES AS NUMINGRES_PAD,
	PAD.IFECHAING AS [FECHA INGRESO PAD],
	PAD.[FECHA EGRESO PAD],
	PAD.[FECHA ALTA MEDICA PAD],
	CASE SOL.ESTPACEGR   WHEN 1 THEN 'MEJOR'
                         WHEN 2 THEN 'IGUAL Y PEOR'
                         WHEN 3 THEN 'FALLECIDO'
                         WHEN 4 THEN 'REMITIDO'
                         WHEN 5 THEN 'HOSPITALIZACION EN CASA' END AS [ESTADO PACIENTE AL EGRESO], 
	CASE SOL.ESTPACEGR  WHEN 3 THEN 'SI' ELSE 'NO' END AS [FALLECIDO],
	SOL.FECMUEPAC AS [FECHA DE MUERTE],
	SOL.NUMCERDEF AS [NO_CERT_DEFUNCION],
	CASE SOL.CODCAUMUE  WHEN 001 THEN 'MUERTE NATURAL'
                    WHEN 002 THEN 'MUERTE VIOLENTA'
                    WHEN 003 THEN 'MUERTE EN ESTUDIO' END AS [CAUSA DE MUERTE]
	FROM 
	CTE_SOLICITUD SOL LEFT JOIN
	CTE_INGRESO_PAD PAD ON SOL.IPCODPACI=PAD.IPCODPACI AND SOL.NUMERO=PAD.NUMERO
)

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CEN.NOMCENATE AS [CENTRO DE ATENCION],
CON.IPCODPACI AS [IDENTIFICACION PACIENTE],
CASE PAC.IPTIPODOC WHEN 1 THEN 'CC - CEDULA DE CIUDADANIA' 
				   WHEN 2 THEN 'CE - CEDULA DE EXTRANJERIA' 
				   WHEN 3 THEN 'TI - TARJETA DE IDENTIDAD' 
				   WHEN 4 THEN 'RC - REGISTRO CIVIL' 
				   WHEN 5 THEN 'PA - PASAPORTE' 
				   WHEN 6 THEN 'AS - ADULTO SIN IDENTIFICACION' 
				   WHEN 7 THEN 'MS - MENOR SIN IDENTIFICACION' 
				   WHEN 8 THEN 'NU - NUMERO UNICO DE IDENTIFICACIÒN' 
				   WHEN 9 THEN 'NV - CERTIFICADO NACIDO VIVO' 
				   WHEN 10 THEN 'CD - CARNET DIPLOMATICO' 
				   WHEN 11 THEN 'SC - SALVOCONDUCTO' 
				   WHEN 12 THEN 'PE - PERMISO ESPECIAL DE PERMANENCIA' ELSE 'OTRO' END [TIPO IDENTIFICACION],
PAC.IPNOMCOMP AS [NOMBRES Y APELLIDOS PACIENTE],
CAST(PAC.IPFECNACI AS DATE) AS [FECHA DE NACIMIENTO],
FLOOR((CAST(CONVERT(VARCHAR(8), ING.IFECHAING , 112) AS INT) - CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS [EDAD],
CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' WHEN 2 THEN 'FEMENINO' END AS SEXO,
PAC.IPTELEFON AS [TELEFONO],
PAC.IPTELMOVI AS [CELULAR],
EAPB.CODENTIDA AS [CODIGO EAPB],
EAPB.NOMENTIDA AS [NOMBRE EAPB],
CASE PAC.IPTIPOPAC WHEN 1 THEN 'Contributivo'
				   WHEN 2 THEN 'Subsidiado'
				   WHEN 3 THEN 'Vinculado'
				   WHEN 4 THEN 'Particular'
				   WHEN 5 THEN 'Otro' 
				   WHEN 6 THEN 'Desplazado Reg. Contributivo'
				   WHEN 7 THEN 'Desplazado Reg. Subsidiado'
				   WHEN 8 THEN 'Desplazado No Asegurado' END AS [REGIMEN],
CON.NUMINGRES_SOL AS INGRESO,
CON.NUMEFOLIO AS FOLIO,
CON.FECHAREGISTRO AS [FECHA SOLICITUD],
CON.ESTADO,
CON.[TIPO DE SOLICITUD],
PADO.PERTINENCIA,
CON.[FECHA ALTA MEDICA],
ING.FECHEGRESO AS [FECHA EGRESO],
CON.[ESTADO PACIENTE AL EGRESO],
CON.FALLECIDO,
CON.[FECHA DE MUERTE],
CON.[CAUSA DE MUERTE],
CON.NO_CERT_DEFUNCION,
CON.[FECHA INGRESO PAD],
CON.NUMINGRES_PAD AS [INGRESO PAD],
CON.[FECHA ALTA MEDICA PAD],
CON.[FECHA EGRESO PAD],
CON.FECHASUSP AS [FECHA SUSPENCIÓN DE SOLICITUD],
'1' AS CATIDAD,
CAST (CON.FECHAREGISTRO as date) 'FECHA BUSQUEDA',
YEAR(CON.FECHAREGISTRO) AS 'AÑO FECHA BUSQUEDA',
MONTH(CON.FECHAREGISTRO) AS 'MES AÑO FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(CON.FECHAREGISTRO), '00') ,' - ', 
	   CASE MONTH(CON.FECHAREGISTRO) 
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
		END) AS 'MES NOMBRE FECHA BUSQUEDA',
DAY(CON.FECHAREGISTRO) AS 'DIA FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
CTE_CONTROL CON INNER JOIN
dbo.PADORDEN PADO ON CON.ID=PADO.IDPADCONTROL INNER JOIN
dbo.ADINGRESO ING ON CON.NUMINGRES_SOL=ING.NUMINGRES INNER JOIN 
DBO.INPACIENT PAC on CON.IPCODPACI=PAC.IPCODPACI INNER JOIN
DBO.ADCENATEN CEN ON ING.CODCENATE=CEN.CODCENATE INNER JOIN
DBO.INENTIDAD EAPB ON PAC.CODENTIDA=EAPB.CODENTIDA
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada al seguimiento de oportunidad en el Programa de Atención Domiciliaria (PAD). Consolida solicitudes domiciliarias con sus ingresos PAD asociados, datos demográficos del paciente, aseguradora, régimen y centro de atención, cruzando el estado de cada solicitud (registrado, atendido, suspendido), tipo de atención (domiciliaria u hospitalización en casa), pertinencia de la orden, fechas clave de ingreso/egreso/alta y condición de egreso incluyendo fallecimiento. Incluye dimensiones temporales por año, mes y día para análisis periódico en herramientas de Business Intelligence.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la trazabilidad de las solicitudes del Programa de Atención Domiciliaria (PAD) por paciente, cruzando solicitud, ingreso PAD, egreso, alta médica, fallecimiento y datos sociodemográficos/aseguramiento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener al menos una orden registrada en dbo.PADORDEN ligada a PADCONTROL (INNER JOIN con PADO).; El ingreso de la solicitud (NUMINGRES_SOL) debe existir en dbo.ADINGRESO.; El paciente debe existir en INPACIENT y tener entidad (EAPB) en INENTIDAD y centro de atención en ADCENATEN.; Para considerar un ingreso como PAD, su unidad funcional (INUNIFUNC.UFUDESCRI) debe contener el texto ''DOMICILIARIA'' y debe existir registro en PADASIGNACION asociado al ingreso.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La solicitud representativa por ingreso es la del último folio: PAD.NUMEFOLIO = MAX(NUMEFOLIO) por NUMINGRES.; La numeración de solicitudes e ingresos PAD por paciente se hace con ROW_NUMBER particionado por IPCODPACI; el cruce solicitud↔ingreso PAD se realiza por correspondencia ordinal (mismo NUMERO) dentro del paciente.; La edad se calcula en años completos comparando IFECHAING vs IPFECNACI usando formato YYYYMMDD (FLOOR((ing-nac)/10000)).; CATIDAD siempre se reporta como ''1'' (constante para conteos).; ID_COMPANY corresponde al nombre de la base de datos actual truncado a 9 caracteres (DB_NAME()).; ULT_ACTUAL se entrega convertido a la zona horaria ''Pakistan Standard Time''.; Solo se consideran ingresos PAD aquellos cuya unidad funcional contiene ''DOMICILIARIA'' en su descripción.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Programa de Atención Domiciliaria (PAD); Hospitalización en casa; Solicitud de atención domiciliaria; Ingreso/egreso hospitalario; Alta médica; Fallecimiento del paciente y causa de muerte; Certificado de defunción; Régimen de afiliación (EAPB); Tipo de documento de identificación; Centro de atención; Unidad funcional; Pertinencia de la orden PAD', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewPAD: Devuelve un set tabular de solicitudes PAD enriquecido con paciente, EAPB, centro, fechas (ingreso, egreso, alta, muerte) y descomposición de la fecha de solicitud (año, mes, nombre del mes, día).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAD.NUMINGRES IS NOT NULL (existe ingreso PAD asociado a la solicitud) → ESTADO = ''ATENDIDO'' y FECHASUSP se anula (NULL) else Si SOL.ESTADO=1 → ''REGISTRADO''; en otro caso → ''SUSPENDIDO'', conservando FECHASUSP; si SOL.TIPOATENCION → 1 → ''ATENCIÓN DOMICILIARIA''; 2 → ''HOSPITALIZACIÓN EN CASA''; si SOL.ESTPACEGR (estado del paciente al egreso) → 1=MEJOR, 2=IGUAL Y PEOR, 3=FALLECIDO, 4=REMITIDO, 5=HOSPITALIZACION EN CASA; si SOL.ESTPACEGR = 3 → FALLECIDO = ''SI'' else FALLECIDO = ''NO''; si SOL.CODCAUMUE (causa de muerte) → 001=MUERTE NATURAL, 002=MUERTE VIOLENTA, 003=MUERTE EN ESTUDIO; si PAC.IPTIPODOC → Mapeo a etiquetas de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE) else ''OTRO''; si PAC.IPTIPOPAC → Mapeo a régimen de afiliación (Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado Contributivo/Subsidiado/No Asegurado); si PAC.IPSEXOPAC → 1=MASCULINO, 2=FEMENINO', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.PADCONTROL; dbo.ADINGRESO; dbo.HCREGEGRE; dbo.INUNIFUNC; dbo.PADASIGNACION; dbo.PADORDEN; dbo.INPACIENT; dbo.ADCENATEN; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPAD';
GO
