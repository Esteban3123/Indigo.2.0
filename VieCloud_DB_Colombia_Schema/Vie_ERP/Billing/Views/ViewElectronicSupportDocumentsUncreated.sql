-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2025-09-29
-- Description:	Vista encargada de mostrar documentos electronicos no creados
-- =============================================

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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Exponer la información de documentos soporte electrónicos para su consulta, en el contexto de documentos pendientes de creación ante la DIAN.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentsUncreated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir información en la tabla de documentos soporte electrónicos para retornar resultados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentsUncreated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Expone exclusivamente los atributos del documento soporte electrónico tal como están almacenados, sin transformaciones ni cálculos adicionales.; No aplica filtros: retorna todos los registros de la tabla origen, dejando al consumidor la responsabilidad de filtrar por estado o condición de ''no creados''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentsUncreated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento soporte electrónico; Facturación electrónica; DIAN; Proveedor; Cliente; Autorización de facturación; CUDS; Unidad operativa; Impuestos; Fecha de radicación; Fecha de expiración; Fecha de envío', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentsUncreated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ElectronicSupportDocument: Devuelve todas las filas de la tabla de documentos soporte electrónicos con sus campos de identificación, fechas, terceros, valores, estado y datos de transmisión electrónica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentsUncreated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ElectronicSupportDocument', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentsUncreated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentsUncreated';
GO
