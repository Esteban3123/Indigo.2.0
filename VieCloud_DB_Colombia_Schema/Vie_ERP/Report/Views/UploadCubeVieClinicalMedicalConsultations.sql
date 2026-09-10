

CREATE view [Report].[UploadCubeVieClinicalMedicalConsultations] AS

	WITH CTE_LISTADO_FINAL AS
	(

		SELECT HIS.IPCODPACI,HIS.NUMINGRES ,HIS.NUMEFOLIO ,HIS.FECHISPAC FROM
		dbo.HCHISPACA AS HIS INNER JOIN
		dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES =HIS.NUMINGRES
		WHERE HIS.HCOTROSPROC IS NULL AND (HIS.GENCONEXT = 1) AND (HIS.TIPHISPAC = 'I') 
		--AND CAST(HIS.FECHISPAC AS DATE) BETWEEN  @FechaInicio AND @FechaFin 

	),CTE_FACTURACION AS
	(

		SELECT AdmissionNumber AS INGRESO ,InvoiceNumber AS FACTURA ,CUPS.Code AS CODIGO_CUP ,CUPS.Description CUPS ,CDD.Code AS CODIGO_RELACIONADO ,CDD.Name AS DESCRIPCION_RELACIONADA
			,F.InvoiceDate AS 'FECHA FACTURA'
			,DF.TotalSalesPrice AS 'VALOR SERVICIO'
			,F.InvoiceValue AS 'VALOR FACTURA'
		FROM Billing.Invoice AS F
		LEFT JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
		LEFT JOIN Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = DF.ServiceOrderDetailId
		LEFT JOIN Contract.CUPSEntity AS CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
		LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD WITH (NOLOCK) ON CECD.ID=SOD.CUPSEntityContractDescriptionId
		LEFT JOIN Contract.ContractDescriptions AS CDD WITH (NOLOCK) ON CECD.ContractDescriptionId =CDD.Id
		LEFT JOIN CTE_LISTADO_FINAL AS FIN ON FIN.NUMINGRES =F.AdmissionNumber 
		WHERE F.Status =1 AND CUPS.ServiceType IN (6,8)

	), CTE_PACIENTES_INGRESOS AS
	(

		SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
			CASE PAC.IPTIPODOC 	
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
				WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',-- [TipoIdentificacion], 

			HIS.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion], 
			RTRIM(PAC.IPNOMCOMP) AS 'NOMBRE PACIENTE',--[NombrePaciente], 

			CASE PAC.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS 'SEXO',--[Sexo],
			CASE PAC.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS 'DESCRIPCION SEXO',--[DescripcionSexo],
			CAST(PAC.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO',--[FechaNacimiento], 
			cast(DATEDIFF(YEAR, PAC.IPFECNACI, GETDATE()) as char) AS 'EDAD',--[Edad], 
			PAC.IPTELEFON AS 'TELEFONO PRINICPAL',--[TelPrincipal],
			PAC.IPTELMOVI 'TELEFONO ALTERNATIVO',--[TelAlternativo],
			UPPER(PAC.IPDIRECCI) AS 'DIRECCION',--[Direccion],
			DEP.depcodigo AS 'CODIGO DEPARTAMENTO',--[CodDepartamento],
			DEP.nomdepart AS 'DEPARTAMETO',--[Departamento],
			MUN.MUNCODIGO 'CODIGO MUNICIPIO',--[CodMunicipio], 
			MUN.MUNNOMBRE AS 'MUNICIPIO',--[Municipio],
			TP.Nit AS 'NIT',--[NIT],
			HA.HealthEntityCode 'CODIGO EPS',--[CodEPS],
			HA.Name AS 'EPS',--[EPS],
			CASE HA.EntityType 
				WHEN 1 THEN 'EPS Contributivo' 
				WHEN 2 THEN 'EPS Subsidiado' 
				WHEN 3 THEN 'ET Vinculados Municipios' 
				WHEN 4 THEN 'ET Vinculados Departamentos'
				WHEN 5 THEN 'ARL Riesgos Laborales' 
				WHEN 6 THEN 'MP Medicina Prepagada' 
				WHEN 7 THEN 'IPS Privada' 
				WHEN 8 THEN 'IPS Publica' 
				WHEN 9 THEN 'Regimen Especial' 
				WHEN 10 THEN 'Accidentes de transito'
				WHEN 11 THEN 'Fosyga' 
				WHEN 99 THEN 'Particulares'
				WHEN 12 THEN 'Otros' else 'Otros' end as 'REGIMEN',-- [Regimen],
			--CG.Code [CODIGO GRUPO ATENCION],
			CG.Name AS 'GRUPO ATENCION',--[GrpAtencion],

			HIS.NUMINGRES AS 'NRO INRESO',--[NroIngreso],
			ing.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
			CASE WHEN CS.NUMCONCIT IS NULL THEN '' ELSE CS.NUMINGRES END AS 'INGRESO DESDE CONTROL',--[IngresoDesdeControl], 
			CEN.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion],
			--HIS.UFUCODIGO AS CODIGO_UNIDAD_FUNCIONAL, 
			UNI.UFUDESCRI AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
			HIS.CODPROSAL AS 'IDENTIFICACION MEDICO',--[IdentificacionMedico], 
			PRO.NOMMEDICO AS 'MEDICO',--[Medico], 
			HIS.CODESPTRA AS 'CODIGO ESPECIALIDAD',--[CodEspecialidad],
			ESP.DESESPECI AS 'ESPECIALIDAD',--[Especialidad], 
			AGA.CODACTMED AS 'CODIGO ACTIVIDAD MEDICA',--[CodActividadMedica], 
			AGAC.DESACTMED AS 'ACTIVIDAD MEDICA',--[ActividadMedica],
			HIS.FECHISPAC AS 'FECHA HISTORIA',--[FechaHistoria], 
			HIS.CODDIAGNO AS 'CODIGO DIAGNOSTICO',--[CodDiagnostico], 
			DIA.NOMDIAGNO AS 'DIAGNOSTICO',--[Diagnostico],
			CASE dxp.tipdiagno 
				WHEN 'I' THEN 'Impresion Diagnostica' 
				WHEN 'C' THEN 'Confirmado Nuevo' 
				WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',--[TipoDiagnostico],
			CASE AGA.CODESTCIT 
				WHEN 0 THEN 'ASIGNADA' 
				WHEN 1 THEN 'CUMPLIDA'
				WHEN 2 THEN 'INCUMPLIDA' 
				WHEN 3 THEN 'PREASIGNADA' 
				WHEN 4 THEN 'CITA CANCELADA' END 'ESTADO CITA',--[EstadoCita],
			CASE URG.TIPCITMED WHEN 1 THEN 'PRIMERA VEZ' WHEN 2 THEN 'CONTROL' ELSE 'N/A' END AS 'TIPO CITA',--[TipoCita],
			AGA.FECREGSIS AS 'FECHA SOLICITUD CITA',--[FechaSolicitudCita], 
			AGA.FECHORAIN AS 'FECHA ASIGNACION CITA',--[FechaAsignacionCita], 
			AGA.FECHAOFERTADA AS 'FECHA MEJOR CITA DISPONIBLE',--[FechaMejorCitaDisponible],
			AGA.FECITADES AS 'FECHA DESEADA CITA',--[FechaDeseadaCita] , 
			URG.FECHINIHI AS 'FECHA INICIAL ATENCION',--[FechaIniciaAtencion], 
			URG.FECHFINH  AS 'FECHA FIN ATNCION',--[FechaFinAtencion], 
			CAST(DATEDIFF (MINUTE,URG.FECHINIHI,URG.FECHFINH ) AS CHAR) 'TIEMPO CONSULTA MINUTOS',--[TiempoConsultaMinutos],
			CASE AGA.MODALIDAD WHEN 0 THEN 'Presencial' WHEN 1 THEN 'Teleconsulta' ELSE 'N/A' END AS 'MODALIDAD',-- [Modalidad],
			--IIF (G.IPCODPACI IS NULL,'NO','SI') AS [ATENCIONES ANTERIORES POR LA ESPECIALIDAD],
			CASE WHEN (SELECT DISTINCT codesptra FROM dbo.hchispaca WHERE ipcodpaci = urg.ipcodpaci AND codesptra = urg.codesptra AND fechispac < his.fechispac) IS NULL THEN 'NO' ELSE 'SI' END AS 'ATENCION ANTERIOR ESPECIALIDAD',--[AtencionAnteriorEspecialidad],
			ING.IAUTORIZA AS 'NRO AUTORIZACION',-- [NroAutorizacion], 
			CASE 
				WHEN ING.IESTADOIN LIKE ' ' THEN 'SIN FACTURAR'
				WHEN G2.FACTURA IS NULL THEN 'NO FACTURABLE'
				WHEN G2.[VALOR SERVICIO]=0 THEN 'NO FACTURABLE'
				WHEN ING.IESTADOIN LIKE 'F' THEN 'FACTURADO' 
				WHEN ING.IESTADOIN LIKE 'A' THEN 'ANULADO' 
				WHEN ING.IESTADOIN LIKE 'C' THEN 'NO FACTURABLE' 
				WHEN ING.IESTADOIN LIKE 'P' THEN 'PARCIAL' END AS 'ESTADO FACTURACION',-- [EstadoFacturacion],
			ISNULL(G2.CODIGO_CUP, 'SIN DATO') AS 'CODIGO CUPS',--[CodCUPS], 
			ISNULL(G2.CUPS , 'SIN DATO') AS 'CUPS',--CUPS,
			ISNULL(G2.CODIGO_RELACIONADO, 'SIN DATO') AS 'CODIGO DESCRIPCION RELACIONADA',-- [CodDescripcionRelacionada],  
			ISNULL(G2.DESCRIPCION_RELACIONADA, 'SIN DATO') AS 'DESCRIPCOIN RELACIONADA',--[DescripcionRelacionada], 
			'PBS' AS 'TIPO SERVICIO',--[TipoServicio],
			ISNULL(G2.FACTURA, 'SIN DATO') AS 'FACTURA',--[Factura],
			CAST(G2.[FECHA FACTURA] AS DATE) 'FECHA FACTURA',--[FechaFactura],
			ISNULL([VALOR SERVICIO], 0) AS 'VALOR SERVICIO',--[ValorServicio],
			ISNULL([VALOR FACTURA],0) AS 'VALOR FACTURA',--[ValorFactura]
			CAST(HIS.FECHISPAC AS DATE) as 'FECHA BUSQUEDA',
			CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

		FROM dbo.HCURGING1 AS URG INNER JOIN
		dbo.HCHISPACA AS HIS WITH (NOLOCK) ON URG.IDETIPHIS = HIS.IDETIPHIS AND URG.IPCODPACI = HIS.IPCODPACI AND URG.NUMINGRES = HIS.NUMINGRES AND HIS.NUMEFOLIO = URG.NUMEFOLIO INNER JOIN
		dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES =URG.NUMINGRES INNER JOIN
		Contract.CareGroup AS CG WITH (NOLOCK) ON ING.GENCAREGROUP =CG.Id INNER JOIN
		Contract.HealthAdministrator AS HA WITH (NOLOCK) ON ING.GENCONENTITY =HA.Id INNER JOIN
		Common .ThirdParty AS TP WITH (NOLOCK) ON HA.ThirdPartyId =TP.Id INNER JOIN
		dbo.INPACIENT AS PAC WITH (NOLOCK) ON HIS.IPCODPACI = PAC.IPCODPACI INNER JOIN
		dbo.ADCENATEN AS CEN WITH (NOLOCK) ON HIS.CODCENATE = CEN.CODCENATE INNER JOIN
		dbo.INUNIFUNC AS UNI WITH (NOLOCK) ON HIS.UFUCODIGO = UNI.UFUCODIGO INNER JOIN
		dbo.INPROFSAL AS PRO WITH (NOLOCK) ON HIS.CODPROSAL = PRO.CODPROSAL INNER JOIN
		dbo.INESPECIA AS ESP WITH (NOLOCK) ON HIS.CODESPTRA = ESP.CODESPECI INNER JOIN
		CTE_LISTADO_FINAL FIN ON FIN.NUMINGRES = HIS.NUMINGRES AND FIN.NUMEFOLIO = HIS.NUMEFOLIO AND HIS.IPCODPACI = FIN.IPCODPACI LEFT JOIN
		dbo.INDIAGNOS AS DIA WITH (NOLOCK) ON HIS.CODDIAGNO = DIA.CODDIAGNO 
		INNER JOIN dbo.indiagnop AS dxp ON his.numingres = dxp.numingres AND his.coddiagno = dxp.coddiagno
		LEFT JOIN
		(SELECT CS2.* FROM DBO.ADCONCOEX CS2 INNER JOIN ( SELECT IPCODPACI,MAX(IPFECHACO) IPFECHACO, NUMINGRES FROM DBO.ADCONCOEX WHERE CONESTADO='3' --AND IPFECHACO BETWEEN @FechaInicio AND @FechaFin
		GROUP BY IPCODPACI, NUMINGRES) CS1 ON CS2.IPCODPACI=CS1.IPCODPACI AND CS2.IPFECHACO=CS1.IPFECHACO AND CS2.NUMINGRES=CS1.NUMINGRES 
		)CS ON CS.NUMINGRES =URG.NUMINGRES AND CS.CONESTADO =3 AND CS.CODCENATE =CEN.CODCENATE  LEFT JOIN
		DBO.AGASICITA AS AGA WITH (NOLOCK) ON CS.NUMCONCIT=AGA.CODAUTONU LEFT JOIN
		(
		SELECT HCH.IPCODPACI, HCH.CODESPTRA FROM dbo.HCHISPACA HCH INNER JOIN 
		(
		SELECT IPCODPACI, CODESPTRA FROM dbo.HCHISPACA HCA
		WHERE FECHISPAC >= EOMONTH(GETDATE(),-1) 
		GROUP BY IPCODPACI, CODESPTRA
		) HCA ON HCH.IPCODPACI=HCA.IPCODPACI AND HCH.CODESPTRA=HCA.CODESPTRA
		WHERE HCH.FECHISPAC <= EOMONTH(GETDATE(),-1) GROUP BY HCH.IPCODPACI, HCH.CODESPTRA 
		) AS G ON G.IPCODPACI=URG.IPCODPACI AND G.CODESPTRA=URG.CODESPTRA  
		LEFT JOIN DBO.AGACTIMED AS AGAC WITH (NOLOCK) ON AGA.CODACTMED=AGAC.CODACTMED 
		LEFT JOIN DBO.INUBICACI AS UBI WITH (NOLOCK) ON PAC.AUUBICACI =UBI.AUUBICACI 
		LEFT JOIN DBO.INMUNICIP AS MUN WITH (NOLOCK) ON UBI.DEPMUNCOD =MUN.DEPMUNCOD 
		LEFT JOIN DBO.INDEPARTA AS DEP WITH (NOLOCK) ON DEP.depcodigo =MUN.DEPCODIGO 
		LEFT JOIN CTE_FACTURACION AS G2 WITH (NOLOCK) ON G2.INGRESO = HIS.NUMINGRES 

	), CTE_PACIENTES_EVOLUCION AS
	(

		SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
			CASE PAC.IPTIPODOC 	
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

			HIS.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion], 
			RTRIM(PAC.IPNOMCOMP) AS 'NOMBRE PACIENTE',--[NombrePaciente], 

			CASE PAC.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS 'SEXO',--[Sexo],
			CASE PAC.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS 'DESCRIPCION SEXO',--[DescripcionSexo],
			CAST(PAC.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO',--[FechaNacimiento], 
			cast(DATEDIFF(YEAR, PAC.IPFECNACI, GETDATE()) as char) AS 'EDAD',-- [Edad], 
			PAC.IPTELEFON AS 'TELEFONO PRINCIPAL',-- [TelPrincipal],
			PAC.IPTELMOVI 'TELEFONO ALTERNATIVO',--[TelAlternativo],
			UPPER(PAC.IPDIRECCI) AS 'DIRECCION',--[Direccion],
			DEP.depcodigo AS 'CODIGO DEPARTAMENTO',--[CODIGO DEPARTAMENTO],
			DEP.nomdepart AS 'DEPARTAMENTO',--[Departamento],
			MUN.MUNCODIGO 'CODIGO MUNICIPIO RESIDENCIA',--[CODIGO MUNICIPIO RESIDENCIA], 
			MUN.MUNNOMBRE AS 'MUNICIPIO',--[Municipio],
			TP.Nit AS 'NIT',--[NIT],
			HA.HealthEntityCode 'CODIGO EPS',--[CodEPS],
			HA.Name AS 'EPS',--[EPS],
			CASE HA.EntityType 
				WHEN 1 THEN 'EPS Contributivo' 
				WHEN 2 THEN 'EPS Subsidiado' 
				WHEN 3 THEN 'ET Vinculados Municipios' 
				WHEN 4 THEN 'ET Vinculados Departamentos'
				WHEN 5 THEN 'ARL Riesgos Laborales' 
				WHEN 6 THEN 'MP Medicina Prepagada' 
				WHEN 7 THEN 'IPS Privada' 
				WHEN 8 THEN 'IPS Publica' 
				WHEN 9 THEN 'Regimen Especial' 
				WHEN 10 THEN 'Accidentes de transito'
				WHEN 11 THEN 'Fosyga' 
				WHEN 12 THEN 'Otros' end as 'REGIMEN',--[Regimen],
			--CG.Code [CODIGO GRUPO ATENCION],
			CG.Name AS 'GRUPO ATENCION',--[GrpAtencion],
			HIS.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
			ing.ifechaing AS 'FECHA INGRESO',-- [FechaIngreso],
			CASE WHEN CS.NUMCONCIT IS NULL THEN '' ELSE CS.NUMINGRES END AS 'INGRESO DESDE CONTROL',--[IngresoDesdeControl], 
			CEN.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion],
			--HIS.UFUCODIGO AS CODIGO_UNIDAD_FUNCIONAL, 
			UNI.UFUDESCRI AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
			HIS.CODPROSAL AS 'IDENTIFICACION MEDICO',--[IdentificacionMedico], 
			PRO.NOMMEDICO AS 'MEDICO',--[Medico], 
			HIS.CODESPTRA AS 'CODIGO ESPECIALIDAD',--[CodEspecialidad],
			ESP.DESESPECI AS 'ESPECIALIDAD',--[Especialidad], 
			AGA.CODACTMED AS 'CODIGO ACTIVIDAD MEDICA',--[CodActividadMedica], 
			AGAC.DESACTMED AS 'ACTIVIDAD MEDICA',--[ActividadMedica],
			HIS.FECHISPAC AS 'FECHA HISTORIA',--[FechaHistoria], 
			HIS.CODDIAGNO AS 'CODIGO DIAGNOSTICO',--[CodDiagnostico], 
			DIA.NOMDIAGNO AS 'DIAGNOSTICO',--[Diagnostico],
			CASE dxp.tipdiagno 
				WHEN 'I' THEN 'Impresion Diagnostica' 
				WHEN 'C' THEN 'Confirmado Nuevo' 
				WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',-- [TipoDiagnostico],
			CASE AGA.CODESTCIT 
				WHEN 0 THEN 'ASIGNADA' 
				WHEN 1 THEN 'CUMPLIDA'
				WHEN 2 THEN 'INCUMPLIDA' 
				WHEN 3 THEN 'PREASIGNADA' 
				WHEN 4 THEN 'CITA CANCELADA' END 'ESTADO CITA',--[EstadoCita],
			'CONTROL' AS 'TIPO CITA',--[TipoCita],
			AGA.FECREGSIS AS 'FECHA SOLICITUD CITA',--[FechaSolicitudCita], 
			AGA.FECHORAIN AS 'FECHA ASIGNACION CITA',--[FechaAsignacionCita], 
			AGA.FECHAOFERTADA AS 'FECHA MEJOR CITA DISPONIBLE',--[FechaMejorCitaDisponible],
			AGA.FECITADES AS 'FECHA DESEADA CITA',--[FechaDeseadaCita] , 
			URG.FECHINIHI AS 'FECHA INICIAL ATENCION',--[FechaIniciaAtencion], 
			URG.FECHINIHI AS 'FECHA FINAL ATENCION',--[FechaFinAtencion], 
			CAST(DATEDIFF (MINUTE,URG.FECHINIHI, URG.FECHINIHI) AS CHAR) 'TIPO CONSULTA MINUTOS',--[TiempoConsultaMinutos],
			CASE AGA.MODALIDAD WHEN 0 THEN 'Presencial' WHEN 1 THEN 'Teleconsulta' ELSE 'N/A' END AS 'MODALIDAD',--[Modalidad],
			--IIF (G.IPCODPACI IS NULL,'NO','SI') AS [ATENCIONES ANTERIORES POR LA ESPECIALIDAD],
			CASE WHEN (SELECT DISTINCT codesptra FROM dbo.hchispaca WHERE ipcodpaci = urg.ipcodpaci AND codesptra = urg.codesptra AND fechispac < his.fechispac) IS NULL THEN 'NO' ELSE 'SI' END AS 'ATENCION ANTERIOR ESPECIALIDAD',-- [AtencionAnteriorEspecialidad],
			ING.IAUTORIZA AS 'NRO AUTORIZACION',--[NroAutorizacion], 
			CASE 
				WHEN ING.IESTADOIN LIKE ' ' THEN 'SIN FACTURAR'
				WHEN G2.FACTURA IS NULL THEN 'NO FACTURABLE'
				WHEN G2.[VALOR SERVICIO]=0 THEN 'NO FACTURABLE'
				WHEN ING.IESTADOIN LIKE 'F' THEN 'FACTURADO' 
				WHEN ING.IESTADOIN LIKE 'A' THEN 'ANULADO' 
				WHEN ING.IESTADOIN LIKE 'C' THEN 'NO FACTURABLE' 
				WHEN ING.IESTADOIN LIKE 'P' THEN 'PARCIAL' END AS 'ESTADO FACTURACION',-- [EstadoFacturacion],
			ISNULL(G2.CODIGO_CUP, 'SIN DATO') AS 'CODIGO CUPS',--[CodCUPS], 
			ISNULL(G2.CUPS , 'SIN DATO') AS 'CUPS',--CUPS,
			ISNULL(G2.CODIGO_RELACIONADO, 'SIN DATO') AS 'CODIGO DESCRIPCION RELACIONADA',--  [CodDescripcionRelacionada],  
			ISNULL(G2.DESCRIPCION_RELACIONADA, 'SIN DATO') AS 'DESCRIPCION RELACIONADA',--[DescripcionRelacionada], 
			'PBS' AS 'TIPO SERVICIO',--[TipoServicio],
			ISNULL(G2.FACTURA, 'SIN DATO') AS 'FACTURA',--[Factura],
			CAST(G2.[FECHA FACTURA] AS DATE) 'FECHA FACTURA',--[FechaFactura],
			ISNULL([VALOR SERVICIO], 0) AS 'VALOR SERVICIO',--[ValorServicio],
			ISNULL([VALOR FACTURA],0) AS 'VALOR FACTURA',--[ValorFactura]
			CAST(HIS.FECHISPAC AS DATE) as 'FECHA BUSQUEDA',
			CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
		FROM dbo.HCNOTEVO1 AS URG INNER JOIN
		dbo.HCHISPACA AS HIS WITH (NOLOCK) ON URG.IDETIPHIS = HIS.IDETIPHIS AND URG.IPCODPACI = HIS.IPCODPACI AND URG.NUMINGRES = HIS.NUMINGRES AND HIS.NUMEFOLIO = URG.NUMEFOLIO INNER JOIN
		dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES =URG.NUMINGRES INNER JOIN
		Contract.CareGroup AS CG WITH (NOLOCK) ON ING.GENCAREGROUP =CG.Id INNER JOIN
		Contract.HealthAdministrator AS HA WITH (NOLOCK) ON ING.GENCONENTITY =HA.Id INNER JOIN
		Common .ThirdParty AS TP WITH (NOLOCK) ON HA.ThirdPartyId =TP.Id INNER JOIN
		dbo.INPACIENT AS PAC WITH (NOLOCK) ON HIS.IPCODPACI = PAC.IPCODPACI INNER JOIN
		dbo.ADCENATEN AS CEN WITH (NOLOCK) ON HIS.CODCENATE = CEN.CODCENATE INNER JOIN
		dbo.INUNIFUNC AS UNI WITH (NOLOCK) ON HIS.UFUCODIGO = UNI.UFUCODIGO INNER JOIN
	    dbo.INPROFSAL AS PRO WITH (NOLOCK) ON HIS.CODPROSAL = PRO.CODPROSAL INNER JOIN
		dbo.INESPECIA AS ESP WITH (NOLOCK) ON HIS.CODESPTRA = ESP.CODESPECI INNER JOIN
		CTE_LISTADO_FINAL FIN ON FIN.NUMINGRES =HIS.NUMINGRES AND FIN.NUMEFOLIO =HIS.NUMEFOLIO AND HIS.IPCODPACI =FIN.IPCODPACI LEFT JOIN
		dbo.INDIAGNOS AS DIA WITH (NOLOCK) ON HIS.CODDIAGNO = DIA.CODDIAGNO 
		INNER JOIN dbo.indiagnop AS dxp ON his.numingres = dxp.numingres AND his.coddiagno = dxp.coddiagno
		LEFT JOIN
		(SELECT CS2.* FROM DBO.ADCONCOEX CS2 INNER JOIN ( SELECT IPCODPACI,MAX(IPFECHACO) IPFECHACO, NUMINGRES FROM DBO.ADCONCOEX WHERE CONESTADO='3' --AND IPFECHACO BETWEEN @FechaInicio AND @FechaFin
				GROUP BY IPCODPACI, NUMINGRES) CS1 ON CS2.IPCODPACI=CS1.IPCODPACI AND CS2.IPFECHACO=CS1.IPFECHACO AND CS2.NUMINGRES=CS1.NUMINGRES 
		)CS ON CS.NUMINGRES =URG.NUMINGRES AND CS.CONESTADO =3 AND CS.CODCENATE =CEN.CODCENATE  LEFT JOIN
		DBO.AGASICITA AS AGA WITH (NOLOCK) ON CS.NUMCONCIT=AGA.CODAUTONU LEFT JOIN
			(
			SELECT HCH.IPCODPACI, HCH.CODESPTRA FROM dbo.HCHISPACA HCH INNER JOIN 
				(
				SELECT IPCODPACI, CODESPTRA FROM dbo.HCHISPACA HCA
				WHERE FECHISPAC >= EOMONTH(GETDATE(),-1) 
				GROUP BY IPCODPACI, CODESPTRA
				) HCA ON HCH.IPCODPACI=HCA.IPCODPACI AND HCH.CODESPTRA=HCA.CODESPTRA
				WHERE HCH.FECHISPAC <= EOMONTH(GETDATE(),-1) GROUP BY HCH.IPCODPACI, HCH.CODESPTRA 
			) AS G ON G.IPCODPACI=URG.IPCODPACI AND G.CODESPTRA=URG.CODESPTRA  LEFT JOIN 
		DBO.AGACTIMED AS AGAC WITH (NOLOCK) ON AGA.CODACTMED=AGAC.CODACTMED 
		LEFT JOIN DBO.INUBICACI AS UBI WITH (NOLOCK) ON PAC.AUUBICACI =UBI.AUUBICACI 
		LEFT JOIN DBO.INMUNICIP AS MUN WITH (NOLOCK) ON UBI.DEPMUNCOD =MUN.DEPMUNCOD 
		LEFT JOIN DBO.INDEPARTA AS DEP WITH (NOLOCK) ON DEP.depcodigo =MUN.DEPCODIGO 
		LEFT JOIN CTE_FACTURACION AS G2 WITH (NOLOCK) ON G2.INGRESO =HIS.NUMINGRES 
	)

	SELECT * FROM CTE_PACIENTES_INGRESOS 
	UNION 
	SELECT * FROM CTE_PACIENTES_EVOLUCION
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de aplanado para cubos de inteligencia de negocio que consolida consultas médicas externas (urgencias y evoluciones clínicas con `TIPHISPAC=''I''` y `GENCONEXT=1`). Cruza datos demográficos del paciente, admisión, especialidad, médico tratante, diagnóstico CIE-10, cita (estado, tipo, tiempos, modalidad presencial/teleconsulta) y facturación CUPS (tipos de servicio 6 y 8), calculando estado de facturación y valor del servicio para alimentar reportes analíticos multidimensionales por empresa, EPS/régimen y período.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalConsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalConsultations';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida y aplana, para alimentación de cubo analítico, las consultas médicas clínicas (registros iniciales de urgencias y notas de evolución) con datos demográficos del paciente, ingreso, EPS, especialidad, diagnóstico, cita y estado de facturación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalConsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La historia clínica (HCHISPACA) debe tener HCOTROSPROC IS NULL, GENCONEXT = 1 y TIPHISPAC = ''I'' para incluirse en el listado base.; El ingreso (ADINGRESO) referenciado por la historia debe existir.; Para enlazar facturación, la factura (Billing.Invoice) debe tener Status = 1 y el CUPS asociado debe tener ServiceType IN (6,8).; La consulta del paciente debe tener un diagnóstico principal asociado en dbo.indiagnop (INNER JOIN obligatorio) ligado por NUMINGRES y CODDIAGNO.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalConsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen historias clínicas con HCOTROSPROC IS NULL, GENCONEXT = 1 y TIPHISPAC = ''I'' (consulta interna válida).; Solo se considera el último control por paciente/ingreso desde ADCONCOEX con CONESTADO = ''3'' (MAX(IPFECHACO)) y mismo centro de atención que la atención.; Solo se vinculan facturas activas (Billing.Invoice.Status = 1) y servicios CUPS de tipo 6 u 8.; Los valores monetarios y datos de factura faltantes se reemplazan por ''SIN DATO'' o 0 vía ISNULL para garantizar columnas no nulas.; El identificador de la compañía se obtiene siempre de DB_NAME() truncado a VARCHAR(9).; El timestamp de actualización (ULT_ACTUAL) se calcula con la hora local convertida a ''Pakistan Standard Time''.; El campo TIPO SERVICIO siempre se reporta como ''PBS''.; El UNION elimina duplicados exactos entre las dos ramas (consultas iniciales y notas de evolución).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalConsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalConsultations: Devuelve la UNION de CTE_PACIENTES_INGRESOS (consultas iniciales desde HCURGING1) y CTE_PACIENTES_EVOLUCION (notas de evolución desde HCNOTEVO1) filtradas por el listado base de HCHISPACA con HCOTROSPROC NULL, GENCONEXT=1 y TIPHISPAC=''I''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalConsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPTIPODOC entre 1..15 → Mapea a códigos de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI) else NULL; si PAC.IPSEXOPAC = ''1'' / ''2'' → Asigna ''H''/''HOMBRE'' o ''M''/''MUJER'' respectivamente else NULL; si HA.EntityType entre 1..12 ó 99 → Clasifica el régimen del pagador (EPS Contributivo, EPS Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de tránsito, Fosyga, Particulares, Otros) else ''Otros'' (solo en CTE_PACIENTES_INGRESOS); si dxp.tipdiagno = ''I'' / ''C'' / ''R'' → Etiqueta diagnóstico como ''Impresion Diagnostica'', ''Confirmado Nuevo'' o ''Confirmado Repetido'' else NULL; si AGA.CODESTCIT entre 0..4 → Mapea estado de cita: 0=ASIGNADA, 1=CUMPLIDA, 2=INCUMPLIDA, 3=PREASIGNADA, 4=CITA CANCELADA else NULL; si URG.TIPCITMED = 1 / 2 (solo en CTE_PACIENTES_INGRESOS) → Clasifica tipo de cita como ''PRIMERA VEZ'' o ''CONTROL'' else ''N/A''; si Subconsulta sobre HCHISPACA con mismo paciente y especialidad y FECHISPAC < actual → Marca ''SI'' en ''ATENCION ANTERIOR ESPECIALIDAD'' else ''NO''; si Cascada sobre ING.IESTADOIN y datos de facturación G2 → IESTADOIN='' '' → ''SIN FACTURAR''; G2.FACTURA NULL o VALOR SERVICIO=0 → ''NO FACTURABLE''; ''F''→''FACTURADO''; ''A''→''ANULADO''; ''C''→''NO FACTURABLE''; ''P''→''PARCIAL'' else NULL; si AGA.MODALIDAD = 0 / 1 → Clasifica modalidad como ''Presencial'' o ''Teleconsulta'' else ''N/A''; si CS.NUMCONCIT IS NULL → ''INGRESO DESDE CONTROL'' queda en cadena vacía else Toma CS.NUMINGRES como ingreso de control; si Origen del registro (rama del UNION) → Si proviene de HCURGING1 calcula tiempo de consulta como DATEDIFF entre FECHINIHI y FECHFINH; si proviene de HCNOTEVO1, fija TIPO CITA=''CONTROL'' y duración=0 (DATEDIFF de FECHINIHI consigo mismo)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalConsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalConsultations';
GO
