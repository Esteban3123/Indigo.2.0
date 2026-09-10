

CREATE VIEW [Inventory].[ViewAdmissionProductDetail]
AS
select pd.Id as HeaderId, pdd.id as DetailId , pd.AdmissionNumber,pdd.FunctionalUnitId, pdd.ProductId, 1 as DispensingType, pd.Code , pd.DocumentDate ,pd.CreationUser,pdd.Quantity,  RTRIM ( tp.Nit) +' - '+ RTRIM ( tp.Name) as  HealthProfessionalNitName  
from Inventory.PharmaceuticalDispensing pd with (nolock)
inner join Inventory.PharmaceuticalDispensingDetail pdd with (nolock) on pdd.PharmaceuticalDispensingId = pd.id 
inner join Common.ThirdParty tp  with (nolock) on pdd.OrderedHealthProfessionalThirdPartyId = tp.id
where pd.Status = 2
union all

select pdd.Id as HeaderId,pddd.id as DetailId , pd.AdmissionNumber,pddet.FunctionalUnitId, pddet.ProductId, 2 as DispensingType ,pdd.Code , pdd.DocumentDate ,pdd.CreationUser,pddd.Quantity, RTRIM ( tp.Nit) +' - '+ RTRIM ( tp.Name) as  HealthProfessionalNitName
from Inventory.PharmaceuticalDispensingDevolution pdd with (nolock) 
inner join Inventory.PharmaceuticalDispensingDevolutionDetail pddd  with (nolock) on pddd.PharmaceuticalDispensingDevolutionId = pdd.id 
inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs with (nolock) on pddd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.id 
inner join Inventory.PharmaceuticalDispensingDetail pddet with (nolock) on pddbs.PharmaceuticalDispensingDetailId = pddet.Id 
inner join Inventory.PharmaceuticalDispensing pd with (nolock) on pddet.PharmaceuticalDispensingId = pd.id
inner join Common.ThirdParty tp with (nolock) on pddet.OrderedHealthProfessionalThirdPartyId = tp.id
where pdd.Status = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de productos (medicamentos) dispensados y devueltos en el contexto de ingresos hospitalarios. Combina dos fuentes: las dispensaciones farmacéuticas confirmadas (tipo 1) y las devoluciones de dispensación confirmadas (tipo 2), identificando en ambos casos el número de ingreso del paciente, la unidad funcional, el producto, la cantidad y el profesional de salud que ordenó la fórmula (con su NIT y nombre). Permite rastrear qué medicamentos fueron entregados o retornados por admisión, qué profesional los ordenó y en qué fecha, siendo útil para reportería de consumo farmacéutico por paciente, conciliación de inventarios y auditoría de dispensación en hospitalización.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewAdmissionProductDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewAdmissionProductDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los detalles de productos dispensados y devueltos por dispensación farmacéutica asociados a una admisión, identificando cada movimiento por tipo (dispensación o devolución) y el profesional ordenante.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProductDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las dispensaciones (PharmaceuticalDispensing) deben tener Status = 2 para ser incluidas.; Las devoluciones (PharmaceuticalDispensingDevolution) deben tener Status = 2 para ser incluidas.; Cada detalle de dispensación debe tener un tercero profesional de la salud ordenante (OrderedHealthProfessionalThirdPartyId) válido en Common.ThirdParty.; Las devoluciones deben estar trazadas a un detalle de dispensación a través de la tabla de lotes/seriales (PharmaceuticalDispensingDetailBatchSerial).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProductDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen movimientos cuyo documento (dispensación o devolución) tenga Status = 2.; DispensingType siempre vale 1 para dispensaciones y 2 para devoluciones.; El número de admisión y el producto/unidad funcional de las devoluciones siempre provienen de la dispensación original, no del documento de devolución.; HealthProfessionalNitName siempre se forma como ''Nit - Nombre'' del tercero ordenante, con RTRIM aplicado a ambos componentes.; Las devoluciones siempre quedan trazadas al detalle original a través de la tabla de lotes/seriales.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProductDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Devolución de dispensación; Admisión del paciente; Profesional de la salud ordenante; Unidad funcional; Producto/Medicamento; Lote y serial de inventario; Tercero (NIT)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProductDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ViewAdmissionProductDetail: Cuando pd.Status = 2 en PharmaceuticalDispensing, se retorna la línea con DispensingType = 1 (dispensación).; [RETURN_RESULT] Inventory.ViewAdmissionProductDetail: Cuando pdd.Status = 2 en PharmaceuticalDispensingDevolution, se retorna la línea con DispensingType = 2 (devolución), enlazando devolución → batch/serial → detalle de dispensación original.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProductDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pd.Status = 2 (rama dispensación) → Marca DispensingType = 1 y toma datos directamente de PharmaceuticalDispensing/Detail. else No se incluye la dispensación.; si pdd.Status = 2 (rama devolución) → Marca DispensingType = 2 y resuelve FunctionalUnitId/ProductId/profesional desde el detalle de la dispensación original vinculada vía batch/serial. else No se incluye la devolución.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProductDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensingDevolution; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProductDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProductDetail';
GO
