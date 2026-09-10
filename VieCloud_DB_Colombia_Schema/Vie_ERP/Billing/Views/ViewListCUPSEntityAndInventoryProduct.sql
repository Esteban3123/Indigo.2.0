CREATE VIEW [Billing].[ViewListCUPSEntityAndInventoryProduct]
AS

select ROW_NUMBER() OVER (ORDER BY temp.Id) Row, temp.Id, temp.Code, temp.Description, temp.Identification, temp.IdentificationDescription
from (
	select ce.Id, ce.Code, ce.Description, 0 Identification, 'CUPS' IdentificationDescription
	from Contract.CUPSEntity ce
	where ce.Status = 1
	union all
	select ip.Id, ip.Code, ip.Name Description, 1 Identification, 'Medicamento' IdentificationDescription
	from Inventory.InventoryProduct ip
	where ip.Status = 1
) temp
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista unificada de servicios facturables que combina dos catálogos: los procedimientos y exámenes CUPS (Clasificación Única de Procedimientos en Salud) vigentes, y los medicamentos e insumos del inventario activos. Cada registro indica si el ítem es un servicio CUPS o un medicamento mediante el campo de identificación, permitiendo buscar en un solo lugar cualquier concepto facturable, ya sea un procedimiento clínico o un producto farmacéutico. Se utiliza en el módulo de facturación para seleccionar y codificar los servicios o medicamentos que se cargan a una cuenta de paciente, contrato o aseguradora.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListCUPSEntityAndInventoryProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListCUPSEntityAndInventoryProduct';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los servicios CUPS activos y los productos de inventario (medicamentos) activos, etiquetando el origen de cada registro para su uso unificado en procesos de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListCUPSEntityAndInventoryProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas Contract.CUPSEntity e Inventory.InventoryProduct deben tener la columna Status donde el valor 1 representa el estado activo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListCUPSEntityAndInventoryProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros activos (Status = 1) tanto de CUPSEntity como de InventoryProduct.; Cada fila se etiqueta con un discriminador: Identification = 0 / ''CUPS'' para procedimientos y Identification = 1 / ''Medicamento'' para productos de inventario.; El conjunto resultante es la unión simple (UNION ALL) de ambos catálogos, sin deduplicar entre fuentes.; Se asigna un número de fila secuencial (ROW_NUMBER) ordenado por Id para identificar cada registro dentro del listado consolidado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListCUPSEntityAndInventoryProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS (Clasificación Única de Procedimientos en Salud); Medicamento; Producto de inventario; Catálogo de servicios facturables', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListCUPSEntityAndInventoryProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListCUPSEntityAndInventoryProduct: Devuelve los CUPS activos (Contract.CUPSEntity con Status = 1) marcados con Identification=0/''CUPS'' y los productos de inventario activos (Inventory.InventoryProduct con Status = 1) marcados con Identification=1/''Medicamento'', unidos vía UNION ALL.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListCUPSEntityAndInventoryProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CUPSEntity; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListCUPSEntityAndInventoryProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListCUPSEntityAndInventoryProduct';
GO
