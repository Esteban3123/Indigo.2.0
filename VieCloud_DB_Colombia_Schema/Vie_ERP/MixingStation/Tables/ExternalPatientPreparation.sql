CREATE TABLE [MixingStation].[ExternalPatientPreparation] (
    [Id]                                         INT            IDENTITY (1, 1) NOT NULL,
    [RequestUnitDoseExternalCareCenterPatientId] INT            NOT NULL,
    [PreparationsRequested]                      INT            NOT NULL,
    [PreparationTypeId]                          INT            NULL,
    [AdministrationRouteId]                      INT            NOT NULL,
    [AssociatedPackageId]                        INT            NULL,
    [VolumeTotalOrder]                           DECIMAL (8, 2) NOT NULL,
    [TotalPreparedUnitMeasurementId]             INT            NOT NULL,
    [Concentration]                              VARCHAR (50)   NULL,
    [Description]                                VARCHAR (MAX)  DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_ExternalPatientPreparation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExternalPatientPreparation_AdministrationRoute] FOREIGN KEY ([AdministrationRouteId]) REFERENCES [Inventory].[AdministrationRoute] ([Id]),
    CONSTRAINT [FK_ExternalPatientPreparation_AssociatedPackage] FOREIGN KEY ([AssociatedPackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_ExternalPatientPreparation_RequestUnitDoseExternalCareCenterPatient] FOREIGN KEY ([RequestUnitDoseExternalCareCenterPatientId]) REFERENCES [MixingStation].[RequestUnitDoseExternalCareCenterPatient] ([Id]),
    CONSTRAINT [FK_ExternalPatientPreparation_UnitMeasurement] FOREIGN KEY ([TotalPreparedUnitMeasurementId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la preparación farmacéutica o adecuación magistral realizada para el paciente externo. VARCHAR(MAX), texto libre para notas clínicas y técnicas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración de la adecuación o mezcla magistral expresada en unidades farmacéuticas (mg/mL, %, etc.). VARCHAR(50), puede incluir múltiples componentes.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración de la adecuación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida de volumen utilizada en la mezcla preparada (mL, L, etc.). Referencia a InventoryMeasurementUnit. INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'TotalPreparedUnitMeasurementId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'UNidad de volumen de la mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'TotalPreparedUnitMeasurementId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'TotalPreparedUnitMeasurementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total de la mezcla o preparación magistral solicitada y preparada. DECIMAL(8,2), expresado en la unidad de medida asociada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen total de la mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paquete o envase asociado a esta preparación externa. Referencia a Package (MixingStation). INT, puede ser NULL si sin empaque predefinido.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paquete asociado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración del medicamento o mezcla preparada (oral, IV, IM, tópica, inhalada, etc.). Referencia obligatoria a AdministrationRoute (Inventory). INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vía de administración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de adecuación realizada: 1=Reconstitución, 2=Reconstitución+dilución, 3=Dilución. INT, puede ser NULL para preparaciones simples.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'PreparationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de preparación:
1. Reconstitución
2. Reconstitución + dilución
3. Dilución
', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'PreparationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'PreparationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de preparaciones, mezclas o adecuaciones magistrales solicitadas para este paciente. INT, incluye unidades múltiples si aplica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'PreparationsRequested';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de preparaciones/Mezclas/Adecuaciones solicitadas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'PreparationsRequested';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'PreparationsRequested';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preparaciones de medicamentos en la estación de mezclas para pacientes externos de centros de atención externos. Registra cada preparación solicitada con su tipo, ruta de administración, volumen, concentración y unidad de medida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de la preparación externa.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la solicitud de dosis unitaria del paciente externo en el centro de atención externo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterPatientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterPatientId';
