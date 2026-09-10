

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista unificada de documentos soporte electrónicos de facturación activos, que consolida en un único resultado tanto los documentos soporte originales como sus notas de ajuste (notas débito y crédito). Combina los registros vigentes de ElectronicSupportDocument con las notas de ajuste activas de ElectronicSupportDocumentAdjustmentNote, indicando en cada fila el tipo de documento (documento soporte original o nota de ajuste), su número, proveedor, estado de transmisión electrónica a la DIAN, fecha del documento, fecha de envío y unidad operativa. Sirve como fuente centralizada para consultas y reportería de facturación electrónica entre la entidad y sus proveedores, facilitando el seguimiento del ciclo de vida del documento soporte electrónico y sus correcciones o anulaciones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicSupportDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicSupportDocument';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en un solo conjunto los documentos soporte electrónicos vigentes y sus notas de ajuste activas, exponiéndolos con un tipo discriminador para consultas consolidadas de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Toda nota de ajuste debe estar asociada a un documento soporte electrónico existente (JOIN por ElectronicSupportDocumentId).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila se etiqueta con DocumentType = ''ElectronicSupportDocument'' o ''ElectronicSupportDocumentAdjustmentNote'' para distinguir el origen.; Para los documentos soporte, los campos Code y StatusNote siempre se exponen como NULL.; Las notas de ajuste heredan DocumentNumber y SupplierThirdPartyId del documento soporte al que están vinculadas vía ElectronicSupportDocumentId.; Solo se exponen documentos soporte con Status = 1 y notas de ajuste con Status = 2 (filtrado fijo en la vista).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento soporte electrónico; Nota de ajuste (débito/crédito); Proveedor (SupplierThirdParty); Unidad operativa; Estado de transmisión electrónica', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ElectronicSupportDocument: Cuando esd.Status = 1, retorna el documento soporte con DocumentType=''ElectronicSupportDocument'', Code y StatusNote en NULL.; [RETURN_RESULT] Billing.ElectronicSupportDocumentAdjustmentNote: Cuando edan.Status = 2, retorna la nota de ajuste con DocumentType=''ElectronicSupportDocumentAdjustmentNote'', tomando DocumentNumber y SupplierThirdPartyId del documento soporte relacionado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ElectronicSupportDocument; Billing.ElectronicSupportDocumentAdjustmentNote', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocument';
GO
