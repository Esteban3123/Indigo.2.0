CREATE TABLE [Budget].[AnnualizedCashFlowTransfer] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [DocumentSource]      TINYINT       NOT NULL,
    [BudgetaryValidityId] INT           NOT NULL,
    [Document]            VARCHAR (100) NOT NULL,
    [DocumentDate]        DATETIME      NOT NULL,
    [Observations]        VARCHAR (500) NOT NULL,
    [PACType]             TINYINT       NOT NULL,
    [Status]              TINYINT       NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_AnnualizedCashFlowTransfer_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_AnnualizedCashFlowTransfer_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [ConfirmationUser]    VARCHAR (20)  NULL,
    [ConfirmationDate]    DATETIME      NULL,
    [AnnulmentUser]       VARCHAR (20)  NULL,
    [AnnulmentDate]       DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    CONSTRAINT [PK_AnnualizedCashFlowTransfer__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualizedCashFlowTransfer_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);


GO
ALTER TABLE [Budget].[AnnualizedCashFlowTransfer] NOCHECK CONSTRAINT [FK_AnnualizedCashFlowTransfer_BudgetaryValidity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransfer', @level2type = N'COLUMN', @level2name = N'TimeStamp';

