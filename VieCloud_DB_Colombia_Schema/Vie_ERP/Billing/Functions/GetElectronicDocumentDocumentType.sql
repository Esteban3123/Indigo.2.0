-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-11-27
-- Description:	De acuerdo con el tipo de documento devolver el tipo de documento electrónico al que corresponde
-- =============================================
CREATE FUNCTION [Billing].[GetElectronicDocumentDocumentType]
(	
	@Type INT
)
RETURNS VARCHAR(2) 
BEGIN

	DECLARE @DocumentType INT

	SELECT @DocumentType = 
		CASE @Type
			WHEN 1 THEN 1 -- Factura EAPB con Contrato
			WHEN 2 THEN 1 -- Factura EAPB Sin Contrato
			WHEN 3 THEN 1 -- Factura Particular
			WHEN 4 THEN 1 -- Factura Capitada
			WHEN 6 THEN 1 -- Factura Basica
			WHEN 7 THEN 1 -- Factura de Venta de Productos
			--------------------------------			
			WHEN 98 THEN 98 -- Nota Debito
			WHEN 99 THEN 99 -- Nota Credito
			ELSE @Type
		END

	 RETURN @DocumentType
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte el tipo de documento de facturación interno del sistema al tipo de documento electrónico correspondiente según la normativa de facturación electrónica en Colombia. Recibe un código numérico que identifica el tipo de factura (EAPB con contrato, sin contrato, particular, capitada, básica, venta de productos) y lo traduce al código estándar: 1 para cualquier tipo de factura, 98 para nota débito y 99 para nota crédito. Se utiliza en el proceso de generación de documentos electrónicos de facturación para asegurar que el tipo enviado cumpla con los códigos requeridos por la DIAN o la entidad reguladora correspondiente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetElectronicDocumentDocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetElectronicDocumentDocumentType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Mapea un tipo de documento interno al tipo de documento electrónico equivalente para efectos de facturación electrónica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las modalidades de factura (con/sin contrato, particular, capitada, básica, venta de productos) se consolidan bajo un único tipo electrónico (1).; Las notas crédito y débito conservan su identificador original al convertirse a documento electrónico.; Aunque la variable interna y el CASE operan con INT, el valor retornado se trunca a VARCHAR(2).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura EAPB con contrato; Factura EAPB sin contrato; Factura Particular; Factura Capitada; Factura Básica; Factura de Venta de Productos; Nota Débito; Nota Crédito; Documento electrónico', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Tipos 1,2,3,4,6,7 (variantes de Factura: EAPB con/sin contrato, Particular, Capitada, Básica, Venta de Productos) se normalizan al tipo electrónico 1 (Factura).; [RETURN_RESULT] : Tipo 98 se mantiene como 98 (Nota Débito) y tipo 99 como 99 (Nota Crédito).; [RETURN_RESULT] : Cualquier otro valor distinto a los mapeados se devuelve sin transformación (ELSE @Type).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Type IN (1,2,3,4,6,7) → Retorna 1 (Factura electrónica) else Continúa evaluando; si @Type = 98 → Retorna 98 (Nota Débito); si @Type = 99 → Retorna 99 (Nota Crédito); si @Type no coincide con ningún caso → Retorna el mismo @Type recibido', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentDocumentType';
GO
