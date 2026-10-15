CREATE TABLE [Payroll].[TradeUnion] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdThirdParty]     INT           NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [Description]      VARCHAR (MAX) NULL,
    [PayrollConceptId] INT           NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_TradeUnion__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TradeUnion_Concept] FOREIGN KEY ([PayrollConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_TradeUnion_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payroll].[TradeUnion] NOCHECK CONSTRAINT [FK_TradeUnion_Concept];


GO
ALTER TABLE [Payroll].[TradeUnion] NOCHECK CONSTRAINT [FK_TradeUnion_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del sindicato  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'Status';

