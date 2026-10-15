CREATE TABLE [Cost].[CostGeneralExpense] (
    [Id]                            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                          VARCHAR (20)  NOT NULL,
    [Name]                          VARCHAR (100) NOT NULL,
    [CostGeneralExpenseCategoryId]  INT           NOT NULL,
    [ElementCostType]               TINYINT       CONSTRAINT [DF_CostGeneralExpense_ElementCostType] DEFAULT ((5)) NOT NULL,
    [ExpenditureType]               TINYINT       NOT NULL,
    [Status]                        BIT           NOT NULL,
    [CreationUser]                  VARCHAR (20)  NOT NULL,
    [CreationDate]                  DATETIME      NOT NULL,
    [ModificationUser]              VARCHAR (20)  NULL,
    [ModificationDate]              DATETIME      NULL,
    [ConfirmUser]                   VARCHAR (20)  NULL,
    [ConfirmDate]                   DATETIME      NULL,
    [TimeStamp]                     ROWVERSION    NOT NULL,
    [DistributionType]              TINYINT       CONSTRAINT [DF__CostGener__Distr__24056691] DEFAULT ((1)) NOT NULL,
    [GenerateAccountPayable]        BIT           CONSTRAINT [DF__CostGener__Gener__11728061] DEFAULT ((1)) NULL,
    [JournalVoucherTypesId]         INT           NULL,
    [ReversalJournalVoucherTypesId] INT           NULL,
    CONSTRAINT [PK_CostGeneralExpense] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostGeneralExpense_CostGeneralExpenseCategory] FOREIGN KEY ([CostGeneralExpenseCategoryId]) REFERENCES [Cost].[CostGeneralExpenseCategory] ([Id]),
    CONSTRAINT [FK_CostGeneralExpense_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypesId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CostGeneralExpense_ReversalJournalVoucherTypes] FOREIGN KEY ([ReversalJournalVoucherTypesId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);


GO
ALTER TABLE [Cost].[CostGeneralExpense] NOCHECK CONSTRAINT [FK_CostGeneralExpense_CostGeneralExpenseCategory];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Tipo de comprobante contable para su reversión', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'ReversalJournalVoucherTypesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Tipo de comprobante contable para Distribución Mano de Obra Directa', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Si se genera cuenta por pagar:
0 - No
1 - Sí', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'GenerateAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el tipo de Distribucion
1 - Distribución estándar
2 - Distribución Mano de obra
3 - Distribución de gastos generales
4 - Distribución productos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se confirma el gasto', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmo el gasto', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'ConfirmUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del gasto general
1 - Activo
0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la Clase del Costo
1 - Fijo
2 - Variable', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'ExpenditureType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el tipo del elemento de Costo
1 - Mano de obra Directa
2 - Mano de obra Indirecta
3 - Materiales Directos
4 - Materiales Indirectos
5 - Otros Gastos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'ElementCostType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la categoría de elemento del costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'CostGeneralExpenseCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del gasto general', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Gasto General', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la tabla de gastos generales', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de gastos generales de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostGeneralExpense';

