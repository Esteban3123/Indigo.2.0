-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-11-10
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo MegaPlano
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateMegaPlaneFileData] 
	@XmlParameters as XML,
	@XmlInvoices AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/***************************************** VARIABLES *****************************************/

	--Variables de control
	DECLARE	@RadicateInvoiceId INT,
			@CodificationType INT,
			@ServiceCode INT,
			---------------------------------------------------------------------------------------
			@IPSCode VARCHAR(3),
			@IPSDocumentType VARCHAR(2),
			@IPSNit VARCHAR(15),
			@IPSDigitVerification VARCHAR(1),
			@IPSName VARCHAR(50),
			@CustomerDocumentType VARCHAR(2),
			@CustomerNit VARCHAR(15),
			@CustomerDigitVerification VARCHAR(1),
			@CustomerName VARCHAR(50),
			---------------------------------------------------------------------------------------
			@FilterByInvoices BIT = 0

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Table_Invoices TABLE(InvoiceId INT)

	--Facturas a generar
	DECLARE @Invoices TABLE
	(
		InvoiceId INT,
		InvoiceDate DATE,
		InvoicePrefix VARCHAR(6),
		InvoiceNumber VARCHAR(10),
		DetailInvoiceId INT,
		DetailInvoiceNumber VARCHAR(10),
		DetailDiscountValue DECIMAL(18,0),
		DetailModeratingFee DECIMAL(18,0),
		DetailCopay DECIMAL(18,0),
		DetailNetValue DECIMAL(18,0)
	)

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		IPSCode VARCHAR(3), 
		IPSDocumentType VARCHAR(2),
		IPSNit VARCHAR(15),
		IPSDigitVerification VARCHAR(1),
		IPSName VARCHAR(50),
		CustomerDocumentType VARCHAR(2),
		CustomerNit VARCHAR(15),
		CustomerDigitVerification VARCHAR(1),
		CustomerName VARCHAR(50),
		---------------------------------------------------
		InvoiceId INT, 
		InvoiceDate VARCHAR(10),
		InvoicePrefix VARCHAR(6),
		InvoiceNumber VARCHAR(10),
		InvoiceGrossValue DECIMAL(18,0),
		InvoiceModeratingFee DECIMAL(18,0),
		InvoiceCopay DECIMAL(18,0),
		InvoiceNetValue DECIMAL(18,0),
		---------------------------------------------------
		DetailId INT,
		DetailInvoiceId INT,
		DetailInvoiceNumber VARCHAR(10),
		DetailAuthorizationNumber VARCHAR(15),
		DetailDocumentType VARCHAR(2),
		DetailDocumentNumber VARCHAR(16),
		DetailLastName VARCHAR(25),
		DetailFirstName VARCHAR(15),
		DetailAdmissionDate VARCHAR(10),
		DetailOutputDate VARCHAR(10), 
		DetailAttentionType VARCHAR(2),
		DetailCode VARCHAR(12), 
		DetailDescription VARCHAR(100), 
		DetailQuantity INT, 
		DetailUnitValue DECIMAL(18,0),
		DetailTotalValue DECIMAL(18,0),
		DetailDiagnostic VARCHAR(7),
		DetailModeratingFee DECIMAL(18,0),
		DetailCopay DECIMAL(18,0)
	)

	BEGIN TRY

		/*************************************** CRITERIOS ***************************************/

		SELECT	@IPSNit = t.x.value('IPSNit[1]','varchar(15)'),
				@RadicateInvoiceId = t.x.value('RadicateInvoiceId[1]','int'),
				@CodificationType = t.x.value('CodificationType[1]','int'),
				@ServiceCode = t.x.value('ServiceCode[1]','int')
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
				InvoiceId, InvoiceDate, InvoicePrefix, InvoiceNumber, 
				DetailInvoiceId, DetailInvoiceNumber, DetailDiscountValue, DetailModeratingFee, DetailCopay, DetailNetValue
			)
			SELECT DISTINCT 
				IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.Id, ri.Id) InvoiceId,
				IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.InvoiceDate, ri.RadicatedDate) InvoiceDate,
				REPLACE(IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.InvoiceNumber, ''), dbo.udf_GetNumeric(IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.InvoiceNumber, '')), '') InvoicePrefix,
				REPLACE(LTRIM(REPLACE(dbo.udf_GetNumeric(IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.InvoiceNumber, ri.RadicatedConsecutive)),'0',' ')),' ','0') InvoiceNumber,
				IIF(i.DocumentType = 4 AND cc.DocumentType = 5, cc.Id, i.Id) AS DetailInvoiceId,
				i.InvoiceNumber DetailInvoiceNumber,
				IIF(i.DocumentType = 4, 0, i.ThirdPartyDiscountValue) DetailDiscountValue,
				IIF(i.DocumentType = 4, 0, 0) DetailModeratingFee,
				IIF(i.DocumentType = 4, 0, ISNULL(id.DetailCopay, 0)) DetailCopay,
				IIF(i.DocumentType = 4, 0, i.ThirdPartySalesValue) DetailNetValue
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId AND	rid.State <> 4
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber AND i.Status = 1
			LEFT JOIN @Table_Invoices ti ON i.Id = ti.InvoiceId
			LEFT JOIN Billing.Invoice cc WITH (NOLOCK) ON i.DocumentType = 4 AND cc.DocumentType = 5 AND cc.Status = 1
				AND i.ThirdPartyId = cc.ThirdPartyId
				AND i.CareGroupId = cc.CareGroupId
				AND i.InvoiceCategoryId = cc.InvoiceCategoryId
				AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			LEFT JOIN
			(
				SELECT InvoiceId, SUM(ROUND(SubTotalPatientSalesPrice / InvoicedQuantity, 0)) DetailCopay
				FROM Billing.InvoiceDetail
				GROUP BY InvoiceId
			) id ON i.Id = id.InvoiceId
			WHERE ri.Id = @RadicateInvoiceId
				AND (@FilterByInvoices = 0 OR ti.InvoiceId IS NOT NULL)

		SELECT	@IPSCode = '001',
				@IPSDocumentType = Common.GetIdentificationTypeCode(p.IdentificationType, 2),
				@IPSDigitVerification = tp.DigitVerification,
				@IPSName = SUBSTRING(tp.Name, 1, 50)
		FROM Common.ThirdParty tp WITH (NOLOCK)
		JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
		WHERE tp.Nit = @IPSNit

		SELECT	@CustomerDocumentType = Common.GetIdentificationTypeCode(p.IdentificationType, 2),
				@CustomerNit = tp.Nit,
				@CustomerDigitVerification = tp.DigitVerification,
				@CustomerName = SUBSTRING(tp.Name, 1, 50)
		FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
		JOIN Common.Customer c WITH (NOLOCK) ON ri.CustomerId = c.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON c.ThirdPartyId = tp.Id
		JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
		WHERE ri.Id = @RadicateInvoiceId

		/*****************************************************************************************/

		INSERT INTO @TableResult 
			 SELECT	@IPSCode IPSCode,
					@IPSDocumentType IPSDocumentType,
					@IPSNit IPSNit,
					@IPSDigitVerification IPSDigitVerification,
					@IPSName IPSName,
					@CustomerDocumentType CustomerDocumentType,
					@CustomerNit CustomerNit,
					@CustomerDigitVerification CustomerDigitVerification,
					@CustomerName CustomerName,
					---------------------------------------------------
					xi.InvoiceId,
					CONVERT(VARCHAR(10),xi.InvoiceDate,103) InvoiceDate,
					xi.InvoicePrefix,
					xi.InvoiceNumber,
					0 DetailGrossValue,
					0 DetailModeratingFee,
					0 DetailCopay,
					0 DetailNetValue,
					---------------------------------------------------
					id.Id,
					xi.DetailInvoiceId,
					xi.DetailInvoiceNumber,
					SUBSTRING(COALESCE(sod.AuthorizationNumber, admission.IAUTORIZA, ''), 1, 15) DetailAuthorizationNumber,
					Common.GetIdentificationTypeCode(pacient.IPTIPODOC, 1) DetailDocumentType,
					pacient.IPCODPACI DetailDocumentNumber,
					SUBSTRING(CONCAT(LTRIM(RTRIM(pacient.IPPRIAPEL)), ' ', LTRIM(RTRIM(pacient.IPSEGAPEL))), 1, 25) DetailLastName,
					SUBSTRING(CONCAT(LTRIM(RTRIM(pacient.IPPRINOMB)), ' ', LTRIM(RTRIM(pacient.IPSEGNOMB))), 1, 15) DetailFirstName,
					CONVERT(VARCHAR(10), IIF(admission.IFECHAING > id.ServiceDate, id.ServiceDate, admission.IFECHAING), 103) DetailAdmissionDate,
					CONVERT(VARCHAR(10), IIF(Alta.FECALTPAC > id.ServiceDate, Alta.FECALTPAC, id.ServiceDate), 103) DetailOutputDate,
					'17' DetailAttentionType,
					SUBSTRING
					(	
						CASE sod.RecordType
							WHEN 1 THEN
								CASE @ServiceCode
									WHEN 1 THEN cups.RIPSCode
									WHEN 2 THEN cups.Code
									WHEN 3 THEN ips.Code
									WHEN 4 THEN cups.Code
									WHEN 5 THEN cups.Code
								END
							WHEN 2 THEN
								CASE @ServiceCode
									WHEN 1 THEN ip.Code
									WHEN 2 THEN ip.CodeAlternative
									WHEN 3 THEN ip.CodeAlternativeTwo
									WHEN 4 THEN ip.CodeAlternative
									WHEN 5 THEN ip.CodeAlternative
								END
						END, 1, 12
					) DetailCode,
					SUBSTRING
					(
						CASE sod.RecordType
							WHEN 1 THEN
								CASE @ServiceCode
									WHEN 1 THEN ISNULL(cups.RIPSDescription, cups.Description)
									WHEN 2 THEN ISNULL(cups.RIPSDescription, cups.Description)
									WHEN 3 THEN cups.RIPSCode
									WHEN 4 THEN COALESCE(cd.Name, cups.RIPSDescription, cups.Description)
									WHEN 5 THEN CONCAT(ISNULL(cups.RIPSDescription, cups.Description), ' ' + cd.Name)
								END
							WHEN 2 THEN ip.Name
						END, 1, 100
					) DetailDescription,
					id.InvoicedQuantity DetailQuantity,
					ROUND(id.GrandTotalSalesPrice / id.InvoicedQuantity, 0) DetailUnitValue,
					id.GrandTotalSalesPrice DetailTotalValue,
					SUBSTRING(COALESCE(i.OutputDiagnosis,admission.CODDIAEGR,'Z000'), 1, 7) DetailDiagnostic,
					0 DetailModeratingFee,
					0 DetailCopay
			FROM @Invoices xi		
			JOIN Billing.Invoice i WITH (NOLOCK) ON xi.DetailInvoiceId = i.Id
			JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id
			JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES
			JOIN dbo.INPACIENT pacient WITH (NOLOCK) ON i.PatientCode = pacient.IPCODPACI
			LEFT JOIN 
			(
				SELECT Alta.NUMINGRES, MAX(Alta.FECALTPAC) FECALTPAC
				FROM
				(
						SELECT NUMINGRES, MAX(FECFINEST) AS FECALTPAC 
						FROM dbo.CHREGESTA AS CHR WITH (NOLOCK) 
						JOIN dbo.CHCAMASHO AS CHC WITH (NOLOCK) ON CHR.CODICAMAS = CHC.CODICAMAS 
						WHERE CHC.CODCLACAM  = 1 
						GROUP BY NUMINGRES
					UNION ALL
						SELECT NUMINGRES, FECALTPAC
						FROM dbo.HCREGEGRE
				) Alta
				GROUP BY NUMINGRES
			) AS Alta ON admission.NUMINGRES = ALTA.NUMINGRES
			LEFT JOIN Contract.IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id
			LEFT JOIN Contract.CUPSEntity cups WITH (NOLOCK) ON sod.CUPSEntityId = cups.Id
			LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON sod.CUPSEntityContractDescriptionId = cecd.Id
			LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
			LEFT JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sod.ProductId = ip.id 
			LEFT JOIN Inventory.ProductType as PT WITH (NOLOCK) ON ip.ProductTypeId = PT.Id
			WHERE id.InvoicedQuantity > 0 AND sod.SettlementType <> 3 AND sod.IsDelete = 0
				AND
				(
					(sod.RecordType = 1 AND cups.RIPSConcept in ('06','07','08','09','11','14'))
					OR
					(sod.RecordType = 2 AND pt.Class = 3)
				)

		-- Se agrega el valor del copago y cuota moderadora al primer registro de cada factura
		UPDATE tr
			SET tr.DetailModeratingFee = xi.DetailModeratingFee,
				tr.DetailCopay = xi.DetailCopay
		FROM @Invoices xi
		JOIN @TableResult tr ON xi.DetailInvoiceId = tr.DetailInvoiceId
		JOIN
		(
			SELECT InvoiceId, DetailInvoiceId, MIN(DetailId) DetailId
			FROM @TableResult
			GROUP BY InvoiceId, DetailInvoiceId
		) trd ON tr.InvoiceId = trd.InvoiceId AND tr.DetailInvoiceId = trd.DetailInvoiceId AND tr.DetailId = trd.DetailId

		-- Se agrega mayoriza el valor del valor factura, copago y cuota moderadora, y valor neto en el registro de cabecera
		UPDATE tr
			SET tr.InvoiceGrossValue = trd.DetailNetValue + trd.DetailDiscountValue + trd.DetailModeratingFee + trd.DetailCopay,
				tr.InvoiceModeratingFee = trd.DetailModeratingFee,
				tr.InvoiceCopay = trd.DetailCopay,
				tr.InvoiceNetValue = trd.DetailNetValue
		FROM @TableResult tr
		JOIN
		(
			SELECT	tr.InvoiceId, 					
					SUM(tr.DetailModeratingFee) DetailModeratingFee, 
					SUM(tr.DetailCopay) DetailCopay,
					SUM(xi.DetailNetValue) DetailNetValue,
					SUM(xi.DetailDiscountValue) DetailDiscountValue
			FROM 
			(
				SELECT	tr.InvoiceId,
						tr.DetailInvoiceId,
						SUM(tr.DetailModeratingFee) DetailModeratingFee,
						SUM(tr.DetailCopay) DetailCopay
				FROM @TableResult tr
				GROUP BY tr.InvoiceId, tr.DetailInvoiceId
			) tr 
			JOIN @Invoices xi ON tr.InvoiceId = xi.InvoiceId AND tr.DetailInvoiceId = xi.DetailInvoiceId
			GROUP BY tr.InvoiceId
		) trd ON tr.InvoiceId = trd.InvoiceId

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * 
	FROM @TableResult
	ORDER BY InvoicePrefix, InvoiceNumber, DetailInvoiceNumber, DetailId
END