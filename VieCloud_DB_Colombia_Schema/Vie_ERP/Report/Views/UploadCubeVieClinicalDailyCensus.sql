

--CREATE PROCEDURE [Hospitalization].[SP_CENSO_DIARIO]
--	 @Centro char(80) 
--AS
CREATE view [Report].[UploadCubeVieClinicalDailyCensus] as

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE c.iptipodoc 
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
			WHEN 13 THEN 'PT'
			WHEN 14 THEN 'DE'
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',--[TipoIdentificacion],
		COALESCE (NULLIF (A.IPCODPACI,''''), '''') AS 'NRO IDENTIFICACION',--[NroIdentificacion],
        RTRIM(C.IPNOMCOMP) AS 'PACIENTE',--[Paciente],
		DATEDIFF(YEAR, C.IPFECNACI, GETDATE()) AS 'EDAD',--[Edad],
        IIF(CG.CAREGROUPTYPE = 3,
                (SELECT TOP(1) HA.NAME AS ENTIDAD
                FROM BILLING.SETTINGSBILLING AS F
                INNER JOIN CONTRACT.HEALTHADMINISTRATOR AS HA ON F.PARTICULARHEALTHADMINISTRATORID=HA.ID),K.NAME) AS 'ENTIDAD',--[Entidad],
        CASE K.ENTITYTYPE
            WHEN 1 THEN 'EPS CONTRIBUTIVO'
            WHEN 2 THEN 'EPS SUBSIDIADO'
            WHEN 3 THEN 'ET VINCULADO MUNICIPIO'
            WHEN 4 THEN 'ET VINCULADOS DAPARTAMENTO'
            WHEN 5 THEN 'ARL RIESGO LABORALES'
            WHEN 6 THEN 'MP MEDICINA PREPAGADA'
            WHEN 7 THEN 'IPS PRIVADA'
            WHEN 8 THEN 'IPS PUBLICA'
            WHEN 9 THEN 'REGIMEN ESPECIAL'
            WHEN 10 THEN 'ACCIDENTE DE TRANSITO'
            WHEN 11 THEN 'FOSYGA'
            WHEN 12 THEN 'OTROS'
        END AS 'REGIMEN',--[Regimen],
		CG.name AS 'GRUPO ATENCION',--[GrupoAtencion],
		D.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion],
        E.UFUDESCRI AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		A.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
		J.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
		UPPER(RTRIM(B.DESCCAMAS)) AS 'CAMA',--[Cama],
		A.FECINIEST AS 'FECHA INICIO',--[FechaInicio],
		DATEDIFF(DAY, A.FECINIEST, GETDATE()) + 1 AS 'DIAS',--[Dias],
		RTRIM(F.DESTIPEST) AS 'ESTANCIA',--[Estancia],
		egr.fecaltpac AS 'FECHA ALTA MEDICA',--[FechaAltaMedica],
        RTRIM(PROF.CODPROSAL) + ' - ' + RTRIM(PROF.NOMMEDICO) AS 'MEDICO',--[Medico],
        RTRIM(ESPE.CODESPECI) + ' - ' + RTRIM(ESPE.DESESPECI) AS 'ESPECIALIDAD',--[Especialidad],
        DI.CODDIAGNO AS 'CODIGO DIAGNOSTICO',--[CodDiagnostico],
        DI.NOMDIAGNO AS 'DIAGNOSTICO',--[Diagnostico],
		A.FECINIEST [FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM DBO.CHREGESTA A
	INNER JOIN DBO.CHCAMASHO B ON A.CODICAMAS=B.CODICAMAS AND A.REGESTADO = 1
	INNER JOIN DBO.INPACIENT C ON A.IPCODPACI=C.IPCODPACI
	INNER JOIN DBO.ADCENATEN D ON B.CODCENATE=D.CODCENATE --AND D.NOMCENATE = @Centro
	INNER JOIN DBO.INUNIFUNC E ON B.UFUCODIGO=E.UFUCODIGO
	INNER JOIN DBO.CHTIPESTA F ON A.CODTIPEST=F.CODTIPEST
	INNER JOIN DBO.ADINGRESO J ON A.NUMINGRES=J.NUMINGRES
	INNER JOIN CONTRACT.HEALTHADMINISTRATOR K ON J.GENCONENTITY = K.ID
	INNER JOIN CONTRACT.CAREGROUP AS CG ON CG.ID =J.GENCAREGROUP
	LEFT JOIN DBO.INPROFSAL PROF ON A.CODPROSAL = PROF.CODPROSAL
	LEFT JOIN DBO.INESPECIA ESPE ON A.CODESPECI = ESPE.CODESPECI
	LEFT JOIN DBO.INDIAGNOP DIA WITH (NOLOCK) ON J.NUMINGRES=DIA.NUMINGRES AND DIA.CODDIAPRI=1
	LEFT JOIN DBO.INDIAGNOS DI WITH (NOLOCK) ON DI.CODDIAGNO=DIA.CODDIAGNO 
	LEFT JOIN dbo.hcregegre AS egr ON j.numingres = egr.numingres

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el censo diario de pacientes hospitalizados activos con datos demográficos, administrativos, clínicos y de estancia para alimentar un cubo analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalDailyCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros de estancia con estado activo (REGESTADO=1); Cada estancia debe tener cama, paciente, centro de atención, unidad funcional, tipo de estancia, ingreso, entidad administradora de salud y grupo de atención asociados; Para resolver la entidad como ''particular'' debe existir configuración en BILLING.SETTINGSBILLING con PARTICULARHEALTHADMINISTRATORID válido', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalDailyCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan estancias activas (REGESTADO=1); El diagnóstico mostrado siempre es el principal (CODDIAPRI=1); La edad se calcula en años completos por diferencia entre fecha de nacimiento y fecha actual; Los días de estancia incluyen el día de inicio (DATEDIFF + 1); La identificación de empresa proviene del nombre de la base de datos actual (DB_NAME); Para pacientes ''particulares'' la entidad se sustituye por la configurada en facturación, no por la del ingreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalDailyCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Censo diario hospitalario; Paciente; Tipo de identificación; Ingreso hospitalario; Cama; Centro de atención; Unidad funcional; Estancia; Alta médica; Médico tratante; Especialidad; Diagnóstico principal; Entidad administradora de salud; Régimen (EPS, ARL, Medicina Prepagada, IPS, Fosyga, etc.); Grupo de atención; Paciente particular', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalDailyCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalDailyCensus: Devuelve una fila por estancia hospitalaria con REGESTADO=1, calculando edad, días de estancia (DATEDIFF desde FECINIEST + 1) y marca de tiempo en zona ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalDailyCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.iptipodoc entre 1 y 15 → Mapea a código de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI) else NULL; si CG.CAREGROUPTYPE = 3 (grupo de atención particular) → Toma el nombre de la entidad desde CONTRACT.HEALTHADMINISTRATOR usando PARTICULARHEALTHADMINISTRATORID de BILLING.SETTINGSBILLING else Usa el nombre de la entidad K.NAME asociada al ingreso; si K.ENTITYTYPE entre 1 y 12 → Clasifica el régimen (EPS Contributivo, EPS Subsidiado, ET Vinculado Municipio/Departamento, ARL, MP, IPS Privada/Pública, Régimen Especial, Accidente de Tránsito, Fosyga, Otros) else NULL; si Existe diagnóstico principal (DIA.CODDIAPRI=1) para el ingreso → Incluye código y descripción de diagnóstico principal else Diagnóstico nulo (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalDailyCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.CHREGESTA; DBO.CHCAMASHO; DBO.INPACIENT; DBO.ADCENATEN; DBO.INUNIFUNC; DBO.CHTIPESTA; DBO.ADINGRESO; CONTRACT.HEALTHADMINISTRATOR; CONTRACT.CAREGROUP; DBO.INPROFSAL; DBO.INESPECIA; DBO.INDIAGNOP; DBO.INDIAGNOS; DBO.HCREGEGRE; BILLING.SETTINGSBILLING', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalDailyCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalDailyCensus';
GO
