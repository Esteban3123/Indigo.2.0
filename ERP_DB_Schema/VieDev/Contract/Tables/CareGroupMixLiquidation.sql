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
ALTER TABLE [Contract].[CareGroupMixLiquidation] NOCHECK CONSTRAINT [FK_CareGroupMixLiquidation_CareGroupId];


GO
ALTER TABLE [Contract].[CareGroupMixLiquidation] NOCHECK CONSTRAINT [FK_CareGroupMixLiquidation_UnitDoseTypeId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo cobro, 1 = Aplicación en paciente, 2 = Terminacion de campaña', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'TypePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id tipo dosis unitaria', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMixLiquidation', @level2type = N'COLUMN', @level2name = N'Id';

