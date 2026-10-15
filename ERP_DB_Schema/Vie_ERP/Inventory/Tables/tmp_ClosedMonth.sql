CREATE TABLE [Inventory].[tmp_ClosedMonth] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [Year]                         INT             NOT NULL,
    [Month]                        INT             NOT NULL,
    [InventoryNoLegalizedBalance]  DECIMAL (18, 2) NOT NULL,
    [InventoryLegalizedBalance]    DECIMAL (18, 2) NOT NULL,
    [AccountingNoLegalizedBalance] DECIMAL (18, 2) NOT NULL,
    [AccountingLegalizedBalance]   DECIMAL (18, 2) NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    CONSTRAINT [PK_Tmp_Inventory_ClosedMonth] PRIMARY KEY CLUSTERED ([Id] ASC)
);

