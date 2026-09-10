CREATE TABLE [Treasury].[CardCostCenter] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CardId]          INT NOT NULL,
    [OperatingUnitId] INT NOT NULL,
    [CostCenterId]    INT NOT NULL,
    CONSTRAINT [PK_CardCostCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CardCostCenter_Cards] FOREIGN KEY ([CardId]) REFERENCES [Treasury].[Cards] ([Id]),
    CONSTRAINT [FK_CardCostCenter_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CardCostCenter_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [UQ_CardCostCenter__CardId__OperatingUnitId] UNIQUE NONCLUSTERED ([CardId] ASC, [OperatingUnitId] ASC)
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (FK Payroll.CostCenter). Referencia al centro de costo asociado a la tarjeta de crédito corporativa para asignación de gastos y contabilidad.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (FK Common.OperatingUnit). Referencia a la unidad funcional, sede o centro de atención donde opera la tarjeta corporativa.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la franquicia o tarjeta de crédito (FK Treasury.Cards). Referencia única a la tarjeta corporativa asignada al centro de costo y unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'CardId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la franquicia', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'CardId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'CardId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de la tabla CardCostCenter (INT IDENTITY). Identificador único de la relación entre tarjeta, unidad operativa y centro de costo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre tarjetas de crédito o débito corporativas, la unidad operativa y el centro de costo al que están asignadas. Permite distribuir los gastos de cada tarjeta según la estructura financiera de la organización.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter';
