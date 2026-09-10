CREATE TABLE [Budget].[Budget] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetHeaderId]          INT          NOT NULL,
    [CategoryId]              INT          NOT NULL,
    [RevenueTypeId]           INT          NOT NULL,
    [InitialValue]            NUMERIC (18) NOT NULL,
    [DebitValueModification]  NUMERIC (18) NOT NULL,
    [CreditValueModification] NUMERIC (18) NOT NULL,
    [DebitValueTransfer]      NUMERIC (18) NOT NULL,
    [CreditValueTransfer]     NUMERIC (18) NOT NULL,
    [TotalBudget]             NUMERIC (18) NOT NULL,
    [ExecutedValue]           NUMERIC (18) NOT NULL,
    [SuspendedValue]          NUMERIC (18) NOT NULL,
    [Balance]                 NUMERIC (18) NOT NULL,
    [CreationUser]            VARCHAR (20) CONSTRAINT [DF_Budget_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]            DATETIME     CONSTRAINT [DF_Budget_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]        VARCHAR (20) NULL,
    [ModificationDate]        DATETIME     NULL,
    [TimeStamp]               ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Budget__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Budget_Budget_ValidateValues] CHECK ((((([InitialValue]-[DebitValueModification])+[CreditValueModification])-[DebitValueTransfer])+[CreditValueTransfer])=[TotalBudget] AND (([TotalBudget]-[ExecutedValue])-[SuspendedValue])=[Balance]),
    CONSTRAINT [FK_Budget_BudgetHeader] FOREIGN KEY ([BudgetHeaderId]) REFERENCES [Budget].[BudgetHeader] ([Id]),
    CONSTRAINT [FK_Budget_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_Budget_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id]),
    CONSTRAINT [UQ_Budget__CategoryId__RevenueTypeId] UNIQUE NONCLUSTERED ([CategoryId] ASC, [RevenueTypeId] ASC)
);




GO





GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) de auditoría que registra automáticamente el instante exacto de creación, modificación o evento en el presupuesto. Tipo: binary(8), no editable, para control de cambios y concurrencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME) del registro presupuestario. Nula si nunca fue editado. Búsqueda: cuándo se modificó el presupuesto, auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación (VARCHAR 20) del usuario que realizó la última edición del presupuesto. Nulo si nunca fue modificado. Auditoría y trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó originalmente el registro presupuestario. Default: función getdate() del sistema. Búsqueda: fecha de creación del presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación (VARCHAR 20) del usuario que creó el registro presupuestario. Default: 999 (sistema). Auditoría de origen y responsable de captura.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo disponible del presupuesto (NUMERIC 18). Cálculo: TotalBudget - ExecutedValue - SuspendedValue. Representa dinero no comprometido ni ejecutado. Búsqueda: saldo presupuestario, dinero disponible.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el saldo del presupuesto el cual se obtiene de la siguiente manera     TotalBudget - ExecutedValue - SuspendValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto (NUMERIC 18) del rubro que está suspendido o congelado, sin ejecutar ni disponible. Parte de la asignación presupuestaria en pausa. Búsqueda: presupuesto suspendido, bloqueado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'SuspendedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor suspendido del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'SuspendedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'SuspendedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto (NUMERIC 18) ya ejecutado o gastado del rubro presupuestario. Dinero invertido contra la asignación inicial. Búsqueda: gasto ejecutado, presupuesto gastado, ejecución.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor ejecutado al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presupuesto total ajustado (NUMERIC 18) del rubro tras todas las modificaciones y traslados. Cálculo: InitialValue - DebitValueModification + CreditValueModification - DebitValueTransfer + CreditValueTransfer. Búsqueda: presupuesto total, asignación final.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'TotalBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total del presupesuto el cual se obtiene de la siguiente manera InitialValue - DebitValueModification + CreditValueModification - DebitValueTransfer + CreditValueTransfer', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'TotalBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'TotalBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto (NUMERIC 18) de traslado tipo crédito (aumento) recibido por el rubro desde otra partida presupuestaria. Aumenta el presupuesto disponible. Búsqueda: traslado crédito, adición presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreditValueTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor traslado credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreditValueTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreditValueTransfer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto (NUMERIC 18) de traslado tipo débito (disminución) enviado desde este rubro a otra partida presupuestaria. Reduce el presupuesto asignado. Búsqueda: traslado débito, reducción presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'DebitValueTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor traslados debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'DebitValueTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'DebitValueTransfer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto (NUMERIC 18) de modificación tipo crédito (aumento) directo al rubro presupuestario. Ampliación de presupuesto. Búsqueda: modificación crédito, ampliación, adición.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreditValueModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor modificaciones credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreditValueModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CreditValueModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto (NUMERIC 18) de modificación tipo débito (disminución) directo del rubro presupuestario. Reducción de presupuesto asignado. Búsqueda: modificación débito, reducción, disminución.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'DebitValueModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valorde modificaciones debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'DebitValueModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'DebitValueModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial (NUMERIC 18) asignado al rubro presupuestario antes de cualquier modificación o traslado. Base presupuestaria de origen. Búsqueda: presupuesto inicial, asignación original.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del tipo de ingreso o fuente de financiamiento del presupuesto. Referencia a tabla RevenueType. Búsqueda: tipo de ingresos, fuente de recursos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de ingreso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la categoría o rubro presupuestario. Referencia a tabla Category. Parte de clave única con RevenueTypeId. Búsqueda: rubro, categoría presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'CategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cabecera o encabezado del presupuesto al que pertenece este detalle. Referencia a tabla BudgetHeader. Vínculo con presupuesto padre. Búsqueda: cabecera presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'BudgetHeaderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera del presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'BudgetHeaderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'BudgetHeaderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro presupuestario individual. Clave primaria. Búsqueda: presupuesto, identificador presupuesto, budget ID.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de líneas presupuestales: registra los valores iniciales, modificaciones (débitos y créditos), traslados, ejecución y saldo de cada rubro presupuestal, clasificado por categoría y tipo de ingreso dentro de un encabezado de presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget';
