

CREATE VIEW [Contract].[VProcedureMarketingUnitCUPS]
as
(
select  ROW_NUMBER() OVER(ORDER BY pc.ProceduresTemplateId ASC) as Row,
pc.ProceduresTemplateId
,cups.Id as CUPSId
,cups.Code as CUPSCode
,cups.Description as CUPSDescription
,csub.Id as SubGroupId
,csub.Code as SubGroupCode
,csub.Name as SubGroupName
,cgro.Id as GroupId
,cgro.Code as GroupCode
,cgro.Name as GroupName
from [Contract].ProcedureCups pc with (nolock) 
inner join [Contract].CUPSEntity cups  with (nolock)  on pc.CupsId = cups.Id
inner join [Contract].CupsSubgroup csub  with (nolock) on cups.CUPSSubGroupId = csub.Id
inner join [Contract].CupsGroup cgro  with (nolock) on csub.CupsGroupId = cgro.Id
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los procedimientos y servicios de salud (CUPS) asociados a plantillas de contrato, mostrando para cada procedimiento su código y descripción CUPS junto con su subgrupo y grupo de clasificación contractual. Integra la tabla de procedimientos contratados con el catálogo maestro de CUPS y su jerarquía de clasificación (grupo y subgrupo), permitiendo identificar qué servicios —como laboratorios, imágenes diagnósticas o procedimientos quirúrgicos— están incluidos en cada plantilla de contrato. Es útil para reportería contractual, tarifación y negociación de servicios de salud con aseguradores o pagadores.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'VProcedureMarketingUnitCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'VProcedureMarketingUnitCUPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los procedimientos CUPS asociados a plantillas de contrato junto con su clasificación jerárquica (subgrupo y grupo) para consulta unificada.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'VProcedureMarketingUnitCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen relaciones válidas entre ProcedureCups y CUPSEntity por CupsId; Cada CUPS debe tener un subgrupo asignado (CUPSSubGroupId); Cada subgrupo debe pertenecer a un grupo (CupsGroupId)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'VProcedureMarketingUnitCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa un CUPS dentro de una plantilla de procedimientos con su jerarquía completa (Grupo→Subgrupo→CUPS); Se asigna un número de fila secuencial ordenado por ProceduresTemplateId ascendente; Lectura con NOLOCK: puede retornar datos sin commit (lecturas sucias); Un mismo CUPS puede aparecer múltiples veces si está asociado a varias plantillas', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'VProcedureMarketingUnitCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS (Clasificación Única de Procedimientos en Salud); Plantilla de procedimientos contractuales; Grupo y subgrupo de procedimientos; Procedimientos contratados', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'VProcedureMarketingUnitCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente CUPS que tengan vínculo completo CUPS→Subgrupo→Grupo (INNER JOIN); excluye CUPS sin clasificación jerárquica completa', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'VProcedureMarketingUnitCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.ProcedureCups; Contract.CUPSEntity; Contract.CupsSubgroup; Contract.CupsGroup', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'VProcedureMarketingUnitCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'VProcedureMarketingUnitCUPS';
GO
