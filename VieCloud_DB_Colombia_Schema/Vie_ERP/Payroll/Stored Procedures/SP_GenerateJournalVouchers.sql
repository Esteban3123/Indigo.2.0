-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-19
-- Description:	Procedimiento que se encarga de realizar la contabilización de la nómina
-- =============================================
CREATE   PROCEDURE [Payroll].[SP_GenerateJournalVouchers]
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

   /************************************* CONTROL DE CONCURRENCIA ************************************/
  -- Evita que dos ejecuciones simultaneas para el mismo Grupo+Periodo generen comprobantes
  -- duplicados (el bloqueo de formulario por si solo no lo garantiza: doble clic, doble
  -- pestana, reintento por timeout, etc. igual llegan a este mismo SP).

  DECLARE @LockResource VARCHAR(100) = 'SP_GenerateJournalVouchers_' + CAST(@GroupId AS VARCHAR(10)) + '_' + CONVERT(VARCHAR(10), @PayrollEndDate, 112)
  DECLARE @LockResult INT

  EXEC @LockResult = sp_getapplock
    @Resource = @LockResource,
    @LockMode = 'Exclusive',
    @LockOwner = 'Session',
    @LockTimeout = 0

  IF @LockResult < 0
  BEGIN
   INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT 999 AS CodeMessage, 'Ya hay un proceso de distribucion de gastos en curso para este grupo y periodo. Intente nuevamente en unos minutos.' AS Message, '' AS Consecutive

   GOTO PRINT_RESULT
  END


  /************************************* ASIGNACIONES SIMPLES ************************************/

  -- Busco cual es el concepto asignado para Retencion
  DECLARE @IdRetentionConcept int = 0

  SELECT @IdRetentionConcept = IdRetentionConcepts FROM Payroll.PayrollSettings

  if @IdRetentionConcept = 0 BEGIN

   INSERT @TableResult (CodeMessage, Message, Consecutive)
     SELECT 999 AS CodeMessage, 'No se ha parametrizado Concepto de Retencion en la Fuente en el Formulario de Parametros de Nomina' AS Message, '' AS Consecutive

    GOTO PRINT_RESULT
  END

  SELECT
   @InabilityAccountedBy = ps.InabilityAccountedBy,
   @CostDistributionParameter = ps.PayrollDistribution
  FROM Payroll.PayrollSettings ps

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
  FROM Payroll.[Group] g
  JOIN Payroll.Company c ON g.CompanyId = c.Id
  JOIN Payroll.PayrollParameter pp ON g.PayrollParameterId = pp.Id
  LEFT JOIN GeneralLedger.JournalVoucherTypes payroll ON pp.PayrollVoucherCode = payroll.Code
  LEFT JOIN GeneralLedger.JournalVoucherTypes prestation ON pp.PrestacionVoucherCode = prestation.Code
  LEFT JOIN GeneralLedger.JournalVoucherTypes provision ON pp.ProvisionVoucherCode = provision.Code
  WHERE g.Id = @GroupId

  SELECT
   @SENAThirdPartyId = tp.Id
  FROM Common.ThirdParty tp
  WHERE tp.Nit = '899999034'

  SELECT
   @ICBFThirdPartyId = tp.Id
  FROM Common.ThirdParty tp
  WHERE tp.Nit = '899999239'

  SELECT @LegalBookId = lb.Id
  FROM GeneralLedger.LegalBook lb
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
   FROM Payroll.CostDistribution  cd
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
   FROM Payroll.Liquidation l
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
   FROM Payroll.PayrollSettings
  )
  BEGIN
   INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT 999 AS CodeMessage, 'No se encontraron parametros de nomina definidos' AS Message, '' AS Consecutive

   GOTO PRINT_RESULT
  END

  IF @PayrollJournalVoucherTypeId IS NULL
  BEGIN
   INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT 999 AS CodeMessage, 'No esta paremetrizado el Comprobante Contable de Nomina' AS Message, '' AS Consecutive

   GOTO PRINT_RESULT
  END

  IF @ProvisionJournalVoucherTypeId IS NULL
  BEGIN
   INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT 999 AS CodeMessage, 'No esta parametrizado el Comprobante Contable de Provisiones' AS Message, '' AS Consecutive

   GOTO PRINT_RESULT
  END

  IF @PrestacionJournalVoucherTypeId IS NULL
  BEGIN
   INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT 999 AS CodeMessage, 'No esta paremetrizado el Comprobante Contable de Prestaciones Sociales' AS Message, '' AS Consecutive

   GOTO PRINT_RESULT
  END

  IF @PayrollAccountId IS NULL
  BEGIN
   INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT 999 AS CodeMessage, 'No esta parametrizado la cuenta de Nomina del Grupo' AS Message, '' AS Consecutive

   GOTO PRINT_RESULT
  END

  IF @LegalBookId IS NULL
  BEGIN
   INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT 999 AS CodeMessage, 'No esta parametrizado un libro oficial' AS Message, '' AS Consecutive

   GOTO PRINT_RESULT
  END

  IF NOT EXISTS
  (
   SELECT 1
   FROM GeneralLedger.MainAccounts ma
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
   FROM Payroll.Liquidation l
   JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
   JOIN Payroll.ViewDistributionOfScheduledExpenses sd ON l.EmployeeId = sd.EmployeeId
    AND ld.ConceptId = sd.ConceptId
    AND sd.DateDetail BETWEEN @PayrollStartDate AND @PayrollEndDate and sd.GroupId = @GroupId
   LEFT JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = IIF(ld.ConceptType = '1', sd.AccruedAccount, sd.DeductedAccount)
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
   FROM Payroll.ConceptAccountingStructure cas
    JOIN Payroll.Concept c ON cas.ConceptId = c.Id
    JOIN Payroll.AccountingStructure acs ON cas.AccountingStructureId = acs.Id
    JOIN PAyroll.LiquidationDetail LD ON LD.ConceptId = C.Id
    JOIN Payroll.Liquidation L ON LD.PayrollId = L.Id
    WHERE cas.AccruedAccount = cas.DeductedAccount
     AND L.PayrollDateLiquidated = @PayrollEndDate
     AND L.GroupId = @GroupId
     AND L.RegisterStatus = 'C'
  )
  BEGIN
   INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT DISTINCT 999 AS CodeMessage, 'El concepto ' + c.Code + ' tiene la cuenta Debito y Credito igual parametrizada en la estructura ' + CONCAT(acs.Code, ' - ', acs.Description) AS Message, '' AS Consecutive
    FROM Payroll.ConceptAccountingStructure cas
    JOIN Payroll.Concept c ON cas.ConceptId = c.Id
    JOIN Payroll.AccountingStructure acs ON cas.AccountingStructureId = acs.Id
    JOIN PAyroll.LiquidationDetail LD ON LD.ConceptId = C.Id
    JOIN Payroll.Liquidation L ON LD.PayrollId = L.Id
    WHERE cas.AccruedAccount = cas.DeductedAccount
     AND L.PayrollDateLiquidated = @PayrollEndDate
     AND L.GroupId = @GroupId
     AND L.RegisterStatus = 'C'

   GOTO PRINT_RESULT
  END

  IF EXISTS
  (
   SELECT 1
   FROM Payroll.Liquidation l
   JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
   JOIN Payroll.Contract c ON l.ContractId = c.Id
   JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
   LEFT JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
   LEFT JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
   LEFT JOIN @ListProvisionConceptClass AS PR ON ld.ConceptClass = PR.ConceptClass
   LEFT JOIN @ListParafiscalConceptClass AS PA ON ld.ConceptClass = PA.ConceptClass
   LEFT JOIN @ListInabilities AS I ON ld.ConceptClass = I.ConceptClass
   LEFT JOIN GeneralLedger.MainAccounts maD ON maD.LegalBookId = @LegalBookId AND
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
  )
  BEGIN
   INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT DISTINCT
     999 AS CodeMessage, 'El concepto ' + ld.ConceptCode + ' tiene cuentas contables sin parametrizar' AS Message, '' AS Consecutive
    FROM Payroll.Liquidation l
    JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
    JOIN Payroll.Contract c ON l.ContractId = c.Id
    JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
    LEFT JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
    LEFT JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
    LEFT JOIN @ListProvisionConceptClass AS PR ON ld.ConceptClass = PR.ConceptClass
    LEFT JOIN @ListParafiscalConceptClass AS PA ON ld.ConceptClass = PA.ConceptClass
    LEFT JOIN @ListInabilities AS I ON ld.ConceptClass = I.ConceptClass
    LEFT JOIN GeneralLedger.MainAccounts maD ON maD.LegalBookId = @LegalBookId AND
     (
      (I.ConceptClass IS NULL AND maD.Number = cas.AccruedAccount)
      OR
      (I.ConceptClass IS NOT NULL AND (maD.Number = cas.InabilityDebitValueEmployeeAccount  OR maD.Number =  cas.InabilityDebitValueEPSAccount) )
      OR
      (ld.ConceptType = 2 AND PA.ConceptClass IS NULL AND PR.ConceptClass IS NULL AND maD.Number = cas.DeductedAccount)--no han sido homologadas
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
   FROM Payroll.Liquidation l
   JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
   JOIN Payroll.Employee e ON l.EmployeeId = e.Id
   JOIN Payroll.Contract c ON l.ContractId = c.Id
   LEFT JOIN Payroll.FundContract fc ON c.Id = fc.ContractId
    AND fc.State = 1
    AND
    (
     (ld.ConceptClass = '009' AND fc.FundType = 4)
     OR
     (ld.ConceptClass = '036' AND fc.FundType = 5)
    )
   LEFT JOIN Payroll.Fund f ON
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
      END + ' valida' AS Message, '' AS Consecutive
    FROM Payroll.Liquidation l
    JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
    JOIN Payroll.Employee e ON l.EmployeeId = e.Id
    JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id
    JOIN Payroll.Contract c ON l.ContractId = c.Id
    LEFT JOIN Payroll.FundContract fc ON c.Id = fc.ContractId
     AND fc.State = 1
     AND
     (
      (ld.ConceptClass = '009' AND fc.FundType = 4)
      OR
      (ld.ConceptClass = '036' AND fc.FundType = 5)
     )
    LEFT JOIN Payroll.Fund f ON
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
   FROM Payroll.Liquidation l
   JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
   LEFT JOIN Payroll.AgreementsC ac ON
   (
    (ld.ConceptClass = '041' AND ld.AgreementsId = ac.Id)
   )
   LEFT JOIN Payroll.Company co ON ac.CompanyId = co.Id
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
    FROM Payroll.Liquidation l
    JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
    JOIN Payroll.Employee e ON l.EmployeeId = e.Id
    JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id
    LEFT JOIN Payroll.AgreementsC ac ON
    (
     (ld.ConceptClass = '041' AND ld.AgreementsId = ac.Id)
    )
    LEFT JOIN Payroll.Company co ON ac.CompanyId = co.Id
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
   FROM Payroll.Liquidation l
   JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
   LEFT JOIN Payroll.TradeUnion tu ON
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
    FROM Payroll.Liquidation l
    JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
    JOIN Payroll.Employee e ON l.EmployeeId = e.Id
    JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id
    LEFT JOIN Payroll.TradeUnion tu ON
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
    SELECT 999 AS CodeMessage, 'No esta creado el tercero de la empresa' AS Message, '' AS Consecutive

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
  FROM Payroll.Liquidation l
  JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
  JOIN Payroll.Employee e ON l.EmployeeId = e.Id
  JOIN
  (
   SELECT sd.EmployeeId, sd.ConceptId, sd.AccruedAccount, sd.DeductedAccount, sd.CostCenterId, SUM(sd.TotalHours) TotalHours
   FROM Payroll.ViewDistributionOfScheduledExpenses sd
   WHERE sd.DateDetail BETWEEN @PayrollStartDate AND @PayrollEndDate and sd.EmployeeId = ISNULL(@EmployeeId, sd.EmployeeId)
   GROUP BY sd.EmployeeId, sd.ConceptId, sd.AccruedAccount, sd.DeductedAccount, sd.CostCenterId
  ) sd ON l.EmployeeId = sd.EmployeeId
   AND ld.ConceptId = sd.ConceptId
  JOIN
  (
   SELECT sd.EmployeeId, sd.ConceptId, SUM(sd.TotalHours) TotalHours
   FROM Payroll.ViewDistributionOfScheduledExpenses sd
   WHERE sd.DateDetail BETWEEN @PayrollStartDate AND @PayrollEndDate AND sd.EmployeeId = ISNULL(@EmployeeId, sd.EmployeeId)
   GROUP BY sd.EmployeeId, sd.ConceptId
  ) tsd ON l.EmployeeId = tsd.EmployeeId AND ld.ConceptId = tsd.ConceptId
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = IIF(ld.ConceptType = '1', sd.AccruedAccount, sd.DeductedAccount)
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
   FROM Payroll.LiquidationDetail ld
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
   'Incapacidades Patrono'
  FROM Payroll.Liquidation l
  JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
  JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
  JOIN Payroll.Employee e ON l.EmployeeId = e.Id
  JOIN Payroll.Contract c ON l.ContractId = c.Id
  JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
  JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
  JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.InabilityDebitValueEmployeeAccount
  WHERE l.PayrollDateLiquidated  = @PayrollEndDate
   AND l.GroupId = @GroupId
   AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
   AND l.RegisterStatus = 'C'
   AND ld.SpendingInability > 0
   AND ld.ConceptClass NOT IN ('076')
   -- Cuando la parte patronal viene itemizada como concepto de clase '067'
   -- (INCAPACIDAD PATRONO) no se vuelve a derivar de SpendingInability. El
   -- guardia cubre solo las clases que ese concepto reemplaza: si la liquidacion
   -- trae otra incapacidad (021/022/023/024), esa conserva su debito patronal.
   AND NOT (
    ld.ConceptClass IN ('068','070')
    AND EXISTS (
     SELECT 1
     FROM Payroll.LiquidationDetail ld2
     WHERE ld2.PayrollId = ld.PayrollId AND ld2.ConceptClass = '067'
    )
   )
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
  FROM Payroll.Liquidation l
  JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
  JOIN Payroll.Employee e ON l.EmployeeId = e.Id
  JOIN Payroll.Contract c ON l.ContractId = c.Id
  JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
  JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
  JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = cas.DeductedAccount
  LEFT JOIN Payroll.Fund f ON
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
  LEFT JOIN Payroll.AgreementsC ac ON
   (
    (ld.ConceptClass = '041' AND ld.AgreementsId = ac.Id)
   )
  LEFT JOIN Payroll.Company co ON ac.CompanyId = co.Id
  LEFT JOIN Payroll.TradeUnion tu ON
   (
    (ld.ConceptClass = '044' AND ld.ConceptId = tu.PayrollConceptId)
   )
  WHERE l.PayrollDateLiquidated  = @PayrollEndDate
   AND l.GroupId = @GroupId
   AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
   AND l.RegisterStatus = 'C'
   AND ld.DistribuirGasto = 0
   AND ld.ConceptType = '2'  -- <== FIX: literal de cadena, antes "= 2" forzaba CONVERT_IMPLICIT(int,ld.ConceptType)
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
  -- OPTIMIZACION (Miguel Angel Ruiz Vega DBA - 2026-08-04):
  -- El JOIN original contra GeneralLedger.MainAccounts usaba OR sobre dos columnas distintas
  -- (cas.AccruedAccount = ma.Number OR (cas.DeductedAccount = ma.Number AND cas.DeductedAccount <> @PayrollAccountNumber)),
  -- lo cual impide usar IX_MainAccounts_LegalBook_Number como seek y fuerza escaneo completo de
  -- MainAccounts por cada fila externa (evidencia en plan: Scan count 15818 / 221.452 lecturas
  -- logicas, ~29s CPU). Se materializa la parte comun en #DevengosBase y se separa el acceso a
  -- MainAccounts en dos ramas seekables via UNION ALL. Las ramas son mutuamente excluyentes porque
  -- la validacion de Fase 1 ya descarta AccruedAccount = DeductedAccount para el periodo procesado.

  IF OBJECT_ID('tempdb..#DevengosBase') IS NOT NULL DROP TABLE #DevengosBase

  SELECT
   l.Id AS LiquidationId,
   ld.Id AS LiquidationDetailId,
   ld.ConceptTotalValue,
   l.EmployeeId,
   fu.CostCenterId,
   cas.AccruedAccount,
   cas.DeductedAccount,
   CASE ld.ConceptClass
    WHEN '014' THEN f.ThirdPartyId
    WHEN '016' THEN f.ThirdPartyId
    WHEN '017' THEN f.ThirdPartyId
    WHEN '019' THEN f.ThirdPartyId
    WHEN '038' THEN f.ThirdPartyId
    WHEN '041' THEN co.ThirdPartyId
    WHEN '044' THEN tu.IdThirdParty
    ELSE e.ThirdPartyId
   END AS ThirdPartyId
  INTO #DevengosBase
  FROM Payroll.Liquidation l
  JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
  JOIN Payroll.Employee e ON l.EmployeeId = e.Id
  JOIN Payroll.Contract c ON l.ContractId = c.Id
  JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
  JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
  JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
  LEFT JOIN Payroll.Fund f ON
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
  LEFT JOIN Payroll.AgreementsC ac ON
   (
    (ld.ConceptClass = '041' AND ld.AgreementsId = ac.Id)
   )
  LEFT JOIN Payroll.Company co ON ac.CompanyId = co.Id
  LEFT JOIN Payroll.TradeUnion tu ON
   (
    (ld.ConceptClass = '044' AND ld.ConceptId = tu.PayrollConceptId)
   )
  WHERE l.PayrollDateLiquidated  = @PayrollEndDate
   AND l.GroupId = @GroupId
   AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
   AND l.RegisterStatus = 'C'
   AND ld.DistribuirGasto = 0
   AND ld.ConceptType = '1'  -- <== FIX: literal de cadena, antes "= 1" forzaba CONVERT_IMPLICIT(int,ld.ConceptType)
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
   )  and ld.ConceptClass not in ('068','069')

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
   b.LiquidationId,
   b.LiquidationDetailId,
   @PayrollJournalVoucherTypeId,
   ma.Id,
   ma.Number,
   b.ThirdPartyId,
   IIF(ma.HandlesCostCenter = 1, b.CostCenterId, NULL),
   b.ConceptTotalValue,
   0,
   b.EmployeeId,
   'Devengos'
  FROM #DevengosBase b
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = b.AccruedAccount
  UNION ALL
  SELECT
   b.LiquidationId,
   b.LiquidationDetailId,
   @PayrollJournalVoucherTypeId,
   ma.Id,
   ma.Number,
   b.ThirdPartyId,
   IIF(ma.HandlesCostCenter = 1, b.CostCenterId, NULL),
   0,
   b.ConceptTotalValue,
   b.EmployeeId,
   'Devengos'
  FROM #DevengosBase b
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND ma.Number = b.DeductedAccount AND b.DeductedAccount <> @PayrollAccountNumber

  DROP TABLE #DevengosBase

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
    SUM(IIF(T.ConceptClass IS NULL, ld.ConceptTotalValue, ISNULL(ld.SpendingInability, 0))) Value,
    l.EmployeeId
   FROM Payroll.Liquidation l
   JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
   JOIN Payroll.Employee e ON l.EmployeeId = e.Id
   JOIN Payroll.Contract c ON l.ContractId = c.Id
   JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
   JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
   JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
   LEFT JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
   JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND
    (
     (cas.DeductedAccount = @PayrollAccountNumber AND cas.DeductedAccount = ma.Number)
     OR
     (T.ConceptClass IS NOT NULL AND ma.Id = @PayrollAccountId)
    )
   WHERE l.PayrollDateLiquidated  = @PayrollEndDate
    AND l.GroupId = @GroupId
    AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
    AND l.RegisterStatus = 'C'
    AND ld.ConceptType = '1'  -- <== FIX: literal de cadena, antes "= 1" forzaba CONVERT_IMPLICIT(int,ld.ConceptType)
    AND ld.ConceptClass NOT IN
    (
     SELECT T.ConceptClass
     FROM @ListProvisionConceptClass AS T
     UNION
     SELECT T.ConceptClass
     FROM @ListParafiscalConceptClass AS T
    )
    AND (
     -- Si ConceptClass NO esta en @ListInabilitiesEPS, incluirlo siempre
     ld.ConceptClass NOT IN (SELECT T.ConceptClass FROM @ListInabilitiesEPS AS T)
     -- Si ConceptClass esta en @ListInabilitiesEPS, incluirlo solo si NO hay otros conceptos que NO esten en @ListInabilitiesEPS
     OR
     (
      ld.ConceptClass IN (SELECT T.ConceptClass FROM @ListInabilitiesEPS AS T)
      AND NOT EXISTS (
       SELECT 1
       FROM Payroll.LiquidationDetail ld2
       WHERE ld2.PayrollId = l.Id AND ld2.ConceptType = '1' AND ld2.ConceptClass NOT IN (SELECT T.ConceptClass FROM @ListInabilitiesEPS AS T)
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

     --Insertamos el registro de nomina
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

  /*************************************** INCAPACIDADES EPS ************************************/


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

   -- El valor a cargo de la EPS excluye la parte patronal, salvo cuando esta ya
   -- viene itemizada como concepto de clase '067' y por tanto no esta incluida
   -- en ConceptTotalValue.
   IIF(ma.Number = cas.InabilityDebitValueEPSAccount,
    ld.ConceptTotalValue - IIF(ld.ConceptClass IN ('068','070')
     AND EXISTS (SELECT 1 FROM Payroll.LiquidationDetail ld2
                 WHERE ld2.PayrollId = ld.PayrollId AND ld2.ConceptClass = '067'),
     0, ISNULL(ld.SpendingInability, 0)),
    0) AS DebitValue,
   IIF(ma.Number = cas.InabilityDebitValueEPSAccount, 0,
    ld.ConceptTotalValue - IIF(ld.ConceptClass IN ('068','070')
     AND EXISTS (SELECT 1 FROM Payroll.LiquidationDetail ld2
                 WHERE ld2.PayrollId = ld.PayrollId AND ld2.ConceptClass = '067'),
     0, ISNULL(ld.SpendingInability, 0))) AS CreditValue,

   l.EmployeeId,
   'Incapacidades EPS'
  FROM Payroll.Liquidation l
  JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
  JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
  JOIN Payroll.Employee e ON l.EmployeeId = e.Id
  JOIN Payroll.Contract c ON l.ContractId = c.Id
  JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
  JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
  JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND (ma.Id = @PayrollAccountId OR ma.Number = cas.InabilityDebitValueEPSAccount)
  JOIN Payroll.Fund f ON l.HealthFundId = f.Id
  WHERE l.PayrollDateLiquidated  = @PayrollEndDate
   AND l.GroupId = @GroupId
   AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
   AND l.RegisterStatus = 'C'
   AND ld.ConceptClass NOT IN ('027','076','069','067','075')


  /*************************************** INCAPACIDADES AR OLD ************************************/

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

   IIF(ma.Number = cas.InabilityDebitValueEPSAccount,  (ld.ConceptTotalValue - ld.SpendingInability), 0) AS DebitValue,
   IIF(ma.Number = cas.InabilityDebitValueEPSAccount, 0, (ld.ConceptTotalValue - ld.SpendingInability)) AS CreditValue,

   l.EmployeeId,
   'Incapacidades ARL'
  FROM Payroll.Liquidation l
  JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
  JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
  JOIN Payroll.Employee e ON l.EmployeeId = e.Id
  JOIN Payroll.Contract c ON l.ContractId = c.Id
  JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
  JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
  JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND (ma.Id = @PayrollAccountId OR ma.Number = cas.InabilityDebitValueEPSAccount)
  OUTER APPLY
  (
   -- Solo la ARL vigente del contrato. El APPLY impide que un historial de
   -- afiliaciones (ej. cambio de ARL) multiplique las filas e infle el
   -- comprobante; si no hay ARL vigente devuelve NULL en lugar de imputar
   -- una afiliacion ya terminada.
   SELECT TOP 1 f2.ThirdPartyId
   FROM Payroll.FundContract fc2
   JOIN Payroll.Fund f2 ON fc2.FundId = f2.Id AND f2.Risk = 1
   WHERE fc2.ContractId = l.ContractId
    AND fc2.State = 1
   ORDER BY fc2.Id DESC
  ) f
  WHERE l.PayrollDateLiquidated  = @PayrollEndDate
   AND l.GroupId = @GroupId
   AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
   AND l.RegisterStatus = 'C'
   AND (ld.ConceptTotalValue - ld.SpendingInability) >=0
   and ld.ConceptClass in ('027')

   /*************************************** INCAPACIDADES ARL NEW ************************************/
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
  FROM Payroll.Liquidation l
  JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
  JOIN @ListInabilities AS T ON ld.ConceptClass = T.ConceptClass
  JOIN Payroll.Employee e ON l.EmployeeId = e.Id
  JOIN Payroll.Contract c ON l.ContractId = c.Id
  JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
  JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
  JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND  ma.Number = cas.InabilityDebitValueEPSAccount
  OUTER APPLY
  (
   -- Solo la ARL vigente del contrato. El APPLY impide que un historial de
   -- afiliaciones (ej. cambio de ARL) multiplique las filas e infle el
   -- comprobante; si no hay ARL vigente devuelve NULL en lugar de imputar
   -- una afiliacion ya terminada.
   SELECT TOP 1 f2.ThirdPartyId
   FROM Payroll.FundContract fc2
   JOIN Payroll.Fund f2 ON fc2.FundId = f2.Id AND f2.Risk = 1
   WHERE fc2.ContractId = l.ContractId
    AND fc2.State = 1
   ORDER BY fc2.Id DESC
  ) f
  WHERE l.PayrollDateLiquidated  = @PayrollEndDate
   AND l.GroupId = @GroupId
   AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
   AND l.RegisterStatus = 'C'
   and ld.ConceptClass in ('076')  --and ld.ConceptClass <> '075'




  /*************************************** PROCESO DE PROVISION  ************************************/
  -- NOTA: mismo patron OR contra MainAccounts que en Devengos. No se reescribe en esta Fase 2:
  -- se deja pendiente para Fase 3 con validacion adicional (ver comentario en cabecera del archivo).

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
  FROM Payroll.Liquidation l
  JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
  JOIN @ListProvisionConceptClass AS T ON ld.ConceptClass = T.ConceptClass
  JOIN Payroll.Employee e ON l.EmployeeId = e.Id
  JOIN Payroll.Contract c ON l.ContractId = c.Id
  JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
  JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
  JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND (ma.Number = cas.AccruedAccount OR ma.Number = cas.DeductedAccount)
  WHERE l.PayrollDateLiquidated  = @PayrollEndDate
   AND l.GroupId = @GroupId
   AND l.EmployeeId = ISNULL(@EmployeeId, l.EmployeeId)
   AND l.RegisterStatus = 'C'

  /*************************************** PROCESO DE PARAFISCALES  ************************************/
  -- NOTA: mismo patron OR contra MainAccounts que en Devengos. No se reescribe en esta Fase 2:
  -- se deja pendiente para Fase 3 con validacion adicional (ver comentario en cabecera del archivo).

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
  FROM Payroll.Liquidation l
  JOIN Payroll.LiquidationDetail ld ON l.Id = ld.PayrollId
  JOIN @ListParafiscalConceptClass AS T ON ld.ConceptClass = T.ConceptClass
  JOIN Payroll.Employee e ON l.EmployeeId = e.Id
  JOIN Payroll.Contract c ON l.ContractId = c.Id
  JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
  JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
  JOIN Payroll.ConceptAccountingStructure cas ON ld.ConceptId = cas.ConceptId AND acs.Id = cas.AccountingStructureId
  JOIN GeneralLedger.MainAccounts ma ON ma.LegalBookId = @LegalBookId AND (ma.Number = cas.AccruedAccount OR ma.Number = cas.DeductedAccount)
  LEFT JOIN Payroll.FundContract fc ON c.Id = fc.ContractId
   AND fc.State = 1
   AND
   (
    (ld.ConceptClass = '009' AND fc.FundType = 4)
    OR
    (ld.ConceptClass = '036' AND fc.FundType = 5)
   )
  LEFT JOIN Payroll.Fund f ON
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

   --select * from @CostDistributions where LiquidationId = 387327
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

   -- SP que se encarga de la distribucion por cuadro de turnos
   EXEC [Payroll].[SP_DistributionBySchedule] @CostDistributionsXML OUTPUT

   INSERT INTO @CostDistributions
   (
    LiquidationId, LiquidationDetailId,
    JournalVoucherTypeId, MainAccountId, MainAccountNumber, ThirdPartyId, CostCenterId,
    DebitValue, CreditValue, EmployeeId, NumberHours, Detail, BaseValue, IdRetention
   )
    SELECT t.x.value('LiquidationId[1]','int') AS LiquidationId,
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
       WHEN @PrestacionJournalVoucherTypeId THEN 'Prestaciones de Nomina'
       WHEN @ProvisionJournalVoucherTypeId THEN 'Provisiones Nomina'
      END + ' del empleado ' + tp.Nit + ' - ' + tp.Name + ' se encuentra desbalanceado'
     AS Message,
     '' AS Consecutive
    FROM @CostDistributions t
    JOIN Payroll.Employee e ON t.EmployeeId = e.Id
    JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id
    GROUP BY t.EmployeeId, t.JournalVoucherTypeId, tp.Nit, tp.Name
    HAVING SUM(t.DebitValue) <> SUM(t.CreditValue)

   GOTO PRINT_RESULT
  END

  /*************************************** CONTABILIZACION ************************************/

  SET @CurrencyID = (SELECT TOP 1 CurrencyId FROM Payroll.PayrollSettings)

  INSERT INTO @JournalVourcherTmp
   ( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName,CurrencyId,TRMDate )
   SELECT TOP 1
    @LegalBookId LegalBookId,
    @PayrollJournalVoucherTypeId IdJournalVoucher,
    IIF(@PayrollEndDate > [Common].[GETDATE](), [Common].[GETDATE](), @PayrollEndDate) VoucherDate,
    2 Status,
    'Comprobante Contable de Nomina - Grupo: ' + g.Code + ' ' + g.Name + ' - Nomina De ' + CAST(CAST(@PayrollEndDate AS DATE) AS VARCHAR(20)) Detail,
    NULL EntityCode,
    NULL EntityId,
    'PayrollLiquidation' EntityName,
    @CurrencyID,
    @PayrollEndDate
   FROM Payroll.[Group] g
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

   select
   @CodeMessage = rjv.code,
   @Message = rjv.MessageResult,
   @IdJournalVoucherResult = rjv.IdJournalVoucher
   from @resultJournalVoucher rjv

   IF @CodeMessage = '999'
   BEGIN
    INSERT @TableResult (CodeMessage, Message, Consecutive)
     SELECT 999 AS CodeMessage, 'No se confirmo el Comprobante Contable de Nomina por ' + @Message AS Message, '' AS Consecutive

    GOTO PRINT_RESULT
   END
   ELSE
   BEGIN
    INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT DISTINCT
     rjv.code AS CodeMessage,
     'Nomina: '+rjv.MessageResult AS Message,
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
     jv.Detail = 'Comprobante Contable de Prestaciones de Nomina - Grupo: ' + g.Code + ' ' + g.Name + ' - Nomina De ' + CAST(CAST(@PayrollEndDate AS DATE) AS VARCHAR(20))
   FROM @JournalVourcherTmp jv
   JOIN Payroll.[Group] g ON g.Id = @GroupId

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
     SELECT 999 AS CodeMessage, 'No se confirmo el Comprobante Contable de Prestaciones de Nomina por ' + @Message AS Message, '' AS Consecutive

    GOTO PRINT_RESULT
   END
   ELSE
   BEGIN
    INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT DISTINCT
    rjv.code AS CodeMessage,
    'Prestaciones de Nomina: '+rjv.MessageResult AS Message,
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
     jv.Detail = 'Comprobante Contable de Provisiones de Nomina - Grupo: ' + g.Code + ' ' + g.Name + ' - Nomina De ' + CAST(CAST(@PayrollEndDate AS DATE) AS VARCHAR(20))
   FROM @JournalVourcherTmp jv
   JOIN Payroll.[Group] g ON g.Id = @GroupId

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
     SELECT 999 AS CodeMessage, 'No se confirmo el Comprobante Contable de Provisiones de Nomina por ' + @Message AS Message, '' AS Consecutive

    GOTO PRINT_RESULT
   END
   ELSE
   BEGIN
    INSERT @TableResult (CodeMessage, Message, Consecutive)
    SELECT DISTINCT
    rjv.code AS CodeMessage,
    'Provisiones de Nomina: '+rjv.MessageResult AS Message,
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


 -- Libera el lock de concurrencia sin importar por cual camino se llego aqui
 -- (validacion fallida, exito, o el CATCH).
 IF @LockResult >= 0
  EXEC sp_releaseapplock @Resource = @LockResource, @LockOwner = 'Session'

 SELECT CodeMessage, Message, Consecutive
 FROM @TableResult
END

GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la contabilización automática de la nómina: genera los comprobantes contables (de nómina, prestaciones sociales y provisiones laborales) para un grupo de empleados y un período de pago determinado. Consulta los parámetros de nómina, los tipos de comprobante contable configurados y la distribución de costos por centro de costo para construir los asientos contables correspondientes. Registra los movimientos débito/crédito en el libro contable legal, asociando cada línea al empleado, cuenta contable, tercero y retención en la fuente según corresponda. Es el punto de integración entre el módulo de nómina (liquidaciones, grupos, empresas) y el módulo de contabilidad (comprobantes de diario), permitiendo cerrar el ciclo contable-laboral de cada período de pago.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVouchers';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVouchers';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina; Liquidación de nómina; Devengos; Deducciones; Retención en la fuente; Provisiones de nómina; Prestaciones sociales; Parafiscales (SENA, ICBF, Caja de Compensación); Incapacidades EPS; Incapacidades ARL; Aportes a salud y pensión; Aporte voluntario de pensión y salud; Libranza; Sindicato; Distribución de gastos por cuadro de turnos; Comprobante contable (Journal Voucher); Centro de costo; Tercero (empleado/empresa/fondo); Libro oficial contable; Plan de cuentas (Main Account); Estructura contable por unidad funcional', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Group.Liquidation = 1 (mensual) → PayrollStartDate = primer día del mes de PayrollEndDate else Liquidación quincenal: si DAY(PayrollEndDate) > 15 inicia el día 16, sino inicia el primer día del mes; si El libro oficial está en moneda COP (Currency.Abbreviation=''COP'') → Se exige que existan los terceros del SENA (Nit 899999034) e ICBF (Nit 899999239); si faltan, aborta con código 999; si PayrollSettings.PayrollDistribution (@CostDistributionParameter) = 2 → Se serializa @CostDistributions a XML, se vacía la tabla y se invoca Payroll.SP_DistributionBySchedule para distribuir por cuadro de turnos, reinsertando el resultado; si Existen filas en @CostDistributions con Detail=''Deducciones'' cuya liquidación no tiene fila Detail=''Nomina'' → Se generan registros ''Nomina'' débito contra los ''Devengos'' con CreditValue>0 hasta cubrir el TotalDeducted; si ld.ConceptClass está en lista de incapacidades y ma.Number = cas.InabilityDebitValueEmployeeAccount → ThirdParty = Empleado y se debita SpendingInability else ThirdParty = empresa (@CompanyThirdPartyId) y el valor va al crédito; si Para incapacidades EPS, ma.Number = cas.InabilityDebitValueEPSAccount → Si @InabilityAccountedBy = 1 ThirdParty=Empleado, sino ThirdParty=Fondo de salud (f.ThirdPartyId); si ConceptClass = ''027'' (incapacidad ARL) → Se procesa en el bloque de Incapacidades ARL else Si está en lista de incapacidades pero <> ''027'' se procesa en bloque Incapacidades EPS; si Para Nómina: @AccountedBy = 1 → ThirdParty del comprobante de nómina = empleado else ThirdParty = empresa (@CompanyThirdPartyId); si Después de armar @CostDistributions existe algún empleado/JournalVoucherType con SUM(Debit) <> SUM(Credit) → Aborta con mensaje ''comprobante contable ... se encuentra desbalanceado'' y código 999; si @PayrollEndDate > Common.GETDATE() → VoucherDate del comprobante se fija en GETDATE() else VoucherDate = @PayrollEndDate; si Existen filas con JournalVoucherTypeId = @PayrollJournalVoucherTypeId / @PrestacionJournalVoucherTypeId / @ProvisionJournalVoucherTypeId → Se construye XML y se llama GeneralLedger.SP_CreateAndValidateJournalVoucherMovement por cada tipo de comprobante (Nómina, Prestaciones, Provisiones); si El SP de creación de comprobante retorna code = 999 → Se registra mensaje ''No se confirmó el Comprobante Contable de ...'' y se va a PRINT_RESULT else Se registra mensaje de éxito con el consecutivo generado en GeneralLedger.JournalVouchers; si ConceptClass = ''035'' → ThirdParty = SENA (con fallback al fondo) else ConceptClass=''037'' → ThirdParty=ICBF; otros parafiscales → ThirdParty=Fondo asociado; si ConceptCode = ''701'' (retención en la fuente) → Se asigna BaseValue = ld.RetentionBase y IdRetention = PayrollSettings.IdRetentionConcepts en la deducción', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.SP_DistributionBySchedule; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Payroll.GetConceptClassByType; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.PayrollSettings; Payroll.Group; Payroll.Company; Payroll.PayrollParameter; GeneralLedger.JournalVoucherTypes; Common.ThirdParty; GeneralLedger.LegalBook; Common.Currency; Payroll.GetConceptClassByType; Payroll.CostDistribution; Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.ViewDistributionOfScheduledExpenses; GeneralLedger.MainAccounts; Payroll.ConceptAccountingStructure; Payroll.Concept; Payroll.AccountingStructure; Payroll.Contract; Payroll.FunctionalUnit; Payroll.Employee; Payroll.FundContract; Payroll.Fund; Payroll.AgreementsC; Payroll.TradeUnion; GeneralLedger.JournalVouchers', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVouchers';
-- GO
