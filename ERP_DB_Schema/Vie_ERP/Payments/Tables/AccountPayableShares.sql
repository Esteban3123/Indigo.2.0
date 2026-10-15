CREATE TABLE [Payments].[AccountPayableShares] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable] INT             NOT NULL,
    [Share]            INT             NOT NULL,
    [DateExpires]      DATETIME        NOT NULL,
    [InitialValue]     DECIMAL (18, 2) NOT NULL,
    [DebitValue]       DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_DebitValue] DEFAULT ((0.0)) NOT NULL,
    [CreditValue]      DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_CreditValue] DEFAULT ((0.0)) NOT NULL,
    [ValueTransfers]   DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_ValueTransfers] DEFAULT ((0.0)) NOT NULL,
    [PaymentValue]     DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_PaymentValue] DEFAULT ((0.0)) NOT NULL,
    [CrossingValue]    DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_CrossingValue] DEFAULT ((0.0)) NOT NULL,
    [Balance]          DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AccountPayableShares__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableShares_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableShares] NOCHECK CONSTRAINT [FK_AccountPayableShares_AccountPayable];


GO
CREATE NONCLUSTERED INDEX [IX_AccountPayableShares__IdAccountPayable__Balance__INC__CreditValue__DateExpires__DebitValue__Id__InitialValue__PaymentValue__S]
    ON [Payments].[AccountPayableShares]([IdAccountPayable] ASC, [Balance] ASC)
    INCLUDE([CreditValue], [DateExpires], [DebitValue], [Id], [InitialValue], [PaymentValue], [Share], [ValueTransfers]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se ha afectado a través de los cruce de cuentas', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Abonos aplicados a la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'PaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Traslados aplicados a la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'ValueTransfers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Créditos aplicados a la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Débitos aplicados a la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Vencimiento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'DateExpires';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de esta cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'Share';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta por pagar asociada a la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableShares', @level2type = N'COLUMN', @level2name = N'Id';

