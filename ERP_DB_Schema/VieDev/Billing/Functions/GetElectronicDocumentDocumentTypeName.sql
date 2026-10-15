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