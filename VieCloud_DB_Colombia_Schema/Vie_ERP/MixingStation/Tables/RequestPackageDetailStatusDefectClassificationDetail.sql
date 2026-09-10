CREATE TABLE [MixingStation].[RequestPackageDetailStatusDefectClassificationDetail] (
    [Id]                                               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestPackageDetailStatusDefectClassificationId] INT NOT NULL,
    [DefectClassificationItemId]                       INT NOT NULL,
    [Production]                                       BIT NULL,
    [Quality]                                          BIT NULL,
    CONSTRAINT [PK_RequestPackageDetailStatusDefectClassificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestPackageDetailStatusDefectClassificationDetail_DefectClassificationItem] FOREIGN KEY ([DefectClassificationItemId]) REFERENCES [MixingStation].[DefectClassificationItem] ([Id]),
    CONSTRAINT [FK_RequestPackageDetailStatusDefectClassificationDetail_RequestPackageDetailStatusDefectClassification] FOREIGN KEY ([RequestPackageDetailStatusDefectClassificationId]) REFERENCES [MixingStation].[RequestPackageDetailStatusDefectClassification] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_DefectClassificationDetail_ClassificationId]
    ON [MixingStation].[RequestPackageDetailStatusDefectClassificationDetail] ([RequestPackageDetailStatusDefectClassificationId] ASC)
    INCLUDE ([DefectClassificationItemId], [Quality], [Production]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los ítems de clasificación de defectos asociados a un estado de defecto en el detalle de un paquete de solicitud en la estación de mezcla. Registra qué tipo de defecto específico aplica y si corresponde a un problema de producción o de calidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de detalle de clasificación de defecto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la clasificación de defecto del estado del detalle del paquete de solicitud al que pertenece este ítem.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusDefectClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusDefectClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al ítem específico del catálogo de clasificación de defectos (tipo o categoría del defecto detectado).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'DefectClassificationItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'DefectClassificationItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el defecto es responsabilidad del área de producción (verdadero/falso).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'Production';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'Production';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el defecto es responsabilidad del área de calidad o control de calidad (verdadero/falso).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'Quality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassificationDetail', @level2type = N'COLUMN', @level2name = N'Quality';
