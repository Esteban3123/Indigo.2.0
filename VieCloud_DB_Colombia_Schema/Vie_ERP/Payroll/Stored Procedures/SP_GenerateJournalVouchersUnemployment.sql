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
                INSERT INTO @TableResult (CodeMessage, Message, Consecutive)
				SELECT  @CodeMessage, 'Comprobante Contable de Cesantias: '+ @Message, @IdJournalVoucherResult
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que confirma (y originalmente contabilizaba) las cesantías de un grupo de empleados para un período determinado. Valida que las cesantías del grupo y año indicados no hayan sido confirmadas previamente, que existan parámetros de nómina configurados y que haya un libro contable oficial definido. Aunque la generación real de comprobantes contables en el libro mayor (GeneralLedger) está deshabilitada, el procedimiento actualiza el estado de las liquidaciones de cesantías en Payroll.UnemployedLiquidation, marcándolas como confirmadas. Utiliza la configuración de grupos (Payroll.Group), empresas (Payroll.Company), parámetros de nómina (Payroll.PayrollParameter) y ajustes generales de nómina (Payroll.PayrollSettings) para obtener el tipo de comprobante, cuenta contable de nómina, tercero de la empresa y criterios de causación de cesantías.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVouchersUnemployment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVouchersUnemployment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y registra el comprobante contable de la liquidación de cesantías para un grupo y periodo, validando parametrización contable y marcando la liquidación como confirmada.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersUnemployment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un Tipo de Comprobante Contable de Cesantías parametrizado en PayrollParameter (IdUnemploymentVoucherType) para el grupo indicado.; Las cesantías del grupo, año y fecha final no deben estar ya confirmadas (UnemployedLiquidation.Status <> 1).; Debe existir al menos un registro en Payroll.PayrollSettings.; Debe existir un libro oficial (GeneralLedger.LegalBook.OfficialBook = 1).; Cada concepto involucrado debe tener cuentas contables distintas para causación y deducción (AccruedAccount <> DeductedAccount) en su ConceptAccountingStructure.; Cada concepto de la liquidación debe tener cuentas contables parametrizadas y existentes en GeneralLedger.MainAccounts para el libro oficial.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersUnemployment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan conceptos cuyo ConceptType <> 3.; Solo se incluyen liquidaciones con TotalUnemployed > 0 para los devengados, y con DeductedValue > 0 para los deducidos.; El año de la liquidación se determina como YEAR(@PeriodEndDate).; Los detalles del comprobante usan el tercero del empleado para devengados y deducidos individuales; el total de cesantías usa el tercero del empleado o de la empresa según AccountedBy.; El comprobante se crea con Status=2 (presumiblemente borrador/pendiente) y EntityName=''PayrollLiquidation''.; La moneda del comprobante se toma del primer registro de Payroll.PayrollSettings.CurrencyId y la DateTRM se fija en @PeriodEndDate.; Solo se actualiza UnemployedLiquidation.Status a 1 cuando el comprobante contable se generó sin errores.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersUnemployment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: Cuando existen distribuciones de costo para el tipo de comprobante de cesantías, se invoca GeneralLedger.SP_CreateAndValidateJournalVoucherMovement con un XML que contiene cabecera y detalle (débitos/créditos por concepto devengado, deducido y total de cesantías) para crear el comprobante contable.; [UPDATE] Payroll.UnemployedLiquidation: Tras confirmar correctamente el comprobante (CodeMessage <> 999), se actualiza Status=1 y UnemployedInterestPayDate = Group.NextDateLiquidation para los registros del año, fecha final y grupo indicados.; [RETURN_RESULT] @TableResult: Al final (etiqueta PRINT_RESULT) se retorna SELECT con CodeMessage, Message y Consecutive describiendo éxito (código 0 con consecutivo del comprobante) o error (código 999 con motivo).; [RETURN_RESULT] @TableResult: Si BEGIN CATCH captura una excepción, se devuelve CodeMessage=999 con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersUnemployment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @PayrollJournalVoucherTypeId IS NULL → Retorna error 999 ''No ha seleccionado ningún Tipo de Comprobante Contable para Cesantias en el Formulario de Grupos'' y termina.; si Existe UnemployedLiquidation con Year, UnemployedEndingDate y GroupId dados con Status=1 → Retorna error 999 ''Las Cesantias para este grupo y este periodo ya fueron confirmadas'' y termina.; si No existen filas en Payroll.PayrollSettings → Retorna error 999 ''No se encontraron parámetros de nómina definidos'' y termina.; si @LegalBookId IS NULL (no hay libro oficial) → Retorna error 999 ''No está parametrizado un libro oficial'' y termina.; si Existe ConceptAccountingStructure con AccruedAccount = DeductedAccount para conceptos de la liquidación del grupo y periodo → Retorna error 999 indicando el concepto y estructura con cuentas iguales y termina.; si Existen conceptos de la liquidación cuyas cuentas no están en GeneralLedger.MainAccounts del libro oficial → Retorna error 999 ''El concepto X tiene cuentas contables sin parametrizar'' y termina.; si Existen registros en @CostDistributions para el tipo de comprobante de nómina → Construye XML, llama SP_CreateAndValidateJournalVoucherMovement, y según resultado registra éxito y actualiza la liquidación, o reporta error 999. else Retorna error 999 ''No se encontro información para contabilizar''.; si @CodeMessage = 999 tras invocar SP_CreateAndValidateJournalVoucherMovement → Retorna error 999 ''No se confirmó el Comprobante Contable de Nómina por '' + mensaje devuelto. else Registra mensaje de éxito con consecutivo y tipo del comprobante, y actualiza UnemployedLiquidation.; si ma.HandlesCostCenter = 1 → Asigna CostCenterId = fu.CostCenterId en la distribución de costos. else Asigna CostCenterId = NULL.; si @AccountedBy = 1 en el total de cesantías → Usa Employee.ThirdPartyId como tercero del crédito total. else Usa Company.ThirdPartyId (@CompanyThirdPartyId) como tercero.; si UnemployedEndingDate > [Common].[GETDATE]() → Usa Common.GETDATE() como VoucherDate. else Usa UnemployedEndingDate como VoucherDate.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersUnemployment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersUnemployment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersUnemployment';
-- GO
