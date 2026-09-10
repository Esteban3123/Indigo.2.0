
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida las órdenes médicas internas (exámenes, procedimientos, laboratorio, imágenes) de un ingreso hospitalario, agrupadas por servicio CUPS, paciente y número de ingreso. Distingue entre órdenes solicitadas y órdenes con respuesta, y muestra el total de unidades ordenadas por servicio. Combina datos del ingreso (ADINGRESO), las órdenes médicas (HCORDINTE), el catálogo de servicios CUPS (INCUPSIPS) y el nombre del paciente (INPACIENT); además incorpora, cuando aplica, la descripción del contrato asociada al servicio (CUPSEntityContractDescriptions, ContractDescriptions) para vincular la orden con la forma de facturación pactada. Incluye un segundo bloque para recién nacidos, donde el ingreso del hijo se asocia al ingreso de la madre, mostrando el número de registro del hijo como nombre de la persona. Sirve como base para reportería de consumo de servicios ordenados, seguimiento de respuestas y vinculación con contratos de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewConsultation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las órdenes internas de servicios médicos por ingreso (incluyendo recién nacidos asociados al ingreso de la madre), clasificándolas como solicitadas o con respuesta y vinculándolas con la descripción contractual de facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe existir en ADINGRESO y tener órdenes en HCORDINTE.; Para el bloque de recién nacidos, debe existir vínculo en HCINGRESORECNAC y registro en HCRECINAC asociados al ingreso del hijo.; Los servicios deben estar catalogados en INCUPSIPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes cuyo manejo no sea externo (MANEXTPRO=0) salvo que el ingreso sea tratamiento especial tipo 3.; Toda orden mostrada está asociada a un ingreso válido y a un servicio CUPS catalogado.; Los identificadores de descripción contractual nunca son nulos en la salida (se sustituyen por 0 o cadena vacía cuando no hay vínculo).; Las órdenes del recién nacido se reportan bajo el NUMINGRES del vínculo madre-hijo (HCINGRESORECNAC.NUMINGRES) y no bajo el ingreso original del hijo.; La clasificación Solicitadas/Con Respuesta depende exclusivamente de la existencia de NUMFOLINT (folio interno de respuesta).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión hospitalaria; Órdenes médicas internas; Servicios CUPS; Paciente; Recién nacido vinculado a la madre; Folio interno de respuesta; Descripción de contrato; Manejo externo / Tratamiento especial; Solicitadas vs Con Respuesta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewConsultation: Devuelve la suma de cantidades ordenadas (CANSERIPS) agrupada por servicio, ingreso, paciente y descripción de contrato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NUMFOLINT IS NULL → La orden se clasifica como ''Solicitadas'' else La orden se clasifica como ''Con Respuesta''; si A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3 → Se incluye la orden en el resultado (no es manejo externo, o el ingreso es de tratamiento especial tipo 3) else La orden se excluye del resultado; si Origen del registro: ingreso propio vs. ingreso de recién nacido → Si es ingreso normal, PersonName = nombre del paciente (IPNOMCOMP); si es recién nacido vinculado a la madre, PersonName = ''Hijo '' + número de registro del hijo (NUMHIJREG)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDINTE; dbo.INCUPSIPS; dbo.INPACIENT; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewConsultation';
GO
