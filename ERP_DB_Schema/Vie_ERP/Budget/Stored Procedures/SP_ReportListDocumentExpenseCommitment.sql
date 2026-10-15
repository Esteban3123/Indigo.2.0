-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de compromisos del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseCommitment]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@CommitmentCode VARCHAR(20),
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
				@CommitmentCode = t.x.value('CommitmentCode[1]','varchar(20)'),
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
				c.Code,
				c.DocumentDate,
				cd.ExpiredDate ExpirationDate,
				c.Document,
				c.Observations,
				CASE c.CommitmentType
					WHEN 1 THEN 'Compromiso' 
					WHEN 2 THEN 'Reserva' 
					ELSE 'N/A'
				END CommitmentTypeName,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE c.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				c.EntityCode OriginCode,
				ISNULL(gend.Description, c.EntityName) OriginName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				cd.InitialValue,
				ISNULL(cm.DebitValue, 0) DebitValue,
				ISNULL(cm.CreditValue, 0) CreditValue,
				cd.InitialValue - ISNULL(cm.DebitValue, 0) + ISNULL(cm.CreditValue, 0) TotalValue,
				ISNULL(o.ExecutedValue, 0) ExecutedValue,
				cd.InitialValue - ISNULL(cm.DebitValue, 0) + ISNULL(cm.CreditValue, 0) - ISNULL(o.ExecutedValue, 0) Balance
		FROM Budget.Commitment c WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON c.ThirdPartyId = tp.Id
		JOIN Budget.CommitmentDetail cd WITH (NOLOCK) ON c.Id = cd.CommitmentId
		JOIN Budget.Category cat WITH (NOLOCK) ON cd.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON cd.RevenueTypeId = rt.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON c.EntityName = gend.EntityName
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT	cmd.CommitmentDetailId, SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue, SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
			FROM Budget.CommitmentModification cm WITH (NOLOCK)
			JOIN Budget.CommitmentModificationDetail cmd WITH (NOLOCK) ON cm.Id = cmd.CommitmentModificationId
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY cmd.CommitmentDetailId
		) cm ON cd.Id = cm.CommitmentDetailId 
		/*************************************  OBLIGACIONES *************************************/
		LEFT JOIN 
		(
			SELECT	od.CommitmentDetailId, SUM(od.InitialValue + ISNULL(om.CreditValue, 0) - ISNULL(om.DebitValue, 0)) ExecutedValue
			FROM Budget.Obligation o WITH (NOLOCK)
			JOIN Budget.ObligationDetail od WITH (NOLOCK) ON o.Id = od.ObligationId
			LEFT JOIN 
			(
				SELECT omd.ObligationDetailId, SUM(IIF(omd.Nature = 1, omd.Value, 0)) DebitValue, SUM(IIF(omd.Nature = 1, 0, omd.Value)) CreditValue
				FROM Budget.ObligationModification om WITH (NOLOCK)
				JOIN Budget.ObligationModificationDetail omd WITH (NOLOCK) ON om.Id = omd.ObligationModificationId
				WHERE om.Status = 2 AND CAST(om.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY omd.ObligationDetailId
			) om ON od.Id = om.ObligationDetailId
			WHERE o.Status = 2 AND CAST(o.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY od.CommitmentDetailId
		) o ON cd.Id = o.CommitmentDetailId 
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(c.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@CommitmentCode, '') = '' OR c.Code = @CommitmentCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY c.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END