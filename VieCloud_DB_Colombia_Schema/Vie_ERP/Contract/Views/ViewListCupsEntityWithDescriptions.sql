

CREATE VIEW [Contract].[ViewListCupsEntityWithDescriptions]
AS

select cecd.Id CUPSEntityContractDescriptionId, cd.Id ContractDescriptionId, ce.Id CUPSEntityId,
ce.Code CUPSEntityCode, ce.Description CUPSEntityName, ce.Code + ' - ' + ce.Description CUPSEntityCodeName,
cd.Code ContractDescriptionCode, cd.Name ContractDescriptionName, cd.Code + ' - ' + cd.Name ContractDescriptionCodeName
from Contract.CUPSEntity ce with(nolock)
inner join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.CUPSEntityId = ce.Id
inner join Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
where cecd.IsDelete = 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de servicios o procedimientos CUPS vigentes junto con las descripciones de contrato asociadas. Combina el catálogo de procedimientos (CUPS) con los conceptos o grupos de facturación definidos en los contratos, mostrando para cada servicio su código, nombre y la descripción contractual bajo la que se factura. Útil para consultas de tarifación, parametrización de contratos y validación de qué conceptos de cobro aplican a cada procedimiento o examen. Solo incluye registros activos (no eliminados).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListCupsEntityWithDescriptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListCupsEntityWithDescriptions';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de relaciones vigentes entre códigos CUPS de entidad y descripciones de contrato, enriquecido con códigos y nombres concatenados para visualización.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsEntityWithDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir relaciones en CUPSEntityContractDescriptions con CUPSEntity y ContractDescriptions referenciados (INNER JOIN).; Las relaciones deben estar activas (no marcadas como eliminadas).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsEntityWithDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye registros lógicamente eliminados al filtrar IsDelete = 0.; Solo se incluyen pares CUPS/descripción que existan en ambos catálogos maestros (INNER JOIN).; Los campos compuestos siempre se construyen con el formato ''Código - Nombre/Descripción''.; La consulta usa NOLOCK en todas las tablas, permitiendo lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsEntityWithDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS (Clasificación Única de Procedimientos en Salud); Descripción de contrato; Entidad CUPS; Eliminación lógica', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsEntityWithDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve solo asociaciones CUPS-descripción de contrato cuya marca de eliminación es 0 (IsDelete = 0).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsEntityWithDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsEntityWithDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsEntityWithDescriptions';
GO
