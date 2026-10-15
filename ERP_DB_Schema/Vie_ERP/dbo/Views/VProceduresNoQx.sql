

CREATE VIEW [dbo].[VProceduresNoQx]
AS
		SELECT 
		datos.CODSERIPS,
		datos.Seleccione,
		datos.IPCODPACI,
		datos.NUMINGRES,
		datos.Tipo,
		datos.DESSERIPS,
		SUM(datos.TOTAL) TOTAL,
		datos.PersonName,
		datos.CUPSEntityContractDescriptionId,
		datos.ContractDescriptionId,
		datos.ContractDescriptionCodeName
	FROM 
	(
		SELECT	A.CODSERIPS,
				CAST(0 AS tinyint) AS Seleccione, 
				A.IPCODPACI, 
				A.NUMINGRES,
				case when MAX(A.ESTSERIPS) = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
				RTRIM(B.DESSERIPS) AS DESSERIPS,
				SUM(A.CANSERIPS) AS TOTAL,
				i.IPNOMCOMP  as PersonName,
				ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
				ISNULL(cd.Id, 0) ContractDescriptionId,
				ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
		FROM dbo.ADINGRESO ing
		JOIN dbo.HCORDPRON A ON ing.NUMINGRES = a.NUMINGRES
		JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
		LEFT JOIN dbo.HCINFPROM AS Men ON A.CODSERIPS=Men.CODSERIPS and A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES and A.NUMEFOLIO = Men.NUMEFOLIO 
		LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
		LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
		WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
		GROUP BY A.CODSERIPS,B.DESSERIPS,A.ESTSERIPS,SERREASIT,TIPSERIPS,A.IPCODPACI,A.NUMINGRES,i.IPNOMCOMP, cecd.Id, cd.Id, cd.Code, cd.Name
	UNION ALL
		SELECT	A.CODSERIPS,
				CAST(0 AS tinyint) AS Seleccione,
				A.IPCODPACI,
				INGMH.NUMINGRES,
				case when MAX(A.ESTSERIPS) = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
				RTRIM(B.DESSERIPS) AS DESSERIPS,
				SUM(A.CANSERIPS) AS TOTAL,
				'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName,
				ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
				ISNULL(cd.Id, 0) ContractDescriptionId,
				ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
		FROM dbo.HCORDPRON A 
		JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
		JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
		JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
		LEFT JOIN dbo.HCINFPROM AS Men ON A.CODSERIPS=Men.CODSERIPS and A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES and A.NUMEFOLIO = Men.NUMEFOLIO 
		LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
		LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
		WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
		GROUP BY A.CODSERIPS,B.DESSERIPS,A.ESTSERIPS,SERREASIT,TIPSERIPS,A.IPCODPACI,INGMH.NUMINGRES,rn.NUMHIJREG, cecd.Id, cd.Id, cd.Code, cd.Name
	) Datos
	GROUP BY datos.CODSERIPS, datos.Seleccione, datos.IPCODPACI, datos.NUMINGRES, datos.Tipo, datos.DESSERIPS, datos.PersonName, datos.CUPSEntityContractDescriptionId, datos.ContractDescriptionId, datos.ContractDescriptionCodeName