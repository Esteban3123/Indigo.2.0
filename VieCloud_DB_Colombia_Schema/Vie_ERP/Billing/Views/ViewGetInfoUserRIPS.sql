
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
				WHEN pacient.IPTIPOPAC = 14 THEN '13' --Tipo paciente "Especial o Excepción" + Tipo afiliado "No cotizante Ley 352 de 1997"
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
			cg.Code as CareGroupCode,
			COALESCE(
				NULLIF(LTRIM(RTRIM(furFur.SirasNumber)), ''),
				NULLIF(LTRIM(RTRIM(furIps.FiledSIRAS)), '')
			) registroSIRAS
	FROM Billing.Invoice i WITH(NOLOCK)
	JOIN Billing.InvoiceCategories ic WITH(NOLOCK) ON i.InvoiceCategoryId = ic.Id
	JOIN Contract.CareGroup cg WITH(NOLOCK) on i.CareGroupId = cg.Id
	JOIN .INPACIENT pacient WITH (NOLOCK) ON i.PatientCode = pacient.IPCODPACI
	JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON tip.CODIGO = pacient.IPTIPODOC
	left join cte_Country c WITH (NOLOCK) ON c.Id = pacient.IDPAIS
	left JOIN cte_INUBICACI ubication WITH (NOLOCK) ON ubication.AUUBICACI = pacient.AUUBICACI
	left JOIN cte_Country c2 WITH (NOLOCK) ON c2.Id = ubication.IDPAIS
	left join HCINCAPAC hci on i.AdmissionNumber = hci.NUMINGRES
	OUTER APPLY (
		SELECT TOP 1 f.SirasNumber
		FROM dbo.ADFURIPS f WITH(NOLOCK)
		WHERE f.AdmissionNumber = i.AdmissionNumber
			AND NULLIF(LTRIM(RTRIM(f.SirasNumber)), '') IS NOT NULL
		ORDER BY f.Id DESC
	) furFur
	OUTER APPLY (
		SELECT TOP 1 u.FiledSIRAS
		FROM dbo.ADFURIPSU u WITH(NOLOCK)
		WHERE u.NUMINGRES = i.AdmissionNumber
			AND NULLIF(LTRIM(RTRIM(u.FiledSIRAS)), '') IS NOT NULL
		ORDER BY u.Id DESC
	) furIps
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información del usuario (paciente) requerida en la sección de usuario para los RIPS electrónicos. Para cada factura activa (excluye notas crédito, débito y documentos equivalentes anulados), integra datos demográficos del paciente —tipo y número de documento de identidad, fecha de nacimiento, sexo, tipo de usuario según régimen de afiliación (contributivo, subsidiado, particular, ARL, SOAT, etc.)—, municipio y zona territorial de residencia, país de residencia y país de origen, categoría de la factura y grupo de atención del contrato. También indica si el paciente tiene una incapacidad médica asociada al ingreso facturado y obtiene el registro SIRAS desde los formularios FUR/FURIPS relacionados por el número de ingreso (AdmissionNumber). Combina las tablas de pacientes, facturas, tipos de documento, ubicaciones geográficas (municipios, departamentos, países), formularios FUR/FURIPS y el catálogo de incapacidades para producir el reporte estructurado exigido por la normativa RIPS electrónicos en Colombia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoUserRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoUserRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los datos del usuario/paciente asociados a cada factura activa en el formato requerido para los RIPS electrónicos (tipo de documento, tipo de usuario, sexo, ubicación, país, incapacidad y registro SIRAS).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoUserRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe tener Status = 1 (activa).; El DocumentType de la factura no puede ser 4, 6 ni 7.; El paciente referido por Invoice.PatientCode debe existir en INPACIENT.; El tipo de documento del paciente (IPTIPODOC) debe existir en ADTIPOIDENTIFICA.; Para resolver país de residencia y municipio, la ubicación (AUUBICACI) debe ligarse vía INMUNICIP a INDEPARTA.; El registro SIRAS se busca por Invoice.AdmissionNumber en ADFURIPS.AdmissionNumber y ADFURIPSU.NUMINGRES.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoUserRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El número de documento (numDocumentoIdentificacion) se entrega sin espacios (LTRIM/RTRIM sobre IPCODPACI).; La fecha de nacimiento se entrega siempre con formato ''yyyy-MM-dd''.; Si IPSEXOPAC no es 1, 2 ni 3, el sexo reportado siempre es ''F''.; Si TIPOUBICA es distinto de 2, la zona territorial siempre se considera urbana (''02'').; Las facturas anuladas o con DocumentType 4, 6 o 7 nunca aparecen en la vista.; El campo ''consecutivo'' siempre se devuelve como NULL.; País y ubicación se obtienen vía LEFT JOIN, por lo que pueden ser NULL si el paciente no tiene país o ubicación asociados.; ''registroSIRAS'' prioriza ADFURIPS.SirasNumber sobre ADFURIPSU.FiledSIRAS y puede ser NULL si no existe un valor no vacío para el mismo AdmissionNumber.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoUserRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS electrónicos; Paciente; Tipo de documento de identificación; Tipo de afiliado (cotizante/beneficiario/adicional); Régimen contributivo; Régimen subsidiado; Régimen especial o de excepción; ARL; SOAT; Planes voluntarios de salud; Particular; Incapacidad; Registro SIRAS; Formulario FUR; Formulario FURIPS; Ubicación geográfica (municipio/departamento/país); Zona territorial rural/urbana; Factura; Grupo de atención (CareGroup)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoUserRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por cada Billing.Invoice con Status=1 y DocumentType NOT IN (4,6,7), con la información del paciente formateada para RIPS.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoUserRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPOPAC = 1 AND IPTIPOAFI = 1 → tipoUsuario = ''01'' (Contributivo cotizante); si IPTIPOPAC = 1 AND IPTIPOAFI = 2 → tipoUsuario = ''02'' (Contributivo beneficiario); si IPTIPOPAC = 1 AND IPTIPOAFI = 3 → tipoUsuario = ''03'' (Contributivo adicional); si IPTIPOPAC = 2 → tipoUsuario = ''04'' (Subsidiado); si IPTIPOPAC = 3 → tipoUsuario = ''05'' (No afiliado); si IPTIPOPAC = 9 AND IPTIPOAFI = 1 → tipoUsuario = ''06'' (Especial/Excepción cotizante); si IPTIPOPAC = 9 AND IPTIPOAFI = 2 → tipoUsuario = ''07'' (Especial/Excepción beneficiario); si IPTIPOPAC = 10 → tipoUsuario = ''08'' (Privados de la libertad - Fondo Nacional de Salud); si IPTIPOPAC = 11 → tipoUsuario = ''09'' (Tomador/Amparado ARL); si IPTIPOPAC = 12 → tipoUsuario = ''10'' (Tomador/Amparado SOAT); si IPTIPOPAC = 13 → tipoUsuario = ''11'' (Tomador/Amparado Planes voluntarios); si IPTIPOPAC = 4 → tipoUsuario = ''12'' (Particular); si IPSEXOPAC = 1 / 2 / 3 → codSexo = ''M'' / ''F'' / ''I'' respectivamente else codSexo = ''F'' por defecto; si INUBICACI.TIPOUBICA = 2 → codZonaTerritorialResidencia = ''01'' (rural) else codZonaTerritorialResidencia = ''02'' (urbano); si Existe registro en HCINCAPAC con NUMINGRES = Invoice.AdmissionNumber → incapacidad = ''SI'' else incapacidad = ''NO''; para ''registroSIRAS'', se busca el último valor no vacío por AdmissionNumber en ADFURIPS y ADFURIPSU, priorizando ADFURIPS.SirasNumber.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoUserRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; Common.Country; Billing.Invoice; Billing.InvoiceCategories; Contract.CareGroup; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.HCINCAPAC; dbo.ADFURIPSInvoice; dbo.ADFURIPS; dbo.ADFURIPSU', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoUserRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoUserRIPS';
GO
