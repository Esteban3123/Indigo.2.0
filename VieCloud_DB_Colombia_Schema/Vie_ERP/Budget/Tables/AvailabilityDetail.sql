CREATE TABLE [Budget].[AvailabilityDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AvailabilityId]          INT             NOT NULL,
    [BudgetId]                INT             NOT NULL,
    [InitialValue]            NUMERIC (18, 2) NOT NULL,
    [DebitModificationValue]  NUMERIC (18, 2) NOT NULL,
    [CreditModificationValue] NUMERIC (18, 2) NOT NULL,
    [TotalAvailability]       NUMERIC (18)    NOT NULL,
    [ExecutedValue]           NUMERIC (18, 2) NOT NULL,
    [Balance]                 NUMERIC (18)    NOT NULL,
    [CPCCodeId]               INT             NULL,
    CONSTRAINT [PK_AvailabilityDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Budget_AvailabilityDetail_ValidateValues] CHECK ((([InitialValue]-[DebitModificationValue])+[CreditModificationValue])=[TotalAvailability] AND ([TotalAvailability]-[ExecutedValue])=[Balance] AND NOT ([TotalAvailability]<(0) OR [ExecutedValue]<(0) OR [Balance]<(0))),
    CONSTRAINT [FK_AvailabilityDetail_Availability] FOREIGN KEY ([AvailabilityId]) REFERENCES [Budget].[Availability] ([Id]),
    CONSTRAINT [FK_AvailabilityDetail_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_AvailabilityDetail_CPCCodeId] FOREIGN KEY ([CPCCodeId]) REFERENCES [Budget].[CPCCatalog] ([Id])
);


GO
ALTER TABLE [Budget].[AvailabilityDetail] NOCHECK CONSTRAINT [CK_Budget_AvailabilityDetail_ValidateValues];




GO
ALTER TABLE [Budget].[AvailabilityDetail] NOCHECK CONSTRAINT [CK_Budget_AvailabilityDetail_ValidateValues];


GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de clasificación presupuestal (CPCCatalog); referencia a código de rubro, línea o elemento de gasto en el catálogo de conceptos presupuestales. Tipo: INT, Nullable.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'CPCCodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de concepto CPCCatalog', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'CPCCodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'CPCCodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo disponible del rubro presupuestal calculado como: TotalAvailability menos ExecutedValue; representa fondos sin ejecutar. Tipo: NUMERIC(18,0), validado ≥0.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el saldo del presupuesto el cual se obtiene de la siguiente manera     TotalAvailability - ExecutedValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto ejecutado, devengado o gastado del rubro presupuestal; importe de pagos, compromisos o egresos reales realizados. Tipo: NUMERIC(18,2), validado ≥0.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor ejecutado del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de disponibilidad presupuestal del rubro, calculado como: InitialValue menos DebitModificationValue más CreditModificationValue; presupuesto ajustado disponible para ejecutar. Tipo: NUMERIC(18,0), validado ≥0.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'TotalAvailability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total del disponibilidad el cual se obtiene de la siguiente manera InitialValue - DebitValueModification + CreditValueModification', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'TotalAvailability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'TotalAvailability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de incremento o adición crediticia al rubro presupuestal; ampliaciones, traslados o ajustes positivos de presupuesto. Tipo: NUMERIC(18,2).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor modificacion credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de reducción o debito al rubro presupuestal; disminuciones, traslados o ajustes negativos de presupuesto. Tipo: NUMERIC(18,2).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto inicial o base del rubro presupuestal asignado; presupuesto de origen antes de modificaciones. Tipo: NUMERIC(18,2), validado ≥0.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del presupuesto (maestro) al cual pertenece este detalle de disponibilidad; clave foránea a tabla Budget. Tipo: INT, requerido.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del presupuesto inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o maestro de disponibilidad presupuestal; agrupa detalles de un mismo periodo o asignación. Tipo: INT, requerido, FK a Availability.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de detalle de disponibilidad presupuestal; línea individual de rubro con valores iniciales, modificaciones, ejecución y saldo. Tipo: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de disponibilidad presupuestal por rubro: registra los valores iniciales asignados, las modificaciones por débito y crédito, el total disponible, lo ejecutado y el saldo restante para cada combinación de disponibilidad y presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail';
