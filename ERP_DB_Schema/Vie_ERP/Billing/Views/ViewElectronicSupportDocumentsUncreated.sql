CREATE VIEW Billing.ViewElectronicSupportDocumentsUncreated 
AS 
SELECT 
	esd.Id,
	esd.Code,
	esd.DocumentDate,
	esd.RadicationDate,
	esd.DueDate, --Fecha de expiración
	esd.SupplierThirdPartyId,
	esd.CustomerThirdPartyId,
	esd.BillingAuthorizationId,
	esd.Description,
	esd.SubTotalValue,
	esd.TaxValue,
	esd.TotalValue,
	esd.Status,
	esd.CUDS,
	esd.EntityCode,
	esd.EntityName,
	esd.EntityId,
	esd.DocumentNumber,
	esd.OperativeUnitId,
	esd.Retry,
	esd.StatusElectronic,
	esd.ShippingDate, --Fecha de envío a la DIAN 
	esd.ZipKey,
	esd.Year,
	esd.Consecutive,
	esd.FilePath,
	esd.QR
FROM Billing.ElectronicSupportDocument esd



