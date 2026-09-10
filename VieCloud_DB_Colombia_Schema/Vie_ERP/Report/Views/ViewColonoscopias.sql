

/*******************************************************************************************************************
Nombre: [Report].[ViewColonoscopias]
Tipo:Vista
Observacion:Reporte sobre colonoscopias
Profesional: Nilsson Miguel Galindo Lopez
Fecha:14-11-2022
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
***********************************************************************************************************************************/
CREATE VIEW [Report].[ViewColonoscopias]
AS
SELECT 
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
				   WHEN 12 THEN 'PE - PERMISO ESPECIAL DE PERMANENCIA' ELSE 'OTRO' END [TIPO IDENTIFICACION],
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
ING.NUMINGRES AS [NUMERO DE INGRESO],
FIS.NOMBRE AS [RESULTADO COLONOSCOPIA],
FEC.VALOR AS [FECHA COLONOSCOPIA],
ING.IFECHAING AS [FECHA INGRESO],
1 as 'CANTIDAD',
CAST(ING.IFECHAING AS date) AS 'FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
[Report].[TablaExamenFisico] FIS INNER JOIN
DBO.INPACIENT PAC ON FIS.IPCODPACI=PAC.IPCODPACI AND NOMBRE_VARIABLE='Resultado de colonoscopia de tamizaje' INNER JOIN 
DBO.INENTIDAD EAPB ON PAC.CODENTIDA=EAPB.CODENTIDA LEFT JOIN
dbo.ADINGRESO ING ON FIS.NUMINGRES=ING.NUMINGRES LEFT JOIN
DBO.ADCENATEN CEN ON ING.CODCENATE=CEN.CODCENATE LEFT JOIN
[Report].[TablaExamenFisico] FEC ON FIS.NUMINGRES=FEC.NUMINGRES AND FEC.NOMBRE_VARIABLE='Fecha de realización de colonoscopia tamizaje'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para colonoscopias de tamizaje que consolida, por episodio de ingreso, datos demográficos del paciente (identificación, nombre, edad calculada, sexo, contacto), régimen y aseguradora (EAPB), centro de atención, resultado de la colonoscopia y fecha de realización. Filtra registros del examen físico donde la variable corresponde a "Resultado de colonoscopia de tamizaje", orientada a consumo en herramientas de reporting o inteligencia de negocio.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida pacientes con resultado y fecha de colonoscopia de tamizaje, junto con datos demográficos, EAPB, régimen, ingreso y centro de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Report.TablaExamenFisico con NOMBRE_VARIABLE=''Resultado de colonoscopia de tamizaje'' para que el paciente aparezca en el reporte.; El paciente debe existir en INPACIENT y estar asociado a una entidad (CODENTIDA) presente en INENTIDAD.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula como diferencia entera de años entre IFECHAING y IPFECNACI usando formato YYYYMMDD (FLOOR((AAAAMMDD_ing - AAAAMMDD_nac)/10000)).; ID_COMPANY se obtiene del nombre de la base de datos actual (DB_NAME()) truncado a 9 caracteres.; El campo CANTIDAD siempre es 1 (cada fila representa una colonoscopia).; ULT_ACTUAL se calcula con GETDATE() convertido a zona horaria ''Pakistan Standard Time''.; FECHA BUSQUEDA es la fecha (sin hora) del ingreso (IFECHAING).; El ingreso (ADINGRESO) y centro de atención (ADCENATEN) son opcionales (LEFT JOIN); pueden ser NULL en el reporte.; La fecha de la colonoscopia es opcional (LEFT JOIN sobre TablaExamenFisico con variable ''Fecha de realización de colonoscopia tamizaje'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Colonoscopia de tamizaje; Paciente; Tipo de documento de identidad; Régimen de afiliación (Contributivo/Subsidiado/Vinculado/Particular/Desplazado); EAPB (Entidad Administradora de Planes de Beneficios); Centro de atención; Ingreso/Admisión; Examen físico', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewColonoscopias: Devuelve una fila por examen físico cuyo NOMBRE_VARIABLE=''Resultado de colonoscopia de tamizaje'', uniendo opcionalmente la fecha desde otro registro con NOMBRE_VARIABLE=''Fecha de realización de colonoscopia tamizaje'' del mismo NUMINGRES.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.IPTIPODOC entre 1 y 12 → Mapea a etiquetas estándar de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE) else Etiqueta ''OTRO''; si PAC.IPSEXOPAC = 1 o 2 → Asigna ''MASCULINO'' o ''FEMENINO'' respectivamente else NULL (no se contempla otro valor); si PAC.IPTIPOPAC entre 1 y 8 → Mapea régimen: Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado Reg. Contributivo, Desplazado Reg. Subsidiado, Desplazado No Asegurado', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Report.TablaExamenFisico; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADINGRESO; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColonoscopias';
GO
