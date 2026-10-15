CREATE TABLE [Portfolio].[AccountReceivableProvision] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId] INT          NOT NULL,
    [ProvisionDate]       DATE         NOT NULL,
    [Value]               NUMERIC (18) NOT NULL,
    [Balance]             NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_AccountReceivableProvision] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableProvision_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivableProvision] NOCHECK CONSTRAINT [FK_AccountReceivableProvision_AccountReceivable];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo de la provision', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableProvision', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la provision', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableProvision', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la provision', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableProvision', @level2type = N'COLUMN', @level2name = N'ProvisionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableProvision', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableProvision', @level2type = N'COLUMN', @level2name = N'Id';

