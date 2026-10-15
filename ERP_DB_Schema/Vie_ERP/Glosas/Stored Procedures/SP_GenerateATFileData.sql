-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AT de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateATFileData] 
	@RadicateInvoiceId AS INT,
	@XmlInvoices as XML,
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
	)

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		ServiceOrderDetailId Int,
		InvoiceId Int,
		InvoiceNumber VARCHAR(50), 
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		IdentificationNumber VARCHAR(50), 
		AuthorizationNumber VARCHAR(50), 
		TypeService INT, 
		RIPSCode VARCHAR(50), 
		CUPSCode VARCHAR(50), 
		CUPSDescription VARCHAR(300), 
		ManualRateCode VARCHAR(50), 
		ManualRateName VARCHAR(300), 
		InvoicedQuantity INT, 
		TotalSalesPrice DECIMAL(18,0) ,
		GrandTotalSalesPrice DECIMAL(18,0),
		ContractDescriptionCode varchar(20),
		ContractDescriptionName varchar(250),
		ProductTypeClass Tinyint NULL,
		RIPSConcept Char(2) NULL
	)

	BEGIN TRY

		INSERT INTO @Invoices (InvoiceId, InvoiceNumber)
			SELECT DISTINCT
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM @XmlInvoices.nodes('/Data') t(x)
			JOIN Billing.Invoice i ON t.x.value('InvoiceId[1]','int') = i.Id
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
				SOD.Id, I.Id, xi.InvoiceNumber as InvoiceNumber, 
				LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
				TIP.SIGLA AS IdentificationTypeCode, 
				LTRIM(RTRIM(pacient.IPCODPACI)) as IdentificationNumber, 
				RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber, 
				CASE CUPS.RIPSConcept 
					WHEN '06' THEN 3 
					WHEN '07' THEN 4 
					WHEN '08' THEN 3 
					WHEN '09' THEN 1 
					WHEN '11' THEN 1 
					WHEN '14' THEN 2 
					ELSE 0 
				END AS TypeService, 
				CUPS.RIPSCode as RIPSCode, 
				RTRIM(CUPS.Code) as CUPSCode, 
				ISNULL(RTRIM(CUPS.RIPSDescription),RTRIM(CUPS.Description)) as CUPSDescription,
				RTRIM(IPS.Code) as ManualRateCode, 
				RTRIM(IPS.Name) as ManualRateName, 
				ID.InvoicedQuantity as InvoicedQuantity, 
				CAST(ROUND((ID.GrandTotalSalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) as TotalSalesPrice, 
				Id.GrandTotalSalesPrice AS GrandTotalSalesPrice,
				ISNULL(cd.Code, '') ContractDescriptionCode,
				ISNULL(cd.Name, '') ContractDescriptionName,
				NULL AS ProductRateClass,
				CUPS.RIPSConcept
			FROM Billing.Invoice I WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId
			JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.Id 
			JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON I.RevenueControlDetailId = RCD.Id 
			JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.Id 
			JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
			JOIN [Contract].IPSService IPS WITH (NOLOCK) ON SOD.IPSServiceId = IPS.Id 
			JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI 
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			left join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.Id = SOD.CUPSEntityContractDescriptionId
			left join Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
			WHERE SOD.InvoicedQuantity > 0 
				AND I.[Status] = 1 
				AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept in ('06','07','08','09','11','14'))
				AND SOD.SettlementType <> 3 
				AND SOD.IsDelete = 0 
		UNION ALL 
			SELECT 
				SOD.Id, I.Id, xi.InvoiceNumber as InvoiceNumber, 
				CA.CODIPSSEC AS IPSCode, 
				TIP.SIGLA AS IdentificationTypeCode, 
				pacient.IPCODPACI as IdentificationNumber, 
				RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber, 
				1 AS TypeService, 
				IP.Code as RIPSCode, 
				IP.CodeAlternative as CUPSCode, 
				IP.Name as CUPSDescription,
				IP.CodeAlternativeTwo as ManualRateCode, 
				IP.Name as ManualRateName, 
				SOD.InvoicedQuantity as InvoicedQuantity, 
				CAST(ROUND((ID.GrandTotalSalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) as TotalSalesPrice, 
				Id.GrandTotalSalesPrice AS GrandTotalSalesPrice,
				'' ContractDescriptionCode,
				'' ContractDescriptionName,
				PT.Class AS ProductRateClass,
				NULL AS RIPSConcept
			FROM Billing.Invoice I WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId
			JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.ID 
			JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.Id 
			JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
			JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON SOD.ProductId = ip.id 
			JOIN Inventory.ProductType as PT WITH (NOLOCK) ON IP.ProductTypeId = PT.Id 
			JOIN [Contract].HealthAdministrator HA WITH (NOLOCK) ON I.HealthAdministratorId = HA.Id 
			JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON I.RevenueControlDetailId = RCD.Id 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			WHERE SOD.InvoicedQuantity > 0 
				AND I.[Status] = 1 
				AND ((@PackageDetail = 1 AND sod.Packaging = 1) OR PT.Class = 3)
				AND SOD.SettlementType <> IIF(@PackageDetail = 0,3,0) 
				AND SOD.IsDelete = 0
				
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados

	If @PackageDetail = 1 Begin
		INSERT Into @TableResult
		select SOD.Id, tr.InvoiceId, tr.InvoiceNumber as InvoiceNumber, 
			LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
			tr.IdentificationTypeCode, 
			tr.IdentificationNumber, 
			RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber, 
			CASE CUPS.RIPSConcept 
				WHEN '06' THEN 3 
				WHEN '07' THEN 4 
				WHEN '08' THEN 3 
				WHEN '09' THEN 1 
				WHEN '11' THEN 1 
				WHEN '14' THEN 2 
				ELSE 0 
			END AS TypeService, 
			CUPS.RIPSCode as RIPSCode, 
			RTRIM(CUPS.Code) as CUPSCode, 
			ISNULL(RTRIM(CUPS.RIPSDescription),RTRIM(CUPS.Description)) as CUPSDescription,
			RTRIM(IPS.Code) as ManualRateCode, 
			RTRIM(IPS.Name) as ManualRateName, 
			SOD.InvoicedQuantity as InvoicedQuantity, 
			0 as TotalSalesPrice, 
			0 AS GrandTotalSalesPrice,
			ISNULL(cd.Code, '') ContractDescriptionCode,
			ISNULL(cd.Name, '') ContractDescriptionName,
			NULL AS ProductRateClass,
			CUPS.RIPSConcept
		from @TableResult tr
		JOIN Billing.ServiceOrderDetail SOD WITH(NOLOCK) ON sod.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id
		JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
		JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
		JOIN [Contract].IPSService IPS WITH (NOLOCK) ON SOD.IPSServiceId = IPS.Id 
		JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId 
		left join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.Id = SOD.CUPSEntityContractDescriptionId
		left join Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
		WHERE SOD.CUPSEntityId IS NOT NULL 
				AND SOD.InvoicedQuantity > 0 
				AND cups.RIPSConcept in ('06','07','08','09','11','14')
				AND SOD.SettlementType <> 3 
				AND SOD.IsDelete = 0 
		UNION ALL
		select SOD.Id, tr.InvoiceId, tr.InvoiceNumber as InvoiceNumber, 
			CA.CODIPSSEC AS IPSCode, 
			tr.IdentificationTypeCode, 
			tr.IdentificationNumber, 
			RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber, 
			1 AS TypeService, 
			IP.Code as RIPSCode, 
			IP.CodeAlternative as CUPSCode, 
			IP.Name as CUPSDescription,
			IP.CodeAlternativeTwo as ManualRateCode, 
			IP.Name as ManualRateName, 
			SOD.InvoicedQuantity as InvoicedQuantity, 
			0 as TotalSalesPrice, 
			0 AS GrandTotalSalesPrice,
			'' ContractDescriptionCode,
			'' ContractDescriptionName,
			PT.Class AS ProductRateClass,
			NULL AS RIPSConcept
		from @TableResult tr
		JOIN Billing.ServiceOrderDetail SOD WITH(NOLOCK) ON sod.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id
		JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON SOD.ProductId = ip.id 
		JOIN Inventory.ProductType as PT WITH (NOLOCK) ON IP.ProductTypeId = PT.Id 
		JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
		JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
		WHERE SOD.ProductId IS NOT NULL
			AND sod.InvoicedQuantity > 0 
			AND sod.SettlementType <>  IIF(@PackageDetail = 0,3,0)   
			AND sod.IsDelete = 0 
			AND PT.Class = 3
	End

	SELECT * 
	FROM @TableResult
	WHERE (RIPSConcept IS NULL OR RIPSConcept IN ('06','07','08','09','11','14')) AND (ProductTypeClass IS NULL OR ProductTypeClass = 3)
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)
END