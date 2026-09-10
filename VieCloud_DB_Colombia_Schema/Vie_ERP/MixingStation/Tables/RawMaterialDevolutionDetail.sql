CREATE TABLE [MixingStation].[RawMaterialDevolutionDetail] (
    [Id]                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RawMaterialDevolutionId]    INT          NOT NULL,
    [CampaignDetailValidationId] INT          NOT NULL,
    [Quantity]                   INT          NOT NULL,
    [CreationUser]               VARCHAR (20) NOT NULL,
    [CreationDate]               DATETIME     NOT NULL,
    [ModificationUser]           VARCHAR (20) NULL,
    [ModificationDate]           DATETIME     NULL,
    CONSTRAINT [PK_RawMaterialDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RawMaterialDevolutionDetail_CampaignDetailValidation] FOREIGN KEY ([CampaignDetailValidationId]) REFERENCES [MixingStation].[CampaignDetailValidation] ([Id]),
    CONSTRAINT [FK_RawMaterialDevolutionDetail_RawMaterialDevolution] FOREIGN KEY ([RawMaterialDevolutionId]) REFERENCES [MixingStation].[RawMaterialDevolution] ([Id])
);




GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de devoluciones de materia prima en la estación de mezcla. Registra cada ítem devuelto dentro de una devolución, vinculando la validación de detalle de campaña con la cantidad de material retornado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle de devolución de materia prima.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la devolución de materia prima a la que pertenece este detalle.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RawMaterialDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RawMaterialDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la validación del detalle de campaña asociada al material devuelto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'CampaignDetailValidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'CampaignDetailValidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de materia prima devuelta en este registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró el detalle de devolución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el registro del detalle de devolución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación al detalle de devolución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación al detalle de devolución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RawMaterialDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
