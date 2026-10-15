CREATE TABLE [InteropCost].[DistributionDirectCost] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [GeneralExpenseId] INT           NOT NULL,
    [Description]      VARCHAR (300) NOT NULL,
    [Year]             INT           NOT NULL,
    [Month]            INT           NOT NULL,
    [Value]            NUMERIC (18)  NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [ConfirmUser]      VARCHAR (20)  NULL,
    [ConfirmDate]      DATETIME      NULL,
    [AnnulmentUser]    VARCHAR (20)  NULL,
    [AnnulmentDate]    DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [Observation]      VARCHAR (500) CONSTRAINT [DF_DistributionDirectCost_Observation] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_DistributionDirectCost] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionDirectCost_GeneralExpense] FOREIGN KEY ([GeneralExpenseId]) REFERENCES [InteropCost].[GeneralExpense] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionDirectCost] NOCHECK CONSTRAINT [FK_DistributionDirectCost_GeneralExpense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad   1 - Sin Confirmar  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Status';

