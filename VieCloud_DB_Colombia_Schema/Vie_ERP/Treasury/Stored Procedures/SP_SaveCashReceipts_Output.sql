-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 09-06-2016
-- Description:	Procedimiento para generar un recibo de caja
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveCashReceipts_Output] 
	@CashReceiptsXml AS XML,
	@User VARCHAR(20),
	@CompanyType int,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@StatusResult TINYINT OUTPUT,
	------------------------------------------------------
	@IdCashReceipt INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	set DATEFORMAT DMY; 

    --Se declaran las variables para obtener la cabecera
    DECLARE @CodeCashReceipt VARCHAR(20), 
			@IdThirdParty INT, 
			@CollectType TINYINT, 
			@IdMainAccount INT, 
			@IdCostCenter INT, 
			@Detail VARCHAR(2000), 
			@DocumentDate DATETIME, 
			@IdCashRegister INT, 
			@IdBankAccount INT, 
			@PaymentResponsibles VARCHAR(100), 
			@Value DECIMAL(18, 2), 
			@IdRefund INT, 
			@OperatingUnitId INT, 
			@Status TINYINT, 
			@Prefix VARCHAR(50)= '',
			@ChangeTracker VARCHAR(30), 
			@CurrencyId INT,
			-------------------------------------------------
			@Rows Int, 
			@RowId Int,
			-------------------------------------------------
			@Message VARCHAR(MAX),
			-------------------------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			-------------------------------------------------
			@EntityName [varchar](250),
			@EntityNameJournalVoucher [varchar](250)

	--tabla temporar para pronto pago
	declare @DiscountValue table(RowId Int Identity(1,1) Primary Key, accountReceivableId int,mainAccountDiscountId int, mainAccountAccountReceivableId int,[value] numeric(18,2))

    --Se declara la tabla temporal del detalle del recibo de caja
    DECLARE @CashReceiptDetails TABLE
    (
		RowId					        INT IDENTITY(1,1) PRIMARY KEY,
		ChangeTracker                   VARCHAR(30),
        [IdTmp]                         [INT] NOT NULL,
        [Id]                            [INT] NOT NULL,
        [IdCashReceipt]                 [INT] NOT NULL,
        [IdThirdParty]                  [INT] NOT NULL,
        [IdMainAccount]                 [INT] NOT NULL,
        [IdCostCenter]                  [INT] NULL,
        [Nature]                        [TINYINT] NOT NULL,
        [IdCashReceiptConcept]          [INT] NOT NULL,
        [CashReceiptConceptAffectation] [TINYINT] NOT NULL,
        [Value]                         [DECIMAL](18, 2) NOT NULL,
        [IdRetentionConcept]            [INT] NULL,
        [PercentageRetention]           [DECIMAL](5, 3) NULL,
		[BillingValue]				    [DECIMAL](18, 2) NULL,
        [BaseValue]                     [DECIMAL](18, 2) NULL,		  

        [CardNumber]                    [VARCHAR](30) NULL,
        [Detail]                        [VARCHAR](MAX) NULL,
		[IdCashFlowConcept]             [INT] NULL,
		[CurrencyId]                    [INT] NOT NULL,
		[TRM]                           [NUMERIC](20,5)NOT NULL,
		[ValueInCurrencyHeader]         [NUMERIC](20,5)NOT NULL,
		[ThirdPartyBeneficiaryId]             [INT] NULL
    );

    --se declara la tabla temporal para relacionar las cuentas por cobrar
    DECLARE @CashReceiptAccountReceivable AS TABLE
    (
		RowId							  INT IDENTITY(1,1) PRIMARY KEY,
		ChangeTracker            VARCHAR(30),
        [Id]                     [INT] NOT NULL,
        [CashReceiptDetailIdTmp] [INT] NOT NULL,
        [CashReceiptDetailId]    [INT] NOT NULL,
        [AccountReceivableId]    [INT] NOT NULL,
        [InvoiceNumber]          [VARCHAR](20) NOT NULL,
        [Value]                  [NUMERIC](18, 2) NOT NULL,
		[ValueInCurrencyHeader]  [NUMERIC](20,5)NOT NULL
    );

    --se declara la tabla temporal para los reintegros de anticipos
    DECLARE @CashReceiptAdvancePayment AS TABLE
    (
		ChangeTracker            VARCHAR(30),
        [Id]                     [INT] NOT NULL,
        [CashReceiptDetailIdTmp] [INT] NOT NULL,
        [CashReceiptDetailId]    [INT] NOT NULL,
        [AdvancePaymentId]       [INT] NOT NULL,
        [AdvancePaymentCode]     [VARCHAR](20) NOT NULL,
        [PaymentValue]           [NUMERIC](18, 2) NOT NULL,
		[ValueInCurrencyHeader]  [NUMERIC](20,5) NOT NULL
    );

    --Se declara la tabla temporal para guardar los reintegros de cuentas por pagar
    DECLARE @CashReceiptDetailAccountPayable AS TABLE
    (
		RowId					   INT IDENTITY(1,1) PRIMARY KEY,
		ChangeTracker            VARCHAR(30),
        [Id]                     [INT] NOT NULL,
        [CashReceiptDetailIdTmp] [INT] NOT NULL,
        [CashReceiptDetailId]    [INT] NOT NULL,
        [AccountPayableId]       [INT] NOT NULL,
        [RefundValue]            [NUMERIC](18, 2) NOT NULL
    );
	
    --Se declara la tambla temporal de metodos de pago
    DECLARE @PaymentMethods AS TABLE
    (
		RowId				   INT IDENTITY(1,1) PRIMARY KEY,
		ChangeTracker          VARCHAR(30),
        [Id]                   [INT] NOT NULL,
        [IdCashReceipt]        [INT] NOT NULL,
        [PaymentMethodTypes]   [TINYINT] NOT NULL,
        [Value]                [DECIMAL](18, 2) NOT NULL,
        [IdBank]               [INT] NULL,
        [CheckNumber]          [VARCHAR](30) NULL,
        [DepositDate]          [DATETIME] NULL,
        [IdEntityBankAccount]  [INT] NULL,
        [DepositNumber]        [VARCHAR](30) NULL,
        [DepositType]          [TINYINT] NULL,
        [IdCard]               [INT] NULL,
        [CardNumber]           [VARCHAR](30) NULL,
        [BaseValue]            [DECIMAL](18, 2) NULL,
        [CommissionValue]      [DECIMAL](18, 2) NULL,
        [PercentageCommission] [DECIMAL](5, 2) NULL,
        [RTFValue]             [DECIMAL](18, 2) NULL,
        [PercentageRTF]        [DECIMAL](5, 2) NULL,
        [ICAValue]             [DECIMAL](18, 2) NULL,
        [PercentageICA]        [DECIMAL](5, 2) NULL,
        [IdCostCenter]         [INT] NULL,
		[CurrencyId]           [INT] NOT NULL,
		[TRM]                  [NUMERIC](20,5)NOT NULL default (1),
		[ValueInCurrencyHeader][NUMERIC](20,5)NOT NULL,
		[IdAgreementsRedemptionPoints] [int] NULL,
		[TransactionNumber] [varchar](30) NULL,
		[TransactionDate] [datetime] NULL,
		[RedemptionPoints] [int] NULL,
		[CardType]			[Tinyint] NULL
    );
	
    --se declara la table de anticipos
    DECLARE @PortfolioAdvance AS TABLE
    (
		ChangeTracker            VARCHAR(30),
        [Id]                     [INT] NOT NULL,
        [Code]                   [VARCHAR](20) NULL,
        [CashReceiptId]          [INT] NULL,
        [CashReceiptDetailIdTmp] [INT] NULL,
        [CashReceiptDetailId]    [INT] NULL,
        [AdmissionNumber]        [CHAR](10) NULL,
        [ThirdPartyId]           [INT] NOT NULL,
        [MainAccountId]          [INT] NOT NULL,
        [CostCenterId]           [INT] NULL,
        [DocumentDate]           [DATETIME] NOT NULL,
        [CustomerId]             [INT] NULL,
        [SellerId]               [INT] NULL,
        [Value]                  [NUMERIC](18, 2) NOT NULL,
        [TransferValue]          [NUMERIC](18, 2) NOT NULL,
        [DebitValue]             [DECIMAL](18, 2) NOT NULL,
        [CreditValue]            [DECIMAL](18, 2) NOT NULL,
        [DistributionValue]      [DECIMAL](18, 2) NOT NULL,
        [Balance]                [DECIMAL](18, 2) NOT NULL,
        [Observations]           [VARCHAR](300) NULL,
        [OpeningBalance]         [BIT] NOT NULL,
        [Status]                 [TINYINT] NOT NULL,
		[CurrencyId]             [INT] NOT NULL,
		[TRMValue]               [NUMERIC](20,5)NOT NULL,
		[ValueInCurrencyHeader]  [NUMERIC](20,5)NOT NULL,
		[ThirdPartyBeneficiaryId][INT] NULL
    );
		 
	declare @JournalVouchers as table
	(
		[Id] [int] NOT NULL,
		[AccountingMovementId] [int] NOT NULL,
		[Consecutive] [bigint] NOT NULL,
		[LegalBookId] [int] NOT NULL ,
		[IdJournalVoucher] [int] NOT NULL,
		[VoucherDate] [datetime] NOT NULL,
		[Imported] [bit] NOT NULL,
		[Status] [tinyint] NOT NULL,
		[Detail] [varchar](MAX) NULL ,
		[EntityCode] [varchar](20) NULL,
		[EntityId] [int] NULL,
		[EntityName] [varchar](250) NULL,
		[IsClosedYear] [bit] NOT NULL,
		[CurrencyId] [int],
		[EntityNameOriginalConversion] [varchar](250) NULL,
		[CreationUser] [varchar](20) NOT NULL,
		[CreationDate] [datetime] NOT NULL,
		[ModificationUser] [varchar](20) NULL,
		[ModificationDate] [datetime] NULL,
		[ConfirmationUser] [varchar](20) NULL,
		[ConfirmationDate] [datetime] NULL
	)

	declare @JournalVoucherDetails as table
	(
		[Id] [int] NOT NULL,
		[IdAccounting] [int] NOT NULL,
		[IdMainAccount] [int] NOT NULL,
		[IdThirdParty] [int] NULL,
		[IdCostCenter] [int] NULL,
		[DebitValue] [decimal](18, 2) NOT NULL ,
		[CreditValue] [decimal](18, 2) NOT NULL ,
		[Detail] [varchar](max) NULL ,
		[IdRetention] [int] NULL,
		[RetentionRate] [decimal](5, 3) NULL,
		[BaseValue] [decimal](18, 0) NULL ,
		[BillingValue] [decimal](18, 0) NULL
	)

	DECLARE @TableResult as table([message] varchar(300))
	--tabla temporal para almacenar el resultado de guardar los pagos parciales
	declare @resultPartialPaymentC table (code varchar(20),MessageResult varchar(max))
	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
	--tabla temporal para almacenar el resultado de guardar el reconociminto
	declare @resultRecognition table(CodeMessage int,Message varchar(max),IdRecognition int,codeCollection varchar(20))
	declare @_cOfficialCurrencyId int
	SELECT top 1  @_cOfficialCurrencyId = OfficialCurrencyId from GeneralLedger.CompanySettings
    BEGIN TRY
        --se establece la cabecera del recibo de caja
        SELECT @ChangeTracker = t.x.value('ChangeTracker[1]', 'varchar(30)'),
            @IdCashReceipt = t.x.value('Id[1]', 'int'),
            @CodeCashReceipt = t.x.value('Code[1]', 'varchar(20)'),
            @IdThirdParty = t.x.value('IdThirdParty[1]', 'int'),
            @CollectType = t.x.value('CollectType[1]', 'tinyint'),
            @IdMainAccount = t.x.value('IdMainAccount[1]', 'int'),
            @IdCostCenter = t.x.value('IdCostCenter[1]', 'int'),
            @Detail = t.x.value('Detail[1]', 'varchar(2000)'),
            @DocumentDate = CONVERT( DATETIME, t.x.value('DocumentDate[1]', 'varchar(20)'), 103),
            @IdCashRegister = t.x.value('IdCashRegister[1]', 'int'),
            @IdBankAccount = t.x.value('IdBankAccount[1]', 'int'),
            @PaymentResponsibles = t.x.value('PaymentResponsibles[1]', 'varchar(100)'),
            @Value = t.x.value('Value[1]', 'decimal(18, 2)'),
			@CurrencyId = T.x.value('CurrencyId[1]', 'int'),
            @IdRefund = t.x.value('IdRefund[1]', 'int'),
            @OperatingUnitId = t.x.value('OperatingUnitId[1]', 'int'),
            @Status = t.x.value('Status[1]', 'tinyint'),
            @Prefix = isnull(t.x.value('Prefix[1]', 'varchar(50)'),''),
			@EntityName =t.x.value('EntityName[1]', 'varchar(250)')	
        FROM @CashReceiptsXml.nodes('/CashReceipts') t(x);

		if @EntityName is null or @EntityName ='' BEGIN
			set @EntityNameJournalVoucher = 'CashReceipts'
		
		end
		else begin
			set @EntityNameJournalVoucher = (	CASE
												WHEN @EntityName ='Invoice' THEN 'AutomaticCashReceipts'
												WHEN SUBSTRING(@EntityName,0,10) = 'Automatic' THEN @EntityName
												ELSE 'CashReceipts' END) 
		end
		 
        --se establece los detalles del recibo de caja
        INSERT INTO @CashReceiptDetails
            SELECT t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
                    t.x.value('IdTmp[1]', 'int') AS IdTmp,
                    t.x.value('Id[1]', 'int') AS Id,
                    t.x.value('IdCashReceipt[1]', 'int') AS IdCashReceipt,
                    t.x.value('IdThirdParty[1]', 'int') AS IdThirdParty,
                    t.x.value('IdMainAccount[1]', 'int') AS IdMainAccount,
                    t.x.value('IdCostCenter[1]', 'int') AS IdCostCenter,
                    t.x.value('Nature[1]', 'tinyint') AS Nature,
                    t.x.value('IdCashReceiptConcept[1]', 'int') AS IdCashReceiptConcept,
                    t.x.value('CashReceiptConceptAffectation[1]', 'tinyint') AS CashReceiptConceptAffectation,
                    t.x.value('Value[1]', 'decimal(18, 2)') AS Value,
                    t.x.value('IdRetentionConcept[1]', 'int') AS IdRetentionConcept,
                    t.x.value('PercentageRetention[1]', 'decimal(5, 3)') AS PercentageRetention,
					t.x.value('BillingValue[1]', 'decimal(18, 2)') AS BillingValue,
                    t.x.value('BaseValue[1]', 'decimal(18, 2)') AS BaseValue,
                    t.x.value('CardNumber[1]', 'varchar(30)') AS CardNumber,
                    t.x.value('Detail[1]', 'varchar(max)') AS Detail,
					t.x.value('IdCashFlowConcept[1]', 'int') AS IdCashFlowConcept,
					iif(t.x.value('CurrencyId[1]', 'int') is null or t.x.value('CurrencyId[1]', 'int')='',@_cOfficialCurrencyId,t.x.value('CurrencyId[1]', 'int')  ) AS CurrencyId,
					ISNULL(t.x.value('TRM[1]', 'decimal(20,5)'),1) AS TRM,
					iif(t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)')is null or t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)')=0, t.x.value('Value[1]', 'decimal(18, 2)'),   t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)')) AS ValueInCurrencyHeader,
					t.x.value('ThirdPartyBeneficiaryId[1]', 'int') AS ThirdPartyBeneficiaryId
					--isnull(t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)'),t.x.value('Value[1]', 'decimal(18, 2)')) AS ValueInCurrencyHeader
            FROM @CashReceiptsXml.nodes('/CashReceipts/CashReceiptDetails') t(x);

		--Actualizo el concepto de flujo de caja
		UPDATE crd
			SET crd.IdCashFlowConcept = crc.IdCashFlowConcept
		FROM @CashReceiptDetails crd
		JOIN Treasury.CashReceiptConcepts crc ON crd.IdCashReceiptConcept = crc.Id
		WHERE ISNULL(crd.IdCashFlowConcept, 0) = 0

        --Se insertan los metodos de pagos
        INSERT INTO @PaymentMethods
            SELECT t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
                    t.x.value('Id[1]', 'int') AS Id,
                    t.x.value('IdCashReceipt[1]', 'int') AS IdCashReceipt,
                    t.x.value('PaymentMethodTypes[1]', 'tinyint') AS PaymentMethodTypes,
                    t.x.value('Value[1]', 'decimal(18, 2)') AS Value,
                    t.x.value('IdBank[1]', 'int') AS IdBank,
                    t.x.value('CheckNumber[1]', 'varchar(30)') AS CheckNumber,
                    CONVERT( DATETIME, t.x.value('DepositDate[1]', 'varchar(20)'), 103),
                    t.x.value('IdEntityBankAccount[1]', 'int') AS IdEntityBankAccount,
                    t.x.value('DepositNumber[1]', 'varchar(30)') AS DepositNumber,
                    t.x.value('DepositType[1]', 'tinyint') AS DepositType,
                    t.x.value('IdCard[1]', 'int') AS IdCard,
                    t.x.value('CardNumber[1]', 'varchar(30)') AS CardNumber,
                    t.x.value('BaseValue[1]', 'decimal(18, 2)') AS BaseValue,
                    t.x.value('CommissionValue[1]', 'decimal(18, 2)') AS CommissionValue,
                    t.x.value('PercentageCommission[1]', 'decimal(5, 2)') AS PercentageCommission,
                    t.x.value('RTFValue[1]', 'decimal(18, 2)') AS RTFValue,
                    t.x.value('PercentageRTF[1]', 'decimal(5, 2)') AS PercentageRTF,
                    t.x.value('ICAValue[1]', 'decimal(18, 2)') AS ICAValue,
                    t.x.value('PercentageICA[1]', 'decimal(5, 2)') AS PercentageICA,
                    t.x.value('IdCostCenter[1]', 'int') AS IdCostCenter,
					iif(t.x.value('CurrencyId[1]', 'int') is null or t.x.value('CurrencyId[1]', 'int')='',@_cOfficialCurrencyId,t.x.value('CurrencyId[1]', 'int')  ) AS CurrencyId,
					isnull(t.x.value('TRM[1]', 'decimal(20,5)'),1) AS TRM,
					iif(t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)') is null or t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)') =0 ,t.x.value('Value[1]', 'decimal(18, 2)'),t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)')) AS ValueInCurrencyHeader,
					t.x.value('IdAgreementsRedemptionPoints[1]', 'int') AS IdAgreementsRedemptionPoints,
					t.x.value('TransactionNumber[1]', 'varchar(30)') AS TransactionNumber,
					t.x.value('TransactionDate[1]', 'datetime') AS TransactionDate,
					t.x.value('RedemptionPoints[1]', 'int') AS RedemptionPoints,
					t.x.value('CardType[1]', 'tinyint') AS CardType
            FROM @CashReceiptsXml.nodes('/CashReceipts/PaymentMethods') t(x);

        --Cuentas por pagar
        INSERT INTO @CashReceiptAccountReceivable
            SELECT t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
                    t.x.value('Id[1]', 'int') AS Id,
                    t.x.value('CashReceiptDetailIdTmp[1]', 'int') AS CashReceiptDetailIdTmp,
                    t.x.value('CashReceiptDetailId[1]', 'int') AS CashReceiptDetailId,
                    t.x.value('AccountReceivableId[1]', 'int') AS AccountReceivableId,
                    t.x.value('InvoiceNumber[1]', 'varchar(20)') AS InvoiceNumber,
                    t.x.value('Value[1]', 'decimal(18, 2)') AS Value,
					isnull(t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)'), t.x.value('Value[1]', 'decimal(18, 2)')) AS ValueInCurrencyHeader
            FROM @CashReceiptsXml.nodes('/CashReceipts/CashReceiptDetails/CashReceiptAccountReceivable') t(x);

        --Reintegro de anticipos
        INSERT INTO @CashReceiptAdvancePayment
            SELECT t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
                    t.x.value('Id[1]', 'int') AS Id,
                    t.x.value('CashReceiptDetailIdTmp[1]', 'int') AS CashReceiptDetailIdTmp,
                    t.x.value('CashReceiptDetailId[1]', 'int') AS CashReceiptDetailId,
                    t.x.value('AdvancePaymentId[1]', 'int') AS AdvancePaymentId,
                    t.x.value('AdvancePaymentCode[1]', 'varchar(20)') AS AdvancePaymentCode,
                    t.x.value('PaymentValue[1]', 'decimal(18, 2)') AS PaymentValue,
					t.x.value('ValueInCurrencyHeader[1]', 'numeric(20, 5)') AS ValueInCurrencyHeader
            FROM @CashReceiptsXml.nodes('/CashReceipts/CashReceiptDetails/CashReceiptAdvancePayment') t(x);

        --Reintegro de Cuentas por Pagar
        INSERT INTO @CashReceiptDetailAccountPayable
            SELECT t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
                    t.x.value('Id[1]', 'int') AS Id,
                    t.x.value('CashReceiptDetailIdTmp[1]', 'int') AS CashReceiptDetailIdTmp,
                    t.x.value('CashReceiptDetailId[1]', 'int') AS CashReceiptDetailId,
                    t.x.value('AccountPayableId[1]', 'int') AS AccountPayableId,
                    t.x.value('RefundValue[1]', 'decimal(18, 2)') AS RefundValue
            FROM @CashReceiptsXml.nodes('/CashReceipts/CashReceiptDetails/CashReceiptDetailAccountPayable') t(x);
	
        --Anticipos
        INSERT INTO @PortfolioAdvance
            SELECT t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
                    t.x.value('Id[1]', 'int') AS Id,
                    t.x.value('Code[1]', 'varchar(20)') AS Code,
                    t.x.value('CashReceiptId[1]', 'int') AS CashReceiptId,
                    t.x.value('CashReceiptDetailIdTmp[1]', 'int') AS CashReceiptDetailIdTmp,
                    t.x.value('CashReceiptDetailId[1]', 'int') AS CashReceiptDetailId,
                    t.x.value('AdmissionNumber[1]', 'char(10)') AS AdmissionNumber,
                    t.x.value('ThirdPartyId[1]', 'int') AS ThirdPartyId,
                    t.x.value('MainAccountId[1]', 'int') AS MainAccountId,
                    t.x.value('CostCenterId[1]', 'int') AS CostCenterId,
                    CONVERT( DATETIME, t.x.value('DocumentDate[1]', 'varchar(20)'), 103),
                    t.x.value('CustomerId[1]', 'int') AS CustomerId,
                    t.x.value('SellerId[1]', 'int') AS SellerId,
                    t.x.value('Value[1]', 'decimal(18, 2)') AS Value,
                    t.x.value('TransferValue[1]', 'decimal(18, 2)') AS TransferValue,
                    t.x.value('DebitValue[1]', 'decimal(18, 2)') AS DebitValue,
                    t.x.value('CreditValue[1]', 'decimal(18, 2)') AS CreditValue,
                    t.x.value('DistributionValue[1]', 'decimal(18, 2)') AS DistributionValue,
                    t.x.value('Balance[1]', 'decimal(18, 2)') AS Balance,
                    t.x.value('Observations[1]', 'varchar(300)') AS Observations,
                    t.x.value('OpeningBalance[1]', 'bit') AS OpeningBalance,
                    t.x.value('Status[1]', 'tinyint'),
					t.x.value('CurrencyId[1]', 'int') AS CurrencyId,
					t.x.value('TRMValue[1]', 'decimal(20,5)') AS TRMValue,
					t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)') AS ValueInCurrencyHeader,
					t.x.value('ThirdPartyBeneficiaryId[1]', 'int') AS ThirdPartyBeneficiaryId
            FROM @CashReceiptsXml.nodes('/CashReceipts/PortfolioAdvance') t(x);

		DECLARE @ConfirmationUser AS VARCHAR(20)= CASE WHEN @Status <> 2 THEN NULL ELSE @User END;
        DECLARE @ConfirmationDate AS DATETIME= CASE WHEN @Status <> 2 THEN NULL ELSE [Common].[GETDATE]() END;
        DECLARE @AnnulmentUser AS VARCHAR(20)= CASE WHEN @Status <> 3 THEN NULL ELSE @User END;
        DECLARE @AnnulmentDate AS DATETIME= CASE WHEN @Status <> 3 THEN NULL ELSE [Common].[GETDATE]() END;

		Declare @Uno Tinyint = 1,
			@Cero Tinyint = 0,
			@Dos Tinyint = 2,
			@Tres Tinyint = 3,
			@IdForm635 VARCHAR(5) = '635',
			@Deleted Varchar(7) = 'Deleted',
			@Modified Varchar(8) = 'Modified',
			@Added Varchar(5) = 'Added'
			
        --************VALIDACIONES DEL RECIBO DE CAJA********************
				-- Valido si el prefijo viene
		DECLARE @CurrentPrefix  VARCHAR(50)

		SET @CurrentPrefix = (	SELECT TOP 1 cr.Prefix
										FROM Treasury.CashRegisters cr 
										WHERE cr.Id = @IdCashRegister

										UNION ALL

										SELECT TOP 1 eba.Prefix
										FROM Treasury.EntityBankAccounts eba 
										WHERE eba.Id = @IdBankAccount
										)

		IF @Status in (1,2) BEGIN
		--valido que la cuenta contable no sea vacia 
			IF @IdMainAccount is null or @IdMainAccount =0 BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = CONCAT('La cuenta contable de ', IIF(@IdBankAccount IS NULL, 'la caja ', 'el banco '),' esta llegando vacía'),
						@IdCashRegister = 0,
						@StatusResult = 3
				RETURN				
			END	
			--Valido que el prefijo sea el mismo que esta parametrizado 
			IF ISNULL(@Prefix,'') <> '' AND @Prefix <> @CurrentPrefix BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult =CONCAT('El prefijo ',@Prefix ,' No corresponde al parámetrizado ',IIF(@IdBankAccount IS NULL, 'en la caja: ', 'en el banco: '),@CurrentPrefix),
						@IdCashRegister = 0,
						@StatusResult = 3
				RETURN
			END
		END

		IF EXISTS ( 
			SELECT 1 
			FROM Treasury.CashReceipts cr
			WHERE cr.Id = @IdCashReceipt AND cr.Status <> 1
		)
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El recibo de caja ' + cr.Code + ' se encuentra en estado ' + 
						CASE cr.Status 
							WHEN 2 THEN 'Confirmado'
							WHEN 3 THEN 'Anulado'
							WHEN 4 THEN 'Reversado'
							ELSE 'N/A'
						END,
					@IdCashReceipt = 0,
					@StatusResult = 3
			FROM Treasury.CashReceipts cr
			WHERE cr.Id = @IdCashReceipt AND cr.Status <> 1
			RETURN
		END

		--*******************************
		-- Actualizar el centro de costo del anticipo solo si la cuenta contable no maneja centro de costo
		UPDATE pa
		SET pa.CostCenterId = NULL
		FROM @PortfolioAdvance pa
		JOIN GeneralLedger.MainAccounts ma ON ma.Id = pa.MainAccountId
		WHERE ma.HandlesCostCenter = 0;

		if (@status <3) 
		begin
			--valido el que el mes este abierto en contabilidad
			if Not Exists (select 1 from GeneralLedger.ClosedMonth  where [Year] = YEAR(@DocumentDate) and [Month]  = MONTH(@DocumentDate) and [Status] = @Uno) begin
				SELECT	@CodeResult = 999,
						@MessageResult = 'El mes seleccionado en la fecha del recibo de caja esta cerrado',
						@IdCashRegister = 0,
						@StatusResult = 3
				RETURN
			end

			--Tabla para ir almacenando los errores 
			DECLARE @TableErrors as table([message] varchar(300))

			declare @debit decimal(18,2) =0
			declare @credit decimal(18,2) =0
			--valido que existan detalles en el recibo de caja
			if Not Exists (select 1 from @CashReceiptDetails) begin
				insert into @TableErrors select 'Se debe agregar mínimo un detalle al recibo de caja'+ CHAR(13) + CHAR(10)
			end
			--valido que en los detalles no exista ninguno con valores en negativo
			if Exists (select 1 from @CashReceiptDetails where Value<0 or ValueInCurrencyHeader<0) begin
				insert into @TableErrors select 'No pueden existir detalles con valores negativos'+ CHAR(13) + CHAR(10)
			end
			--valido que existan metodos de pago
			if Not Exists (select 1 from @PaymentMethods) begin
				insert into @TableErrors select 'Se debe agregar mínimo un método de pago'+ CHAR(13) + CHAR(10)
			end

			----validamos que los que son metodo de pago redencion de puntos no tenga el mismo convenio
			if (select count(*) from @PaymentMethods where PaymentMethodTypes = 5 GROUP by IdAgreementsRedemptionPoints HAVING count(*) > 1) > 1
			BEGIN
				select @CodeResult = 999,
				@MessageResult = 'Hay convenios de redención de puntos repetidos en los métodos de pago.'
				return
			END
		
			--Obtengo los debitos		  
			select @debit += ISNULL(SUM(ValueInCurrencyHeader),0) from @CashReceiptDetails where Nature = @Uno and ChangeTracker <> @Deleted		  
			select @debit += ISNULL(SUM(ValueInCurrencyHeader),0) from @PaymentMethods where ChangeTracker <> @Deleted
			--Obtengo los creditos
			select @credit  += ISNULL(SUM(ValueInCurrencyHeader),0) from @CashReceiptDetails where Nature = @Dos and ChangeTracker <> @Deleted

			IF ROUND(@credit,-1) =  ROUND(@debit,-1) and @credit <> @debit BEGIN
				declare @DiffValue as decimal(18,2) = @debit -@credit
				set @credit += @DiffValue
			END

			if(@debit = 0 or @credit =0) begin
				insert into @TableErrors select 'Los debitos o creditos no pueden ser 0'+ CHAR(13) + CHAR(10)
			end
			if(@debit > 0 and @credit >0 and @debit <> @credit ) begin			 
				insert into @TableErrors select 'Existe diferencia entre el débito y el crédito'+ CHAR(13) + CHAR(10)
			end
		  		  
			--si se va a confirmar el recibo de caja 
			if(@Status =2) 
			begin
				--validamos que que las cuentas por cobrar sean del mismo tipo 
				if 
				(
					select count(*) 
					from 
					(
						select distinct ar.AccountReceivableType 
						from @CashReceiptAccountReceivable car 
						inner join Portfolio.AccountReceivable ar  on car.AccountReceivableId = ar.id 
						where car.ChangeTracker<>@Deleted
					) as AccountReceivableType 
				) > 1
				begin
					DECLARE @valores VARCHAR(1000)
					select @valores= COALESCE(@valores + ', ', '')  +  
					case when  AccountReceivableType = 1 then 'Facturación Básica' 
					when  AccountReceivableType = 2 then 'Facturación Ley 100'
					when  AccountReceivableType = 3 then 'Impuestos Industria y Comercio'
					when  AccountReceivableType = 4 then 'Pagarés'
					when  AccountReceivableType = 5 then 'Acuerdos de Pago'
					when  AccountReceivableType = 6 then 'Documento de Pago a Cuota Moderadora'
					when  AccountReceivableType = 7 then 'Factura de Producto'
					when  AccountReceivableType = 8 then 'Impuesto Predial'
					end  from (select distinct ar.AccountReceivableType from @CashReceiptAccountReceivable car 
					inner join Portfolio.AccountReceivable ar  on car.AccountReceivableId = ar.id where car.ChangeTracker<>@Deleted) as AccountReceivableType 
							
					insert into @TableErrors select 'Existen cuentas por cobrar con diferente tipo ('+ @valores + ')' + CHAR(13) + CHAR(10)
				end
			
				if (@CollectType =1) 
				begin --caja
					if Not Exists 
					(
						select 1 
						from Treasury.CashRegisterUser cr 
						inner join [Security].[User] u on cr.IdUser = u.Id 
						where cr.IdCashRegister =@IdCashRegister  and u.UserCode =@User
					)
					begin
						insert into @TableErrors select 'El usuario no tiene permiso para la caja seleccionada'+ CHAR(13) + CHAR(10)
					end
				end
				else
				begin --banco
					if Not Exists (select 1 from Treasury.EntityBankAccountUser  where IdEntityBankAccount = @IdBankAccount  and CodUser = @User) 
					begin
						insert into @TableErrors select 'El usuario no tiene permiso para la cuenta bancaria seleccionada'+ CHAR(13) + CHAR(10)
					end
				end

				--valido los saldos de las facturas
				if Exists
				(
					select 1 from @CashReceiptDetails crd
					inner join @CashReceiptAccountReceivable crar on crd.IdTmp = crar.CashReceiptDetailIdTmp
					inner join Portfolio.AccountReceivable ar  on crar.AccountReceivableId = ar.Id
					inner join Portfolio.AccountReceivableAccounting ara  on ar.Id = ara.AccountReceivableId and ara.MainAccountId = crd.IdMainAccount
					left join Common.Currency c on ar.CurrencyId = c.Id
					where crd.CashReceiptConceptAffectation = @Dos and (crar.[ValueInCurrencyHeader] - ara.Balance) > ISNULL(Common.GetRoundTolerance(c.RoundingType), 0.01) and crd.ChangeTracker<> @Deleted and crar.ChangeTracker<> @Deleted
				)
				begin
					insert into @TableErrors
						select 'El saldo ('+FORMAT(ara.balance, 'C0', 'es-co')+') de la factura ' +
							ar.InvoiceNumber  + ', en la cuenta contable '+ma.number+' - '+ma.[name]+' es menor que el valor ('+ FORMAT(crar.[Value], 'C0', 'es-co')+') del ajuste'+ CHAR(13) + CHAR(10)
						from @CashReceiptDetails crd
						inner join @CashReceiptAccountReceivable crar on crd.IdTmp = crar.CashReceiptDetailIdTmp
						inner join Portfolio.AccountReceivable ar  on crar.AccountReceivableId = ar.Id
						inner join Portfolio.AccountReceivableAccounting ara  on ar.Id = ara.AccountReceivableId and ara.MainAccountId = crd.IdMainAccount
						inner join generalLedger.MainAccounts ma  on ara.MainAccountId = ma.Id
						left join Common.Currency c on ar.CurrencyId = c.Id
						where crd.CashReceiptConceptAffectation =@Dos and (crar.[ValueInCurrencyHeader] - ara.Balance) > ISNULL(Common.GetRoundTolerance(c.RoundingType), 0.01) and crd.ChangeTracker<> @Deleted and crar.ChangeTracker<> @Deleted
				end

				--valido el saldo de los anticipos
				if Exists 
				(
					select 1 from @CashReceiptDetails crd 
					inner join @CashReceiptAdvancePayment  crap on crd.IdTmp = crap.CashReceiptDetailIdTmp  
					inner join Payments.AdvancePayments  ap  on crap.AdvancePaymentId  = ap.Id				
					where crd.CashReceiptConceptAffectation = @Tres and ap.Balance < crap.PaymentValue  and crd.ChangeTracker<> @Deleted and crap.ChangeTracker<> @Deleted
				)
				begin
					insert into @TableErrors 
						select 'El saldo ('+FORMAT(ap.balance, 'C0', 'es-co')+') del anticipo ' + ap.Code  + ', es menor al valor ('+ FORMAT(crap.PaymentValue, 'C0', 'es-co')+') a pagar'+ CHAR(13) + CHAR(10) from @CashReceiptDetails crd 
						inner join @CashReceiptAdvancePayment  crap on crd.IdTmp = crap.CashReceiptDetailIdTmp  
						inner join Payments.AdvancePayments  ap  on crap.AdvancePaymentId  = ap.Id				
						where crd.CashReceiptConceptAffectation =@Tres and ap.Balance < crap.PaymentValue  and crd.ChangeTracker<> @Deleted and crap.ChangeTracker<> @Deleted
				end

				--Valido el anticipo en caso de que la cuenta contable maneje centro de costo y éste venga vacio
				IF EXISTS
				(
					SELECT 1 FROM @PortfolioAdvance pa
					JOIN GeneralLedger.MainAccounts ma ON ma.Id = pa.MainAccountId
					where ma.HandlesCostCenter = 1 AND pa.CostCenterId IS NULL
				)
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'La cuenta contable ' + CONCAT(ma.Number, ' - ', ma.Name) + ' del anticipo maneja centro de costo y éste se encuentra vacio' 
						FROM @PortfolioAdvance pa
						JOIN GeneralLedger.MainAccounts ma ON ma.Id = pa.MainAccountId			
						WHERE ma.HandlesCostCenter = 1 AND pa.CostCenterId IS NULL
				END
			 
				--si existen errores y se esta confirmando se termina el proceso
				if (select COUNT(*) from @TableErrors )>0 
				begin 	
					SELECT @Message = COALESCE(@Message + '', '') + [message]  from @TableErrors

					SELECT	@CodeResult = 999,
							@MessageResult = @Message,
							@IdCashRegister = 0,
							@StatusResult = 3
					RETURN
				end
			end
		end
        --*********************************************************************
        --si el recibo de caja es nuevo
		IF(@IdCashReceipt = 0) 
		BEGIN
			if @CodeCashReceipt = ''
			begin
				--- Obtenemos la secuencia numerica
				IF Not Exists
				(
					SELECT 1
					FROM Treasury.TreasurySequence 
					WHERE IdForm = @IdForm635
				)
				BEGIN
					SELECT	@CodeResult = 999,
							@MessageResult = 'No existe secuencia numerica para el formulario de recibos de caja',
							@IdCashRegister = 0,
							@StatusResult = 3
					RETURN
				END;

				DECLARE @idSequenceDetail INT;
				DECLARE @pattern VARCHAR(300);
				DECLARE @NextS BIGINT;
				DECLARE @Scope VARCHAR(5);
				DECLARE @IdSequence INT;
				DECLARE @IdSequenceCommon INT;

				SELECT	@IdSequence = Id,
						@Scope = Scope,
						@IdSequenceCommon = IdSequence
				FROM Treasury.TreasurySequence 
				WHERE IdForm = @IdForm635;
				 
				IF @Scope = 'O'				 
				BEGIN --- Secuencia por Prefijo

					--Valido caundo el prefijo no llega ó llega vacio
					IF ISNULL (@Prefix,'') = '' BEGIN						
						SET @Prefix = @CurrentPrefix
					END

					SELECT	@pattern = cs.Pattern,
							@idSequenceDetail = psd.Id,
							@NextS = psd.[Next]
					FROM Treasury.TreasurySequenceDetail psd 
					INNER JOIN Common.Sequense cs  ON cs.Id = psd.IdSequense
					WHERE psd.Prefix = @Prefix and psd.IdSequenseTreasuryC = @IdSequence;
					
					IF Not Exists 
					(
						SELECT 1 
						FROM Treasury.TreasurySequenceDetail psd 
						INNER JOIN Common.Sequense cs  ON cs.Id = psd.IdSequense
						WHERE psd.Prefix = @Prefix and psd.IdSequenseTreasuryC = @IdSequence
					)
					BEGIN
						--Si es nuevo el Prefix de la caja se inserta en la tabla TreasurySequenceDetail 
						--se almacena el Pattern, para enviar a la función GetSequence
						SELECT @pattern = Pattern from Common.Sequense  where Id = @IdSequenceCommon                                    
						--se agrega un nuevo registro al detalle con el prefijo que no existe
						INSERT INTO Treasury.TreasurySequenceDetail (IdSequenseTreasuryC, IdSequense, IdOperatingUnit, Next, Prefix)
						VALUES (@IdSequence,@IdSequenceCommon,@OperatingUnitId,1,@Prefix);

						SET @idSequenceDetail = SCOPE_IDENTITY();
						SET @NextS = 1;
					END;
				END;
				ELSE
				BEGIN -- Secuencia por Unidad operativa
					SELECT @pattern = cs.Pattern,
						@idSequenceDetail = psd.Id,
						@NextS = psd.[Next]
					FROM Treasury.TreasurySequenceDetail psd 
						INNER JOIN Common.Sequense cs  ON cs.Id = psd.IdSequense
					WHERE psd.IdSequenseTreasuryC  = @IdSequence
						AND IdOperatingUnit = @OperatingUnitId;
				END;

				SELECT @CodeCashReceipt = dbo.GetSequence(@Prefix, @pattern, @NextS);
				--valido que la secuancia tenga valor disponible
				IF @CodeCashReceipt = '__ERROR_MAXVALUE__'
				BEGIN
					SELECT	@CodeResult = 999,
							@MessageResult = 'La secuencia alcanzo su valor maximo',
							@IdCashRegister = 0,
							@StatusResult = 3
					RETURN
				END;

				--actualizo la secuencia
				UPDATE Treasury.TreasurySequenceDetail SET [Next]+=1 WHERE Id = @idSequenceDetail;
			end                     
		
            --inserto en la tabla de control de tesereria
            INSERT INTO [Treasury].[TreasuryControl] ([DocumentNumber],[DocumentType],[DocumentUser],[DocumentDate])
            VALUES (@CodeCashReceipt,1,@User,@DocumentDate);

            --inserto la cabecera del recibo de caja		
            IF YEAR(@DocumentDate) < '1900'
            BEGIN
                SET @DocumentDate = [Common].[GETDATE]()
            END

            INSERT INTO [Treasury].[CashReceipts]
            (
				[Code],[IdThirdParty],[CollectType],[IdMainAccount],[IdCostCenter],[Detail],[DocumentDate],[IdCashRegister],[IdBankAccount],[PaymentResponsibles],[Value],[IdRefund],[OperatingUnitId],
				[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],[AnnulmentUser],[AnnulmentDate],[ReversedUser],[ReversedDate],[EntityName]
            )
            VALUES
            (
				@CodeCashReceipt,@IdThirdParty,@CollectType,@IdMainAccount,@IdCostCenter,@Detail,@DocumentDate,@IdCashRegister,@IdBankAccount,@PaymentResponsibles,@Value,@IdRefund,@OperatingUnitId,
				@Status,@User,[Common].[GETDATE](),NULL,NULL,@ConfirmationUser,@ConfirmationDate,NULL,NULL,NULL,NULL,@EntityName
            );

            SET @IdCashReceipt = SCOPE_IDENTITY();		
			update @CashReceiptDetails set IdCashReceipt = @IdCashReceipt		 
        END;
        ELSE
        BEGIN
            --si se va actualizar		
            --actulizo valores de la cabecera
            UPDATE [Treasury].[CashReceipts]
            SET
                [Code] = @CodeCashReceipt,
                [IdThirdParty] = @IdThirdParty,
                [CollectType] = @CollectType,
                [IdMainAccount] = @IdMainAccount,
                [IdCostCenter] = @IdCostCenter,
                [Detail] = @Detail,
                [DocumentDate] = @DocumentDate,
                [IdCashRegister] = @IdCashRegister,
                [IdBankAccount] = @IdBankAccount,
                [PaymentResponsibles] = @PaymentResponsibles,
                [Value] = @Value,
                [IdRefund] = @IdRefund,
                [OperatingUnitId] = @OperatingUnitId,
                [Status] = @Status,
                [ModificationUser] = @User,
                [ModificationDate] = [Common].[GETDATE](),
                [ConfirmationUser] = @ConfirmationUser,
                [ConfirmationDate] = @ConfirmationDate,
                [AnnulmentUser] = @AnnulmentUser,
                [AnnulmentDate] = @AnnulmentDate,
				[EntityName] = @EntityName
            WHERE Id = @IdCashReceipt; 

            --Si se esta anulando 
            IF(@Status = 3) 
			BEGIN
                --elimino el registro de la tabla de control
                DELETE Treasury.TreasuryControl
                WHERE DocumentNumber = @CodeCashReceipt AND DocumentType = @Uno;

                --anulo los anticipos por cada detalle del recibo de caja
                UPDATE Portfolio.PortfolioAdvance
                SET
                    [Status] = 3,
                    AnnulmentDate = [Common].[GETDATE](),
                    AnnulmentUser = @User
                FROM @CashReceiptDetails crdTmp
                    INNER JOIN Portfolio.PortfolioAdvance pa ON pa.CashReceiptDetailId = crdTmp.Id
                WHERE pa.CashReceiptId = @IdCashReceipt;
            END;                     
        END;
		--si se actualiza o se confirma
		IF(@Status < 3)
		BEGIN		
			--eliminos todos los detalles de los detalles del recibo de caja
			DELETE Treasury.CashReceiptAccountReceivable
			FROM @CashReceiptAccountReceivable crarTmp
				INNER JOIN Treasury.CashReceiptAccountReceivable crar ON crarTmp.Id = crar.Id
			WHERE crarTmp.ChangeTracker = @Deleted;
					    
			DELETE Treasury.CashReceiptAdvancePayment
			FROM @CashReceiptAdvancePayment crapTmp
				INNER JOIN Treasury.CashReceiptAdvancePayment crap ON crapTmp.Id = crap.Id
			WHERE crapTmp.ChangeTracker = @Deleted;
			DELETE Treasury.CashReceiptDetailAccountPayable
			FROM @CashReceiptDetailAccountPayable crdapTmp
				INNER JOIN Treasury.CashReceiptDetailAccountPayable crdap ON crdapTmp.Id = crdap.Id
			WHERE crdapTmp.ChangeTracker = @Deleted;
			DELETE Portfolio.PortfolioAdvance
			FROM @CashReceiptDetails crdTmp
				INNER JOIN Portfolio.PortfolioAdvance pa ON crdTmp.Id = pa.CashReceiptDetailId
			WHERE crdTmp.ChangeTracker = @Deleted;

			--elimino los detalles del recibo de caja
			DELETE Treasury.CashReceiptDetails
			FROM @CashReceiptDetails crdTmp
				INNER JOIN Treasury.CashReceiptDetails crd ON crdTmp.Id = crd.Id
			WHERE crdTmp.ChangeTracker = @Deleted;

			--Actulizo todos los detalles del detalle
			UPDATE [Treasury].[CashReceiptAccountReceivable]
			SET
				CashReceiptDetailId = crarTmp.CashReceiptDetailId,
				AccountReceivableId = crarTmp.AccountReceivableId,
				InvoiceNumber = crarTmp.InvoiceNumber,
				Value = crarTmp.Value,
				ValueInCurrencyHeader = crarTmp.ValueInCurrencyHeader
			FROM @CashReceiptAccountReceivable crarTmp
				INNER JOIN Treasury.CashReceiptAccountReceivable crar ON crarTmp.Id = crar.id
			WHERE crarTmp.ChangeTracker = @Modified;

			UPDATE [Treasury].[CashReceiptAdvancePayment]
			SET
				[CashReceiptDetailId] = crapTmp.CashReceiptDetailId,
				[AdvancePaymentId] = crapTmp.AdvancePaymentId,
				[AdvancePaymentCode] = crapTmp.AdvancePaymentCode,
				[PaymentValue] = crapTmp.PaymentValue,
				[ValueInCurrencyHeader] = crapTmp.ValueInCurrencyHeader
			FROM @CashReceiptAdvancePayment crapTmp
				INNER JOIN Treasury.CashReceiptAdvancePayment crap ON crapTmp.Id = crap.Id
			WHERE crapTmp.ChangeTracker = @Modified;

			UPDATE [Treasury].[CashReceiptDetailAccountPayable]
			SET
				[CashReceiptDetailId] = crdapTmp.CashReceiptDetailId,
				[AccountPayableId] = crdapTmp.AccountPayableId,
				[RefundValue] = crdapTmp.RefundValue
			FROM @CashReceiptDetailAccountPayable crdapTmp
				INNER JOIN Treasury.CashReceiptDetailAccountPayable crdap ON crdapTmp.Id = crdap.Id
			WHERE crdapTmp.ChangeTracker = @Modified;

			UPDATE [Portfolio].[PortfolioAdvance]
			SET
				[Code] = paTmp.Code,
				[CashReceiptId] = paTmp.CashReceiptId,
				[CashReceiptDetailId] = paTmp.CashReceiptDetailId,
				[AdmissionNumber] = paTmp.AdmissionNumber,
				[ThirdPartyId] = paTmp.ThirdPartyId,
				[MainAccountId] = paTmp.MainAccountId,
				[CostCenterId] = paTmp.CostCenterId,
				[DocumentDate] = paTmp.DocumentDate,
				[CustomerId] = paTmp.CustomerId,
				[SellerId] = paTmp.SellerId,
				[Value] = paTmp.Value,
				[TransferValue] = paTmp.TransferValue,
				[DebitValue] = paTmp.DebitValue,
				[CreditValue] = paTmp.CreditValue,
				[DistributionValue] = paTmp.DistributionValue,
				[Balance] = paTmp.Balance,
				[Observations] = paTmp.Observations,
				[OpeningBalance] = paTmp.OpeningBalance,
				[CurrencyId] =paTmp.CurrencyId,
				[TRMValue] = paTmp.TRMValue,
				[ValueInCurrencyHeader] = paTmp.ValueInCurrencyHeader,
				[ThirdPartyBeneficiaryId] = paTmp.ThirdPartyBeneficiaryId,
				[Status] = @Status,
				[ModificationUser] = @User,
				[ModificationDate] = [Common].[GETDATE](),
			ConfirmationDate = @ConfirmationDate ,
			ConfirmationUser = @ConfirmationUser 					   					   
			FROM @PortfolioAdvance paTmp
				INNER JOIN Portfolio.PortfolioAdvance pa ON paTmp.Id = pa.Id
			WHERE paTmp.ChangeTracker <> @Deleted;

			--Actualizo el detalle del recibo de caja
			UPDATE [Treasury].[CashReceiptDetails]
			SET
				[IdThirdParty] = crdTmp.IdThirdParty,
				[IdMainAccount] = crdTmp.IdMainAccount,
				[IdCostCenter] = crdTmp.IdCostCenter,
				[Nature] = crdTmp.Nature,
				[IdCashReceiptConcept] = crdTmp.IdCashReceiptConcept,
				[CashReceiptConceptAffectation] = crdTmp.CashReceiptConceptAffectation,
				[Value] = crdTmp.Value,
				[IdRetentionConcept] = crdTmp.IdRetentionConcept,
				[PercentageRetention] = crdTmp.PercentageRetention,
				[BillingValue] = crdtmp.BillingValue,
				[BaseValue] = crdTmp.BaseValue,
				[CardNumber] = crdTmp.CardNumber,
				[Detail] = crdTmp.Detail,
				[IdCashFlowConcept] = crdTmp.IdCashFlowConcept,
				[CurrencyId] =crd.CurrencyId,
				[TRM] = crd.TRM,
				[ValueInCurrencyHeader] = crdTmp.ValueInCurrencyHeader,
				[ThirdPartyBeneficiaryId] = crdTmp.ThirdPartyBeneficiaryId
			FROM @CashReceiptDetails crdTmp
				INNER JOIN Treasury.CashReceiptDetails crd ON crdTmp.Id = crd.Id
			WHERE crdTmp.ChangeTracker = @Modified;

			--recorro los detalles del recibo de caja
			DECLARE @CashReceiptDetailId INT, @CashReceiptDetailIdTmp INT, @DetailTmp VARCHAR(MAX);						
			Set @Rows = 1
			Set @RowId = 1

			While @Rows > 0
			Begin
				Select Top 1 
					@RowId = RowId,
					@CashReceiptDetailId = Id,
					@CashReceiptDetailIdTmp = IdTmp,
					@DetailTmp = Detail
				From @CashReceiptDetails
				Where RowId >= @RowId And ChangeTracker <> @Deleted Order By RowId

				Set @Rows = @@ROWCOUNT
				If @Rows = 0 
					Break

				SET @DetailTmp = REPLACE(@DetailTmp, 'code', @CodeCashReceipt);
				--si el detalle es nuevo
				IF(@CashReceiptDetailId = 0)
				BEGIN 
					--inserto el nuevo detalle para obtener el id
					INSERT INTO [Treasury].[CashReceiptDetails]
					(
						[IdCashReceipt],[IdThirdParty],[IdMainAccount],[IdCostCenter],[Nature],[IdCashReceiptConcept],[CashReceiptConceptAffectation]
						,[Value],[IdRetentionConcept],[PercentageRetention],[BillingValue],[BaseValue],[CardNumber],[Detail],[IdCashFlowConcept], 
						[CurrencyId], [TRM], [ValueInCurrencyHeader], [ThirdPartyBeneficiaryId]
					)
						SELECT @IdCashReceipt,[IdThirdParty],[IdMainAccount],IIF([IdCostCenter] > 0,[IdCostCenter],NULL),[Nature],[IdCashReceiptConcept],[CashReceiptConceptAffectation],
								[Value],[IdRetentionConcept],[PercentageRetention],[BillingValue],[BaseValue],[CardNumber],@DetailTmp,[IdCashFlowConcept]
								, [CurrencyId],[TRM], [ValueInCurrencyHeader], [ThirdPartyBeneficiaryId]
						FROM @CashReceiptDetails
						WHERE IdTmp = @CashReceiptDetailIdTmp;

					SET @CashReceiptDetailId = SCOPE_IDENTITY();
				END;

				update @CashReceiptAccountReceivable set CashReceiptDetailId = @CashReceiptDetailId	  
					where CashReceiptDetailIdTmp = @CashReceiptDetailIdTmp and ChangeTracker='Added'

				update @CashReceiptDetails set Id = @CashReceiptDetailId 
					where IdTmp = @CashReceiptDetailIdTmp and ChangeTracker='Added'                                
					
				--insertamos los nuevos detalles
				INSERT INTO [Treasury].[CashReceiptAccountReceivable]
					SELECT @CashReceiptDetailId, [AccountReceivableId], [InvoiceNumber], [Value], [ValueInCurrencyHeader]
					FROM @CashReceiptAccountReceivable
					WHERE CashReceiptDetailIdTmp = @CashReceiptDetailIdTmp AND ChangeTracker = @Added;

				INSERT INTO [Treasury].[CashReceiptAdvancePayment]
					SELECT @CashReceiptDetailId, [AdvancePaymentId], [AdvancePaymentCode], [PaymentValue],[ValueInCurrencyHeader]
					FROM @CashReceiptAdvancePayment
					WHERE CashReceiptDetailIdTmp = @CashReceiptDetailIdTmp AND ChangeTracker = @Added;

				INSERT INTO [Treasury].[CashReceiptDetailAccountPayable]
					SELECT @CashReceiptDetailId,[AccountPayableId],[RefundValue]
					FROM @CashReceiptDetailAccountPayable
					WHERE CashReceiptDetailIdTmp = @CashReceiptDetailIdTmp AND ChangeTracker = @Added;

				INSERT INTO [Portfolio].[PortfolioAdvance]
				(
					[Code],[CashReceiptId],[CashReceiptDetailId],[AdmissionNumber],[ThirdPartyId],[MainAccountId],
					[CostCenterId],[DocumentDate],[CustomerId],[SellerId],[Value],[TransferValue],[DebitValue],[CreditValue],
					[DistributionValue],[Balance],[Observations],[OpeningBalance],[Status],[CreationUser],[CreationDate],
					[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],[AnnulmentUser],[AnnulmentDate]
					,[CurrencyId], [TRMValue], [ValueInCurrencyHeader], [ThirdPartyBeneficiaryId]
				)
					SELECT @CodeCashReceipt,@IdCashReceipt,@CashReceiptDetailId,[AdmissionNumber],[ThirdPartyId],[MainAccountId],
							[CostCenterId],[DocumentDate],[CustomerId],[SellerId],[Value],[TransferValue],[DebitValue],[CreditValue],
							[DistributionValue],[Balance],[Observations],[OpeningBalance],@Status,@User,[Common].[GETDATE](),
							NULL,NULL,@ConfirmationUser,@ConfirmationDate,NULL,NULL,[CurrencyId], [TRMValue], [ValueInCurrencyHeader],
							[ThirdPartyBeneficiaryId]
					FROM @PortfolioAdvance
					WHERE CashReceiptDetailIdTmp = @CashReceiptDetailIdTmp AND ChangeTracker = @Added;

				Set @RowId += 1
			End
			
			--inserto los nuevos metodos
			INSERT INTO Treasury.PaymentMethods
			(
				IdCashReceipt,
				PaymentMethodTypes,
				[Value],
				IdBank,
				CheckNumber,
				DepositDate,
				IdEntityBankAccount,
				DepositNumber,
				DepositType,
				IdCard,
				CardNumber,
				BaseValue,
				CommissionValue,
				PercentageCommission,
				RTFValue,
				PercentageRTF,
				ICAValue,
				PercentageICA,
				IdCostCenter,
				CurrencyId,
				TRM,
				ValueInCurrencyHeader,
				IdAgreementsRedemptionPoints,
				TransactionNumber,
				TransactionDate,
				RedemptionPoints,
				CardType
			)
			SELECT @IdCashReceipt,
					PaymentMethodTypes,
					[Value],
					IdBank,
					CheckNumber,
					DepositDate,
					IdEntityBankAccount,
					DepositNumber,
					DepositType,
					IdCard,
					CardNumber,
					BaseValue,
					CommissionValue,
					PercentageCommission,
					RTFValue,
					PercentageRTF,
					ICAValue,
					PercentageICA,
					IdCostCenter,
					CurrencyId,
					TRM,
					ValueInCurrencyHeader,
					IdAgreementsRedemptionPoints,
					TransactionNumber,
					TransactionDate,
					RedemptionPoints,
					CardType
			FROM @PaymentMethods
			WHERE ChangeTracker = 'Added';
			
			--actualizo los metodos de pago
			UPDATE Treasury.PaymentMethods
			SET
			--Id - this column value is auto-generated
				IdCashReceipt = @IdCashReceipt, -- int
				PaymentMethodTypes = pm.PaymentMethodTypes, -- tinyint
				[Value] = pm.[Value], -- decimal
				IdBank = pm.IdBank, -- int
				CheckNumber = pm.CheckNumber, -- varchar
				DepositDate = pm.DepositDate, -- datetime
				IdEntityBankAccount = pm.IdEntityBankAccount, -- int
				DepositNumber = pm.DepositNumber, -- varchar
				DepositType = pm.DepositType, -- tinyint
				IdCard = pm.IdCard, -- int
				CardNumber = pm.CardNumber, -- varchar
				BaseValue = pm.BaseValue, -- decimal
				CommissionValue = pm.CommissionValue, -- decimal
				PercentageCommission = pm.PercentageCommission, -- decimal
				RTFValue = pm.RTFValue, -- decimal
				PercentageRTF = pm.PercentageRTF, -- decimal
				ICAValue = pm.ICAValue, -- decimal
				PercentageICA = pm.PercentageICA, -- decimal
				IdCostCenter = pm.IdCostCenter, -- int
				CurrencyId = pm.CurrencyId,--int
				TRM = pm.TRM,-- decimal
				ValueInCurrencyHeader = pm.ValueInCurrencyHeader,-- decimal
				IdAgreementsRedemptionPoints = pm.IdAgreementsRedemptionPoints, --int
				TransactionNumber = pm.TransactionNumber,--varchar
				TransactionDate = pm.TransactionDate,--datetime
				RedemptionPoints= pm.RedemptionPoints, --int
				CardType = pm.CardType
			FROM @PaymentMethods pm
			INNER JOIN Treasury.PaymentMethods pm2 ON pm.id = pm2.Id
			WHERE pm.ChangeTracker = @Modified;

			--elimino los metdos de pago	  
			DELETE Treasury.PaymentMethods
			FROM @PaymentMethods pm 
			INNER JOIN Treasury.PaymentMethods pm2 ON pm.id = pm2.Id
			WHERE pm.ChangeTracker = @Deleted;
		END;
			 
		--si se guardo con errores
		if(select COUNT(*) from @TableErrors )>0 begin 			
			SELECT @Message = COALESCE(@Message + '', '') + [message]  from @TableErrors
			
			SELECT	@CodeResult = 0,
					@MessageResult = @Message,
					@StatusResult = 1
			RETURN
		end

		--********************************CONFIRMACION DEL RECIBO DE CAJA***************************************************************
		
		if(@Status = 2) 
		begin
			--Variable que guarda el valor sin redencion de puntos
			declare @ValueWithOutRedemptionPoints as DECIMAL(18,2) 

			--Obtengo el valor real por el cual se va a afectar la caja, sin redencion de puntos.PaymentMethodTypes  5 = redencion de puntos  
			select @ValueWithOutRedemptionPoints = sum(Value) from @PaymentMethods where PaymentMethodTypes <> 5

			--validacion cuando solo se agrega como metodo de pago redencion de puntos, el valor de la caja no se ve afectado
			SET @ValueWithOutRedemptionPoints = ISNULL(@ValueWithOutRedemptionPoints, 0)

			if (@CollectType =1) 
			begin

				--se inserta un registro en la tabla de saldo de tesoreria
				INSERT INTO [Treasury].[TreasuryBalance]
				(
					[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
				)
					select @CodeCashReceipt ,@DocumentDate ,1,1,@IdCashRegister ,null,CurrentBalance,@ValueWithOutRedemptionPoints ,[Common].[GETDATE]() from Treasury.CashRegisters where Id = @IdCashRegister 

				-- se actualiza la caja
				update Treasury.CashRegisters set CurrentBalance +=@ValueWithOutRedemptionPoints ,IsMovement = 1 where Id = @IdCashRegister 

				--cursor para crear una CxC por cada metodo de pago que sea redencion de puntos y diferente convenio
				declare @IdAgreementsRedemptionPoints as INT

				declare PaymentMethodPoints cursor 
				for select pmtmp.IdAgreementsRedemptionPoints from @PaymentMethods pmtmp where PaymentMethodTypes = 5

				OPEN PaymentMethodPoints 
				fetch next from PaymentMethodPoints  into @IdAgreementsRedemptionPoints
					while (@@FETCH_STATUS = 0)
					begin
					--convierto el metodo de pago en xml
					select @SubXml =  convert
									(
										xml, 
										(
											select * from @PaymentMethods 
											where IdAgreementsRedemptionPoints = @IdAgreementsRedemptionPoints
											For xml AUTO,TYPE, ELEMENTS
											)
										)	 

						--Sp que crea una CxC para cada registro de redencion de puntos sin tener que crear un documento contable
						exec [Billing].[SP_CreateAccountReceivableWithoutJournalVoucher_Output] @SubXml,@IdCashReceipt, @OperatingUnitId, @IdThirdParty,@DocumentDate,@User, @Code_Output OUTPUT, @Message_Output OUTPUT
						
						--validamos que no retorne errores
						IF ISNULL(@Code_Output, 999) <> 0 
						BEGIN
							SELECT	@CodeResult = 999,
									@MessageResult = ISNULL(@Message_Output, 'Error al crear la cuenta por cobrar'),
									@IdCashRegister = 0,
									@StatusResult = 3
							RETURN
						END

						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
								
						fetch next from PaymentMethodPoints into @IdAgreementsRedemptionPoints
					end
				CLOSE PaymentMethodPoints
				DEALLOCATE PaymentMethodPoints

			end
			else 
			begin
				--se inserta un registro en la tabla de saldo de tesoreria
				INSERT INTO [Treasury].[TreasuryBalance]
				(
					[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
				)
				select @CodeCashReceipt ,@DocumentDate ,1,1,null ,@IdBankAccount ,CurrentBalance,@ValueWithOutRedemptionPoints ,[Common].[GETDATE]() from Treasury.EntityBankAccounts  where Id = @IdBankAccount  

				--se actuliza la cuenta bancaria
				update Treasury.EntityBankAccounts set CurrentBalance +=@ValueWithOutRedemptionPoints  where Id = @IdBankAccount  
			end

			--valido que existan detalles con comportamiento 2-Cancelacion / Abonos Facturas CxC y qye tengan facturas asociadas para hacer pagos parciales en glosas
			if Exists 
			(
				select 1 from @CashReceiptDetails crd 
				inner join @CashReceiptAccountReceivable crar on crd.IdTmp = crar.CashReceiptDetailIdTmp 
				where crd.CashReceiptConceptAffectation = @Dos and crd.ChangeTracker <> @Deleted
			)
			begin				
				--Tabla de resultados para la generacion de detalles de comprobante contable para provision y deterioro
				declare @ResultProvisionAndDeterioration table(Id int, [Status] int, [Message] varchar(max),
				IdMainAccount int, IdThirdParty int, IdCostCenter int, DebitValue decimal(20, 4), CreditValue decimal(20, 4))

				--Obtengo los datos que se asignarán en el xml para enviar al sp
				select @SubXml =  convert
				(
					xml, 
					(
						select Data.AccountReceivableId, @DocumentDate DocumentDate, Data.InvoiceNumber, Data.ValueInCurrencyHeader as Value
						from @CashReceiptDetails crd 
						join @CashReceiptAccountReceivable Data on crd.IdTmp = Data.CashReceiptDetailIdTmp 
						where crd.CashReceiptConceptAffectation = @Dos and crd.ChangeTracker <> @Deleted
						For xml AUTO,TYPE, ELEMENTS
					)
				)
				
				--Se ejecuta el sp que genera los detalles de comprobante para provision y deterioro
				insert @ResultProvisionAndDeterioration
					exec Portfolio.SP_CreateDetailsJournalVoucher @SubXml
			
				--Se valida que el sp no haya devuelto algun error
				if Exists (select 1 from @ResultProvisionAndDeterioration where [Status] = @Cero or [Status] = @Dos)
				Begin
					select @Message = COALESCE(@Message + '', '') + [Message] from @ResultProvisionAndDeterioration where [Status] = @Cero or [Status] = @Dos

					SELECT	@CodeResult = 999,
							@MessageResult = @Message,
							@IdCashRegister = 0,
							@StatusResult = 3
					RETURN 
				End

				--Si no hay ningun error entonces se procede a crear los demas detalles de comprobante de provision y deterioro
				--siempre y cuando hayan registros en estado ok
				if Exists (select 1 from @ResultProvisionAndDeterioration where [Status] = @Uno)
				Begin
					insert into @JournalVoucherDetails 
					SELECT
							0,
							0,
							rpd.IdMainAccount,
							IIF(ma.HandlesThirdParty = 1, rpd.IdThirdParty, NULL),
							IIF(ma.HandlesCostCenter  = 1, rpd.IdCostCenter,  NULL),
							rpd.DebitValue,
							rpd.CreditValue,
							N'Detalle generado con Provision/Deterioro',
							NULL, NULL, NULL, NULL
					from @ResultProvisionAndDeterioration rpd
					inner join GeneralLedger.MainAccounts ma on ma.Id = rpd.IdMainAccount
					where rpd.Status = @Uno
				End
				
				--recorro las facturas para hacer los pagos parciales
				DECLARE @AccountReceivableIdCursor INT, @MainAccountIdCursor INT,@ValueCursor numeric(18,2)
				DECLARE @RoundToleranceItem DECIMAL(18,2)

				--declaro las tablas de pagos parciales
				--Cabecera
				declare @PartialPaymentsC as table
				(
					Id INT,
					CustomerId INT,
					DocumentDate DATETIME,
					State CHAR(1),
					Comments VARCHAR(250),
					EntityId INT,
					EntityCode VARCHAR(20),
					EntityName VARCHAR(250)
				)

				--Detalle
				declare @PartialPaymentsD as table
				(
					Id INT, 
					PartialPaymentsCId INT,
					PortfolioGlosaId INT,
					InvoiceNumber VARCHAR(50),
					InvoiceDate DATETIME,
					RadicatedNumber VARCHAR(50),
					RadicatedDate DATETIME,
					PatientCode VARCHAR(15),
					PatientName VARCHAR(200),
					ContractCode VARCHAR(15),
					ValuePendingConciliation DECIMAL(18,2),
					ValuePayments DECIMAL(18,2)
				)
				    
				Set @Rows = 1
				Set @RowId = 1

				While @Rows > 0
				Begin						
					Select Top 1 
							@RowId = crar.RowId, 
							@AccountReceivableIdCursor = crar.AccountReceivableId ,
							@MainAccountIdCursor = crd.IdMainAccount, 
							@ValueCursor = crar.[Value]   
					From @CashReceiptDetails crd 
					Inner Join @CashReceiptAccountReceivable crar on crd.IdTmp = crar.CashReceiptDetailIdTmp 
					Where crar.RowId >= @RowId And crd.CashReceiptConceptAffectation = @Dos and crd.ChangeTracker <> @Deleted
					Order By crar.RowId

					Set @Rows = @@ROWCOUNT
					If @Rows = 0 
						Break

					if(@CompanyType =1) 
					begin
						--Empresa privada
						--se valida que las cuentas contables coincidan para hacer los pagos parciales
						if Exists 
						(
							select 1 from Portfolio.AccountReceivable ar 
							Inner Join Portfolio.AccountReceivableAccounting ara  on ar.Id = ara.AccountReceivableId 
							where ara.AccountReceivableId  = @AccountReceivableIdCursor and ara.MainAccountId = @MainAccountIdCursor 
								And ar.AccountObjectionRemediedId is not null 
								And ar.AccountObjectionRemediedId = @MainAccountIdCursor and ar.PortfolioStatus > @Tres
						)
						begin														
							--Valido que exista informacion en la tabla glosaPortfolioGlosada
							if Not Exists 
							(
								select 1 from Glosas.GlosaPortfolioGlosada pgg 
								inner join Portfolio.AccountReceivable ar  on pgg.InvoiceNumber = ar.InvoiceNumber  
								where ar.Id = @AccountReceivableIdCursor
							) 
							begin
								SELECT	@CodeResult = 999,
										@MessageResult = 'No se encontro información en la cartera de glosa',
										@IdCashRegister = 0,
										@StatusResult = 3
								RETURN
							end
						end
					end
					else 
					begin
						--Empresa publica
						if Exists 
						(
							select 1 from Portfolio.AccountReceivable ar 
							inner join Portfolio.AccountReceivableAccounting ara  on ar.Id = ara.AccountReceivableId 
							where ara.AccountReceivableId  = @AccountReceivableIdCursor 
								And ara.MainAccountId = @MainAccountIdCursor and ar.AccountRadicateId  is not null 
								And ar.AccountRadicateId = @MainAccountIdCursor and ar.PortfolioStatus <> @Uno
						)
						begin
							--Valido que exista informacion en la tabla glosaPortfolioGlosada
							if NOT Exists 
							(
								select 1
								from Glosas.GlosaPortfolioGlosada pgg 
								inner join Portfolio.AccountReceivable ar  on pgg.InvoiceNumber = ar.InvoiceNumber  
								where ar.Id = @AccountReceivableIdCursor
							)
							begin
								SELECT	@CodeResult = 999,
										@MessageResult = 'No se encontro información en la cartera de glosa',
										@IdCashRegister = 0,
										@StatusResult = 3
								RETURN
							end
						end
					end

											
					delete @PartialPaymentsC
					delete @PartialPaymentsD 

					insert into @PartialPaymentsC 
						select 0,ar.CustomerId,@DocumentDate,2,'Creado desde recibo de caja',@IdCashReceipt,@CodeCashReceipt,'PortfolioTransfer'
						from Portfolio.AccountReceivable ar 
						where ar.Id = @AccountReceivableIdCursor 

					if(@CompanyType =1) 
					begin
						insert into @PartialPaymentsD 
						select 0,0,pgg.Id,ar.InvoiceNumber,ar.AccountReceivableDate ,pgg.RadicatedNumber,pgg.RadicatedDate ,
							pgg.PatientCode,pgg.PatientName,ISNULL(pgg.ContractCode, ''),pgg.BalanceGlosa, (@ValueCursor - (ar.Balance - pgg.BalanceGlosa))
						from Glosas.GlosaPortfolioGlosada pgg 
						inner join Portfolio.AccountReceivable ar on pgg.InvoiceNumber = ar.InvoiceNumber  
						where ar.Id = @AccountReceivableIdCursor AND pgg.BalanceGlosa > 0 
					end
					else
					begin
						insert into @PartialPaymentsD 
							select 0,0,pgg.Id,ar.InvoiceNumber,ar.AccountReceivableDate ,pgg.RadicatedNumber,pgg.RadicatedDate ,
									pgg.PatientCode, pgg.PatientName, isnull(pgg.ContractCode, ''),pgg.BalanceGlosa, (@ValueCursor - (ar.Balance - pgg.BalanceGlosa))
							from Glosas.GlosaPortfolioGlosada pgg 
							inner join Portfolio.AccountReceivable ar on pgg.InvoiceNumber = ar.InvoiceNumber  
							where ar.Id = @AccountReceivableIdCursor AND pgg.BalanceGlosa > 0 
					end

					--si tiene detalles la tabla se hace el pago parcial
					if (select COUNT(*) from @PartialPaymentsD)>0 
					begin
						--se obtiene el xml para generar pagos parciales
						select @SubXml =  convert
						(
							xml, 
							(
								select * from @PartialPaymentsC PartialPaymentsC 
								inner join @PartialPaymentsD PartialPaymentsD on PartialPaymentsC.id = PartialPaymentsD.PartialPaymentsCId 
								For xml AUTO,TYPE, ELEMENTS
							)
						)
								 
						delete @resultPartialPaymentC
						--ejecuto el sp de pagos parciales
						insert @resultPartialPaymentC exec Glosas.SP_GeneratePartialPayments  @SubXml, @User
							
						if (select code  from @resultPartialPaymentC) = '999' begin
							select @Message = MessageResult  from @resultPartialPaymentC 
								
							SELECT	@CodeResult = 999,
									@MessageResult = 'No se generó pagos parciales',
									@IdCashRegister = 0,
									@StatusResult = 3
							RETURN
						end
					end

					declare @ValueAccountReceivable numeric(18,2)

					select @ValueAccountReceivable = [Value]
					from Portfolio.AccountReceivable 
					where Id = @AccountReceivableIdCursor

					declare @DiscountValueItem numeric(18,2) = 0
					declare @DiscountMainAccountId int

					if Exists 
					(
						select 1 
						from Portfolio.AccountReceivablePromptPayment  
						where DeadLine >= cast( @DocumentDate as date) and AccountReceivableId =@AccountReceivableIdCursor  
					)
					begin
						declare @PercentageDiscount numeric(5,2)
								
						select top 1 @PercentageDiscount = PercentageDiscount ,@DiscountMainAccountId = MainAccountId 
						from Portfolio.AccountReceivablePromptPayment 
						where DeadLine >= cast( @DocumentDate as date) and AccountReceivableId =@AccountReceivableIdCursor  
						order by DeadLine asc

						set @DiscountValueItem = @ValueAccountReceivable * @PercentageDiscount /100
					end

					if (@DiscountValueItem+@ValueCursor = @ValueAccountReceivable and @DiscountValueItem > 0) 
					begin
						--inserto en la tabla para luego crear el detalle en el comprobante con el valor del descuento
						insert into @DiscountValue values(@AccountReceivableIdCursor,@DiscountMainAccountId,@MainAccountIdCursor,@DiscountValueItem)
					end
					else
					begin
						set @DiscountValueItem = 0
					end

					-- Obtener tolerancia de redondeo basada en la moneda de la factura
					SELECT @RoundToleranceItem = Common.GetRoundTolerance(c.RoundingType)
					FROM Portfolio.AccountReceivable ar
					JOIN Common.Currency c ON ar.CurrencyId = c.Id
					WHERE ar.Id = @AccountReceivableIdCursor
					SET @RoundToleranceItem = ISNULL(@RoundToleranceItem, 0.01)

					--Validacion de error cartera negativa en PortfolioAccountReeciable Balance.
					if Exists
					(
						select 1
						from Portfolio.AccountReceivable 
						where (@ValueCursor + @DiscountValueItem - Balance) > @RoundToleranceItem and Id = @AccountReceivableIdCursor
					) begin
						declare @InvoiceNumberPortfolio VARCHAR(20)
						declare @BalancePortfolio DECIMAL(18, 2),
								@V75000 Decimal(18, 0) = 75000

						select @InvoiceNumberPortfolio = InvoiceNumber,@BalancePortfolio = Balance
						from Portfolio.AccountReceivable  where (@ValueCursor + @DiscountValueItem - Balance) > @RoundToleranceItem and Id = @AccountReceivableIdCursor

						SELECT	@CodeResult = 999,
								@MessageResult = 'El valor pagado (' + cast(@ValueCursor + @DiscountValueItem as varchar(30)) + ') no puede ser mayor al saldo (' + cast(@BalancePortfolio as varchar(30)) + ') de la factura : '+@InvoiceNumberPortfolio,
								@IdCashRegister = 0,
								@StatusResult = 3
						RETURN
					end

					--actualizo el saldo de la cuenta por cobrar
					update Portfolio.AccountReceivable
						set Balance = CASE
							WHEN (Balance - (@ValueCursor + @DiscountValueItem)) < 0 AND ABS(Balance - (@ValueCursor + @DiscountValueItem)) <= @RoundToleranceItem THEN 0
							ELSE Balance - (@ValueCursor + @DiscountValueItem)
						END
					where Id = @AccountReceivableIdCursor

					update Portfolio.AccountReceivableAccounting
						set Balance = CASE
							WHEN (Balance - @ValueCursor) < 0 AND ABS(Balance - @ValueCursor) <= @RoundToleranceItem THEN 0
							ELSE Balance - @ValueCursor
						END
					where AccountReceivableId = @AccountReceivableIdCursor and MainAccountId = @MainAccountIdCursor

					declare @responseRevaluaiton table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
					
					INSERT @responseRevaluaiton
					EXEC [Portfolio].[SP_AccountReceivableRevaluation]
						@AccountReceivableIdCursor,
						@ValueCursor,
						@CodeCashReceipt,
						@IdCashReceipt,
						'CashReceipts',
						@User,
						@DocumentDate

					if (select COUNT(*) from @responseRevaluaiton WHERE Code = '999') > 0
					Begin			
						set @Message = (select STRING_AGG(MessageResult, ', ')  from @responseRevaluaiton)

						SELECT	@CodeResult = 999,
								@MessageResult = @Message,
								@IdCashRegister = 0,
								@StatusResult = 3
						RETURN
					End 

					insert into @TableResult
					SELECT top 1 'Se generó el Comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name + CHAR(13) + CHAR(10)
					from GeneralLedger.JournalVoucherTypes jvt
					INNER JOIN GeneralLedger.CompanySettings CS ON CS.ProfitLostJournalVoucherTypeId = jvt.id

					---se crean los registros en CashReceiptAccountReceivableShare por cada cuota de AccountReceivableShare
					declare @accountReceivableShareIdCursor int
					declare @cashReceiptAccountReceivableIdCursor int
					declare @valueShareCursor decimal(18,2)
								
					Declare @_Rows Int, @_RowId Int
					Set @_Rows = 1
					Set @_RowId = 1

					Declare @tmpAccountReceivableShare Table
					(
						RowId Int Identity(1,1) Primary key,
						AccountReceivableId Int,
						accountReceivableShareIdCursor Int,
						cashReceiptAccountReceivableIdCursor Int,
						valueShareCursor Decimal(18,2)
					)

					Delete From @tmpAccountReceivableShare
					Insert Into @tmpAccountReceivableShare
					Select ars.AccountReceivableId, ars.Id,crar.id,ars.Balance  
					From Portfolio.AccountReceivableShare ars 
					Inner Join Portfolio.AccountReceivable ar  on ars.AccountReceivableId = ar.Id
					Inner Join Treasury.CashReceiptAccountReceivable crar  on crar.AccountReceivableId = ar.Id 
					and crar.CashReceiptDetailId = @CashReceiptDetailId
					where ars.AccountReceivableId = @AccountReceivableIdCursor

					While @_Rows > 0
					begin									
						Select Top 1 @_RowId = RowId,
							@accountReceivableShareIdCursor = accountReceivableShareIdCursor,
							@cashReceiptAccountReceivableIdCursor = cashReceiptAccountReceivableIdCursor,
							@valueShareCursor = valueShareCursor
						From @tmpAccountReceivableShare
						where RowId >= @_RowId 
						Order By RowId

						Set @_Rows = @@ROWCOUNT
						If (@_Rows = 0) OR (@ValueCursor <=0)
							Break

						if(@ValueCursor >@valueShareCursor ) 
						begin								
							INSERT INTO [Treasury].[CashReceiptAccountReceivableShare]
							(
								[CashReceiptAccountReceivableId],[AccountReceivableShareId],[Value]
							)
								select @cashReceiptAccountReceivableIdCursor,@accountReceivableShareIdCursor,@valueShareCursor 

							update Portfolio.AccountReceivableShare 
								set PaymentValue +=@valueShareCursor , Balance -=@valueShareCursor 
							where Id = @accountReceivableShareIdCursor
						end
						else 
						begin		
							INSERT INTO [Treasury].[CashReceiptAccountReceivableShare]
							(
								[CashReceiptAccountReceivableId],[AccountReceivableShareId],[Value]
							)
								select @cashReceiptAccountReceivableIdCursor,@accountReceivableShareIdCursor,@ValueCursor  

							update Portfolio.AccountReceivableShare set PaymentValue +=@ValueCursor , Balance -=@ValueCursor where Id = @accountReceivableShareIdCursor
						end

						set @ValueCursor -=@valueShareCursor 
						Set @_RowId += 1
					End
					------------------------------------------------------------------------------------------------------

					Set @RowId += 1
				End

				EXEC [Treasury].[SP_GenerateCollectionByCashReceiptId_Output] @OperatingUnitId, @IdCashReceipt, @User, @Code_Output OUTPUT, @Message_Output OUTPUT

				IF ISNULL(@Code_Output, 999) <> 0 
				BEGIN
					SELECT	@CodeResult = 999,
							@MessageResult = ISNULL(@Message_Output, 'Error al generar el recaudo'),
							@IdCashRegister = 0,
							@StatusResult = 3
					RETURN
				END

				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			end

			----reintegro de anticipos
			if (select COUNT(*) from @CashReceiptDetails crd  where crd.CashReceiptConceptAffectation = @Tres and crd.ChangeTracker <> @Deleted)>0 
			begin				
				update Payments.AdvancePayments set Balance -=crapTmp.PaymentValue , CreditValue +=crapTmp.PaymentValue
				from @CashReceiptAdvancePayment crapTmp inner join Payments.AdvancePayments ap on crapTmp.AdvancePaymentId = ap.Id 
				where crapTmp.ChangeTracker<>'Deleted'
			end
			 
			------reintegro de cuentas por pagar
			if Exists (select 1 from @CashReceiptDetails crd  where crd.CashReceiptConceptAffectation = 4 and crd.ChangeTracker <> 'Deleted') 
			begin			 				
				declare  @idAccountPayableCursor int ,@idShareCursor int, 
					@refundValueCursor numeric(18,2),@initialValueShareCursor numeric(18,2),
					@balanceShareCursor numeric(18,2),@maximunValueRefund numeric(18,2)				
				
				Set @Rows = 1
				Set @RowId = 1

				While @Rows > 0
				Begin
					Select Top 1 @RowId = RowId, 
						@refundValueCursor = RefundValue,
						@idAccountPayableCursor = AccountPayableId 
					From @CashReceiptDetailAccountPayable 
					Where RowId >= @RowId And ChangeTracker<>@Deleted
					Order By RowId

					Set @Rows = @@ROWCOUNT
					If @Rows = 0 
						Break

					Declare @tmpAccountPayable Table(
						RowId Int Identity(1,1) Primary Key,
						Id Int, 
						InitialValue Decimal(20, 4),
						Balance Decimal(20, 4)
					)

					Delete From @tmpAccountPayable
					Insert Into @tmpAccountPayable
					select aps.Id, aps.InitialValue ,aps.Balance  
					from  Payments.AccountPayable ac  
					inner join Payments.AccountPayableShares aps on aps.IdAccountPayable = ac.Id 					
					where aps.IdAccountPayable =  @idAccountPayableCursor
					order by aps.Share

					Declare @__Rows Int, @__RowId Int
					Set @__Rows = 1
					Set @__RowId = 1

					While @__Rows > 0
					Begin
						Select Top 1 @__RowId = RowId, 
							@idShareCursor = Id,
							@initialValueShareCursor = InitialValue,
							@balanceShareCursor = Balance
						From @tmpAccountPayable Where RowId >= @__RowId Order By RowId
							
						Set @__Rows = @@ROWCOUNT
						If @__Rows = 0 
							Break

						if (@refundValueCursor <=0) begin 
							break
						end
						set @maximunValueRefund = @initialValueShareCursor-@balanceShareCursor					
					
						if (@refundValueCursor>@maximunValueRefund) begin					
							update Payments.AccountPayableShares set Balance += @maximunValueRefund , CreditValue += @maximunValueRefund where Id = @idShareCursor 
							set @refundValueCursor -=@maximunValueRefund 
						end
						else begin					
							update Payments.AccountPayableShares set Balance += @refundValueCursor  , CreditValue += @refundValueCursor  where Id = @idShareCursor 
							set @refundValueCursor =0 
							break
						end

						Set @__RowId += 1
					End

					Set @RowId += 1
				End

			end

			--actualizo el saldo de la factura
			update Payments.AccountPayable set Balance = shares.sumBalance 
			from @CashReceiptDetailAccountPayable cap 
			inner join (select cap.AccountPayableId , SUM(aps.Balance) as sumBalance 
				from @CashReceiptDetailAccountPayable cap inner join Payments.AccountPayable ap on cap.AccountPayableId = ap.Id 
			inner join Payments.AccountPayableShares aps on ap.Id = aps.IdAccountPayable 
				where cap.ChangeTracker <> @Deleted group by 
				cap.AccountPayableId ) shares on cap.AccountPayableId = shares .AccountPayableId 
			inner join Payments.AccountPayable ac on cap.AccountPayableId = ac.Id
		
			--generacion del comprobante contable
			if Not Exists (select 1 from Treasury.SettingsTreasury  where IdOperatingUnit = @OperatingUnitId) 
			begin
				SELECT '999' AS CodeMessage,'No se encontro parametros de tesoreria para la unidad operativa seleccionada' Message,0 AS CashReceiptId,CAST(3 AS TINYINT) AS [Status];
			end

			--Obtengo el libro oficial
			declare @LegalBookId integer
			select @LegalBookId = id  from GeneralLedger.LegalBook  where OfficialBook = @Uno

			--inserto la cabecera del comprobante
			insert into @JournalVouchers 
				select 0,0,0,@LegalBookId,JournalVoucherTypeCashReceipts,@DocumentDate ,0,2,'Recibo de Caja N'+@CodeCashReceipt +' '+ @Detail,@CodeCashReceipt,@IdCashReceipt,@EntityNameJournalVoucher,0,@CurrencyId,@EntityNameJournalVoucher,@User,[Common].[GETDATE](),@User,[Common].[GETDATE](),@User,[Common].[GETDATE]()
				from Treasury.SettingsTreasury  where IdOperatingUnit =@OperatingUnitId 

			if(select count(*) from @DiscountValue) > 0 
			begin
			SELECT
							0,
							0,
							rpd.IdMainAccount,
							IIF(ma.HandlesThirdParty = 1, rpd.IdThirdParty, NULL),
							IIF(ma.HandlesCostCenter  = 1, rpd.IdCostCenter,  NULL),
							rpd.DebitValue,
							rpd.CreditValue,
							N'Detalle generado con Provision/Deterioro',
							NULL, NULL, NULL, NULL
					from @ResultProvisionAndDeterioration rpd
					inner join GeneralLedger.MainAccounts ma on ma.Id = rpd.IdMainAccount
					where rpd.Status = @Uno
				--- Se insertan los descuentos
				insert into @JournalVoucherDetails 
					select  0,
							0,
							dv.mainAccountDiscountId,
							IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL),
							IIF(ma.HandlesCostCenter  = 1, ar.CostCenterId,  NULL),
							dv.value,
							 0,
							NULL,NULL, NULL, NULL, NULL 
					from @DiscountValue dv 
					inner join Portfolio.AccountReceivable ar  on dv.accountReceivableId = ar.Id 
					inner join GeneralLedger.MainAccounts ma on ma.id = dv.mainAccountDiscountId
			end

			--se realizan movientos diferentes cuando es pago de impuuesto predial
			if 
			(
				select AccountReceivableType  from @CashReceiptAccountReceivable car 
				inner join Portfolio.AccountReceivable ar  on car.AccountReceivableId = ar.id 
				where car.ChangeTracker<>@Deleted group by ar.AccountReceivableType
			)=8 
			begin
				insert into @JournalVoucherDetails 
					select  0,
							0,
							crdTmp.IdMainAccount,
							IIF(ma.HandlesThirdParty = 1, crdTmp.IdThirdParty, NULL),
							IIF(ma.HandlesCostCenter  = 1,crdTmp.IdCostCenter,  NULL),
							case when crdTmp.Nature  =  1 then crdTmp.ValueInCurrencyHeader else 0 end as DebitValue,
							case when crdTmp.Nature  =  1 then 0 else crdTmp.ValueInCurrencyHeader end as CreditValue,
							crdTmp.Detail,
							crdTmp.IdRetentionConcept ,
							crdTmp.PercentageRetention,
							crdTmp.BaseValue ,
							crdTmp.BillingValue  
					from @CashReceiptDetails crdTmp 
					inner join GeneralLedger.MainAccounts ma on ma.id = crdTmp.IdMainAccount
					where ChangeTracker<>@Deleted And Not Exists (select 1  from @CashReceiptAccountReceivable Where CashReceiptDetailId = crdTmp.Id)

				--valido que las facturas existan en la tabla de impuestos
				if Exists 
				(
					select 1 from @CashReceiptAccountReceivable cra 
					inner join Portfolio .AccountReceivable ar  on cra.AccountReceivableId = ar.id
					left join Taxes.TaxesInvoice i  on i.InvoiceNumber = ar.InvoiceNumber 
					where i.Id is null
				)
				begin					
					insert into @TableErrors 
						select 'La factura ' + ar.InvoiceNumber + ', no existe en el modulo de impuestos '+ CHAR(13) + CHAR(10)
						from @CashReceiptAccountReceivable cra 
						inner join Portfolio .AccountReceivable ar  on cra.AccountReceivableId = ar.id
						left join Taxes.TaxesInvoice i  on i.InvoiceNumber = ar.InvoiceNumber 
						where i.Id is null

					SELECT @Message = COALESCE(@Message + '', '') + [message]  from @TableErrors				
					
					SELECT	@CodeResult = 999,
							@MessageResult = @Message,
							@IdCashRegister = 0,
							@StatusResult = 3
					RETURN
				end

				declare @AccountReceivableId int, @taxesInvoiceId int,@detailValue numeric(18,2),@thirdAccountReceivable int,@costCenterAccountReceivable int

				Declare @tmpAccountReceivableInvoice Table(
					RowId Int Identity(1,1) Primary Key,
					AccountReceivableId Int,
					TaxesInvoiceId Int,
					CashReceiptAccountReceivableValue Decimal(18,2),
					AccountReceivableThirdPartyId Int,
					AccountReceivableCostCenterId Int
				)

				Delete From @tmpAccountReceivableInvoice
				Insert Into @tmpAccountReceivableInvoice
					Select cra.AccountReceivableId ,ti.Id ,cra.[Value],ar.ThirdPartyId,ar.CostCenterId   
					From @CashReceiptAccountReceivable cra 
					inner join Portfolio .AccountReceivable ar  on cra.AccountReceivableId = ar.id 
					inner join Taxes.TaxesInvoice ti  on ar.InvoiceNumber = ti.InvoiceNumber 
					where cra.ChangeTracker<> @Deleted

				Set @Rows = 1
				Set @RowId = 1

				While @Rows > 0
				Begin
					Select Top 1 @RowId = RowId, @AccountReceivableId = AccountReceivableId,
							@taxesInvoiceId = TaxesInvoiceId, @detailValue = CashReceiptAccountReceivableValue,
							@thirdAccountReceivable = AccountReceivableThirdPartyId,
							@costCenterAccountReceivable = AccountReceivableCostCenterId
					From @tmpAccountReceivableInvoice 
					where RowId >= @RowId Order By RowId

					Set @Rows = @@ROWCOUNT
					If @Rows = 0 
						Break

					----- Acredito todas las cuentas que se debitaron cuando se genero la factura
					declare @totalDiscount numeric(18,2) = isnull((select sum(value) from @DiscountValue where accountReceivableId = @AccountReceivableId),0)
					declare @totalPayment numeric(18,2) = @totalDiscount + @detailValue
					

					insert into @JournalVoucherDetails 
						select  0,
								0, 
								lc.DebitAccountId, 
								IIF(ma.HandlesThirdParty  = 1, @thirdAccountReceivable,  NULL), 
								IIF(ma.HandlesCostCenter  = 1, @costCenterAccountReceivable,  NULL), 
								0, 
								((td.Value * 100 / TaxValue) * @totalPayment /100), 
								lc.Name, 
								null,null,null,null
						from Taxes.TaxesInvoiceDetail td 
						inner join Taxes.TaxesInvoice t  on t.Id = td.TaxesInvoiceId
						inner join Taxes.TaxesLiquidationConcept lc  on td.LiquidationConceptId = lc.Id
						inner join GeneralLedger.MainAccounts ma on ma.Id = lc.DebitAccountId
						where t.Id = @taxesInvoiceId

					--- Debito la cuenta del concepto de la CAM
					insert into @JournalVoucherDetails 
						select  0,
								0, 
								lc.CreditAccountId, 
								IIF(ma.HandlesThirdParty  = 1, @thirdAccountReceivable,  NULL), 
								IIF(ma.HandlesCostCenter  = 1, @costCenterAccountReceivable,  NULL), 
								((td.Value * 100 / TaxValue) * @totalPayment /100), 
								0, 
								lc.Name, 
								null,null,null,null
						from Taxes.TaxesInvoiceDetail td 
						inner join Taxes.TaxesInvoice t  on t.Id = td.TaxesInvoiceId
						inner join Taxes.TaxesLiquidationConcept lc  on td.LiquidationConceptId = lc.Id
						inner join GeneralLedger.MainAccounts ma on ma.Id = lc.CreditAccountId
						where lc.ConceptType = @Tres and t.Id = @taxesInvoiceId

					--- Se acredita la cuenta de la cuenta por pagar de la cam
					insert into @JournalVoucherDetails 
						SELECT
						0,
						0,
						sel.Id,
						CASE WHEN sel.HandlesThirdParty = 1 THEN @thirdAccountReceivable END,
						CASE WHEN sel.HandlesCostCenter  = 1 THEN @costCenterAccountReceivable END,
						0,
						((td.Value * 100 / TaxValue) * @totalPayment /100),
						lc.Name, NULL, NULL, NULL, NULL
						FROM Taxes.TaxesInvoiceDetail td 
						INNER JOIN Taxes.TaxesInvoice t  ON t.Id = td.TaxesInvoiceId
						INNER JOIN Taxes.TaxesLiquidationConcept lc  ON td.LiquidationConceptId = lc.Id
						CROSS APPLY (
							SELECT TOP (1)
								   ma.Id,
								   ma.HandlesThirdParty,
								   ma.HandlesCostCenter
							FROM GeneralLedger.MainAccounts ma 
							INNER JOIN GeneralLedger.LegalBook l  ON l.Id = ma.LegalBookId
							WHERE ma.Number LIKE '2%'
							  AND ma.AllowsMovement = @Uno
							  AND l.OfficialBook    = @Uno
						) sel
						WHERE lc.ConceptType = @Tres AND t.Id = @taxesInvoiceId;

					Set @RowId += 1
				End

			end
			else begin 	
			insert into @JournalVoucherDetails 
					select 0,
					0,
					crdTmp.IdMainAccount ,
					IIF(ma.HandlesThirdParty = 1, crdTmp.IdThirdParty, NULL),
					IIF(ma.HandlesCostCenter  = 1, crdTmp.IdCostCenter,  NULL),
					case when crdTmp.Nature  =  1 then crdTmp.ValueInCurrencyHeader else 0 end as DebitValue,
					case when crdTmp.Nature  =  1 then 0 else crdTmp.ValueInCurrencyHeader end as CreditValue,
					crdTmp.Detail ,
					crdTmp.IdRetentionConcept ,
					crdTmp.PercentageRetention,
					crdTmp.BaseValue ,
					crdTmp.BillingValue  
					from @CashReceiptDetails crdTmp 
					inner join GeneralLedger.MainAccounts ma on ma.Id = crdTmp.IdMainAccount
					where ChangeTracker<>@Deleted

			end
	
			--inserto los detalles por los metodos de pago			
			declare @valuePaymetCursos numeric(18,2)
			declare @mainAccountPayment int
			declare @thirdPartyPayment int
			declare @costCenterPayment int
			declare @PaymentMethodsType int

			Set @Rows = 1
			Set @RowId = 1

			While @Rows > 0
			Begin
				
				Select Top 1 @RowId = RowId,
					@valuePaymetCursos = [ValueInCurrencyHeader] ,
					@PaymentMethodsType = PaymentMethodTypes
				From @PaymentMethods Where RowId >= @RowId And ChangeTracker <> @Deleted
				Order By RowId

				Set @Rows = @@ROWCOUNT
				If @Rows = 0 
					Break
				if (@CollectType =1) 
				begin				
					--si el metodo de pago es redencion de puntos se obtienen los datos del cliente
					if(@PaymentMethodsType=5)
					begin
						Select @thirdPartyPayment = arp.CustomerId,
							@mainAccountPayment = c.MainAccountReceivableId,
							@costCenterPayment = case when ma.HandlesCostCenter =1 then @IdCostCenter end  
						From @PaymentMethods pm
						join Treasury.AgreementsRedemptionPoints arp on pm.IdAgreementsRedemptionPoints = arp.Id
						join Common.Customer c on arp.CustomerId = c.Id
						join GeneralLedger.MainAccounts ma on c.MainAccountReceivableId = ma.Id
						Where RowId >= @RowId And ChangeTracker <> @Deleted
						Order By RowId
					end
					else
					begin
						select @thirdPartyPayment = cg.ThirdPartyId ,
						@mainAccountPayment = cg.IdMainAccount,
						@costCenterPayment = case when ma.HandlesCostCenter =1 then @IdCostCenter end  
						from Treasury.CashRegisters cg 
						inner join GeneralLedger.MainAccounts ma  on cg.IdMainAccount = ma.Id 
						where cg.Id = @IdCashRegister

						select @thirdPartyPayment = case when st.GetThirdPartyCashRegister = 1 then @thirdPartyPayment ELSE @IdThirdParty end 
						from Treasury.SettingsTreasury st 
						where st.IdOperatingUnit = @OperatingUnitId
					end					
				end
				else
				begin				
					select @thirdPartyPayment = ba.ThirdPartyId , @mainAccountPayment = ba.IdMainAccount,@costCenterPayment = case when ma.HandlesCostCenter =1 then @IdCostCenter else null end  from Treasury.EntityBankAccounts  ba
					inner join GeneralLedger.MainAccounts ma  on ba.IdMainAccount = ma.Id 
					where ba.Id = @IdBankAccount  

					select @thirdPartyPayment = case when st.GetThirdPartyBank = 1 then @thirdPartyPayment ELSE @IdThirdParty end 
					from Treasury.SettingsTreasury st  where st.IdOperatingUnit = @OperatingUnitId 

				end
				DECLARE @handlesThirdParty bit, 
				@handlesCostCenter bit;

				SELECT
					@handlesThirdParty = ma.HandlesThirdParty,
					@handlesCostCenter = ma.HandlesCostCenter
				FROM GeneralLedger.MainAccounts ma
				WHERE ma.Id = @mainAccountPayment;

				DECLARE @thirdPartyId  int = CASE WHEN @handlesThirdParty = 1 THEN @thirdPartyPayment END;
				DECLARE @costCenterId  int = CASE WHEN @handlesCostCenter = 1 AND @costCenterPayment > 0 THEN @costCenterPayment END;

				insert into @JournalVoucherDetails 
					select  0,
							0,
							@mainAccountPayment,
							@thirdPartyId,
							@costCenterId,
							@valuePaymetCursos,
							0,
							NULL, NULL, NULL, NULL, NULL
					
				Set @RowId += 1
			End

			--genero el XML para guardar el comprobante 
			select @SubXml =  convert
			(
				xml, 
				(
					select * from @JournalVouchers JournalVoucher 
					inner join @JournalVoucherDetails JournalVoucherDetail on JournalVoucher.id = JournalVoucherDetail.IdAccounting   
					For xml AUTO,TYPE, ELEMENTS
					)
				)
				
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml, @User 

			if (select code  from @resultJournalVoucher) = '999' 
			Begin			
				select @Message = MessageResult  from @resultJournalVoucher

				SELECT	@CodeResult = 999,
						@MessageResult = @Message,
						@IdCashRegister = 0,
						@StatusResult = 3
				RETURN
			End  

			-------------------fin comprobante--------------------

			--elimino el registro de la tabla de control
			delete Treasury.TreasuryControl where DocumentNumber = @CodeCashReceipt and DocumentType = @Uno
		end
		
		--********************************FIN CONFIRMACION DEL RECIBO DE CAJA***********************************************************

		if(@Status =2) 
		begin		
			--retorno cuando el recibo de caja se confirma

			insert into @TableResult select 'Se guardo y confirmo el Recibo de Caja con código '+ @CodeCashReceipt + CHAR(13) + CHAR(10)

			IF ISNULL(@Message, '') <> ''
			BEGIN
				insert into @TableResult select @Message + CHAR(13) + CHAR(10)
				SET @Message = ''
			END						
			
			insert into @TableResult
				SELECT 'Se generó el Comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name + CHAR(13) + CHAR(10)
				FROM Treasury.SettingsTreasury st
				JOIN GeneralLedger.JournalVoucherTypes jvt on st.JournalVoucherTypeCashReceipts = jvt.Id
				WHERE st.IdOperatingUnit = @OperatingUnitId
			
			---------------------------------------------
			if(select COUNT(*) from @resultPartialPaymentC )>0 begin
				insert into @TableResult select 'Saldos de glosas afectados correctamente ' + CHAR(13) + CHAR(10)
			end

			--retorno el mensaje 
			SELECT @Message = COALESCE(@Message + '', '') + [message]  from @TableResult
			
			SELECT	@CodeResult = 0,
					@MessageResult = @Message,
					@StatusResult = 1
		end
		else 
		begin
			SELECT	@CodeResult = 0,
					@MessageResult = '',
					@StatusResult = 1
		end
    END TRY
    BEGIN CATCH
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE()+', [Treasury].[SP_SaveCashReceipts] - Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)),
				@IdCashRegister = 0,
				@StatusResult = 3
    END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y registra un recibo de caja en el módulo de Tesorería a partir de un XML con los datos del cobro. Crea el encabezado del recibo (tercero, fecha, caja o cuenta bancaria receptora, valor, tipo de recaudo y responsable de pago), sus líneas de detalle con conceptos contables y flujo de caja, los métodos de pago utilizados (efectivo, cheque, tarjeta, depósito), y las relaciones con cuentas por cobrar, anticipos y cuentas por pagar que se están cruzando o reintegrando. Consulta la tabla CashReceipts para persistir el recibo, CashRegisters y EntityBankAccounts para obtener el prefijo y la caja o cuenta bancaria asociada, CashReceiptConcepts para validar los conceptos de recaudo con su afectación contable, y CompanySettings para determinar la moneda oficial de la empresa. Retorna como parámetros de salida el identificador del recibo creado, un código de resultado, un mensaje descriptivo del proceso y un estado de éxito o error.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCashReceipts_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCashReceipts_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_CreateAccountReceivableWithoutJournalVoucher_Output; Portfolio.SP_CreateDetailsJournalVoucher; Glosas.SP_GeneratePartialPayments; Portfolio.SP_AccountReceivableRevaluation; Treasury.SP_GenerateCollectionByCashReceiptId_Output; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Treasury.CashReceiptConcepts; Treasury.CashRegisters; Treasury.EntityBankAccounts; Treasury.CashReceipts; GeneralLedger.MainAccounts; GeneralLedger.ClosedMonth; Portfolio.AccountReceivable; Treasury.CashRegisterUser; Security.User; Treasury.EntityBankAccountUser; Portfolio.AccountReceivableAccounting; Payments.AdvancePayments; Treasury.TreasurySequence; Treasury.TreasurySequenceDetail; Common.Sequense; Glosas.GlosaPortfolioGlosada; Portfolio.AccountReceivablePromptPayment; Portfolio.AccountReceivableShare; Treasury.SettingsTreasury; GeneralLedger.LegalBook; Taxes.TaxesInvoice; Taxes.TaxesInvoiceDetail; Taxes.TaxesLiquidationConcept; Treasury.AgreementsRedemptionPoints; Common.Customer; Payments.AccountPayable; Payments.AccountPayableShares; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts_Output';
-- GO
