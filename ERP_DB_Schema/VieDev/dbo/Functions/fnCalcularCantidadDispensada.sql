

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