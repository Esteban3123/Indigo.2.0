

    /*******************************************************************************************************************
Nombre: [Report].[ViewPacientesDiscapacidad]
Tipo:Vista
Observacion:Se elistan todos los pacientes con algun tipo de discapacidad
Profesional: Nilsson Miguel Galindo Lopez
Fecha:25-08-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: 
Fecha:
Ovservaciones: 
--------------------------------------
Vercion 2
Persona que modifico:
Fecha:
--***********************************************************************************************************************************/

CREATE view [Report].[ViewPacientesDiscapacidad]
as

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
PAC.IPCODPACI AS IDENTIFICACION,
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
PAC.IPNOMCOMP AS [NOMBRE COMPLETO],
PAC.IPPRINOMB AS [PRIMER NOMBRE],
PAC.IPSEGNOMB AS [SEGUNDO NOMBRE], 
PAC.IPPRIAPEL AS [PRIMER APELLIDO],
PAC.IPSEGAPEL AS [SEGUNDO APELLIDO],
CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END [SEXO],
CAST(PAC.IPFECNACI AS DATE ) AS [FECHA_NACIMIENTO],
PAC.IPDIRECCI AS DIRECCION,
DEP.NOMDEPART AS DEPARTAMENTO, 
MUN.MUNNOMBRE AS MUNICIPIO,
UB.UBINOMBRE AS UBICACION,
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
DIS.DISCDESCRI AS DISCAPACIDAD,
ING.NUMINGRES AS INGRESO,
ING.IFECHAING AS [FECHA DE ATENCIÓN],
CASE ING.TIPOINGRE WHEN 1 THEN 'AMBULATORIO'
				   WHEN 2 THEN 'HOSPITALARIO'END AS [TIPO INGRESO],
CASE ING.IINGREPOR WHEN 1 THEN 'URGENCIAS'
				   WHEN 2 THEN 'CONSULTA EXTERNA'
				   WHEN 3 THEN 'NACIDO HOSPITAL'
				   WHEN 4 THEN 'REMITIDO'
				   WHEN 5 THEN 'HOSPITALIZACIÓN DE URGENCIAS' END AS [INGRESO POR],
DI.CODDIAGNO+' - '+DI.NOMDIAGNO AS [DIAGNOSTICO DE INGRESO],
DIA.CODDIAGNO+' - '+DIA.NOMDIAGNO AS [DIAGNOSTICO DE EGRESO],
 1 'CANTIDAD',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
DBO.INPACIENT PAC INNER JOIN
INUBICACI UB ON PAC.AUUBICACI=UB.AUUBICACI INNER JOIN
INMUNICIP MUN ON UB.DEPMUNCOD = MUN.DEPMUNCOD INNER JOIN
INDEPARTA DEP ON MUN.DEPCODIGO = DEP.DEPCODIGO AND PAC.AUUBICACI = UB.AUUBICACI INNER JOIN
INENTIDAD AS EAPB ON PAC.CODENTIDA=EAPB.CODENTIDA INNER JOIN
dbo.ADDISCAPACI DIS ON PAC.DISCCODIGO=DIS.DISCCODIGO INNER JOIN 
ADINGRESO ING ON PAC.IPCODPACI=ING.IPCODPACI LEFT JOIN
DBO.INDIAGNOS DI ON ING.CODDIAING=DI.CODDIAGNO LEFT JOIN
DBO.INDIAGNOS DIA ON ING.CODDIAEGR=DIA.CODDIAGNO
WHERE DIS.DISCDESCRI not like'%ningun%' and DIS.DISCDESCRI not like'%sin dis%'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida todos los pacientes que tienen registrado algún tipo de discapacidad (excluyendo registros con "ningún" o "sin dis"), combinando datos demográficos, tipo y número de documento, régimen de aseguramiento, EAPB, municipio y departamento de residencia. Incorpora información de cada ingreso asociado al paciente, incluyendo fechas, tipo de ingreso, vía de ingreso y diagnósticos de ingreso y egreso con su código CIE. Está orientada a reporting de población con discapacidad para análisis epidemiológico o de gestión en salud.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con algún tipo de discapacidad registrada junto con sus datos demográficos, EAPB, régimen, ubicación e información del ingreso (diagnósticos y tipo de atención).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener asignada una ubicación (AUUBICACI) válida con municipio y departamento existentes.; El paciente debe tener entidad EAPB (CODENTIDA) registrada en INENTIDAD.; El paciente debe tener un código de discapacidad (DISCCODIGO) presente en ADDISCAPACI.; El paciente debe tener al menos un ingreso registrado en ADINGRESO (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen pacientes con discapacidad efectiva (se excluyen descripciones que indican ''ninguna'' o ''sin discapacidad'').; Cada fila representa la combinación paciente-ingreso (un paciente con varios ingresos aparecerá múltiples veces).; El identificador de compañía se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.; La fecha de última actualización se calcula con la zona horaria ''Pakistan Standard Time''.; Los diagnósticos de ingreso y egreso son opcionales (LEFT JOIN con INDIAGNOS); pueden no existir.; La cantidad reportada por fila es siempre 1.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Discapacidad; Tipo de identificación; EAPB; Régimen de afiliación (Contributivo/Subsidiado/Vinculado/Particular/Desplazado); Ingreso/Admisión; Tipo de ingreso (ambulatorio/hospitalario); Origen de ingreso (urgencias/consulta externa/remitido); Diagnóstico de ingreso y egreso; Ubicación geográfica (departamento/municipio)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado de la vista: Excluye registros cuya descripción de discapacidad contenga ''ningun'' o ''sin dis'' (filtro: DIS.DISCDESCRI NOT LIKE ''%ningun%'' AND NOT LIKE ''%sin dis%'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPTIPODOC entre 1 y 12 → Mapea a etiqueta de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE) else Etiqueta ''OTRO''; si PAC.IPSEXOPAC = 1 → Sexo=''MASCULINO'' else Sexo=''FEMENINO'' (cualquier otro valor se considera femenino); si PAC.IPTIPOPAC entre 1 y 8 → Mapea régimen (Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado Reg. Contributivo, Desplazado Reg. Subsidiado, Desplazado No Asegurado) else NULL; si ING.TIPOINGRE = 1 o 2 → Clasifica como ''AMBULATORIO'' u ''HOSPITALARIO'' else NULL; si ING.IINGREPOR entre 1 y 5 → Clasifica origen de ingreso (URGENCIAS, CONSULTA EXTERNA, NACIDO HOSPITAL, REMITIDO, HOSPITALIZACIÓN DE URGENCIAS) else NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.INENTIDAD; dbo.ADDISCAPACI; dbo.ADINGRESO; DBO.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPacientesDiscapacidad';
GO
