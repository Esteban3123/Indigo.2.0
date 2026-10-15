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
ALTER TABLE [Budget].[AvailabilityDetail] NOCHECK CONSTRAINT [FK_AvailabilityDetail_Availability];


GO
ALTER TABLE [Budget].[AvailabilityDetail] NOCHECK CONSTRAINT [FK_AvailabilityDetail_Budget];


GO
ALTER TABLE [Budget].[AvailabilityDetail] NOCHECK CONSTRAINT [FK_AvailabilityDetail_CPCCodeId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de concepto CPCCatalog', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'CPCCodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo del presupuesto el cual se obtiene de la siguiente manera     TotalAvailability - ExecutedValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor ejecutado del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total del disponibilidad el cual se obtiene de la siguiente manera InitialValue - DebitValueModification + CreditValueModification', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'TotalAvailability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor modificacion credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del presupuesto inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityDetail', @level2type = N'COLUMN', @level2name = N'Id';

