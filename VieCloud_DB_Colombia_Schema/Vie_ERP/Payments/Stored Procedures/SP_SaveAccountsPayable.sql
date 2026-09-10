
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 07/06/2019
-- Description:	Se encarga de guardar las CxP
-- =============================================
CREATE PROCEDURE [Payments].[SP_SaveAccountsPayable]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	--Tabla de cabecera de cxp que se obtiene del xml
	declare @TableAccountPayable table(TempAccountPayableId int, Id int, NumberFiling bigint, EntityId int, EntityCode varchar(20), EntityName varchar(250), IdSupplier int, IdThirdParty int, 
	IdAccount int, IdCostCenter int, BillNumber varchar(100), BillDate datetime, FilingUnitId int, SupplierTypeId int, Term int, ExpirationDate datetime, Coments varchar(max), Status tinyint, 
	InitialBalance bit, IdInitialBalance int, PreviousBudget bit, Shares int, InvoiceValue numeric(20, 4), Value numeric(20, 4), Balance numeric(20, 4), IdSuppliersDistributionLines int, 
	CostDistributionDirectCostId int, PositionId int, Hours int, ChangeProperties bit, AuxEntityName varchar(250), AuxEntityCode varchar(20), AuxEntityId int, Code varchar(20),
	DocumentDate datetime, ServicePeriodDate datetime, OperatingUnitId int, IsDelete bit, CommitmentDetailId int, HandlesDocumentSupport bit, DocumentSupportId int, DeductibleIva bit, 
	CurrencyId int, TaxRegistration tinyint, IdEconomicActivity int)

	--Tabla de conceptos de cxp que se obtiene del xml
	declare @TableAccountPayableDetailConcepts table(TempAccountPayableId int, TempAccountPayableDetailConceptId int, Id int, IdAccountPayable int, IdConceptAccountPayable int, IdAccount int, 
	IdThirdParty int, IdCostCenter int, Nature tinyint,	BaseValue numeric(20, 4), BillingValue numeric(20, 4), Value numeric(20, 4), IdRetentionConcept int, Percentage decimal(18, 3), 
	Detail varchar(500), DeferredCausation bit, IsDirectCost bit, IsDelete bit, RateIva int, IvaValue decimal(18,2), TotalConcept decimal(18,2))

	--Tabla de cuotas de cxp que se obtiene del xml
	declare @TableAccountPayableShares table(TempAccountPayableId int, Id int, IdAccountPayable int, Share int, DateExpires datetime, InitialValue numeric(20, 4), DebitValue numeric(20, 4),
	CreditValue numeric(20, 4), ValueTransfers numeric(20, 4), PaymentValue numeric(20, 4), CrossingValue numeric(20, 4), Balance numeric(20, 4), IsDelete bit)

	--Tabla de cabecera de causación diferida
	declare @TableDeferredCausation table(TempAccountPayableId int, TempDeferredCausationId int, Id int, IdAccountPayable int, BillNumber varchar(20), IdMainAccount int, PeriodsNumber int, 
	TypeDistribution tinyint, InitialDate datetime, EndDate datetime, IdThirdParty int, IdCostCenter int, ValueCreditPeriod decimal(20,2), Status tinyint, IsDelete bit)

	--Tabla de detalles de causación diferida
	declare @TableDeferredCausationDetails table(TempDeferredCausationId int, Id int, IdDeferredCausation int, IdMainAccount int, IdCostCenter int, Value decimal(20,2), 
	Nature tinyint, DateNextPeriod datetime, IsDelete bit)

	--Tabla de cuotas de causación diferida
	declare @TableDeferredCausationShare table(TempDeferredCausationId int, Id int, DeferredCausationId int, PaymentMonth int, PaymentYear int, Value numeric(20,2), Amortized bit, IsDelete bit)

	--Tabla para la calculadora
	declare @TableAccountPayableDetailConceptLiquidation table(TempAccountPayableDetailConceptId int, TempAccountPayableDetailConceptLiquidationId int, Id int, AccountPayableDetailConceptId int, TotalIncome decimal(18, 0), PensionFundContribution decimal(18, 0),
	PensionFundContributionReal decimal(18, 0), VoluntaryPensionFundContribution decimal(18, 0), VoluntaryPensionFundContributionReal decimal(18, 0), SolidarityPensionFund decimal(18, 0),
	SolidarityPensionFundReal decimal(18, 0), ContributionAccountAFC decimal(18, 0), ContributionAccountAFCReal decimal(18, 0), TotalIncomeExempt decimal(18, 0), TotalIncomeExemptReal decimal(18, 0),
	PaymentCompulsoryHealth decimal(18, 0), PaymentCompulsoryHealthReal decimal(18, 0), PaymentPrepaidMedical decimal(18, 0), PaymentPrepaidMedicalReal decimal(18, 0), PaymentForDependent decimal(18, 0),
	PaymentForDependentReal decimal(18, 0), HousingLoanInterest decimal(18, 0), HousingLoanInterestReal decimal(18, 0), OccupationalRiskContribution decimal(18, 0), OccupationalRiskContributionReal decimal(18, 0),
	TotalDeduction decimal(18, 0), TotalDeductionReal decimal(18, 0), SubTotal decimal(18, 0), ExemptIncome decimal(18, 0), TaxableBase decimal(18, 0), RetentionValue383 decimal(18, 0),
	RetentionValue384 decimal(18, 0), ApplyRetention decimal(18, 0), MaxDeductionsAndRentExents decimal(18, 0), PensionByIndividualSavingsRegime decimal(18, 0), PensionByIndividualSavingsRegimeReal decimal(18, 0),
	UVT decimal(18, 0), SMLV decimal(18, 0), PreviousDeductionsForWithholdings decimal(18,0), AccumulatedIncome DECIMAL(18,2), IsDelete bit)

	declare @TableAccountPayableDetailConceptLiquidationValuesModificated table(
		TempAccountPayableDetailConceptLiquidationId int, 
		Id int,
		LiquidationId int,
		[ConceptType] [tinyint],
		[PreviousValue] [decimal](18, 0),
		[NewValue] [decimal](18, 0),
		[Observations] [varchar](max),
		IsDelete bit
	)

	declare @TableAccountPayableDetailConceptLiquidationAdjusments table(
		TempAccountPayableDetailConceptLiquidationId int, 
		[Id] int,
		[LiquidationId] int,
		[ConceptType] [tinyint],
		[Nature] [tinyint],
		[Value] [decimal](18, 0),
		[Observations] [varchar](500),
		[Status] [tinyint],
		[TotalIncome][decimal](18, 0),
		IsDelete bit	
	)

	--Tabla de compromisos presupuestales que se obtiene del xml
	declare @TableAccountPayableCommitments table(TempAccountPayableId int, Id int, AccountPayableId int, CommitmentDetailId int, Value numeric(20, 4), IsDelete bit)

	--Tabla en donde se almacenan los id que se generan al guardar masivamente las cabeceras de cxp
	declare @IDSAccountPayable table(Id int primary key, BillNumber varchar(100), IdSupplier int, CurrencyId int)

	--Tabla en donde se almacenan los id que se generan al guardar masivamente las cabeceras de causación diferida
	declare @IDSDeferredCausation table(Id int primary key, BillNumber varchar(100), IdMainAccount int, IdCostCenter int)

	--Tabla en donde se almacenan los id que se generan al guardar masivamente los conceptos de la cxp
	declare @IDSAccountPayableDetailConcept table(Id int primary key, IdConceptAccountPayable int, IdAccount int, IdThirdParty int, IdCostCenter int, Nature tinyint, IdAccountPayable int)

	--Variable para obtener el consecutivo del radicado
	declare @ConsecutiveFiling bigint

	--Id de la cabecera para validar si ya hay facturas guardadas
	declare @AccountPayableId int

	--Variable para obtener los errores de las validaciones
	declare @Errors varchar(MAX) = ''

	--Código de la CxP
	declare @Code varchar(20)

	--Fecha del documento
	declare @DocumentDate datetime

	--Fecha de radicación
	declare @ServicePeriodDate datetime

	--Id de la unidad operativa
	declare @OperatingUnitId int

	--Estado de la cxp
	declare @Status tinyint

	--Variables de interfaz presupuestal
	DECLARE @BudgetInterface BIT,
			@ObligationBudgetInterface BIT,
			@ObligationDebitValue BIT,
			@OfficialCurrencyId INT
			
			--se establece la moneda Oficial del sistema
			SELECT @OfficialCurrencyId = c.OfficialCurrencyId
			FROM GeneralLedger.CompanySettings c
	
	BEGIN TRY
		
		--Se obtienen las cxp del xml
		insert into @TableAccountPayable
		select 
			t.x.value('TempAccountPayableId[1]','int') as TempAccountPayableId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('NumberFiling[1]','bigint') as NumberFiling,
			IIF(t.x.value('EntityId[1]','varchar(20)') = '', null, t.x.value('EntityId[1]','varchar(20)')) as EntityId,
			IIF(t.x.value('EntityCode[1]','varchar(20)') = '', null, t.x.value('EntityCode[1]','varchar(20)')) as EntityCode,
			IIF(t.x.value('EntityName[1]','varchar(250)') = '', null, t.x.value('EntityName[1]','varchar(250)')) as EntityName,
			t.x.value('IdSupplier[1]','int') as IdSupplier,
			IIF(t.x.value('IdThirdParty[1]','varchar(20)') = '', null, t.x.value('IdThirdParty[1]','varchar(20)')) as IdThirdParty,
			t.x.value('IdAccount[1]','int') as IdAccount,
			IIF(t.x.value('IdCostCenter[1]','varchar(20)') = '', null, t.x.value('IdCostCenter[1]','varchar(20)')) as IdCostCenter,
			t.x.value('BillNumber[1]','varchar(100)') as BillNumber,
			convert(date, t.x.value('BillDate[1]','varchar(20)'), 103) as BillDate,
			t.x.value('FilingUnitId[1]','int') as FilingUnitId,
			t.x.value('SupplierTypeId[1]','int') as SupplierTypeId,
			t.x.value('Term[1]','int') as Term,
			convert(date, t.x.value('ExpirationDate[1]','varchar(20)'), 103) as ExpirationDate,
			IIF(t.x.value('Coments[1]','varchar(max)') = '', null, t.x.value('Coments[1]','varchar(max)')) as Coments,
			t.x.value('Status[1]','tinyint') as Status,
			t.x.value('InitialBalance[1]','bit') as InitialBalance,
			IIF(t.x.value('IdInitialBalance[1]','varchar(20)') = '', null, t.x.value('IdInitialBalance[1]','varchar(20)')) as IdInitialBalance,
			t.x.value('PreviousBudget[1]','bit') as PreviousBudget,
			t.x.value('Shares[1]','int') as Shares,
			REPLACE(t.x.value('InvoiceValue[1]','varchar(20)'), ',', '.') as InvoiceValue,
			REPLACE(t.x.value('Value[1]','varchar(20)'), ',', '.') as Value,
			REPLACE(t.x.value('Balance[1]','varchar(20)'), ',', '.') as Balance,
			t.x.value('IdSuppliersDistributionLines[1]','int') as IdSuppliersDistributionLines,
			IIF(t.x.value('CostDistributionDirectCostId[1]','varchar(20)') = '', null, t.x.value('CostDistributionDirectCostId[1]','varchar(20)')) as CostDistributionDirectCostId,
			IIF(t.x.value('PositionId[1]','varchar(20)') = '', null, t.x.value('PositionId[1]','varchar(20)')) as PositionId,
			t.x.value('Hours[1]','int') as Hours,
			t.x.value('ChangeProperties[1]','bit') as ChangeProperties,
			IIF(t.x.value('AuxEntityName[1]','varchar(250)') = '', null, t.x.value('AuxEntityName[1]','varchar(250)')) as AuxEntityName,
			IIF(t.x.value('AuxEntityCode[1]','varchar(20)') = '', null, t.x.value('AuxEntityCode[1]','varchar(20)')) as AuxEntityCode,
			IIF(t.x.value('AuxEntityId[1]','varchar(20)') = '', null, t.x.value('AuxEntityId[1]','varchar(20)')) as AuxEntityId,
			t.x.value('Code[1]','varchar(20)') as Code,
			convert(date, t.x.value('DocumentDate[1]','varchar(20)'), 103) as DocumentDate,
			convert(date, t.x.value('ServicePeriodDate[1]','varchar(20)'), 103) as ServicePeriodDate,
			t.x.value('OperatingUnitId[1]','int') as OperatingUnitId,
			t.x.value('IsDelete[1]','bit') as IsDelete,
			IIF(t.x.value('CommitmentDetailId[1]','varchar(20)') = '', null, t.x.value('CommitmentDetailId[1]','varchar(20)')) as CommitmentDetailId,
			t.x.value('HandlesDocumentSupport[1]','bit') as HandlesDocumentSupport,
			IIF(t.x.value('DocumentSupportId[1]','varchar(20)') = '', null, t.x.value('DocumentSupportId[1]','int')) as DocumentSupportId,
			t.x.value('DeductibleIva[1]','bit') as DeductibleIva,
			IIF(t.x.value('CurrencyId[1]','int') IS NULL OR t.x.value('CurrencyId[1]','int') ='',@OfficialCurrencyId, t.x.value('CurrencyId[1]','int')) as CurrencyId,
			t.x.value('TaxRegistration[1]','tinyint') as TaxRegistration,
			IIf(t.x.value('IdEconomicActivity[1]', 'INT') = 0, NULL, t.x.value('IdEconomicActivity[1]', 'INT')) AS IdEconomicActivity
		from @Xml.nodes('/AccountPayable') t(x)
		
		--Se obtienen los campos necesarios de cualquier item ya que estos datos se repiten
		select top 1 @Code = Code, @DocumentDate = DocumentDate, @ServicePeriodDate = ServicePeriodDate, @OperatingUnitId = OperatingUnitId, @Status = Status
		from @TableAccountPayable

		--Variables de interfaz presupuestal
		SELECT	@BudgetInterface = sp.BudgetInterface,
				@ObligationBudgetInterface = sp.ObligationBudgetInterface,
				@ObligationDebitValue = sp.ObligationDebitValue
		FROM Payments.SettingPayments sp
		WHERE sp.IdOperatingUnit = @OperatingUnitId
	
		--Se obtienen los conceptos de cxp del xml
		INSERT INTO @TableAccountPayableDetailConcepts
		SELECT 
			t.x.value('TempAccountPayableId[1]','int') as TempAccountPayableId,
			t.x.value('TempAccountPayableDetailConceptId[1]','int') as TempAccountPayableDetailConceptId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('IdAccountPayable[1]','int') as IdAccountPayable,
			t.x.value('IdConceptAccountPayable[1]','int') as IdConceptAccountPayable,
			t.x.value('IdAccount[1]','int') as IdAccount,
			t.x.value('IdThirdParty[1]','int') as IdThirdParty,
			IIF(t.x.value('IdCostCenter[1]','varchar(20)') = '', null, t.x.value('IdCostCenter[1]','varchar(20)')) as IdCostCenter,
			t.x.value('Nature[1]','tinyint') as Nature,
			REPLACE(t.x.value('BaseValue[1]','varchar(20)'), ',', '.') as BaseValue,
			IIF(t.x.value('BillingValue[1]','varchar(20)') = '', null, REPLACE(t.x.value('BillingValue[1]','varchar(20)'), ',', '.')) as BillingValue,
			REPLACE(t.x.value('Value[1]','varchar(20)'), ',', '.') as Value,
			IIF(t.x.value('IdRetentionConcept[1]','varchar(20)') = '', null, t.x.value('IdRetentionConcept[1]','varchar(20)')) as IdRetentionConcept,
			IIF(t.x.value('Percentage[1]','varchar(20)') = '', null, REPLACE(t.x.value('Percentage[1]','varchar(20)'), ',', '.')) as Percentage,
			IIF(t.x.value('Detail[1]','varchar(500)') = '', null, t.x.value('Detail[1]','varchar(500)')) as Detail,
			IIF(t.x.value('DeferredCausation[1]','varchar(20)') = '', null, t.x.value('DeferredCausation[1]','varchar(20)')) as DeferredCausation,
			t.x.value('IsDirectCost[1]','bit') as IsDirectCost,
			t.x.value('IsDelete[1]','bit') as IsDelete,
			IIF(t.x.value('RateIva[1]','int') ='', null,t.x.value('RateIva[1]','int')) as RateIva,
			IIF(t.x.value('IvaValue[1]','varchar(20)') = '', null,REPLACE(t.x.value('IvaValue[1]','varchar(20)'), ',', '.')) as IvaValue,
			IIF(t.x.value('TotalConcept[1]','varchar(20)') ='', null,REPLACE(t.x.value('TotalConcept[1]','varchar(20)'), ',', '.')) as TotalConcept
		FROM @Xml.nodes('/AccountPayable/AccountPayableDetailConcept') t(x)
		
		
		--Se obtienen las cuotas de cxp del xml
		insert into @TableAccountPayableShares
		select 
			t.x.value('TempAccountPayableId[1]','int') as TempAccountPayableId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('IdAccountPayable[1]','int') as IdAccountPayable,
			t.x.value('Share[1]','int') as Share,
			convert(date, t.x.value('DateExpires[1]','varchar(20)'), 103) as DateExpires,
			REPLACE(t.x.value('InitialValue[1]','varchar(20)'), ',', '.') as InitialValue,
			REPLACE(t.x.value('DebitValue[1]','varchar(20)'), ',', '.') as DebitValue,
			REPLACE(t.x.value('CreditValue[1]','varchar(20)'), ',', '.') as CreditValue,
			REPLACE(t.x.value('ValueTransfers[1]','varchar(20)'), ',', '.') as ValueTransfers,
			REPLACE(t.x.value('PaymentValue[1]','varchar(20)'), ',', '.') as PaymentValue,
			REPLACE(t.x.value('CrossingValue[1]','varchar(20)'), ',', '.') as CrossingValue,
			REPLACE(t.x.value('Balance[1]','varchar(20)'), ',', '.') as Balance,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/AccountPayable/AccountPayableShares') t(x)
				
		--Se obtienen las cabeceras de las causaciones diferidas del xml
		insert into @TableDeferredCausation
		select 
			t.x.value('TempAccountPayableId[1]','int') as TempAccountPayableId,
			t.x.value('TempDeferredCausationId[1]','int') as TempDeferredCausationId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('IdAccountPayable[1]','int') as IdAccountPayable,
			t.x.value('BillNumber[1]','varchar(20)') as BillNumber,
			t.x.value('IdMainAccount[1]','int') as IdMainAccount,
			t.x.value('PeriodsNumber[1]','int') as PeriodsNumber,
			t.x.value('TypeDistribution[1]','tinyint') as TypeDistribution,
			convert(date, t.x.value('InitialDate[1]','varchar(20)'), 103) as InitialDate,
			convert(date, t.x.value('EndDate[1]','varchar(20)'), 103) as EndDate,
			t.x.value('IdThirdParty[1]','int') as IdThirdParty,
			IIF(t.x.value('IdCostCenter[1]','varchar(20)') = '', null, t.x.value('IdCostCenter[1]','varchar(20)')) as IdCostCenter,
			REPLACE(t.x.value('ValueCreditPeriod[1]','varchar(20)'), ',', '.') as ValueCreditPeriod,
			t.x.value('Status[1]','tinyint') as Status,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/AccountPayable/DeferredCausation') t(x)		

		--Se obtienen los detalles de las causaciones diferidas del xml
		insert into @TableDeferredCausationDetails
		select 
			t.x.value('TempDeferredCausationId[1]','int') as TempDeferredCausationId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('IdDeferredCausation[1]','int') as IdDeferredCausation,
			t.x.value('IdMainAccount[1]','int') as IdMainAccount,
			IIF(t.x.value('IdCostCenter[1]','varchar(20)') = '', null, t.x.value('IdCostCenter[1]','varchar(20)')) as IdCostCenter,
			REPLACE(t.x.value('Value[1]','varchar(20)'), ',', '.') as Value,
			t.x.value('Nature[1]','tinyint') as Nature,
			convert(date, t.x.value('DateNextPeriod[1]','varchar(20)'), 103) as DateNextPeriod,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/AccountPayable/DeferredCausation/DeferredCausationDetails') t(x)

		--Se obtienen las cuotas de las causaciones diferidas del xml
		insert into @TableDeferredCausationShare
		select 
			t.x.value('TempDeferredCausationId[1]','int') as TempDeferredCausationId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('DeferredCausationId[1]','int') as DeferredCausationId,
			t.x.value('PaymentMonth[1]','int') as PaymentMonth,
			t.x.value('PaymentYear[1]','int') as PaymentYear,
			REPLACE(t.x.value('Value[1]','varchar(20)'), ',', '.') as Value,
			t.x.value('Amortized[1]','bit') as Amortized,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/AccountPayable/DeferredCausation/DeferredCausationShare') t(x)
		
		--Se obtiene la calculadora del xml
		insert into @TableAccountPayableDetailConceptLiquidation
		select 
			t.x.value('TempAccountPayableDetailConceptId[1]','int') as TempAccountPayableDetailConceptId,
			t.x.value('TempAccountPayableDetailConceptLiquidationId[1]','int') as TempAccountPayableDetailConceptLiquidationId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('AccountPayableDetailConceptId[1]','int') as AccountPayableDetailConceptId,
			REPLACE(t.x.value('TotalIncome[1]','varchar(20)'), ',', '.') as TotalIncome,
			REPLACE(t.x.value('PensionFundContribution[1]','varchar(20)'), ',', '.') as PensionFundContribution,
			REPLACE(t.x.value('PensionFundContributionReal[1]','varchar(20)'), ',', '.') as PensionFundContributionReal,
			REPLACE(t.x.value('VoluntaryPensionFundContribution[1]','varchar(20)'), ',', '.') as VoluntaryPensionFundContribution,
			REPLACE(t.x.value('VoluntaryPensionFundContributionReal[1]','varchar(20)'), ',', '.') as VoluntaryPensionFundContributionReal,
			REPLACE(t.x.value('SolidarityPensionFund[1]','varchar(20)'), ',', '.') as SolidarityPensionFund,
			REPLACE(t.x.value('SolidarityPensionFundReal[1]','varchar(20)'), ',', '.') as SolidarityPensionFundReal,
			REPLACE(t.x.value('ContributionAccountAFC[1]','varchar(20)'), ',', '.') as ContributionAccountAFC,
			REPLACE(t.x.value('ContributionAccountAFCReal[1]','varchar(20)'), ',', '.') as ContributionAccountAFCReal,
			REPLACE(t.x.value('TotalIncomeExempt[1]','varchar(20)'), ',', '.') as TotalIncomeExempt,
			REPLACE(t.x.value('TotalIncomeExemptReal[1]','varchar(20)'), ',', '.') as TotalIncomeExemptReal,
			REPLACE(t.x.value('PaymentCompulsoryHealth[1]','varchar(20)'), ',', '.') as PaymentCompulsoryHealth,
			REPLACE(t.x.value('PaymentCompulsoryHealthReal[1]','varchar(20)'), ',', '.') as PaymentCompulsoryHealthReal,
			REPLACE(t.x.value('PaymentPrepaidMedical[1]','varchar(20)'), ',', '.') as PaymentPrepaidMedical,
			REPLACE(t.x.value('PaymentPrepaidMedicalReal[1]','varchar(20)'), ',', '.') as PaymentPrepaidMedicalReal,
			REPLACE(t.x.value('PaymentForDependent[1]','varchar(20)'), ',', '.') as PaymentForDependent,
			REPLACE(t.x.value('PaymentForDependentReal[1]','varchar(20)'), ',', '.') as PaymentForDependentReal,
			REPLACE(t.x.value('HousingLoanInterest[1]','varchar(20)'), ',', '.') as HousingLoanInterest,
			REPLACE(t.x.value('HousingLoanInterestReal[1]','varchar(20)'), ',', '.') as HousingLoanInterestReal,
			REPLACE(t.x.value('OccupationalRiskContribution[1]','varchar(20)'), ',', '.') as OccupationalRiskContribution,
			REPLACE(t.x.value('OccupationalRiskContributionReal[1]','varchar(20)'), ',', '.') as OccupationalRiskContributionReal,
			REPLACE(t.x.value('TotalDeduction[1]','varchar(20)'), ',', '.') as TotalDeduction,
			REPLACE(t.x.value('TotalDeductionReal[1]','varchar(20)'), ',', '.') as TotalDeductionReal,
			REPLACE(t.x.value('SubTotal[1]','varchar(20)'), ',', '.') as SubTotal,
			REPLACE(t.x.value('ExemptIncome[1]','varchar(20)'), ',', '.') as ExemptIncome,
			REPLACE(t.x.value('TaxableBase[1]','varchar(20)'), ',', '.') as TaxableBase,
			REPLACE(t.x.value('RetentionValue383[1]','varchar(20)'), ',', '.') as RetentionValue383,
			REPLACE(t.x.value('RetentionValue384[1]','varchar(20)'), ',', '.') as RetentionValue384,
			REPLACE(t.x.value('ApplyRetention[1]','varchar(20)'), ',', '.') as ApplyRetention,
			IIF(t.x.value('MaxDeductionsAndRentExents[1]','varchar(20)') = '', null, REPLACE(t.x.value('MaxDeductionsAndRentExents[1]','varchar(20)'), ',', '.')) as MaxDeductionsAndRentExents,
			REPLACE(t.x.value('PensionByIndividualSavingsRegime[1]','varchar(20)'), ',', '.') as PensionByIndividualSavingsRegime,
			REPLACE(t.x.value('PensionByIndividualSavingsRegimeReal[1]','varchar(20)'), ',', '.') as PensionByIndividualSavingsRegimeReal,
			IIF(t.x.value('UVT[1]','varchar(20)') = '', null, REPLACE(t.x.value('UVT[1]','varchar(20)'), ',', '.')) as UVT,
			IIF(t.x.value('SMLV[1]','varchar(20)') = '', null, REPLACE(t.x.value('SMLV[1]','varchar(20)'), ',', '.')) as SMLV,
			REPLACE(t.x.value('PreviousDeductionsForWithholdings[1]', 'varchar(20)'), ',', '.') As PreviousDeductionsForWithholdings,
			REPLACE(t.x.value('AccumulatedIncome[1]', 'varchar(20)'), ',','.') As AccumulatedIncome,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/AccountPayable/AccountPayableDetailConcept/AccountPayableDetailConceptLiquidation') t(x)

		insert into @TableAccountPayableDetailConceptLiquidationValuesModificated
		select 
			t.x.value('TempAccountPayableDetailConceptLiquidationId[1]','int') as TempAccountPayableDetailConceptLiquidationId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('LiquidationId[1]','int') as LiquidationId,
			t.x.value('ConceptType[1]','tinyint') as ConceptType,
			REPLACE(t.x.value('PreviousValue[1]','varchar(20)'), ',', '.') as PreviousValue,
			REPLACE(t.x.value('NewValue[1]','varchar(20)'), ',', '.') as NewValue,
			t.x.value('Observations[1]','varchar(500)') as Observations,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/AccountPayable/AccountPayableDetailConcept/AccountPayableDetailConceptLiquidation/AccountPayableDetailConceptLiquidationValuesModificated') t(x)

		insert into @TableAccountPayableDetailConceptLiquidationAdjusments
		select 
			t.x.value('TempAccountPayableDetailConceptLiquidationId[1]','int') as TempAccountPayableDetailConceptLiquidationId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('LiquidationId[1]','int') as LiquidationId,
			t.x.value('ConceptType[1]','tinyint') as ConceptType,
			t.x.value('Nature[1]','tinyint') as Nature,
			REPLACE(t.x.value('Value[1]','varchar(20)'), ',', '.') as [Value],
			t.x.value('Observations[1]','varchar(500)') as Observations,
			t.x.value('Status[1]','tinyint') as [Status],
			REPLACE(t.x.value('TotalIncome[1]','varchar(20)'), ',', '.') as [TotalIncome],
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/AccountPayable/AccountPayableDetailConcept/AccountPayableDetailConceptLiquidation/AccountPayableDetailConceptLiquidationAdjusments') t(x)

		--Se obtienen los compromisos asociados a la cxp del xml
		insert into @TableAccountPayableCommitments
		select 
			t.x.value('TempAccountPayableId[1]','int') as TempAccountPayableId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('AccountPayableId[1]','int') as AccountPayableId,
			t.x.value('CommitmentDetailId[1]','int') as CommitmentDetailId,
			REPLACE(t.x.value('Value[1]','varchar(20)'), ',', '.') as Value,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/AccountPayable/AccountPayableCommitments') t(x)

		--Se elimianan los compromisos asociados de la BD y de la tabla temporal
		delete apc from Payments.AccountPayableCommitments apc JOIN @TableAccountPayableCommitments t ON apc.Id = t.Id WHERE t.IsDelete = 1
		delete from @TableAccountPayableCommitments where Id > 0 and IsDelete = 1

		--Se eliminan las cuotas de la causación diferida de la BD cuando se elimina la factura de la rejilla
		delete from Payments.DeferredCausationShare where DeferredCausationId in (
			select dc.Id 
			from @TableAccountPayable temp
			inner join Payments.DeferredCausation dc on dc.IdAccountPayable = temp.Id
			where temp.Id > 0 and temp.IsDelete = 1
		)

		--Se eliminan los detalles de la causación diferida de la BD cuando se elimina la factura de la rejilla
		delete from Payments.DeferredCausationDetails where IdDeferredCausation in (
			select dc.Id 
			from @TableAccountPayable temp
			inner join Payments.DeferredCausation dc on dc.IdAccountPayable = temp.Id
			where temp.Id > 0 and temp.IsDelete = 1
		)
		
		--Se eliminan las cuotas de la causación diferida de la BD cuando se elimina toda la causación
		delete from Payments.DeferredCausationShare where DeferredCausationId in (
			select Id
			from @TableDeferredCausation
			where Id > 0 and IsDelete = 1
		)

		--Se eliminan los detalles de la causación diferida de la BD cuando se elimina toda la causación
		delete from Payments.DeferredCausationDetails where IdDeferredCausation in (
			select Id
			from @TableDeferredCausation
			where Id > 0 and IsDelete = 1
		)

		--Se eliminan las calculadoras de los detalles que fueron eliminados
		delete from Payments.AccountPayableDetailConceptLiquidation where AccountPayableDetailConceptId in (
			select Id
			from @TableAccountPayableDetailConcepts
			where Id > 0 and IsDelete = 1
		)

		--Se eliminan los detalles de la causación diferida de la BD y de la tabla temporal
		delete from Payments.DeferredCausationDetails where Id in (select Id from @TableDeferredCausationDetails where Id > 0 and IsDelete = 1)
		delete from @TableDeferredCausationDetails where Id in (select Id from @TableDeferredCausationDetails where Id > 0 and IsDelete = 1)
		
		--Se eliminan las cuotas de la causación diferida de la BD y de la tabla temporal
		delete from Payments.DeferredCausationShare where Id in (select Id from @TableDeferredCausationShare where Id > 0 and IsDelete = 1)
		delete from @TableDeferredCausationShare where Id in (select Id from @TableDeferredCausationShare where Id > 0 and IsDelete = 1)
		
		--Se eliminan las causaciones diferidas de la BD y de la tabla temporal
		delete from Payments.DeferredCausation where Id in (select Id from @TableDeferredCausation where Id > 0 and IsDelete = 1)
		delete from @TableDeferredCausation where Id in (select Id from @TableDeferredCausation where Id > 0 and IsDelete = 1)

		--Se elimianan las cuotas de la BD y de la tabla temporal
		delete from Payments.AccountPayableShares where Id in (select Id from @TableAccountPayableShares where Id > 0 and IsDelete = 1)
		delete from @TableAccountPayableShares where Id in (select Id from @TableAccountPayableShares where Id > 0 and IsDelete = 1)

		--Se elimina la calculadora de la BD y de la tabla temporal
		delete from Payments.AccountPayableDetailConceptLiquidation where Id in (select Id from @TableAccountPayableDetailConceptLiquidation where Id > 0 and IsDelete = 1)
		delete from @TableAccountPayableDetailConceptLiquidation where Id in (select Id from @TableAccountPayableDetailConceptLiquidation where Id > 0 and IsDelete = 1)

		delete from Payments.AccountPayableDetailConceptLiquidationValuesModificated where Id in (select Id from @TableAccountPayableDetailConceptLiquidationValuesModificated where Id > 0 and IsDelete = 1)
		delete from @TableAccountPayableDetailConceptLiquidationValuesModificated where IsDelete = 1

		delete from Payments.AccountPayableDetailConceptLiquidationAdjusments where Id in (select Id from @TableAccountPayableDetailConceptLiquidationAdjusments where Id > 0 and IsDelete = 1)
		delete from @TableAccountPayableDetailConceptLiquidationAdjusments where IsDelete = 1

		--Se eliminan los conceptos de la BD y de la tabla temporal
		delete from Payments.AccountPayableDetailConcept where Id in (select Id from @TableAccountPayableDetailConcepts where Id > 0 and IsDelete = 1)
		delete from @TableAccountPayableDetailConcepts where Id in (select Id from @TableAccountPayableDetailConcepts where Id > 0 and IsDelete = 1)
		/*----------Se eliminan los datos en la tabla EXCHANGE para actualizarlos---------------------*/
		DELETE FROM [Payments].[AccountPayableExchangeRate]  where AccountPayableId in (select Id from @TableAccountPayable where Id > 0) 
		DELETE FROM [Payments].[DeferredCausationExchangeRate]  where DeferredCausationId in ( SELECT temp.Id FROM @TableDeferredCausation temp WHERE temp.Id > 0) 
		/*----------------------------------------------------------*/
		--Se eliminan las facturas de la BD y de la tabla temporal
		delete from Payments.AccountPayable where Id in (select Id from @TableAccountPayable where Id > 0 and IsDelete = 1)
		delete from @TableAccountPayable where Id in (select Id from @TableAccountPayable where Id > 0 and IsDelete = 1)
			
		--Se valida que el periodo de la fecha del documento este abierto
		if NOT EXISTS (select 1 from [GeneralLedger].[ClosedMonth] With(Nolock) where [Year] = Year(@DocumentDate) and [Month] = Month(@DocumentDate) and [Status] = 1) 
		begin
			select 999 as CodeResult, 'El mes ' + cast(Month(@DocumentDate) as varchar(2)) + ' de la fecha de documento no se encuentra abierto' as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
			return
		end

		--Se valida que el periodo de la fecha radicación este abierto
		if NOT EXISTS (select 1 from [GeneralLedger].[ClosedMonth] With(Nolock) where [Year] = Year(@ServicePeriodDate) and [Month] = Month(@ServicePeriodDate) and [Status] = 1) 
		begin
			select 999 as CodeResult, 'El mes ' + cast(Month(@ServicePeriodDate) as varchar(2)) + ' de la fecha de radicación no se encuentra abierto' as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
			return
		end
		
		--Si no hay ninguna factura guardada, se realiza la lógica del consecutivo de radicación
		if ISNULL((select top 1 Id from @TableAccountPayable where Id > 0), 0) = 0
		begin
			--Valido que hayan parámetros de empresa para poder sacar el valor del consecutivo del radicado
			if NOT EXISTS (select 1 from GeneralLedger.CompanySettings With(Nolock))
			begin
				select 999 as CodeResult, 'No existe parámetros de empresa para obtener el consecutivo de radicación' as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
				return
			end

			--Se obtiene el consecutivo del radicado
			select top 1 @ConsecutiveFiling = ConsecutiveFiling from GeneralLedger.CompanySettings With(Nolock)

			--Se actualiza el consecutivo de radicación
			update GeneralLedger.CompanySettings set ConsecutiveFiling += 1
		end
		else begin --Si hay alguna factura guardada se obtiene el consecutivo de radicación
			set @ConsecutiveFiling = (select top 1 NumberFiling from @TableAccountPayable where Id > 0)
		end		

		--Se validan que los numeros de facturas contengan al menos un numero
		if EXISTS (select * from @TableAccountPayable where PATINDEX('%[0-9]%', BillNumber) = 0 and IsDelete = 0)
		begin
			set @Errors = ''
			select @Errors = stuff((select N'; El No. de factura ' + BillNumber + ' debe tener al menos un número'
			from @TableAccountPayable where PATINDEX('%[0-9]%', BillNumber) = 0 and IsDelete = 0
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			select 999 as CodeResult, @Errors as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
			return
		end
		
		--Se validan que los No. de facturas no existan con el proveedor
		if EXISTS (select 1
		from Payments.AccountPayable ap
		inner join @TableAccountPayable tap on tap.BillNumber = ap.BillNumber and tap.IdSupplier = ap.IdSupplier
		where tap.Id <> ap.Id and tap.IsDelete = 0)
		begin
			set @Errors = ''
			select @Errors = stuff((select N'; El No. de factura ' + ap.BillNumber + ' ya existe con el proveedor ' + s.Code + ' - ' + s.Name
			from Payments.AccountPayable ap
			inner join @TableAccountPayable tap on tap.BillNumber = ap.BillNumber and tap.IdSupplier = ap.IdSupplier
			inner join Common.Supplier s on s.Id = ap.IdSupplier
			where tap.Id <> ap.Id and tap.IsDelete = 0
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			select 999 as CodeResult, @Errors as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
			return
		end

		--Se validan que las facturas no esten asociadas a un linea de distribución inactiva
		IF EXISTS 
		(
			SELECT 1
			FROM @TableAccountPayable tap 
			LEFT JOIN Common.SuppliersDistributionLines sdl ON tap.IdSuppliersDistributionLines = sdl.Id
			WHERE tap.IsDelete = 0 AND ISNULL(sdl.Status, 0) = 0
		)
		BEGIN
			SELECT @Errors = STUFF((
				SELECT N'; La factura No. ' + tap.BillNumber + ' se encuentra asociada a una linea de distribución inactiva'
				FROM @TableAccountPayable tap 
				LEFT JOIN Common.SuppliersDistributionLines sdl ON tap.IdSuppliersDistributionLines = sdl.Id
				WHERE tap.IsDelete = 0 AND ISNULL(sdl.Status, 0) = 0
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 as CodeResult, @Errors as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
			RETURN
		END

		--Se valida que si la cxp tiene asociado un id de distribución de elementos del costo no este asociado a otra cxp
		if (@Status = 1 or @Status = 2) and EXISTS (select 1 from @TableAccountPayable where CostDistributionDirectCostId is not null and CostDistributionDirectCostId > 0 and IsDelete = 0)
		begin
			if EXISTS (select 1
			from Payments.AccountPayable
			where CostDistributionDirectCostId in (select CostDistributionDirectCostId from @TableAccountPayable where CostDistributionDirectCostId is not null and CostDistributionDirectCostId > 0 and IsDelete = 0)
			and Status <> 3 and Id not in (select Id from @TableAccountPayable where CostDistributionDirectCostId is not null and CostDistributionDirectCostId > 0 and IsDelete = 0))
			begin
				set @Errors = ''
				select @Errors = stuff((select N'; No se puede guardar porque la distribución de elementos del costo asociado ya está asignado a la CxP ' + Code + ' con No. factura ' + BillNumber
				from Payments.AccountPayable
				where CostDistributionDirectCostId in (select CostDistributionDirectCostId from @TableAccountPayable where CostDistributionDirectCostId is not null and CostDistributionDirectCostId > 0 and IsDelete = 0)
				and Status <> 3 and Id not in (select Id from @TableAccountPayable where CostDistributionDirectCostId is not null and CostDistributionDirectCostId > 0 and IsDelete = 0)
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				select 999 as CodeResult, @Errors as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
				return	
			end
		end
		
		IF @BudgetInterface = 1
		BEGIN --Si maneja interfaz presupuestal
			IF @ObligationBudgetInterface = 1
			BEGIN --Si la interfaz presupuestal es obligatoria
				IF EXISTS 
				(
					SELECT 1 
					FROM @TableAccountPayable  tap
					LEFT JOIN @TableAccountPayableCommitments tapc ON tap.TempAccountPayableId = tapc.TempAccountPayableId AND tapc.IsDelete = 0
					LEFT JOIN Budget.CommitmentDetail cd ON tapc.CommitmentDetailId = cd.Id
					WHERE cd.Id IS NULL AND (ISNULL(tap.EntityName, '') NOT IN ('CostDistributionDirectCost') OR tap.Id > 0)
				) 
				BEGIN
					SET @Errors = ''
					SELECT @Errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + Code + ' con No. factura ' + BillNumber
						FROM @TableAccountPayable  tap
						LEFT JOIN @TableAccountPayableCommitments tapc ON tap.TempAccountPayableId = tapc.TempAccountPayableId AND tapc.IsDelete = 0
						LEFT JOIN Budget.CommitmentDetail cd ON tapc.CommitmentDetailId = cd.Id
						WHERE cd.Id IS NULL AND (ISNULL(tap.EntityName, '') NOT IN ('CostDistributionDirectCost') OR tap.Id > 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT 999 as CodeResult, 'Las siguientes facturas no tienen un compromiso seleccionado: ' + CHAR(13) + CHAR(10) + ISNULL(@Errors, '') as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
					RETURN
				END
			END

			IF EXISTS
			(
				SELECT 1
				FROM 
				(
					SELECT tapc.TempAccountPayableId, SUM(tapc.Value) Value
					FROM @TableAccountPayableCommitments tapc
					WHERE tapc.IsDelete = 0
					GROUP BY tapc.TempAccountPayableId
				) tapc				
				LEFT JOIN
				(
					SELECT tapdc.TempAccountPayableId, SUM(tapdc.Value * IIF(tapdc.Nature = 1, 1, IIF(@ObligationDebitValue = 1, 0, -1))) Value
					FROM @TableAccountPayableDetailConcepts tapdc
					WHERE tapdc.IsDelete = 0
					GROUP BY tapdc.TempAccountPayableId
				) tapdc ON tapdc.TempAccountPayableId = tapc.TempAccountPayableId
				WHERE ROUND(tapc.Value, 0) <> ROUND(ISNULL(tapdc.Value, 0), 0)
			)
			BEGIN
				SET @Errors = ''
					SELECT @Errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + Code + ' con No. factura ' + BillNumber + ': El valor de la obligación presupuestal (' + CAST(ROUND(tapc.Value, 0) AS VARCHAR(20)) + ') no es igual al valor de la factura (' + CAST(ROUND(ISNULL(tapdc.Value, 0), 0) AS VARCHAR(20)) + ')'
						FROM @TableAccountPayable t
						JOIN
						(
							SELECT tapc.TempAccountPayableId, SUM(tapc.Value) Value
							FROM @TableAccountPayableCommitments tapc
							WHERE tapc.IsDelete = 0
							GROUP BY tapc.TempAccountPayableId
						) tapc ON t.TempAccountPayableId = tapc.TempAccountPayableId
						LEFT JOIN
						(
							SELECT tapdc.TempAccountPayableId, SUM(tapdc.Value * IIF(tapdc.Nature = 1, 1, IIF(@ObligationDebitValue = 1, 0, -1))) Value
							FROM @TableAccountPayableDetailConcepts tapdc
							WHERE tapdc.IsDelete = 0
							GROUP BY tapdc.TempAccountPayableId
						) tapdc ON tapdc.TempAccountPayableId = tapc.TempAccountPayableId
						WHERE ROUND(tapc.Value, 0) <> ROUND(ISNULL(tapdc.Value, 0), 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
				SELECT 999 as CodeResult, 'Las siguientes facturas presentaron errores: ' + CHAR(13) + CHAR(10) + ISNULL(@Errors, '') as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
				RETURN
			END
		END
		
		--Si no viene el código se genera
		if @Code = '' or @Code is null
		begin
			--Consultamos si la secuencia es con O o OU
			declare @scope varchar(5) = ''
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @scope = Scope from Payments.PaymentsSecuence
			where IdForm = '730'

			--Se valida el scope
			if @scope = 'O'
			Begin
				-- Consultamos la secuencia numerica del form de CxP
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from Payments.PaymentsSecuenceDetail bsd 
				inner join Payments.PaymentsSecuence bs on bs.Id = bsd.IdSequensePaymentsC 
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '730'
				order by bsd.Next desc
			End
			Else
			Begin
				-- Consultamos la secuencia numerica del form de CxP
				select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from Payments.PaymentsSecuenceDetail bsd 
				inner join Payments.PaymentsSecuence bs on bs.Id = bsd.IdSequensePaymentsC 
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '730' and bsd.IdOperatingUnit = @OperatingUnitId
			End

			if (@idSequenceDetail is null)
			Begin
				select 999 as CodeResult, 'Secuencia no encontrada para generar la cuenta por pagar' as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
				return
			End

			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update Payments.PaymentsSecuenceDetail set [Next] += 1 where Id = @idSequenceDetail
		end

		IF EXISTS(SELECT 1 FROM @TableAccountPayable WHERE EntityName = 'CostDistributionDirectCost')
		BEGIN
			--- Determina si  puede o no manejar documento soporte
			DECLARE @HandlesDocumentSupport BIT = (SELECT HandlesSupportDocument 
													 FROM GeneralLedger.GeneralLedgerSettings 
													 WHERE IdOperatingUnit = @OperatingUnitId)

			-- Si el tercero no es facturador electronico, genera documento soporte
			IF @HandlesDocumentSupport = 1
			BEGIN
					UPDATE  ap
					SET ap.HandlesDocumentSupport = 1
					FROM @TableAccountPayable ap
					INNER JOIN Common.ThirdParty tp on tp.id = ap.IdThirdParty
					WHERE AP.EntityName = 'CostDistributionDirectCost' AND TP.ElectronicBiller = 0
			END
		END
		
		--Se actualizan las cabeceras
		IF EXISTS (SELECT 1 FROM @TableAccountPayable WHERE Id > 0 and IsDelete = 0)
		BEGIN
			UPDATE ap SET ap.[Code] = @Code, ap.[NumberFiling] = @ConsecutiveFiling, ap.[EntityId] = temp.EntityId, ap.[EntityCode] = temp.EntityCode, ap.[EntityName] = temp.EntityName, 
			ap.[IdSupplier] = temp.IdSupplier, ap.[IdThirdParty] = temp.IdThirdParty, ap.[IdAccount] = temp.IdAccount, ap.[IdCostCenter] = temp.IdCostCenter, ap.[BillNumber] = temp.BillNumber,
			ap.[BillDate] = temp.BillDate, ap.[DocumentDate] = @DocumentDate, ap.[ServicePeriodDate] = @ServicePeriodDate, ap.[FilingUnitId] = temp.FilingUnitId, ap.[SupplierTypeId] = temp.SupplierTypeId, 
			ap.[Term] = temp.Term, ap.[ExpirationDate] = temp.ExpirationDate, ap.[Coments] = temp.Coments, ap.[Status] = temp.Status, ap.[InitialBalance] = temp.InitialBalance, 
			ap.[IdInitialBalance] = temp.IdInitialBalance, ap.[PreviousBudget] = temp.PreviousBudget, ap.[Shares] = temp.Shares, ap.[InvoiceValue] = temp.InvoiceValue, ap.[Value] = temp.Value, 
			ap.[Balance] = temp.Balance, ap.[IdOperatingUnit] = @OperatingUnitId, ap.[IdSuppliersDistributionLines] = temp.IdSuppliersDistributionLines, ap.ModificationUser = @UserCode, 
			ap.ModificationDate = [Common].[GETDATE](), ap.[CostDistributionDirectCostId] = temp.CostDistributionDirectCostId, ap.[PositionId] = temp.PositionId, 
			ap.[Hours] = temp.Hours, ap.CommitmentDetailId = temp.CommitmentDetailId, ap.HandlesDocumentSupport = temp.HandlesDocumentSupport, ap.DocumentSupportId = temp.DocumentSupportId, ap.DeductibleIva = temp.DeductibleIva,
			ap.[CurrencyId]=temp.CurrencyId, ap.TaxRegistration = temp.TaxRegistration, ap.IdEconomicActivity = temp.IdEconomicActivity
			FROM @TableAccountPayable temp
			INNER JOIN Payments.AccountPayable ap on ap.Id = temp.Id
			WHERE temp.Id > 0 and temp.IsDelete = 0
		END

		--Se guardan las cabeceras nuevas
		IF EXISTS (SELECT 1 FROM @TableAccountPayable WHERE Id = 0)
		BEGIN		
			INSERT INTO [Payments].[AccountPayable]
			(
				[Code], [NumberFiling], [EntityId], [EntityCode], [EntityName], [IdSupplier], [IdThirdParty], [IdAccount], [IdCostCenter], [BillNumber],
				[BillDate], [DocumentDate], [ServicePeriodDate], [FilingUnitId], [SupplierTypeId], [Term], [ExpirationDate], [Coments], [Status], [InitialBalance], [IdInitialBalance], [PreviousBudget],
				[Shares], [InvoiceValue], [Value], [Balance], [IdOperatingUnit], [IdSuppliersDistributionLines], [CreationUser], [CreationDate], [CostDistributionDirectCostId], 
				[PositionId], [Hours], CommitmentDetailId, HandlesDocumentSupport, DocumentSupportId, DeductibleIva,CurrencyId, TaxRegistration, IdEconomicActivity
			) OUTPUT inserted.Id, inserted.BillNumber, inserted.IdSupplier, INSERTED.CurrencyId into @IDSAccountPayable(Id, BillNumber, IdSupplier,CurrencyId)
			SELECT	@Code, @ConsecutiveFiling, IIF(ChangeProperties = 1, AuxEntityId, EntityId), IIF(ChangeProperties = 1, AuxEntityCode, EntityCode), IIF(ChangeProperties = 1, AuxEntityName, EntityName),
					IdSupplier, IdThirdParty, IdAccount, IdCostCenter, BillNumber, BillDate, @DocumentDate, @ServicePeriodDate, FilingUnitId, SupplierTypeId, Term, ExpirationDate, Coments, Status, InitialBalance,
					IdInitialBalance, PreviousBudget, Shares, InvoiceValue, Value, Balance, @OperatingUnitId, IdSuppliersDistributionLines, @UserCode, [Common].[GETDATE](), 
					CostDistributionDirectCostId, PositionId, Hours, CommitmentDetailId, HandlesDocumentSupport, DocumentSupportId, DeductibleIva,CurrencyId, TaxRegistration, IdEconomicActivity
			FROM @TableAccountPayable
			WHERE Id = 0

			--Se actualizan los ids generados
			UPDATE temp SET temp.Id = ids.Id
			FROM @TableAccountPayable temp
			INNER JOIN @IDSAccountPayable ids on ids.BillNumber = temp.BillNumber and ids.IdSupplier = temp.IdSupplier

			--Se actualiza el id de la cxp del detalle
			UPDATE apdc SET apdc.IdAccountPayable = ap.Id
			FROM @TableAccountPayable ap
			INNER JOIN @TableAccountPayableDetailConcepts apdc on apdc.TempAccountPayableId = ap.TempAccountPayableId
			WHERE apdc.Id = 0
		END

		--Se actualizan los conceptos
		IF EXISTS (SELECT 1 FROM @TableAccountPayableDetailConcepts WHERE Id > 0 and IsDelete = 0)
		BEGIN
			UPDATE apdc set apdc.[IdAccountPayable] = temp.IdAccountPayable, apdc.[IdConceptAccountPayable] = temp.IdConceptAccountPayable, apdc.[IdAccount] = temp.IdAccount, 
			apdc.[IdThirdParty] = temp.IdThirdParty, apdc.[IdCostCenter] = temp.IdCostCenter, apdc.[Nature] = temp.Nature, apdc.[BaseValue] = temp.BaseValue, apdc.[BillingValue] = temp.BillingValue, 
			apdc.[Value] = temp.Value, apdc.[IdRetentionConcept] = temp.IdRetentionConcept, apdc.[Percentage] = temp.Percentage, apdc.[Detail] = temp.Detail, 
			apdc.[DeferredCausation] = temp.DeferredCausation, apdc.[IsDirectCost] = temp.IsDirectCost,  apdc.[RateIva] = temp.RateIva, apdc.[IvaValue] = temp.IvaValue, apdc.[TotalConcept] = temp.TotalConcept
			FROM @TableAccountPayableDetailConcepts temp
			INNER JOIN Payments.AccountPayableDetailConcept apdc on apdc.Id = temp.Id
			WHERE temp.Id > 0 and temp.IsDelete = 0
		end

		--Se guardan los conceptos nuevos
		IF EXISTS (SELECT 1 FROM @TableAccountPayableDetailConcepts WHERE Id = 0)
		BEGIN
			INSERT INTO [Payments].[AccountPayableDetailConcept]([IdAccountPayable], [IdConceptAccountPayable], [IdAccount], [IdThirdParty], [IdCostCenter], [Nature], [BaseValue],
			[BillingValue], [Value], [IdRetentionConcept], [Percentage], [Detail], [DeferredCausation], 
			[IsDirectCost], [RateIva], [IvaValue], [TotalConcept]) output inserted.Id, inserted.IdConceptAccountPayable, inserted.IdAccount, inserted.IdThirdParty, inserted.IdCostCenter, inserted.Nature, inserted.IdAccountPayable 
			INTO @IDSAccountPayableDetailConcept(Id, IdConceptAccountPayable, IdAccount, IdThirdParty, IdCostCenter, Nature, IdAccountPayable)
			select ap.Id, apdc.IdConceptAccountPayable, apdc.IdAccount, apdc.IdThirdParty, apdc.IdCostCenter, apdc.Nature, apdc.BaseValue, apdc.BillingValue, apdc.Value,
			apdc.IdRetentionConcept, apdc.Percentage, apdc.Detail, apdc.DeferredCausation, apdc.IsDirectCost, apdc.RateIva, apdc.IvaValue, apdc.TotalConcept
			FROM @TableAccountPayableDetailConcepts apdc
			INNER JOIN @TableAccountPayable ap on ap.TempAccountPayableId = apdc.TempAccountPayableId
			WHERE apdc.Id = 0  
			
			--Se actualizan los ids generados
			UPDATE d SET d.Id = ids.Id
			FROM @TableAccountPayableDetailConcepts d
			INNER JOIN @IDSAccountPayableDetailConcept ids on ids.IdConceptAccountPayable = d.IdConceptAccountPayable and ids.IdAccount = d.IdAccount 
			AND ids.IdThirdParty = d.IdThirdParty and ISNULL(ids.IdCostCenter, 0) = ISNULL(d.IdCostCenter, 0) and ids.Nature = d.Nature and ids.IdAccountPayable = d.IdAccountPayable   
		END
		
		--Se actualizan las cuotas
		IF EXISTS (SELECT 1 FROM @TableAccountPayableShares WHERE Id > 0 and IsDelete = 0)
		BEGIN
			UPDATE aps set aps.[IdAccountPayable] = temp.IdAccountPayable, aps.[Share] = temp.Share, aps.[DateExpires] = temp.DateExpires, aps.[InitialValue] = temp.InitialValue, aps.[DebitValue] = temp.DebitValue, 
			aps.[CreditValue] = temp.CreditValue, aps.[ValueTransfers] = temp.ValueTransfers, aps.[PaymentValue] = temp.PaymentValue, aps.[CrossingValue] = temp.CrossingValue, aps.[Balance] = temp.Balance
			FROM @TableAccountPayableShares temp
			INNER JOIN Payments.AccountPayableShares aps ON aps.Id = temp.Id
			WHERE temp.Id > 0 and temp.IsDelete = 0
		END
		
		--Se guardan las cuotas nuevas
		IF EXISTS (SELECT 1 FROM @TableAccountPayableShares WHERE Id = 0)
		BEGIN
			INSERT INTO [Payments].[AccountPayableShares]([IdAccountPayable], [Share], [DateExpires], [InitialValue], [DebitValue], [CreditValue], [ValueTransfers], 
			[PaymentValue], [CrossingValue], [Balance])
			SELECT a.Id, s.Share, s.DateExpires, s.InitialValue, s.DebitValue, s.CreditValue, s.ValueTransfers, s.PaymentValue, s.CrossingValue, s.Balance
			FROM @TableAccountPayableShares s
			INNER JOIN @TableAccountPayable a on a.TempAccountPayableId = s.TempAccountPayableId
			WHERE s.Id = 0
		END

		--Se actualizan las causaciones diferidas
		IF EXISTS (SELECT 1 FROM @TableDeferredCausation WHERE Id > 0 and IsDelete = 0)
		BEGIN
			UPDATE dc SET dc.[IdAccountPayable] = temp.IdAccountPayable, dc.[BillNumber] = temp.BillNumber, dc.[IdMainAccount] = temp.IdMainAccount, dc.[PeriodsNumber] = temp.PeriodsNumber,
			dc.[TypeDistribution] = temp.TypeDistribution, dc.[InitialDate] = temp.InitialDate, dc.[EndDate] = temp.EndDate, dc.[IdThirdParty] = temp.IdThirdParty, dc.[IdCostCenter] = temp.IdCostCenter,
			dc.[ValueCreditPeriod] = temp.ValueCreditPeriod, dc.[Status] = temp.Status, dc.[ModificationUser] = @UserCode, dc.[ModificationDate] = [Common].[GETDATE]()
			FROM @TableDeferredCausation temp
			INNER JOIN Payments.DeferredCausation dc on dc.Id = temp.Id
			WHERE temp.Id > 0 and temp.IsDelete = 0
		END
		
		--Se guardan las causaciones diferidas nuevas
		IF EXISTS (SELECT 1 FROM @TableDeferredCausation WHERE Id = 0)
		BEGIN
			INSERT INTO [Payments].[DeferredCausation]([IdAccountPayable], [BillNumber], [IdMainAccount], [PeriodsNumber], [TypeDistribution], [InitialDate], [EndDate], [IdThirdParty],
			[IdCostCenter], [ValueCreditPeriod], [Status], 
			[CreationUser], [CreationDate]) OUTPUT inserted.Id, inserted.BillNumber, inserted.IdMainAccount, inserted.IdCostCenter into @IDSDeferredCausation(Id, BillNumber, IdMainAccount, IdCostCenter)
			SELECT a.Id, d.BillNumber, d.IdMainAccount, d.PeriodsNumber, d.TypeDistribution, d.InitialDate, d.EndDate, d.IdThirdParty, d.IdCostCenter, d.ValueCreditPeriod, 
			d.Status, @UserCode, [Common].[GETDATE]()
			FROM @TableDeferredCausation d
			INNER JOIN @TableAccountPayable a ON a.TempAccountPayableId = d.TempAccountPayableId
			WHERE d.Id = 0			

			--Se actualizan los ids generados
			UPDATE dc SET dc.Id = ids.Id
			from @TableDeferredCausation dc
			inner join @IDSDeferredCausation ids on ids.BillNumber = dc.BillNumber and ids.IdMainAccount = dc.IdMainAccount and ISNULL(ids.IdCostCenter, 0) = ISNULL(dc.IdCostCenter, 0)
		end

		/*-------------------INSERCION TABLA EXGANGED DE CXP--------*/
					DECLARE @_entityId INT,@CurrencyId INT, @_entityName VARCHAR(25)
					DECLARE Cursor_A CURSOR LOCAL
						FOR SELECT e.Id,e.CurrencyId,e.EntityName
							FROM (	SELECT Id,CurrencyId,'AccountPayable' EntityName
									FROM @TableAccountPayable 
									WHERE Id > 0 and IsDelete = 0
									
									UNION ALL

									SELECT dc.Id,ap.CurrencyId,'DeferredCausation' EntityName
									FROM @TableDeferredCausation temp
									JOIN Payments.DeferredCausation dc WITH(NOLOCK) on temp.Id = dc.Id
									JOIN Payments.AccountPayable ap WITH(NOLOCK) on dc.IdAccountPayable =ap.Id
									WHERE temp.Id > 0 and temp.IsDelete = 0 ) e
					OPEN Cursor_A
					FETCH NEXT FROM Cursor_A INTO @_entityId,@CurrencyId,@_entityName
						WHILE (@@FETCH_STATUS = 0)
						BEGIN
						DECLARE @StateResult VARCHAR(3), @MessageOutput VARCHAR(100)
						
						--print @_entityName
						--print @_entityId
						--print @CurrencyId
						--print @DocumentDate
						
						EXEC [Common].[SP_InsertIntoExchangeRateWithDate] @_entityName,@_entityId,@CurrencyId,@DocumentDate,@StateResult=@StateResult OUTPUT, @MessageOutput = @MessageOutput OUTPUT

					

						IF ISNULL(@StateResult, '999') = '999' BEGIN
							FETCH NEXT FROM Cursor_A INTO @_entityId,@CurrencyId,@_entityName
							CLOSE Cursor_A
							DEALLOCATE Cursor_A
							SELECT 999 as CodeResult,
							CONCAT('Error al insertar en la tasa de cambio: ', @MessageOutput)  as MessageResult,
							'' AccountPayableCode,
							'' ConsecutiveFiling,
							0 AccountPayableId
							RETURN
						END

						FETCH NEXT FROM Cursor_A INTO @_entityId,@CurrencyId,@_entityName
						END
					CLOSE Cursor_A
					DEALLOCATE Cursor_A
				
		/*--------------------------------------------------------------------*/
	
		--Se actualizan los detalles de causaciones diferidas
		if EXISTS (select 1 from @TableDeferredCausationDetails where Id > 0 and IsDelete = 0)
		begin
			UPDATE dcd SET dcd.[IdDeferredCausation] = temp.IdDeferredCausation, dcd.[IdMainAccount] = temp.IdMainAccount, dcd.[IdCostCenter] = temp.IdCostCenter, dcd.[Value] = temp.Value,
			dcd.[Nature] = temp.Nature, dcd.[DateNextPeriod] = temp.DateNextPeriod			
			from @TableDeferredCausationDetails temp
			inner join Payments.DeferredCausationDetails dcd on dcd.Id = temp.Id
			where temp.Id > 0 and temp.IsDelete = 0
		end
		
		--Se guardan los detalles de causaciones diferidas nuevas
		if EXISTS (select 1 from @TableDeferredCausationDetails where Id = 0)
		begin
			INSERT INTO [Payments].[DeferredCausationDetails]([IdDeferredCausation], [IdMainAccount], [IdCostCenter], [Value], [Nature], [DateNextPeriod])
			select c.Id, d.IdMainAccount, d.IdCostCenter, d.Value, d.Nature, d.DateNextPeriod
			from @TableDeferredCausationDetails d
			inner join @TableDeferredCausation c on c.TempDeferredCausationId = d.TempDeferredCausationId
			where d.Id = 0
		end
		
		--Se actualizan las cuotas de causaciones diferidas
		if EXISTS (select 1 from @TableDeferredCausationShare where Id > 0 and IsDelete = 0)
		begin
			UPDATE dcs SET dcs.[DeferredCausationId] = temp.DeferredCausationId, dcs.[PaymentMonth] = temp.PaymentMonth, dcs.[PaymentYear] = temp.PaymentYear, dcs.[Value] = temp.Value,
			dcs.[Amortized] = temp.Amortized, dcs.[ModificationUser] = @UserCode, [ModificationDate] = [Common].[GETDATE]()
			from @TableDeferredCausationShare temp
			inner join Payments.DeferredCausationShare dcs on dcs.Id = temp.Id
			where temp.Id > 0 and temp.IsDelete = 0
		end
		
		--Se guardan las cuotas de causaciones diferidas nuevas
		if EXISTS (select 1 from @TableDeferredCausationShare where Id = 0)
		begin
			INSERT INTO [Payments].[DeferredCausationShare]([DeferredCausationId], [PaymentMonth], [PaymentYear], [Value], [Amortized], [CreationUser], [CreationDate])
			select c.Id, s.PaymentMonth, s.PaymentYear, s.Value, s.Amortized, @UserCode, [Common].[GETDATE]()
			from @TableDeferredCausationShare s
			inner join @TableDeferredCausation c on c.TempDeferredCausationId = s.TempDeferredCausationId
			where s.Id = 0
		end
		
		--Se actualizan las calculadoras
		if EXISTS (select 1 from @TableAccountPayableDetailConceptLiquidation where Id > 0 and IsDelete = 0)
		begin
			UPDATE l SET l.[AccountPayableDetailConceptId] = temp.AccountPayableDetailConceptId, l.[TotalIncome] = temp.TotalIncome, l.[PensionFundContribution] = temp.PensionFundContribution,
			l.[PensionFundContributionReal] = temp.PensionFundContributionReal, l.[VoluntaryPensionFundContribution] = temp.VoluntaryPensionFundContribution, 
			l.[VoluntaryPensionFundContributionReal] = temp.VoluntaryPensionFundContributionReal, l.[SolidarityPensionFund] = temp.SolidarityPensionFund, 
			l.[SolidarityPensionFundReal] = temp.SolidarityPensionFundReal, l.[ContributionAccountAFC] = temp.ContributionAccountAFC, l.[ContributionAccountAFCReal] = temp.ContributionAccountAFCReal,
			l.[TotalIncomeExempt] = temp.TotalIncomeExempt, l.[TotalIncomeExemptReal] = temp.TotalIncomeExemptReal, l.[PaymentCompulsoryHealth] = temp.PaymentCompulsoryHealth, 
			l.[PaymentCompulsoryHealthReal] = temp.PaymentCompulsoryHealthReal, l.[PaymentPrepaidMedical] = temp.PaymentPrepaidMedical, l.[PaymentPrepaidMedicalReal] = temp.PaymentPrepaidMedicalReal,
			l.[PaymentForDependent] = temp.PaymentForDependent, l.[PaymentForDependentReal] = temp.PaymentForDependentReal, l.[HousingLoanInterest] = temp.HousingLoanInterest,
			l.[HousingLoanInterestReal] = temp.HousingLoanInterestReal, l.[OccupationalRiskContribution] = temp.OccupationalRiskContribution, 
			l.[OccupationalRiskContributionReal] = temp.OccupationalRiskContributionReal, l.[TotalDeduction] = temp.TotalDeduction, l.[TotalDeductionReal] = temp.TotalDeductionReal, 
			l.[SubTotal] = temp.SubTotal, l.[ExemptIncome] = temp.ExemptIncome, l.[TaxableBase] = temp.TaxableBase, l.[RetentionValue383] = temp.RetentionValue383,
			l.[RetentionValue384] = temp.RetentionValue384, l.[ApplyRetention] = temp.ApplyRetention, l.[MaxDeductionsAndRentExents] = temp.MaxDeductionsAndRentExents,
			l.[PensionByIndividualSavingsRegime] = temp.PensionByIndividualSavingsRegime, l.[PensionByIndividualSavingsRegimeReal] = temp.PensionByIndividualSavingsRegimeReal,
			l.[UVT] = temp.UVT, l.[SMLV] = temp.SMLV, l.PreviousDeductionsForWithholdings = temp.PreviousDeductionsForWithholdings, l.AccumulatedIncome = temp.AccumulatedIncome
			from @TableAccountPayableDetailConceptLiquidation temp
			inner join Payments.AccountPayableDetailConceptLiquidation l on l.Id = temp.Id
			where temp.Id > 0 and temp.IsDelete = 0
		end
		
		--Se guardan las calculadoras nuevas
		if EXISTS (select 1 from @TableAccountPayableDetailConceptLiquidation where Id = 0)
		begin
			DECLARE @TmpLiquidationRows INT,
					@TmpLiquidationId INT

			SET @TmpLiquidationRows = 1
			SET @TmpLiquidationId = 0

			WHILE @TmpLiquidationRows > 0
			BEGIN
				SELECT TOP 1
					@TmpLiquidationId = TempAccountPayableDetailConceptLiquidationId
				FROM @TableAccountPayableDetailConceptLiquidation 
				WHERE Id = 0
					AND TempAccountPayableDetailConceptLiquidationId > @TmpLiquidationId
				ORDER BY TempAccountPayableDetailConceptLiquidationId

				SET @TmpLiquidationRows = @@ROWCOUNT
				IF @TmpLiquidationRows = 0 
				BEGIN
					BREAK
				END

				---------------------------------------------------------------

				INSERT INTO [Payments].[AccountPayableDetailConceptLiquidation]([AccountPayableDetailConceptId], [TotalIncome], [PensionFundContribution], [PensionFundContributionReal], [VoluntaryPensionFundContribution],
				[VoluntaryPensionFundContributionReal], [SolidarityPensionFund], [SolidarityPensionFundReal], [ContributionAccountAFC], [ContributionAccountAFCReal], [TotalIncomeExempt], [TotalIncomeExemptReal],
				[PaymentCompulsoryHealth], [PaymentCompulsoryHealthReal], [PaymentPrepaidMedical], [PaymentPrepaidMedicalReal], [PaymentForDependent], [PaymentForDependentReal], [HousingLoanInterest],
				[HousingLoanInterestReal], [OccupationalRiskContribution], [OccupationalRiskContributionReal], [TotalDeduction], [TotalDeductionReal], [SubTotal], [ExemptIncome], [TaxableBase], [RetentionValue383],
				[RetentionValue384], [ApplyRetention], [MaxDeductionsAndRentExents], [PensionByIndividualSavingsRegime], [PensionByIndividualSavingsRegimeReal], [UVT], [SMLV], [PreviousDeductionsForWithholdings], [AccumulatedIncome])
				select d.Id, l.TotalIncome, l.PensionFundContribution, l.PensionFundContributionReal, l.VoluntaryPensionFundContribution, l.VoluntaryPensionFundContributionReal, l.SolidarityPensionFund, 
				l.SolidarityPensionFundReal, l.ContributionAccountAFC, l.ContributionAccountAFCReal, l.TotalIncomeExempt, l.TotalIncomeExemptReal, l.PaymentCompulsoryHealth, l.PaymentCompulsoryHealthReal, 
				l.PaymentPrepaidMedical, l.PaymentPrepaidMedicalReal, l.PaymentForDependent, l.PaymentForDependentReal, l.HousingLoanInterest, l.HousingLoanInterestReal, l.OccupationalRiskContribution, 
				l.OccupationalRiskContributionReal, l.TotalDeduction, l.TotalDeductionReal, l.SubTotal, l.ExemptIncome, l.TaxableBase, l.RetentionValue383, l.RetentionValue384, l.ApplyRetention, 
				l.MaxDeductionsAndRentExents, l.PensionByIndividualSavingsRegime, l.PensionByIndividualSavingsRegimeReal, l.UVT, l.SMLV, l.PreviousDeductionsForWithholdings, AccumulatedIncome
				from @TableAccountPayableDetailConceptLiquidation l
				inner join @TableAccountPayableDetailConcepts d on d.TempAccountPayableDetailConceptId = l.TempAccountPayableDetailConceptId
				where l.Id = 0 AND l.TempAccountPayableDetailConceptLiquidationId = @TmpLiquidationId

				UPDATE @TableAccountPayableDetailConceptLiquidation
					SET Id = SCOPE_IDENTITY()
				WHERE Id = 0 AND TempAccountPayableDetailConceptLiquidationId = @TmpLiquidationId
			END
		end

		if EXISTS (select 1 from @TableAccountPayableDetailConceptLiquidationValuesModificated where Id = 0)
		begin
			INSERT INTO [Payments].[AccountPayableDetailConceptLiquidationValuesModificated](
				LiquidationId, ConceptType, PreviousValue, NewValue, Observations,
				CreationUser, CreationDate
			)
			select	l.Id, lvm.ConceptType, lvm.PreviousValue, lvm.NewValue, lvm.Observations,
					@UserCode, [Common].[GETDATE]()
			from @TableAccountPayableDetailConceptLiquidationValuesModificated lvm
			join @TableAccountPayableDetailConceptLiquidation l on lvm.TempAccountPayableDetailConceptLiquidationId = l.TempAccountPayableDetailConceptLiquidationId
			where lvm.Id = 0
		end

		--SELECT * FROM @TableAccountPayableDetailConceptLiquidationAdjusments

		if EXISTS (select 1 from @TableAccountPayableDetailConceptLiquidationAdjusments where Id = 0)
		begin
			INSERT INTO [Payments].[AccountPayableDetailConceptLiquidationAdjusments](
				LiquidationId, ConceptType, Nature, [Value], Observations, [Status], TotalIncome,
				CreationUser, CreationDate, ModificationUser, ModificationDate
			)
			select	l.Id, la.ConceptType, la.Nature, la.[Value], la.Observations, la.[Status], la.TotalIncome,
					@UserCode, [Common].[GETDATE](), NULL, NULL
			from @TableAccountPayableDetailConceptLiquidationAdjusments la
			join @TableAccountPayableDetailConceptLiquidation l on la.TempAccountPayableDetailConceptLiquidationId = l.TempAccountPayableDetailConceptLiquidationId
			where la.Id = 0
		end

		--Se actualizan las cuotas
		if EXISTS (select 1 from @TableAccountPayableCommitments where Id > 0 and IsDelete = 0)
		begin
			UPDATE apc 
				set apc.AccountPayableId = temp.AccountPayableId, 
					apc.CommitmentDetailId = temp.CommitmentDetailId, 
					apc.Value = temp.Value
			from @TableAccountPayableCommitments temp
			inner join Payments.AccountPayableCommitments apc on apc.Id = temp.Id
			where temp.Id > 0 and temp.IsDelete = 0
		end
		
		--Se guardan las cuotas nuevas
		if EXISTS (select 1 from @TableAccountPayableCommitments where Id = 0)
		begin
			INSERT INTO [Payments].[AccountPayableCommitments]([AccountPayableId], [CommitmentDetailId], [Value])
			select a.Id, s.CommitmentDetailId, s.Value
			from @TableAccountPayableCommitments s
			inner join @TableAccountPayable a on a.TempAccountPayableId = s.TempAccountPayableId
			where s.Id = 0
		end

		--Se crea el registro en la tabla de control si no existe y el estado sea registrado
		if NOT EXISTS (select 1 from Payments.PaymentsControl where DocumentNumber = @Code and DocumentType = 1) and @Status = 1
		begin
			INSERT INTO [Payments].[PaymentsControl]([DocumentNumber], [DocumentType], [DocumentUser], [DocumentDate])
			VALUES(@Code, 1, @UserCode, @DocumentDate)
		end
		
		--Se realiza esto para poder asignar el id a la cxp generada desde otros procesos
		declare @Id int = (select top 1 Id from @TableAccountPayable)

		--Se retorna el ok
		select 0 as CodeResult, 'Se guardó correctamente' as MessageResult, @Code as AccountPayableCode, CAST(@ConsecutiveFiling as varchar(20)) as ConsecutiveFiling, @Id as AccountPayableId
		return

	end try
	begin catch
		select 999 as CodeResult, CONCAT('Error: SP_SaveAccountsPayable - ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) as MessageResult, '' as AccountPayableCode, '' as ConsecutiveFiling, 0 as AccountPayableId
		return
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento principal para registrar, actualizar y eliminar cuentas por pagar (CxP) en el módulo de pagos. Procesa un documento XML con la información completa de la cuenta por pagar, incluyendo la cabecera del documento, conceptos de facturación, cuotas de pago, causaciones diferidas, compromisos presupuestales y liquidaciones de retención (calculadora de renta). Consulta la configuración general de la empresa y del módulo de pagos (SettingPayments, GeneralLedgerSettings, CompanySettings) para determinar los comprobantes contables, la unidad operativa, la moneda oficial y los parámetros de interfaz presupuestal antes de persistir cada componente. Gestiona además los compromisos presupuestales (AccountPayableCommitments) y la programación de causaciones diferidas por períodos (DeferredCausation, DeferredCausationShare), garantizando la trazabilidad contable y presupuestal de cada obligación de pago registrada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAccountsPayable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAccountsPayable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste de forma masiva (insert/update/delete) cuentas por pagar y todas sus entidades relacionadas (conceptos, cuotas, causaciones diferidas, liquidaciones de retención, compromisos presupuestales y tasas de cambio) a partir de un XML.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountsPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe respetar la estructura /AccountPayable con sus nodos hijos (AccountPayableDetailConcept, AccountPayableShares, DeferredCausation, AccountPayableCommitments, etc.).; Debe existir registro en GeneralLedger.CompanySettings para obtener moneda oficial y consecutivo de radicación cuando no hay facturas previas.; El mes/año de DocumentDate debe estar abierto en GeneralLedger.ClosedMonth (Status=1).; El mes/año de ServicePeriodDate debe estar abierto en GeneralLedger.ClosedMonth (Status=1).; Cada BillNumber debe contener al menos un dígito numérico.; El BillNumber no debe duplicarse para el mismo IdSupplier en otra CxP distinta.; La línea de distribución del proveedor (SuppliersDistributionLines) debe estar activa (Status=1).; Si CostDistributionDirectCostId está asignado y el estado es 1 ó 2, no debe estar usado por otra CxP con Status<>3.; Si SettingPayments.BudgetInterface=1 y ObligationBudgetInterface=1, cada factura debe tener un CommitmentDetail válido (excepto entidades ''CostDistributionDirectCost'' nuevas).; Si maneja interfaz presupuestal, la suma de compromisos debe igualar la suma de conceptos (ajustada por Nature y ObligationDebitValue).; Debe existir secuencia configurada en Payments.PaymentsSecuence/PaymentsSecuenceDetail para IdForm=730 cuando se requiere generar Code.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountsPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar (CxP); Factura de proveedor; Radicado / consecutivo de radicación; Causación diferida; Cuotas de pago; Liquidación de retención en la fuente; Compromiso presupuestal / obligación presupuestal; Documento soporte (facturador electrónico); Tasa de cambio / moneda oficial; Distribución de elementos del costo; Periodo contable abierto/cerrado; IVA deducible; Actividad económica; UVT y SMLV', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountsPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_InsertIntoExchangeRateWithDate; dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountsPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Payments.SettingPayments; GeneralLedger.ClosedMonth; Payments.AccountPayable; Common.Supplier; Common.SuppliersDistributionLines; Budget.CommitmentDetail; Payments.PaymentsSecuence; Payments.PaymentsSecuenceDetail; Common.Sequense; GeneralLedger.GeneralLedgerSettings; Common.ThirdParty; Payments.DeferredCausation; Payments.PaymentsControl', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountsPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountsPayable';
-- GO
