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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si existen documentos de inventario pendientes de confirmar (estado sin confirmar) para un mes y año específicos antes de realizar el cierre del período contable-inventario. Recorre todos los tipos de documentos del módulo de inventario: ajustes, comprobantes de entrada, órdenes de compra, órdenes de traslado, devoluciones (de compra, dispensación farmacéutica, préstamo, remisión, traslado), dispensaciones farmacéuticas, solicitudes, remisiones (entrada, salida, consignación), préstamos de mercancía y contratos. Retorna un listado consolidado con el código de cada documento pendiente y el nombre del proceso al que pertenece, permitiendo al usuario o al sistema identificar qué documentos deben confirmarse o anularse antes de cerrar el mes de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_VerifiyHasConfirmAllDocuments';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_VerifiyHasConfirmAllDocuments';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista todos los documentos de inventario aún sin confirmar (Status=1) cuyo mes y año de fecha de documento/vigencia/remisión coinciden con el periodo indicado, para validar el cierre mensual de inventario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_VerifiyHasConfirmAllDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar un mes y año válidos correspondientes al periodo de cierre a verificar.; Las tablas de documentos de inventario deben usar Status=1 para representar el estado ''sin confirmar''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_VerifiyHasConfirmAllDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen documentos con Status = 1 (sin confirmar); cualquier otro estado se excluye del resultado.; El filtro temporal se aplica sobre DocumentDate en la mayoría de tablas, sobre RemissionDate en remisiones (RemissionDevolution, RemissionEntrance, ConsignmentInventoryRemission, RemissionOutput) y sobre ValidityDate en InventoryContract.; Cada fila resultante identifica el origen funcional mediante una etiqueta literal en NameProcess (Ajustes de Inventario, Comprobante de Entrada, Contrato, Devolución de Compra, etc.).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_VerifiyHasConfirmAllDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cierre mensual de inventario; Documentos sin confirmar; Ajustes de inventario; Comprobantes de entrada; Contratos de inventario; Devoluciones de compra; Dispensación farmacéutica; Órdenes de compra; Órdenes de traslado; Préstamos de mercancía; Remisiones (entrada, salida, consignación); Solicitudes de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_VerifiyHasConfirmAllDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único resultset con columnas Code y NameProcess, uniendo (UNION ALL) los documentos pendientes de confirmar de 18 fuentes distintas del módulo Inventory para el mes y año indicados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_VerifiyHasConfirmAllDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryAdjustment; Inventory.InventoryRequestDevolution; Inventory.EntranceVoucher; Inventory.InventoryContract; Inventory.EntranceVoucherDevolution; Inventory.PharmaceuticalDispensingDevolution; Inventory.PurchaseOrderDevolution; Inventory.TransferOrderDevolution; Inventory.LoanMerchandiseDevolution; Inventory.RemissionDevolution; Inventory.PharmaceuticalDispensing; Inventory.PurchaseOrder; Inventory.TransferOrder; Inventory.LoanMerchandise; Inventory.RemissionEntrance; Inventory.ConsignmentInventoryRemission; Inventory.RemissionOutput; Inventory.InventoryRequest', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_VerifiyHasConfirmAllDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_VerifiyHasConfirmAllDocuments';
-- GO
