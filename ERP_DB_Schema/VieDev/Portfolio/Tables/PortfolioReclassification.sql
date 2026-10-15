CREATE TABLE [Portfolio].[PortfolioReclassification] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)    NOT NULL,
    [DocumentType]        TINYINT         NOT NULL,
    [AccountReceivableId] INT             NOT NULL,
    [SourceAccountId]     INT             NOT NULL,
    [TargetAccountId]     INT             NOT NULL,
    [Value]               NUMERIC (18, 2) NOT NULL,
    [Status]              TINYINT         NOT NULL,
    [CreationUser]        VARCHAR (20)    NOT NULL,
    [CreationDate]        DATETIME        NOT NULL,
    [ModificationUser]    VARCHAR (20)    NULL,
    [ModificationDate]    DATETIME        NULL,
    [ConfirmationUser]    VARCHAR (20)    NULL,
    [ConfirmationDate]    DATETIME        NULL,
    [AnnulmentUser]       VARCHAR (20)    NULL,
    [AnnulmentDate]       DATETIME        NULL,
    [TimeStamp]           ROWVERSION      NOT NULL,
    [DocumentDate]        DATETIME        NULL,
    [EntityId]            INT             NULL,
    [EntityCode]          VARCHAR (20)    NULL,
    [EntityName]          VARCHAR (250)   NULL,
    CONSTRAINT [PK_PortfolioReclassification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioReclassification_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioReclassification_MainAccounts] FOREIGN KEY ([SourceAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioReclassification_MainAccounts1] FOREIGN KEY ([TargetAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioReclassification] NOCHECK CONSTRAINT [FK_PortfolioReclassification_AccountReceivable];


GO
ALTER TABLE [Portfolio].[PortfolioReclassification] NOCHECK CONSTRAINT [FK_PortfolioReclassification_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioReclassification] NOCHECK CONSTRAINT [FK_PortfolioReclassification_MainAccounts1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de la reclasificacion  1 - Radicacion - Publico y privada  2 - Devoluciones - Privada  3 - Recepcion de Glosas Subsanable - Privada  4 - Conciliaciones - Privada  5 - Reiteracion - Privada  6 - Cuentas de Dificil Recaudo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'DocumentType';

