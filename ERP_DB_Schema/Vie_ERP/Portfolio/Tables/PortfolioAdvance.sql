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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor convertido a la moneda de la cabecera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TRM con el que se calculo el valor de la cxc', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Moneda de la nota deb/cred', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si es Saldo Inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'OpeningBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Balance del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los valores que se han distribuido del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DistributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que acredita en el anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se a debitado al anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Traslado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del vendedor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'SellerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cliente asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero del ingreso del paciente asociado al anticipo, esto se saca de la tabla ADINGRESO de Crystal', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del detalle del recibo de caja', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de recibo de caja', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CashReceiptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Id';

