
CREATE VIEW [dbo].[VLaboratories]
AS
	SELECT	A.CODSERIPS,
			CAST(0 AS tinyint) AS Seleccione, 
			A.IPCODPACI, 
			A.NUMINGRES, 
			CASE  
				WHEN ESTSERIPS in('3','4') THEN 'Realizados' 
				WHEN ESTSERIPS = '4' THEN 'Realizados' 
				WHEN ESTSERIPS = '6' THEN 'Anulados' 
				WHEN ESTSERIPS ='1' THEN 'Solicitados' 
				ELSE 'No Realizados' 
			END AS  Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			SUM(A.CANSERIPS) AS TOTAL,
			i.IPNOMCOMP  as PersonName, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			a.ESTSERIPS
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDLABO A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
	GROUP BY A.CODSERIPS,B.DESSERIPS,A.IPCODPACI,A.NUMINGRES,A.ESTSERIPS,i.IPNOMCOMP,SERREASIT,ESTSERIPS, cecd.Id, cd.Id, cd.Code, cd.Name
UNION ALL
	SELECT	A.CODSERIPS,
			CAST(0 AS tinyint) AS Seleccione, 
			a.IPCODPACI, 
			INGMH.NUMINGRES, 
			CASE  
				WHEN ESTSERIPS in('3','4') THEN 'Realizados' 
				WHEN ESTSERIPS = '4' THEN 'Realizados' 
				WHEN ESTSERIPS = '6' THEN 'Anulados' 
				WHEN ESTSERIPS ='1' THEN 'Solicitados' 
				ELSE 'No Realizados' 
			END AS  Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			SUM(A.CANSERIPS) AS TOTAL,'
			Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			a.ESTSERIPS
	FROM dbo.HCORDLABO A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO   
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
	GROUP BY A.CODSERIPS,B.DESSERIPS,a.IPCODPACI,INGMH.NUMINGRES, A.ESTSERIPS,RN.NUMHIJREG,SERREASIT,ESTSERIPS, cecd.Id, cd.Id, cd.Code, cd.Name