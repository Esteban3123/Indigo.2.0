-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-04-23
-- Description:	Generación reporte lista de facturas de productos
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ListDocumentInvoiceProductSales]
	-- Add the parameters for the stored procedure here
	@DateStart AS DATETIME,
	@DateEnd AS DATETIME,
	@Status AS TINYINT,
	@InvoiceNumberStart AS VARCHAR(15),
	@InvoiceNumberEnd AS VARCHAR(15),
	@ThirdPartyId AS INT,
	@BranchOfficeId AS INT,
	@User AS VARCHAR(20)
AS
BEGIN
	SELECT 
		dips.Code
		, i.InvoiceNumber
		, CASE dips.Status WHEN 2 THEN 'Facturado' WHEN 3 THEN 'Anulado' ELSE 'Registrado' END Status
		, dips.DocumentDate
		, dips.Description
		, tp.Id ThirdPartyId
		, tp.Nit ThirdPartyNit
		, tp.Name ThirdPartyName
		, bo.Id SucursalId
		, bo.Name SucursalName
		, city.Name CityName
		, dips.Value Subtotal
		, dips.ValueTax
		, dips.TotalValue	
		, dips.CreationUser
		, dips.ConfirmationUser
	FROM Inventory.DocumentInvoiceProductSales dips
	JOIN Billing.Invoice i ON dips.InvoiceId = i.Id
	JOIN Common.ThirdParty tp ON dips.ThirdPartyId = tp.Id
	LEFT JOIN Payroll.BranchOffice bo ON dips.BranchOfficeId = bo.Id
	LEFT JOIN Common.City city ON bo.CityId = city.Id	
	WHERE dips.DocumentDate BETWEEN @DateStart AND @DateEnd
		AND (ISNULL(@Status, 0) = 0 OR dips.Status = ISNULL(@Status, 0))
		AND (i.InvoiceNumber >= ISNULL(@InvoiceNumberStart,'0') AND i.InvoiceNumber <= ISNULL(@InvoiceNumberEnd, 'z'))
		AND (ISNULL(@ThirdPartyId, 0) = 0 OR tp.Id = ISNULL(@ThirdPartyId, 0))
		AND (ISNULL(@BranchOfficeId, 0) = 0 OR ISNULL(bo.Id, 0) = ISNULL(@BranchOfficeId, 0))
		AND (ISNULL(@User, 0) = 0 OR ISNULL(dips.CreationUser, 0) = ISNULL(@User, 0))
		AND (ISNULL(@User, 0) = 0 OR ISNULL(dips.ConfirmationUser, 0) = ISNULL(@User, 0))
	ORDER BY dips.Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el listado de facturas de venta de productos de inventario (farmacia, insumos, artículos comerciales) para un rango de fechas, estado del documento, rango de número de factura, tercero comprador, sucursal y usuario. Combina los documentos de venta de inventario con la factura de cobro asociada, los datos del tercero comprador (NIT y nombre), la sucursal y su ciudad, mostrando valores de subtotal, impuestos y total. Permite filtrar y auditar las ventas de productos facturadas, anuladas o registradas, siendo útil para conciliación de ventas, reportes de facturación de farmacia o inventario y seguimiento por usuario o sede.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ListDocumentInvoiceProductSales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ListDocumentInvoiceProductSales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un listado de facturas de venta de productos en un rango de fechas, con filtros opcionales por estado, rango de número de factura, tercero, sucursal y usuario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListDocumentInvoiceProductSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango @DateStart–@DateEnd debe estar definido (no es opcional, se aplica siempre en el WHERE).; Las facturas listadas deben tener Invoice asociado (JOIN obligatorio con Billing.Invoice) y ThirdParty asociado (JOIN obligatorio con Common.ThirdParty).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListDocumentInvoiceProductSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de número de factura usa límites por defecto ''0'' y ''z'' cuando los parámetros vienen nulos, comparándose como cadena (orden lexicográfico).; La sucursal y la ciudad son opcionales en el resultado (LEFT JOIN); pueden venir nulas si la factura no tiene sucursal asociada.; El filtro por usuario aplica simultáneamente a CreationUser y ConfirmationUser; sólo se listan registros donde el mismo usuario figura en ambos roles.; Siempre se filtra por DocumentDate dentro del rango proporcionado; no hay rama que omita esta restricción.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListDocumentInvoiceProductSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura de venta de productos; Estado de factura (Registrado/Facturado/Anulado); Tercero (NIT, nombre); Sucursal; Ciudad; Subtotal, Impuestos y Valor total; Usuario creador y confirmador', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListDocumentInvoiceProductSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.DocumentInvoiceProductSales: Devuelve filas ordenadas por Code, traduciendo el campo Status numérico a etiqueta: 2=''Facturado'', 3=''Anulado'', cualquier otro valor=''Registrado''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListDocumentInvoiceProductSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si dips.Status = 2 → Se etiqueta como ''Facturado'' else Si Status = 3 → ''Anulado''; en cualquier otro caso → ''Registrado''; si ISNULL(@Status,0) = 0 → No se filtra por estado (se incluyen todos) else Se filtra dips.Status = @Status; si ISNULL(@ThirdPartyId,0) = 0 → No se filtra por tercero else Se restringe a tp.Id = @ThirdPartyId; si ISNULL(@BranchOfficeId,0) = 0 → No se filtra por sucursal else Se restringe a bo.Id = @BranchOfficeId; si ISNULL(@User,0) = 0 → No se filtra por usuario else Se exige que dips.CreationUser = @User Y dips.ConfirmationUser = @User (ambos deben coincidir con el mismo usuario)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListDocumentInvoiceProductSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.DocumentInvoiceProductSales; Billing.Invoice; Common.ThirdParty; Payroll.BranchOffice; Common.City', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListDocumentInvoiceProductSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListDocumentInvoiceProductSales';
-- GO
