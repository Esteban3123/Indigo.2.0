
CREATE VIEW [dbo].[VServicesProceduresImagesDx]
AS
	SELECT	CONCAT('HCORDIMAG', '-', A.AUTO) Id,
			'HCORDIMAG' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(ISNULL(US.UFUCODIGO, D.UFUCODIGO)), ' - ', RTRIM(ISNULL(US.UFUDESCRI, D.UFUDESCRI))) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.NUMFOLINT AS FolioInterpreta,
			A.CODPROINT AS MedicoInterpreto,
			A.INTERPRET AS Interpretacion,
			A.CODPROSAL,
			CR.CODESPEC1,
			A.CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			ISNULL(S.UFUCODIGO, D.UFUCODIGO) AS UFUCODIGO,
			CR.CODIGONIT as NitMedico,
			CASE
				WHEN sod.Id is not null AND sod.IsDelete =0 THEN sod.Id
				WHEN sod.Id IS NOT NULL AND sod.IsDelete = 1 THEN NULL
				WHEN A.GENSERVICEORDER IS NOT NULL then   A.GENSERVICEORDER
			ELSE NULL END GENSERVICEORDER,
			sod.Id as IdServerOrderDetail,
			CASE WHEN  ESTSERIPS IN ('2','3','4') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulado' ELSE 'Estudios No Realizados' END AS Tipo,
			COALESCE(A.MEDREALEC, A.CODPROSAL) as MedicoRealizo,
			A.FECRECEXA as FechaRealizacion,
			CASE WHEN A.MEDREALEC IS NULL THEN 0 ELSE 1 END as Realizo,
			B.DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDIMAG A ON ing.NUMINGRES = A.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 	
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO	
	LEFT JOIN dbo.AGENSALAC S ON A.IdAGENSALAC = S.CODCONCEC
	LEFT JOIN dbo.INUNIFUNC US ON S.UFUCODIGO = US.UFUCODIGO
	LEFT JOIN dbo.INPROFSAL CR ON COALESCE(A.MEDREALEC, A.CODPROSAL)=CR.CODPROSAL 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDIMAG' and acj.EntityTap='INDlcgImagesDX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	left join billing.ServiceOrderDetail sod on A.IdServerOrderDetail = sod.Id
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
UNION ALL
	SELECT	CONCAT('HCORDIMAG', '-', A.AUTO) Id,
			'HCORDIMAG' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,			
			CONCAT(RTRIM(ISNULL(US.UFUCODIGO, D.UFUCODIGO)), ' - ', RTRIM(ISNULL(US.UFUDESCRI, D.UFUDESCRI))) AS UnidadFuncional,						
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			NUMFOLINT AS FolioInterpreta,
			CODPROINT AS MedicoInterpreto,
			INTERPRET AS Interpretacion,
			A.CODPROSAL,
			CR.CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			ISNULL(S.UFUCODIGO, D.UFUCODIGO) AS UFUCODIGO,
			CR.CODIGONIT as NitMedico,
			CASE
				WHEN sod.Id is not null AND sod.IsDelete =0 THEN sod.Id
				WHEN sod.Id IS NOT NULL AND sod.IsDelete = 1 THEN NULL
				WHEN A.GENSERVICEORDER IS NOT NULL then   A.GENSERVICEORDER
			ELSE NULL END GENSERVICEORDER,
			sod.Id as IdServerOrderDetail,
			CASE WHEN  ESTSERIPS IN ('2','3','4') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulado' ELSE 'Estudios No Realizados' END AS Tipo,
			COALESCE(A.MEDREALEC, A.CODPROSAL) as MedicoRealizo,
			A.FECRECEXA as FechaRealizacion,
			CASE WHEN A.MEDREALEC IS NULL THEN 0 ELSE 1 END as Realizo,
			B.DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.HCORDIMAG A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL = C.CODPROSAL 	
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
	LEFT JOIN dbo.AGENSALAC S ON A.IdAGENSALAC = S.CODCONCEC
	LEFT JOIN dbo.INUNIFUNC US ON S.UFUCODIGO = US.UFUCODIGO
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	LEFT JOIN dbo.INPROFSAL CR ON COALESCE(A.MEDREALEC, A.CODPROSAL)=CR.CODPROSAL
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDIMAG' and acj.EntityTap='INDlcgImagesDX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	left join billing.ServiceOrderDetail sod on A.IdServerOrderDetail = sod.Id
	WHERE ING.IESTADOIN = 'C' AND  A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3