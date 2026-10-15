


CREATE VIEW [dbo].[ViewOxygenConsumption]
AS

SELECT	 A.IDCONOXIG as Row
		,A.FECHREGIS AS DIAS
		,E.CODSERIPS
		--,CASE WHEN A.GENSERVICEORDER is null THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS Seleccione
		,IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
		,A.LITRXMINUT
		,A.HORAINICIA
		,A.HORAFINAL
		,A.TOTHORAS
		,A.TOTLITADM
		,C.NOMMEDICO
		,C.CODIGONIT as NitMedico
		,C.CODPROSAL
		,C.CODESPEC1
		,A.IPCODPACI
		,A.NUMINGRES
		,A.CODVIAADM
		,A.CODCENATE
		,A.UFUCODIGO
		,D.UFUDESCRI
		,E.DESVIAADM
		,A.GENSERVICEORDER
		,i.IPNOMCOMP  as PersonName,
		acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCCONOXIG' EntityName
FROM dbo.HCCONOXIG AS A 
INNER JOIN dbo.INPROFSAL AS C ON A.CODPROSAL=C.CODPROSAL 
INNER JOIN dbo.INUNIFUNC AS D ON A.UFUCODIGO=D.UFUCODIGO 
INNER JOIN dbo.HCPARCONO E ON A.CODVIAADM=E.CODVIAADM
inner join dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.IDCONOXIG=acj.EntityId and acj.EntityName='HCCONOXIG' and acj.EntityTap='INDlcgOxigenConsumer'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id

union all

SELECT   A.IDCONOXIG as Row
		,A.FECHREGIS AS DIAS
		,E.CODSERIPS
		--,CASE WHEN A.GENSERVICEORDER is null THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS Seleccione
		,IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
		,A.LITRXMINUT
		,A.HORAINICIA
		,A.HORAFINAL
		,A.TOTHORAS
		,A.TOTLITADM
		,C.NOMMEDICO
		,C.CODIGONIT as NitMedico
		,C.CODPROSAL
		,C.CODESPEC1
		,A.IPCODPACI
		,INGMH.NUMINGRES
		,A.CODVIAADM
		,A.CODCENATE
		,A.UFUCODIGO
		,D.UFUDESCRI
		,E.DESVIAADM
		,A.GENSERVICEORDER
		,'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName,
		acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCCONOXIG' EntityName
FROM dbo.HCCONOXIG AS A 
INNER JOIN dbo.INPROFSAL AS C ON A.CODPROSAL=C.CODPROSAL 
INNER JOIN dbo.INUNIFUNC AS D ON A.UFUCODIGO=D.UFUCODIGO 
INNER JOIN dbo.HCPARCONO E ON A.CODVIAADM=E.CODVIAADM
inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.IDCONOXIG=acj.EntityId and acj.EntityName='HCCONOXIG' and acj.EntityTap='INDlcgOxigenConsumer'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
WHERE ING.IESTADOIN = 'C'