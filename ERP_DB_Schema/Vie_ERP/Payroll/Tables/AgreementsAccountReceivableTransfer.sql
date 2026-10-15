CREATE TABLE [Payroll].[AgreementsAccountReceivableTransfer] (
    [Id]                                       INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AgreementsCId]                            INT NOT NULL,
    [SourceAccountReceivableAccountingId]      INT NOT NULL,
    [DestinationAccountReceivableAccountingId] INT NOT NULL,
    CONSTRAINT [PK_AgreementsAccountReceivableTransfer__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AgreementsAccountReceivableTransfer_AgreementsC] FOREIGN KEY ([AgreementsCId]) REFERENCES [Payroll].[AgreementsC] ([Id]),
    CONSTRAINT [FK_AgreementsAccountReceivableTransfer_DestinationAccountReceivableAccounting] FOREIGN KEY ([DestinationAccountReceivableAccountingId]) REFERENCES [Portfolio].[AccountReceivableAccounting] ([Id]),
    CONSTRAINT [FK_AgreementsAccountReceivableTransfer_SourceAccountReceivableAccounting] FOREIGN KEY ([SourceAccountReceivableAccountingId]) REFERENCES [Portfolio].[AccountReceivableAccounting] ([Id])
);


GO
ALTER TABLE [Payroll].[AgreementsAccountReceivableTransfer] NOCHECK CONSTRAINT [FK_AgreementsAccountReceivableTransfer_AgreementsC];


GO
ALTER TABLE [Payroll].[AgreementsAccountReceivableTransfer] NOCHECK CONSTRAINT [FK_AgreementsAccountReceivableTransfer_DestinationAccountReceivableAccounting];


GO
ALTER TABLE [Payroll].[AgreementsAccountReceivableTransfer] NOCHECK CONSTRAINT [FK_AgreementsAccountReceivableTransfer_SourceAccountReceivableAccounting];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Movimiento de Cuenta por Cobrar Destino', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'DestinationAccountReceivableAccountingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Movimiento de Cuenta por Cobrar Origen', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'SourceAccountReceivableAccountingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'AgreementsCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'Id';

