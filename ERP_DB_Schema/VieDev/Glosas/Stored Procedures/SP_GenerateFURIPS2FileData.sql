-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-11-12
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo FURIPS2
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateFURIPS2FileData] 
	@XmlParameters as XML,
	@XmlInvoices AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/***************************************** VARIABLES *****************************************/

	--Variables de control
	DECLARE	@RadicateInvoiceId INT,
			---------------------------------------------------------------------------------------
			@FilterByInvoices BIT = 0

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Table_Invoices TABLE(InvoiceId INT)

	--Facturas a generar
	DECLARE @Invoices TABLE
	(
		InvoiceId INT,
		InvoiceNumber VARCHAR(20),
		DetailInvoiceId INT,
		DetailInvoiceNumber VARCHAR(20)
	)

	BEGIN TRY

		/*************************************** CRITERIOS ***************************************/

		SELECT	@RadicateInvoiceId = t.x.value('RadicateInvoiceId[1]','int')
		FROM @XmlParameters.nodes('/Data') t(x)

		INSERT INTO @Table_Invoices
			SELECT DISTINCT
				t.x.value('InvoiceId[1]','int') InvoiceId
			FROM @XmlInvoices.nodes('/Data') t(x)

		IF EXISTS(SELECT 1 FROM @Table_Invoices)
		BEGIN
			SET @FilterByInvoices = 1
		END

		INSERT INTO @Invoices 
			(
				InvoiceId, InvoiceNumber, DetailInvoiceId, DetailInvoiceNumber
			)
			SELECT DISTINCT
				i.Id InvoiceId,
				i.InvoiceNumber InvoiceDate,
				IIF(i.DocumentType = 4, cc.Id, i.Id) DetailInvoiceId,
				IIF(i.DocumentType = 4, cc.InvoiceNumber, i.InvoiceNumber) DetailInvoiceNumber
			FROM @Table_Invoices ti
			JOIN Billing.Invoice i ON ti.InvoiceId = i.Id
			LEFT JOIN Billing.Invoice cc WITH (NOLOCK) ON i.DocumentType = 4 
														AND cc.DocumentType = 5 AND cc.Status = 1
														AND i.ThirdPartyId = cc.ThirdPartyId
														AND i.CareGroupId = cc.CareGroupId
														AND i.InvoiceCategoryId = cc.InvoiceCategoryId
														AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate 
														AND i.CapitationEndDate
			WHERE ISNULL(@RadicateInvoiceId, 0) = 0
		UNION ALL
			SELECT DISTINCT 
				i.Id InvoiceId,
				i.InvoiceNumber InvoiceDate,
				IIF(i.DocumentType = 4, cc.Id, i.Id) DetailInvoiceId,
				IIF(i.DocumentType = 4, cc.InvoiceNumber, i.InvoiceNumber) DetailInvoiceNumber
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId AND	rid.State <> 4
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber AND i.Status = 1
			LEFT JOIN @Table_Invoices ti ON i.Id = ti.InvoiceId
			LEFT JOIN Billing.Invoice cc WITH (NOLOCK) ON i.DocumentType = 4 
														AND cc.DocumentType = 5 
														AND cc.Status = 1
														AND i.ThirdPartyId = cc.ThirdPartyId
														AND i.CareGroupId = cc.CareGroupId
														AND i.InvoiceCategoryId = cc.InvoiceCategoryId
														AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			WHERE ri.Id = @RadicateInvoiceId
				AND (@FilterByInvoices = 0 OR ti.InvoiceId IS NOT NULL)

		/*****************************************************************************************/
		-- Se agrega el valor del copago y cuota moderadora al primer registro de cada factura
		SELECT	CA.CODIPSSEC AS PrestadorServicioSalud,
				i.InvoiceNumber NumeroFactura,
				a.NUMCONREC Consecutivo,
				ingreso.IPCODPACI,
				v.Tipo,
				v.CodigoCUM, 
				v.Descripcion, 
				v.Cantidad, 
				v.ValorUnitario, 
				v.SubTotal, 
				v.Total     
		FROM @Invoices i
		JOIN [Billing].[vFURIPS2Invoice] v ON i.DetailInvoiceId = v.InvoiceId
		JOIN dbo.ADINGRESO Ingreso WITH (NOLOCK) ON v.Ingreso = Ingreso.NUMINGRES  															
		JOIN dbo.[ADCENATEN] CA WITH (NOLOCK) ON CA.CODCENATE = Ingreso.CODCENATE
		JOIN ADFURIPSU a WITH(NOLOCK) ON a.IdInvoice = i.InvoiceId
		ORDER BY CAST(SUBSTRING(i.InvoiceNumber + '0', PATINDEX('%[0-9]%', i.InvoiceNumber + '0'), LEN(i.InvoiceNumber + '0')) AS DECIMAL)

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END