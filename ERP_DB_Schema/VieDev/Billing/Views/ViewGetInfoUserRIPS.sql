
-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2023-12-09
-- Description:	Vista encargada de obetner la informacion de la seccion ususiaio para RIPS electronicos
-- =============================================

CREATE VIEW [Billing].[ViewGetInfoUserRIPS]
AS
	with cte_INUBICACI AS (	select inu.*,ind.IDPAIS
							from [dbo].[INUBICACI] inu WITH(NOLOCK)
							JOIN INMUNICIP inm  WITH(NOLOCK) on inu.DEPMUNCOD = inm.DEPMUNCOD 
							JOIN INDEPARTA ind WITH(NOLOCK) on inm.DEPCODIGO = ind.depcodigo),
		
		cte_Country AS (select * from Common.Country c WITH(NOLOCK))

	select 
			i.InvoiceNumber,
			tip.SIGLA tipoDocumentoIdentificacion,
			LTRIM(RTRIM(pacient.IPCODPACI)) numDocumentoIdentificacion,
			 case 
				WHEN pacient.IPTIPOPAC = 1 AND IPTIPOAFI = 1 THEN '01' --Tipo paciente "Contributivo" + Tipo afiliado "Cotizante" = 01: Contributivo cotizante
				WHEN pacient.IPTIPOPAC = 1 AND IPTIPOAFI = 2 THEN '02' --Tipo paciente "Contributivo" + Tipo afiliado "Beneficiario" = 02: Contributivo beneficiario
				WHEN pacient.IPTIPOPAC = 1 AND IPTIPOAFI = 3 THEN '03' --Tipo paciente "Contributivo" + Tipo afiliado "Adicional" = 03: Contributivo adicional
				WHEN pacient.IPTIPOPAC = 2 THEN '04' --Tipo paciente "Subsidiado" = 04: Subsidiado
				WHEN pacient.IPTIPOPAC = 3 THEN '05' --Tipo paciente "No afiliado" = 05: No afiliado
				WHEN pacient.IPTIPOPAC = 9 AND IPTIPOAFI = 1 THEN '06' --Tipo paciente "Especial o Excepción" + Tipo afiliado "Cotizante" = 06: Especial o Excepción cotizante
				WHEN pacient.IPTIPOPAC = 9 AND IPTIPOAFI = 2 THEN '07' --Tipo paciente "Especial o Excepción" + Tipo afiliado "Beneficiario" = 07: Especial o Excepción beneficiario
				WHEN pacient.IPTIPOPAC = 10 THEN '08' --Tipo paciente "Personas privadas de la libertad a cargo del Fondo Nacional de Salud" = 08: Personas privadas de la libertad a cargo del Fondo Nacional de Salud
				WHEN pacient.IPTIPOPAC = 11 THEN '09' --Tipo paciente "Tomador/ Amparado ARL" = 09: Tomador / Amparado ARL
				WHEN pacient.IPTIPOPAC = 12 THEN '10' --Tipo paciente "Tomador/ Amparado SOAT" = 10: Tomador / Amparado SOAT
				WHEN pacient.IPTIPOPAC = 13 THEN '11' --Tipo paciente "Tomador/ Amparado Planes voluntarios de salud" = 11: Tomador / Amparado Planes voluntarios de salud
				WHEN pacient.IPTIPOPAC = 4 THEN '12' --Tipo paciente "Particular" = 12: Particular
			end tipoUsuario,
			FORMAT(pacient.IPFECNACI, 'yyyy-MM-dd')  fechaNacimiento,
			CASE pacient.IPSEXOPAC 
					WHEN 1 THEN 'M' 
					WHEN 2 THEN 'F' 
					WHEN 3 THEN 'I'
					ELSE 'F'  
			END codSexo,
			c2.StandardCodeNumeric codPaisResidencia,
			ubication.DEPMUNCOD codMunicipioResidencia,
			CASE ubication.TIPOUBICA 
					WHEN 2 THEN '01' --rural
					ELSE '02' --urbano
			END  codZonaTerritorialResidencia, --ARREGLADO
			IIF(hci.CODCONSEC is NOT null,'SI','NO') incapacidad, 
			c.StandardCodeNumeric codPaisOrigen, 
			NULL consecutivo,
			i.DocumentType,
			i.InvoiceDate as DocumentDate,
			ic.Code AS CategoryInvoiceCode,
			cg.Code as CareGroupCode
	FROM Billing.Invoice i WITH(NOLOCK)
	JOIN Billing.InvoiceCategories ic WITH(NOLOCK) ON i.InvoiceCategoryId = ic.Id
	JOIN Contract.CareGroup cg WITH(NOLOCK) on i.CareGroupId = cg.Id
	JOIN .INPACIENT pacient WITH (NOLOCK) ON i.PatientCode = pacient.IPCODPACI
	JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON tip.CODIGO = pacient.IPTIPODOC
	left join cte_Country c WITH (NOLOCK) ON c.Id = pacient.IDPAIS
	left JOIN cte_INUBICACI ubication WITH (NOLOCK) ON ubication.AUUBICACI = pacient.AUUBICACI
	left JOIN cte_Country c2 WITH (NOLOCK) ON c2.Id = ubication.IDPAIS
	left join HCINCAPAC hci on i.AdmissionNumber = hci.NUMINGRES
	WHERE i.DocumentType NOT IN (4,6,7) and i.Status = 1


	--UNION ALL

	--select 
	--		STRING_AGG(i.InvoiceNumber,', ') AS InvoiceNumber,
	--		tip.SIGLA tipoDocumentoIdentificacion,
	--		LTRIM(RTRIM(pacient.IPCODPACI)) numDocumentoIdentificacion,
	--		 case 
	--			WHEN pacient.IPTIPOPAC = 1 AND IPTIPOAFI = 1 THEN '01' --Tipo paciente "Contributivo" + Tipo afiliado "Cotizante" = 01: Contributivo cotizante
	--			WHEN pacient.IPTIPOPAC = 1 AND IPTIPOAFI = 2 THEN '02' --Tipo paciente "Contributivo" + Tipo afiliado "Beneficiario" = 02: Contributivo beneficiario
	--			WHEN pacient.IPTIPOPAC = 1 AND IPTIPOAFI = 3 THEN '03' --Tipo paciente "Contributivo" + Tipo afiliado "Adicional" = 03: Contributivo adicional
	--			WHEN pacient.IPTIPOPAC = 2 THEN '04' --Tipo paciente "Subsidiado" = 04: Subsidiado
	--			WHEN pacient.IPTIPOPAC = 3 THEN '05' --Tipo paciente "No afiliado" = 05: No afiliado
	--			WHEN pacient.IPTIPOPAC = 9 AND IPTIPOAFI = 1 THEN '06' --Tipo paciente "Especial o Excepción" + Tipo afiliado "Cotizante" = 06: Especial o Excepción cotizante
	--			WHEN pacient.IPTIPOPAC = 9 AND IPTIPOAFI = 2 THEN '07' --Tipo paciente "Especial o Excepción" + Tipo afiliado "Beneficiario" = 07: Especial o Excepción beneficiario
	--			WHEN pacient.IPTIPOPAC = 10 THEN '08' --Tipo paciente "Personas privadas de la libertad a cargo del Fondo Nacional de Salud" = 08: Personas privadas de la libertad a cargo del Fondo Nacional de Salud
	--			WHEN pacient.IPTIPOPAC = 11 THEN '09' --Tipo paciente "Tomador/ Amparado ARL" = 09: Tomador / Amparado ARL
	--			WHEN pacient.IPTIPOPAC = 12 THEN '10' --Tipo paciente "Tomador/ Amparado SOAT" = 10: Tomador / Amparado SOAT
	--			WHEN pacient.IPTIPOPAC = 13 THEN '11' --Tipo paciente "Tomador/ Amparado Planes voluntarios de salud" = 11: Tomador / Amparado Planes voluntarios de salud
	--			WHEN pacient.IPTIPOPAC = 4 THEN '12' --Tipo paciente "Particular" = 12: Particular
	--		end tipoUsuario,
	--		FORMAT(pacient.IPFECNACI, 'yyyy-MM-dd')  fechaNacimiento,
	--		CASE pacient.IPSEXOPAC 
	--				WHEN 1 THEN 'M' 
	--				WHEN 2 THEN 'F' 
	--				WHEN 3 THEN 'I'
	--				ELSE 'F' 
	--		END codSexo,
	--		c2.StandardCodeNumeric codPaisResidencia,
	--		ubication.DEPMUNCOD codMunicipioResidencia,
	--		CASE ubication.TIPOUBICA 
	--				WHEN 2 THEN '01' --rural
	--				ELSE '02' --urbano
	--		END  codZonaTerritorialResidencia, --ARREGLADO
	--		IIF(MAX(coalesce(hci.flag,0)) = 1,'SI','NO') incapacidad, 
	--		c.StandardCodeNumeric codPaisOrigen, 
	--		NULL consecutivo,
	--		CAST( 5 AS TINYINT) as DocumentType,
	--		cast(i.InvoiceDate as date) as DocumentDate,
	--		ic.Code AS CategoryInvoiceCode,
	--		cg.Code as CareGroupCode 
	--FROM Billing.Invoice i WITH(NOLOCK)
	--JOIN Billing.InvoiceCategories ic WITH(NOLOCK) ON i.InvoiceCategoryId = ic.Id
	--JOIN Contract.CareGroup cg WITH(NOLOCK) on i.CareGroupId = cg.Id
	--JOIN .INPACIENT pacient WITH (NOLOCK) ON i.PatientCode = pacient.IPCODPACI
	--JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON tip.CODIGO = pacient.IPTIPODOC
	--join cte_Country c WITH (NOLOCK) ON c.Id = pacient.IDPAIS
	--JOIN cte_INUBICACI ubication WITH (NOLOCK) ON ubication.AUUBICACI = pacient.AUUBICACI
	--JOIN cte_Country c2 WITH (NOLOCK) ON c2.Id = ubication.IDPAIS
	--left join(	SELECT hci.NUMINGRES, cast( 1 as bit) flag
	--			from HCINCAPAC hci WITH(NOLOCK) 
	--			GROUP by hci.NUMINGRES) hci on hci.NUMINGRES = i.AdmissionNumber
	--WHERE i.DocumentType =5 and i.Status = 1
	--GROUP BY tip.SIGLA,
	--		pacient.IPCODPACI,
	--		pacient.IPTIPOPAC, 
	--		pacient.IPTIPOAFI,
	--		pacient.IPFECNACI,
	--		pacient.IPSEXOPAC,
	--		c2.StandardCodeNumeric,
	--		ubication.DEPMUNCOD,
	--		ubication.TIPOUBICA,
	--		c.StandardCodeNumeric,
	--		cast(i.InvoiceDate as date),
	--		ic.Code,cg.Code