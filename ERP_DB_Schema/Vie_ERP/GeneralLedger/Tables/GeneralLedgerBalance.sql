CREATE TABLE [GeneralLedger].[GeneralLedgerBalance] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Month]         INT             NOT NULL,
    [Year]          INT             CONSTRAINT [DF_GeneralLedgerBalance_Year] DEFAULT ((2014)) NOT NULL,
    [IdMainAccount] INT             NOT NULL,
    [IdThirdParty]  INT             NULL,
    [IdCostCenter]  INT             NULL,
    [DebitValue]    DECIMAL (21, 5) CONSTRAINT [DF_AccountingBalance_DebitValue] DEFAULT ((0)) NOT NULL,
    [CreditValue]   DECIMAL (21, 5) CONSTRAINT [DF_AccountingBalance_CreditValue] DEFAULT ((0)) NOT NULL,
    [TimeStamp]     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_AccountingBalance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingBalance_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountingBalance_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountingBalance_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerBalance] NOCHECK CONSTRAINT [FK_AccountingBalance_CostCenter];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerBalance] NOCHECK CONSTRAINT [FK_AccountingBalance_MainAccount];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerBalance] NOCHECK CONSTRAINT [FK_AccountingBalance_ThirdParty];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_GeneralLedgerBalance__Month__IdMainAccount__IdThirdParty__IdCostCenter__Year]
    ON [GeneralLedger].[GeneralLedgerBalance]([Month] ASC, [IdMainAccount] ASC, [IdThirdParty] ASC, [IdCostCenter] ASC, [Year] ASC);


GO
-- Índice optimizado para consultas del SP_ReportAuxiliar que filtran por Year/Month
CREATE NONCLUSTERED INDEX [IX_GeneralLedgerBalance__Year__Month__IdMainAccount]
    ON [GeneralLedger].[GeneralLedgerBalance]([Year] ASC, [Month] ASC, [IdMainAccount] ASC)
    INCLUDE ([IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor credito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor debito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año que se esta acumulando', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerBalance', @level2type = N'COLUMN', @level2name = N'Id';

