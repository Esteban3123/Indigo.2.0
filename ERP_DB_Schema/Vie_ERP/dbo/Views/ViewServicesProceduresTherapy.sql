
CREATE VIEW [dbo].[ViewServicesProceduresTherapy]
AS
	SELECT	CONCAT('HCPROCTER', '-', A.CODCONSEC) Id,
			'HCPROCTER' EntityName,
			A.CODCONSEC as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			RTRIM(D.UFUDESCRI) AS UnidadFuncional,
			RTRIM(C.NOMMEDICO) AS Medico,
			'' AS Observacion,
			FECHISPAC AS Fecha,
			'' AS Folio,
			CODESPEC1,
			1 AS CANSERIPS,
			NUMINGRES,
			IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			A.CODPROSAL as MedicoRealizo,
			FECHISPAC as FechaRealizacion,
			case when A.CODPROSAL is null then 0 else 1 end as Realizo,
			B.ARSCODIGO,
			A.CODPROSAL,
			TIPSERIPS,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			IIF(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			IIF(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			cd.Id AS ContractDescriptionId,
			cecd.Id AS CUPSEntityContractDescriptionId,
			IIF(A.IDDESCRIPCIONRELACIONADA IS NOT NULL, CONCAT(cd.Code, ' - ', cd.Name), NULL) CodeNameContractDescriptions
	FROM dbo.HCPROCTER A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON cecd.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd ON cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CODCONSEC=acj.EntityId and acj.EntityName='HCPROCTER' and acj.EntityTap='INDlcgTerapy'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
UNION ALL
	SELECT	CONCAT('HCPROCTER', '-', A.CODCONSEC) Id,
			'HCPROCTER' EntityName,
			A.CODCONSEC as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			RTRIM(D.UFUDESCRI) AS UnidadFuncional,
			RTRIM(C.NOMMEDICO) AS Medico,
			'' AS Observacion,
			a.FECHISPAC AS Fecha,
			'' AS Folio,
			CODESPEC1,
			1 AS CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			A.CODPROSAL as MedicoRealizo,
			a.FECHISPAC as FechaRealizacion,
			case when A.CODPROSAL is null then 0 else 1 end as Realizo,
			B.ARSCODIGO,
			A.CODPROSAL,
			TIPSERIPS,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			IIF(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			IIF(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			cd.Id AS ContractDescriptionId,
			cecd.Id AS CUPSEntityContractDescriptionId,
			IIF(A.IDDESCRIPCIONRELACIONADA IS NOT NULL, CONCAT(cd.Code, ' - ', cd.Name), NULL) CodeNameContractDescriptions
	FROM dbo.HCPROCTER A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON cecd.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd ON cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CODCONSEC=acj.EntityId and acj.EntityName='HCPROCTER' and acj.EntityTap='INDlcgTerapy'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE ING.IESTADOIN = 'C'