
CREATE PROCEDURE [MedicalHistory].[ListFinishedProductsDevolution]
@Paciente varchar(25),
@Ingreso char(10),
@Centro  char(10),
@UnidadFuncional char(10)
AS
BEGIN
	SET NOCOUNT ON;

	Select distinct CAST(0 as bit) as Seleccion,  cum.Id as IdDetailPhysicalCUM, p.MainDrugCode, p.FullProductName, cum.BatchCode, 1 as CantidadDevolver,CONVERT(VARCHAR(50),cum.GroupingCodeDose)
	, CAST('' as varchar) as IdHCMOANULB , CAST('' as varchar(100)) as DevolutionObservations, p.Origin
	From MedicalHistory.ProductSusceptibleMixingStation p 
		inner join MedicalHistory.PharmaDose d on p.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation and AppliedDose = 0 --and p.MainDrugCode = d.ProductCode 
		inner join MedicalHistory.DetailPhysicalCUM cum ON cum.GroupingCodeDose = D.GroupingCodeDose  and UsedQuantity  < cum.DispensedQuantity  
		inner join Inventory.InventoryProduct pro on  pro.Id = cum.ProductId 
		inner join Inventory.ATC atc on  atc.Id = pro.ATCId  
		inner join  HCFISIPRO fis on fis.ID = cum.IDHCFISIPRO  AND IPCODPACI = @Paciente 
	Where 
		fis.IPCODPACI = @Paciente  and fis.NUMINGRES = @Ingreso and p.CenterAttentionCode = @Centro 
		and not exists (select IdDetailPhysicalCUM  from HCDEVMEDD a where a.IdDetailPhysicalCUM = cum.Id and a.PROESTADO IN ('1','2'))  
		---and FunctionalUnitCode = '11011      ' 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los productos o medicamentos susceptibles de mezcla que ya fueron dispensados a un paciente en un ingreso determinado y que tienen cantidades pendientes de devolución, es decir, donde la cantidad utilizada es menor a la cantidad dispensada. Combina la información de la estación de mezclas, las dosis farmacéuticas aplicadas, el detalle físico de control de medicamentos (CUM) y el catálogo de inventario (ATC/productos) para identificar exactamente qué lotes y agrupaciones de dosis están disponibles para devolver. Filtra por cédula del paciente (identificación, documento), número de ingreso, centro de atención y excluye los ítems que ya tienen una devolución en curso o completada. Se usa en el proceso de devolución de medicamentos preparados en farmacia cuando el tratamiento del paciente hospitalizado finaliza o cambia.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'PROCEDURE', @level1name = N'ListFinishedProductsDevolution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'PROCEDURE', @level1name = N'ListFinishedProductsDevolution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos/insumos dispensados a un paciente en un ingreso y centro determinados que aún tienen saldo no usado y son candidatos a devolución, excluyendo los ya gestionados en devolución.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProductsDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un ingreso registrado en HCFISIPRO con coincidencia de IPCODPACI y NUMINGRES.; Debe existir relación entre productos susceptibles de mezcla, dosis farmacéuticas y detalle físico CUM por GroupingCodeDose.; El centro de atención del producto susceptible debe coincidir con el centro indicado.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProductsDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna ítems cuya dosis ya fue aplicada (AppliedDose=0 obligatorio).; Nunca retorna ítems sin saldo pendiente (UsedQuantity debe ser menor que DispensedQuantity).; Nunca retorna ítems con devolución previa en estados ''1'' o ''2''.; Los resultados están restringidos al paciente, ingreso y centro de atención solicitados.; Las filas se devuelven con Seleccion en falso y CantidadDevolver inicial en 1.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProductsDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de medicamentos; Dosis farmacéutica no aplicada; Producto susceptible de mezcla en estación; Lote (BatchCode); Clasificación ATC; Historia clínica del paciente; Ingreso hospitalario; Centro de atención; Control físico CUM; Saldo dispensado vs usado', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProductsDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas con Seleccion=0 (bit), CantidadDevolver=1 e IdHCMOANULB y DevolutionObservations vacíos como valores por defecto para inicializar la grilla de devolución.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProductsDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PharmaDose.AppliedDose = 0 → Solo se consideran dosis no aplicadas como elegibles para devolución.; si DetailPhysicalCUM.UsedQuantity < DispensedQuantity → Solo se incluyen ítems con saldo (cantidad dispensada mayor a la usada).; si NOT EXISTS en HCDEVMEDD con PROESTADO IN (''1'',''2'') para el mismo IdDetailPhysicalCUM → Se excluyen ítems que ya tienen una devolución en estado 1 o 2 registrada.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProductsDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalHistory.ProductSusceptibleMixingStation; MedicalHistory.PharmaDose; MedicalHistory.DetailPhysicalCUM; Inventory.InventoryProduct; Inventory.ATC; HCFISIPRO; HCDEVMEDD', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProductsDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProductsDevolution';
-- GO
