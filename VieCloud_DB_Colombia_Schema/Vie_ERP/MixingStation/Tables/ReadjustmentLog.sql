CREATE TABLE [MixingStation].[ReadjustmentLog] (
    [Id]                           INT          IDENTITY (1, 1) NOT NULL,
    [RequestMixingStationDetailId] INT          NOT NULL,
    [RequestPackageDetailStatusId] INT          NOT NULL,
    [BatchCode]                    VARCHAR (50) NOT NULL,
    [CreationDate]                 DATETIME     NOT NULL,
    [CreationUser]                 VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ReadjustmentLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReadjustmentLog_RequestMixingStationDetail] FOREIGN KEY ([RequestMixingStationDetailId]) REFERENCES [MixingStation].[RequestMixingStationDetail] ([Id]),
    CONSTRAINT [FK_ReadjustmentLog_RequestPackageDetailStatus] FOREIGN KEY ([RequestPackageDetailStatusId]) REFERENCES [MixingStation].[RequestPackageDetailStatus] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de readjustamiento; nombre o identificador del operario/técnico responsable de la readecuación en estación de mezcla (VARCHAR 20, auditoría).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creó el registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de readjustamiento; timestamp del evento de readecuación del lote en estación de mezcla (DATETIME, auditoría).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del lote/remesa antes de la readecuación; identificador único del lote de medicamentos/productos sometido a reajuste en estación de mezcla (VARCHAR 50, trazabilidad).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'BatchCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del lote antes de la readecuación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'BatchCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'BatchCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del estado del detalle de paquete al cual se realizó readecuación; referencia a cambio de estado del paquete tras reajuste (INT, FK RequestPackageDetailStatus, auditoría de cambios).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete al cual se le ha realizado una readecuación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de solicitud en estación de mezcla relacionado con el paquete readecuado; relación entre el registro de readjustamiento y la solicitud original de mezcla (INT, FK RequestMixingStationDetail, trazabilidad de solicitudes).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id rel detalle de la solicitud donde ha estado relacionado el paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de reajustes realizados en los detalles de solicitudes de la estación de mezcla, indicando el estado del paquete, el lote afectado y el usuario que ejecutó el ajuste.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de reajuste, generado automáticamente por el sistema.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'Id';
