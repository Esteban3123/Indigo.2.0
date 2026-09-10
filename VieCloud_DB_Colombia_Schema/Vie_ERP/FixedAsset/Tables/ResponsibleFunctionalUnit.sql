CREATE TABLE [FixedAsset].[ResponsibleFunctionalUnit] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdResponsible]    INT NOT NULL,
    [IdFunctionalUnit] INT NOT NULL,
    CONSTRAINT [PK_ResponsibleFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResponsibleFunctionalUnit_FunctionalUnit] FOREIGN KEY ([IdFunctionalUnit]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ResponsibleFunctionalUnit_ResponsibleFunctionalUnit] FOREIGN KEY ([IdResponsible]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Unidad Funcional (Centro de Atención, Departamento o Servicio) responsable del activo fijo. FK a Payroll.FunctionalUnit.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Responsable del Activo Fijo (Profesional de Salud, Administrador o Custodio). FK a FixedAsset.FixedAssetResponsible.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación entre Responsable y Unidad Funcional responsable del activo fijo. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Funcional Responsable ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los responsables de activos fijos con las unidades funcionales a las que pertenecen. Permite saber qué persona o cargo es responsable de los bienes en cada unidad funcional de la organización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit';
