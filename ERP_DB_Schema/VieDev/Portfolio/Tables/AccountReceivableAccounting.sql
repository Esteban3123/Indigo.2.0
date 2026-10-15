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
CREATE TRIGGER [Portfolio].[ValidateNegativeValue]
   ON  [Portfolio].[AccountReceivableAccounting]
   AFTER UPDATE
AS 
BEGIN
	
	if (select count(*) from inserted where [Value] < 0) > 0 begin
		THROW 51000, 'Error: El movimiento que se va a realizar dejaria la cuenta en negativo', 1;  
	end
END