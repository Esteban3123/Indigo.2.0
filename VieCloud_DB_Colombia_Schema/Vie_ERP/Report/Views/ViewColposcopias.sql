

/*******************************************************************************************************************
Nombre: [Report].[ViewColposcopias]
Tipo:Vista
Observacion:Reporte sobre Colposcopia
Profesional: Nilsson Miguel Galindo Lopez
Fecha:17-11-2022
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
CREATE VIEW [Report].[ViewColposcopias]
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
FIS.VALOR AS [RESULTADO COLONOSCOPIA],
FEC.VALOR AS [FECHA COLONOSCOPIA],
ING.IFECHAING AS [FECHA INGRESO],
1 as 'CANTIDAD',
CAST(ING.IFECHAING AS date) AS 'FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
[Report].[TablaExamenFisico] FIS INNER JOIN
DBO.INPACIENT PAC ON FIS.IPCODPACI=PAC.IPCODPACI AND NOMBRE_VARIABLE='Resultado de colposcopia' INNER JOIN 
DBO.INENTIDAD EAPB ON PAC.CODENTIDA=EAPB.CODENTIDA LEFT JOIN
dbo.ADINGRESO ING ON FIS.NUMINGRES=ING.NUMINGRES LEFT JOIN
DBO.ADCENATEN CEN ON ING.CODCENATE=CEN.CODCENATE LEFT JOIN
[Report].[TablaExamenFisico] FEC ON FIS.NUMINGRES=FEC.NUMINGRES AND FEC.NOMBRE_VARIABLE='Fecha de realización de colposcopia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida información de colposcopias realizadas a pacientes. Cruza datos del examen físico (resultado y fecha de realización de colposcopia) con el episodio de ingreso, el centro de atención, la entidad aseguradora (EAPB) y la información demográfica del paciente, incluyendo edad calculada al momento del ingreso, tipo de documento, régimen de afiliación, teléfonos de contacto y sexo. Está diseñada para consumo en reportes de seguimiento de este procedimiento ginecológico por parte de la institución.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la información de pacientes con resultados de colposcopia, incluyendo datos demográficos, afiliación, ingreso asociado y fecha de realización del procedimiento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla Report.TablaExamenFisico debe contener registros con NOMBRE_VARIABLE=''Resultado de colposcopia'' para que el paciente aparezca en el reporte.; El paciente debe existir en DBO.INPACIENT y tener entidad (EAPB) en DBO.INENTIDAD.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY se obtiene de DB_NAME() truncado a 9 caracteres, identificando la base de datos/empresa actual.; La edad se calcula en años completos como FLOOR de la diferencia entre la fecha de ingreso y la fecha de nacimiento usando formato YYYYMMDD dividido entre 10000.; Se asigna siempre CANTIDAD=1 a cada fila para conteos en reportes.; ULT_ACTUAL refleja la fecha/hora actual convertida a la zona horaria ''Pakistan Standard Time''.; El JOIN con ingreso, centro de atención y fecha de colposcopia es LEFT, por lo que el reporte conserva pacientes con resultado de colposcopia aunque no tengan ingreso, centro o fecha registrada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Colposcopia; Paciente; EAPB (entidad aseguradora); Régimen de afiliación; Tipo de documento; Centro de atención; Ingreso/Admisión; Examen físico', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewColposcopias: Devuelve una fila por cada registro de TablaExamenFisico cuyo NOMBRE_VARIABLE sea ''Resultado de colposcopia'', enriquecido con datos del paciente, EAPB, ingreso, centro de atención y fecha de colposcopia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.IPTIPODOC entre 1 y 12 → Mapea a etiquetas de tipo de documento: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU, 9=NV, 10=CD, 11=SC, 12=PE else Devuelve ''OTRO''; si PAC.IPSEXOPAC = 1 → Sexo=''MASCULINO'' else Si =2 entonces ''FEMENINO''; si PAC.IPTIPOPAC entre 1 y 8 → Mapea régimen: 1=Contributivo, 2=Subsidiado, 3=Vinculado, 4=Particular, 5=Otro, 6=Desplazado Reg. Contributivo, 7=Desplazado Reg. Subsidiado, 8=Desplazado No Asegurado', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Report.TablaExamenFisico; DBO.INPACIENT; DBO.INENTIDAD; dbo.ADINGRESO; DBO.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewColposcopias';
GO
