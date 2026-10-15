CREATE TABLE [Portfolio].[PortfolioTransfer] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20)  NOT NULL,
    [DocumentDate]       DATETIME      NOT NULL,
    [CustomerId]         INT           NULL,
    [ThirdPartyId]       INT           NOT NULL,
    [PortfolioAdvanceId] INT           NOT NULL,
    [TransferType]       TINYINT       NOT NULL,
    [MainAccountId]      INT           NOT NULL,
    [CostCenterId]       INT           NULL,
    [Observations]       VARCHAR (300) NULL,
    [OperatingUnitId]    INT           NOT NULL,
    [Status]             TINYINT       NOT NULL,
    [CreationUser]       VARCHAR (20)  NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    [ConfirmationUser]   VARCHAR (20)  NULL,
    [ConfirmationDate]   DATETIME      NULL,
    [AnnulmentUser]      VARCHAR (20)  NULL,
    [AnnulmentDate]      DATETIME      NULL,
    [ReversalUser]       VARCHAR (20)  NULL,
    [RecersalDate]       DATETIME      NULL,
    [TimeStamp]          ROWVERSION    NOT NULL,
    CONSTRAINT [PK_PortfolioTransfer__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioTransfer_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioTransfer_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_PortfolioTransfer_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioTransfer_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PortfolioTransfer_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_PortfolioTransfer_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioTransfer] NOCHECK CONSTRAINT [FK_PortfolioTransfer_CostCenter];


GO
ALTER TABLE [Portfolio].[PortfolioTransfer] NOCHECK CONSTRAINT [FK_PortfolioTransfer_Customer];


GO
ALTER TABLE [Portfolio].[PortfolioTransfer] NOCHECK CONSTRAINT [FK_PortfolioTransfer_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioTransfer] NOCHECK CONSTRAINT [FK_PortfolioTransfer_OperatingUnit];


GO
ALTER TABLE [Portfolio].[PortfolioTransfer] NOCHECK CONSTRAINT [FK_PortfolioTransfer_PortfolioAdvance];


GO
ALTER TABLE [Portfolio].[PortfolioTransfer] NOCHECK CONSTRAINT [FK_PortfolioTransfer_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de traslado  1 - Traslado al mismo Cliente  2 - Traslado a Diferente Cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransfer', @level2type = N'COLUMN', @level2name = N'TransferType';

