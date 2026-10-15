-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de ordenes de pago del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpensePaymentOrder]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@PaymentOrderCode VARCHAR(20),
			@ThirdParties VARCHAR(MAX),
			-------------
			@FilterByThirdParties BIT = 0

	DECLARE @Table_ThirdParties AS TABLE(Id INT)

	BEGIN TRY
		
		/*************************************** CRITERIOS ***************************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint'),
				@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@PaymentOrderCode = t.x.value('PaymentOrderCode[1]','varchar(20)'),
				@ThirdParties = t.x.value('ThirdParties[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		IF ISNULL(@ThirdParties, '') <> ''
		BEGIN
			SET @FilterByThirdParties = 1

			INSERT INTO @Table_ThirdParties
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@ThirdParties, ',')
		END

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	CASE @GroupBy
					WHEN 1 THEN tp.Id
					WHEN 2 THEN cat.Id
					WHEN 3 THEN fs.Id
					WHEN 4 THEN rt.Id
					ELSE 0
				END GroupId,
				CASE @GroupBy
					WHEN 1 THEN tp.Nit + ' - ' + tp.Name
					WHEN 2 THEN cat.Code + ' - ' + cat.Name
					WHEN 3 THEN fs.Code + ' - ' + fs.Name
					WHEN 4 THEN rt.Code + ' - ' + rt.Name
					ELSE ''
				END GroupName,
				po.Code,
				po.DocumentDate,
				pod.ExpiredDate ExpirationDate,
				po.Document,
				po.Observations,
				CASE po.PaymentOrderType
					WHEN 1 THEN 'Orden de Pago' 
					ELSE 'N/A'
				END PaymentOrderTypeName,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE po.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				po.EntityCode OriginCode,
				ISNULL(gend.Description, po.EntityName) OriginName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				pod.InitialValue,
				ISNULL(rr.DebitValue, 0) DebitValue,
				ISNULL(rr.CreditValue, 0) CreditValue,
				pod.InitialValue - ISNULL(rr.DebitValue, 0) + ISNULL(rr.CreditValue, 0) TotalValue
		FROM Budget.PaymentOrder po WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON po.ThirdPartyId = tp.Id
		JOIN Budget.PaymentOrderDetail pod WITH (NOLOCK) ON po.Id = pod.PaymentOrderId
		JOIN Budget.ObligationDetail od WITH (NOLOCK) ON pod.ObligationDetailId = od.Id
		JOIN Budget.Category cat WITH (NOLOCK) ON od.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON od.RevenueTypeId = rt.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON po.EntityName = gend.EntityName
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT rrd.PaymentOrderDetailId, SUM(rrd.Value) DebitValue, 0 CreditValue
			FROM Budget.ReimbursementResource rr WITH (NOLOCK)
			JOIN Budget.ReimbursementResourceDetaill rrd WITH (NOLOCK) ON rr.Id = rrd.ReimbursementResourceId
			WHERE rr.Status = 2 AND CAST(rr.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY rrd.PaymentOrderDetailId
		) rr ON pod.Id = rr.PaymentOrderDetailId 
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(po.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@PaymentOrderCode, '') = '' OR po.Code = @PaymentOrderCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY po.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END