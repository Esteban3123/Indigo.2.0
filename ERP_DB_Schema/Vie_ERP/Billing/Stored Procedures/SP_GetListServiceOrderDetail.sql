CREATE PROCEDURE [Billing].[SP_GetListServiceOrderDetail]
	@AdmissionNumber VARCHAR(50),
	@ids XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @tableids TABLE (id INT);

	-- Parsear los IDs desde el XML
	INSERT INTO @tableids (id)
	SELECT x.value('.', 'int')
	FROM @ids.nodes('/ArrayOfInt/int') AS A(x);

	SELECT 
		sod.Id,
		CONCAT(ips.Code, ' - ', ips.Name) AS CodeNameIpsService,
		CONCAT(c.Code, ' - ', c.Name) AS CodeNameCareGroup,
		sod.RecordType,
		sod.Packaging,
		sod.InvoicedQuantity,
		so.AdmissionNumber,
		so.Status
	FROM Billing.ServiceOrderDetail sod WITH (NOLOCK)
	INNER JOIN Billing.ServiceOrder so WITH (NOLOCK) ON so.Id = sod.ServiceOrderId
	INNER JOIN Contract.IPSService ips WITH (NOLOCK) ON ips.Id = sod.IPSServiceId
	INNER JOIN Contract.CareGroup c WITH (NOLOCK) ON c.Id = sod.CareGroupId
	WHERE 
		sod.RecordType = 1 AND
		sod.Packaging = 0 AND
		sod.InvoicedQuantity > 0 AND
		so.Status <> 3 AND
		so.AdmissionNumber = @AdmissionNumber AND
		sod.Id NOT IN (SELECT id FROM @tableids);
END