
CREATE VIEW [dbo].[VPathologies]
AS
SELECT 
	Pathologies.CODSERIPS,
	Pathologies.Seleccione,
	Pathologies.IPCODPACI,
	Pathologies.NUMINGRES,
	Pathologies.Tipo,
	Pathologies.DESSERIPS,
	SUM(Pathologies.TOTAL) TOTAL,
	Pathologies.PersonName,
	Pathologies.CUPSEntityContractDescriptionId,
	Pathologies.ContractDescriptionId,
	Pathologies.ContractDescriptionCodeName
FROM 
(
		SELECT	A.CODSERIPS,
				CAST(0 AS tinyint) AS Seleccione, 
				a.IPCODPACI, 
				A.NUMINGRES, 
				CASE 
					WHEN ESTSERIPS IN ('2','3','4') THEN 'Realizados' 
					when ESTSERIPS = '6' then 'Anulados'
					when ESTSERIPS = '1' then 'Solicitados'
					ELSE 'No Realizados' 
				END AS Tipo,
				RTRIM(B.DESSERIPS) AS DESSERIPS,
				SUM(A.CANSERIPS) AS TOTAL,
				i.IPNOMCOMP  as PersonName,
				ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
				ISNULL(cd.Id, 0) ContractDescriptionId, 
				ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
			FROM dbo.ADINGRESO ing
			JOIN dbo.HCORDPATO A ON ing.NUMINGRES = a.NUMINGRES
			JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
			JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
			LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
			LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
			WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
			GROUP BY A.CODSERIPS,B.DESSERIPS,ARSCODIGO,TIPSERIPS,a.IPCODPACI,A.NUMINGRES,i.IPNOMCOMP,ESTSERIPS, cecd.Id, cd.Id, cd.Code, cd.Name
	UNION ALL
		SELECT	A.CODSERIPS,
				CAST(0 AS tinyint) AS Seleccione,
				a.IPCODPACI,
				INGMH.NUMINGRES,
				CASE 
					WHEN ESTSERIPS IN ('2','3','4') THEN 'Realizados' 
					when ESTSERIPS = '6' then 'Anulados'
					when ESTSERIPS = '1' then 'Solicitados'
					ELSE 'No Realizados' 
				END AS Tipo,
				RTRIM(B.DESSERIPS) AS DESSERIPS,
				SUM(A.CANSERIPS) AS TOTAL,
				'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName, 
				ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
				ISNULL(cd.Id, 0) ContractDescriptionId, 
				ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
		FROM dbo.HCORDPATO A 
		JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
		JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
		JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO   
		LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
		LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
		WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
		GROUP BY A.CODSERIPS,B.DESSERIPS,ARSCODIGO,TIPSERIPS,a.IPCODPACI,INGMH.NUMINGRES,RN.NUMHIJREG,ESTSERIPS, cecd.Id, cd.Id, cd.Code, cd.Name
) Pathologies
GROUP BY Pathologies.CODSERIPS, Pathologies.Seleccione, Pathologies.IPCODPACI, Pathologies.NUMINGRES, Pathologies.Tipo, Pathologies.DESSERIPS, Pathologies.PersonName, Pathologies.CUPSEntityContractDescriptionId, Pathologies.ContractDescriptionId, Pathologies.ContractDescriptionCodeName