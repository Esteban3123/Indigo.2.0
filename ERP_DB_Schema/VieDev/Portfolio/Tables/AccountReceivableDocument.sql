CREATE TABLE [Portfolio].[AccountReceivableDocument] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [OperatingUnitId]     INT           NOT NULL,
    [DocumentDate]        DATETIME      NOT NULL,
    [CustomerId]          INT           NOT NULL,
    [MainAccountId]       INT           NOT NULL,
    [CostCenterId]        INT           NULL,
    [InvoiceNumber]       VARCHAR (20)  NOT NULL,
    [Term]                INT           NOT NULL,
    [ExpiredDate]         DATETIME      NOT NULL,
    [Share]               INT           NOT NULL,
    [Value]               NUMERIC (18)  NOT NULL,
    [DebitValue]          NUMERIC (18)  NOT NULL,
    [CreditValue]         NUMERIC (18)  NOT NULL,
    [Observation]         VARCHAR (300) NULL,
    [AccountReceivableId] INT           NULL,
    [Status]              TINYINT       NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [ConfirmationUser]    VARCHAR (20)  NULL,
    [ConfirmationDate]    DATETIME      NULL,
    [AnnulmentUser]       VARCHAR (20)  NULL,
    [AnnulmentDate]       DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [CurrencyId]          INT           NULL,
    CONSTRAINT [PK_AccountReceivableDocument__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableDocument_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivableDocument] NOCHECK CONSTRAINT [FK_AccountReceivableDocument_AccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivableDocument] NOCHECK CONSTRAINT [FK_AccountReceivableDocument_CostCenter];


GO
ALTER TABLE [Portfolio].[AccountReceivableDocument] NOCHECK CONSTRAINT [FK_AccountReceivableDocument_Currency];


GO
ALTER TABLE [Portfolio].[AccountReceivableDocument] NOCHECK CONSTRAINT [FK_AccountReceivableDocument_Customer];


GO
ALTER TABLE [Portfolio].[AccountReceivableDocument] NOCHECK CONSTRAINT [FK_AccountReceivableDocument_MainAccounts];


GO
ALTER TABLE [Portfolio].[AccountReceivableDocument] NOCHECK CONSTRAINT [FK_AccountReceivableDocument_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del centro de costos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CostCenterId';

