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