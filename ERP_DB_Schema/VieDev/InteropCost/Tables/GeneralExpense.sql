CREATE TABLE [InteropCost].[GeneralExpense] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                     VARCHAR (20)  NOT NULL,
    [Name]                     VARCHAR (100) NOT NULL,
    [GeneralExpenseCategoryId] INT           NOT NULL,
    [ElementCostType]          TINYINT       CONSTRAINT [DF_GeneralExpense_ElementCostType] DEFAULT ((5)) NOT NULL,
    [ExpenditureType]          TINYINT       NOT NULL,
    [Status]                   BIT           NOT NULL,
    [CreationUser]             VARCHAR (20)  NOT NULL,
    [CreationDate]             DATETIME      NOT NULL,
    [ModificationUser]         VARCHAR (20)  NULL,
    [ModificationDate]         DATETIME      NULL,
    [ConfirmUser]              VARCHAR (20)  NULL,
    [ConfirmDate]              DATETIME      NULL,
    [TimeStamp]                ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GeneralExpense] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GeneralExpense_GeneralExpenseCategory] FOREIGN KEY ([GeneralExpenseCategoryId]) REFERENCES [InteropCost].[GeneralExpenseCategory] ([Id])
);


GO
ALTER TABLE [InteropCost].[GeneralExpense] NOCHECK CONSTRAINT [FK_GeneralExpense_GeneralExpenseCategory];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del gasto general   1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'GeneralExpense', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Clase del Costo  1 - Fijo  2 - Variable', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'GeneralExpense', @level2type = N'COLUMN', @level2name = N'ExpenditureType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo del elemento de Costo  1 - Mano de obra Directa  2 - Mano de obra Indirecta  3 - Materiales Directos  4 - Materiales Indirectos  5 - Otros Gastos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'GeneralExpense', @level2type = N'COLUMN', @level2name = N'ElementCostType';

