CREATE TABLE [Portfolio].[AccountReceivableAccounting] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId] INT             NOT NULL,
    [MainAccountId]       INT             NOT NULL,
    [ThirdPartyId]        INT             NULL,
    [CostCenterId]        INT             NULL,
    [Value]               NUMERIC (18, 2) NOT NULL,
    [Balance]             NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_AccountReceivableAccounting__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableAccounting_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivableAccounting_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountReceivableAccounting_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivableAccounting_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivableAccounting] NOCHECK CONSTRAINT [FK_AccountReceivableAccounting_AccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivableAccounting] NOCHECK CONSTRAINT [FK_AccountReceivableAccounting_CostCenter];


GO
ALTER TABLE [Portfolio].[AccountReceivableAccounting] NOCHECK CONSTRAINT [FK_AccountReceivableAccounting_MainAccounts];


GO
ALTER TABLE [Portfolio].[AccountReceivableAccounting] NOCHECK CONSTRAINT [FK_AccountReceivableAccounting_ThirdParty];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccountReceivableAccounting__AccountReceivableId__MainAccountId__INC__Id]
    ON [Portfolio].[AccountReceivableAccounting]([AccountReceivableId] ASC, [MainAccountId] ASC)
    INCLUDE([Id]);


GO
CREATE TRIGGER [Portfolio].[ValidateNegativeValue]
   ON  [Portfolio].[AccountReceivableAccounting]
   AFTER UPDATE
AS 
BEGIN
	
	if (select count(*) from inserted where [Value] < 0) > 0 begin
		THROW 51000, 'Error: El movimiento que se va a realizar dejaria la cuenta en negativo', 1;  
	end
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo, depende si la cuenta contable maneja o no centro de costos, por eso puede ser null', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera para la cuenta de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la estructura contable para cuentas de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'Id';

