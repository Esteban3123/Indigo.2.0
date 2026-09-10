CREATE TABLE [Contract].[CareGroupMixLiquidation] (
    [Id]             INT     IDENTITY (1, 1) NOT NULL,
    [CareGroupId]    INT     NOT NULL,
    [UnitDoseTypeId] INT     NOT NULL,
    [TypePayment]    TINYINT NOT NULL,
    CONSTRAINT [PK_CareGroupMixLiquidation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroupMixLiquidation_CareGroupId] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_CareGroupMixLiquidation_UnitDoseTypeId] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cobro o liquidación: 1 = Aplicación en paciente (administración de dosis unitaria), 2 = Terminación de campaña. Determina el modelo de facturación y contabilización de medicamentos o insumos en mezcla.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'TypePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo cobro, 1 = Aplicación en paciente, 2 = Terminacion de campaña', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'TypePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'TypePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del tipo de dosis unitaria asociada. Referencia a MixingStation.UnitDoseType. Clasifica la presentación, concentración o formato de la dosis que se prepara en estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id tipo dosis unitaria', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del grupo de atención o línea de cuidado contractual. Referencia a Contract.CareGroup. Agrupa servicios, procedimientos o medicamentos dentro de un acuerdo contractual con el centro o profesional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) de la liquidación mixta. Tipo INT Identity. Registro que relaciona contrato, dosis unitaria y modelo de cobro para liquidación de medicamentos preparados.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de liquidación por mezcla de grupos de atención en contratos, que define el tipo de pago asociado a cada combinación de grupo de atención y tipo de dosis unitaria para los procesos de facturación y liquidación contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation';
