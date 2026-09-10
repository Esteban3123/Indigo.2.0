

CREATE VIEW  [MixingStation].[ViewComponentsNPT]
AS
SELECT concat(HAD.ID , '' ) Id
, hpc.id IdHCPARNUTC
,atc.Id AtcId
,had.CODPRODUC Code
,atc.Name
,(SELECT TOP 1 DCI.Name from Inventory.DCI DCI
join Inventory.ATC ATC WITH(NOLOCK) ON ATC.DCIId = DCI.Id
WHERE had.CODPRODUC = ATC.Code) AbbreviationName
,had.CODUNIMED MeasurementUnitId
,HAD.TIPOVOLUMEN UnitType
,HAD.INDICATIONS
FROM HCPARNUTC HPC
JOIN HCPARNUTD HAD WITH(NOLOCK) ON HAD.HCPARNUTCID = HPC.ID
JOIN Inventory.ATC ATC WITH(NOLOCK) on ATC.Code = had.CODPRODUC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los componentes o insumos disponibles para la preparación de Nutrición Parenteral Total (NPT) en la estación de mezclas. Integra el catálogo de parámetros nutricionales (HCPARNUTC y HCPARNUTD) con el catálogo de medicamentos e insumos (ATC) y las Denominaciones Comunes Internacionales (DCI), permitiendo identificar cada componente por su código de producto, nombre comercial, principio activo genérico (abreviación DCI), unidad de medida y tipo de volumen. Se utiliza para alimentar el módulo de preparación de fórmulas parenterales, facilitando la selección y validación de nutrientes, electrolitos y aditivos con sus indicaciones clínicas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewComponentsNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewComponentsNPT';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los componentes (detalle) de fórmulas de Nutrición Parenteral Total enriquecidos con datos del catálogo ATC y la DCI asociada para su uso en la estación de mezclas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewComponentsNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros relacionados entre HCPARNUTC (cabecera) y HCPARNUTD (detalle) por HCPARNUTCID.; El código de producto (CODPRODUC) del detalle debe existir en Inventory.ATC para que el componente sea visible.; Para obtener AbbreviationName debe existir una relación ATC→DCI vigente en Inventory.ATC e Inventory.DCI.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewComponentsNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen componentes cuyo código de producto exista en el catálogo Inventory.ATC (JOIN inner sobre ATC.Code = HAD.CODPRODUC).; Cada componente está asociado a una cabecera de fórmula NPT en HCPARNUTC mediante HAD.HCPARNUTCID.; El nombre abreviado se obtiene del primer DCI vinculado al ATC del producto (TOP 1 sin ORDER BY: resultado no determinístico cuando hay múltiples DCIs).; El Id del componente se devuelve como cadena (CONCAT con '''').', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewComponentsNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición Parenteral Total (NPT); Componentes de mezcla; Clasificación ATC; DCI (Denominación Común Internacional); Unidad de medida; Tipo de volumen; Indicaciones', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewComponentsNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewComponentsNPT: Devuelve una fila por cada detalle HCPARNUTD cuyo CODPRODUC tiene correspondencia en Inventory.ATC, incluyendo Id de cabecera (HCPARNUTC), Id ATC, código, nombre ATC, nombre abreviado tomado del primer DCI vinculado, unidad de medida, tipo de volumen e indicaciones.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewComponentsNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPARNUTC; dbo.HCPARNUTD; Inventory.ATC; Inventory.DCI', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewComponentsNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewComponentsNPT';
GO
