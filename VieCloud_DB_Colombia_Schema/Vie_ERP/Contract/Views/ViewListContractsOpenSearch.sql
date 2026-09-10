

CREATE VIEW [Contract].[ViewListContractsOpenSearch]
AS

select c.Id, c.Code, cd.ContractName, cd.ContractNumber, c.ContractObject, c.Status, c.Code + ' - ' + cd.ContractName CodeContractName, 
ha.Id HealthAdministratorId, ha.Code + ' - ' + ha.Name HealthAdministratorCodeName, case c.Status when 1 then 'Activo' when 2 then 'Suspendido' when 3 then 'Terminado' end StatusName
from Contract.Contract c
inner join Contract.HealthAdministrator ha on ha.Id = c.HealthAdministratorId
left join Contract.ContractDetail cd on cd.ContractId = c.Id
where cd.Id is null or cd.ValidRecord = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de contratos con entidades administradoras de salud (EPS, aseguradoras, pagadores) disponible para búsqueda y selección en la interfaz. Combina los datos principales del contrato (código, objeto, estado) con el detalle vigente del contrato (nombre, número) y la información de la administradora de salud asociada (código, nombre). Solo expone el registro de detalle válido o contratos sin detalle registrado, garantizando que no aparezcan versiones obsoletas. Incluye campos calculados como la descripción combinada del contrato (código + nombre) y la descripción combinada de la administradora, así como la traducción del estado numérico a texto legible (Activo, Suspendido, Terminado), facilitando la búsqueda y presentación de contratos en formularios, filtros y reportes comerciales o de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListContractsOpenSearch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListContractsOpenSearch';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un listado consolidado de contratos con su administradora de salud y detalle vigente para búsqueda/selección, traduciendo el estado a etiqueta legible.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsOpenSearch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todo contrato debe tener una administradora de salud asociada existente (INNER JOIN con HealthAdministrator).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsOpenSearch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el detalle de contrato cuando es el registro vigente (ValidRecord = 1).; Un contrato sin detalle igualmente aparece en el listado.; Los estados manejados son exclusivamente Activo (1), Suspendido (2) y Terminado (3).; Se construyen identificadores compuestos legibles: ''Code - ContractName'' y ''Code - Name'' de la administradora.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsOpenSearch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato; Administradora de salud; Detalle de contrato; Estado de contrato (Activo/Suspendido/Terminado); Registro vigente', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsOpenSearch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve contratos junto con su administradora y, si existe, el detalle marcado como vigente (ValidRecord = 1); si no hay detalle se retorna igualmente el contrato.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsOpenSearch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status = 1 → Se etiqueta como ''Activo''; si Status = 2 → Se etiqueta como ''Suspendido''; si Status = 3 → Se etiqueta como ''Terminado'' else Sin etiqueta (NULL); si ContractDetail.Id IS NULL OR ContractDetail.ValidRecord = 1 → Se incluye la fila en el resultado else Se excluyen detalles no vigentes', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsOpenSearch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.Contract; Contract.HealthAdministrator; Contract.ContractDetail', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsOpenSearch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListContractsOpenSearch';
GO
