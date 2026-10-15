-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de obligaciones del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseObligation]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@ObligationCode VARCHAR(20),
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
				@ObligationCode = t.x.value('ObligationCode[1]','varchar(20)'),
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
				o.Code,
				o.DocumentDate,
				od.ExpiredDate ExpirationDate,
				o.Document,
				o.Observations,
				CASE o.ObligationType
					WHEN 1 THEN 'Obligación' 
					WHEN 2 THEN 'Cuenta por Pagar' 
					ELSE 'N/A'
				END ObligationTypeName,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE o.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				od.EntityCode OriginCode,
				ISNULL(gend.Description, od.EntityName) OriginName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				od.InitialValue,
				ISNULL(om.DebitValue, 0) DebitValue,
				ISNULL(om.CreditValue, 0) CreditValue,
				od.InitialValue - ISNULL(om.DebitValue, 0) + ISNULL(om.CreditValue, 0) TotalValue,
				ISNULL(po.ExecutedValue, 0) ExecutedValue,
				od.InitialValue - ISNULL(om.DebitValue, 0) + ISNULL(om.CreditValue, 0) - ISNULL(po.ExecutedValue, 0) Balance
		FROM Budget.Obligation o WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON o.ThirdPartyId = tp.Id
		JOIN Budget.ObligationDetail od WITH (NOLOCK) ON o.Id = od.ObligationId
		JOIN Budget.Category cat WITH (NOLOCK) ON od.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON od.RevenueTypeId = rt.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON od.EntityName = gend.EntityName
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT	omd.ObligationDetailId, SUM(IIF(omd.Nature = 1, omd.Value, 0)) DebitValue, SUM(IIF(omd.Nature = 1, 0, omd.Value)) CreditValue
			FROM Budget.ObligationModification om WITH (NOLOCK)
			JOIN Budget.ObligationModificationDetail omd WITH (NOLOCK) ON om.Id = omd.ObligationModificationId
			WHERE om.Status = 2 AND CAST(om.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY omd.ObligationDetailId
		) om ON od.Id = om.ObligationDetailId 
		/************************************ ORDENES DE PAGO ************************************/
		LEFT JOIN 
		(
			SELECT	pod.ObligationDetailId, SUM(pod.InitialValue + ISNULL(rr.CreditValue, 0) - ISNULL(rr.DebitValue, 0)) ExecutedValue
			FROM Budget.PaymentOrder po WITH (NOLOCK)
			JOIN Budget.PaymentOrderDetail pod WITH (NOLOCK) ON po.Id = pod.PaymentOrderId
			LEFT JOIN 
			(
				SELECT rrd.PaymentOrderDetailId, SUM(rrd.Value) DebitValue, 0 CreditValue
				FROM Budget.ReimbursementResource rr WITH (NOLOCK)
				JOIN Budget.ReimbursementResourceDetaill rrd WITH (NOLOCK) ON rr.Id = rrd.ReimbursementResourceId
				WHERE rr.Status = 2 AND CAST(rr.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY rrd.PaymentOrderDetailId
			) rr ON pod.Id = rr.PaymentOrderDetailId
			WHERE po.Status = 2 AND CAST(po.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY pod.ObligationDetailId
		) po ON od.Id = po.ObligationDetailId 
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(o.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@ObligationCode, '') = '' OR o.Code = @ObligationCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY o.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END