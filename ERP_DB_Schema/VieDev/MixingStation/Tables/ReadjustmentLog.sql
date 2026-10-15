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
ALTER TABLE [MixingStation].[ReadjustmentLog] NOCHECK CONSTRAINT [FK_ReadjustmentLog_RequestMixingStationDetail];


GO
ALTER TABLE [MixingStation].[ReadjustmentLog] NOCHECK CONSTRAINT [FK_ReadjustmentLog_RequestPackageDetailStatus];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que creó el registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del lote antes de la readecuación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'BatchCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete al cual se le ha realizado una readecuación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id rel detalle de la solicitud donde ha estado relacionado el paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReadjustmentLog', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';

