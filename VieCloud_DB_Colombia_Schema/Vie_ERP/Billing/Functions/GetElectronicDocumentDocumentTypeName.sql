-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-01-04
-- Description:	Se devuelve el nombre del tipo de documento de acuerdo con el tipo de documento asociado al documento electronico
-- =============================================
CREATE FUNCTION [Billing].[GetElectronicDocumentDocumentTypeName]
(	
	@DocumentType INT
)
RETURNS VARCHAR(MAX) 
BEGIN

	DECLARE @DocumentTypeName VARCHAR(MAX)

	SELECT @DocumentTypeName = 
		CASE @DocumentType
			WHEN 1 THEN 'Factura EAPB con Contrato'
			WHEN 2 THEN 'Factura EAPB sin Contrato'
			WHEN 3 THEN 'Factura Particular'
			WHEN 4 THEN 'Factura Capitada'
			WHEN 5 THEN 'Control de Capitacion'
			WHEN 6 THEN 'Factura Basica'
			WHEN 7 THEN 'Factura de Venta de Productos'
			-------------------------------------------------------------------
			WHEN 91 THEN 'Nota Credito'
			WHEN 92 THEN 'Nota Debito'
			WHEN 93 THEN 'Nota Credito: Referencia a facturas electrónicas diferentes a validación previa'
			WHEN 94 THEN 'Nota Debito: Referencia a facturas electrónicas diferentes a validación previa'
			WHEN 98 THEN 'Nota Debito'
			WHEN 99 THEN 'Nota Credito'
			ELSE 'N/A'
		END

	 RETURN @DocumentTypeName
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte un código numérico de tipo de documento electrónico en su nombre descriptivo en español. Cubre los tipos de facturación electrónica utilizados en el módulo de cobros: facturas a EAPB (con o sin contrato), facturas particulares, capitadas, básicas y de venta de productos, así como notas crédito y notas débito. Se usa principalmente en la generación y presentación de documentos electrónicos de facturación para mostrar al usuario o en reportes el nombre legible del tipo de comprobante en lugar del código interno.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetElectronicDocumentDocumentTypeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetElectronicDocumentDocumentTypeName';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce el código numérico del tipo de documento electrónico de facturación a su nombre legible (facturas, notas crédito/débito), devolviendo ''N/A'' cuando el código no está catalogado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar un código entero que represente el tipo de documento electrónico a traducir.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los códigos 91 y 99 producen la misma descripción (''Nota Credito''); los códigos 92 y 98 producen la misma descripción (''Nota Debito''), tratándose como sinónimos.; Cualquier código no contemplado en la lista enumerada se rotula como ''N/A'', garantizando un valor de retorno no nulo siempre que se evalúe el CASE.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento electrónico; Factura EAPB con contrato; Factura EAPB sin contrato; Factura particular; Factura capitada; Control de capitación; Factura básica; Factura de venta de productos; Nota crédito; Nota débito; Validación previa de facturas electrónicas', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (valor de retorno): Devuelve un VARCHAR(MAX) con la descripción del tipo de documento según el CASE sobre @DocumentType; si no hay coincidencia, devuelve ''N/A''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de documento = 1 → Retorna ''Factura EAPB con Contrato''; si Tipo de documento = 2 → Retorna ''Factura EAPB sin Contrato''; si Tipo de documento = 3 → Retorna ''Factura Particular''; si Tipo de documento = 4 → Retorna ''Factura Capitada''; si Tipo de documento = 5 → Retorna ''Control de Capitacion''; si Tipo de documento = 6 → Retorna ''Factura Basica''; si Tipo de documento = 7 → Retorna ''Factura de Venta de Productos''; si Tipo de documento = 91 ó 99 → Retorna ''Nota Credito''; si Tipo de documento = 92 ó 98 → Retorna ''Nota Debito''; si Tipo de documento = 93 → Retorna ''Nota Credito: Referencia a facturas electrónicas diferentes a validación previa''; si Tipo de documento = 94 → Retorna ''Nota Debito: Referencia a facturas electrónicas diferentes a validación previa''; si Tipo de documento no coincide con ninguno de los códigos definidos (1-7, 91-94, 98, 99) → Retorna ''N/A''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentTypeName';
GO
