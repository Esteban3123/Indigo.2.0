CREATE TABLE [Budget].[PrivateBudgetItemsStructure] (
    [Id]                       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParentId]                 INT            NULL,
    [Code]                     VARCHAR (20)   NOT NULL,
    [Description]              VARCHAR (100)  NOT NULL,
    [Type]                     TINYINT        NOT NULL,
    [UnBudgetValues]           TINYINT        NULL,
    [PurchaseOrderControl]     BIT            NULL,
    [EmailNotification]        VARCHAR (50)   NULL,
    [Periodicity]              TINYINT        NULL,
    [ExecutionAlertPercentage] NUMERIC (5, 2) NULL,
    [AllowExceedBudget]        BIT            NULL,
    [ExceedBudgetPercentage]   NUMERIC (5, 2) NULL,
    [BudgetControl]            TINYINT        NULL,
    [CreationUser]             VARCHAR (20)   CONSTRAINT [DF_PrivateBudgetItemsStructure_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]             DATETIME       CONSTRAINT [DF_PrivateBudgetItemsStructure_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]         VARCHAR (20)   NULL,
    [ModificationDate]         DATETIME       NULL,
    [TimeStamp]                ROWVERSION     NOT NULL,
    CONSTRAINT [PK_PrivateBudgetItemsStructure__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrivateBudgetItemsStructure_PrivateBudgetItemsStructure] FOREIGN KEY ([ParentId]) REFERENCES [Budget].[PrivateBudgetItemsStructure] ([Id])
);


GO
ALTER TABLE [Budget].[PrivateBudgetItemsStructure] NOCHECK CONSTRAINT [FK_PrivateBudgetItemsStructure_PrivateBudgetItemsStructure];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PrivateBudgetItemsStructure__Code]
    ON [Budget].[PrivateBudgetItemsStructure]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna para controlar la concurrencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control presupuestal, permite saber que cuentas contables listo de la pestaña de cuentas contables del popup:  1 - Ninguno  2 - Tercero  3 - Centro Costo  4 - Centro Costo / Tercero  5 - Tercero / Centro Costo ', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'BudgetControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje exceder presupuesto, se habilita si el campo AllowExceedBudget está en true', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ExceedBudgetPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite exceder presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'AllowExceedBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje ejecución para alerta', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ExecutionAlertPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodicidad:  1 - Diario  2 - Semanal  3 - Quincenal  4 - Mensual', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la notificación por correo electrónico.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'EmailNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Orden de Compra', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'PurchaseOrderControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valores no presupuestados:  1 - Advertir  2 - No Permitir  3 - Ninguno', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'UnBudgetValues';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1 - Grupo  2 - Rubro Venta  3 - Rubro Gasto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la estructura padre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Id';

