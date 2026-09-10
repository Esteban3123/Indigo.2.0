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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar destino en el registro contable; movimiento de cartera destino donde se transfieren los derechos económicos del convenio. FK a Portfolio.AccountReceivableAccounting (INT).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'DestinationAccountReceivableAccountingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Movimiento de Cuenta por Cobrar Destino', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'DestinationAccountReceivableAccountingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'DestinationAccountReceivableAccountingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar origen en el registro contable; movimiento de cartera origen desde el cual se transfieren los derechos económicos. FK a Portfolio.AccountReceivableAccounting (INT).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'SourceAccountReceivableAccountingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Movimiento de Cuenta por Cobrar Origen', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'SourceAccountReceivableAccountingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'SourceAccountReceivableAccountingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera del convenio o contrato (acuerdo); referencia al contrato principal que autoriza la transferencia de cuentas por cobrar. FK a Payroll.AgreementsC (INT).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'AgreementsCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'AgreementsCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'AgreementsCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la transacción de transferencia de cuentas por cobrar entre acuerdos. Clave primaria de la tabla (INT).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las transferencias o traslados de cuentas por cobrar entre cuentas contables dentro de un convenio o acuerdo de nómina, indicando la cuenta contable de origen y la cuenta contable de destino.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsAccountReceivableTransfer';
