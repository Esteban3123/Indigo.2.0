-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-01-04
-- Description:	Se devuelve la ruta del documento electronico de acuerdo a los parametros recibidos
-- =============================================
CREATE FUNCTION [Billing].[GetElectronicDocumentFilePath]
(	
	@FilePath VARCHAR(MAX),
	@UnitCode VARCHAR(5),
	@InvoiceDate DATE,
	@DocumentType INT,
	@InvoiceNumber VARCHAR(20)
)
RETURNS VARCHAR(MAX) 
BEGIN

	DECLARE @Separator VARCHAR(1) = '\'

	SELECT @FilePath = CONCAT(@FilePath, @Separator, @UnitCode, @Separator, YEAR(@InvoiceDate), @Separator, MONTH(@InvoiceDate), @Separator, Billing.GetElectronicDocumentDocumentTypeName(@DocumentType), @Separator, @InvoiceNumber)

	 RETURN @FilePath
END