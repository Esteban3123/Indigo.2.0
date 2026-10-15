-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de disponibilidades del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseAvailability]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@AvailabilityCode VARCHAR(20)

	BEGIN TRY
		
		/*************************************** CRITERIOS ***************************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint'),
				@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@AvailabilityCode = t.x.value('AvailabilityCode[1]','varchar(20)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	CASE @GroupBy
					WHEN 2 THEN cat.Id
					WHEN 3 THEN fs.Id
					WHEN 4 THEN rt.Id
					ELSE 0
				END GroupId,
				CASE @GroupBy
					WHEN 2 THEN cat.Code + ' - ' + cat.Name
					WHEN 3 THEN fs.Code + ' - ' + fs.Name
					WHEN 4 THEN rt.Code + ' - ' + rt.Name
					ELSE ''
				END GroupName,
				a.Code,
				a.DocumentDate,
				a.ExpirationDate,
				a.Observations,
				CASE a.AvailabilityType 
					WHEN 1 THEN 'Ninguno' 
					WHEN 2 THEN 'Disponibilidad' 
					WHEN 3 THEN 'Vigencia Factura'
					ELSE 'N/A'
				END AvailabilityTypeName,
				CASE a.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,				
				ad.InitialValue,
				ISNULL(am.DebitValue, 0) DebitValue,
				ISNULL(am.CreditValue, 0) CreditValue,
				ad.InitialValue - ISNULL(am.DebitValue, 0) + ISNULL(am.CreditValue, 0) TotalValue,
				ISNULL(cn.ExecutedValue, 0) ExecutedValue,
				ad.InitialValue - ISNULL(am.DebitValue, 0) + ISNULL(am.CreditValue, 0) - ISNULL(cn.ExecutedValue, 0) Balance
		FROM Budget.Availability a WITH (NOLOCK)
		JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON a.Id = ad.AvailabilityId
		JOIN Budget.Budget b WITH (NOLOCK) ON ad.BudgetId = b.Id
		JOIN Budget.Category cat WITH (NOLOCK) ON b.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON b.RevenueTypeId = rt.Id
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT	amd.AvailabilityDetailId, SUM(IIF(amd.Nature = 1, amd.Value, 0)) DebitValue, SUM(IIF(amd.Nature = 1, 0, amd.Value)) CreditValue
			FROM Budget.AvailabilityModification am WITH (NOLOCK)
			JOIN Budget.AvailabilityModificationDetail amd WITH (NOLOCK) ON am.Id = amd.AvailabilityModificationId
			WHERE am.Status = 2 AND CAST(am.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY amd.AvailabilityDetailId
		) am ON ad.Id = am.AvailabilityDetailId 
		/************************************** COMPROMISOS **************************************/
		LEFT JOIN 
		(
			SELECT	cd.AvailabilityDetailId, SUM(cd.InitialValue + ISNULL(cm.CreditValue, 0) - ISNULL(cm.DebitValue, 0)) ExecutedValue
			FROM Budget.Commitment c WITH (NOLOCK)
			JOIN Budget.CommitmentDetail cd WITH (NOLOCK) ON c.Id = cd.CommitmentId
			LEFT JOIN 
			(
				SELECT cmd.CommitmentDetailId, SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue, SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
				FROM Budget.CommitmentModification cm WITH (NOLOCK)
				JOIN Budget.CommitmentModificationDetail cmd WITH (NOLOCK) ON cm.Id = cmd.CommitmentModificationId
				WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY cmd.CommitmentDetailId
			) cm ON cd.Id = cm.CommitmentDetailId
			WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY cd.AvailabilityDetailId
		) cn ON ad.Id = cn.AvailabilityDetailId 
		WHERE CAST(a.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@AvailabilityCode, '') = '' OR a.Code = @AvailabilityCode)
		ORDER BY a.Id
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END