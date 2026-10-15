-- =============================================
-- Author:		Miguel Fonseca
-- Create date: 2020-02-24
-- Description:	SP que genera la informacion para el XML FormatoArchiveFT025
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT025]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Year INT,
			@Month INT,
			---------------------------------------------------------------------------------------
			@InitialDate DATE,
			@EndDate DATE,
			@BusinessLine tinyint
	
	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		-------------------------------------------------------------------------------------------

		SET @InitialDate = DATEFROMPARTS(@Year, 1, 1)
		SET @EndDate = EOMONTH(DATEFROMPARTS(@Year,@Month,1))
		SET @BusinessLine = (SELECT cs.BusinessLine FROM GeneralLedger.CompanySettings cs)

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	tp.Nit ThirdPartyNit, 
				tp.Name ThirdPartyName,
				CASE ar.EntityType
					WHEN 1 THEN 1 -- EPS Contributivo
					WHEN 2 THEN 2 -- EPS Subsidiado
					WHEN 3 THEN 5 -- ET Vinculados Municipios
					WHEN 4 THEN 5 -- ET Vinculados Departamentos
					WHEN 5 THEN 8 -- ARL Riesgos Laborales
					WHEN 6 THEN 3 -- MP Medicina Prepagada
					WHEN 9 THEN 4 -- Regimen Especial
					WHEN 10 THEN 9 -- Accidentes de transito
					WHEN 11 THEN 4 -- Fosyga
					WHEN 12 THEN 4 -- Otros
					WHEN 13 THEN 7 -- Aseguradoras
					ELSE 0
				END EntityType,
				CASE ar.LiquidationType
					WHEN 1 THEN 2 -- Pago por Servicios
					WHEN 2 THEN 1 -- Capitacion
					WHEN 3 THEN 3 -- Factura Global
					WHEN 4 THEN 1 -- Capitacion Global
					WHEN 5 THEN 3 --  Global Prospectivo - PGP
					ELSE 6
				END LiquidationType,
				IIF(ar.LiquidationType IN (1,2,3,4,5), 'NA', 'Otro Tipo de Contratación') ContractType,
				SUM(ar.InvoiceValue) InvoiceValue,
				SUM(ar.CollectionValue) CollectionValue,
				@BusinessLine BusinessLine
		FROM
		(
			SELECT	ar.ThirdPartyId,
					ISNULL(cg.EntityType, 0) EntityType,
					ISNULL(cg.LiquidationType, 0) LiquidationType,
					ar.Value InvoiceValue,
					0 CollectionValue
			FROM Portfolio.RadicateInvoiceC ri
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId AND rid.Devolution = 0
			JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
			WHERE ri.State IN ('2')
				AND CAST(ri.ConfirmDate AS DATE) BETWEEN @InitialDate AND @EndDate 
		UNION ALL
			SELECT	ar.ThirdPartyId,
					ISNULL(cg.EntityType, 0) EntityType,
					ISNULL(cg.LiquidationType, 0) LiquidationType,
					0 InvoiceValue,
					ISNULL(pt.TransferValue, 0) + ISNULL(cr.CashReceiptValue, 0) +
					IIF(CAST(ri.ConfirmDate AS DATE) BETWEEN @InitialDate AND @EndDate, ISNULL(pt.PreviousTransferValue, 0) + ISNULL(cr.PreviousCashReceiptValue, 0), 0) CollectionValue
			FROM Portfolio.RadicateInvoiceC ri
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId AND rid.Devolution = 0
			JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
			LEFT JOIN
			(
				SELECT 
					ptd.AccountReceivableId,
					SUM(IIF(CAST(pt.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, 0, ptd.Value)) PreviousTransferValue,
					SUM(IIF(CAST(pt.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, ptd.Value, 0)) TransferValue				
				FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
				JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PortfolioTrasferId
				WHERE pt.Status IN (2, 4) 
					AND CAST(pt.DocumentDate AS DATE) <= @EndDate
					AND CAST(ISNULL(pt.RecersalDate, DATEADD(DAY, 1, @EndDate)) AS DATE) > @EndDate
				GROUP BY ptd.AccountReceivableId
			) pt ON ar.Id = pt.AccountReceivableId
			LEFT JOIN
			(
				SELECT 
					crar.AccountReceivableId,
					SUM(IIF(CAST(cr.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, 0, crar.Value)) PreviousCashReceiptValue,
					SUM(IIF(CAST(cr.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, crar.Value, 0)) CashReceiptValue
				FROM Treasury.CashReceipts cr WITH (NOLOCK)
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK) ON crd.Id = crar.CashReceiptDetailId
				WHERE cr.Status IN (2 , 4)
					AND CAST(cr.DocumentDate AS DATE) <= @EndDate
					AND CAST(ISNULL(cr.ReversedDate, DATEADD(DAY, 1, @EndDate)) AS DATE) > @EndDate
				GROUP BY crar.AccountReceivableId
			) cr ON ar.Id = cr.AccountReceivableId
			WHERE ri.State IN ('2')
				AND CAST(ri.ConfirmDate AS DATE) <= @EndDate
				AND (ISNULL(pt.TransferValue, 0) + ISNULL(pt.PreviousTransferValue, 0) + ISNULL(cr.CashReceiptValue, 0) + ISNULL(cr.PreviousCashReceiptValue, 0)) > 0
		) ar
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
		WHERE ar.EntityType NOT IN (7,8,99)
		GROUP BY tp.Nit, tp.Name, ar.EntityType, ar.LiquidationType
		HAVING
            SUM(ar.InvoiceValue) <> 0
            OR SUM(ar.CollectionValue) <> 0;
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END