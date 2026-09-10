

CREATE VIEW [Report].[ViewVerficacionCUPS] AS
SELECT DISTINCT
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 ce.Code [Cod. CUPS],
 CASE ce.status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END AS [Estado CUP],
 CASE ce.financedresourceUPC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END AS [Financiado con Recursos de la UPC],
 ce.Description [Descripción CUPS],
 cg.Code as CodigoGrupo,
 cg.Description [Grupo CUPS],
 csg.Code as CodigoSubGrupo,
 csg.Description [Subgrupo CUPS],
 cd.Code as CodDescripcionRelacionada,
 cd.Name [Descripción Relacionada],
 ips.Name [Homologo],
 rm.Name [Manual],
 IIF(ce.Status = 1, 'Activo', 'Inactivo') [Estado],
 1 as 'CANTIDAD',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 Contract.CUPSEntity ce
 INNER JOIN Contract.CupsSubgroup csg ON ce.CUPSSubGroupId = csg.Id
 INNER JOIN Contract.CupsGroup cg ON csg.CupsGroupId = cg.Id
 LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId
 LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
 LEFT JOIN Contract.CupsHomologation ch ON ce.Id = ch.CupsEntityId
 LEFT JOIN Contract.IPSService ips ON ch.IPSServiceId = ips.Id
 LEFT JOIN Contract.RateManualDetail rmd ON ips.Id = rmd.IPSServiceId
 LEFT JOIN Contract.RateManualDetailSurgical rmds ON ips.Id = rmds.IPSServiceId
 LEFT JOIN Contract.RateManual rm ON rmd.RateManualId = rm.Id OR rmds.RateManualId = rm.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida el catálogo de códigos CUPS configurados en el sistema, enriquecido con su clasificación jerárquica (grupo y subgrupo), estado activo/inactivo, financiación con recursos UPC, descripciones contractuales asociadas, homologación con servicios internos de la IPS y el manual tarifario vinculado. Está orientada a auditoría y verificación de la parametrización CUPS para reporting, identificando la compañía por nombre de base de datos y registrando la fecha de consulta en zona horaria de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la verificación de códigos CUPS con su clasificación (grupo/subgrupo), descripciones contractuales relacionadas, homologación a servicios IPS y manual tarifario asociado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada CUPSEntity debe tener un CupsSubgroup válido y este un CupsGroup válido (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'DISTINCT garantiza que no se devuelvan filas duplicadas por las múltiples relaciones (descripciones, homologación, tarifas).; ID_COMPANY siempre corresponde al nombre de la base de datos actual truncado a 9 caracteres.; CANTIDAD siempre es 1 (constante para conteos en el reporte).; ULT_ACTUAL siempre se calcula con la hora actual convertida a ''Pakistan Standard Time''.; Los CUPS sin homologación, descripción contractual o tarifa siguen apareciendo (LEFT JOIN) con esos campos en NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS; Grupo CUPS; Subgrupo CUPS; Homologación de servicios IPS; Manual tarifario; Financiación con recursos UPC; Descripción contractual; Servicios quirúrgicos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewVerficacionCUPS: Devuelve un registro distinto por cada combinación CUPS-grupo-subgrupo-descripción contractual-homologación IPS-manual tarifario, con estado, indicador de financiación UPC y marca de tiempo en zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ce.status = 1 → Se reporta ''Activo'' en columnas Estado CUP y Estado else Se reporta ''Inactivo''; si ce.financedresourceUPC = 1 → Se marca ''Si'' en ''Financiado con Recursos de la UPC'' else Se marca ''No''; si rmd.RateManualId = rm.Id OR rmds.RateManualId = rm.Id → El manual tarifario se obtiene por detalle estándar o quirúrgico, indistintamente', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CUPSEntity; Contract.CupsSubgroup; Contract.CupsGroup; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Contract.CupsHomologation; Contract.IPSService; Contract.RateManualDetail; Contract.RateManualDetailSurgical; Contract.RateManual', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewVerficacionCUPS';
GO
