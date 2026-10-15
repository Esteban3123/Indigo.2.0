

CREATE VIEW [dbo].[ViewServicesProceduresConsultation]
AS
	SELECT	CONCAT('HCORDINTE', '-', A.AUTO) Id,
			'HCORDINTE' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NOT NULL, 1, 0) AS Seleccione,
			RTRIM(D.UFUDESCRI) AS UnidadFuncional,
			RTRIM(C.NOMMEDICO) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.NUMEFOLIO AS FolioSolicitud,
			A.CODPROSAL AS MedicoSolicitud,
			A.FECORDMED AS FechaSolicitud,
			A.NUMFOLINT AS FolioRealizado,
			CASE WHEN A.NUMFOLINT IS NULL THEN NULL ELSE A.CODPROINT END AS MedicoRealizado,
			CASE WHEN A.NUMFOLINT IS NULL THEN NULL ELSE A.FECHAINT END AS FechaRealizado,
			INTERPRET AS Interpretacion,
			CODESPEC1,
			CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			CASE 
				WHEN A.NUMFOLINT IS NULL THEN NULL
				ELSE COALESCE(A.FECHAINT, HV.FECHISPAC)
			END AS FechaRealizacion,			
			CASE WHEN A.CODPROINT IS NULL THEN 0 ELSE 1 END As Realizo,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId,
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			CASE WHEN A.NUMFOLINT IS NULL THEN 'Solicitadas' ELSE 'Con Respuesta' END AS Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,		
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			CAST(IIF(A.NUMFOLINT IS NOT NULL AND HV.ID IS NOT NULL, 1, 0) AS BIT) AS ExistsInViewReviews
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDINTE A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDINTE' and acj.EntityTap='INDlcgInterSearch'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	OUTER APPLY (
		SELECT TOP 1 H.ID, H.FECHISPAC
		FROM dbo.HCHISPACA H
		WHERE H.IPCODPACI = A.IPCODPACI
			AND H.NUMINGRES = A.NUMINGRES
			AND H.NUMEFOLIO = A.NUMFOLINT
		ORDER BY H.FECHISPAC ASC
	) HV
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
UNION ALL
	SELECT	CONCAT('HCORDINTE', '-', A.AUTO) Id,
			'HCORDINTE' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NOT NULL, 1, 0) AS Seleccione,
			RTRIM(D.UFUDESCRI) AS UnidadFuncional,
			RTRIM(C.NOMMEDICO) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.NUMEFOLIO AS FolioSolicitud,
			A.CODPROSAL AS MedicoSolicitud,
			A.FECORDMED AS FechaSolicitud,
			A.NUMFOLINT AS FolioRealizado,
			CASE WHEN A.NUMFOLINT IS NULL THEN NULL ELSE A.CODPROINT END AS MedicoRealizado,
			CASE WHEN A.NUMFOLINT IS NULL THEN NULL ELSE A.FECHAINT END AS FechaRealizado,
			INTERPRET AS Interpretacion,
			CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			CASE 
				WHEN A.NUMFOLINT IS NULL THEN NULL
				ELSE COALESCE(A.FECHAINT, HV.FECHISPAC)
			END AS FechaRealizacion,				
			CASE WHEN A.CODPROINT IS NULL THEN 0 ELSE 1 END As Realizo,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId,
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			CASE WHEN A.NUMFOLINT IS NULL THEN 'Solicitadas' ELSE 'Con Respuesta' END AS Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,		
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			CAST(IIF(A.NUMFOLINT IS NOT NULL AND HV.ID IS NOT NULL, 1, 0) AS BIT) AS ExistsInViewReviews
	FROM dbo.HCORDINTE A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDINTE' and acj.EntityTap='INDlcgInterSearch'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	OUTER APPLY (
		SELECT TOP 1 H.ID, H.FECHISPAC
		FROM dbo.HCHISPACA H
		WHERE H.IPCODPACI = A.IPCODPACI
			AND H.NUMINGRES = A.NUMINGRES
			AND H.NUMEFOLIO = A.NUMFOLINT
		ORDER BY H.FECHISPAC ASC
	) HV
	WHERE ING.IESTADOIN = 'C' AND A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3