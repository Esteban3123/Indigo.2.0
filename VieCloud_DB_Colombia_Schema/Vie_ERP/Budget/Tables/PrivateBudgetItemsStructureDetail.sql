CREATE TABLE [Budget].[PrivateBudgetItemsStructureDetail] (
    [Id]                            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PrivateBudgetItemsStructureId] INT NOT NULL,
    [MainAccountId]                 INT NULL,
    [CostCenterId]                  INT NULL,
    [ThirdPartyId]                  INT NULL,
    CONSTRAINT [PK_PrivateBudgetItemsStructureDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_PrivateBudgetItemsStructure] FOREIGN KEY ([PrivateBudgetItemsStructureId]) REFERENCES [Budget].[PrivateBudgetItemsStructure] ([Id]),
    CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor, acreedor, entidad externa) asociado al detalle presupuestario; FK a Common.ThirdParty', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (unidad funcional, departamento, área operativa) vinculado al presupuesto; FK a Payroll.CostCenter', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal (código contable, cuenta mayor) del movimiento presupuestario; FK a GeneralLedger.MainAccounts', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la estructura presupuestaria padre (cabecera del presupuesto privado); FK a Budget.PrivateBudgetItemsStructure', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'PrivateBudgetItemsStructureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'PrivateBudgetItemsStructureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'PrivateBudgetItemsStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de detalle presupuestario; INT IDENTITY', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la estructura de ítems de presupuesto privado: registra la distribución contable de cada ítem presupuestario, asociando la cuenta principal, el centro de costos y el tercero responsable para la gestión financiera y contable del presupuesto privado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail';
