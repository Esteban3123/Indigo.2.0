CREATE TABLE [Common].[EconomicActivity] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20)  NOT NULL,
    [Name]               VARCHAR (200) NOT NULL,
    [Status]             BIT           NOT NULL,
    [CreationUser]       VARCHAR (20)  NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    [IsIncomeGenerating] BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EconomicActivity] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si es generadora de Ingreso Si = 1 | No = 0 ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'EconomicActivity', @level2type = N'COLUMN', @level2name = N'IsIncomeGenerating';

