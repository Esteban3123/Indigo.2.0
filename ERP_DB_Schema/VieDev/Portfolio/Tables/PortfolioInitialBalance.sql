CREATE TABLE [Portfolio].[PortfolioInitialBalance] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20)  NOT NULL,
    [OperatingUnitId]   INT           NOT NULL,
    [DocumentDate]      DATETIME      NOT NULL,
    [Observations]      VARCHAR (200) NULL,
    [Import]            BIT           CONSTRAINT [DF_PortfolioInitialBalance_Import] DEFAULT ((0)) NOT NULL,
    [AllBudgetAssigned] BIT           CONSTRAINT [DF_PortfolioInitialBalance_AllBudgetAssigned] DEFAULT ((0)) NOT NULL,
    [Status]            TINYINT       NOT NULL,
    [CreationUser]      VARCHAR (20)  NOT NULL,
    [CreationDate]      DATETIME      NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [ConfirmationUser]  VARCHAR (20)  NULL,
    [ConfirmationDate]  DATETIME      NULL,
    [AnnulmentUser]     VARCHAR (20)  NULL,
    [AnnulmentDate]     DATETIME      NULL,
    [TimeStamp]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_PortfolioInitialBalance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioInitialBalance_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalance] NOCHECK CONSTRAINT [FK_PortfolioInitialBalance_OperatingUnit];

