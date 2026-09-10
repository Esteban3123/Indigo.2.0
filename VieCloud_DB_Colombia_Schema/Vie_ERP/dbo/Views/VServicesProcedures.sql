
CREATE VIEW [dbo].[VServicesProcedures]
AS
	SELECT	CONCAT('HCORDLABO', '-', A.AUTO) Id,
			'HCORDLABO' EntityName,
			CAST(A.AUTO AS VARCHAR) AS Row,
			A.AuthorizationEventId,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
								  
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.NUMFOLINT AS FolioInterpreta,
			A.CODPROINT AS MedicoInterpreto,
			A.INTERPRET AS Interpretacion,
			A.CODPROSAL,
			C.CODESPEC1,
			A.CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT AS NitMedico,
			A.GENSERVICEORDER,
			CASE A.ESTSERIPS 
				WHEN '3' THEN 'Realizados' 
				WHEN '4' THEN 'Realizados' 
				WHEN '6' THEN 'Anulados' 
				WHEN '1' THEN 'Solicitados' 
				WHEN '2' THEN 'Muestra Recolectada'
				WHEN '5' THEN 'Remitido'
				ELSE 'No Realizados' 
			END AS Tipo,
			CASE 
				WHEN ISNULL(C2.NOMMEDICO, '') <> '' THEN RTRIM(LTRIM(C2.NOMMEDICO))
				WHEN ISNULL(C.NOMMEDICO, '') <> '' THEN RTRIM(LTRIM(C.NOMMEDICO))
				ELSE NULL 
			END AS MedicoRealizo,
			CAST(REPLACE(CONVERT(VARCHAR,
					CASE 
					WHEN ISNULL(I.FECREGIST,'') <> ''  THEN I.FECREGIST
					WHEN ISNULL(I.FECGENERA, '') <> '' THEN TRY_PARSE(I.FECGENERA AS DATETIME USING 'es-co')
					WHEN ISNULL(I.FECSERIPS, '') <> '' THEN TRY_PARSE(I.FECSERIPS  AS DATETIME USING 'es-co')
					WHEN ISNULL(A.FECHARESULT, '') <> '' THEN A.FECHARESULT 
					WHEN ISNULL(CodigoAzul.FECHAORDE, '') <> '' THEN CodigoAzul.FECHAORDE
					WHEN ISNULL(E.FECHISPAC, '') <> '' THEN E.FECHISPAC
					ELSE ISNULL(A.FECHARESULT ,A.FECRECMUE)
				END, 126
			), 'T00:00:00.000', 'T23:59:59.998') AS DATETIME) AS FechaRealizacion ,
			IIF(ISNULL(I.CODPROSAL, '') = '', 0, 1) AS Realizo, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			A.ESTSERIPS,
			cast(acj.Id as VARCHAR) ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDLABO A  ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 	
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	LEFT JOIN
	(
		SELECT I.AUTOLABOR, MIN(I.AUTO) AUTO
		FROM dbo.INTERCTRL I
		WHERE I.NUMUESTRA = 1
		GROUP BY I.AUTOLABOR
	) ID ON A.AUTO = ID.AUTOLABOR
	LEFT JOIN dbo.INTERCTRL I ON ID.AUTO = I.AUTO
	LEFT JOIN dbo.INPROFSAL C2 ON I.CODPROSAL=C2.CODPROSAL
	LEFT JOIN
	(
		SELECT Azul.NUMEFOLIO, Azul.NUMINGRES, Azul.FECHAORDE,Azul.NUMCODAZU
		FROM dbo.HCCODAZUC Azul
	) CodigoAzul ON A.NUMINGRES = CodigoAzul.NUMINGRES AND A.NUMEFOLIO = CodigoAzul.NUMCODAZU AND A.IDETIPHIS = 'CODIGOAZU'
	LEFT JOIN dbo.HCHISPACA E ON A.NUMINGRES = E.NUMINGRES AND A.NUMEFOLIO = E.NUMEFOLIO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.AUTO=acj.EntityId and acj.EntityName='HCORDLABO' and acj.EntityTap='INDlcgLaboratories'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE (A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3) AND A.CUPSEntityPanelId is null
UNION ALL
	SELECT	CONCAT('HCORDLABO', '-', A.AUTO) Id,
			'HCORDLABO' EntityName,
			CAST(A.AUTO AS VARCHAR) AS Row,
			A.AuthorizationEventId,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
								  
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.NUMFOLINT AS FolioInterpreta,
			A.CODPROINT AS MedicoInterpreto,
			A.INTERPRET AS Interpretacion,
			A.CODPROSAL,
			C.CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT AS NitMedico,
			A.GENSERVICEORDER,
			CASE A.ESTSERIPS 
				WHEN '3' THEN 'Realizados' 
				WHEN '4' THEN 'Realizados' 
				WHEN '6' THEN 'Anulados' 
				WHEN '1' THEN 'Solicitados'
				WHEN '2' THEN 'Muestra Recolectada'
				WHEN '5' THEN 'Remitido'
			ELSE 'No Realizados' END AS Tipo,
			CASE 
				WHEN ISNULL(C2.NOMMEDICO, '') <> '' THEN RTRIM(LTRIM(C2.NOMMEDICO))
				WHEN ISNULL(C.NOMMEDICO, '') <> '' THEN RTRIM(LTRIM(C.NOMMEDICO))
				ELSE NULL 
			END AS MedicoRealizo,
			CAST(REPLACE(CONVERT(VARCHAR,
				CASE 
					WHEN ISNULL(I.FECREGIST,'') <> ''  THEN I.FECREGIST
					WHEN ISNULL(I.FECGENERA, '') <> '' THEN TRY_PARSE(I.FECGENERA AS DATETIME USING 'es-co')
					WHEN ISNULL(I.FECSERIPS, '') <> '' THEN TRY_PARSE(I.FECSERIPS  AS DATETIME USING 'es-co')
					WHEN ISNULL(A.FECHARESULT, '') <> '' THEN A.FECHARESULT 
					WHEN ISNULL(E.FECHISPAC, '') <> '' THEN E.FECHISPAC	
					ELSE ISNULL(A.FECHARESULT,A.FECRECMUE) 
				END, 126
			), 'T00:00:00.000', 'T23:59:59.998') AS DATETIME) AS FechaRealizacion,	
			IIF(ISNULL(I.CODPROSAL, '') = '', 0, 1) AS Realizo, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			A.ESTSERIPS,
			cast(acj.Id as VARCHAR) ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.HCORDLABO A
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL = C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
	JOIN
	(
		SELECT INGMH.NUMINGRESHIJO, MIN(INGMH.ID) ID
		FROM dbo.HCINGRESORECNAC INGMH
		GROUP BY INGMH.NUMINGRESHIJO
	) INGMHD ON A.NUMINGRES = INGMHD.NUMINGRESHIJO
	JOIN dbo.HCINGRESORECNAC INGMH on INGMHD.ID = INGMH.ID
	JOIN dbo.HCRECINAC RN on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	LEFT JOIN
	(
		SELECT I.AUTOLABOR, MIN(I.AUTO) AUTO
		FROM dbo.INTERCTRL I
		WHERE I.NUMUESTRA = 1
		GROUP BY I.AUTOLABOR
	) ID ON A.AUTO = ID.AUTOLABOR
	LEFT JOIN dbo.INTERCTRL I ON ID.AUTO = I.AUTO
	LEFT JOIN dbo.INPROFSAL C2 ON I.CODPROSAL = C2.CODPROSAL 
	LEFT JOIN dbo.HCHISPACA E ON INGMH.NUMINGRES = E.NUMINGRES AND A.NUMEFOLIO = E.NUMEFOLIO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.AUTO=acj.EntityId and acj.EntityName='HCORDLABO' and acj.EntityTap='INDlcgLaboratories'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE ING.IESTADOIN = 'C' AND (A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3) AND A.CUPSEntityPanelId is null

	UNION ALL
SELECT	CONCAT('HCORDLABO', '-', STRING_AGG( A.AUTO,'-')) Id,
			'HCORDLABO' EntityName,
			STRING_AGG( A.AUTO,',') AS Row,--A.AUTO AS Row,
			CASE WHEN COUNT(DISTINCT A.AuthorizationEventId) = 1 THEN MAX(A.AuthorizationEventId) ELSE NULL END AS AuthorizationEventId,
			cups.Code CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,								  
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			STRING_AGG( ISNULL(A.NUMFOLINT,''),',') AS FolioInterpreta,
			STRING_AGG(ISNULL(A.CODPROINT,''),',') AS MedicoInterpreto,
			STRING_AGG(ISNULL(A.INTERPRET,''),',') AS Interpretacion,
			A.CODPROSAL,
			C.CODESPEC1,
			A.CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT AS NitMedico,
			A.GENSERVICEORDER,
			CASE min(A.ESTSERIPS)
				WHEN '3' THEN 'Realizados' 
				WHEN '4' THEN 'Realizados' 
				WHEN '6' THEN 'Anulados' 
				WHEN '1' THEN 'Solicitados' 
				WHEN '2' THEN 'Muestra Recolectada'
				WHEN '5' THEN 'Remitido'
				ELSE 'No Realizados' 
			END AS Tipo,
			MIN(
			CASE 
				WHEN ISNULL(C2.NOMMEDICO, '') <> '' THEN RTRIM(LTRIM(C2.NOMMEDICO))
				WHEN ISNULL(C.NOMMEDICO, '') <> '' THEN RTRIM(LTRIM(C.NOMMEDICO))
				ELSE NULL 
			END )AS MedicoRealizo,
			MIN (CAST(REPLACE(CONVERT(VARCHAR,
					CASE 
					WHEN ISNULL(I.FECREGIST,'') <> ''  THEN I.FECREGIST
					WHEN ISNULL(I.FECGENERA, '') <> '' THEN TRY_PARSE(I.FECGENERA AS DATETIME USING 'es-co')
					WHEN ISNULL(I.FECSERIPS, '') <> '' THEN TRY_PARSE(I.FECSERIPS  AS DATETIME USING 'es-co')
					WHEN ISNULL(A.FECHARESULT, '') <> '' THEN A.FECHARESULT 
					WHEN ISNULL(E.FECHISPAC, '') <> '' THEN E.FECHISPAC
					ELSE ISNULL(A.FECHARESULT ,A.FECRECMUE)
				END, 126
			), 'T00:00:00.000', 'T23:59:59.998') AS DATETIME)) AS FechaRealizacion,
			IIF(ISNULL(A.CODPROSAL, '') = '', 0, 1) AS Realizo, 
			 0 CUPSEntityContractDescriptionId, 
			 0 ContractDescriptionId, 
			'' ContractDescriptionCodeName,
			cups.[Description] AS DESSERIPS,
			MIN(A.ESTSERIPS) ESTSERIPS,
			STRING_AGG(acj.Id,',') ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.JustificationId is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.JustificationId is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.ADINGRESO ing WITH(NOLOCK)
	JOIN dbo.HCORDLABO A WITH(NOLOCK) ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C WITH(NOLOCK) ON A.CODPROSAL=C.CODPROSAL 	
	JOIN dbo.INUNIFUNC D WITH(NOLOCK) ON A.UFUCODIGO=D.UFUCODIGO
	JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON cups.Id= A.CUPSEntityPanelId  
	LEFT JOIN
	(
		SELECT I.AUTOLABOR, MIN(I.AUTO) AUTO
		FROM dbo.INTERCTRL I WITH(NOLOCK)
		WHERE I.NUMUESTRA = 1
		GROUP BY I.AUTOLABOR
	) ID ON A.AUTO = ID.AUTOLABOR
	LEFT JOIN dbo.INTERCTRL I WITH(NOLOCK) ON ID.AUTO = I.AUTO
	LEFT JOIN dbo.INPROFSAL C2 WITH(NOLOCK) ON I.CODPROSAL=C2.CODPROSAL
	LEFT JOIN dbo.HCHISPACA E WITH(NOLOCK) ON A.NUMINGRES = E.NUMINGRES AND A.NUMEFOLIO = E.NUMEFOLIO
	LEFT JOIN (	select	acj.Id,
						acj.JustificationId,
						acj.EntityName,
						acj.EntityId,
						acj.EntityTap,
						cast(acj.CreationDate as date) CreationDate,
						acj.CreationUser
				from Billing.AccountControlJustification acj WITH(NOLOCK)) acj on A.AUTO=acj.EntityId and acj.EntityName='HCORDLABO' and acj.EntityTap='INDlcgLaboratories'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE (A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3) AND A.CUPSEntityPanelId is not null
	GROUP by cups.Code,A.GENSERVICEORDER,D.UFUCODIGO,D.UFUDESCRI,C.CODIGONIT,C.NOMMEDICO,A.OBSSERIPS,A.FECORDMED,
			 A.NUMEFOLIO,A.CODPROSAL,C.CODESPEC1,A.CANSERIPS, A.NUMINGRES,	A.IPCODPACI
			 ,cups.[Description],acj.CreationUser,acj.CreationDate,acj.JustificationId,bjc.Code,bjc.[Description],
			 bjc.SkipClearance, A.CUPSEntityPanelId, A.FECHASUGE

	UNION ALL

		SELECT	CONCAT('HCORDLABO', '-', STRING_AGG( A.AUTO,'-')) Id,
			'HCORDLABO' EntityName,
			STRING_AGG( A.AUTO,',') AS Row,
			CASE WHEN COUNT(DISTINCT A.AuthorizationEventId) = 1 THEN MAX(A.AuthorizationEventId) ELSE NULL END AS AuthorizationEventId,
			cups.Code	CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,								  
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			STRING_AGG( ISNULL(A.NUMFOLINT,''),',') AS FolioInterpreta,
			STRING_AGG(ISNULL(A.CODPROINT,''),',') AS MedicoInterpreto,
			STRING_AGG(ISNULL(A.INTERPRET,''),',') AS Interpretacion,
			A.CODPROSAL,
			C.CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT AS NitMedico,
			A.GENSERVICEORDER,
			CASE  min(A.ESTSERIPS)
				WHEN '3' THEN 'Realizados' 
				WHEN '4' THEN 'Realizados' 
				WHEN '6' THEN 'Anulados' 
				WHEN '1' THEN 'Solicitados'
				WHEN '2' THEN 'Muestra Recolectada'
				WHEN '5' THEN 'Remitido'
			ELSE 'No Realizados' END AS Tipo,
			MIN(CASE 
				WHEN ISNULL(C2.NOMMEDICO, '') <> '' THEN RTRIM(LTRIM(C2.NOMMEDICO))
				WHEN ISNULL(C.NOMMEDICO, '') <> '' THEN RTRIM(LTRIM(C.NOMMEDICO))
				ELSE NULL	
			END) AS MedicoRealizo,
			MIN(CAST(REPLACE(CONVERT(VARCHAR,
				CASE 
					WHEN ISNULL(I.FECREGIST,'') <> ''  THEN I.FECREGIST
					WHEN ISNULL(I.FECGENERA, '') <> '' THEN TRY_PARSE(I.FECGENERA AS DATETIME USING 'es-co')
					WHEN ISNULL(I.FECSERIPS, '') <> '' THEN TRY_PARSE(I.FECSERIPS  AS DATETIME USING 'es-co')
					WHEN ISNULL(A.FECHARESULT, '') <> '' THEN A.FECHARESULT 
					WHEN ISNULL(E.FECHISPAC, '') <> '' THEN E.FECHISPAC	
					ELSE ISNULL(A.FECHARESULT ,A.FECRECMUE)
				END, 126
			), 'T00:00:00.000', 'T23:59:59.998') AS DATETIME)) AS FechaRealizacion,		
			IIF(ISNULL(A.CODPROSAL, '') = '', 0, 1) AS Realizo, 
			0 CUPSEntityContractDescriptionId, 
			0 ContractDescriptionId, 
			'' ContractDescriptionCodeName,
			cups.Description AS DESSERIPS,
			 min(A.ESTSERIPS) ESTSERIPS,
			STRING_AGG(acj.Id,',') ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.JustificationId is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.JustificationId is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.HCORDLABO A
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL = C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
	JOIN
	(
		SELECT INGMH.NUMINGRESHIJO, MIN(INGMH.ID) ID
		FROM dbo.HCINGRESORECNAC INGMH
		GROUP BY INGMH.NUMINGRESHIJO
	) INGMHD ON A.NUMINGRES = INGMHD.NUMINGRESHIJO
	JOIN dbo.HCINGRESORECNAC INGMH on INGMHD.ID = INGMH.ID
	JOIN dbo.HCRECINAC RN on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON cups.Id= A.CUPSEntityPanelId  
	LEFT JOIN
	(
		SELECT I.AUTOLABOR, MIN(I.AUTO) AUTO
		FROM dbo.INTERCTRL I
		WHERE I.NUMUESTRA = 1
		GROUP BY I.AUTOLABOR
	) ID ON A.AUTO = ID.AUTOLABOR
	LEFT JOIN dbo.INTERCTRL I ON ID.AUTO = I.AUTO
	LEFT JOIN dbo.INPROFSAL C2 ON I.CODPROSAL = C2.CODPROSAL 
	LEFT JOIN dbo.HCHISPACA E ON INGMH.NUMINGRES = E.NUMINGRES AND A.NUMEFOLIO = E.NUMEFOLIO
	LEFT JOIN (	select	acj.Id,
						acj.JustificationId,
						acj.EntityName,
						acj.EntityId,
						acj.EntityTap,
						cast(acj.CreationDate as date) CreationDate,
						acj.CreationUser
				from Billing.AccountControlJustification acj WITH(NOLOCK)) acj on A.AUTO=acj.EntityId and acj.EntityName='HCORDLABO' and acj.EntityTap='INDlcgLaboratories'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE ING.IESTADOIN = 'C' AND (A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3) AND A.CUPSEntityPanelId is NOT null
	GROUP by	INGMH.NUMINGRES,A.CODPROSAL, cups.Code,A.GENSERVICEORDER,D.UFUCODIGO,D.UFUDESCRI,C.CODIGONIT,C.NOMMEDICO,
				A.OBSSERIPS,A.FECORDMED,A.NUMEFOLIO, A.NUMFOLINT,A.CODPROINT,A.INTERPRET,C.CODESPEC1,A.CANSERIPS,	A.IPCODPACI
				,A.PROFRESULT,cups.[Description],acj.CreationUser,acj.CreationDate,acj.JustificationId,bjc.Code,bjc.[Description],
				bjc.SkipClearance, A.CUPSEntityPanelId, A.FECHASUGE
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todas las órdenes de servicios y procedimientos de laboratorio clínico registradas en la historia clínica, integrando datos del ingreso o admisión del paciente (urgencias, hospitalización, consulta externa), el catálogo de servicios CUPS/IPS, el profesional de salud que ordenó el examen, la unidad funcional o área que lo ejecutó, y el control e interpretación de resultados. Permite conocer el estado de cada orden (Solicitado, Muestra Recolectada, Realizado, Remitido, Anulado), el profesional que la realizó según el estado, la fecha de realización efectiva, así como información contractual asociada (descripción de contrato CUPS) y justificaciones de glosa o control de facturación. También vincula alertas de código azul cuando el procedimiento está ligado a una emergencia crítica. Se usa principalmente en la reportería clínica, seguimiento de órdenes médicas de laboratorio, auditoría de prestación de servicios y procesos de facturación y liquidación de cuentas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProcedures';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida las órdenes de laboratorio (individuales y agrupadas en paneles CUPS) de pacientes, mostrando estado, médicos, fechas de realización, descripciones contractuales y justificaciones de control de cuenta para su uso en facturación e historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de laboratorio (HCORDLABO) debe estar asociada a un ingreso vigente (ADINGRESO) o, en el caso de recién nacidos, a un ingreso hijo (HCINGRESORECNAC) cuyo ingreso esté en estado ''C''.; Se excluyen órdenes con MANEXTPRO distinto de 0 salvo que el ingreso tenga TRATAESPECIA = 3.; Las ramas de paneles requieren que A.CUPSEntityPanelId no sea nulo y exista en Contract.CUPSEntity; las ramas individuales requieren CUPSEntityPanelId nulo.; Para tomar la interpretación se considera solo el primer registro de INTERCTRL con NUMUESTRA = 1 (mínimo AUTO por AUTOLABOR).; La justificación de control de cuenta se vincula sólo cuando EntityName=''HCORDLABO'' y EntityTap=''INDlcgLaboratories''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila tiene EntityName fijo ''HCORDLABO'' y un Id con prefijo ''HCORDLABO-''.; Las órdenes pertenecientes a un panel CUPS siempre se agrupan; nunca aparecen individuales y agrupadas a la vez (CUPSEntityPanelId IS NULL vs IS NOT NULL son mutuamente excluyentes).; Sólo se toma la interpretación de INTERCTRL correspondiente a la primera muestra (NUMUESTRA = 1).; Las órdenes con manejo externo (MANEXTPRO<>0) sólo aparecen si el ingreso es de tratamiento especial tipo 3.; Las ramas de recién nacido sólo incluyen ingresos con estado ''C'' (cerrado/confirmado).; FechaRealizacion nunca queda con hora 00:00:00.000: se reemplaza por 23:59:59.998.; La justificación de control de cuenta siempre corresponde al tap ''INDlcgLaboratories'' del módulo de laboratorios.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] VServicesProcedures: Devuelve el conjunto unificado (UNION ALL) de cuatro consultas: órdenes individuales por ingreso normal, órdenes individuales por ingreso de recién nacido (IESTADOIN=''C''), órdenes agrupadas por panel CUPS por ingreso normal, y órdenes agrupadas por panel CUPS por ingreso de recién nacido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.CUPSEntityPanelId IS NULL → Cada orden HCORDLABO se devuelve como fila individual con su CODSERIPS y descripción desde INCUPSIPS. else Las órdenes con CUPSEntityPanelId no nulo se agrupan (GROUP BY) y sus AUTO, NUMFOLINT, CODPROINT, INTERPRET y ACJustificationId se concatenan con STRING_AGG; el código y descripción se toman de Contract.CUPSEntity.; si A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3 → La orden se incluye en la vista. else La orden se excluye.; si ESTSERIPS IN (''3'',''4'') → Tipo = ''Realizados''. else ESTSERIPS ''6''=''Anulados'', ''1''=''Solicitados'', ''2''=''Muestra Recolectada'', ''5''=''Remitido'', cualquier otro = ''No Realizados''.; si Existe ingreso en HCINGRESORECNAC vinculado a NUMINGRES y ING.IESTADOIN = ''C'' → La orden se considera del ingreso del recién nacido y se asocia al NUMINGRES del hijo. else Se usa el flujo estándar contra ADINGRESO.; si C2.NOMMEDICO (profesional desde INTERCTRL) tiene valor → MedicoRealizo = nombre del profesional que registró la interpretación. else Si C.NOMMEDICO tiene valor, se usa el médico solicitante; en caso contrario NULL.; si Cadena de coalesce sobre fechas (I.FECREGIST, I.FECGENERA, I.FECSERIPS, A.FECHARESULT, CodigoAzul.FECHAORDE, E.FECHISPAC) → FechaRealizacion toma el primer valor no vacío en ese orden de prioridad y reemplaza la hora 00:00:00 por 23:59:59.998. else Usa ISNULL(A.FECHARESULT, A.FECRECMUE) como fallback.; si acj.Id (o acj.JustificationId en ramas agrupadas) IS NULL → JustificationCodeName = '''' y SkipLiquidation = 0. else Se concatena Código - Descripción de BillingJustificationControl y se toma SkipClearance como SkipLiquidation.; si I.CODPROSAL (o A.CODPROSAL en ramas agrupadas) tiene valor → Realizo = 1 (la orden se considera realizada). else Realizo = 0.; si A.IDETIPHIS = ''CODIGOAZU'' y existe registro en HCCODAZUC con mismo NUMINGRES y NUMEFOLIO=NUMCODAZU → La fecha de orden de código azul (FECHAORDE) participa como candidato a FechaRealizacion.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDLABO; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.INTERCTRL; dbo.HCCODAZUC; dbo.HCHISPACA; dbo.HCINGRESORECNAC; dbo.HCRECINAC; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Contract.CUPSEntity; Billing.AccountControlJustification; Billing.BillingJustificationControl', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProcedures';
GO
