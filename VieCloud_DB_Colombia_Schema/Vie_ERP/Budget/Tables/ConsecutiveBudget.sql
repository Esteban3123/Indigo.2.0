CREATE TABLE [Budget].[ConsecutiveBudget] (
    [Id]                INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ValidityId]        INT     NOT NULL,
    [FormId]            INT     NOT NULL,
    [NumberConsecutive] INT     NOT NULL,
    [ProcessType]       TINYINT NULL,
    [Used]              BIT     CONSTRAINT [DF_ConsecutiveBudget_Used] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ConsecutiveBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsecutiveBudget_Validity] FOREIGN KEY ([ValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (bit) que marca si el consecutivo presupuestario está en uso activo (1) o disponible (0); controla disponibilidad del número secuencial en flujo presupuestario', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'Used';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si el consecutivo se encuentra en uso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'Used';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'Used';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de proceso presupuestario: 1=Ingreso/Ingresos, 2=Gastos/Egresos; clasifica la naturaleza del movimiento presupuestario', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'ProcessType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de proceso: 1->ingreso,2-> gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'ProcessType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'ProcessType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial único del consecutivo presupuestario; identificador de orden en la serie de documentos presupuestarios por validez', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'NumberConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'NumberConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'NumberConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formulario o comprobante donde se bloquea/asigna el consecutivo presupuestario; referencia al documento de origen', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del formulario en el que se encuentra bloqueado el registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'FormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la vigencia presupuestaria (año fiscal/período); clave foránea a tabla BudgetaryValidity; agrupa consecutivos por período válido', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'ValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de validez', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'ValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'ValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY); clave primaria que incrementa automáticamente desde 1 para cada registro nuevo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de consecutivos utilizados para la numeración de presupuestos. Controla qué números de presupuesto han sido asignados por vigencia y formulario, indicando si ya fueron usados y el tipo de proceso al que pertenecen.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ConsecutiveBudget';
