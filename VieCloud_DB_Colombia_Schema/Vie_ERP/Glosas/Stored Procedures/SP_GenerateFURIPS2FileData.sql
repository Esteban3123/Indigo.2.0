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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos del archivo FURIPS2 (Factura Única de Recobro IPS, formato 2) para la transmisión de información a entidades pagadoras. Recibe como parámetros un XML con el identificador de radicado de cartera y un XML con las facturas a procesar; a partir de estos, determina qué facturas incluir: bien por listado explícito de facturas o por las facturas asociadas a un radicado específico en cartera (RadicateInvoiceC / RadicateInvoiceD). Consolida el detalle del archivo cruzando las facturas de cobro (Billing.Invoice), el ingreso del paciente (ADINGRESO), el centro de atención (ADCENATEN) y el consecutivo FURIPS (ADFURIPSU), e incluye campos como el código del prestador, número de factura, cédula del paciente, tipo de servicio, código CUM, descripción, cantidades y valores cobrados. Este procedimiento existe para apoyar el proceso de glosas y recobros, facilitando la generación del reporte FURIPS2 exigido por la normativa colombiana de facturación en salud.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateFURIPS2FileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateFURIPS2FileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el detalle del reporte FURIPS2 (anexo de glosas/recobros) consolidando facturas seleccionadas explícitamente o asociadas a un radicado de cartera, junto con datos del prestador, paciente, ingreso y consecutivo FURIPS.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS2FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de parámetros debe exponer el nodo /Data con RadicateInvoiceId (puede ser nulo); El XML de facturas debe exponer nodos /Data con InvoiceId si se quiere filtrar por listado explícito; Las facturas deben existir en Billing.Invoice y tener registro en ADFURIPSU (consecutivo FURIPS) y un ingreso válido en ADINGRESO con su centro de atención en ADCENATEN; Si se filtra por radicado, debe existir el RadicateInvoiceC y sus detalles RadicateInvoiceD asociados', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS2FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas activas (Status = 1) cuando se obtienen vía radicado; Se excluyen los detalles de radicado anulados/estado 4 (rid.State <> 4); Para facturas de capitación (DocumentType = 4) el detalle proviene siempre de su nota crédito asociada (DocumentType = 5) dentro del rango de capitación; Los errores se capturan y solo se imprimen, sin interrumpir ni propagar la excepción; El resultado se ordena numéricamente por el número de factura, no alfabéticamente', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS2FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'FURIPS2; Glosas y recobros; Factura de capitación; Nota crédito; Radicación de cartera; Copago y cuota moderadora; Prestador de servicios de salud; Ingreso/admisión del paciente; Centro de atención; Código CUM', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS2FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT: Devuelve el detalle FURIPS2 (prestador, número de factura, consecutivo, cédula del paciente, tipo, CUM, descripción, cantidad, valor unitario, subtotal y total) ordenado por la parte numérica del número de factura', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS2FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen filas en la tabla derivada del XML de facturas → Activa el filtro por listado explícito (@FilterByInvoices = 1) restringiendo las facturas del radicado a las indicadas else No se aplica filtro por listado y se toman todas las facturas asociadas al radicado; si ISNULL(@RadicateInvoiceId,0) = 0 → Toma las facturas directamente del listado XML cruzadas con Billing.Invoice else Toma las facturas a partir del radicado de cartera (RadicateInvoiceC/RadicateInvoiceD con State <> 4) y Billing.Invoice activa (Status = 1); si i.DocumentType = 4 (factura de capitación) → Usa como detalle la nota crédito (DocumentType = 5, Status = 1) que coincide en tercero, grupo de atención, categoría y cuya fecha cae entre CapitationInitialDate y CapitationEndDate else Usa la propia factura como detalle', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS2FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.vFURIPS2Invoice; dbo.ADINGRESO; dbo.ADCENATEN; dbo.ADFURIPSU', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS2FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS2FileData';
-- GO
