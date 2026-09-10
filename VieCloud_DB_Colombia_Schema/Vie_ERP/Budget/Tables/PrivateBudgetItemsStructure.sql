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



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PrivateBudgetItemsStructure__Code]
    ON [Budget].[PrivateBudgetItemsStructure]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP, autoincremental) para control de concurrencia optimista en SQL Server. Detecta conflictos de actualización simultánea.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para controlar la concurrencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha/hora (DATETIME, nullable) de última modificación. Auditoria de cambios en configuración presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20, nullable) que modificó por última vez. Trazabilidad de cambios en estructura presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha/hora (DATETIME, DEFAULT getdate()) de creación del registro. Marca temporal de origen del elemento presupuestario.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20, DEFAULT 999) que creó el registro. Trazabilidad de creación en auditoria presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de control presupuestario (TINYINT): 1=Ninguno, 2=Tercero, 3=Centro Costo, 4=Centro Costo+Tercero, 5=Tercero+Centro Costo. Determina dimensiones de validación contable.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'BudgetControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control presupuestal, permite saber que cuentas contables listo de la pestaña de cuentas contables del popup:  1 - Ninguno  2 - Tercero  3 - Centro Costo  4 - Centro Costo / Tercero  5 - Tercero / Centro Costo ', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'BudgetControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'BudgetControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje máximo (NUMERIC 5,2) permitido para exceder presupuesto. Activo solo si AllowExceedBudget=true.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ExceedBudgetPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje exceder presupuesto, se habilita si el campo AllowExceedBudget está en true', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ExceedBudgetPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ExceedBudgetPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano (BIT). Autoriza superar presupuesto asignado. Si es verdadero, habilita ExceedBudgetPercentage.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'AllowExceedBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite exceder presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'AllowExceedBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'AllowExceedBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje (NUMERIC 5,2) de gasto ejecutado que dispara alerta. Ej: 80% genera notificación antes de agotar presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ExecutionAlertPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje ejecución para alerta', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ExecutionAlertPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ExecutionAlertPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de control (TINYINT): 1=Diario, 2=Semanal, 3=Quincenal, 4=Mensual. Frecuencia de evaluación presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodicidad:  1 - Diario  2 - Semanal  3 - Quincenal  4 - Mensual', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Email (VARCHAR 50) para notificaciones de alertas presupuestarias. Destinatario de avisos de ejecución o exceso.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'EmailNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la notificación por correo electrónico.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'EmailNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'EmailNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano (BIT). Habilita/deshabilita validación y control de Órdenes de Compra en este rubro presupuestario.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'PurchaseOrderControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Orden de Compra', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'PurchaseOrderControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'PurchaseOrderControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comportamiento de valores no presupuestados (TINYINT): 1=Advertir, 2=No Permitir, 3=Ninguno. Control de transacciones fuera de presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'UnBudgetValues';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valores no presupuestados:  1 - Advertir  2 - No Permitir  3 - Ninguno', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'UnBudgetValues';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'UnBudgetValues';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo (TINYINT): 1=Grupo, 2=Rubro Venta, 3=Rubro Gasto. Clasifica el elemento en la estructura presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo:  1 - Grupo  2 - Rubro Venta  3 - Rubro Gasto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción (VARCHAR 100) del elemento de presupuesto. Nombre o label del rubro, grupo o categoría de gasto/venta.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del elemento presupuestario. Identificador legible para clasificación de grupos, rubros de venta o gasto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK recursiva) de la estructura padre en jerarquía de presupuesto. Permite nidación de grupos y rubros.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la estructura padre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'ParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de estructura de presupuesto privado. Clave primaria secuencial.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estructura jerárquica de rubros o ítems del presupuesto privado. Define los conceptos presupuestales (centros de costo, cuentas, categorías) con sus reglas de control: si permite sobrepasar el presupuesto, si requiere orden de compra, alertas de ejecución y notificaciones por correo.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructure';
