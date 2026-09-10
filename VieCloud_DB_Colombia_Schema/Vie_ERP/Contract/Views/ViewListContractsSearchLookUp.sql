

CREATE VIEW [Contract].[ViewListContractsSearchLookUp]
AS

select c.Id, c.Code, cd.ContractName, cd.ContractNumber, c.ContractObject, c.Status, c.Code + ' - ' + cd.ContractName CodeContractName, 
ha.Id HealthAdministratorId, ha.Code + ' - ' + ha.Name HealthAdministratorCodeName, case c.Status when 1 then 'Activo' when 2 then 'Suspendido' when 3 then 'Terminado' end StatusName
from Contract.Contract c
inner join Contract.HealthAdministrator ha on ha.Id = c.HealthAdministratorId
inner join Contract.ContractDetail cd on cd.ContractId = c.Id
where cd.ValidRecord = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de contratos vigentes suscritos con administradoras de salud (EPS, aseguradoras, pagadores), combinando los datos principales del contrato, el detalle activo más reciente y la información de la entidad pagadora. Muestra el identificador, código, nombre, número y objeto del contrato, junto con el estado traducido a texto (Activo, Suspendido, Terminado) y el nombre completo de la administradora de salud. Se usa como catálogo de búsqueda rápida o selector (lookup) de contratos en formularios y reportes de facturación, glosas y autorizaciones, filtrando únicamente los registros de detalle válidos (vigentes).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListContractsSearchLookUp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListContractsSearchLookUp';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un listado de búsqueda/lookup de contratos vigentes con su administradora de salud asociada y la descripción legible de su estado.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsSearchLookUp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de relación entre el contrato, su administradora de salud y un detalle de contrato marcado como registro válido', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsSearchLookUp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen contratos cuyo detalle esté marcado como vigente (ValidRecord = 1); Todo contrato listado debe tener una administradora de salud asociada y al menos un detalle vigente (joins internos); El estado del contrato se traduce a un nombre legible únicamente para los valores 1, 2 y 3; otros valores quedarían sin descripción', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsSearchLookUp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato; Administradora de salud; Detalle de contrato; Estado de contrato (Activo/Suspendido/Terminado)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsSearchLookUp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.Contract: Devuelve un contrato por cada detalle con ValidRecord = 1, combinando datos del contrato, su administradora y el nombre del estado', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsSearchLookUp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status = 1 → Se etiqueta como ''Activo''; si Status = 2 → Se etiqueta como ''Suspendido''; si Status = 3 → Se etiqueta como ''Terminado''', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsSearchLookUp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.Contract; Contract.HealthAdministrator; Contract.ContractDetail', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsSearchLookUp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsSearchLookUp';
GO
