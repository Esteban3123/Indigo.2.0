CREATE VIEW [dbo].[VImagesDX]
AS
	SELECT	A.CODSERIPS,
			CAST(0 AS tinyint) AS Seleccione, 
			a.IPCODPACI, 
			A.NUMINGRES,
			CASE WHEN SERREASIT='1' THEN 'Estudios Realizados en Sitio' WHEN  ESTSERIPS IN ('2','3','4') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulado' ELSE 'Estudios No Realizados' END AS Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			SUM(A.CANSERIPS) AS TOTAL,
			i.IPNOMCOMP  as PersonName, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDIMAG A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE A.MANEXTPRO = 0  OR ISNULL(ing.TRATAESPECIA, 0) = 3
	GROUP BY A.CODSERIPS,B.DESSERIPS,SERREASIT,ESTSERIPS,a.IPCODPACI,A.NUMINGRES,i.IPNOMCOMP, cecd.Id, cd.Id, cd.Code, cd.Name
UNION ALL
	SELECT	A.CODSERIPS,
			CAST(0 AS tinyint) AS Seleccione, 
			a.IPCODPACI, 
			INGMH.NUMINGRES,
			CASE WHEN SERREASIT='1' THEN 'Estudios Realizados en Sitio' WHEN  ESTSERIPS IN ('2','3','4') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulado' ELSE 'Estudios No Realizados' END AS Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			SUM(A.CANSERIPS) AS TOTAL,
			'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
	FROM dbo.HCORDIMAG A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE A.MANEXTPRO = 0  OR ISNULL(ing.TRATAESPECIA, 0) = 3
	GROUP BY A.CODSERIPS,B.DESSERIPS,SERREASIT,ESTSERIPS,a.IPCODPACI,INGMH.NUMINGRES,rn.NUMHIJREG, cecd.Id, cd.Id, cd.Code, cd.Name