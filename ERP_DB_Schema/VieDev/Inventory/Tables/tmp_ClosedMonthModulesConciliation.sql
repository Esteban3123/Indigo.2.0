CREATE TABLE [Inventory].[tmp_ClosedMonthModulesConciliation] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [Year]                  INT             NOT NULL,
    [Month]                 INT             NOT NULL,
    [EntityName]            VARCHAR (250)   NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [TotalDebitAccounting]  DECIMAL (20, 2) NOT NULL,
    [TotalCreditAccounting] DECIMAL (20, 2) NOT NULL,
    [TotalDebitInventory]   DECIMAL (20, 2) NOT NULL,
    [TotalCreditInventory]  DECIMAL (20, 2) NOT NULL,
    CONSTRAINT [PK_Tmp_ClosedMonthModulesConciliation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

