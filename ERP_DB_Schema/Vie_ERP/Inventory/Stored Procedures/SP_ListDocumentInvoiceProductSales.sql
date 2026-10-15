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