-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-04-12
-- Description:	Procedimiento para el reporte de saldos por tercero
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportThirdPartyBalance]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Year INT,
			@Month INT,
			@LegalBookId INT,
			@AccountsZero BIT,
			@ThirdPartyInitial VARCHAR(MAX),
			@ThirdPartyFinal VARCHAR(MAX),
			@MainAccountInitial VARCHAR(MAX),
			@MainAccountFinal VARCHAR(MAX),
			-------------------------------------------------------------------
			@LegalBookType TINYINT,
			@LastYearClose INT,
			@LegalBookName VARCHAR(200)

	BEGIN TRY
		
		/******************************************** CRITERIOS Y FILTROS ********************************************/

		--Se obtienen los datos de los criterios
		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int'),
				@AccountsZero = t.x.value('AccountsZero[1]','bit'),
				@ThirdPartyInitial = t.x.value('ThirdPartyInitial[1]','varchar(max)'),
				@ThirdPartyFinal = t.x.value('ThirdPartyFinal[1]','varchar(max)'),
				@MainAccountInitial = t.x.value('MainAccountInitial[1]','varchar(max)'),
				@MainAccountFinal = t.x.value('MainAccountFinal[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		SELECT	@ThirdPartyInitial = IIF(ISNULL(@ThirdPartyInitial, '') = '', '0', @ThirdPartyInitial),
				@ThirdPartyFinal = IIF(ISNULL(@ThirdPartyFinal, '') = '', 'ZZZZZZZZZZZZZZZZZZZZ', @ThirdPartyFinal),
				@MainAccountInitial = IIF(ISNULL(@MainAccountInitial, '') = '', '0', @MainAccountInitial),
				@MainAccountFinal = IIF(ISNULL(@MainAccountFinal, '') = '', 'ZZZZZZZZZZZZZZZZZZZZ', @MainAccountFinal)

		---------------------------------------------------------------------------------------------------------------

		SELECT	@LegalBookType = lb.TypeBook,
				@LegalBookName = lb.Name,
				@LastYearClose = lb.LastYearClose
		FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
		WHERE lb.Id = @LegalBookId

		/*****************************************  OBTENEMOS LA INFORMACION *****************************************/

		--Insertamos los movimientos del periodo inicial
		SELECT 	tp.Id AS ThirdPartyId, tp.Nit AS ThirdPartyNit, tp.Name AS ThirdPartyName, 
				-----------------------------------------------------------------------------------------------------------------
				@LegalBookName LegalBookName,
				glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName,
				-----------------------------------------------------------------------------------------------------------------
				glb.ValueDebitInitial, glb.ValueCreditInitial,
				((glb.ValueDebitInitial - glb.ValueCreditInitial) * IIF(glb.MainAccountNature = 1, 1, -1)) PreviousBalance,
				-----------------------------------------------------------------------------------------------------------------
				glb.ValueDebitMovement, glb.ValueCreditMovement,
				-----------------------------------------------------------------------------------------------------------------
				(glb.ValueDebitInitial + glb.ValueDebitMovement) ValueDebitFinal, (glb.ValueCreditInitial + glb.ValueCreditMovement) ValueCreditFinal,
				((glb.ValueDebitInitial - glb.ValueCreditInitial + glb.ValueDebitMovement - glb.ValueCreditMovement) * IIF(glb.MainAccountNature = 1, 1, -1)) NewBalance
		INTO #SP_ReportThirdPartyBalance
		FROM [GeneralLedger].[GetGeneralLedgerBalance](@LegalBookType, @LegalBookId, @LastYearClose, @Year, @Month, @Month, 0) glb
		JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON glb.ThirdPartyId  = tp.Id
		WHERE	glb.MainAccountCode BETWEEN ISNULL(@MainAccountInitial, '0') AND ISNULL(@MainAccountFinal, 'ZZZZZZZZZZZZZZZZZZZZ')
				AND ISNULL(tp.Nit, '0') BETWEEN ISNULL(@ThirdPartyInitial, '0') AND ISNULL(@ThirdPartyFinal, 'ZZZZZZZZZZZZZZZZZZZZ')

		/******************************************* APLICAMOS LOS FILTROS *******************************************/

		--Filtros del reporte
		IF @AccountsZero = 0 
		BEGIN
			DELETE e 
			FROM #SP_ReportThirdPartyBalance e 
			JOIN 
			(
				SELECT MainAccountId, ThirdPartyId
				FROM #SP_ReportThirdPartyBalance 
				GROUP BY MainAccountId, ThirdPartyId 
				HAVING SUM(newBalance) = 0
			) AS data ON data.MainAccountId = e.MainAccountId AND ISNULL(data.ThirdPartyId, 0) = ISNULL(e.ThirdPartyId, 0)
		END		
    
		/*****************************************  MOSTRAMOS LOS RESULTADOS *****************************************/

		--Mostramos los resultados
		SELECT	tpb.LegalBookName, 
				tpb.ThirdPartyId, 
				tpb.ThirdPartyNit, 
				tpb.ThirdPartyName,
				tpb.MainAccountId, 
				tpb.MainAccountCode, 
				tpb.MainAccountName,
				tpb.ValueDebitInitial,
				tpb.ValueCreditInitial,
				tpb.PreviousBalance,
				tpb.ValueDebitMovement, 
				tpb.ValueCreditMovement,
				tpb.ValueDebitFinal,
				tpb.ValueCreditFinal,
				tpb.NewBalance
		FROM #SP_ReportThirdPartyBalance tpb
		ORDER BY 3, 6
	END TRY
	BEGIN CATCH
		PRINT CONCAT('Error: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH

	-- Eliminamos Las tablas temporales
	IF OBJECT_ID('tempdb..#SP_ReportThirdPartyBalance') IS NOT NULL DROP TABLE #SP_ReportThirdPartyBalance
END