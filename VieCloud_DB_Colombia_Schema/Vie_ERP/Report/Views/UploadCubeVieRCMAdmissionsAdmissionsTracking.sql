

--CREATE PROCEDURE [Billing].[SP_INGRESOS_ABIERTOS]
--DECLARE	@ini_date AS DATE='2024-06-01';
--DECLARE	@end_date AS DATE='2024-06-30';

--AS
CREATE view [Report].[UploadCubeVieRCMAdmissionsAdmissionsTracking] as 

	SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE p.iptipodoc 
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
			WHEN 15 THEN 'SI' END AS 'TIPO DOCUMENTO',--[TipoDocumento],
			CASE
				WHEN I.IPCODPACI IS NULL THEN '0'
				ELSE I.IPCODPACI
			END AS 'NRO IDENTIFICACION',--[NroIdentificacion],
			CASE
				WHEN P.GENEXPEDITIONCITY IS NULL
					AND CI.NAME IS NULL THEN '0 - NO_APLICA'
				ELSE CAST(P.GENEXPEDITIONCITY AS VARCHAR(20)) + ' - ' + ISNULL(RTRIM(CI.NAME), '')
			END AS 'LUGAR EXPEDICION',--[LugarExpedicion],
			CASE
				WHEN P.IPNOMCOMP IS NULL THEN 'NO_APLICA'
				ELSE RTRIM(P.IPNOMCOMP)
			END AS 'NOMBRE PACIENTE',--[NombrePaciente],
			DATEDIFF(YEAR,P.IPFECNACI, I.IFECHAING) AS 'EDAD',--[Edad],
			CASE
				WHEN UBINOMBRE IS NULL THEN 'NO_APLICA'
				ELSE UBINOMBRE
			END AS 'UBICACION',--[Ubicacion],
			CASE
				WHEN MUNNOMBRE IS NULL THEN 'NO_APLICA'
				ELSE MUNNOMBRE
			END AS 'MUNICIPIO',--[Municipio],
			CASE
				WHEN P.IPTELEFON IS NULL
					OR P.IPTELEFON LIKE '' THEN '0'
				ELSE P.IPTELEFON
			END AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal],
			CASE
				WHEN P.IPTELMOVI IS NULL
					OR P.IPTELMOVI LIKE '' THEN '0'
				ELSE P.IPTELMOVI
			END AS 'TELEFONO ALTERNATIVO',--[TelefonoAlternativo],
			IIF(GA.CODE IS NULL, ISNULL(EA.CODE, '0'), ISNULL(GA.CODE, '0')) AS 'CODIGO GRUPO ATENCION',--[CodigoGrupoAtencion],
			IIF(GA.NAME IS NULL, ISNULL(EA.NAME, 'NO_APLICA'), ISNULL(GA.NAME, 'NO_APLICA')) AS 'GRUPO ATENCION',--[GrupoAtencion],
			CASE
				WHEN EA.NAME IS NULL THEN 'NO_APLICA'
				ELSE EA.NAME
			END AS 'ENTIDAD',--[Entidad],
			DGE.GRUPO_ETAREO_CICLO_VITAL AS 'GRP ETAREO CICLO VITAL',--[GrpEtareoCicloVital],
			DGE.GRUPO_ETAREO_RES_5268 AS 'GRP ETAREO RES 5268',--[GrpEtareoRes5268],
			DGE.GRUPO_ETAREO_UPC AS 'GRP ETAREO UPS',--[GrpEtareoUPS],
			CASE
				WHEN I.CODCENATE IS NULL THEN '0'
				ELSE I.CODCENATE
			END AS 'CODIGO CENTRO ATENCION',--[CodigoCentroAtencion],
			CASE
				WHEN AD.NOMCENATE IS NULL THEN 'NO_APLICA'
				ELSE AD.NOMCENATE
			END AS 'CENTRO ATENCION',--[CentroAtencion],
			CASE
				WHEN UF.UFUDESCRI IS NULL THEN 'NO_APLICA'
				ELSE UF.UFUDESCRI
			END AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
			I.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
			I.IFECHAING AS 'FECHA INGRESO',--[FechaIngreso],
			YEAR(I.IFECHAING) 'AÑO INGRESO',--[AñoIngreso],
			CASE MONTH(I.IFECHAING)
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
				ELSE 'NO_APLICA'
			END 'MES FECHA INGRESO',--[MesFechaIngreso],
			MONTH(I.IFECHAING) 'NRO MES FECHA INGRESO',--[NroMesFechaIngreso],
			DAY(I.IFECHAING) 'DIA INGRESO',--[DiaIngreso],
			CASE I.IESTADOIN
				WHEN '  ' THEN 'SIN CONFIRMAR HOJA DE TRABAJO'
				WHEN 'F' THEN 'CONFIRMADA HOJA DE TRABAJO'
				WHEN 'A' THEN 'ANULADO'
				WHEN 'C' THEN 'CERRADO'
				WHEN 'P' THEN 'FACTURADO PARCIAL'
				ELSE 'NO_APLICA'
			END AS 'ESTADO',--[Estado],
			EM.FECALTPAC AS 'FECHA ALTA MEDICA',--[FechaAltaMedica],
			CASE MONTH(EM.FECALTPAC)
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
				ELSE 'NO_APLICA'
			END 'MES ALTA MEDICA',--[MesAltaMedica],
			CASE I.IESTADOIN
				WHEN '  ' THEN DATEDIFF(DAY, EM.FECALTPAC, GETDATE())
				WHEN 'F' THEN 0
				WHEN 'A' THEN 0
				WHEN 'C' THEN 0
				WHEN 'P' THEN DATEDIFF(DAY, EM.FECALTPAC, GETDATE())
				ELSE 0
			END AS 'DIA ALTA MEDICA',--[DiaAltaMedica],
			CASE
				WHEN I.CODDIAING IS NULL THEN '0'
				ELSE I.CODDIAING
			END AS 'CIE10 INGRESP',--[CIE10Ingreso],
			CASE
				WHEN CIE10.NOMDIAGNO IS NULL THEN 'NO_APLICA'
				ELSE CIE10.NOMDIAGNO
			END AS 'DIAGNOSTICO INGRESO',--[DiagnosticoIngreso],
			CASE
				WHEN I.CODDIAEGR IS NULL THEN 'NO_APLICA'
				ELSE I.CODDIAEGR
			END AS 'CIE10 EGRESO',--[CIE10Egreso],
			CASE
				WHEN DI.NOMDIAGNO IS NULL THEN 'NO_APLICA'
				ELSE DI.NOMDIAGNO
			END AS 'DIAGNOSTICO EGRESO',--[DiagnosticoEgreso],
			ISNULL(SEG2.CODUSUARI, UU.CODUSUARI) 'CODIGGO USUARIO CREO',--[CodUsuarioCreo],
			ISNULL(SEG2.NOMUSUARI, UU.NOMUSUARI) 'USUARIO CREO',-- [UsuarioCreo],
			I.FECREGCRE AS 'FECHA CREACION',--[FechaCreacion],
			CASE
				WHEN I.CODUSUMOD IS NULL THEN '0'
				ELSE I.CODUSUMOD
			END AS 'CODIGO USUARIO MODIFICO',--[CodUsuarioModifico],
			CASE
				WHEN UU2.NOMUSUARI IS NULL THEN 'NO_APLICA'
				ELSE UU2.NOMUSUARI
			END AS 'USUARIO MODIFICO',--[UsuarioModifico],
			I.FECREGMOD AS 'FECHA MODIFICO',--[FechaModificacion],
			CASE
				WHEN D.UFUDESCRI IS NULL THEN 'NO_APLICA'
				ELSE D.UFUDESCRI
			END AS 'UNIDAD ACTUAL',--[UnidadActual],
			CASE
				WHEN I.IOBSERVAC IS NULL
					OR I.IOBSERVAC LIKE '' THEN 'NO APLICA'
				ELSE I.IOBSERVAC
			END AS 'ONSERVACIONES',--[Observaciones],
			CASE I.TIPOINGRE
				WHEN 1 THEN 'AMBULATORIO'
				WHEN 2 THEN 'HOSPITALARIO'
			END AS 'TIPO INGRESO',--[TipoIngreso],
			CASE
				WHEN CIE10_ac.NOMDIAGNO IS NULL THEN 'NO_APLICA'
				ELSE UPPER(CIE10_ac.NOMDIAGNO)
			END AS 'ENFERMEDAD ACTUAL',--[EnfermedadActual],
			CASE I.ICAUSAING
				WHEN '1' THEN 'HERIDOS EN COMBATE'
				WHEN '2' THEN 'ENFERMEDAD PROFESIONAL'
				WHEN '3' THEN 'ENFERMEDAD GENERAL ADULTO'
				WHEN '4' THEN 'ENFERMEDAD GENERAL PEDIATRIA'
				WHEN '5' THEN 'ODONTOLOGÍA'
				WHEN '6' THEN 'ACCIDENTE DE TRANSITO'
				WHEN '7' THEN 'CATASTROFE/FISALUD'
				WHEN '8' THEN 'QUEMADOS'
				WHEN '9' THEN 'MATERNIDAD'
				WHEN '10' THEN 'ACCIDENTE LABORAL'
				WHEN '11' THEN 'CIRUGIA PROGRAMADA'
				ELSE 'NO_APLICA'
			END AS 'CAUSA INGRESO',--[CausaIngreso],
			CASE I.ITIPORIES
				WHEN 1 THEN 'ENFERMEDAD GENERAL Y MATERNIDAD'
				WHEN 2 THEN 'ACCIDENTE DE TRANSITO'
				WHEN 3 THEN 'CATASTROFE'
				ELSE 'NO_APLICA'
			END AS 'MOTIVO INGRESO',--[MotivoIngreso],
			IIF(G.ADMISSIONNUMBER IS NULL,'SIN SERVICIOS','CON SERVICIOS') AS 'ESTADO CARGUES',--[EstadoCargues],
			CASE
				WHEN EM.FECALTPAC IS NULL THEN 'SIN ALTA MÉDICA'
				ELSE 'CON ALTA MÉDICA'
			END AS 'INGRESO ALTA MEDICA',--[IngresoAltaMedica],
		    CAST(i.ifechaing AS DATE) [FECHA BUSQUEDA],
            CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM DBO.ADINGRESO AS I WITH (NOLOCK)
	LEFT JOIN DBO.INUNIFUNC AS UF WITH (NOLOCK) ON UF.UFUCODIGO = I.UFUCODIGO
	LEFT JOIN CONTRACT.CAREGROUP AS GA WITH (NOLOCK) ON GA.ID = I.GENCAREGROUP
	LEFT JOIN [Security].[UserINT] AS U WITH (NOLOCK) ON U.USERCODE = I.CODUSUCRE
	LEFT JOIN DBO.SEGUSUARU AS U2 WITH (NOLOCK) ON U2.CODUSUARI = I.CODUSUCRE
	LEFT JOIN [Security].[UserINT] AS UM WITH (NOLOCK) ON UM.USERCODE = I.CODUSUMOD
	LEFT JOIN SECURITY.PERSONINT AS PER WITH (NOLOCK) ON PER.ID = U.IDPERSON
	LEFT JOIN SECURITY.PERSONINT AS PERM WITH (NOLOCK) ON PERM .ID = UM.IDPERSON
	LEFT JOIN DBO.INPACIENT AS P WITH (NOLOCK) ON P.IPCODPACI = I.IPCODPACI
	LEFT JOIN CONTRACT.HEALTHADMINISTRATOR AS EA WITH (NOLOCK) ON EA.ID = I.GENCONENTITY
	LEFT JOIN COMMON.CITY AS CI WITH (NOLOCK) ON CI.ID = P.GENEXPEDITIONCITY
	LEFT JOIN (SELECT HC.NUMEFOLIO, HC.NUMINGRES, HC.IPCODPACI, CODDIAGNO FROM DBO.HCHISPACA HC WITH (NOLOCK) INNER JOIN (SELECT MAX(NUMEFOLIO) NUMEFOLIO, NUMINGRES, IPCODPACI 
				FROM DBO.HCHISPACA WITH (NOLOCK) GROUP BY NUMINGRES, IPCODPACI ) HC1 ON HC.NUMINGRES=HC1.NUMINGRES AND  HC.NUMEFOLIO=HC1.NUMEFOLIO AND HC.IPCODPACI=HC1.IPCODPACI) DA ON I.NUMINGRES=DA.NUMINGRES AND I.IPCODPACI=DA.IPCODPACI --DIAGNOSTICO ACTUAL
	LEFT JOIN DBO.INDIAGNOS AS CIE10 WITH (NOLOCK) ON CIE10.CODDIAGNO = I.CODDIAING
	LEFT JOIN DBO.INDIAGNOS AS CIE10_ac WITH (NOLOCK) ON CIE10_ac.CODDIAGNO = DA.CODDIAGNO
	LEFT JOIN DBO.ADINGRESO AS I2 WITH (NOLOCK) ON I2.NUMINGRES = I.NUMINGRES
	LEFT JOIN DBO.INUNIFUNC AS D WITH (NOLOCK) ON I2.UFUAACTHOS = D .UFUCODIGO
	LEFT JOIN DBO.HCREGEGRE AS EM WITH (NOLOCK) ON EM.IPCODPACI = I.IPCODPACI AND EM.NUMINGRES = I.NUMINGRES
	LEFT JOIN (SELECT MIN(TRIANUMER) TRIANUMER, NUMINGRES, MIN(CODCONCEC) CODCONCEC FROM DBO.ADTRIAGEU WITH (NOLOCK) WHERE CODCONCEC IS NOT NULL GROUP BY NUMINGRES) AS ADT ON ADT.NUMINGRES = I.NUMINGRES
	LEFT JOIN DBO.ADCONTURG AS ADCO WITH (NOLOCK) ON ADT.CODCONCEC = ADCO.CODCONCEC
	LEFT JOIN DBO.SEGUSUARU AS SEG2 WITH (NOLOCK) ON SEG2.CODUSUARI = ADCO.CODUSUARI
	LEFT JOIN DBO.SEGUSUARU AS UU WITH (NOLOCK) ON UU.CODUSUARI = I.CODUSUCRE
	LEFT JOIN DBO.SEGUSUARU AS UU2 WITH (NOLOCK) ON UU2.CODUSUARI = I.CODUSUMOD
	LEFT JOIN DBO.INUBICACI AS BB WITH (NOLOCK) ON BB.AUUBICACI = P.AUUBICACI
	LEFT JOIN DBO.INMUNICIP AS EE WITH (NOLOCK) ON EE.DEPMUNCOD = BB.DEPMUNCOD
	LEFT JOIN DBO.INDIAGNOS AS DI WITH (NOLOCK) ON I.CODDIAEGR = DI.CODDIAGNO
	LEFT JOIN DBO.ADCENATEN AS AD WITH (NOLOCK) ON I.CODCENATE = AD.CODCENATE 
	LEFT JOIN [REPORT].[Table_HOMO_GRUPO_ETARIO] HGE WITH(NOLOCK) ON DATEDIFF(YEAR, P.IPFECNACI,I.IFECHAING) = HGE.GRUPO_ETAREO_EDAD
	LEFT JOIN [REPORT].[Table_DIM_GRUPO_ETAREO] DGE WITH(NOLOCK) ON HGE.ID_GRUPO_ETAREO = DGE.ID_GRUPO_ETAREO
	LEFT JOIN (
		SELECT AdmissionNumber FROM BILLING.REVENUECONTROL AS RC WITH(NOLOCK) 
		INNER JOIN dbo.ADINGRESO AS ING ON ING.NUMINGRES = RC.AdmissionNumber
		WHERE ING.IESTADOIN <> 'F' AND ING.IESTADOIN <> 'A' AND ING.IESTADOIN <> 'C'
		GROUP BY AdmissionNumber) AS G ON G.AdmissionNumber = I.NUMINGRES	
	--WHERE i.ifechaing BETWEEN @ini_date AND @end_date

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de seguimiento de admisiones para carga a cubo RCM, consolidando datos del paciente, ingreso, diagnósticos, entidad, grupo de atención, alta médica y estado de cargue de servicios.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMAdmissionsAdmissionsTracking';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de ingresos, pacientes, diagnósticos CIE10, usuarios y administradoras de salud deben estar pobladas para resolver descripciones; ausencias se sustituyen por ''NO_APLICA'' o ''0''.; La tabla Report.Table_HOMO_GRUPO_ETARIO debe contener las edades calculadas (años entre fecha de nacimiento e ingreso) para clasificar grupos etáreos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMAdmissionsAdmissionsTracking';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los campos string nulos se normalizan a ''NO_APLICA'' o ''0'' para evitar valores nulos en el cubo.; La edad se calcula como diferencia en años entre fecha de nacimiento y fecha de ingreso.; Para diagnóstico actual se toma siempre el folio máximo (último) por NUMINGRES+IPCODPACI en HCHISPACA.; El triage considerado es el de menor TRIANUMER y menor CODCONCEC con concepto no nulo por ingreso.; El ID_COMPANY se deriva del nombre de la base de datos actual truncado a 9 caracteres.; La marca de última actualización se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Sólo ingresos en estados distintos de Confirmado/Anulado/Cerrado se consideran ''CON SERVICIOS'' aunque tengan registros en REVENUECONTROL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMAdmissionsAdmissionsTracking';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Tipo de documento; Lugar de expedición; Diagnóstico CIE10 ingreso/egreso; Alta médica; Triage de urgencias; Grupo de atención; Entidad/Administradora de salud; Centro de atención; Unidad funcional; Grupo etáreo (Ciclo Vital, Resolución 5268, UPC); Causa de ingreso (accidente tránsito, maternidad, enfermedad profesional, etc.); Tipo de ingreso (ambulatorio/hospitalario); Estado de cargue de servicios (Revenue Control); Facturación parcial/cerrada/anulada', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMAdmissionsAdmissionsTracking';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMAdmissionsAdmissionsTracking: Devuelve un conjunto DISTINCT de admisiones con datos demográficos, diagnósticos de ingreso/egreso, alta médica, estado de cargue de servicios y marca temporal en zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMAdmissionsAdmissionsTracking';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iptipodoc del paciente entre 1 y 15 → Mapea a sigla de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si IESTADOIN del ingreso (''  '', ''F'', ''A'', ''C'', ''P'') → Traduce estado a ''SIN CONFIRMAR HOJA DE TRABAJO'', ''CONFIRMADA HOJA DE TRABAJO'', ''ANULADO'', ''CERRADO'' o ''FACTURADO PARCIAL'' respectivamente; si IESTADOIN = ''  '' o ''P'' → Calcula días desde alta médica hasta hoy (DATEDIFF FECALTPAC, GETDATE) else Para ''F'',''A'',''C'' o cualquier otro, días desde alta = 0; si TIPOINGRE = 1 o 2 → Clasifica ingreso como ''AMBULATORIO'' u ''HOSPITALARIO''; si ICAUSAING entre ''1'' y ''11'' → Mapea causa de ingreso (combate, enfermedad profesional, general adulto/pediatría, odontología, accidente tránsito, catástrofe/Fisalud, quemados, maternidad, accidente laboral, cirugía programada) else ''NO_APLICA''; si ITIPORIES = 1, 2 o 3 → Mapea motivo a ''ENFERMEDAD GENERAL Y MATERNIDAD'', ''ACCIDENTE DE TRANSITO'' o ''CATASTROFE'' else ''NO_APLICA''; si Existe registro en BILLING.REVENUECONTROL para la admisión cuyo ingreso no esté en estado ''F'',''A'' ni ''C'' → Marca ''CON SERVICIOS'' en estado de cargues else ''SIN SERVICIOS''; si FECALTPAC IS NULL en HCREGEGRE → ''SIN ALTA MÉDICA'' else ''CON ALTA MÉDICA''; si GA.CODE/NAME (grupo atención de contrato) IS NULL → Usa código/nombre de la administradora EA (HEALTHADMINISTRATOR) como grupo de atención else Usa el grupo de atención del contrato', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMAdmissionsAdmissionsTracking';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.ADINGRESO; DBO.INUNIFUNC; CONTRACT.CAREGROUP; Security.UserINT; DBO.SEGUSUARU; Security.PERSONINT; DBO.INPACIENT; CONTRACT.HEALTHADMINISTRATOR; COMMON.CITY; DBO.HCHISPACA; DBO.INDIAGNOS; DBO.HCREGEGRE; DBO.ADTRIAGEU; DBO.ADCONTURG; DBO.INUBICACI; DBO.INMUNICIP; DBO.ADCENATEN; Report.Table_HOMO_GRUPO_ETARIO; Report.Table_DIM_GRUPO_ETAREO; BILLING.REVENUECONTROL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMAdmissionsAdmissionsTracking';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMAdmissionsAdmissionsTracking';
GO
