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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todas las órdenes de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias y otros estudios de imagen) solicitadas en historia clínica, combinando tanto las órdenes de pacientes regulares (con su ingreso y datos de identidad) como las órdenes asociadas a recién nacidos (registrando el número de hijo en lugar del nombre). Para cada orden muestra el código y descripción del servicio CUPS, el código y nombre del paciente, el número de ingreso, la cantidad total de estudios, el estado de realización (realizado en sitio, realizado, anulado o no realizado) y la descripción del concepto de contrato vinculado para efectos de facturación. Se usa principalmente en módulos de reportería de imágenes diagnósticas, seguimiento de solicitudes radiológicas y verificación de facturación de estudios de imagen por ingreso o episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VImagesDX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VImagesDX';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista las órdenes de imágenes diagnósticas de pacientes adultos y de recién nacidos, clasificándolas por estado de ejecución y enriqueciéndolas con datos de paciente, CUPS y descripción de contrato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de órdenes de imágenes, ingresos, CUPS y pacientes deben estar pobladas y referencialmente consistentes; Para el segundo bloque, deben existir registros en HCINGRESORECNAC y HCRECINAC que vinculen al recién nacido con su ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes de imágenes con MANEXTPRO=0, salvo cuando el ingreso tenga TRATAESPECIA=3 (tratamiento especial), caso en el que también se incluyen; Las cantidades (CANSERIPS) se totalizan por combinación de paciente, ingreso, código de servicio y estado; La descripción de contrato y CUPS-entidad-contrato son opcionales (LEFT JOIN); si no existen, se devuelven Id=0 y cadena vacía; Para recién nacidos, el ingreso usado es el del hijo (NUMINGRESHIJO) y la persona se identifica por el número de hijo, no por el paciente; El campo Seleccione siempre se devuelve como 0 (marcador para selección en UI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas; Ingresos/admisiones de pacientes; Códigos CUPS; Descripciones de contrato; Recién nacidos / hijos de madre hospitalizada; Estados de servicio (realizado, anulado, no realizado, en sitio); Tratamiento especial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve órdenes de imágenes agrupadas por paciente/ingreso/servicio cuando MANEXTPRO=0 o TRATAESPECIA=3, con clasificación del tipo de estudio según SERREASIT y ESTSERIPS, sumando CANSERIPS; [RETURN_RESULT] resultset: Para ingresos de recién nacidos (vinculados vía HCINGRESORECNAC/HCRECINAC) devuelve las imágenes etiquetando la persona como ''Hijo N'' donde N es NUMHIJREG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SERREASIT = ''1'' → Clasifica como ''Estudios Realizados en Sitio'' else Evalúa siguiente condición; si ESTSERIPS IN (''2'',''3'',''4'') → Clasifica como ''Estudios Realizados'' else Evalúa siguiente condición; si ESTSERIPS = ''6'' → Clasifica como ''Anulado'' else Clasifica como ''Estudios No Realizados''; si El ingreso corresponde a un recién nacido (existe vínculo en HCINGRESORECNAC y HCRECINAC) → El nombre de la persona se construye como ''Hijo '' + número de hijo del registro de recién nacido else Se usa el nombre completo del paciente desde INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDIMAG; dbo.INCUPSIPS; dbo.INPACIENT; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VImagesDX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VImagesDX';
GO
