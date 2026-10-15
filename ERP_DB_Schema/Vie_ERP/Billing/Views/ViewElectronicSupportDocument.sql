




CREATE VIEW [Billing].[ViewElectronicSupportDocument]
AS
(	SELECT	esd.Id,
			'ElectronicSupportDocument' DocumentType,
			esd.EntityId,
			esd.EntityName,
			null Code,
			esd.DocumentNumber,
			esd.SupplierThirdPartyId,
			esd.StatusElectronic,
			null StatusNote,
			esd.DocumentDate,
			esd.ShippingDate,
			esd.OperativeUnitId
	FROM Billing.ElectronicSupportDocument esd
	WHERE esd.Status = 1
)	
UNION ALL
(
	SELECT	edan.Id,
			'ElectronicSupportDocumentAdjustmentNote' DocumentType,
			edan.EntityId,
			edan.EntityName,
			edan.Code,
			esd.DocumentNumber,
			esd.SupplierThirdPartyId,
			edan.StatusElectronic,
			edan.[Status] StatusNote,
			edan.DocumentDate,
			edan.ShippingDate,
			edan.OperativeUnitId
	FROM Billing.ElectronicSupportDocumentAdjustmentNote edan
	JOIN Billing.ElectronicSupportDocument esd on edan.ElectronicSupportDocumentId = esd.Id
	WHERE edan.Status = 2
)