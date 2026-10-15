CREATE TABLE [Portfolio].[AccountReceivablePromptPayment] (
    [Id]                  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId] INT            NOT NULL,
    [DeadLine]            DATE           NOT NULL,
    [PercentageDiscount]  NUMERIC (5, 2) NOT NULL,
    [MainAccountId]       INT            NOT NULL,
    CONSTRAINT [PK_AccountReceivablePromptPayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivablePromptPayment_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivablePromptPayment_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivablePromptPayment] NOCHECK CONSTRAINT [FK_AccountReceivablePromptPayment_AccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivablePromptPayment] NOCHECK CONSTRAINT [FK_AccountReceivablePromptPayment_MainAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta contable del descuento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de Descuento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'PercentageDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha limite', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'DeadLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'Id';

