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



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (INT) de unidades a preparar en maquila; se asigna cuando tipo solicitud cabecera = maquila; acompaña medicamento (Type=1) o paquete (Type=2)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad, se asigna cuando el tipo solicitud de la cabecera sea maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a tipo de dosis unitaria (MixingStation.UnitDoseType); define formato/presentación de dosis (cápsula, comprimido, ampolla, etc.) en maquila; se asigna cuando solicitud cabecera es tipo maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de dosis unitaria, se asigna cuando el tipo de solicitud de la cabecera sea maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a paquete de medicamentos (MixingStation.Package); obligatorio cuando Type=2 (paquete maquila); agrupa múltiples medicamentos pre-configurados', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete, se asigna cuando el tipo solicitud de la cabecera sea maquila y el type 2.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'PackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a clasificación ATC de medicamento (Inventory.ATC); obligatorio cuando Type=1 (medicamento maquila); referencia identificador del fármaco/principio activo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del medicamento, se asigna cuando el tipo solicitud de la cabecera sea maquila y el type sea 1.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificador de detalle (TINYINT): 1=Medicamento (ATC), 2=Paquete. Se asigna cuando solicitud cabecera es tipo maquila; activa campos ATCId o PackageId según corresponda', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de detalle:  1. Medicamento  2. Paquete    Se asigna cuando el tipo de solicitud en la cabecera sea maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a cabecera de solicitud de dosis unitaria (RequestUnitDoseExternalCareCenter); agrupa detalles de maquila cuando tipo solicitud = maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de detalle de maquila en solicitud de dosis unitaria a centro externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los ítems o líneas de solicitud de dosis unitaria enviadas a maquila (elaboración externa) para un centro de atención externo, indicando el tipo de medicamento, presentación y cantidad requerida por cada solicitud.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterMaquila';
