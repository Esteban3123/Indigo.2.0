CREATE TABLE [MixingStation].[RequestUnitDoseExternalCareCenterPatientDetails] (
    [Id]                                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestUnitDoseExternalCareCenterPatientId] INT             NOT NULL,
    [ATCId]                                      INT             NOT NULL,
    [Dosage]                                     DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId]                          INT             NOT NULL,
    [Quantity]                                   INT             NOT NULL,
    [Observations]                               VARCHAR (MAX)   NULL,
    [SendTo]                                     TINYINT         NULL,
    [AdministrationRouteId]                      INT             CONSTRAINT [DF_RequestUnitDoseExternalCareCenterPatientDetails_AdministrationRouteId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_RequestUnitDoseExternalCareCenterPatientDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_AdministrationRoute] FOREIGN KEY ([AdministrationRouteId]) REFERENCES [Inventory].[AdministrationRoute] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_RequestUnitDoseExternalCareCenterPatient] FOREIGN KEY ([RequestUnitDoseExternalCareCenterPatientId]) REFERENCES [MixingStation].[RequestUnitDoseExternalCareCenterPatient] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la vía de administración del medicamento (oral, intravenosa, intramuscular, tópica, etc.). FK a Inventory.AdministrationRoute. Valor por defecto: 1.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la vía de administración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino de procesamiento: 1=Central de Mezclas, 2=Dashboard Farmacia. Asignado en dashboard confirmación dosis unitarias (ERP) al revisar detalle de aplicación. TINYINT, nullable.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'SendTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica a donde se envía el registro:  1 - Enviar para procesar en central de mezclas  2 - Enviar para procesar en dashboard farmacia    Este campo se asigna en la dashboard confirmación dosis unitarias(ERP) al momento de ver el detalle de aplicación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'SendTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'SendTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas o administrativas de la dosis unitaria, asignadas al guardar detalle de aplicación en dashboard confirmación dosis unitarias. VARCHAR(MAX), nullable.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación, se asigna al momento de guardar en detalle de aplicación en dashboard confirmación dosis unitarias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/número de unidades del medicamento a administrar según la dosis prescrita. INT requerido.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (mg, ml, comprimido, ampolla, etc.). FK a Inventory.InventoryMeasurementUnit.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del medicamento expresada en valor numérico (ej: 500, 1.5). DECIMAL(18,2) requerido.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Dosage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Dosage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Dosage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del medicamento según código ATC (Anatomical Therapeutic Chemical). FK a Inventory.ATC. Vinculado a receta, prescripción farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a la cabecera (RequestUnitDoseExternalCareCenterPatient) que agrupa los detalles de dosis unitarias por paciente y centro de atención externo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterPatientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterPatientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterPatientId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del registro de detalle de dosis unitaria en solicitud de farmacia para centro externo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de medicamentos solicitados en dosis unitaria para pacientes de centros de atención externos. Registra cada ítem de la solicitud con su principio activo (ATC), dosis, unidad de medida, cantidad, vía de administración y observaciones de dispensación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails';
