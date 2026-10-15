
CREATE VIEW [dbo].[ViewConsultation]
AS
	SELECT 
		HCRDINTE.CODSERIPS,
		HCRDINTE.Seleccione,
		HCRDINTE.IPCODPACI,
		HCRDINTE.NUMINGRES,
		HCRDINTE.Tipo,
		HCRDINTE.DESSERIPS,
		SUM(HCRDINTE.TOTAL) TOTAL,
		HCRDINTE.PersonName,
		HCRDINTE.CUPSEntityContractDescriptionId,
		HCRDINTE.ContractDescriptionId,
		HCRDINTE.ContractDescriptionCodeName
	FROM 
	(
		SELECT	A.CODSERIPS,
				CAST(0 AS tinyint) AS Seleccione,
				a.IPCODPACI,
				A.NUMINGRES,
				CASE WHEN NUMFOLINT IS NULL THEN 'Solicitadas' ELSE 'Con Respuesta' END AS Tipo,
				RTRIM(B.DESSERIPS) AS DESSERIPS,
				SUM(A.CANSERIPS) AS TOTAL,
				i.IPNOMCOMP  as PersonName,
				ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
				ISNULL(cd.Id, 0) ContractDescriptionId,
				ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
		FROM dbo.ADINGRESO ing
		JOIN dbo.HCORDINTE A ON ing.NUMINGRES = a.NUMINGRES
		JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
		LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
		LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
		WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
		GROUP BY A.CODSERIPS,B.DESSERIPS,ARSCODIGO, NUMFOLINT,TIPSERIPS,a.IPCODPACI,A.NUMINGRES,i.IPNOMCOMP, cecd.Id, cd.Id, cd.Code, cd.Name
	union all
		SELECT	A.CODSERIPS,
				CAST(0 AS tinyint) AS Seleccione,
				a.IPCODPACI,
				INGMH.NUMINGRES,
				CASE WHEN NUMFOLINT IS NULL THEN 'Solicitadas' ELSE 'Con Respuesta' END AS Tipo,
				RTRIM(B.DESSERIPS) AS DESSERIPS,
				SUM(A.CANSERIPS) AS TOTAL,
				'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName,
				ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
				ISNULL(cd.Id, 0) ContractDescriptionId,
				ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
		FROM dbo.HCORDINTE A 
		JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
		JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
		JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
		LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
		LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
		WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
		GROUP BY A.CODSERIPS,B.DESSERIPS,ARSCODIGO, NUMFOLINT,TIPSERIPS,a.IPCODPACI,INGMH.NUMINGRES,rn.NUMHIJREG, cecd.Id, cd.Id, cd.Code, cd.Name
	) HCRDINTE
	GROUP BY 	HCRDINTE.CODSERIPS, HCRDINTE.Seleccione, HCRDINTE.IPCODPACI, HCRDINTE.NUMINGRES, HCRDINTE.Tipo, HCRDINTE.DESSERIPS, HCRDINTE.PersonName, HCRDINTE.CUPSEntityContractDescriptionId, HCRDINTE.ContractDescriptionId, HCRDINTE.ContractDescriptionCodeName