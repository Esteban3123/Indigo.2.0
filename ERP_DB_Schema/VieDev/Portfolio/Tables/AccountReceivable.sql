CREATE TABLE [Portfolio].[AccountReceivable] (
    [Id]                                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                                         VARCHAR (20)    NOT NULL,
    [OperatingUnitId]                              INT             NOT NULL,
    [AccountReceivableType]                        TINYINT         NOT NULL,
    [ThirdPartyId]                                 INT             NOT NULL,
    [CustomerId]                                   INT             NULL,
    [SellerId]                                     INT             NULL,
    [InvoiceId]                                    INT             NULL,
    [InvoiceNumber]                                VARCHAR (20)    NOT NULL,
    [AccountReceivableDate]                        DATETIME        NOT NULL,
    [Term]                                         INT             NOT NULL,
    [ExpiredDate]                                  DATETIME        NOT NULL,
    [Observations]                                 VARCHAR (300)   NOT NULL,
    [PortfolioStatus]                              TINYINT         NOT NULL,
    [OpeningBalance]                               BIT             NOT NULL,
    [RecognitionId]                                INT             NULL,
    [PaymentAgreement]                             BIT             NOT NULL,
    [RegistrationAdjusted]                         BIT             NOT NULL,
    [MainAccountWithoutFilingId]                   INT             NULL,
    [NumberShares]                                 INT             NOT NULL,
    [Value]                                        NUMERIC (18, 2) NOT NULL,
    [Balance]                                      NUMERIC (18, 2) NOT NULL,
    [DeteriorationBalance]                         DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_DeteriorationBalance] DEFAULT ((0)) NOT NULL,
    [DeteriorationPayment]                         DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_DeteriorationPayment] DEFAULT ((0)) NOT NULL,
    [ProvisionBalance]                             DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_ProvisionBalance] DEFAULT ((0)) NOT NULL,
    [ProvisionPayment]                             DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_ProvisionPayment] DEFAULT ((0)) NOT NULL,
    [Status]                                       TINYINT         NOT NULL,
    [CostCenterId]                                 INT             NULL,
    [InvoiceCategoryId]                            INT             NULL,
    [AccountWithoutRadicateId]                     INT             CONSTRAINT [DF_AccountReceivable_AccountWithoutRadicateId] DEFAULT ((203)) NULL,
    [AccountRadicateId]                            INT             CONSTRAINT [DF_AccountReceivable_AccountRadicateId] DEFAULT ((203)) NULL,
    [AccountObjectionRemediedId]                   INT             CONSTRAINT [DF_AccountReceivable_AccountObjectionRemediedId] DEFAULT ((203)) NULL,
    [AccountConciliationId]                        INT             NULL,
    [AccountLegalCollectionId]                     INT             NULL,
    [AccountHardCollectionId]                      INT             NULL,
    [AccountDebtorOrder]                           INT             NULL,
    [AccountCreditorOrder]                         INT             NULL,
    [CreditProvisionAccountId]                     INT             NULL,
    [DebitProvisionAccountId]                      INT             NULL,
    [DebitAccountDeteriorationId]                  INT             NULL,
    [CreditAccountDeteriorationId]                 INT             NULL,
    [ReversalAccountDeteriorationId]               INT             NULL,
    [PreviousPeriodReversalAccountDeteriorationId] INT             NULL,
    [AffectBudget]                                 BIT             CONSTRAINT [DF_AccountReceivable_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BudgetId]                                     INT             NULL,
    [CreationUser]                                 VARCHAR (20)    NOT NULL,
    [CreationDate]                                 DATETIME        NOT NULL,
    [ModificationUser]                             VARCHAR (20)    NULL,
    [ModificationDate]                             DATETIME        NULL,
    [ConfirmationUser]                             VARCHAR (20)    NULL,
    [ConfirmationDate]                             DATETIME        NULL,
    [AnnulmentUser]                                VARCHAR (20)    NULL,
    [AnnulmentDate]                                DATETIME        NULL,
    [TimeStamp]                                    ROWVERSION      NOT NULL,
    [DeteriorationBalanceCurrentYear]              DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_DeteriorationBalanceCurrentYear] DEFAULT ((0)) NOT NULL,
    [DeteriorationBalancePreviousYear]             DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_DeteriorationBalancePreviousYear] DEFAULT ((0)) NOT NULL,
    [CurrentDeteriorationYear]                     INT             NULL,
    [CareGroupId]                                  INT             NULL,
    [CurrencyId]                                   INT             NULL,
    [TRMValue]                                     NUMERIC (20, 5) CONSTRAINT [DF__AccountRe__TRMVa__1F3FE61D] DEFAULT ((1)) NOT NULL,
    [DateAccountBecomeZero]                        DATE            NULL,
    CONSTRAINT [PK_AccountReceivable__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_AccountReceivable] CHECK ([DeteriorationBalance]<=abs([Balance])),
    CONSTRAINT [CK_AccountReceivable_1] CHECK ([ProvisionBalance]<=abs([Balance])),
    CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalance] CHECK ([DeteriorationBalance]=([DeteriorationBalanceCurrentYear]+[DeteriorationBalancePreviousYear])),
    CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalancesNotNegatives] CHECK ([DeteriorationBalance]>=(0) AND [DeteriorationBalanceCurrentYear]>=(0) AND [DeteriorationBalancePreviousYear]>=(0)),
    CONSTRAINT [FK_AccountReceivable_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_AccountReceivable_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_AccountReceivable_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_AccountReceivable_InvoiceCategories] FOREIGN KEY ([InvoiceCategoryId]) REFERENCES [Billing].[InvoiceCategories] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts] FOREIGN KEY ([AccountWithoutRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts1] FOREIGN KEY ([MainAccountWithoutFilingId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts10] FOREIGN KEY ([DebitProvisionAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts2] FOREIGN KEY ([AccountRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts3] FOREIGN KEY ([AccountObjectionRemediedId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts4] FOREIGN KEY ([AccountConciliationId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts5] FOREIGN KEY ([AccountLegalCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts6] FOREIGN KEY ([AccountDebtorOrder]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts7] FOREIGN KEY ([AccountCreditorOrder]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts8] FOREIGN KEY ([AccountHardCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts9] FOREIGN KEY ([CreditProvisionAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Recognition] FOREIGN KEY ([RecognitionId]) REFERENCES [Budget].[Recognition] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Seller] FOREIGN KEY ([SellerId]) REFERENCES [Common].[Seller] ([Id]),
    CONSTRAINT [FK_AccountReceivable_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable_1];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalance];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalancesNotNegatives];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_Budget];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_CareGroup];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_CostCenter];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_Currency];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_Customer];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_Invoice];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_InvoiceCategories];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts1];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts10];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts2];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts3];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts4];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts5];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts6];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts7];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts8];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_MainAccounts9];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_OperatingUnit];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_Recognition];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_Seller];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_AccountReceivable_AccountReceivableDate]
    ON [Portfolio].[AccountReceivable]([AccountReceivableDate] ASC)
    INCLUDE([AccountReceivableType], [ThirdPartyId], [InvoiceId], [InvoiceNumber], [Term], [PortfolioStatus], [OpeningBalance], [NumberShares], [Value], [InvoiceCategoryId], [AccountWithoutRadicateId], [AccountRadicateId], [AccountObjectionRemediedId], [AccountLegalCollectionId], [AccountHardCollectionId], [CareGroupId], [CurrencyId], [DateAccountBecomeZero]);


GO
-- =============================================
-- Author:		<Ruben Dario Scalante>
-- Create date: <16/12/2022>
-- Description:	<Trigger que se enfoca en detectar cuando haya una insercción en la tabla [AccountReceivable] 
--				 Esto con el fin de guardar en la tabla [AccountReceivableExchangeRate] datos como:
--				 *El id de la cuenta por cobrar.
--				 *La moneda (CurrencyId) registrada en la tabla [TRM] - que es distinta a la moneda registrada en la cuenta por cobrar.
--               *El valor de la moneda (CurrencyId) que es distinta a la cuenta por cobrar.
--				 *El valor de la moneda oficial que se relaciona con la cuenta por cobrar>
-- =============================================

CREATE TRIGGER [Portfolio].[TG_AccountReceivableRates]
ON [Portfolio].[AccountReceivable]
AFTER INSERT
AS
BEGIN

	SET NOCOUNT ON;

	/*
	Permite obtener el id de la cuenta cuenta por cobrar en el momento que se crea un registro, además,
	se obtiene la moneda (CurrencyId) con la cual se efectuó esa cuenta por cobrar
	*/
	DECLARE @accountReceivableId AS INT = (SELECT Id FROM INSERTED)
	DECLARE @accountReceivableCurrencyId AS INT = (SELECT CurrencyId FROM INSERTED)

	--Atributos para la insercción, relacionando la tabla [AccountReceivableExchangeRate]
	declare @actualCurrencyRate table (CurrencyFrom int, CurrencyTo int, [Value] numeric(20, 5), ValueReverse numeric(20, 5))
	DECLARE @currencyId AS INT

	DECLARE Cursor1 CURSOR LOCAL
		FOR SELECT Id FROM Common.Currency where Id <> @accountReceivableCurrencyId
	--Abrir el cursor
	OPEN Cursor1
	--Navegar
	FETCH NEXT FROM Cursor1 INTO @currencyId
		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			
			delete from @actualCurrencyRate
			--Busco la tasa de conversion de la moneda con la fecha actual
			
			if EXISTS(SELECT top 1 1 
						from INSERTED i
						join Billing.Invoice ie WITH(NOLOCK) on i.InvoiceId = ie.Id
						where ie.DocumentType not in (6,7) ) and EXISTS(	SELECT top 1 1 
																								from INSERTED i WITH(NOLOCK)
																								join Billing.SettingsBilling sb WITH(NOLOCK) 
																								on sb.IdOperatingUnit =i.OperatingUnitId and sb.HasCustomTRM = 1) BEGIN
					declare @OperativeUnitId INT
					set @OperativeUnitId =(SELECT OperatingUnitId  from INSERTED i WITH(NOLOCK))

					if (select count(*) from Billing.CustomTRM 
						where (cast(Common.GETDATE() as date) >= InitialMeasurementDate AND cast(Common.GETDATE() as date) <= FinalMeasurementDate )  
								and CurrencyId = @accountReceivableCurrencyId and OfficialCurrencyId = @currencyId and OperatingUnitId =@OperativeUnitId) > 0 begin
						
						insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
						select top 1 @accountReceivableCurrencyId, @currencyId, [Value], ValueOfficialToCurrency 
						from Billing.CustomTRM trm
						where (cast(Common.GETDATE() as date) >= InitialMeasurementDate AND cast(Common.GETDATE() as date) <= FinalMeasurementDate ) 
								and CurrencyId = @accountReceivableCurrencyId and OfficialCurrencyId = @currencyId and trm.OperatingUnitId = @OperativeUnitId
						order by trm.Id DESC
					
					end
					else if (select count(*) from Billing.CustomTRM 
							 where (cast(Common.GETDATE() as date) >= InitialMeasurementDate AND cast(Common.GETDATE() as date) <= FinalMeasurementDate )and OfficialCurrencyId = @accountReceivableCurrencyId and CurrencyId = @currencyId) > 0 begin
							
							insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
							select top 1 @accountReceivableCurrencyId, @currencyId, ValueOfficialToCurrency, [Value]
							from Billing.CustomTRM trm
							where (cast(Common.GETDATE() as date) >= InitialMeasurementDate AND cast(Common.GETDATE() as date) <= FinalMeasurementDate )
									and OfficialCurrencyId = @accountReceivableCurrencyId and CurrencyId = @currencyId and trm.OperatingUnitId = @OperativeUnitId
							order by trm.Id DESC
					end
					ELSE BEGIN
						GOTO Branch_One
					end				
			end
			else BEGIN
				Branch_One:

				if (select count(*) from Common.TRM where MeasurementDate = cast(Common.GETDATE() as date) and CurrencyId = @accountReceivableCurrencyId and OfficialCurrencyId = @currencyId) > 0 begin
				insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
				select @accountReceivableCurrencyId, @currencyId, [Value], ValueOfficialToCurrency 
				from Common.TRM where MeasurementDate = cast(Common.GETDATE() as date) and CurrencyId = @accountReceivableCurrencyId and OfficialCurrencyId = @currencyId
				end
				else if (select count(*) from Common.TRM where MeasurementDate = cast(Common.GETDATE() as date) and OfficialCurrencyId = @accountReceivableCurrencyId and CurrencyId = @currencyId) > 0 begin
					insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
					select @accountReceivableCurrencyId, @currencyId, ValueOfficialToCurrency, [Value]
					from Common.TRM where MeasurementDate = cast(Common.GETDATE() as date) and OfficialCurrencyId = @accountReceivableCurrencyId and CurrencyId = @currencyId
				end
			END
			
			INSERT INTO [Portfolio].[AccountReceivableExchangeRate]
           ([AccountReceivableId]
           ,[CurrencyId]
           ,[Value]
           ,[ValueReverse])
		   select @accountReceivableId, CurrencyTo, [Value], [ValueReverse] 
		   from @actualCurrencyRate

			FETCH NEXT FROM Cursor1 INTO @currencyId
		END
	--Cerrar el cursor
	CLOSE Cursor1
	--Liberar Memoria
	DEALLOCATE Cursor1

END
GO

CREATE TRIGGER Portfolio.AccountReceivable_DateBecomeZero 
   ON  Portfolio.AccountReceivable 
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if exists( select 1 from INSERTED where Balance =0 and DateAccountBecomeZero is null) BEGIN
			
			UPDATE ar set ar.DateAccountBecomeZero = common.GETDATE()
			from Portfolio.AccountReceivable ar WITH(NOLOCK)
			join INSERTED i on ar.Id=i.Id
			WHERE i.Balance =0 and i.DateAccountBecomeZero is null

	END

	if EXISTS (SELECT 1 from INSERTED where Balance >0 and DateAccountBecomeZero is not null) BEGIN
			
			UPDATE ar set ar.DateAccountBecomeZero = null
			from Portfolio.AccountReceivable ar WITH(NOLOCK)
			join INSERTED i on ar.Id=i.Id
			where i.Balance >0 and i.DateAccountBecomeZero is not null
	END

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha en la que el saldo de la cuenta por cobrar llego a 0.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DateAccountBecomeZero';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor deteriorado de la cuenta por cobrar.  Este valor tambien corresponde a la suma los campos DeteriorationBalancePreviousYear + DeteriorationBalanceCurrentYear', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de cartera   1 - Sin Radicar  2 - Radicada sin Confirmar  3 - Radicada Entidad  4 - Objetada o Glosada  7 - Certificada Parcial  8 - Certificada Total  14 - Devolución Factura  15 - Cuenta de Dificil Recaudo  16 - Cobro Jurídico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cuenta por cobrar   Facturación Básica = 1  Facturación Ley 100 = 2  Impuestos Industria y Comercio = 3  Pagarés = 4  Acuerdos de Pago = 5  Documento de Pago a Cuota Moderadora = 6  Factura de Producto = 7  Impuesto Predial = 8', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';

