-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AF de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateAFFileData] 
	@RadicateInvoiceId AS INT,
	@XmlInvoices AS XML
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
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		InvoiceNumber VARCHAR(50), 
		AdmissionDate VARCHAR(50), 
		InvoiceDate VARCHAR(50), 
		OutputDate VARCHAR(50), 
		HealthEntityCode VARCHAR(50), 
		ThirdPartyName VARCHAR(300), 
		ContractNumber VARCHAR(100), 
		CareGroupName VARCHAR(100), 
		PolicyNumber VARCHAR(50), 
		TotalPatientSalesPrice DECIMAL(18,0),
		CommissionValue DECIMAL(18,0), 
		PatientDiscount DECIMAL(18,0), 
		ThirdPartySalesValue DECIMAL(18,0)
	)

	BEGIN TRY

		INSERT INTO @Invoices (InvoiceId, InvoiceNumber)
			SELECT DISTINCT
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM @XmlInvoices.nodes('/Data') t(x)
			JOIN Billing.Invoice i ON t.x.value('InvoiceId[1]','int') = i.Id
			--WHERE i.DocumentType <> 5
		UNION ALL
			SELECT DISTINCT 
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber
			WHERE ri.Id = @RadicateInvoiceId
				AND rid.State <> 4
				--AND i.DocumentType <> 5

		/*******************************************************************************************/

		INSERT INTO @TableResult 
			SELECT DISTINCT 
				RTRIM(center.CODIPSSEC) AS IPSCode, 
				'NI' AS IdentificationType, 
				xi.InvoiceNumber AS InvoiceNumber, 
				CONVERT(VARCHAR(10),admission.IFECHAING,103) AS AdmissionDate, 
				CONVERT(VARCHAR(10),i.InvoiceDate,103) AS InvoiceDate, 
				CONVERT(VARCHAR(10),IIF(i.OutputDate > [Common].[GETDATE](), [Common].[GETDATE](), i.OutputDate),103) AS OutputDate, 
				ha.HealthEntityCode AS HealthEntityCode, 
				tp.Name AS ThirdPartyName, 
				ISNULL(c.ContractNumber,'SINCONTRATO') AS ContractNumber, 
				cg.Name AS CareGroupName, 
				''  AS PolicyNumber, 
				i.TotalPatientSalesPrice AS TotalPatientSalesPrice, 
				0 AS CommissionValue, 
				i.ThirdPartyDiscountValue AS PatientDiscount, 
				ROUND(i.ThirdPartySalesValue,0) AS ThirdPartySalesValue
			FROM Billing.RevenueControlDetail rcd WITH (NOLOCK) 
			JOIN Billing.Invoice i WITH (NOLOCK) ON i.RevenueControlDetailId = rcd.Id 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN [Contract].HealthAdministrator ha WITH (NOLOCK) ON i.HealthAdministratorId = ha.Id 
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON i.ThirdPartyId = tp.ID 
			JOIN [Contract].CareGroup cg WITH (NOLOCK) ON I.CareGroupId = cg.ID 
			JOIN .ADINGRESO admission WITH (NOLOCK) ON i.AdmissionNumber = admission.NUMINGRES 
			JOIN .ADCENATEN center WITH (NOLOCK) ON admission.CODCENATE = center.CODCENATE 
			LEFT JOIN [Contract].[Contract] c WITH (NOLOCK) ON cg.ContractId = c.ID 
			WHERE i.Status = 1
				AND i.DocumentType <> 4
		UNION ALL
			SELECT DISTINCT 
				RTRIM(ou.IPSCode) AS IPSCode, 
				'NI' AS IdentificationType, 
				i.InvoiceNumber AS InvoiceNumber, 
				CONVERT(VARCHAR(10),i.CapitationInitialDate,103) AS AdmissionDate, 
				CONVERT(VARCHAR(10),i.InvoiceDate,103) AS InvoiceDate, 
				CONVERT(VARCHAR(10),i.CapitationEndDate,103) AS OutputDate, 
				ha.HealthEntityCode AS HealthEntityCode, 
				tp.Name AS ThirdPartyName, 
				ISNULL(c.ContractNumber,'SINCONTRATO') AS ContractNumber, 
				cg.Name AS CareGroupName, 
				''  AS PolicyNumber, 
				0 AS TotalPatientSalesPrice, 
				0 AS CommissionValue, 
				0 AS PatientDiscount, 
				ROUND(i.ThirdPartySalesValue,0) as ThirdPartySalesValue
			FROM Billing.Invoice i WITH (NOLOCK)
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN Common.OperatingUnit ou WITH (NOLOCK) ON i.OperatingUnitId = ou.Id
			JOIN [Contract].HealthAdministrator ha WITH (NOLOCK) ON i.HealthAdministratorId = ha.Id 
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON i.ThirdPartyId = tp.ID 
			JOIN [Contract].CareGroup cg WITH (NOLOCK) ON I.CareGroupId = cg.ID 
			LEFT JOIN [Contract].[Contract] c WITH (NOLOCK) ON cg.ContractId = c.ID 
			WHERE i.Status = 1
				AND i.DocumentType = 4
				
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * 
	FROM @TableResult
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)
END