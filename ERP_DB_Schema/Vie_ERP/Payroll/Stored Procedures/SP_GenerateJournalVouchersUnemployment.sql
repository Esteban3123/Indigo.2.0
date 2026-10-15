-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 2019-12-13
-- Description:	Procedimiento que se encarga de realizar la contabilización de las Cesantias
-- =============================================
CREATE PROCEDURE [Payroll].[SP_GenerateJournalVouchersUnemployment] @GroupId AS           INT, 
                                                                   @PeriodInitialDate AS DATE, 
                                                                   @PeriodEndDate AS     DATE, 
                                                                   @CodeUser AS          VARCHAR(50)
AS
    BEGIN
        SET NOCOUNT ON;

        /************************************* VARIABLES ************************************/

        DECLARE @EmployeeId AS INT, -- Variable usada para validaciones
        @PayrollStartDate AS DATE, @CompanyThirdPartyId INT, @IdUnemploymentConcept INT;
        DECLARE @CostDistributions TABLE
        ([Id]                   [INT] IDENTITY(1, 1) NOT NULL, 
         [LiquidationDetailId]  [INT] NOT NULL, 
         [JournalVoucherTypeId] [INT] NOT NULL, 
         [MainAccountId]        [INT] NULL, 
         [MainAccountNumber]    [VARCHAR](50) NOT NULL, 
         [ThirdPartyId]         [INT] NULL, 
         [CostCenterId]         [INT] NULL, 
         [DebitValue]           DECIMAL(18, 0) DEFAULT(0), 
         [CreditValue]          DECIMAL(18, 0) DEFAULT(0), 
         [Detail]               VARCHAR(500), 
         [EmployeeId]           [INT] NOT NULL
        );

        --Tabla con el resultado del proceso
        DECLARE @TableResult TABLE
        (CodeMessage INT, 
         Message     VARCHAR(MAX), 
         Consecutive VARCHAR(MAX)
        );
        DECLARE @Rows_Liquidation INT= 1, @LiquidationId INT= 0, @TotalDeducted DECIMAL(18, 0),
        -------------------------------------------			
        @Rows_LiquidationDetail INT= 1, @LiquidationDetailId INT= 0, @AdjustedValue DECIMAL(18, 0), @Nature TINYINT,
        -------------------------------------------
        @Rows_CostDistribution INT= 1, @CostDistributionId INT= 0, @CostDistributionAdjusted DECIMAL(18, 0);

        /******************************** VARIABLES CONTABLES *******************************/

        DECLARE @LegalBookId INT, @PayrollJournalVoucherTypeId INT= NULL, @PayrollAccountId INT, @PayrollAccountNumber VARCHAR(20), @AccountedBy CHAR(1), @InabilityAccountedBy TINYINT, @YearUnemployment INT;

        --Se declara una tabla con los datos para la cabecera del comprobante contable 
        DECLARE @JournalVourcherTmp TABLE
        (Id               INT DEFAULT(0), 
         Consecutive      BIGINT DEFAULT(0), 
         LegalBookId      INT, 
         IdJournalVoucher INT, 
         VoucherDate      VARCHAR(30), 
         Imported         VARCHAR(5) DEFAULT('False'), 
         [Status]           TINYINT, 
         Detail           VARCHAR(MAX), 
         EntityCode       VARCHAR(20), 
         EntityId         INT, 
         EntityName       VARCHAR(250), 
         OriginEntityName VARCHAR(250), 
         IsClosedYear     TINYINT DEFAULT(0),
		 CurrencyId       INT,
		 DateTRM		  DATE
        );

        --Se declara una tabla temporal para los detalles del comprobante
        DECLARE @JournalVourcherDetailTmp TABLE
        (Id            INT DEFAULT(0), 
         IdAccounting  INT DEFAULT(0), 
         IdMainAccount INT, 
         IdThirdParty  INT, 
         IdCostCenter  INT, 
         DebitValue    DECIMAL(18, 2), 
         CreditValue   DECIMAL(18, 2), 
         Detail        VARCHAR(500), 
         IdRetention   INT, 
         RetentionRate DECIMAL(6, 3), 
         BaseValue     DECIMAL(18, 2), 
         BillingValue  DECIMAL(18, 2)
        );

        --Variable para obtener el xml
        DECLARE @JournalVoucherXML AS XML, @CodeMessage INT, @Message VARCHAR(MAX), @IdJournalVoucherResult INT;
		DECLARE @CurrencyId AS INT  = (select top 1 CurrencyId  from Payroll.PayrollSettings)

        --tabla temporal para almacenar el resultado del movimiento contable
        DECLARE @resultJournalVoucher TABLE
	    (
		    code INT, 
		    MessageResult VARCHAR(max), 
		    IdJournalVoucher INT
	    )
        BEGIN TRY

            /************************************* ASIGNACIONES SIMPLES ************************************/

            SELECT @IdUnemploymentConcept = IdConceptUnemployment
            FROM Payroll.PayrollSettings;
            SELECT @InabilityAccountedBy = ps.InabilityAccountedBy
            FROM Payroll.PayrollSettings ps;
            SELECT @PayrollJournalVoucherTypeId = payroll.Id, 
                   @PayrollAccountId = pp.IdPayrollAccount, 
                   @PayrollAccountNumber = pp.PayrollAccount, 
                   @CompanyThirdPartyId = c.ThirdPartyId, 
                   @AccountedBy = pp.AccountedBy				   
            FROM Payroll.[Group] g
                 JOIN Payroll.Company c ON g.CompanyId = c.Id
                 JOIN Payroll.PayrollParameter pp ON g.PayrollParameterId = pp.Id
                 LEFT JOIN GeneralLedger.JournalVoucherTypes payroll ON pp.IdUnemploymentVoucherType = payroll.Id
            WHERE g.Id = @GroupId;

            -- Busco el Libro Oficial
            SELECT @LegalBookId = lb.Id
            FROM GeneralLedger.LegalBook lb
            WHERE lb.OfficialBook = 1;
            SET @YearUnemployment = YEAR(@PeriodEndDate);

            /************************************* VALIDACIONES ************************************/

            IF @PayrollJournalVoucherTypeId IS NULL
                BEGIN
                    INSERT INTO @TableResult
                    (CodeMessage, 
                     Message, 
                     Consecutive
                    )
                           SELECT 999 AS CodeMessage, 
                                  'No ha seleccionado ningún Tipo de Comprobante Contable para Cesantias en el Formulario de Grupos' AS Message, 
                                  '' AS Consecutive;
                    GOTO PRINT_RESULT;
            END;
            IF EXISTS
            (
                SELECT 1
                FROM Payroll.UnemployedLiquidation cd
                WHERE cd.[Year] = @YearUnemployment
                      AND cd.UnemployedEndingDate = @PeriodEndDate
                      AND cd.GroupId = @GroupId
                      AND cd.[Status] = 1
            )
                BEGIN
                    INSERT INTO @TableResult
                    (CodeMessage, 
                     Message, 
                     Consecutive
                    )
                           SELECT 999 AS CodeMessage, 
                                  'Las Cesantias para este grupo y este periodo ya fueron confirmadas' AS Message, 
                                  '' AS Consecutive;
                    GOTO PRINT_RESULT;
            END;
            IF NOT EXISTS
            (
                SELECT 1
                FROM Payroll.PayrollSettings
            )
                BEGIN
                    INSERT INTO @TableResult
                    (CodeMessage, 
                     Message, 
                     Consecutive
                    )
                           SELECT 999 AS CodeMessage, 
                                  'No se encontraron parámetros de nómina definidos' AS Message, 
                                  '' AS Consecutive;
                    GOTO PRINT_RESULT;
            END;
            IF @PayrollJournalVoucherTypeId IS NULL
                BEGIN
                    INSERT INTO @TableResult
                    (CodeMessage, 
                     Message, 
                     Consecutive
                    )
                           SELECT 999 AS CodeMessage, 
                                  'No está parametrizado el Comprobante Contable de Primas' AS Message, 
                                  '' AS Consecutive;
                    GOTO PRINT_RESULT;
            END;
            IF @LegalBookId IS NULL
                BEGIN
                    INSERT INTO @TableResult
                    (CodeMessage, 
                     Message, 
                     Consecutive
                    )
                           SELECT 999 AS CodeMessage, 
                                  'No está parametrizado un libro oficial' AS Message, 
                                  '' AS Consecutive;
                    GOTO PRINT_RESULT;
            END;

            -- Validar que los conceptos tengan parametrizados sus respectivas cuentas contables

            IF EXISTS
            (
                SELECT 1
                FROM Payroll.ConceptAccountingStructure cas
                     JOIN Payroll.Concept c ON cas.ConceptId = c.Id
					 JOIN Payroll.UnemployedConcept UC ON UC.IdConcept = C.Id
					 JOIN Payroll.UnemployedLiquidation UL ON UL.Id = UC.IdUnemployementLiquidation
                WHERE cas.AccruedAccount = cas.DeductedAccount AND UL.GroupId = @GroupId and UL.UnemployedInitialDate = @PeriodInitialDate and UL.UnemployedEndingDate = @PeriodEndDate
            )
                BEGIN
                    INSERT INTO @TableResult
                    (CodeMessage, 
                     Message, 
                     Consecutive
                    )
                           SELECT DISTINCT 
                                  999 AS CodeMessage, 
                                  'El concepto ' + c.Code + ' tiene la misma cuenta contable parametrizada en la estructura ' + CONCAT(acs.Code, ' - ', acs.Description) AS Message, 
                                  '' AS Consecutive
                           FROM Payroll.ConceptAccountingStructure cas
                                JOIN Payroll.Concept c ON cas.ConceptId = c.Id
                                JOIN Payroll.AccountingStructure acs ON cas.AccountingStructureId = acs.Id
								JOIN Payroll.UnemployedConcept UC ON UC.IdConcept = C.Id
								JOIN Payroll.UnemployedLiquidation UL ON UL.Id = UC.IdUnemployementLiquidation
                           WHERE cas.AccruedAccount = cas.DeductedAccount AND UL.GroupId = @GroupId and UL.UnemployedInitialDate = @PeriodInitialDate and UL.UnemployedEndingDate = @PeriodEndDate
                    GOTO PRINT_RESULT;
            END;
           IF EXISTS
		(
			SELECT 1
			FROM Payroll.UnemployedLiquidation l
			JOIN Payroll.UnemployedConcept ld ON l.Id = ld.IdUnemployementLiquidation
			JOIN Payroll.Contract c ON l.ContractId = c.Id
			JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
			LEFT JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
			LEFT JOIN Payroll.ConceptAccountingStructure cas ON ld.IdConcept = cas.ConceptId AND acs.Id = cas.AccountingStructureId
			LEFT JOIN GeneralLedger.MainAccounts maD ON maD.LegalBookId = @LegalBookId 
				WHERE l.[Year]  = @YearUnemployment
				AND l.UnemployedEndingDate = @PeriodEndDate
				AND l.GroupId = @GroupId
				AND (maD.Id IS NULL)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT 
					999 AS CodeMessage, 'El concepto ' + conc.Code + ' tiene cuentas contables sin parametrizar' AS Message, '' AS Consecutive
				FROM Payroll.UnemployedLiquidation l
			JOIN Payroll.UnemployedConcept ld ON l.Id = ld.IdUnemployementLiquidation
			JOIN Payroll.Concept conc ON conc.Id = ld.IdConcept
			JOIN Payroll.Contract c ON l.ContractId = c.Id
			JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
			LEFT JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
			LEFT JOIN Payroll.ConceptAccountingStructure cas ON ld.IdConcept = cas.ConceptId AND acs.Id = cas.AccountingStructureId
			LEFT JOIN GeneralLedger.MainAccounts maD ON maD.LegalBookId = @LegalBookId 
			WHERE l.[Year]  = @YearUnemployment
				AND l.UnemployedEndingDate = @PeriodEndDate
				AND l.GroupId = @GroupId
				AND (maD.Id IS NULL)

			GOTO PRINT_RESULT
		END

            -- Inserto en la Tabla de Distribución de Costos los Conceptos Devengados
           INSERT INTO @CostDistributions 
		(
			LiquidationDetailId,
			JournalVoucherTypeId,
			MainAccountId,
			MainAccountNumber,
			ThirdPartyId,
			CostCenterId,
			DebitValue,
			CreditValue,
			EmployeeId,
			Detail
		)
		SELECT 
			uc.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			e.ThirdPartyId AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			IIF(uc.AccruedValue > 0 , uc.AccruedValue, 0) AS DebitValue,
			0 AS CreditValue,
			e.Id,
			'Cesantias'
		FROM Payroll.UnemployedLiquidation	 l
		JOIN Payroll.UnemployedConcept uc ON uc.IdUnemployementLiquidation = l.Id
		JOIN Payroll.Contract c ON l.ContractId = c.Id
		JOIN Payroll.Employee e on e.Id = c.EmployeeId
		JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas ON uc.IdConcept = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN Payroll.Concept CONC ON CONC.Id = UC.IdConcept
		JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.AccruedAccount
		WHERE l.[year]  = @YearUnemployment
			AND l.UnemployedEndingDate = @PeriodEndDate
			AND l.GroupId = @GroupId
			AND CONC.ConceptType <> 3
			AND L.TotalUnemployed > 0

            -- Inserto en la Tabla de Distribución de Costos los Conceptos Deducidos
           INSERT INTO @CostDistributions 
		(
			LiquidationDetailId,
			JournalVoucherTypeId,
			MainAccountId,
			MainAccountNumber,
			ThirdPartyId,
			CostCenterId,
			DebitValue,
			CreditValue,
			EmployeeId,
			Detail
		)
		SELECT 
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			e.ThirdPartyId AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			0 AS DebitValue,
			IIF(ld.DeductedValue > 0 , ld.DeductedValue, 0) AS CreditValue,
			e.Id,
			'Cesantias'
		FROM Payroll.UnemployedLiquidation l
		JOIN Payroll.UnemployedConcept ld ON l.Id = ld.IdUnemployementLiquidation
		JOIN Payroll.Contract c ON l.ContractId = c.Id
		JOIN Payroll.Employee e on e.Id = c.EmployeeId
		JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas ON ld.IdConcept = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN Payroll.Concept CONC ON CONC.Id = LD.IdConcept
		JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.DeductedAccount
		WHERE l.[YEAR]  = @YearUnemployment
			AND l.UnemployedEndingDate = @PeriodEndDate
			AND l.GroupId = @GroupId
			AND CONC.ConceptType <> 3
			AND LD.DeductedValue > 0

            -- Totalizo las Cesantias
         INSERT INTO @CostDistributions 
		(
			LiquidationDetailId,
			JournalVoucherTypeId,
			MainAccountId,
			MainAccountNumber,
			ThirdPartyId,
			CostCenterId,
			DebitValue,
			CreditValue,
			EmployeeId,
			Detail
		)
		SELECT 
			l.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			IIF(@AccountedBy = 1, E.ThirdPartyId, @CompanyThirdPartyId) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			0 AS DebitValue,
			SUM(ILD.AccruedValue) - SUM(ILD.DeductedValue) AS CreditValue,
			c.EmployeeId,
			'Cesantias'
		FROM Payroll.UnemployedLiquidation l	
		JOIN Payroll.UnemployedConcept ILD ON L.Id = ILD.IdUnemployementLiquidation
		JOIN Payroll.Concept CONC ON CONC.Id = ILD.IdConcept	
		JOIN Payroll.Contract c ON l.ContractId = c.Id
		JOIN Payroll.Employee E ON E.Id = C.EmployeeId
		JOIN Payroll.[Group] G ON G.Id = L.GroupId
		JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas ON cas.ConceptId = @IdUnemploymentConcept AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.DeductedAccount
		WHERE l.[Year]  = @YearUnemployment
			AND l.UnemployedEndingDate = @PeriodEndDate
			AND l.GroupId = @GroupId
			AND conc.ConceptType <> 3
		GROUP BY l.Id,ma.Id,ma.Number,IIF(@AccountedBy = 1, E.ThirdPartyId, @CompanyThirdPartyId),ma.HandlesCostCenter,fu.CostCenterId, c.EmployeeId

            /*************************************** CONTABILIZACIÓN ************************************/

            INSERT INTO @JournalVourcherTmp
            (LegalBookId, 
             IdJournalVoucher, 
             VoucherDate, 
             [Status], 
             Detail, 
             EntityCode, 
             EntityId, 
             EntityName,
			 CurrencyId,
			 DateTRM
            )
                   SELECT TOP 1 @LegalBookId LegalBookId, 
                                @PayrollJournalVoucherTypeId IdJournalVoucher, 
                                IIF(UnemployedEndingDate > [Common].[GETDATE](), [Common].[GETDATE](), UnemployedEndingDate) VoucherDate, 
                                2 [Status], 
                                'Comprobante Contable de Cesantias - Grupo: ' + g.Code + ' ' + g.Name + ' - Cesantias De ' + CAST(CAST(UnemployedEndingDate AS DATE) AS VARCHAR(20)) Detail, 
                                NULL EntityCode, 
                                NULL EntityId, 
                                'PayrollLiquidation' EntityName,
								@CurrencyId,
								@PeriodEndDate
                   FROM Payroll.UnemployedLiquidation ipa 
                        JOIN Payroll.[Group] G ON ipa.GroupId = G.Id
                   WHERE ipa.[Year] = @YearUnemployment
                         AND IPA.UnemployedEndingDate = @PeriodEndDate
                         AND G.Id = @GroupId;

            /*************************************** NOMINA ************************************/

            IF EXISTS
            (
                SELECT 1
                FROM @CostDistributions t
                WHERE t.JournalVoucherTypeId = @PayrollJournalVoucherTypeId
            )
                BEGIN
                    INSERT INTO @JournalVourcherDetailTmp
                    (IdMainAccount, 
                     IdThirdParty, 
                     IdCostCenter, 
                     Detail, 
                     DebitValue, 
                     CreditValue, 
                     IdRetention, 
                     RetentionRate, 
                     BaseValue, 
                     BillingValue
                    )
                           SELECT t.MainAccountId AS IdMainAccount, 
                                  t.ThirdPartyId AS IdThirdParty, 
                                  t.CostCenterId AS IdCostCenter, 
                                  CONCAT('Registro ', t.Detail, ' empleado: ', tp.Nit, ' - ', tp.Name) AS Detail, 
                                  t.DebitValue AS DebitValue, 
                                  t.CreditValue AS CreditValue, 
                                  NULL AS IdRetention, 
                                  NULL AS RetentionRate, 
                                  NULL AS BaseValue, 
                                  NULL AS BillingValue
                           FROM @CostDistributions t
                                LEFT JOIN Payroll.Employee e ON t.EmployeeId = e.Id
                                LEFT JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id
                           WHERE t.JournalVoucherTypeId = @PayrollJournalVoucherTypeId;

                    --Obtengo el xml para poder consumir el sp que guarda el comprobante contable
                    SELECT @JournalVoucherXML = CONVERT(XML,
                    (
                        SELECT *
                        FROM @JournalVourcherTmp JournalVoucher
                             JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting FOR XML AUTO, TYPE, ELEMENTS
                    ));
 
                    --Se consume el sp que guarda el movimiento contable
                    insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 

                    select 
                    @CodeMessage = rjv.code, 
                    @Message = rjv.MessageResult, 
                    @IdJournalVoucherResult = rjv.IdJournalVoucher
                    from @resultJournalVoucher rjv

					INSERT @TableResult (CodeMessage, Message, Consecutive)
								SELECT DISTINCT 
								rjv.code AS CodeMessage, 
								rjv.MessageResult AS Message, 
								'' AS Consecutive
								FROM @resultJournalVoucher as rjv
                         
                        IF @CodeMessage = 0
                        BEGIN
                            -- ACTUALIZAMOS EL ESTADO DE LA TABLA DE PRIMAS
                            UPDATE Payroll.UnemployedLiquidation
                              SET 
                                  [Status] = 1,
                                  [ConfirmLiquidated] = 1,
                                  [ConfirmationDate] = GETDATE(),
								  [UnemployedInterestPayDate] = (SELECT G.NextDateLiquidation --campo para que se pueda tomar en los interese de cesantias pagados con nomina
																FROM Payroll.[Group] G WHERE G.ID = @GroupId)
                            WHERE [Year] = @YearUnemployment
                                  AND UnemployedEndingDate = @PeriodEndDate
                                  AND GroupId = @GroupId;
                        END;

            END;
			ELSE
			BEGIN
				INSERT INTO @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 As CodeMessage, 'No se encontro información para contabilizar' AS Message, '' AS Consecutive
			END;
        END TRY
        BEGIN CATCH
            INSERT INTO @TableResult
            (CodeMessage, 
             Message, 
             Consecutive
            )
                   SELECT 999 AS CodeMessage, 
                          ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, 
                          '' AS Consecutive;
        END CATCH;
        PRINT_RESULT:
        SELECT CodeMessage, 
               Message, 
               Consecutive
        FROM @TableResult;
    END;