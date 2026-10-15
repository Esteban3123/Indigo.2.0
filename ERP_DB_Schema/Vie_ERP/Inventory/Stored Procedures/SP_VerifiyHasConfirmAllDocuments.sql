-- =============================================
-- Author:		Juan Carlos Bermudez Gutierrez
-- Create date: 24-06-2015
-- Description:	sp para consultar los documentos que esten sin confirmar en el modelo de inventario
-- =============================================
CREATE PROCEDURE [Inventory].[SP_VerifiyHasConfirmAllDocuments]
@MonthClosed As int,
@YearClosed as int
AS
BEGIN
	SET NOCOUNT ON;

	--Consultamos los ajustes de inventario que esten sin confirmar
	SELECT Code AS Code,
	       'Ajustes de Inventario' AS NameProcess
	FROM Inventory.InventoryAdjustment WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL
	--Consultamos los ajustes de inventario que esten sin confirmar
	SELECT Code AS Code,
	       'Cancelación de Solicitudes' AS NameProcess
	FROM Inventory.InventoryRequestDevolution WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL
	--Consultamos los Comprobantes de entrada que esten sin confirmar
	SELECT Code AS Code,
	       'Comprobante de Entrada' AS NameProcess
	FROM Inventory.EntranceVoucher WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL
	SELECT Code AS Code,
	       'Contrato' AS NameProcess
	FROM Inventory.InventoryContract WHERE Status = 1 And Month(ValidityDate) = @MonthClosed And Year(ValidityDate) = @YearClosed
	UNION ALL
	--Consultamos las devoluciones de compra que esten sin confirmar
	SELECT Code AS Code,
	       'Devolución de Compra' AS NameProcess
	FROM Inventory.EntranceVoucherDevolution WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL
	--Consultamos las devoluciones de dispensacion farmaceutica que esten sin confirmar
	SELECT Code AS Code,
	       'Devolucion de Dispensación Farmaceutica' AS NameProcess
	FROM Inventory.PharmaceuticalDispensingDevolution WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL
	--Consultamos las ordenes de compra que esten sin confirmar
	SELECT Code AS Code,
	       'Devolución de Orden de Compra' AS NameProcess
	FROM Inventory.PurchaseOrderDevolution WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL
	--Consultamos las devoluciones de orden de traslado que esten sin confirmar
	SELECT Code AS Code,
	       'Devolución de Orden de Traslado' AS NameProcess
	FROM Inventory.TransferOrderDevolution WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL
	--Consultamos las devoluciones de prestamo que esten sin confirmar
	SELECT Code AS Code,
	       'Devolucion de Préstamo' AS NameProcess
	FROM Inventory.LoanMerchandiseDevolution WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL
	--Consultamos las devoluciones de remision que esten sin confirmar
	SELECT Code AS Code,
	       'Devolución de Remisiones' AS NameProcess
	FROM Inventory.RemissionDevolution WHERE Status = 1 And Month(RemissionDate) = @MonthClosed And Year(RemissionDate) = @YearClosed	
	UNION ALL
	--Consultamos las dispensaciones farmaceuticas que esten sin confirmar
	SELECT Code AS Code,
	       'Dispensación Farmaceutica' AS NameProcess
	FROM Inventory.PharmaceuticalDispensing WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed	
	UNION ALL
	--Consultamos las ordenes de compra que esten sin confirmar
	SELECT Code AS Code,
	       'Orden de Compra' AS NameProcess
	FROM Inventory.PurchaseOrder WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL	
	--Consultamos las Ordenes de Traslado que esten sin confirmar
	SELECT Code AS Code,
	       'Orden de Traslado' AS NameProcess
	FROM Inventory.TransferOrder WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL
	--Consultamos los prestamos de mercancia que esten sin confirmar
	SELECT Code AS Code,
	       'Prestamo de Mercancia' AS NameProcess
	FROM Inventory.LoanMerchandise WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed
	UNION ALL	
	--Consultamos las remisiones de entrada que esten sin confirmar
	SELECT Code AS Code,
	       'Remisión de Entrada' AS NameProcess
	FROM Inventory.RemissionEntrance WHERE Status = 1 And Month(RemissionDate) = @MonthClosed And Year(RemissionDate) = @YearClosed
	UNION ALL
	--Consultamos las remisiones de inventario en consignacion que esten sin confirmar
	SELECT Code AS Code,
	       'Remisión de Inventario en Consignación' AS NameProcess
	FROM Inventory.ConsignmentInventoryRemission WHERE Status = 1 And Month(RemissionDate) = @MonthClosed And Year(RemissionDate) = @YearClosed
	UNION ALL
	--Consultamos las remisiones de salida que esten sin confirmar
	SELECT Code AS Code,
	       'Remisión de Salida' AS NameProcess
	FROM Inventory.RemissionOutput WHERE Status = 1 And Month(RemissionDate) = @MonthClosed And Year(RemissionDate) = @YearClosed
	UNION ALL	
	--Consultamos las solicitudes que esten sin confirmar
	SELECT Code AS Code,
	       'Solicitud' AS NameProcess
	FROM Inventory.InventoryRequest WHERE Status = 1 And Month(DocumentDate) = @MonthClosed And Year(DocumentDate) = @YearClosed	
	
END