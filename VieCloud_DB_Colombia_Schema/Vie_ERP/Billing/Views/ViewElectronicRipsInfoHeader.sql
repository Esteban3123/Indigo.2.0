


CREATE view [Billing].[ViewElectronicRipsInfoHeader]
as

SELECT 
	i.InvoiceNumber,
	tp.Nit
FROM Billing.Invoice i
join Common.ThirdParty tp on i.ThirdPartyId = tp.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Encabezado de información electrónica para RIPS: combina el número de factura con el NIT del tercero pagador (EPS, aseguradora o empresa) al que fue emitida. Cruza las facturas de cobro con los datos del tercero para construir el encabezado requerido en la generación de archivos RIPS electrónicos. Se usa en reportería de facturación electrónica y envío de cuentas a pagadores.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicRipsInfoHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicRipsInfoHeader';
GO
