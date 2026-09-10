

/*******************************************************************************************************************
Nombre: [Report].[ViewExamenFisico]
Tipo:Vista
Observacion:Reporte de examen fisico normal sin histrorias clinicas parametrizables. 
Profesional: Nilsson Miguel Galindo Lopez
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Amira Gil Meneses
Fecha: 01-06-2023
Ovservaciones:Se ingresan el campo 'DOLOR' de la tabla dbo.HCEXFISIC al reporte.
-------------------------------------------------------------------------------------
Version 3
Persona que modifico: Nilsson Miguel Galindo Lopez 
Observacion:Se mejora la velocidad del la consulta en un 95%
Fecha:
-------------------------------------------------------------------------------------
Version 3
Persona que modifico:
Observacion:
Fecha:
****************************************************************************************************************************/

CREATE view [Report].[ViewExamenFisico] as

WITH 
CTE_DIAP AS
(
SELECT 
DIA.NUMINGRES,DIA.CODDIAGNO,DG.NOMDIAGNO
FROM
DBO.INDIAGNOP DIA INNER JOIN
DBO.ADINGRESO ING ON DIA.NUMINGRES=ING.NUMINGRES AND ING.IFECHAING BETWEEN GETDATE() - 400 AND GETDATE() INNER JOIN
DBO.INDIAGNOS DG ON DIA.CODDIAGNO=DG.CODDIAGNO
WHERE CODDIAPRI=1
),
CTE_DIAGNOSTICOS AS
(
select 
ROW_NUMBER ( )   
OVER ( PARTITION BY DIA.NUMINGRES  order by DIA.CODDIAGNO,DIA.NUMINGRES) 'NUMERO',
DIA.NUMINGRES,
DIA.CODDIAGNO,
DG.NOMDIAGNO
from 
DBO.INDIAGNOP DIA INNER JOIN
DBO.ADINGRESO ING ON DIA.NUMINGRES=ING.NUMINGRES AND ING.IFECHAING BETWEEN GETDATE() - 400 AND GETDATE() INNER JOIN
DBO.INDIAGNOS DG ON DIA.CODDIAGNO=DG.CODDIAGNO
WHERE DIAESTADO=1 AND CODDIAPRI!=1
)

SELECT --TOP 100
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CEN.NOMCENATE AS [CENTRO DE ATENCION],
CASE pac.IPTIPODOC WHEN 1 THEN 'CC - CEDULA DE CIUDADANIA' 
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
				   WHEN 12 THEN 'PE - PERMISO ESPECIAL DE PERMANENCIA' 
				   WHEN 13 THEN 'PT - PERMISO TEMPORAL DE PERMANENCIA'
				   WHEN 14 THEN 'DE - DOCUMENTO EXTRANJERO'
				   WHEN 15 THEN 'SI - SIN IDENTIFICACION'
				   END [TIPO IDENTIFICACION],
pac.IPCODPACI AS IDENTIFICACION,
pac.IPNOMCOMP AS [NOMBRE PACIENTE],
pac.IPFECNACI AS [FECHA DE NACIMIENTO],
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
ing.NUMINGRES as INGRESO,
/*IN V3*/
FIS.NUMEFOLIO AS FOLIO,
CASE ING.TIPOINGRE WHEN 1 THEN 'Ambulatorio'
				   WHEN 2 THEN 'Hospitalario' END [TIPO DE INGRESO],/*FN V3*/
ING.IFECHAING AS [FECHA DE INGRESO],
FIS.NUMCONSEC AS [NUMERO DE REGISTRO],
fis.FECREGITE as [FECHA DE REGISTRO],
fis.TENARTSIS as [TENSION SISTOLICA],
fis.TENARTDIA as [TENSION ARTERIAL DIASTOLICA],
fis.TEMPERPAC as TEMPERATURA,
fis.FRECARPAC as [FRECUENCIA CARDIACA],
fis.FRERESPAC as [FRECUENCIA RESPIRATORIA],
fis.REGSO2PAC as [SATURACION OXIGENO],
fis.TALLAPACI as TALLA,
convert(bigint,fis.PESOPACIE) as [PESO Gr],
(convert(bigint,fis.PESOPACIE)/1000) as [PESO Kg],
/*IN V2*/FIS.DOLOR as [DOLOR],/*FN V2*/
DXP.CODDIAGNO+' - '+DXP.NOMDIAGNO AS [DX PRINCIPAL],
DXP.CODDIAGNO+' - '+DXP.NOMDIAGNO AS [DX 2],
DXP.CODDIAGNO+' - '+DXP.NOMDIAGNO AS [DX 3],
ESP.DESESPECI AS ESPECIALIDAD,
1 as 'CANTIDAD',
CAST(ING.IFECHAING AS date) AS 'FECHA BUSQUEDA',
YEAR(ING.IFECHAING) AS 'AÑO FECHA BUSQUEDA',
MONTH(ING.IFECHAING) AS 'MES AÑO FECHA BUSQUEDA',
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
	WHEN 12 THEN 'DICIEMBRE'
  END AS 'MES NOMBRE FECHA BUSQUEDA', 
FORMAT(DAY(ING.IFECHAING), '00') AS 'DIA FECHA BUSQUEDA',
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
			WHEN 12 THEN 'DICIEMBRE'
		END) MES_LABEL_BUSQUEDA,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from 
dbo.ADINGRESO ING inner join
DBO.INPACIENT PAC on ING.IPCODPACI=pac.IPCODPACI INNER JOIN
dbo.HCEXFISIC FIS ON ING.NUMINGRES=FIS.NUMINGRES AND FIS.NUMCONSEC=(SELECT MAX(F.NUMCONSEC) FROM dbo.HCEXFISIC F WHERE ING.NUMINGRES=F.NUMINGRES) INNER JOIN
dbo.INPROFSAL PRO ON FIS.CODPROSAL=PRO.CODPROSAL INNER JOIN
DBO.ADCENATEN CEN ON ING.CODCENATE=CEN.CODCENATE INNER JOIN
DBO.INENTIDAD AS EAPB ON PAC.CODENTIDA=EAPB.CODENTIDA INNER JOIN
dbo.INESPECIA ESP ON PRO.CODESPEC1=ESP.CODESPECI 
LEFT JOIN CTE_DIAP DXP ON ING.NUMINGRES=DXP.NUMINGRES
LEFT JOIN CTE_DIAGNOSTICOS DX2 ON ING.NUMINGRES=DX2.NUMINGRES AND DX2.NUMERO=1
LEFT JOIN CTE_DIAGNOSTICOS DX3 ON ING.NUMINGRES=DX3.NUMINGRES AND DX3.NUMERO=2
WHERE 
ING.IFECHAING BETWEEN GETDATE() - 400 AND GETDATE()
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida el último examen físico registrado por ingreso, limitado a los 400 días previos. Combina datos demográficos del paciente, tipo de documento e identificación, régimen de aseguramiento, signos vitales y antropometría (incluyendo dolor desde v2), junto con diagnóstico principal y secundarios, especialidad del profesional y centro de atención. Está diseñada para consumo en herramientas de reporting, con campos de fecha desagregados por año, mes y día para facilitar filtros y agrupaciones.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los signos vitales y el examen físico más reciente de cada ingreso de paciente ocurrido en los últimos 400 días, enriqueciéndolo con datos demográficos, EAPB, especialidad y diagnósticos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existir un ingreso en ADINGRESO con IFECHAING dentro de los últimos 400 días respecto a GETDATE(); Existir al menos un registro de examen físico (HCEXFISIC) asociado al ingreso; El paciente debe estar en INPACIENT y la EAPB en INENTIDAD; El profesional asociado al examen físico debe tener especialidad principal (CODESPEC1) registrada en INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan ingresos cuya fecha (IFECHAING) esté en la ventana móvil de los últimos 400 días; Para cada ingreso solo se considera el examen físico con el mayor NUMCONSEC (último registro); La edad se calcula en años completos (FLOOR) usando la diferencia entre IFECHAING y IPFECNACI en formato YYYYMMDD; El peso se expone tanto en gramos (PESOPACIE) como en kilogramos (PESOPACIE/1000) truncado a entero; ULT_ACTUAL siempre se devuelve convertido a la zona horaria ''Pakistan Standard Time''; ID_COMPANY corresponde al nombre de la base de datos truncado a 9 caracteres; La cantidad reportada por fila siempre es 1; Los diagnósticos CTE solo provienen de ingresos también dentro de los últimos 400 días', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Examen físico; Signos vitales (TA sistólica/diastólica, temperatura, FC, FR, SatO2); Talla y peso; Dolor; Diagnóstico principal y secundarios; EAPB; Régimen de afiliación (Contributivo, Subsidiado, Vinculado, Particular, Desplazado); Tipo de identificación; Centro de atención; Especialidad médica; Tipo de ingreso (Ambulatorio/Hospitalario); Folio de historia clínica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewExamenFisico: Devuelve una sola fila por ingreso correspondiente al examen físico con MAX(NUMCONSEC) por NUMINGRES, filtrando ingresos con IFECHAING entre GETDATE()-400 y GETDATE().', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (1..15) → Se traduce a etiqueta de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE, PT, DE, SI); si IPSEXOPAC = 1 / 2 → Sexo se reporta como MASCULINO / FEMENINO; si IPTIPOPAC (1..8) → Régimen se traduce a Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado Reg. Contributivo, Desplazado Reg. Subsidiado o Desplazado No Asegurado; si TIPOINGRE = 1 / 2 → Tipo de ingreso se reporta como Ambulatorio / Hospitalario; si CTE_DIAP: CODDIAPRI = 1 → Se considera el diagnóstico como principal (DX PRINCIPAL) else CTE_DIAGNOSTICOS: DIAESTADO=1 y CODDIAPRI<>1 se enumeran con ROW_NUMBER por NUMINGRES para diagnósticos secundarios; si MONTH(IFECHAING) → Se traduce el número de mes a su nombre en español (ENERO..DICIEMBRE) para los campos de etiqueta de fecha', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INDIAGNOP; DBO.ADINGRESO; DBO.INDIAGNOS; DBO.INPACIENT; dbo.HCEXFISIC; dbo.INPROFSAL; DBO.ADCENATEN; DBO.INENTIDAD; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExamenFisico';
GO
