
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AM de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateAMFileData] 
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
		HealthEntityCode VARCHAR(50), 
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		IdentificationNumber VARCHAR(50), 
		AuthorizationNumber VARCHAR(50), 
		Code VARCHAR(50), 
		CodeCUM VARCHAR(50), 
		CodeAlternative VARCHAR(50), 
		CodeAlternativeTwo VARCHAR(50), 
		ProductId INT, 
		MedicationType INT, 
		DrugName VARCHAR(300), 
		PharmaceuticalForm VARCHAR(300), 
		Concentration VARCHAR(50), 
		UnitMeasure VARCHAR(50), 
		InvoicedQuantity INT, 
		TotalSalesPrice DECIMAL(18,0), 
		GrandTotalSalesPrice DECIMAL(18,0),

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
			SELECT SOD.Id, I.Id,
					xi.InvoiceNumber AS InvoiceNumber, 
					ha.HealthEntityCode AS HealthEntityCode, 
					ca.CODIPSSEC AS IPSCode, 
					TIP.SIGLA AS IdentificationTypeCode, 
					RTRIM(pacient.IPCODPACI) AS IdentificationNumber, 
					RTRIM(ISNULL(sod.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) AS AuthorizationNumber, 
					cups.RIPSCode as Code, 
					ISNULL(cups.Code, '') as CodeCUM, 
					ips.Code as CodeAlternative, 
					cups.RIPSCode as CodeAlternativeTwo, 
					cups.Id as ProductId, 
					CASE cups.RIPSConcept 
						WHEN '12' THEN 1 
						WHEN '13' THEN 1 
						ELSE 2 
					END as MedicationType, 
					cups.Description as DrugName, 
					'GAS MEDICINAL' as PharmaceuticalForm, 
					'35%' as Concentration, 
					'LT X MIN' as UnitMeasure, 
					ID.InvoicedQuantity, 
					CAST(ROUND((ID.GrandTotalSalesPrice) / sod.InvoicedQuantity, 0) AS NUMERIC(18,2)) as TotalSalesPrice, 
					(ID.GrandTotalSalesPrice) AS GrandTotalSalesPrice,
					NULL As ProductTypeClass,
					cups.RIPSConcept
			FROM Billing.Invoice i WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.Id 
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON ID.ServiceOrderDetailId = sod.Id 
			JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
			JOIN [Contract].IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id 
			JOIN [Contract].CUPSEntity cups WITH (NOLOCK) ON cups.Id = sod.CUPSEntityId 
			JOIN [Contract].HealthAdministrator ha WITH (NOLOCK) ON I.HealthAdministratorId = ha.Id 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI 
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
			WHERE sod.InvoicedQuantity > 0 
				AND I.[Status] = 1 
				AND sod.SettlementType <> 3 
				AND sod.IsDelete = 0 
				AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept in ('12','13'))
		UNION ALL
			SELECT SOD.Id, I.Id,
					xi.InvoiceNumber AS InvoiceNumber, 
					ha.HealthEntityCode AS HealthEntityCode, 
					ca.CODIPSSEC AS IPSCode, 
					TIP.SIGLA AS IdentificationTypeCode,--- ''''''''
					RTRIM(pacient.IPCODPACI) AS IdentificationNumber, 
					RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber,  
					IP.Code, 
					ISNULL(IP.CodeCUM, '') CodeCUM, 
					IP.CodeAlternative, 
					IP.CodeAlternativeTwo, 
					sod.ProductId, 
					CASE ISNULL(ATC.POSProduct, IP.POSProduct) WHEN 1 THEN 1 ELSE 2 END as MedicationType, 
					DCI.Name as DrugName, 
					ISNULL(PF.Name, '') as PharmaceuticalForm, 
					ISNULL(ATC.Concentration, '') as Concentration, 
					CASE ATC.FormulationType
						WHEN 1 THEN ISNULL(IMUW.Abbreviation, 'NA')     
						WHEN 2 THEN ISNULL(IMUV.Abbreviation, 'NA')     
						WHEN 3 THEN ISNULL(IMUW.Abbreviation, 'NA') + ' ' + ISNULL(IMUV.Abbreviation, 'NA')
						WHEN 4 THEN ISNULL(IMUA.Abbreviation,'NA')      
						ELSE 'NA'    
					END AS UnitMeasure, 
					sod.InvoicedQuantity, 
					CAST(ROUND((ID.GrandTotalSalesPrice) / sod.InvoicedQuantity, 2) AS NUMERIC(18,2)) as TotalSalesPrice, 
					(ID.GrandTotalSalesPrice) AS GrandTotalSalesPrice,
					PT.Class As ProductTypeClass,
					NULL AS RIPSConcept
			FROM Billing.Invoice i WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.ID 
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON ID.ServiceOrderDetailId = sod.Id 
			JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
			JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sod.ProductId = ip.id 
			JOIN Inventory.ProductType PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id 			
			JOIN [Contract].HealthAdministrator ha WITH (NOLOCK) ON I.HealthAdministratorId = ha.Id 
			JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON I.RevenueControlDetailId = RCD.Id 
			JOIN dbo.INPACIENT pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.ADCENATEN AS ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
			-----------------------------------------------------------------------------------------------------------
			LEFT JOIN Inventory.ATC as ATC WITH (NOLOCK) ON IP.ATCId = ATC.id 
			LEFT JOIN Inventory.DCI as DCI WITH (NOLOCK) ON atc.DCIId = dci.Id 
			LEFT JOIN Inventory.PharmaceuticalForm AS PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId = PF.Id 
			LEFT JOIN Inventory.InventoryMeasurementUnit IMUW WITH (NOLOCK) ON ATC.WeightMeasureUnit = IMUW.Id 
			LEFT JOIN Inventory.InventoryMeasurementUnit IMUV WITH (NOLOCK) ON ATC.VolumeMeasureUnit = IMUV.Id 
			LEFT JOIN Inventory.InventoryMeasurementUnit IMUA WITH (NOLOCK) ON ATC.AdministrationUnitId = IMUA.Id 
			WHERE sod.InvoicedQuantity > 0 
				AND	I.[Status] = 1 
				AND sod.SettlementType <>  IIF(@PackageDetail = 0,3,0)   
				AND sod.IsDelete = 0 
				AND ((@PackageDetail = 1 AND sod.Packaging = 1) OR PT.Class = 2)
				
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados

	if @PackageDetail = 1 Begin
		Insert Into @TableResult
		SELECT SOD.Id, tr.InvoiceId
			, tr.InvoiceNumber
			, tr.HealthEntityCode
			, ca.CODIPSSEC AS IPSCode
			, tr.IdentificationTypeCode
			, tr.IdentificationNumber
			, RTRIM(ISNULL(sod.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) AS AuthorizationNumber
			, cups.RIPSCode as Code
			, ISNULL(cups.Code, '') as CodeCUM
			, ips.Code as CodeAlternative
			, cups.RIPSCode as CodeAlternativeTwo
			, cups.Id as ProductId
			, CASE cups.RIPSConcept 
				WHEN '12' THEN 1 
				WHEN '13' THEN 1 
				ELSE 2 
			END as MedicationType
			, cups.Description AS DrugName
			, tr.PharmaceuticalForm
			, tr.Concentration
			, tr.UnitMeasure
			, SOD.InvoicedQuantity
			, 0 AS TotalSalesPrice
			, 0 AS GrandTotalSalesPrice
			, NULL As ProductTypeClass
			, cups.RIPSConcept
		FROM @TableResult tr
		JOIN Billing.ServiceOrderDetail SOD WITH(NOLOCK) ON SOD.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN [Contract].IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id 
		JOIN [Contract].CUPSEntity cups WITH (NOLOCK) ON cups.Id = sod.CUPSEntityId 
		JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
		JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES 
		JOIN dbo.[ADCENATEN] AS ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
		WHERE SOD.CUPSEntityId IS NOT NULL
			AND sod.InvoicedQuantity > 0
			AND sod.SettlementType <> 3 
			AND sod.IsDelete = 0 
			AND cups.RIPSConcept in ('12','13')
		UNION ALL
		SELECT SOD.Id, tr.InvoiceId
			, tr.InvoiceNumber
			, tr.HealthEntityCode
			, ca.CODIPSSEC AS IPSCode
			, tr.IdentificationTypeCode
			, tr.IdentificationNumber
			, RTRIM(ISNULL(sod.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) AS AuthorizationNumber
			, IP.Code as Code
			, ISNULL(IP.CodeCUM, '') CodeCUM
			, IP.CodeAlternative
			, IP.CodeAlternativeTwo
			, sod.ProductId
			, CASE ISNULL(ATC.POSProduct, IP.POSProduct) WHEN 1 THEN 1 ELSE 2 END as MedicationType
			, DCI.Name as DrugName
			, ISNULL(PF.Name, '') AS PharmaceuticalForm
			, ISNULL(ATC.Concentration, '') as Concentration
			, CASE ATC.FormulationType
					WHEN 1 THEN ISNULL(IMUW.Abbreviation, 'NA')     
					WHEN 2 THEN ISNULL(IMUV.Abbreviation, 'NA')     
					WHEN 3 THEN ISNULL(IMUW.Abbreviation, 'NA') + ' ' + ISNULL(IMUV.Abbreviation, 'NA')
					WHEN 4 THEN ISNULL(IMUA.Abbreviation,'NA')      
					ELSE 'NA'    
				END AS UnitMeasure
			, SOD.InvoicedQuantity
			, 0 AS TotalSalesPrice
			, 0 AS GrandTotalSalesPrice
			, PT.Class AS ProductTypeClass
			, NULL AS RIPSConcept
		FROM @TableResult tr
		JOIN Billing.ServiceOrderDetail SOD WITH(NOLOCK) ON SOD.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
		JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES 
		JOIN dbo.[ADCENATEN] AS ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
		JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sod.ProductId = ip.id 
		JOIN Inventory.ProductType PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id 	
		-----------------------------------------------------------------------------------------------------------
		LEFT JOIN Inventory.ATC as ATC WITH (NOLOCK) ON IP.ATCId = ATC.id 
		LEFT JOIN Inventory.DCI as DCI WITH (NOLOCK) ON atc.DCIId = dci.Id 
		LEFT JOIN Inventory.PharmaceuticalForm AS PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId = PF.Id 
		LEFT JOIN Inventory.InventoryMeasurementUnit IMUW WITH (NOLOCK) ON ATC.WeightMeasureUnit = IMUW.Id 
		LEFT JOIN Inventory.InventoryMeasurementUnit IMUV WITH (NOLOCK) ON ATC.VolumeMeasureUnit = IMUV.Id 
		LEFT JOIN Inventory.InventoryMeasurementUnit IMUA WITH (NOLOCK) ON ATC.AdministrationUnitId = IMUA.Id 
		WHERE SOD.ProductId IS NOT NULL
			AND sod.InvoicedQuantity > 0 
			AND sod.SettlementType <>  IIF(@PackageDetail = 0,3,0)   
			AND sod.IsDelete = 0 
			AND PT.Class = 2
	End
	
	SELECT	InvoiceNumber, HealthEntityCode, IPSCode, IdentificationTypeCode, IdentificationNumber, AuthorizationNumber, 
			Code, CodeCUM, CodeAlternative, CodeAlternativeTwo, ProductId, MedicationType, DrugName, PharmaceuticalForm, 
			Concentration, UnitMeasure, SUM(InvoicedQuantity) InvoicedQuantity, TotalSalesPrice, SUM(GrandTotalSalesPrice) GrandTotalSalesPrice
	FROM @TableResult
	WHERE (RIPSConcept IS NULL OR RIPSConcept IN ('12','13')) AND (ProductTypeClass IS NULL OR ProductTypeClass = 2)
	GROUP BY InvoiceNumber, HealthEntityCode, IPSCode, IdentificationTypeCode, IdentificationNumber, AuthorizationNumber, 
			Code, CodeCUM, CodeAlternative, CodeAlternativeTwo, ProductId, MedicationType, DrugName, PharmaceuticalForm, 
			Concentration, UnitMeasure, TotalSalesPrice
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)

END