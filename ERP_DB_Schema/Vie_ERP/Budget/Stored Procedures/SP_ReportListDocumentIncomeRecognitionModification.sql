-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-30
-- Description:	Procedimiento para el reporte de listado de modificación de reconocimientos del presupuesto de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentIncomeRecognitionModification]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@RecognitionModificationCode VARCHAR(20),
			@ThirdParties VARCHAR(MAX),
			-------------
			@FilterByThirdParties BIT = 0

	DECLARE @Table_ThirdParties AS TABLE(Id INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint'),
				@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@RecognitionModificationCode = t.x.value('RecognitionModificationCode[1]','varchar(20)'),
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
					WHEN 2 THEN c.Id
					WHEN 3 THEN fs.Id
					WHEN 4 THEN rt.Id
					ELSE 0
				END GroupId,
				CASE @GroupBy
					WHEN 1 THEN tp.Nit + ' - ' + tp.Name
					WHEN 2 THEN c.Code + ' - ' + c.Name
					WHEN 3 THEN fs.Code + ' - ' + fs.Name
					WHEN 4 THEN rt.Code + ' - ' + rt.Name
					ELSE ''
				END GroupName,
				rm.Code,
				rm.DocumentDate,
				rm.Document,
				rm.Observations,
				r.Code RecognitionCode,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE rm.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				rm.EntityCode OriginCode,
				ISNULL(gend.Description, rm.EntityName) OriginName,
				c.Code CategoryCode,
				c.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				CASE rmd.Nature
					WHEN 1 THEN '(-) Débito'
					WHEN 2 THEN '(+) Crédito'
					ELSE 'N/A'
				END NatureName,
				rmd.Value,
				rd.TotalRecognition RecognitionTotal,
				rd.Balance RecognitionBalance,
				b.ExecutedValue BudgetExecutedValue,
				b.Balance BudgetBalance
		FROM Budget.RecognitionModification rm WITH (NOLOCK)
		JOIN Budget.RecognitionModificationDetail rmd WITH (NOLOCK) ON rm.Id = rmd.RecognitionModificationId
		/************************************  RECONOCIMIENTO ************************************/
		JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON rmd.RecognitionDetailId = rd.Id				
		JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON c.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id
		JOIN Budget.Recognition r WITH (NOLOCK) ON rd.RecognitionId = r.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON r.ThirdPartyId = tp.Id
		/************************************** ADICIONALES **************************************/
		LEFT JOIN Budget.Budget b WITH (NOLOCK) ON rd.CategoryId = b.CategoryId AND rd.RevenueTypeId = b.RevenueTypeId
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON rm.EntityName = gend.EntityName
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(rm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@RecognitionModificationCode, '') = '' OR rm.Code = @RecognitionModificationCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY rm.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END