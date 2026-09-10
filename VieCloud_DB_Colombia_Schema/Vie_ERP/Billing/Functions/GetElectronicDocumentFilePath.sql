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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Construye y devuelve la ruta de archivo en disco donde se almacena un documento electrónico de facturación (factura electrónica, nota crédito u otro tipo de documento), organizando la estructura de carpetas por unidad funcional, año, mes, tipo de documento y número de factura. Utiliza la función GetElectronicDocumentDocumentTypeName para convertir el código numérico del tipo de documento en un nombre legible que forma parte de la ruta. Es utilizada en el módulo de facturación electrónica para ubicar o guardar físicamente los archivos XML o PDF de documentos electrónicos generados por la IPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetElectronicDocumentFilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetElectronicDocumentFilePath';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye la ruta jerárquica del archivo de un documento electrónico de facturación organizada por unidad, año, mes, tipo de documento y número de factura.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentFilePath';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La fecha de factura debe ser válida para extraer YEAR/MONTH.; El tipo de documento debe existir en el catálogo consultado por Billing.GetElectronicDocumentDocumentTypeName.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentFilePath';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El separador de directorios siempre es ''\'' (estilo Windows).; La estructura de carpetas siempre se ordena: raíz → unidad → año → mes → tipo de documento → número.; El mes y año se derivan exclusivamente de la fecha de factura, no de la fecha actual.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentFilePath';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'documento electrónico; factura; tipo de documento; unidad de facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentFilePath';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (return value): Devuelve la concatenación: <FilePath>\<UnitCode>\<YEAR(InvoiceDate)>\<MONTH(InvoiceDate)>\<NombreTipoDocumento>\<InvoiceNumber>.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentFilePath';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.GetElectronicDocumentDocumentTypeName', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentFilePath';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetElectronicDocumentFilePath';
GO
