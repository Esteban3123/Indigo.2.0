-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 2019-10-22
-- Description:	Procedimiento que se encarga de realizar la contabilización de la Prima
-- =============================================
CREATE PROCEDURE [Payroll].[SP_GenerateJournalVouchersIncentivePayment]
	@GroupId AS INT,
	@PeriodInitialDate AS DATE,
	@PeriodEndDate AS DATE,
	@Period AS INT, 
	@CodeUser AS VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	DECLARE @EmployeeId AS INT, -- Variable usada para validaciones
			@PayrollStartDate AS DATE,
			@CompanyThirdPartyId INT,
			@IdConceptIncentivePayment INT

	
	DECLARE @CostDistributions TABLE
	(
		[Id] [int] IDENTITY(1,1) NOT NULL,	
		[LiquidationDetailId] [int] NOT NULL,	
		[JournalVoucherTypeId] [int] NOT NULL,
		[MainAccountId] [int] NULL,
		[MainAccountNumber] [varchar](50) NOT NULL,
		[ThirdPartyId] [int] NULL,
		[CostCenterId] [int] NULL,
		[DebitValue] DECIMAL(18,0) DEFAULT(0),
		[CreditValue] DECIMAL(18,0) DEFAULT(0),
		[Detail] VARCHAR(500),
		[EmployeeId] [int] NOT NULL
	)

	--Tabla con el resultado del proceso
	DECLARE @TableResult TABLE
	(
		CodeMessage INT,
		Message VARCHAR(MAX),
		Consecutive VARCHAR(MAX)
	)

	DECLARE @Rows_Liquidation INT = 1,
			@LiquidationId INT = 0,
			@TotalDeducted DECIMAL(18, 0),
			-------------------------------------------			
			@Rows_LiquidationDetail INT = 1,
			@LiquidationDetailId INT = 0,
			@AdjustedValue DECIMAL(18, 0),
			@Nature TINYINT,
			-------------------------------------------
			@Rows_CostDistribution INT = 1,
			@CostDistributionId INT = 0,
			@CostDistributionAdjusted DECIMAL(18, 0)			

	/******************************** VARIABLES CONTABLES *******************************/

	DECLARE @LegalBookId INT,
			@PayrollJournalVoucherTypeId INT,
			@PayrollAccountId INT,
			@PayrollAccountNumber VARCHAR(20),
			@AccountedBy CHAR(1),
			@InabilityAccountedBy TINYINT,
			@PayrollCurrencyId INT,
			@DateTRM as DATE,
			@OfficialCurrencyId INT

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
	(
		Id INT DEFAULT(0),
		Consecutive BIGINT DEFAULT(0),
		LegalBookId INT,
		IdJournalVoucher INT,
		VoucherDate VARCHAR(30),
		Imported VARCHAR(5) DEFAULT('False'),
		Status TINYINT,
		Detail VARCHAR(MAX),
		EntityCode VARCHAR(20),
		EntityId INT,
		EntityName VARCHAR(250),
		OriginEntityName VARCHAR(250),
		IsClosedYear TINYINT DEFAULT(0),
		DateTRM DATE,
		CurrencyId INT
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		Detail VARCHAR(500),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Variables para la contabilizacion
	DECLARE @JournalVoucherXML as XML,
			@CodeMessage Int,
			@Message Varchar(Max),
			@IdJournalVoucherResult Int

	--Variables para la nomina electronica
	DECLARE @IncentivePaymentRows INT,
			@IncentivePaymentId INT,
			@ThirdPartyId INT,
			@Year INT,
			@Month INT,
			@SupportSequenseId INT,
			@SupportSequenseDetailId INT,
			@SupportId INT,
			@SupportPrefix VARCHAR(20),
			@SupportConsecutive BIGINT

	--tabla temporal para almacenar el resultado del movimiento contable
	DECLARE @resultJournalVoucher TABLE
		(
			code INT, 
			MessageResult VARCHAR(max), 
			IdJournalVoucher INT
		)
	BEGIN TRY
		
		/*******************************************  ASIGNACIONES SIMPLES *******************************************/

		IF @Period = 1
		BEGIN
			SELECT @IdConceptIncentivePayment = ISNULL(pp.idIncentivePaymentConcept1, ps.ServicesIncentivePaymentConceptId)
			FROM Payroll.PayrollParameter pp
			JOIN Payroll.[Group] g ON g.PayrollParameterId = pp.Id
			LEFT JOIN Payroll.PayrollSettings ps ON 1 = 1
			WHERE g.Id = @GroupId
		END
		ELSE
		BEGIN
			SELECT @IdConceptIncentivePayment = ISNULL(pp.idIncentivePaymentConcept2, ps.ChristmasIncentivePaymentConceptId)
			FROM Payroll.PayrollParameter pp
			JOIN Payroll.[Group] g ON g.PayrollParameterId = pp.Id
			LEFT JOIN Payroll.PayrollSettings ps ON 1 = 1
			WHERE g.Id = @GroupId
		END

		SELECT
			@InabilityAccountedBy = ps.InabilityAccountedBy
		FROM Payroll.PayrollSettings ps

		SELECT
			@PayrollJournalVoucherTypeId = payroll.Id,		
			@PayrollAccountId = pp.IdPayrollAccount,
			@PayrollAccountNumber = pp.PayrollAccount,
			@CompanyThirdPartyId = c.ThirdPartyId,
			@AccountedBy = pp.AccountedBy
		FROM Payroll.[Group] g
		JOIN Payroll.Company c ON g.CompanyId = c.Id
		JOIN Payroll.PayrollParameter pp ON g.PayrollParameterId = pp.Id
		LEFT JOIN GeneralLedger.JournalVoucherTypes payroll ON pp.IdIncentiveVoucherType = payroll.Id
		WHERE g.Id = @GroupId

		-- Busco el Libro Oficial
		SELECT @LegalBookId = lb.Id , @OfficialCurrencyId = lb.OfficialCurrencyId
		FROM GeneralLedger.LegalBook lb
		WHERE lb.OfficialBook = 1

		--Asignacion moneda del modulo
		SELECT @PayrollCurrencyId = CurrencyId
		FROM Payroll.PayrollSettings

		/***********Actualizacion datos cabecera***********/
		UPDATE Payroll.IncentivePayment SET ConfirmationDate = [Common].[GETDATE]() , ConfirmationUser = @CodeUser
		WHERE PeriodInitialDate = @PeriodInitialDate AND PeriodEndDate = @PeriodEndDate AND GroupId = @GroupId

		--Asignacion fecha TRM 
		SELECT TOP 1 @DateTRM = ConfirmationDate
		FROM Payroll.IncentivePayment
		WHERE PeriodInitialDate = @PeriodInitialDate AND PeriodEndDate = @PeriodEndDate AND GroupId = @GroupId

		
		/***********************************************  VALIDACIONES ***********************************************/

		IF @PayrollCurrencyId IS NULL
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage,  'No se encuentra parametrizada la moneda en los parámetros de nómina.' AS Message, '' AS Consecutive
			GOTO PRINT_RESULT
		END

		IF EXISTS (SELECT 1 FROM GeneralLedger.GeneralLedgerSettings WHERE HandlesElectronicPayroll = 1) 
		BEGIN
			SELECT @SupportSequenseId = Id
			FROM Payroll.PayrollSequence 
			WHERE IdForm = '2635' AND IsManual = 0

			IF @SupportSequenseId IS NULL
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT 999 AS CodeMessage, 'No existe una secuencia numérica automatica para los soportes de pago de nómina electrónica.' AS Message, '' AS Consecutive

				GOTO PRINT_RESULT
			END

			SELECT	@SupportSequenseDetailId = psd.Id,
					@SupportPrefix = REPLACE(s.Pattern, '#', '')
			FROM Payroll.PayrollSequenceDetail psd
			JOIN Common.Sequense s ON psd.IdSequense = s.Id
			WHERE psd.PayrollSequenceId = @SupportSequenseId

			IF @SupportSequenseDetailId IS NULL
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT 999 AS CodeMessage, 'No existe un detalle de secuencia numérica para los soportes de pago de nómina electrónica.' AS Message, '' AS Consecutive

				GOTO PRINT_RESULT
			END
		END

		IF EXISTS 
		(
			SELECT 1
			FROM Payroll.IncentivePayment  cd
			WHERE cd.PeriodInitialDate = @PeriodInitialDate AND cd.PeriodEndDate = @PeriodEndDate and cd.GroupId = @GroupId
				AND cd.RegisterStatus = 2
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'Las primas para este grupo y este periodo ya fueron confirmadas' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF NOT EXISTS
		(
			SELECT 1
			FROM Payroll.PayrollSettings
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No se encontraron parámetros de nómina definidos' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF @PayrollJournalVoucherTypeId IS NULL
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No está parametrizado el Comprobante Contable de Primas' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		
		IF @LegalBookId IS NULL
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No está parametrizado un libro oficial' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		-- Validar que los conceptos tengan parametrizados sus respectivas cuentas contables

		IF EXISTS
		(
			SELECT 1
			FROM Payroll.ConceptAccountingStructure cas
			JOIN Payroll.Concept c ON cas.ConceptId = c.Id
			JOIN Payroll.IncentivePaymentDetail IPD ON IPD.ConceptId = c.Id
			JOIN Payroll.IncentivePayment IPA ON IPA.Id = IPD.IncentivePaymentId
			WHERE cas.AccruedAccount = cas.DeductedAccount and IPA.PeriodInitialDate = @PeriodInitialDate 
				AND IPA.PeriodEndDate = @PeriodEndDate and IPA.GroupId = @GroupId 
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT 999 AS CodeMessage, 'El concepto ' + c.Code + ' tiene la misma cuenta contable parametrizada en la estructura ' + CONCAT(acs.Code, ' - ', acs.Description) AS Message, '' AS Consecutive
				FROM Payroll.ConceptAccountingStructure cas
				JOIN Payroll.Concept c ON cas.ConceptId = c.Id
				JOIN Payroll.AccountingStructure acs ON cas.AccountingStructureId = acs.Id
				JOIN Payroll.IncentivePaymentDetail IPD ON IPD.ConceptId = c.Id
				JOIN Payroll.IncentivePayment IPA ON IPA.Id = IPD.IncentivePaymentId
				WHERE cas.AccruedAccount = cas.DeductedAccount and IPA.PeriodInitialDate = @PeriodInitialDate 
				AND IPA.PeriodEndDate = @PeriodEndDate and IPA.GroupId = @GroupId

			GOTO PRINT_RESULT
		END

		IF EXISTS
		(
			SELECT 1
			FROM Payroll.IncentivePayment l
			JOIN Payroll.IncentivePaymentDetail ld ON l.Id = ld.IncentivePaymentId
			JOIN Payroll.Contract c ON l.ContractId = c.Id
			JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
			LEFT JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
			LEFT JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
			LEFT JOIN GeneralLedger.MainAccounts maD ON maD.LegalBookId = @LegalBookId 
				WHERE l.PeriodInitialDate  = @PeriodInitialDate
				AND l.PeriodEndDate = @PeriodEndDate
				AND l.GroupId = @GroupId
				AND (maD.Id IS NULL)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT 
					999 AS CodeMessage, 'El concepto ' + conc.Code + ' tiene cuentas contables sin parametrizar' AS Message, '' AS Consecutive
				FROM Payroll.IncentivePayment l
			JOIN Payroll.IncentivePaymentDetail ld ON l.Id = ld.IncentivePaymentId
			JOIN Payroll.Concept conc ON conc.Id = ld.ConceptId
			JOIN Payroll.Contract c ON l.ContractId = c.Id
			JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
			LEFT JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
			LEFT JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
			LEFT JOIN GeneralLedger.MainAccounts maD ON maD.LegalBookId = @LegalBookId 
			WHERE l.PeriodInitialDate  = @PeriodInitialDate
				AND l.PeriodEndDate = @PeriodEndDate
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
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			e.ThirdPartyId AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			IIF(ld.AccruedValue > 0 , ld.AccruedValue, 0) AS DebitValue,
			0 AS CreditValue,
			e.Id,
			'Primas'
		FROM Payroll.IncentivePayment l
		JOIN Payroll.IncentivePaymentDetail ld ON l.Id = ld.IncentivePaymentId
		JOIN Payroll.Contract c ON l.ContractId = c.Id
		JOIN Payroll.Employee e on e.Id = c.EmployeeId
		JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN Payroll.Concept CONC ON CONC.Id = LD.ConceptId
		JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.AccruedAccount
		WHERE l.PeriodInitialDate  = @PeriodInitialDate
			AND l.PeriodEndDate = @PeriodEndDate
			AND l.GroupId = @GroupId
			AND CONC.ConceptType <> 3
			AND LD.AccruedValue > 0

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
			'Primas'
		FROM Payroll.IncentivePayment l
		JOIN Payroll.IncentivePaymentDetail ld ON l.Id = ld.IncentivePaymentId
		JOIN Payroll.Contract c ON l.ContractId = c.Id
		JOIN Payroll.Employee e on e.Id = c.EmployeeId
		JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN Payroll.Concept CONC ON CONC.Id = LD.ConceptId
		JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.DeductedAccount
		WHERE l.PeriodInitialDate  = @PeriodInitialDate
			AND l.PeriodEndDate = @PeriodEndDate
			AND l.GroupId = @GroupId
			AND CONC.ConceptType <> 3
			AND LD.DeductedValue > 0

		-- Totalizo la Prima
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
			IIF(@AccountedBy = 1, e.thirdPartyID, @CompanyThirdPartyId) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			0 AS DebitValue,
			SUM(ILD.AccruedValue) - SUM(ILD.DeductedValue) AS CreditValue,
			c.EmployeeId,
			'Primas'
		FROM Payroll.IncentivePayment l	
		JOIN Payroll.IncentivePaymentDetail ILD ON L.Id = ILD.IncentivePaymentId
		JOIN Payroll.Concept CONC ON CONC.Id = ILD.ConceptId	
		JOIN Payroll.Contract c ON l.ContractId = c.Id
		JOIN Payroll.[Group] G ON G.Id = L.GroupId
		JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas ON cas.ConceptId = @IdConceptIncentivePayment AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.DeductedAccount
		JOIN Payroll.Employee E ON E.Id = c.EmployeeId
		WHERE l.PeriodInitialDate  = @PeriodInitialDate
			AND l.PeriodEndDate = @PeriodEndDate
			AND l.GroupId = @GroupId
			AND conc.ConceptType <> 3
		GROUP BY l.Id,ma.Id,ma.Number,IIF(@AccountedBy = 1, c.EmployeeId, @CompanyThirdPartyId),ma.HandlesCostCenter,fu.CostCenterId, c.EmployeeId, e.thirdPartyID
		

		/********************************************** CONTABILIZACIÓN **********************************************/
	    --SELECT * from @CostDistributions
		--SELECT * FROM @CostDistributions t WHERE t.JournalVoucherTypeId = @PayrollJournalVoucherTypeId

		IF EXISTS (SELECT 1 FROM @CostDistributions t WHERE t.JournalVoucherTypeId = @PayrollJournalVoucherTypeId)
		BEGIN
			--Si la moneda del libro oficial es igual a la moneda del parametro del modulo
				--el sistema no debe realizar conversiones ni ajustes, y el comprobante contable debe generarse según la moneda definida en el libro oficial
			--Si son diferentes el sistema  realiza la conversión de valores utilizando la TRM del día de la Fecha de Confirmación [ConfirmationDate] 
			IF @PayrollCurrencyId = @OfficialCurrencyId
			BEGIN
				SELECT @PayrollCurrencyId = NULL, @DateTRM = NULL
			END

		
			INSERT INTO @JournalVourcherTmp
				( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, DateTRM , CurrencyId)
				SELECT TOP 1
					@LegalBookId LegalBookId, 
					@PayrollJournalVoucherTypeId IdJournalVoucher, 
					IIF(PeriodEndDate > [Common].[GETDATE](), [Common].[GETDATE](), PeriodEndDate) VoucherDate, 
					2 Status, 
					'Comprobante Contable de Primas - Grupo: ' + g.Code + ' ' + g.Name + ' - Prima De ' + CAST(CAST(PeriodEndDate AS DATE) AS VARCHAR(20)) Detail,
					NULL EntityCode,
					NULL EntityId,
					'PayrollLiquidation' EntityName,
					@DateTRM DateTRM,
					@PayrollCurrencyId CurrencyId
				FROM Payroll.IncentivePayment ipa, Payroll.[Group] G
				WHERE ipa.PeriodInitialDate = @PeriodInitialDate AND IPA.PeriodEndDate = @PeriodEndDate AND ipa.[Period] = @Period
				AND G.Id = IPA.GroupId and G.Id = @GroupId

			INSERT INTO @JournalVourcherDetailTmp
				( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue )
				SELECT
					t.MainAccountId AS IdMainAccount,
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
				WHERE t.JournalVoucherTypeId = @PayrollJournalVoucherTypeId

			--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
			SELECT @JournalVoucherXML = CONVERT(xml, 
				(
					SELECT * FROM @JournalVourcherTmp JournalVoucher 
					JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
					For xml AUTO,TYPE, ELEMENTS
				)
			)
		
			--Se consume el sp que guarda el movimiento contable
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 

			IF @CodeMessage = '999' 
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT 999 AS CodeMessage, 'No se confirmó el Comprobante Contable de Nómina por ' + @Message AS Message, '' AS Consecutive

				GOTO PRINT_RESULT
			END
			ELSE
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT DISTINCT 0 AS CodeMessage, 'Se Confirmó la prima Correctamente y se generó el Comprobante de Tipo ' + CONCAT(jvt.Code, ' - ', jvt.Name) AS Message, '' AS Consecutive
					FROM GeneralLedger.JournalVoucherTypes jvt
					WHERE jvt.Id = @PayrollJournalVoucherTypeId

				-- ACTUALIZAMOS EL ESTADO DE LA TABLA DE PRIMAS
				UPDATE Payroll.IncentivePayment
				SET RegisterStatus = 2
				WHERE PeriodInitialDate = @PeriodInitialDate AND PeriodEndDate = @PeriodEndDate AND GroupId = @GroupId

			END

			

		END
		ELSE
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No se encontró ningún registro para crear los comprobantes contables de nómina.'  AS Message, '' AS Consecutive
			GOTO PRINT_RESULT
		END

		/********************************************  NOMINA ELECTRONICA ********************************************/
		-- FilePath se inserta vacío; se llena cuando el usuario procesa el documento en Trazabilidad de Nómina Electrónica.

		IF EXISTS (SELECT 1 FROM GeneralLedger.GeneralLedgerSettings WHERE HandlesElectronicPayroll = 1)
		BEGIN
			SET @IncentivePaymentRows = 1
			SET @IncentivePaymentId = 0

			WHILE @IncentivePaymentRows > 0
			BEGIN
				SELECT TOP 1
					@IncentivePaymentId = p.Id,
					-----------------------------------------------------------
					@ThirdPartyId = e.ThirdPartyId,
					@Year = YEAR(p.PeriodEndDate),
					@Month = MONTH(p.PeriodEndDate),
					-----------------------------------------------------------
					@SupportId = NULL
				FROM Payroll.IncentivePayment p
				JOIN Payroll.Contract c ON p.ContractId = c.Id
				JOIN Payroll.Employee e ON c.EmployeeId = e.Id
				LEFT JOIN Payroll.ElectronicPayrollPaymentSupportDetail eppsd ON eppsd.EntityId = p.Id AND 'IncentivePayment' = eppsd.EntityName
				WHERE p.Id > @IncentivePaymentId
					AND p.RegisterStatus = 2
					AND p.GroupId = @GroupId 
					AND p.PeriodInitialDate = @PeriodInitialDate AND p.PeriodEndDate = @PeriodEndDate
					AND eppsd.Id IS NULL
				ORDER BY p.Id

				SET @IncentivePaymentRows = @@ROWCOUNT
				IF @IncentivePaymentRows = 0 
				BEGIN
					BREAK
				END

				---------------------------------------------------------------

				SELECT @SupportId = s.Id
				FROM Payroll.ElectronicPayrollPaymentSupport s
				WHERE @ThirdPartyId = s.EmployeePartyId AND @Year = s.Year AND @Month = s.Month

				IF @SupportId IS NULL
				BEGIN
					UPDATE psd
						SET @SupportConsecutive = psd.Next += 1
					FROM Payroll.PayrollSequenceDetail psd
					WHERE psd.Id = @SupportSequenseDetailId

					SET @SupportConsecutive -= 1

					INSERT INTO Payroll.ElectronicPayrollPaymentSupport
						(Prefix, Consecutive, EmployeePartyId, Year, Month)
						SELECT @SupportPrefix, @SupportConsecutive, @ThirdPartyId, @Year, @Month

					SET @SupportId = SCOPE_IDENTITY()


					INSERT INTO Payroll.ElectronicPayroll
						(
							DocumentType, Year, Month, EmployeePartyId, EntityName, EntityId, Prefix, DocumentNumber, Status, CreationDate,
							FilePath,Consecutive,Retry
						)
						SELECT	1, @Year, @Month, @ThirdPartyId, 'ElectronicPayrollPaymentSupport', @SupportId, @SupportPrefix, @SupportConsecutive, 1, Common.GETDATE(),
								'', 0, 0
				END

				INSERT INTO Payroll.ElectronicPayrollPaymentSupportDetail
						(ElectronicPayrollPaymentSupportId, EntityId, EntityName)
						SELECT @SupportId, @IncentivePaymentId, 'IncentivePayment'
			END
		END
	END TRY
	BEGIN CATCH

		INSERT @TableResult (CodeMessage, Message, Consecutive)
			SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Consecutive

	END CATCH

	PRINT_RESULT:

	SELECT CodeMessage, Message, Consecutive
	FROM @TableResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y contabiliza los comprobantes de diario (vouchers contables) correspondientes al pago de prima de servicios (incentivo salarial) para un grupo de nómina y período determinados. Toma como base los parámetros del grupo de nómina (Payroll.Group y Payroll.PayrollParameter) para identificar el concepto de prima aplicable —ya sea prima de servicios del primer semestre o prima de navidad del segundo semestre— y obtiene las cuentas contables, el tipo de comprobante, el tercero de la empresa y el libro oficial de contabilidad (GeneralLedger.LegalBook). Actualiza la fecha y usuario de confirmación en Payroll.IncentivePayment, construye la cabecera y el detalle del comprobante contable distribuyendo débitos y créditos por empleado y centro de costo, y registra el resultado en contabilidad general. Existe para automatizar el cierre contable de la prima semestral dentro del módulo de nómina, garantizando trazabilidad por período, grupo, usuario y libro oficial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVouchersIncentivePayment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVouchersIncentivePayment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'PayrollSettings debe existir y tener CurrencyId definida.; Debe existir un libro oficial (LegalBook.OfficialBook = 1).; El grupo (@GroupId) debe tener parametrizado IdIncentiveVoucherType (tipo de comprobante de prima) en PayrollParameter.; Las primas para el grupo y período no deben estar previamente confirmadas (IncentivePayment.RegisterStatus <> 2).; Los conceptos involucrados deben tener cuentas contables parametrizadas y distintas para AccruedAccount y DeductedAccount en ConceptAccountingStructure.; Las cuentas contables de los conceptos deben existir en GeneralLedger.MainAccounts para el libro oficial.; Si HandlesElectronicPayroll = 1, debe existir secuencia automática (IdForm=''2243'', IsManual=0) y su detalle en PayrollSequenceDetail.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersIncentivePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante contable de prima solo se genera si existen líneas de distribución para el tipo de comprobante de nómina parametrizado.; Los conceptos con ConceptType = 3 se excluyen de la distribución contable de la prima.; Solo se procesan registros donde AccruedValue > 0 (para el débito) o DeductedValue > 0 (para el crédito).; Si la moneda del módulo coincide con la del libro oficial, no se aplica conversión TRM al comprobante.; El tercero del asiento total de la prima depende de PayrollParameter.AccountedBy: 1=empleado, otro=empresa.; Una prima ya confirmada (RegisterStatus = 2) para el mismo grupo y período no se puede recontabilizar.; Cada IncentivePayment confirmado genera a lo más un detalle de soporte de pago electrónico (controlado por ElectronicPayrollPaymentSupportDetail).; El soporte de nómina electrónica es único por (EmployeePartyId, Year, Month).; Toda ejecución registra ConfirmationDate y ConfirmationUser sobre IncentivePayment antes de validar y contabilizar.; Cualquier excepción en TRY/CATCH se reporta como CodeMessage 999 con el mensaje y línea de error sin abortar la salida del SP.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersIncentivePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prima de servicios; Prima de navidad; Comprobante contable; Libro oficial; Distribución de costos de nómina; Cuentas contables de devengado y deducido; Centro de costo; Estructura contable; Tercero (empleado/empresa); Nómina electrónica; Soporte de pago de nómina electrónica; Secuencia/consecutivo; TRM (conversión de moneda); Confirmación de prima', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersIncentivePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Period = 1 → Toma el concepto de prima desde PayrollParameter.idIncentivePaymentConcept1 o PayrollSettings.ServicesIncentivePaymentConceptId (prima de servicios) else Toma el concepto desde PayrollParameter.idIncentivePaymentConcept2 o PayrollSettings.ChristmasIncentivePaymentConceptId (prima de navidad); si EXISTS GeneralLedger.GeneralLedgerSettings con HandlesElectronicPayroll = 1 → Valida secuencia de soportes de nómina electrónica (IdForm=''2243'', IsManual=0) y al final genera soportes de pago electrónico (ElectronicPayrollPaymentSupport, ElectronicPayroll y ElectronicPayrollPaymentSupportDetail) por cada IncentivePayment confirmado sin soporte previo; si @PayrollCurrencyId = @OfficialCurrencyId → Anula @PayrollCurrencyId y @DateTRM (no se aplica conversión de moneda al comprobante) else Conserva la moneda del módulo y la fecha TRM (ConfirmationDate) para que el comprobante se contabilice con conversión; si EXISTS registros en @CostDistributions con JournalVoucherTypeId = @PayrollJournalVoucherTypeId → Construye XML de cabecera/detalle y ejecuta GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; si éxito, marca IncentivePayment.RegisterStatus = 2 else Reporta CodeMessage 999 ''No se encontró ningún registro para crear los comprobantes contables de nómina.''; si Al consultar ElectronicPayrollPaymentSupport para (EmployeePartyId, Year, Month) no existe soporte previo (@SupportId IS NULL) → Incrementa el consecutivo en PayrollSequenceDetail.Next, inserta nuevo ElectronicPayrollPaymentSupport y registro en ElectronicPayroll else Reutiliza el SupportId existente y solo inserta el detalle en ElectronicPayrollPaymentSupportDetail; si @AccountedBy = 1 (al totalizar la prima) → Usa el ThirdPartyId del empleado como tercero del asiento de la prima total else Usa el tercero de la empresa (Company.ThirdPartyId) como tercero del asiento', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersIncentivePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersIncentivePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.PayrollParameter; Payroll.Group; Payroll.PayrollSettings; Payroll.Company; GeneralLedger.JournalVoucherTypes; GeneralLedger.LegalBook; Payroll.IncentivePayment; GeneralLedger.GeneralLedgerSettings; Payroll.PayrollSequence; Payroll.PayrollSequenceDetail; Common.Sequense; Payroll.ConceptAccountingStructure; Payroll.Concept; Payroll.IncentivePaymentDetail; Payroll.AccountingStructure; Payroll.Contract; Payroll.FunctionalUnit; GeneralLedger.MainAccounts; Payroll.Employee; Common.ThirdParty; GeneralLedger.JournalVouchers; Payroll.ElectronicPayrollPaymentSupport; Payroll.ElectronicPayrollPaymentSupportDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersIncentivePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchersIncentivePayment';
-- GO
