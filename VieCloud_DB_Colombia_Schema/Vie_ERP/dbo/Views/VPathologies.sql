
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de órdenes de patología e imágenes diagnósticas (exámenes de laboratorio, estudios de imagen y procedimientos diagnósticos CUPS) solicitadas o realizadas durante un ingreso, tanto para pacientes adultos como para recién nacidos registrados en el módulo de neonatología. Combina las órdenes médicas de patología (HCORDPATO) con los datos del ingreso (ADINGRESO), el catálogo de servicios CUPS (INCUPSIPS) y la información del paciente (INPACIENT), clasificando cada examen según su estado: Solicitado, Realizado o Anulado, con su cantidad total. Incluye el concepto de facturación o descripción de contrato asociada al servicio (ContractDescriptions / CUPSEntityContractDescriptions), permitiendo identificar cómo se factura cada procedimiento diagnóstico dentro del contrato vigente. Se usa en la interfaz clínica y de facturación para consultar, seleccionar y gestionar los exámenes de patología e imágenes de un ingreso, incluyendo los servicios prestados a hijos recién nacidos vinculados al ingreso de la madre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VPathologies';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las órdenes de patología/imágenes diagnósticas tanto del paciente titular del ingreso como de los recién nacidos asociados, clasificándolas por estado (Realizados, Anulados, Solicitados, No Realizados) y enriqueciéndolas con datos de contrato CUPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben existir en HCORDPATO vinculadas a un ingreso válido (ADINGRESO).; El código de servicio (CODSERIPS) debe existir en el catálogo INCUPSIPS.; Para el bloque de recién nacidos, debe existir vínculo en HCINGRESORECNAC y registro en HCRECINAC para el ingreso del hijo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El total de órdenes (TOTAL) se calcula sumando CANSERIPS y se reagrega tras la unión de los dos bloques.; Los identificadores de contrato CUPS (CUPSEntityContractDescriptionId, ContractDescriptionId) se devuelven como 0 cuando no hay relación, y la descripción concatenada como cadena vacía.; Toda orden cuya MANEXTPRO sea distinta de 0 sólo aparece si el ingreso es de tratamiento especial (TRATAESPECIA = 3).; Las órdenes del ingreso titular y las del ingreso del recién nacido se unifican en una sola vista (UNION ALL) sin duplicar el filtro de exclusión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de patología; Imágenes diagnósticas; Ingreso hospitalario; Paciente; Recién nacido; Estado de la orden (Realizado/Anulado/Solicitado/No Realizado); CUPS; Contrato/Descripción de contrato; Tratamiento especial; Manejo externo del procedimiento (MANEXTPRO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve únicamente órdenes donde MANEXTPRO = 0 o el ingreso tiene TRATAESPECIA = 3 (tratamiento especial); las demás se excluyen del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS IN (''2'',''3'',''4'') → Clasifica la orden como ''Realizados''; si ESTSERIPS = ''6'' → Clasifica la orden como ''Anulados''; si ESTSERIPS = ''1'' → Clasifica la orden como ''Solicitados''; si ESTSERIPS no coincide con ninguno de los anteriores → Clasifica la orden como ''No Realizados''; si A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA,0) = 3 → Incluye la orden en el resultado else Excluye la orden del resultado; si La orden pertenece al ingreso de un recién nacido (existe en HCINGRESORECNAC como NUMINGRESHIJO) → Asigna PersonName = ''Hijo '' + número de hijo registrado (NUMHIJREG) else Asigna PersonName = nombre completo del paciente (IPNOMCOMP)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDPATO; dbo.INCUPSIPS; dbo.INPACIENT; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VPathologies';
GO
