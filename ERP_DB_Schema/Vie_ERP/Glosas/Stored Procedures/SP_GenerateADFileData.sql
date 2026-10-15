
-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2023-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AD de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateADFileData] 
	@RadicateInvoiceId AS INT,
	@XmlInvoices AS XML,
	@PackageDetail BIT
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Invoices TABLE
	(
		InvoiceId INT,
		InvoiceNumber VARCHAR(50)
		UNIQUE NONCLUSTERED (InvoiceId),
		UNIQUE NONCLUSTERED (InvoiceNumber)
	)

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		ServiceOrderDetailId Int,
		InvoiceNumber VARCHAR(50), 
		IPSCode VARCHAR(50), 
		RIPSConcept VARCHAR(50),
		Quantity Int,
		TotalSalesPrice DECIMAL(18,0),
		NetToPay DECIMAL(18,0)
	)

	BEGIN TRY

		INSERT INTO @Invoices (InvoiceId, InvoiceNumber)
			SELECT DISTINCT
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM @XmlInvoices.nodes('/Data') t(x)
			JOIN Billing.Invoice i with(nolock) ON t.x.value('InvoiceId[1]','int') = i.Id
			WHERE i.DocumentType <> 4
		UNION ALL
			SELECT DISTINCT 
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber
			WHERE ri.Id = @RadicateInvoiceId
				AND rid.State <> 4
				AND i.DocumentType <> 4
		UNION ALL
			SELECT DISTINCT 
				cc.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber
			JOIN Billing.Invoice cc WITH (NOLOCK) ON i.ThirdPartyId = cc.ThirdPartyId
				AND i.CareGroupId = cc.CareGroupId
				AND i.InvoiceCategoryId = cc.InvoiceCategoryId
				AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			WHERE ri.Id = @RadicateInvoiceId
				AND rid.State <> 4
				AND i.DocumentType = 4
				AND cc.DocumentType = 5


		/*******************************************************************************************/
		
			INSERT INTO @TableResult 
			 SELECT 
				SOD.Id,
				xi.InvoiceNumber AS InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
				RTRIM(CUPS.RIPSConcept) AS RIPSConcept,
				IIF(SOD.InvoicedQuantity > 1, 1,SOD.InvoicedQuantity)as Quantity,--
				CAST(ROUND((ID.ThirdPartySalesPrice + ID.SubTotalPatientSalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) AS TotalSalesPrice, 
				CAST(ROUND((ID.ThirdPartySalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) AS NetToPay--
			 FROM Billing.Invoice AS I WITH (NOLOCK)
			 JOIN @Invoices xi ON i.Id = xi.InvoiceId
			 JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.Id 
			 JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId 
			 JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
			 JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
			 JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = SO.AdmissionNumber 
			 JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON admision.CODCENATE = CA.CODCENATE
			 CROSS APPLY [Billing].[DuplicateRows](SOD.Id, SOD.InvoicedQuantity) 
			 WHERE SOD.SettlementType <> IIF(@PackageDetail = 0,3,0) 
				and sod.InvoicedQuantity > 0 
				AND SOD.IsDelete = 0 
				AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept in ('01','02','03','04','05','06','07','08','09','11', '12', '13','14'))
				AND I.[Status] = 1
				AND SOD.SettlementType <> 3 

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	If @PackageDetail = 1 Begin

		INSERT INTO @TableResult 
		SELECT 
				SOD.Id,
				tr.InvoiceNumber AS InvoiceNumber, 
				LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
				RTRIM(CUPS.RIPSConcept) AS RIPSConcept,
				SOD.InvoicedQuantity as Quantity,
				tr.TotalSalesPrice AS TotalSalesPrice, 
				tr.NetToPay AS NetToPay
		FROM @TableResult tr
		JOIN Billing.ServiceOrderDetail sod with(nolock) on sod.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
		JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = SO.AdmissionNumber 
		JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON admision.CODCENATE = CA.CODCENATE
		JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
		JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sod.ProductId = ip.id 
		JOIN Inventory.ProductType PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id 	
		WHERE SOD.SettlementType <> IIF(@PackageDetail = 0,3,0)  
			and sod.InvoicedQuantity > 0 
			AND SOD.IsDelete = 0 
			AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept in ('01','02','03','04','05','06','07','08','09','11', '12', '13','14'))
			AND SOD.SettlementType <> 3 
	End

	SELECT 
	InvoiceNumber,
	IPSCode,
	RIPSConcept,
	Quantity,
	TotalSalesPrice,
	NetToPay 
	FROM @TableResult
	WHERE RIPSConcept IS NULL OR RIPSConcept in  ('01','02','03','04','05','06','07','08','09','11', '12', '13','14')
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)

END