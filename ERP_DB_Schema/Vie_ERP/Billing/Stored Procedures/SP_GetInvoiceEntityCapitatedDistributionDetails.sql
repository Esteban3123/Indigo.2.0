-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-15
-- Description:	Procedimiento que se encarga de obtener los controles de una distribución de Factura Monto Fijo
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetInvoiceEntityCapitatedDistributionDetails]
	@InvoiceEntityCapitatedId AS INT,
	@InvoiceEntityCapitatedDistributionId AS INT
AS
BEGIN

	DECLARE @DistributionStatus TINYINT = 1

	SELECT @DistributionStatus = iecd.Status
	FROM Billing.InvoiceEntityCapitatedDistribution iecd
	WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId
	
	SELECT DISTINCT
		iecdd.Id AS Id,
		iecdd.InvoiceEntityCapitatedDistributionId,
		i.Id AS InvoiceId,
		i.HealthAdministratorId AS HealthAdministratorId,
		CONCAT(ha.Code, ' - ', ha.Name) AS HealthAdministratorName,
		i.InvoiceCategoryId,
		IIF(ic.Id IS NULL, '', CONCAT(ic.Code, ' - ', ic.Name)) AS InvoiceCategoryName,
		i.InvoiceNumber,
		i.InvoiceDate,
		i.ThirdPartySalesValue AS InvoiceValue,
		i.Status,
		i.AdmissionNumber
	FROM Billing.InvoiceEntityCapitated iec
	JOIN Billing.Invoice i ON i.DocumentType = 5 
		AND iec.CareGroupId = i.CareGroupId 
		AND CAST(i.InvoiceDate AS DATE) BETWEEN iec.InitialDate AND iec.EndDate
	JOIN Contract.HealthAdministrator ha ON i.HealthAdministratorId = ha.Id
	LEFT JOIN Billing.InvoiceCategories ic ON i.InvoiceCategoryId = ic.Id
	LEFT JOIN Billing.InvoiceEntityCapitatedDistributionDetail iecdd ON i.Id = iecdd.InvoiceId 
		AND iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId
	-- NO MOSTRAR LOS QUE YA ESTEN EN OTRA DISTRIBUCION --
	LEFT JOIN 
	(
		SELECT iecdi.Id, iecddi.InvoiceId
		FROM Billing.InvoiceEntityCapitatedDistribution iecdi
		JOIN Billing.InvoiceEntityCapitatedDistributionDetail iecddi ON iecdi.Status IN (1, 2)
			AND iecdi.Id = iecddi.InvoiceEntityCapitatedDistributionId	
	) iecd ON i.Id = iecd.InvoiceId
	WHERE iec.Id = @InvoiceEntityCapitatedId 		
		AND (iecd.InvoiceId IS NULL OR iecd.Id = @InvoiceEntityCapitatedDistributionId)
		AND 
		(
			(@DistributionStatus = 1 AND i.Status = 1)
			OR 
			(@DistributionStatus <> 1 AND iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId)
		)

END