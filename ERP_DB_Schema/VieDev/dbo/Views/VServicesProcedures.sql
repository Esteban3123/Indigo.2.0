
CREATE VIEW [dbo].[VServicesProcedures]
AS
	SELECT	CONCAT('HCORDLABO', '-', A.AUTO) Id,
			'HCORDLABO' EntityName,
			CAST(A.AUTO AS VARCHAR) AS Row,
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