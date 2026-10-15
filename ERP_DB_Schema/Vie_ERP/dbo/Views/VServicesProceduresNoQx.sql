
CREATE VIEW [dbo].[VServicesProceduresNoQx]
AS
	SELECT	CONCAT('HCORDPRON', '-', A.AUTO) Id,
			'HCORDPRON' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS,
			--case when A.GENSERVICEORDER is null then CAST(0 as BIT) else CAST(1 as BIT) end AS Seleccione,
			--CAST(0 AS BIT) AS Seleccione,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			case when A.ESTSERIPS = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
			COALESCE((	SELECT TOP 1 CODPROSAL 
						FROM dbo.HCINFPROM 
						where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC),A.MEDREALI, A.CODPROSAL) as MedicoRealizo,
			CASE WHEN A.ESTSERIPS = '5' THEN A.Fechaanulado
			ELSE
			COALESCE((SELECT TOP 1 FECREAPRO 
						FROM dbo.HCINFPROM 
						where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES AND NUMEFOLIO = A.NUMEFOLIO ORDER BY FECREAPRO ASC),ISNULL(A.FECHREALI, A.FECHAPRO), A.FECORDMED)
			END as FechaRealizacion,
			CASE WHEN (SELECT TOP 1 CODPROSAL FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC) IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
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
	JOIN dbo.HCORDPRON A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPRON' and acj.EntityTap='INDlcgProceduresNoQX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
UNION ALL
	SELECT CONCAT('HCORDPRON', '-', A.AUTO) Id,
			'HCORDPRON' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS, 
			--case when A.GENSERVICEORDER is null then CAST(0 as BIT) else CAST(1 as BIT) end AS Seleccione,
			--CAST(0 AS BIT) AS Seleccione,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER, 
			case when A.ESTSERIPS = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
			COALESCE((SELECT TOP 1 CODPROSAL 
					FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC),A.MEDREALI, A.CODPROSAL) as MedicoRealizo,
			CASE WHEN A.ESTSERIPS = '5' THEN A.Fechaanulado
			ELSE
			COALESCE((SELECT TOP 1 FECREAPRO 
					FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES AND NUMEFOLIO = A.NUMEFOLIO ORDER BY FECREAPRO ASC),ISNULL(A.FECHREALI, A.FECHAPRO), A.FECORDMED) 
			END as FechaRealizacion,
			CASE WHEN (SELECT TOP 1 CODPROSAL FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC) IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId,
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,		
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.HCORDPRON A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPRON' and acj.EntityTap='INDlcgProceduresNoQX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE ING.IESTADOIN = 'C' AND A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3

	
UNION ALL
	
SELECT CONCAT('HCPLAOTRPROCUPS', '-', d.ID) Id,
		'HCPLAOTRPROCUPS' EntityName,
		d.ID [Row],
		d.CODSERIPS,
		IIF(d.GENSERVICEORDER IS NULL, 0, 1) Seleccione,
		CONCAT(RTRIM(u.UFUCODIGO), ' - ', RTRIM(u.UFUDESCRI)) UnidadFuncional,
		CONCAT(RTRIM(m.CODIGONIT), ' - ', RTRIM(m.NOMMEDICO)) Medico,
		CONCAT ('Notas otros procedimientos - Datos clínicos relevantes: ', c.ANALISIS) Observacion, -- Observacion Servicio IPS
		c.FECHAREGISTRO Fecha,
		c.NUMEFOLIO Folio,
		c.CODPROSAL,
		m.CODESPEC1,
		d.CANTIDAD CANSERIPS, -- Cantidad Servicio IPS - Aca tambien va el Numero de fracciones cuando el servicio es radioterapias
		c.NUMINGRES,
		c.IPCODPACI,
		u.UFUCODIGO,
		m.CODIGONIT NitMedico,
		d.GENSERVICEORDER,
		'Realizados' Tipo,
		c.CODPROSAL MedicoRealizo,
		c.FECHAREGISTRO FechaRealizacion,
		1 Realizo,   
		RTRIM(cups.DESSERIPS) DESSERIPS, --Descripcion del Procedimiento o Servicio
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
		ISNULL(cd.Id, 0) ContractDescriptionId,
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,		
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
FROM HCHISPACA h
JOIN HCPLAOTRPROC c ON h.ID = c.IDHCHISPACA
JOIN HCPLAOTRPROCUPS d ON c.ID = d.IDHCPLAOTRPROC
-------------
JOIN dbo.INUNIFUNC u ON h.UFUCODIGO = u.UFUCODIGO 
JOIN dbo.INPROFSAL m ON c.CODPROSAL = m.CODPROSAL 
JOIN dbo.INCUPSIPS cups ON d.CODSERIPS = cups.CODSERIPS 
-------------
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = d.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON d.ID = acj.EntityId and acj.EntityName='HCPLAOTRPROCUPS' and acj.EntityTap='INDlcgProceduresNoQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId = bjc.Id
-------------
LEFT JOIN dbo.HCORDPRON a ON c.IPCODPACI = a.IPCODPACI AND c.NUMINGRES = a.NUMINGRES 
	AND d.CODSERIPS = a.CODSERIPS AND ISNULL(d.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(a.IDDESCRIPCIONRELACIONADA, 0)
WHERE a.AUTO IS NULL AND d.FACTURAR = 1