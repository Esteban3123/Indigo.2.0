





CREATE VIEW [dbo].[ViewSurgeriesPerformed]
AS
select	 CAST(RA.CONSECUQX AS VARCHAR) CONSECUQXId 
		,RA.CONSECUQX 
		,RA.NUMEFOLIO
		,RA.IPCODPACI
		,RA.NUMINGRES
		,RA.CODCENATE
		,RA.UFUCODIGO
		,RA.CODSERIPS
		,IPS.DESSERIPS
		,QXPRINCIP
		,CANTIDAQX
		,concat(ISNULL(RA.CODVIAABO,abo.CODVIAABO),' - ',ABO.DESVIAABO)  CODVIAABO
		,null as MedicoRealizo
		,inf.FECHORFIN as FechaRealizacion
		,0 as Realizo
		,IIF(RA.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
		,RA.GENSERVICEORDER
		,acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCQXREALI' EntityName,
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
		ISNULL(cd.Id, 0) ContractDescriptionId, 
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		'Procedimientos con Informe QX' AS Tipo
from dbo.HCQXREALI RA
inner join dbo.INCUPSIPS AS IPS on IPS.CODSERIPS = RA.CODSERIPS
inner join [dbo].[HCQXVIABO] ABO WITH(NOLOCK) ON ABO.CODVIAABO = ISNULL(RA.CODVIAABO,abo.CODVIAABO)
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = RA.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON RA.CONSECUQX =acj.EntityId and acj.EntityName='HCQXREALI' and acj.EntityTap='INDlcgProceduresQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id	
LEFT JOIN HCQXINFOR inf WITH(NOLOCK) on RA.IPCODPACI=inf.IPCODPACI and RA.NUMEFOLIO= inf.NUMEFOLIO and RA.NUMINGRES=inf.NUMINGRES

union all 

select	CAST(CONSECUQX AS VARCHAR) CONSECUQXId,
		CONSECUQX,
		a.NUMEFOLIO,
		a.IPCODPACI,
		INGMH.NUMINGRES,
		a.CODCENATE
		,a.UFUCODIGO,
		IPS.CODSERIPS,
		IPS.DESSERIPS,
		QXPRINCIP,
		CANTIDAQX,
		concat(ISNULL(a.CODVIAABO,abo.CODVIAABO),' - ',ABO.DESVIAABO)  CODVIAABO
		,null as MedicoRealizo
		,inf.FECHORFIN as FechaRealizacion
		,0 as Realizo
		,IIF(a.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
		,a.GENSERVICEORDER
		,acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCQXREALI' EntityName,
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
		ISNULL(cd.Id, 0) ContractDescriptionId, 
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		'Procedimientos con Informe QX' AS Tipo
from dbo.HCQXREALI a
inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
inner join dbo.INCUPSIPS AS IPS on IPS.CODSERIPS = a.CODSERIPS
inner join [dbo].[HCQXVIABO] ABO WITH(NOLOCK) ON ABO.CODVIAABO = ISNULL(a.CODVIAABO,abo.CODVIAABO)
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON a.CONSECUQX =acj.EntityId and acj.EntityName='HCQXREALI' and acj.EntityTap='INDlcgProceduresQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
LEFT JOIN HCQXINFOR inf WITH(NOLOCK) on a.IPCODPACI=inf.IPCODPACI and a.NUMEFOLIO= inf.NUMEFOLIO and a.NUMINGRES=inf.NUMINGRES

UNION ALL

select	 CONCAT(RA.CONSECUQX,'-',1) CONSECUQXId
		,RA.CONSECUQX 
		,RA.NUMEFOLIO
		,RA.IPCODPACI
		,RA.NUMINGRES
		,RA.CODCENATE
		,RA.UFUCODIGO
		,RA.CODSERIPS
		,IPS.DESSERIPS
		,QXPRINCIP
		,CANTIDAQX
		,concat(ISNULL(RA.CODVIAABO,abo.CODVIAABO),' - ',ABO.DESVIAABO)  CODVIAABO
		,null as MedicoRealizo
		,inf.FECHORFIN as FechaRealizacion
		,0 as Realizo
		,IIF(RA.GENSERVICEORDER2 IS NULL, 0, 1) AS Seleccione
		,RA.GENSERVICEORDER2
		,acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCQXREALI' EntityName,
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
		ISNULL(cd.Id, 0) ContractDescriptionId, 
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		'Procedimientos con Informe QX' AS Tipo
from dbo.HCQXREALI RA
inner join dbo.INCUPSIPS AS IPS on IPS.CODSERIPS = RA.CODSERIPS
inner join [dbo].[HCQXVIABO] ABO WITH(NOLOCK) ON ABO.CODVIAABO = ISNULL(RA.CODVIAABO,abo.CODVIAABO)
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = RA.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON RA.CONSECUQX =acj.EntityId and acj.EntityName='HCQXREALI' and acj.EntityTap='INDlcgProceduresQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id	
LEFT JOIN HCQXINFOR inf WITH(NOLOCK) on RA.IPCODPACI=inf.IPCODPACI and RA.NUMEFOLIO= inf.NUMEFOLIO and RA.NUMINGRES=inf.NUMINGRES
WHERE ABO.CODVIAABO ='02'

union all 

select	CONCAT(CONSECUQX,'-',2) CONSECUQXId,
		CONSECUQX ,
		a.NUMEFOLIO,
		a.IPCODPACI,
		INGMH.NUMINGRES,
		a.CODCENATE
		,a.UFUCODIGO,
		IPS.CODSERIPS,
		IPS.DESSERIPS,
		QXPRINCIP,
		CANTIDAQX,
		concat(ISNULL(a.CODVIAABO,abo.CODVIAABO),' - ',ABO.DESVIAABO)  CODVIAABO
		,null as MedicoRealizo
		,inf.FECHORFIN as FechaRealizacion
		,0 as Realizo
		,IIF(a.GENSERVICEORDER2 IS NULL, 0, 1) AS Seleccione
		,a.GENSERVICEORDER2
		,acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCQXREALI' EntityName,
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
		ISNULL(cd.Id, 0) ContractDescriptionId, 
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		'Procedimientos con Informe QX' AS Tipo
from dbo.HCQXREALI a
inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
inner join dbo.INCUPSIPS AS IPS on IPS.CODSERIPS = a.CODSERIPS
inner join [dbo].[HCQXVIABO] ABO WITH(NOLOCK) ON ABO.CODVIAABO = ISNULL(a.CODVIAABO,abo.CODVIAABO)
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON a.CONSECUQX =acj.EntityId and acj.EntityName='HCQXREALI' and acj.EntityTap='INDlcgProceduresQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
LEFT JOIN HCQXINFOR inf WITH(NOLOCK) on a.IPCODPACI=inf.IPCODPACI and a.NUMEFOLIO= inf.NUMEFOLIO and a.NUMINGRES=inf.NUMINGRES
WHERE ABO.CODVIAABO ='02'

UNION ALL

	SELECT  CAST(A.AUTO AS VARCHAR) CONSECUQXId,
			A.AUTO CONSECUQX,
			A.NUMEFOLIO,
			A.IPCODPACI,
			A.NUMINGRES,
			A.CODCENATE,
			A.UFUCODIGO,
			ISNULL(MEN.CODSERIPS, A.CODSERIPS) AS CODSERIPS,
			ISNULL(RTRIM(cupsInf.DESSERIPS), B.DESSERIPS) AS DESSERIPS,
			0 QXPRINCIP,
			SUM(A.CANSERIPS) CANTIDAQX,
			'' CODVIAABO,
			NULL MedicoRealizo,
			Men.FECREAPRO FechaRealizacion,
			0 as Realizo,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			A.GENSERVICEORDER,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			'HCORDPROQ' EntityName,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			'Procedimientos Menores' AS Tipo
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDPROQ A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	JOIN dbo.HCINFPROM AS Men ON A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES And A.NUMEFOLIO = Men.NUMEFOLIO and A.CODSERIPS=Men.CODSERIPS
	LEFT JOIN dbo.INCUPSIPS cupsInf ON cupsInf.CODSERIPS = men.CODSERIPS
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPROQ' and acj.EntityTap='INDlcgProceduresQX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3 
	GROUP BY	Men.CODSERIPS,A.CODSERIPS,cupsInf.DESSERIPS,B.DESSERIPS,CODCONCEC,A.IPCODPACI, A.NUMINGRES,A.NUMEFOLIO,A.GENSERVICEORDER,i.IPNOMCOMP,
				cecd.Id, cd.Id, cd.Code, cd.Name,A.AUTO,A.CODCENATE,A.UFUCODIGO,Men.FECREAPRO,acj.Id,acj.CreationUser,
				acj.CreationDate,acj.JustificationId,bjc.Code,bjc.[Description],bjc.SkipClearance

UNION ALL
	
	SELECT	CAST(A.AUTO AS VARCHAR) CONSECUQXId,
			A.AUTO CONSECUQX,
			A.NUMEFOLIO,
			A.IPCODPACI,
			INGMH.NUMINGRES,
			A.CODCENATE,
			A.UFUCODIGO,
			ISNULL(MEN.CODSERIPS, A.CODSERIPS) AS CODSERIPS,
			ISNULL(RTRIM(cupsInf.DESSERIPS), B.DESSERIPS) AS DESSERIPS,
			0 QXPRINCIP,
			SUM(A.CANSERIPS) AS CANTIDAQX,
			'' CODVIAABO,
			NULL MedicoRealizo,
			Men.FECREAPRO FechaRealizacion,
			0 as Realizo,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			A.GENSERVICEORDER,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			'HCORDPROQ' EntityName,	
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			'Procedimientos Menores' AS Tipo
	FROM dbo.HCORDPROQ A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	JOIN dbo.HCINFPROM AS Men ON A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES And A.NUMEFOLIO = Men.NUMEFOLIO and A.CODSERIPS=Men.CODSERIPS
	LEFT JOIN dbo.INCUPSIPS cupsInf ON cupsInf.CODSERIPS = men.CODSERIPS
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPROQ' and acj.EntityTap='INDlcgProceduresQX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id	
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
	GROUP BY MEN.CODSERIPS,A.CODSERIPS,B.DESSERIPS,cupsInf.DESSERIPS,CODCONCEC,A.IPCODPACI, INGMH.NUMINGRES,A.NUMEFOLIO,A.GENSERVICEORDER, cecd.Id, cd.Id, cd.Code, cd.Name,
				A.AUTO,	A.IPCODPACI, A.CODCENATE, A.UFUCODIGO,Men.FECREAPRO,acj.Id,acj.CreationUser,acj.CreationDate
				,acj.CreationDate,acj.JustificationId,bjc.Code,bjc.[Description],bjc.SkipClearance