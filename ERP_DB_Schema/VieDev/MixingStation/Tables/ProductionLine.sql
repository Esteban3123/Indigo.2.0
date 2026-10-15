CREATE TABLE [MixingStation].[ProductionLine] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]              VARCHAR (40) NOT NULL,
    [Code]              VARCHAR (20) NOT NULL,
    [State]             BIT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    [TimeStamp]         ROWVERSION   NOT NULL,
    [Id_FunctionalUnit] INT          NULL,
    [Work24Hours]       BIT          NULL,
    CONSTRAINT [PK_ProductionLine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionLine_FunctionalUnit] FOREIGN KEY ([Id_FunctionalUnit]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [UQ_Constraint] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [MixingStation].[ProductionLine] NOCHECK CONSTRAINT [FK_ProductionLine_FunctionalUnit];


GO
CREATE NONCLUSTERED INDEX [IDX_ProductionLine_Code_Name]
    ON [MixingStation].[ProductionLine]([Code] ASC, [Name] ASC);

