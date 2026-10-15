-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-30
-- Description:	Procedimiento para el reporte de listado de reconocimientos del presupuesto de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentIncomeRecognition]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@RecognitionCode VARCHAR(20),
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
				@RecognitionCode = t.x.value('RecognitionCode[1]','varchar(20)'),
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
				r.Code,
				r.DocumentDate,
				r.Document,
				r.Observations,
				CASE r.RecognitonType 
					WHEN 1 THEN 'Reconocimiento' 
					WHEN 2 THEN 'Cuenta x Cobrar' 
					WHEN 3 THEN 'Vigencia Anterior' 
					ELSE 'N/A'
				END RecognitionTypeName,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE r.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				r.EntityCode OriginCode,
				ISNULL(gend.Description, r.EntityName) OriginName,
				c.Code CategoryCode,
				c.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				rd.InitialValue,
				ISNULL(rm.DebitValue, 0) DebitValue,
				ISNULL(rm.CreditValue, 0) CreditValue,
				rd.InitialValue - ISNULL(rm.DebitValue, 0) + ISNULL(rm.CreditValue, 0) TotalValue,
				ISNULL(cn.ExecutedValue, 0) ExecutedValue,
				rd.InitialValue - ISNULL(rm.DebitValue, 0) + ISNULL(rm.CreditValue, 0) - ISNULL(cn.ExecutedValue, 0) Balance
		FROM Budget.Recognition r WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON r.ThirdPartyId = tp.Id
		JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON r.Id = rd.RecognitionId
		JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON c.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON r.EntityName = gend.EntityName
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT	rmd.RecognitionDetailId, 
					SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, 
					SUM(IIF(rmd.Nature = 1, 0, rmd.Value)) CreditValue
			FROM Budget.RecognitionModification rm WITH (NOLOCK)
			JOIN Budget.RecognitionModificationDetail rmd WITH (NOLOCK) ON rm.Id = rmd.RecognitionModificationId
			WHERE rm.Status = 2 AND CAST(rm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY rmd.RecognitionDetailId
		) rm ON rd.Id = rm.RecognitionDetailId 
		/***************************************  RECAUDOS ***************************************/
		LEFT JOIN 
		(
			SELECT	cnd.RecognitionDetailId, 
					SUM(cnd.InitialValue + ISNULL(cm.CreditValue, 0) - ISNULL(cm.DebitValue, 0)) ExecutedValue
			FROM Budget.Collection cn WITH (NOLOCK)
			JOIN Budget.CollectionDetail cnd WITH (NOLOCK) ON cn.Id = cnd.CollectionId
			LEFT JOIN 
			(
				SELECT cmd.CollectionDetailId, SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue, SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
				FROM Budget.CollectionModification cm WITH (NOLOCK)
				JOIN Budget.CollectionModificationDetail cmd WITH (NOLOCK) ON cm.Id = cmd.CollectionModificationId
				WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY cmd.CollectionDetailId
			) cm ON cnd.Id = cm.CollectionDetailId
			WHERE cn.Status = 2 AND CAST(cn.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY cnd.RecognitionDetailId
		) cn ON rd.Id = cn.RecognitionDetailId 
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(r.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@RecognitionCode, '') = '' OR r.Code = @RecognitionCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY r.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END