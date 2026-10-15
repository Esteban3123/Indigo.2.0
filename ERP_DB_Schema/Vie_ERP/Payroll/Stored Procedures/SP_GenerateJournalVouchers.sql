-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-19
-- Description:	Procedimiento que se encarga de realizar la contabilización de la nómina
-- =============================================
CREATE PROCEDURE [Payroll].[SP_GenerateJournalVouchers]
		@GroupId AS INT,
		@PayrollEndDate AS DATE,
		@PayrollIntegration AS INT, /*** Por el momento cuando integracion sea 1 (Nativa) */
		@CodeUser AS VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	DECLARE @EmployeeId AS INT, -- Variable usada para validaciones
			@PayrollStartDate AS DATE,
			---------------------------------------
			@SENAThirdPartyId INT,
			@ICBFThirdPartyId INT,
			@CompanyThirdPartyId INT

	DECLARE @ListParafiscalConceptClass AS TABLE (ConceptClass VARCHAR(3))
	DECLARE @ListProvisionConceptClass AS TABLE (ConceptClass VARCHAR(3))
	DECLARE @ListInabilities AS TABLE (ConceptClass VARCHAR(3))
	DECLARE @ListInabilitiesEPS AS TABLE (ConceptClass VARCHAR(3))

	DECLARE @CostDistributions TABLE
	(
		[Id] [int] IDENTITY(1,1) NOT NULL,	
		[LiquidationId] [int] NOT NULL,	
		[LiquidationDetailId] [int] NOT NULL,	
		[JournalVoucherTypeId] [int] NOT NULL,
		[MainAccountId] [int] NULL,
		[MainAccountNumber] [varchar](50) NOT NULL,
		[ThirdPartyId] [int] NULL,
		[CostCenterId] [int] NULL,
		[DebitValue] DECIMAL(18,0) DEFAULT(0),
		[CreditValue] DECIMAL(18,0) DEFAULT(0),
		-----------------------------------------------
		[EmployeeId] INT,
		[NumberHours] INT DEFAULT(0),
		[Detail] VARCHAR(500),
		[BaseValue] DECIMAL(18,2) DEFAULT(0),
		[IdRetention] INT NULL
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
			@PrestacionJournalVoucherTypeId INT,
			@ProvisionJournalVoucherTypeId INT,
			@PayrollAccountId INT,
			@PayrollAccountNumber VARCHAR(20),
			@AccountedBy CHAR(1),
			@InabilityAccountedBy TINYINT,
			@CostDistributionParameter TINYINT,
			@CurrencyID INT

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
		CurrencyId INT,
		TRMDate Date
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

	--Variable para obtener el xml
	DECLARE @JournalVoucherXML as XML,
			@CodeMessage Int,
			@Message Varchar(Max),
			@IdJournalVoucherResult Int

	--Variable para obtener el xml de los costos distribuidos
	DECLARE @CostDistributionsXML as XML

	--tabla temporal para almacenar el resultado del movimiento contable
	DECLARE @resultJournalVoucher TABLE
		(
			code INT, 
			MessageResult VARCHAR(max), 
			IdJournalVoucher INT
		)
	BEGIN TRY
		
		/************************************* ASIGNACIONES SIMPLES ************************************/

		-- Busco cual es el concepto asignado para Retención
		DECLARE @IdRetentionConcept int = 0

		SELECT @IdRetentionConcept = IdRetentionConcepts FROM Payroll.PayrollSettings WITH (NOLOCK)

		if @IdRetentionConcept = 0 BEGIN

			INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT 999 AS CodeMessage, 'No se ha parametrizado Concepto de Retención en la Fuente en el Formulario de Parámetros de Nómina' AS Message, '' AS Consecutive

				GOTO PRINT_RESULT
		END

		SELECT
			@InabilityAccountedBy = ps.InabilityAccountedBy,
			@CostDistributionParameter = ps.PayrollDistribution
		FROM Payroll.PayrollSettings ps WITH (NOLOCK)

		SELECT
			@PayrollStartDate = CASE g.Liquidation
										WHEN 1 THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @PayrollEndDate), 0)
										ELSE IIF(DAY(@PayrollEndDate) > 15, DATEADD(DAY, 15, DATEADD(MONTH, DATEDIFF(MONTH, 0, @PayrollEndDate), 0)), DATEADD(MONTH, DATEDIFF(MONTH, 0, @PayrollEndDate), 0))
									END,
			@PayrollJournalVoucherTypeId = payroll.Id,
			@ProvisionJournalVoucherTypeId = provision.Id,
			@PrestacionJournalVoucherTypeId = prestation.Id,			
			@PayrollAccountId = pp.IdPayrollAccount,
			@PayrollAccountNumber = pp.PayrollAccount,
			@CompanyThirdPartyId = c.ThirdPartyId,
			@AccountedBy = pp.AccountedBy
		FROM Payroll.[Group] g WITH (NOLOCK)
		JOIN Payroll.Company c WITH (NOLOCK) ON g.CompanyId = c.Id
		JOIN Payroll.PayrollParameter pp WITH (NOLOCK) ON g.PayrollParameterId = pp.Id
		LEFT JOIN GeneralLedger.JournalVoucherTypes payroll WITH (NOLOCK) ON pp.PayrollVoucherCode = payroll.Code
		LEFT JOIN GeneralLedger.JournalVoucherTypes prestation WITH (NOLOCK) ON pp.PrestacionVoucherCode = prestation.Code
		LEFT JOIN GeneralLedger.JournalVoucherTypes provision WITH (NOLOCK) ON pp.ProvisionVoucherCode = provision.Code
		WHERE g.Id = @GroupId
		
		SELECT
			@SENAThirdPartyId = tp.Id
		FROM Common.ThirdParty tp WITH (NOLOCK)
		WHERE tp.Nit = '899999034'

		SELECT
			@ICBFThirdPartyId = tp.Id
		FROM Common.ThirdParty tp WITH (NOLOCK)
		WHERE tp.Nit = '899999239'

		SELECT @LegalBookId = lb.Id
		FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
		WHERE lb.OfficialBook = 1

		INSERT INTO @ListParafiscalConceptClass
			SELECT ConceptClass FROM Payroll.GetConceptClassByType(3)
			UNION
			SELECT ConceptClass FROM Payroll.GetConceptClassByType(4)

		INSERT INTO @ListProvisionConceptClass
			SELECT ConceptClass FROM Payroll.GetConceptClassByType(5)

		INSERT INTO @ListInabilities
			SELECT ConceptClass FROM Payroll.GetConceptClassByType(6)
		INSERT INTO @ListInabilitiesEPS
             SELECT ConceptClass FROM Payroll.Concept where ConceptClass in ('068', '070')
		/************************************* VALIDACIONES ************************************/

		IF EXISTS 
		(
			SELECT 1
			FROM Payroll.CostDistribution  cd WITH (NOLOCK)
			WHERE cd.PayrollEndDate = @PayrollEndDate
				AND cd.GroupId = @GroupId
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'Ya existen distribuciones de gasto para el grupo en el periodo seleccionado' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF NOT EXISTS 
		(
			SELECT 1
			FROM Payroll.Liquidation l WITH (NOLOCK)
			WHERE l.PayrollDateLiquidated = @PayrollEndDate
				AND l.GroupId = @GroupId
				AND l.RegisterStatus = 'C'
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No hay liquidaciones confirmadas para el grupo en el periodo seleccionado' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF NOT EXISTS
		(
			SELECT 1
			FROM Payroll.PayrollSettings WITH (NOLOCK)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No se encontraron parámetros de nómina definidos' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF @PayrollJournalVoucherTypeId IS NULL
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No está paremetrizado el Comprobante Contable de Nómina' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF @ProvisionJournalVoucherTypeId IS NULL
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No está parametrizado el Comprobante Contable de Provisiones' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF @PrestacionJournalVoucherTypeId IS NULL
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No está paremetrizado el Comprobante Contable de Prestaciones Sociales' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF @PayrollAccountId IS NULL
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No está parametrizado la cuenta de Nómina del Grupo' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF @LegalBookId IS NULL
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No está parametrizado un libro oficial' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF NOT EXISTS
		(
			SELECT 1
			FROM GeneralLedger.MainAccounts ma WITH (NOLOCK)
			WHERE ma.Id = @PayrollAccountId AND ma.Number = @PayrollAccountNumber AND ma.LegalBookId = @LegalBookId
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'La cuenta de nomina parametrizada no corresponde con el numero de cuenta seleccionado en el grupo' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		-- Validar que los conceptos tengan parametrizados sus respectivas cuentas contables

		IF EXISTS
		(
			SELECT 1
			FROM Payroll.Liquidation l WITH (NOLOCK)
			JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
			JOIN Payroll.ViewDistributionOfScheduledExpenses sd WITH (NOLOCK) ON l.EmployeeId = sd.EmployeeId
				AND ld.ConceptId = sd.ConceptId
				AND sd.DateDetail BETWEEN @PayrollStartDate AND @PayrollEndDate and sd.GroupId = @GroupId
			LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND ma.Number = IIF(ld.ConceptType = '1', sd.AccruedAccount, sd.DeductedAccount)
			WHERE l.PayrollDateLiquidated  = @PayrollEndDate
				AND l.GroupId = @GroupId
				AND l.RegisterStatus = 'C'
				AND ld.DistribuirGasto = 1
				AND ma.Id IS NULL
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'Existen distribuciones de gasto sin cuenta parametrizada' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END

		IF EXISTS
		(
			SELECT 1
			FROM Payroll.ConceptAccountingStructure cas WITH (NOLOCK)
				JOIN Payroll.Concept c WITH (NOLOCK) ON cas.ConceptId = c.Id
				JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON cas.AccountingStructureId = acs.Id
				JOIN PAyroll.LiquidationDetail LD WITH (NOLOCK) ON LD.ConceptId = C.Id
				JOIN Payroll.Liquidation L WITH (NOLOCK) ON LD.PayrollId = L.Id
				WHERE cas.AccruedAccount = cas.DeductedAccount
					AND L.PayrollDateLiquidated = @PayrollEndDate
					AND L.GroupId = @GroupId
					AND L.RegisterStatus = 'C'
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT 999 AS CodeMessage, 'El concepto ' + c.Code + ' tiene la cuenta Débito y Credito igual parametrizada en la estructura ' + CONCAT(acs.Code, ' - ', acs.Description) AS Message, '' AS Consecutive
				FROM Payroll.ConceptAccountingStructure cas WITH (NOLOCK)
				JOIN Payroll.Concept c WITH (NOLOCK) ON cas.ConceptId = c.Id
				JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON cas.AccountingStructureId = acs.Id
				JOIN PAyroll.LiquidationDetail LD WITH (NOLOCK) ON LD.ConceptId = C.Id
				JOIN Payroll.Liquidation L WITH (NOLOCK) ON LD.PayrollId = L.Id
				WHERE cas.AccruedAccount = cas.DeductedAccount
					AND L.PayrollDateLiquidated = @PayrollEndDate
					AND L.GroupId = @GroupId
					AND L.RegisterStatus = 'C'

			GOTO PRINT_RESULT
		END

		IF EXISTS
		(
			SELECT 1
			FROM Payroll.Liquidation l WITH (NOLOCK)
			JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
			JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
			JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
			LEFT JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
			LEFT JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
			LEFT JOIN @ListProvisionConceptClass AS PR ON ld.ConceptClass = PR.ConceptClass
			LEFT JOIN @ListParafiscalConceptClass AS PA ON ld.ConceptClass = PA.ConceptClass
			LEFT JOIN @ListInabilities AS I ON ld.ConceptClass = I.ConceptClass
			LEFT JOIN GeneralLedger.MainAccounts maD WITH (NOLOCK) ON maD.LegalBookId = @LegalBookId AND
				(
					(I.ConceptClass IS NULL AND maD.Number = cas.AccruedAccount) 
					OR 
					(I.ConceptClass IS NOT NULL AND maD.Number = cas.InabilityDebitValueEmployeeAccount  OR maD.Number =  cas.InabilityDebitValueEPSAccount)
					OR
					(ld.ConceptType = 2 AND PA.ConceptClass IS NULL AND PR.ConceptClass IS NULL AND maD.Number = cas.DeductedAccount)
				)
			LEFT JOIN GeneralLedger.MainAccounts maC ON maC.LegalBookId = @LegalBookId AND
				(
					(I.ConceptClass IS NULL AND maC.Number = cas.DeductedAccount)
					OR 
					(I.ConceptClass IS NOT NULL AND maC.Number = cas.InabilityDebitValueEPSAccount)
				)
			WHERE l.PayrollDateLiquidated  = @PayrollEndDate
				AND l.GroupId = @GroupId
				AND l.RegisterStatus = 'C'
				AND (maD.Id IS NULL OR maC.Id IS NULL)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT 
					999 AS CodeMessage, 'El concepto ' + ld.ConceptCode + ' tiene cuentas contables sin parametrizar' AS Message, '' AS Consecutive
				FROM Payroll.Liquidation l WITH (NOLOCK)
				JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId				
				JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
				JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
				LEFT JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
				LEFT JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
				LEFT JOIN @ListProvisionConceptClass AS PR ON ld.ConceptClass = PR.ConceptClass
				LEFT JOIN @ListParafiscalConceptClass AS PA ON ld.ConceptClass = PA.ConceptClass
				LEFT JOIN @ListInabilities AS I ON ld.ConceptClass = I.ConceptClass
				LEFT JOIN GeneralLedger.MainAccounts maD WITH (NOLOCK) ON maD.LegalBookId = @LegalBookId AND
					(
						(I.ConceptClass IS NULL AND maD.Number = cas.AccruedAccount) 
						OR 
						(I.ConceptClass IS NOT NULL AND  (maD.Number = cas.InabilityDebitValueEmployeeAccount  OR maD.Number =  cas.InabilityDebitValueEPSAccount)) 
						OR
						(ld.ConceptType = 2 AND PA.ConceptClass IS NULL AND PR.ConceptClass IS NULL AND maD.Number = cas.DeductedAccount)
					)
				LEFT JOIN GeneralLedger.MainAccounts maC ON maC.LegalBookId = @LegalBookId AND
					(
						(I.ConceptClass IS NULL AND maC.Number = cas.DeductedAccount)
						OR 
						(I.ConceptClass IS NOT NULL AND maC.Number = cas.InabilityDebitValueEPSAccount)
					)
				WHERE l.PayrollDateLiquidated  = @PayrollEndDate
					AND l.GroupId = @GroupId
					AND l.RegisterStatus = 'C'
					AND (maD.Id IS NULL OR maC.Id IS NULL)

			GOTO PRINT_RESULT
		END

		-- Existan los terceros solo para Colombia

		IF EXISTS (SELECT 1 FROM GeneralLedger.LegalBook lb
		JOIN Common.Currency c ON c.Id = lb.OfficialCurrencyId
		WHERE c.Abbreviation = 'COP')
		BEGIN
			IF @SENAThirdPartyId IS NULL
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT 999 AS CodeMessage, 'No está creado el tercero del SENA' AS Message, '' AS Consecutive

				GOTO PRINT_RESULT
			END

			IF @ICBFThirdPartyId IS NULL
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT 999 AS CodeMessage, 'No está creado el tercero del ICBF' AS Message, '' AS Consecutive

				GOTO PRINT_RESULT
			END
		END


		IF EXISTS
		(
			SELECT 
				1
			FROM Payroll.Liquidation l WITH (NOLOCK)
			JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
			JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
			JOIN Payroll.Contract c ON l.ContractId = c.Id
			LEFT JOIN Payroll.FundContract fc WITH (NOLOCK) ON c.Id = fc.ContractId 
				AND fc.State = 1
				AND 
				(
					(ld.ConceptClass = '009' AND fc.FundType = 4)
					OR
					(ld.ConceptClass = '036' AND fc.FundType = 5)				
				)
			LEFT JOIN Payroll.Fund f WITH (NOLOCK) ON 
				(
					(ld.ConceptClass = '009' AND fc.FundId = f.Id)
					OR
					(ld.ConceptClass = '014' AND l.PensionFundId = f.Id)
					OR
					(ld.ConceptClass = '015' AND l.PensionFundId = f.Id)
					OR
					(ld.ConceptClass = '016' AND l.VoluntaryPensionFundId = f.Id)
					OR
					(ld.ConceptClass = '017' AND l.HealthFundId = f.Id)
					OR
					(ld.ConceptClass = '018' AND l.HealthFundId = f.Id)
					OR
					(ld.ConceptClass = '019' AND l.VoluntaryHealthFundId = f.Id)
					OR
					(ld.ConceptClass = '021' AND l.HealthFundId = f.Id)
					OR
					(ld.ConceptClass = '022' AND l.HealthFundId = f.Id)
					OR
					(ld.ConceptClass = '023' AND l.HealthFundId = f.Id)
					OR
					(ld.ConceptClass = '027' AND l.HealthFundId = f.Id)
					OR
                    (ld.ConceptClass = '075' AND l.HealthFundId = f.Id)
                    OR
                    (ld.ConceptClass = '076' AND l.HealthFundId = f.Id)
					OR
					(ld.ConceptClass = '036' AND fc.FundId = f.Id)
					OR
					(ld.ConceptClass = '038' AND l.PensionFundId = f.Id)
				)
			WHERE l.PayrollDateLiquidated  = @PayrollEndDate
				AND l.GroupId = @GroupId
				AND l.RegisterStatus = 'C'
				AND ld.ConceptClass IN ('009', '014', '015', '016', '017', '018', '019', '021', '022', '023', '027', '036', '038', '075','076')
				AND f.ThirdPartyId IS NULL
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT
					999 AS CodeMessage, 'El empleado ' + tp.Nit + ' - ' + tp.Name + ' no tiene una entidad de ' + 
						CASE ld.ConceptClass 
							WHEN '009' THEN 'ARL'
							WHEN '014' THEN 'PENSION'
							WHEN '015' THEN 'PENSION'
							WHEN '016' THEN 'APORTE VOLUNTARIO DE PENSION'
							WHEN '017' THEN 'SALUD'
							WHEN '018' THEN 'SALUD'
							WHEN '019' THEN 'APORTE VOLUNTARIO DE SALUD'
							WHEN '021' THEN 'SALUD'
							WHEN '022' THEN 'SALUD'
							WHEN '023' THEN 'SALUD'
							WHEN '027' THEN 'SALUD'
							WHEN '075' THEN 'SALUD'
							WHEN '076' THEN 'SALUD'
							WHEN '036' THEN 'CCF'
							WHEN '038' THEN 'PENSION'
							ELSE ''
						END + ' válida' AS Message, '' AS Consecutive
				FROM Payroll.Liquidation l WITH (NOLOCK)
				JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
				JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
				JOIN Common.ThirdParty tp WITH (NOLOCK) ON e.ThirdPartyId = tp.Id
				JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
				LEFT JOIN Payroll.FundContract fc WITH (NOLOCK) ON c.Id = fc.ContractId 
					AND fc.State = 1
					AND 
					(
						(ld.ConceptClass = '009' AND fc.FundType = 4)
						OR
						(ld.ConceptClass = '036' AND fc.FundType = 5)				
					)
				LEFT JOIN Payroll.Fund f WITH (NOLOCK) ON 
					(
						(ld.ConceptClass = '009' AND fc.FundId = f.Id)
						OR
						(ld.ConceptClass = '014' AND l.PensionFundId = f.Id)
						OR
						(ld.ConceptClass = '015' AND l.PensionFundId = f.Id)
						OR
						(ld.ConceptClass = '016' AND l.VoluntaryPensionFundId = f.Id)
						OR
						(ld.ConceptClass = '017' AND l.HealthFundId = f.Id)
						OR
						(ld.ConceptClass = '018' AND l.HealthFundId = f.Id)
						OR
						(ld.ConceptClass = '019' AND l.VoluntaryHealthFundId = f.Id)
						OR
						(ld.ConceptClass = '021' AND l.HealthFundId = f.Id)
						OR
						(ld.ConceptClass = '022' AND l.HealthFundId = f.Id)
						OR
						(ld.ConceptClass = '023' AND l.HealthFundId = f.Id)
						OR
						(ld.ConceptClass = '027' AND l.HealthFundId = f.Id)
						OR
						(ld.ConceptClass = '075' AND l.HealthFundId = f.Id)
						OR
						(ld.ConceptClass = '076' AND l.HealthFundId = f.Id)
						OR
						(ld.ConceptClass = '036' AND fc.FundId = f.Id)
						OR
						(ld.ConceptClass = '038' AND l.PensionFundId = f.Id)
					)
				WHERE l.PayrollDateLiquidated  = @PayrollEndDate
					AND l.GroupId = @GroupId
					AND l.RegisterStatus = 'C'
					AND ld.ConceptClass IN ('009', '014', '015', '016', '017', '018', '019', '021', '022', '023', '027', '036', '038', '075','076')
					AND f.ThirdPartyId IS NULL

			GOTO PRINT_RESULT
		END

		IF EXISTS
		(
			SELECT 
				1
			FROM Payroll.Liquidation l WITH (NOLOCK)
			JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
			LEFT JOIN Payroll.AgreementsC ac WITH (NOLOCK) ON 
			(
				(ld.ConceptClass = '041' AND ld.AgreementsId = ac.Id)
			)
			LEFT JOIN Payroll.Company co WITH (NOLOCK) ON ac.CompanyId = co.Id
			WHERE l.PayrollDateLiquidated  = @PayrollEndDate
				AND l.GroupId = @GroupId
				AND l.RegisterStatus = 'C'
				AND ld.ConceptClass IN ('041')
				AND co.ThirdPartyId IS NULL
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT
					999 AS CodeMessage, 'El empleado ' + tp.Nit + ' no tiene una entidad de LIBRANZA definido para el concepto ' + ld.ConceptCode AS Message, '' AS Consecutive
				FROM Payroll.Liquidation l WITH (NOLOCK)
				JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
				JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
				JOIN Common.ThirdParty tp WITH (NOLOCK) ON e.ThirdPartyId = tp.Id
				LEFT JOIN Payroll.AgreementsC ac WITH (NOLOCK) ON 
				(
					(ld.ConceptClass = '041' AND ld.AgreementsId = ac.Id)
				)
				LEFT JOIN Payroll.Company co WITH (NOLOCK) ON ac.CompanyId = co.Id
				WHERE l.PayrollDateLiquidated  = @PayrollEndDate
					AND l.GroupId = @GroupId
					AND l.RegisterStatus = 'C'
					AND ld.ConceptClass IN ('041')
					AND co.ThirdPartyId IS NULL

			GOTO PRINT_RESULT
		END

		IF EXISTS
		(
			SELECT 
				1
			FROM Payroll.Liquidation l WITH (NOLOCK)
			JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
			LEFT JOIN Payroll.TradeUnion tu WITH (NOLOCK) ON 
			(
				(ld.ConceptClass = '044' AND ld.ConceptId = tu.PayrollConceptId)
			)
			WHERE l.PayrollDateLiquidated  = @PayrollEndDate
				AND l.GroupId = @GroupId
				AND l.RegisterStatus = 'C'
				AND ld.ConceptClass IN ('044')
				AND tu.IdThirdParty IS NULL
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT
					999 AS CodeMessage, 'El empleado ' + tp.Nit + ' no tiene una entidad de SINDICATO definido para el concepto ' + ld.ConceptCode AS Message, '' AS Consecutive
				FROM Payroll.Liquidation l WITH (NOLOCK)
				JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
				JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
				JOIN Common.ThirdParty tp WITH (NOLOCK) ON e.ThirdPartyId = tp.Id
				LEFT JOIN Payroll.TradeUnion tu WITH (NOLOCK) ON 
				(
					(ld.ConceptClass = '044' AND ld.ConceptId = tu.PayrollConceptId)
				)
				WHERE l.PayrollDateLiquidated  = @PayrollEndDate
					AND l.GroupId = @GroupId
					AND l.RegisterStatus = 'C'
					AND ld.ConceptClass IN ('044')
					AND tu.IdThirdParty IS NULL

			GOTO PRINT_RESULT
		END
		
		IF @CompanyThirdPartyId IS NULL
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No está creado el tercero de la empresa' AS Message, '' AS Consecutive

			GOTO PRINT_RESULT
		END
			
		/*************************************** PROCESO CONCEPTOS QUE DISTRIBUYEN GASTOS  ************************************/
		
		INSERT INTO @CostDistributions 
		(
			LiquidationId,
			LiquidationDetailId,
			JournalVoucherTypeId,
			MainAccountId,
			MainAccountNumber,
			ThirdPartyId,
			CostCenterId,
			DebitValue,
			CreditValue,
			-----------------------------------------------
			EmployeeId,
			NumberHours,
			Detail
		)
		SELECT 
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number MainAccountNumber,
			e.ThirdPartyId AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, sd.CostCenterId, NULL) AS CostCenterId,
			IIF(ld.ConceptType = '1', (ld.ConceptTotalValue / tsd.TotalHours * sd.TotalHours), 0) AS DebitValue,
			IIF(ld.ConceptType = '1', 0, (ld.ConceptTotalValue / tsd.TotalHours * sd.TotalHours)) AS CreditValue,
			l.EmployeeId,
			sd.TotalHours NumberHours,
			'Distribucion de gastos'
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN
		(
			SELECT sd.EmployeeId, sd.ConceptId, sd.AccruedAccount, sd.DeductedAccount, sd.CostCenterId, SUM(sd.TotalHours) TotalHours
			FROM Payroll.ViewDistributionOfScheduledExpenses sd WITH (NOLOCK)
			WHERE sd.DateDetail BETWEEN @PayrollStartDate AND @PayrollEndDate and sd.EmployeeId = ISNULL(@EmployeeId, sd.EmployeeId)
			GROUP BY sd.EmployeeId, sd.ConceptId, sd.AccruedAccount, sd.DeductedAccount, sd.CostCenterId
		) sd ON l.EmployeeId = sd.EmployeeId 
			AND ld.ConceptId = sd.ConceptId
		JOIN
		(
			SELECT sd.EmployeeId, sd.ConceptId, SUM(sd.TotalHours) TotalHours
			FROM Payroll.ViewDistributionOfScheduledExpenses sd WITH (NOLOCK)
			WHERE sd.DateDetail BETWEEN @PayrollStartDate AND @PayrollEndDate AND sd.EmployeeId = ISNULL(@EmployeeId, sd.EmployeeId)
			GROUP BY sd.EmployeeId, sd.ConceptId			
		) tsd ON l.EmployeeId = tsd.EmployeeId AND ld.ConceptId = tsd.ConceptId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND ma.Number = IIF(ld.ConceptType = '1', sd.AccruedAccount, sd.DeductedAccount)
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'
			AND ld.DistribuirGasto = 1

		-- Ajustamos el valor para que el valor distribuido sea igual al valor del concepto
		SET @Rows_LiquidationDetail = 1
		SET @LiquidationDetailId = 0

		WHILE @Rows_LiquidationDetail > 0
		BEGIN
			SELECT TOP 1 
				@LiquidationDetailId = ld.Id,
				@AdjustedValue = ld.ConceptTotalValue - SUM(IIF(ld.ConceptType = '1', c.DebitValue, c.CreditValue)),
				@Nature = IIF(ld.ConceptType = '1', 1, 2)
			FROM Payroll.LiquidationDetail ld WITH (NOLOCK)
			JOIN @CostDistributions c ON ld.Id = c.LiquidationDetailId
			WHERE ld.Id > @LiquidationDetailId
			GROUP BY ld.Id, ld.ConceptType, ld.ConceptTotalValue
			HAVING ld.ConceptTotalValue <> SUM(IIF(ld.ConceptType = '1', c.DebitValue, c.CreditValue))
			ORDER BY ld.Id

			-- Verifico que haya encontrado un resultado
			SET @Rows_LiquidationDetail = @@RowCount
			IF @Rows_LiquidationDetail = 0
			BEGIN
				BREAK
			END

			SET @Rows_CostDistribution = 1
			SET @CostDistributionId = 0
			
			WHILE @Rows_CostDistribution > 0
			BEGIN
				SELECT TOP 1 
					@CostDistributionId = c.Id,
					@CostDistributionAdjusted = IIF(@Nature = 1, c.DebitValue, c.CreditValue)
				FROM @CostDistributions c
				WHERE c.LiquidationDetailId = @LiquidationDetailId
					AND c.Id > @CostDistributionId				
				ORDER BY c.Id

				-- Verifico que haya encontrado un resultado
				SET @Rows_CostDistribution = @@RowCount
				IF @AdjustedValue = 0 OR @Rows_CostDistribution = 0
				BEGIN
					BREAK
				END

				SET @CostDistributionAdjusted = IIF(@AdjustedValue > 0, @AdjustedValue, IIF(ABS(@AdjustedValue) > @CostDistributionAdjusted, @CostDistributionAdjusted * -1, @AdjustedValue))

				UPDATE c
					SET c.DebitValue = c.DebitValue + IIF(@Nature = 1, @CostDistributionAdjusted, 0),
						c.CreditValue = c.CreditValue + IIF(@Nature = 1, 0, @CostDistributionAdjusted)
				FROM @CostDistributions c
				WHERE c.Id = @CostDistributionId
				
				SET @AdjustedValue = @AdjustedValue - @CostDistributionAdjusted
			END
		END

		/*************************************** PROCESO DE INCAPACIDADES ************************************/

		INSERT INTO @CostDistributions 
		(
			LiquidationId,
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
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			IIF(ma.Number = cas.InabilityDebitValueEmployeeAccount, e.ThirdPartyId, @CompanyThirdPartyId) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			IIF(ma.Number = cas.InabilityDebitValueEmployeeAccount, ld.SpendingInability, 0) AS DebitValue,
			IIF(ma.Number = cas.InabilityDebitValueEmployeeAccount, 0, ld.SpendingInability) AS CreditValue,
			l.EmployeeId,
			'Incapacidades'
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.InabilityDebitValueEmployeeAccount
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'
			AND ld.SpendingInability > 0
			AND ld.ConceptClass NOT IN ('068','076')

		/*************************************** DEDUCCIONES  ************************************/

		INSERT INTO @CostDistributions 
		(
			LiquidationId,
			LiquidationDetailId,
			JournalVoucherTypeId,
			MainAccountId,
			MainAccountNumber,
			ThirdPartyId,
			CostCenterId,
			DebitValue,
			CreditValue,
			EmployeeId,
			Detail,
			BaseValue,
			IdRetention
		)
		SELECT 
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			CASE ld.ConceptClass
				WHEN '014' THEN f.ThirdPartyId
				WHEN '016' THEN f.ThirdPartyId
				WHEN '017' THEN f.ThirdPartyId
				WHEN '019' THEN f.ThirdPartyId
				WHEN '038' THEN f.ThirdPartyId
				WHEN '041' THEN co.ThirdPartyId
				WHEN '044' THEN tu.IdThirdParty
				ELSE e.ThirdPartyId
			END AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			0 AS DebitValue,
			ld.ConceptTotalValue AS CreditValue,
			l.EmployeeId,
			'Deducciones',
			IIF(LD.ConceptCode = '701',LD.RetentionBase,0) AS BaseValue,
			IIF(LD.ConceptCode = '701', @IdRetentionConcept, NULL) as IdRetentionConcept 
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.DeductedAccount
		LEFT JOIN Payroll.Fund f WITH (NOLOCK) ON 
			(
				(ld.ConceptClass = '014' AND l.PensionFundId = f.Id)
				OR
				(ld.ConceptClass = '016' AND l.VoluntaryPensionFundId = f.Id)
				OR
				(ld.ConceptClass = '017' AND l.HealthFundId = f.Id)
				OR
				(ld.ConceptClass = '019' AND l.VoluntaryHealthFundId = f.Id)
				OR
				(ld.ConceptClass = '038' AND l.PensionFundId = f.Id)
			)
		LEFT JOIN Payroll.AgreementsC ac WITH (NOLOCK) ON 
			(
				(ld.ConceptClass = '041' AND ld.AgreementsId = ac.Id)
			)
		LEFT JOIN Payroll.Company co WITH (NOLOCK) ON ac.CompanyId = co.Id
		LEFT JOIN Payroll.TradeUnion tu WITH (NOLOCK) ON 
			(
				(ld.ConceptClass = '044' AND ld.ConceptId = tu.PayrollConceptId)
			)
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'
			AND ld.DistribuirGasto = 0
			AND ld.ConceptType = 2
			AND ld.ConceptClass NOT IN
			(
				SELECT T.ConceptClass
				FROM @ListProvisionConceptClass AS T
				UNION
				SELECT T.ConceptClass
				FROM @ListParafiscalConceptClass AS T
				UNION
				SELECT T.ConceptClass
				FROM @ListInabilities AS T
			)

		/*************************************** DEVENGOS  ************************************/

		INSERT INTO @CostDistributions 
		(
			LiquidationId,
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
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			CASE ld.ConceptClass
				WHEN '014' THEN f.ThirdPartyId
				WHEN '016' THEN f.ThirdPartyId
				WHEN '017' THEN f.ThirdPartyId
				WHEN '019' THEN f.ThirdPartyId
				WHEN '038' THEN f.ThirdPartyId
				WHEN '041' THEN co.ThirdPartyId
				WHEN '044' THEN tu.IdThirdParty
				ELSE e.ThirdPartyId
			END AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			IIF(cas.AccruedAccount = ma.Number, ld.ConceptTotalValue, 0) AS DebitValue,
			IIF(cas.AccruedAccount = ma.Number, 0, ld.ConceptTotalValue) AS CreditValue,
			l.EmployeeId,
			'Devengos'
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND (cas.AccruedAccount = ma.Number OR (cas.DeductedAccount = ma.Number AND cas.DeductedAccount <> @PayrollAccountNumber))
		LEFT JOIN Payroll.Fund f WITH (NOLOCK) ON 
			(
				(ld.ConceptClass = '014' AND l.PensionFundId = f.Id)
				OR
				(ld.ConceptClass = '016' AND l.VoluntaryPensionFundId = f.Id)
				OR
				(ld.ConceptClass = '017' AND l.HealthFundId = f.Id)
				OR
				(ld.ConceptClass = '019' AND l.VoluntaryHealthFundId = f.Id)
				OR
				(ld.ConceptClass = '038' AND l.PensionFundId = f.Id)
			)
		LEFT JOIN Payroll.AgreementsC ac WITH (NOLOCK) ON 
			(
				(ld.ConceptClass = '041' AND ld.AgreementsId = ac.Id)
			)
		LEFT JOIN Payroll.Company co WITH (NOLOCK) ON ac.CompanyId = co.Id
		LEFT JOIN Payroll.TradeUnion tu WITH (NOLOCK) ON 
			(
				(ld.ConceptClass = '044' AND ld.ConceptId = tu.PayrollConceptId)
			)
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'
			AND ld.DistribuirGasto = 0
			AND ld.ConceptType = 1
			AND ld.ConceptClass NOT IN
			(
				SELECT T.ConceptClass
				FROM @ListProvisionConceptClass AS T
				UNION
				SELECT T.ConceptClass
				FROM @ListParafiscalConceptClass AS T
				UNION
				SELECT T.ConceptClass
				FROM @ListInabilities AS T
			) and ld.ConceptClass not in ('068','069')

		/*************************************** NOMINA  ************************************/

		INSERT INTO @CostDistributions 
		(
			LiquidationId,
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
			ld.LiquidationId,
			ld.LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ld.MainAccountId,
			ld.Number,
			ld.ThirdPartyId,
			ld.CostCenterId,
			IIF(ld.TotalDeducted > ld.Value, ld.TotalDeducted - ld.Value, 0) AS DebitValue,
			IIF(ld.Value > ld.TotalDeducted, ld.Value - ld.TotalDeducted, 0) AS CreditValue,
			ld.EmployeeId,
			'Nomina'
		FROM
		(
			SELECT 
				l.Id LiquidationId,
				MAX(ld.Id) AS LiquidationDetailId,
				ma.Id AS MainAccountId,
				ma.Number,
				IIF(@AccountedBy = 1, e.ThirdPartyId, @CompanyThirdPartyId) AS ThirdPartyId,
				IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
				l.TotalDeducted,
				SUM(IIF(T.ConceptClass IS NULL, ld.ConceptTotalValue, ld.SpendingInability)) Value,
				l.EmployeeId
			FROM Payroll.Liquidation l WITH (NOLOCK)
			JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
			JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
			JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
			JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
			JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
			JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
			LEFT JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND
				(
					(cas.DeductedAccount = @PayrollAccountNumber AND cas.DeductedAccount = ma.Number)
					OR 
					(T.ConceptClass IS NOT NULL AND ma.Id = @PayrollAccountId)
				)
			WHERE l.PayrollDateLiquidated  = @PayrollEndDate
				AND l.GroupId = @GroupId
				AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
				AND l.RegisterStatus = 'C'
				AND ld.ConceptType = 1
				AND ld.ConceptClass NOT IN
				(
					SELECT T.ConceptClass
					FROM @ListProvisionConceptClass AS T
					UNION
					SELECT T.ConceptClass
					FROM @ListParafiscalConceptClass AS T
				)
				 AND (
					-- Si ConceptClass NO está en @ListInabilitiesEPS, incluirlo siempre
					ld.ConceptClass NOT IN (SELECT T.ConceptClass FROM @ListInabilitiesEPS AS T)
					-- Si ConceptClass está en @ListInabilitiesEPS, incluirlo solo si NO hay otros conceptos que NO estén en @ListInabilitiesEPS
					OR
					(
						ld.ConceptClass IN (SELECT T.ConceptClass FROM @ListInabilitiesEPS AS T)
						AND NOT EXISTS (
								SELECT 1
								FROM Payroll.LiquidationDetail ld2 WITH (NOLOCK)
								WHERE ld2.PayrollId = l.Id AND ld2.ConceptType = 1 AND ld2.ConceptClass NOT IN (SELECT T.ConceptClass FROM @ListInabilitiesEPS AS T)
						)
					)
					)
			GROUP BY l.Id, l.EmployeeId, l.TotalDeducted, ma.Id, ma.Number, ma.HandlesCostCenter, IIF(@AccountedBy = 1, e.ThirdPartyId, @CompanyThirdPartyId), fu.CostCenterId
		) ld
		
		IF EXISTS 
		(
			SELECT 1 
			FROM @CostDistributions cd
			LEFT JOIN @CostDistributions cdn ON cd.LiquidationId = cdn.LiquidationId AND cdn.Detail = 'Nomina'
			WHERE cd.Detail = 'Deducciones' AND cdn.Id IS NULL
		)
		BEGIN
			-- Llevamos las deducciones contra otros devengos
			SET @Rows_Liquidation = 1
			SET @LiquidationId = 0

			WHILE @Rows_Liquidation > 0
			BEGIN
				SELECT TOP 1 
					@LiquidationId = cd.LiquidationId,
					@TotalDeducted = SUM(cd.CreditValue)
				FROM @CostDistributions cd
				LEFT JOIN @CostDistributions cdn ON cd.LiquidationId = cdn.LiquidationId AND cdn.Detail = 'Nomina'
				WHERE cd.Detail = 'Deducciones' AND cdn.Id IS NULL
					AND cd.LiquidationId > @LiquidationId
				GROUP BY cd.LiquidationId
				ORDER BY cd.LiquidationId

				-- Verifico que haya encontrado un resultado
				SET @Rows_Liquidation = @@RowCount
				IF @Rows_Liquidation = 0
				BEGIN
					BREAK
				END

				-- Ajustamos el valor para que el valor distribuido sea igual al valor del concepto
				SET @Rows_LiquidationDetail = 1
				SET @LiquidationDetailId = 0

				WHILE @Rows_LiquidationDetail > 0
				BEGIN
					SELECT TOP 1 
						@LiquidationDetailId = cd.Id,
						@AdjustedValue = IIF(@TotalDeducted > cd.CreditValue, cd.CreditValue, @TotalDeducted)
					FROM @CostDistributions cd
					WHERE cd.LiquidationId = @LiquidationId AND cd.Detail = 'Devengos' AND cd.CreditValue > 0
						AND cd.Id > @LiquidationDetailId
					ORDER BY cd.Id

					-- Verifico que haya encontrado un resultado
					SET @Rows_LiquidationDetail = @@RowCount
					IF @Rows_LiquidationDetail = 0 OR @AdjustedValue = 0
					BEGIN
						BREAK
					END

					--Insertamos el registro de nómina
					INSERT INTO @CostDistributions 
					(
						LiquidationId,
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
						cd.LiquidationId,
						cd.LiquidationDetailId,
						@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
						cd.MainAccountId,
						cd.MainAccountNumber,
						cd.ThirdPartyId,
						cd.CostCenterId,
						@AdjustedValue AS DebitValue,
						0 AS CreditValue,
						cd.EmployeeId,
						'Nomina'
					FROM @CostDistributions cd
					WHERE cd.Id = @LiquidationDetailId

					--Disminuimos el valor por deducir
					SET @TotalDeducted = @TotalDeducted - @AdjustedValue
				END
			END
		END

		/*************************************** INCAPACIDADES EPS - unificado ************************************/

		INSERT INTO @CostDistributions 
		(
			LiquidationId,
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
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, IIF(@InabilityAccountedBy = 1, e.ThirdPartyId, f.ThirdPartyId), e.ThirdPartyId) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, ld.ConceptTotalValue, 0) AS DebitValue,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, 0, (ld.ConceptTotalValue - ld.SpendingInability)) AS CreditValue,
			l.EmployeeId,
			'Incapacidades EPS'
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND (ma.Id = @PayrollAccountId OR ma.Number = cas.InabilityDebitValueEPSAccount)
		JOIN Payroll.Fund f WITH (NOLOCK) ON l.HealthFundId = f.Id
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'
			AND (ld.ConceptTotalValue - ld.SpendingInability) > 0
			and ld.ConceptClass NOT IN ('027')

	   
	   /*************************************** INCAPACIDADES EPS - unico ************************************/
		
		
		INSERT INTO @CostDistributions 
		(
			LiquidationId,
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
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, IIF(@InabilityAccountedBy = 1, e.ThirdPartyId, f.ThirdPartyId), e.ThirdPartyId) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,

			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, ld.ConceptTotalValue, 0) AS DebitValue,
		    IIF(ma.Number = cas.InabilityDebitValueEPSAccount, 0, ld.ConceptTotalValue ) AS CreditValue,


			l.EmployeeId,
			'Incapacidades EPS'
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND (ma.Id = @PayrollAccountId OR ma.Number = cas.InabilityDebitValueEPSAccount)
		JOIN Payroll.Fund f WITH (NOLOCK) ON l.HealthFundId = f.Id
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'
			AND ld.ConceptClass NOT IN ('027','076','069','067','075')


		/*************************************** INCAPACIDADES ARL - unificado ************************************/

		INSERT INTO @CostDistributions 
		(
			LiquidationId,
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
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, IIF(@InabilityAccountedBy = 1, e.ThirdPartyId, f.ThirdPartyId), e.ThirdPartyId) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, (ld.ConceptTotalValue - ld.SpendingInability), 0) AS DebitValue,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, 0, (ld.ConceptTotalValue - ld.SpendingInability)) AS CreditValue,
			l.EmployeeId,
			'Incapacidades ARL'
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND (ma.Id = @PayrollAccountId OR ma.Number = cas.InabilityDebitValueEPSAccount)
		JOIN Payroll.Fund f WITH (NOLOCK) ON l.HealthFundId = f.Id
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'
			AND (ld.ConceptTotalValue - ld.SpendingInability) > 0
			and ld.ConceptClass = '027'



			/*************************************** INCAPACIDADES ARL unico ************************************/
		INSERT INTO @CostDistributions 
		(
			LiquidationId,
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
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@PayrollJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, IIF(@InabilityAccountedBy = 1, e.ThirdPartyId, f.ThirdPartyId), e.ThirdPartyId) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, ld.ConceptTotalValue, 0) AS DebitValue,
			IIF(ma.Number = cas.InabilityDebitValueEPSAccount, 0, (ld.ConceptTotalValue - ld.SpendingInability)) AS CreditValue,
			l.EmployeeId,
			'Incapacidades ARL'
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND  ma.Number = cas.InabilityDebitValueEPSAccount
		join Payroll.FundContract as fc on fc.ContractId = l.ContractId 
		JOIN Payroll.Fund f WITH (NOLOCK) ON fc.FundId = f.Id and f.Risk =1
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'
			and ld.ConceptClass in ('076') 
		/*************************************** PROCESO DE PROVISION  ************************************/

		INSERT INTO @CostDistributions 
		(
			LiquidationId,
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
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@ProvisionJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			e.ThirdPartyId AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			IIF(ma.Number = cas.AccruedAccount, ld.ConceptTotalValue, 0) AS DebitValue,
			IIF(ma.Number = cas.AccruedAccount, 0, ld.ConceptTotalValue) AS CreditValue,
			l.EmployeeId,
			'Provision'
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN @ListProvisionConceptClass AS T ON ld.ConceptClass = T.ConceptClass
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND (ma.Number = cas.AccruedAccount OR ma.Number = cas.DeductedAccount)
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'

		/*************************************** PROCESO DE PARAFISCALES  ************************************/

		INSERT INTO @CostDistributions 
		(
			LiquidationId,
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
			ld.PayrollId LiquidationId,
			ld.Id AS LiquidationDetailId,
			@PrestacionJournalVoucherTypeId AS JournalVoucherTypeId,
			ma.Id AS MainAccountId,
			ma.Number,
			CASE ld.ConceptClass
				WHEN '035' THEN ISNULL(@SENAThirdPartyId, f.ThirdPartyId)
				WHEN '037' THEN ISNULL(@ICBFThirdPartyId, f.ThirdPartyId)
				ELSE f.ThirdPartyId					
			END AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) AS CostCenterId,
			IIF(ma.Number = cas.AccruedAccount, ld.ConceptTotalValue, 0) AS DebitValue,
			IIF(ma.Number = cas.AccruedAccount, 0, ld.ConceptTotalValue) AS CreditValue,
			l.EmployeeId,
			'Parafiscales'
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN @ListParafiscalConceptClass AS T ON ld.ConceptClass = T.ConceptClass
		JOIN Payroll.Employee e WITH (NOLOCK) ON l.EmployeeId = e.Id
		JOIN Payroll.Contract c WITH (NOLOCK) ON l.ContractId = c.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.AccountingStructure acs WITH (NOLOCK) ON fu.AccountingStructureId = acs.Id
		JOIN Payroll.ConceptAccountingStructure cas WITH (NOLOCK) ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.LegalBookId = @LegalBookId AND (ma.Number = cas.AccruedAccount OR ma.Number = cas.DeductedAccount)
		LEFT JOIN Payroll.FundContract fc WITH (NOLOCK) ON c.Id = fc.ContractId 
			AND fc.State = 1
			AND 
			(
				(ld.ConceptClass = '009' AND fc.FundType = 4)
				OR
				(ld.ConceptClass = '036' AND fc.FundType = 5)				
			)
		LEFT JOIN Payroll.Fund f WITH (NOLOCK) ON 
			(
				(ld.ConceptClass = '009' AND fc.FundId = f.Id)
				OR
				(ld.ConceptClass = '015' AND l.PensionFundId = f.Id)
				OR
				(ld.ConceptClass = '018' AND l.HealthFundId = f.Id)
				OR
				(ld.ConceptClass = '036' AND fc.FundId = f.Id)
			)
		WHERE l.PayrollDateLiquidated  = @PayrollEndDate
			AND l.GroupId = @GroupId
			AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
			AND l.RegisterStatus = 'C'

		/*******************************************************************************************************/

		-- Eliminamos las distribuciones que hayan sido agregados sin movimiento 
		DELETE c
		FROM @CostDistributions c
		WHERE c.DebitValue = 0 AND c.CreditValue = 0

		/*************************************** PRORRATEO ************************************/
		IF @CostDistributionParameter = 2 -- Costeo por cuadro de turnos
		BEGIN
			SELECT @CostDistributionsXML = CONVERT(xml, 
				(
					SELECT * FROM @CostDistributions CostDistributions
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			DELETE c
			FROM @CostDistributions c

			-- SP que se encarga de la distribución por cuadro de turnos
			EXEC [Payroll].[SP_DistributionBySchedule] @CostDistributionsXML OUTPUT

			INSERT INTO @CostDistributions
			(
				LiquidationId, LiquidationDetailId, 
				JournalVoucherTypeId, MainAccountId, MainAccountNumber, ThirdPartyId, CostCenterId, 
				DebitValue, CreditValue, EmployeeId, NumberHours, Detail, BaseValue, IdRetention
			)
				SELECT	t.x.value('LiquidationId[1]','int') AS LiquidationId,
						t.x.value('LiquidationDetailId[1]','int') AS LiquidationDetailId,
						t.x.value('JournalVoucherTypeId[1]','int') AS JournalVoucherTypeId,
						t.x.value('MainAccountId[1]','int') AS MainAccountId,
						t.x.value('MainAccountNumber[1]','varchar(50)') AS MainAccountNumber,
						t.x.value('ThirdPartyId[1]','int') AS ThirdPartyId,
						t.x.value('CostCenterId[1]','int') AS CostCenterId,
						t.x.value('DebitValue[1]','decimal(18,0)') AS DebitValue,
						t.x.value('CreditValue[1]','decimal(18,0)') AS CreditValue,
						t.x.value('EmployeeId[1]','int') AS EmployeeId,
						t.x.value('NumberHours[1]','int') AS NumberHours,
						t.x.value('Detail[1]','varchar(500)') AS Detail,
						t.x.value('BaseValue[1]','decimal(18,0)') AS BaseValue,
						t.x.value('IdRetention[1]','int') AS IdRetention
				FROM @CostDistributionsXML.nodes('/CostDistributions') t(x)
		END		

		/*******************************************************************************************************/

		IF EXISTS
		(
			SELECT 1
			FROM @CostDistributions t
			GROUP BY t.EmployeeId, t.JournalVoucherTypeId
			HAVING SUM(t.DebitValue) <> SUM(t.CreditValue)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 
					'El comprobante contable de ' + 
						CASE t.JournalVoucherTypeId
							WHEN @PayrollJournalVoucherTypeId THEN 'Nomina'
							WHEN @PrestacionJournalVoucherTypeId THEN 'Prestaciones de Nómina'
							WHEN @ProvisionJournalVoucherTypeId THEN 'Provisiones Nomina'
						END + ' del empleado ' + tp.Nit + ' - ' + tp.Name + ' se encuentra desbalanceado'
					AS Message, 
					'' AS Consecutive
				FROM @CostDistributions t
				JOIN Payroll.Employee e WITH (NOLOCK) ON t.EmployeeId = e.Id
				JOIN Common.ThirdParty tp WITH (NOLOCK) ON e.ThirdPartyId = tp.Id
				GROUP BY t.EmployeeId, t.JournalVoucherTypeId, tp.Nit, tp.Name
				HAVING SUM(t.DebitValue) <> SUM(t.CreditValue)

			GOTO PRINT_RESULT
		END		

		/*************************************** CONTABILIZACIÓN ************************************/
		SET @CurrencyID = (SELECT TOP 1 CurrencyId FROM Payroll.PayrollSettings)
		INSERT INTO @JournalVourcherTmp
			( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName,CurrencyId,TRMDate )
			SELECT TOP 1
				@LegalBookId LegalBookId, 
				@PayrollJournalVoucherTypeId IdJournalVoucher, 
				IIF(@PayrollEndDate > [Common].[GETDATE](), [Common].[GETDATE](), @PayrollEndDate) VoucherDate, 
				2 Status, 
				'Comprobante Contable de Nómina - Grupo: ' + g.Code + ' ' + g.Name + ' - Nómina De ' + CAST(CAST(@PayrollEndDate AS DATE) AS VARCHAR(20)) Detail,
				NULL EntityCode,
				NULL EntityId,
				'PayrollLiquidation' EntityName,
				@CurrencyID,
				@PayrollEndDate
			FROM Payroll.[Group] g WITH (NOLOCK)
			WHERE g.Id = @GroupId

		/*************************************** NOMINA ************************************/

		IF EXISTS (SELECT 1 FROM @CostDistributions t WHERE t.JournalVoucherTypeId = @PayrollJournalVoucherTypeId)
		BEGIN
			INSERT INTO @JournalVourcherDetailTmp
				( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue )
				SELECT
					t.MainAccountId AS IdMainAccount,
					t.ThirdPartyId AS IdThirdParty,
					t.CostCenterId AS IdCostCenter,
					CONCAT('Registro ', t.Detail, ' empleado: ', tp.Nit, ' - ', tp.Name) AS Detail,
					t.DebitValue AS DebitValue,
					t.CreditValue AS CreditValue,
					t.IdRetention AS IdRetention,
					NULL AS RetentionRate,
					t.BaseValue AS BaseValue,
					t.BaseValue AS BillingValue
				FROM @CostDistributions t
				LEFT JOIN Payroll.Employee e WITH (NOLOCK) ON t.EmployeeId = e.Id
				LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON e.ThirdPartyId = tp.Id
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

			select 
			@CodeMessage = rjv.code, 
			@Message = rjv.MessageResult, 
			@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv

			IF @CodeMessage = '999' 
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT 999 AS CodeMessage, 'No se confirmó el Comprobante Contable de Nómina por ' + @Message AS Message, '' AS Consecutive

				GOTO PRINT_RESULT
			END
			ELSE
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT 
					rjv.code AS CodeMessage, 
					'Nómina: '+rjv.MessageResult AS Message, 
					'' AS Consecutive
					FROM @resultJournalVoucher as rjv
			END
		END

		SELECT @JournalVoucherXML = NULL, @CodeMessage = NULL, @Message = NULL, @IdJournalVoucherResult = NULL
		DELETE FROM @JournalVourcherDetailTmp

		/*************************************** PRESTACIONES SOCIALES ************************************/

		IF EXISTS (SELECT 1 FROM @CostDistributions t WHERE t.JournalVoucherTypeId = @PrestacionJournalVoucherTypeId)
		BEGIN
			UPDATE jv
				SET jv.IdJournalVoucher = @PrestacionJournalVoucherTypeId,
					jv.Detail = 'Comprobante Contable de Prestaciones de Nómina - Grupo: ' + g.Code + ' ' + g.Name + ' - Nómina De ' + CAST(CAST(@PayrollEndDate AS DATE) AS VARCHAR(20))
			FROM @JournalVourcherTmp jv
			JOIN Payroll.[Group] g WITH (NOLOCK) ON g.Id = @GroupId

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
				LEFT JOIN Payroll.Employee e WITH (NOLOCK) ON t.EmployeeId = e.Id
				LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON e.ThirdPartyId = tp.Id
				WHERE t.JournalVoucherTypeId = @PrestacionJournalVoucherTypeId

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

			select 
			@CodeMessage = rjv.code, 
			@Message = rjv.MessageResult, 
			@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv

			IF @CodeMessage = '999' 
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT 999 AS CodeMessage, 'No se confirmó el Comprobante Contable de Prestaciones de Nómina por ' + @Message AS Message, '' AS Consecutive

				GOTO PRINT_RESULT
			END
			ELSE
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT 
				rjv.code AS CodeMessage, 
				'Prestaciones de Nómina: '+rjv.MessageResult AS Message, 
				'' AS Consecutive
				FROM @resultJournalVoucher as rjv
			END
		END

		SELECT @JournalVoucherXML = NULL, @CodeMessage = NULL, @Message = NULL, @IdJournalVoucherResult = NULL
		DELETE FROM @JournalVourcherDetailTmp

		/*************************************** PROVISIONES ************************************/

		IF EXISTS (SELECT 1 FROM @CostDistributions t WHERE t.JournalVoucherTypeId = @ProvisionJournalVoucherTypeId)
		BEGIN
			UPDATE jv
				SET jv.IdJournalVoucher = @ProvisionJournalVoucherTypeId,
					jv.Detail = 'Comprobante Contable de Provisiones de Nómina - Grupo: ' + g.Code + ' ' + g.Name + ' - Nómina De ' + CAST(CAST(@PayrollEndDate AS DATE) AS VARCHAR(20))
			FROM @JournalVourcherTmp jv
			JOIN Payroll.[Group] g WITH (NOLOCK) ON g.Id = @GroupId

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
				LEFT JOIN Payroll.Employee e WITH (NOLOCK) ON t.EmployeeId = e.Id
				LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON e.ThirdPartyId = tp.Id
				WHERE t.JournalVoucherTypeId = @ProvisionJournalVoucherTypeId

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

			select 
			@CodeMessage = rjv.code, 
			@Message = rjv.MessageResult, 
			@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv

			IF @CodeMessage = '999' 
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
					SELECT 999 AS CodeMessage, 'No se confirmó el Comprobante Contable de Provisiones de Nómina por ' + @Message AS Message, '' AS Consecutive

				GOTO PRINT_RESULT
			END
			ELSE
			BEGIN
				INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT DISTINCT 
				rjv.code AS CodeMessage, 
				'Provisiones de Nómina: '+rjv.MessageResult AS Message, 
				'' AS Consecutive
				FROM @resultJournalVoucher as rjv
			END
		END
				
		/*************************************** INSERTAR TABLA DETALLES ************************************/

		INSERT INTO Payroll.CostDistribution
		(
			GroupId,
			PayrollEndDate,
			LiquidationDetailId,
			JournalVoucherTypeId,
			MainAccountId,
			ThirdPartyId,
			CostCenterId,
			DebitValue,
			CreditValue,
			-----------------------------------------------
			EmployeeId,
			NumberHours,
			Detail
		)
		SELECT
			@GroupId,
			@PayrollEndDate,
			t.LiquidationDetailId,
			t.JournalVoucherTypeId,
			t.MainAccountId,
			t.ThirdPartyId,
			t.CostCenterId,
			t.DebitValue,
			t.CreditValue,
			t.EmployeeId,
			t.NumberHours,
			t.Detail
		FROM @CostDistributions t

	END TRY
	BEGIN CATCH

		INSERT @TableResult (CodeMessage, Message, Consecutive)
			SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Consecutive

	END CATCH


	PRINT_RESULT:

	SELECT CodeMessage, Message, Consecutive
	FROM @TableResult
END