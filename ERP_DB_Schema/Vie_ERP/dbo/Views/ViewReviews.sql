

CREATE VIEW [dbo].[ViewReviews]
AS

	SELECT A.ID
	,RTRIM(E.NOMCENATE) AS NOMCENATE
	,IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
	,A.NUMEFOLIO as Folio
	,B.CODPROSAL
	,B.CODIGONIT as NitMedico
	,B.NOMMEDICO as Medico
	,cast((case when ing.CODESPTRA is not null And ing.CODESPTRA = C.CODESPECI 
		then 1 else 0 end) as bit) as Tratante
	,ing.CODESPTRA
	,D.UFUCODIGO
	,RTRIM(D.UFUDESCRI) AS UFUDESCRI
	--,ing.CODESPTRA
	,C.CODESPECI
	,RTRIM(C.DESESPECI) AS DESESPECI
	,FECHISPAC
	,a.IPCODPACI
	,A.NUMINGRES
	,i.IPNOMCOMP  as PersonName
	,A.GENSERVICEORDER
	,s.CODSERIPSINTRA as CUPSManejo
	,s.IDDESCRIPCIONRELACIONADA_INTRA as CUPSEntityContractDescriptionIdManejo
	,contDesManejo.ContractDescriptionId as ContractDescriptionIdManejo
	,s.CODSERINT as CUPSInterconsulta
	,s.IDDESCRIPCIONRELACIONADA_INTER as CUPSEntityContractDescriptionIdInterconsulta
	,contDesInterConsulta.ContractDescriptionId as ContractDescriptionIdInterconsulta
	,s.CODSERCEX as CUPSControl
	,s.IDDESCRIPCIONRELACIONADA_CONS as CUPSEntityContractDescriptionIdControl
	,contDesControl.ContractDescriptionId as ContractDescriptionIdControl
	,cast(iif(ad.NUMEFOLIO is null,0,1) as bit) FirstEmergencyCare
	,iif(ad.NUMEFOLIO is null,NULL,s.CodServiceUrg)  as CupsEmergency
	,iif(ad.NUMEFOLIO is null,NULL,contDesEmergency.Id)  as CUPSEntityContractDescriptionIdEmergency
	,iif(ad.NUMEFOLIO is null,NULL,contDesEmergency.ContractDescriptionId)  as ContractDescriptionIdEmergency,
	acj.Id ACJustificationId,
	acj.CreationUser ACCreationUser,
	acj.CreationDate ACCreationDate,
	acj.JustificationId,
	iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
	iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
	'HCHISPACA' EntityName,
	D.UFUTIPUNI
	FROM dbo.HCHISPACA A  
	INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
	INNER JOIN dbo.INESPECIA C ON c.CODESPECI = ISNULL(A.CODESPTRA,B.CODESPEC1)	
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	INNER JOIN dbo.ADCENATEN E ON A.CODCENATE=E.CODCENATE
	inner join dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	inner join dbo.ADINGRESO ing on A.NUMINGRES = ing.NUMINGRES
	left join dbo.HCESPSERU s on s.CODESPECI = C.CODESPECI
	left join (SELECT NUMEFOLIO,NUMINGRES
				FROM ADATEINIU ad WITH(NOLOCK)
				GROUP BY NUMEFOLIO,NUMINGRES ) ad ON ad.NUMEFOLIO = A.NUMEFOLIO AND ad.NUMINGRES = A.NUMINGRES
	left join Contract.CUPSEntityContractDescriptions contDesManejo WITH(NOLOCK) on contDesManejo.Id = s.IDDESCRIPCIONRELACIONADA_INTRA
	left join Contract.CUPSEntityContractDescriptions contDesInterConsulta WITH(NOLOCK) on contDesInterConsulta.Id = s.IDDESCRIPCIONRELACIONADA_INTER
	left join Contract.CUPSEntityContractDescriptions contDesControl WITH(NOLOCK) on contDesControl.Id = s.IDDESCRIPCIONRELACIONADA_CONS
	left join Contract.CUPSEntityContractDescriptions contDesEmergency WITH(NOLOCK) on  contDesEmergency.Id = s.IdRelatedDescription_Urg
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.ID=acj.EntityId and acj.EntityName='HCHISPACA' and acj.EntityTap='INDlcgValoraciones'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id

	union all

	SELECT A.ID
	,RTRIM(E.NOMCENATE) AS NOMCENATE	
	,IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
	,A.NUMEFOLIO as Folio
	,B.CODPROSAL
	,B.CODIGONIT as NitMedico
	,B.NOMMEDICO as Medico
	,cast((case when ing.CODESPTRA is not null And ing.CODESPTRA = C.CODESPECI 
		then 1 else 0 end) as bit) as Tratante
	,ing.CODESPTRA
	,D.UFUCODIGO
	,RTRIM(D.UFUDESCRI) AS UFUDESCRI
	--,ing.CODESPTRA
	,C.CODESPECI
	,RTRIM(C.DESESPECI) AS DESESPECI
	,a.FECHISPAC
	,a.IPCODPACI
	,INGMH.NUMINGRES
	,'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName
	,A.GENSERVICEORDER
	,s.CODSERIPSINTRA as CUPSManejo
	,s.IDDESCRIPCIONRELACIONADA_INTRA as CUPSEntityContractDescriptionIdManejo
	,contDesManejo.ContractDescriptionId as ContractDescriptionIdManejo
	,s.CODSERINT as CUPSInterconsulta
	,s.IDDESCRIPCIONRELACIONADA_INTER as CUPSEntityContractDescriptionIdInterconsulta
	,contDesInterConsulta.ContractDescriptionId as ContractDescriptionIdInterconsulta
	,s.CODSERCEX as CUPSControl
	,s.IDDESCRIPCIONRELACIONADA_CONS as CUPSEntityContractDescriptionIdControl
	,contDesControl.ContractDescriptionId as ContractDescriptionIdControl
	,cast(iif(ad.NUMEFOLIO is null,0,1) as bit) FirstEmergencyCare
	,iif(ad.NUMEFOLIO is null,NULL,s.CodServiceUrg)  as CupsEmergency
	,iif(ad.NUMEFOLIO is null,NULL,contDesEmergency.Id)  as CUPSEntityContractDescriptionIdEmergency
	,iif(ad.NUMEFOLIO is null,NULL,contDesEmergency.ContractDescriptionId)  as ContractDescriptionIdEmergency,
	acj.Id ACJustificationId,
	acj.CreationUser ACCreationUser,
	acj.CreationDate ACCreationDate,
	acj.JustificationId,
	iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
	iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
	'HCHISPACA' EntityName,
	D.UFUTIPUNI
	FROM dbo.HCHISPACA A  
	INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
	INNER JOIN dbo.INESPECIA C ON c.CODESPECI = ISNULL(A.CODESPTRA,B.CODESPEC1)	
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	INNER JOIN dbo.ADCENATEN E ON A.CODCENATE=E.CODCENATE
	inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
	left join dbo.HCESPSERU s on s.CODESPECI = C.CODESPECI
	left join (SELECT NUMEFOLIO,NUMINGRES
				FROM ADATEINIU ad WITH(NOLOCK)
				GROUP BY NUMEFOLIO,NUMINGRES ) ad ON ad.NUMEFOLIO = A.NUMEFOLIO AND ad.NUMINGRES = A.NUMINGRES
	left join Contract.CUPSEntityContractDescriptions contDesManejo on contDesManejo.Id = s.IDDESCRIPCIONRELACIONADA_INTRA
	left join Contract.CUPSEntityContractDescriptions contDesInterConsulta on contDesInterConsulta.Id = s.IDDESCRIPCIONRELACIONADA_INTER
	left join Contract.CUPSEntityContractDescriptions contDesControl on contDesControl.Id = s.IDDESCRIPCIONRELACIONADA_CONS
	left join Contract.CUPSEntityContractDescriptions contDesEmergency WITH(NOLOCK) on  contDesEmergency.Id = s.IdRelatedDescription_Urg
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.ID=acj.EntityId and acj.EntityName='HCHISPACA' and acj.EntityTap='INDlcgValoraciones'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	where ING.IESTADOIN = 'C'