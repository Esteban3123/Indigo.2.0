CREATE TABLE [Portfolio].[PortfolioAdvance] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)    NOT NULL,
    [CashReceiptId]           INT             NULL,
    [CashReceiptDetailId]     INT             NULL,
    [AdmissionNumber]         CHAR (10)       NULL,
    [ThirdPartyId]            INT             NOT NULL,
    [MainAccountId]           INT             NOT NULL,
    [CostCenterId]            INT             NULL,
    [DocumentDate]            DATETIME        NOT NULL,
    [CustomerId]              INT             NULL,
    [SellerId]                INT             NULL,
    [Value]                   NUMERIC (18, 2) NOT NULL,
    [TransferValue]           NUMERIC (18, 2) NOT NULL,
    [DebitValue]              DECIMAL (18, 2) NOT NULL,
    [CreditValue]             DECIMAL (18, 2) NOT NULL,
    [DistributionValue]       DECIMAL (18, 2) CONSTRAINT [DF_PortfolioAdvance_DistributionValue] DEFAULT ((0)) NOT NULL,
    [Balance]                 DECIMAL (18, 2) NOT NULL,
    [Observations]            VARCHAR (300)   NULL,
    [OpeningBalance]          BIT             CONSTRAINT [DF_PortfolioAdvance_OpeningBalance] DEFAULT ((0)) NOT NULL,
    [Status]                  TINYINT         NOT NULL,
    [CreationUser]            VARCHAR (20)    NOT NULL,
    [CreationDate]            DATETIME        NOT NULL,
    [ModificationUser]        VARCHAR (20)    NULL,
    [ModificationDate]        DATETIME        NULL,
    [ConfirmationUser]        VARCHAR (20)    NULL,
    [ConfirmationDate]        DATETIME        NULL,
    [AnnulmentUser]           VARCHAR (20)    NULL,
    [AnnulmentDate]           DATETIME        NULL,
    [TimeStamp]               ROWVERSION      NOT NULL,
    [CurrencyId]              INT             NULL,
    [TRMValue]                NUMERIC (20, 5) CONSTRAINT [DF__Portfolio__TRMVa__21282E8F] DEFAULT ((1)) NOT NULL,
    [ValueInCurrencyHeader]   NUMERIC (20, 5) CONSTRAINT [DF__Portfolio__Value__0362C17E] DEFAULT ((0)) NOT NULL,
    [ThirdPartyBeneficiaryId] INT             NULL,
    CONSTRAINT [PK_PortfolioAdvance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioAdvance_CashReceiptDetails] FOREIGN KEY ([CashReceiptDetailId]) REFERENCES [Treasury].[CashReceiptDetails] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_CashReceipts] FOREIGN KEY ([CashReceiptId]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_Seller] FOREIGN KEY ([SellerId]) REFERENCES [Common].[Seller] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_CashReceiptDetails];


GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_CashReceipts];


GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_CostCenter];


GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_Currency];


GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_Customer];


GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_Seller];


GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_PortfolioAdvance__CashReceiptDetailId]
    ON [Portfolio].[PortfolioAdvance]([CashReceiptDetailId] ASC);


GO
-- =============================================
-- Author:		<Giovanny Plazas>
-- Create date: <20/06/2024>
-- Description:	<Trigger para insertar la tasa del anticipo cuando se confirme>
-- =============================================

CREATE TRIGGER [Portfolio].[TG_PortfolioAdvanceRates]
ON [Portfolio].[PortfolioAdvance]
AFTER INSERT,UPDATE
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @_portfolioAdvanceId INT, @_currencyId INT , @_documentDate DATE;

	IF EXISTS(	SELECT 1 
				FROM INSERTED i
				left join Portfolio.PortfolioAdvanceExchangeRate paer with(NOLOCK) on i.Id=paer.PortfolioAdvanceId
				WHERE i.Status=2 and paer.Id is NULL)
	BEGIN
		/*-------------------INSERCION TABLA EXGANGED DE Advanced--------*/
		DECLARE @TablePortfolioAdvance TABLE (Id INT, CurrencyId INT, DocumentDate DATE);

		INSERT INTO @TablePortfolioAdvance (Id, CurrencyId,DocumentDate)
		SELECT i.Id, i.CurrencyId , i.DocumentDate
		FROM [INSERTED] i
		left join Portfolio.PortfolioAdvanceExchangeRate paer with(NOLOCK) on i.Id=paer.PortfolioAdvanceId
		WHERE i.Status=2 and paer.Id is NULL

		DECLARE @rowcount INT;
		SET @rowcount = (select count(*) from @TablePortfolioAdvance );;

		WHILE (@rowcount > 0)
		BEGIN
			SELECT TOP 1 @_portfolioAdvanceId = Id, @_currencyId = CurrencyId , @_documentDate = DocumentDate
			FROM @TablePortfolioAdvance;

			DECLARE @StateResult INT, @MessageOutput VARCHAR(100);

			EXEC [Common].[SP_InsertIntoExchangeRateWithDate] 
				'PortfolioAdvance', @_portfolioAdvanceId, @_currencyId, @_documentDate,
				@StateResult=@StateResult OUTPUT, @MessageOutput = @MessageOutput OUTPUT;

			IF ISNULL(@StateResult, 999) = 999
			BEGIN
				-- Lanzar la excepción con código de error y mensaje
				THROW 51000, 'Error al guardar el TRM del documento', 1
				BREAK; -- Exit the loop on error
			END

			DELETE FROM @TablePortfolioAdvance where Id=@_portfolioAdvanceId;
			SET @rowcount = (select count(*) from @TablePortfolioAdvance );
		END;

		/*--------------------------------------------------------------------*/
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica el tercero beneficiario resultante del recibo de caja, este dato depende del tipo de concepto de recibo de caja seleccionado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyBeneficiaryId';

