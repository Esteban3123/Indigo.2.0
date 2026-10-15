CREATE TABLE [MixingStation].[RequestUnitDoseExternalCareCenterMaquila] (
    [Id]                                  INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestUnitDoseExternalCareCenterId] INT     NOT NULL,
    [Type]                                TINYINT NOT NULL,
    [ATCId]                               INT     NULL,
    [PackageId]                           INT     NULL,
    [UnitDoseTypeId]                      INT     NOT NULL,
    [Quantity]                            INT     NOT NULL,
    CONSTRAINT [PK_RequestUnitDoseExternalCareCenterMaquila_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterMaquila_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterMaquila_Package] FOREIGN KEY ([PackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterMaquila_RequestUnitDoseExternalCareCenter1] FOREIGN KEY ([RequestUnitDoseExternalCareCenterId]) REFERENCES [MixingStation].[RequestUnitDoseExternalCareCenter] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterMaquila_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterMaquila] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterMaquila_ATC];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterMaquila] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterMaquila_Package];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterMaquila] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterMaquila_RequestUnitDoseExternalCareCenter1];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterMaquila] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterMaquila_UnitDoseType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad, se asigna cuando el tipo solicitud de la cabecera sea maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de dosis unitaria, se asigna cuando el tipo de solicitud de la cabecera sea maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete, se asigna cuando el tipo solicitud de la cabecera sea maquila y el type 2.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'PackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento, se asigna cuando el tipo solicitud de la cabecera sea maquila y el type sea 1.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de detalle:  1. Medicamento  2. Paquete    Se asigna cuando el tipo de solicitud en la cabecera sea maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Id';

