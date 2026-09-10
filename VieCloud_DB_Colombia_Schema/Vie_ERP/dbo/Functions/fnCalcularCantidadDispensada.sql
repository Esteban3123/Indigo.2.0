

CREATE FUNCTION [dbo].[fnCalcularCantidadDispensada] (@AdmissionNumber varchar(10), @CodProduct as varchar(20))
RETURNS Integer
AS
BEGIN
	Declare @Cantidad Integer = 0
	
	SELECT @Cantidad = SUM(PHD.Quantity - PHD.ReturnedQuantity)
	FROM Inventory.PharmaceuticalDispensing PH (nolock)
	JOIN Inventory.PharmaceuticalDispensingDetail PHD WITH(NOLOCK) ON PHD.PharmaceuticalDispensingId = PH.Id
	JOIN Inventory.InventoryProduct PR WITH(NOLOCK) ON PHD.ProductId = PR.Id
	JOIN Inventory.ATC a (nolock) on a.Id = pr.ATCId
	WHERE PH.AdmissionNumber = @AdmissionNumber AND a.Code = @CodProduct and PH.Status = 2
	GROUP BY PH.AdmissionNumber, PHD.ProductId

	RETURN @Cantidad
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la cantidad neta dispensada de un medicamento específico para un ingreso (hospitalización) determinado, restando las devoluciones a las entregas realizadas. Recibe como parámetros el número de ingreso del paciente y el código ATC del producto, y retorna un entero con el total neto entregado (cantidad dispensada menos cantidad devuelta). Consulta los documentos de despacho farmacéutico activos (estado 2), cruza el detalle de ítems dispensados con el catálogo de productos y la clasificación ATC para identificar el medicamento por su código. Se utiliza para conocer cuántas unidades de un medicamento han sido efectivamente suministradas a un paciente durante su estancia, considerando solo las dispensaciones vigentes y descontando las devoluciones registradas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnCalcularCantidadDispensada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnCalcularCantidadDispensada';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la cantidad neta dispensada (entregado menos devuelto) para una admisión y un código ATC dado, considerando solo dispensaciones en estado confirmado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalcularCantidadDispensada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una admisión identificable por número de admisión.; El código suministrado debe corresponder a un código ATC existente en el catálogo.; Las dispensaciones deben estar en estado 2 para ser contabilizadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalcularCantidadDispensada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran dispensaciones con Status = 2 (dispensación efectiva/confirmada).; La cantidad neta dispensada se calcula como Quantity - ReturnedQuantity, descontando devoluciones.; El producto se identifica por su código ATC, no por el Id del producto, agrupando todos los productos que compartan ese ATC.; Si no hay dispensaciones que cumplan los criterios, retorna 0 (valor inicial de @Cantidad).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalcularCantidadDispensada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Admisión de paciente; Clasificación ATC; Cantidad devuelta; Producto de inventario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalcularCantidadDispensada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensingDetail: Cuando existen dispensaciones con Status=2 para la admisión y código ATC, retorna SUM(Quantity - ReturnedQuantity); en caso contrario retorna 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalcularCantidadDispensada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalcularCantidadDispensada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalcularCantidadDispensada';
GO
