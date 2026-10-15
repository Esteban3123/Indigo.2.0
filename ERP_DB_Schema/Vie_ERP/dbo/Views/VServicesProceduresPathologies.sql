
CREATE VIEW [dbo].[VServicesProceduresPathologies]
AS
	SELECT	CONCAT('HCORDPATO', '-', A.AUTO) Id,
			'HCORDPATO' EntityName,
			A.AUTO AS Row,
			A.CODSERIPS, 
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			NUMFOLINT AS FolioInterpreta,
			CODPROINT AS MedicoInterpreto,
			INTERPRET AS Interpretacion,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			CASE 
				WHEN ESTSERIPS IN ('2','3','4') THEN 'Realizados' 
				when ESTSERIPS = '6' then 'Anulados'
				when ESTSERIPS = '1' then 'Solicitados'
				ELSE 'No Realizados' 
			END AS Tipo,
			CASE WHEN A.PROFRESULTADO IS NULL THEN A.CODPROSAL ELSE A.PROFRESULTADO END as MedicoRealizo,
			CASE WHEN A.PROFRESULTADO IS NULL THEN A.FECRECEPMUES ELSE A.FECHARESULT END as FechaRealizacion,
			CASE WHEN A.PROFRESULTADO IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			i.IPNOMCOMP  as PersonName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDPATO A ON ing.NUMINGRES = A.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPATO' and acj.EntityTap='INDlcgPathology'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
UNION ALL
	SELECT	CONCAT('HCORDPATO', '-', A.AUTO) Id,
			'HCORDPATO' EntityName,
			A.AUTO AS Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			NUMFOLINT AS FolioInterpreta,
			CODPROINT AS MedicoInterpreto,
			INTERPRET AS Interpretacion,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			CASE 
				WHEN ESTSERIPS IN ('2','3','4') THEN 'Realizados' 
				when ESTSERIPS = '6' then 'Anulados'
				when ESTSERIPS = '1' then 'Solicitados'
				ELSE 'No Realizados' END 
			AS Tipo,
			CASE WHEN A.PROFRESULTADO IS NULL THEN A.CODPROSAL ELSE A.PROFRESULTADO END as MedicoRealizo,
			CASE WHEN A.PROFRESULTADO IS NULL THEN A.FECRECEPMUES ELSE A.FECHARESULT END as FechaRealizacion,
			CASE WHEN A.PROFRESULTADO IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,		
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.HCORDPATO A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPATO' and acj.EntityTap='INDlcgPathology'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE ING.IESTADOIN = 'C' AND A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3