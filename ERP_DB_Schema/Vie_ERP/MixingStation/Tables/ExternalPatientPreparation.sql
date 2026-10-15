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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración de la adecuación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'UNidad de volumen de la mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'TotalPreparedUnitMeasurementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total de la mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paquete asociado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de preparación:
1. Reconstitución
2. Reconstitución + dilución
3. Dilución
', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'PreparationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de preparaciones/Mezclas/Adecuaciones solicitadas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparation', @level2type = N'COLUMN', @level2name = N'PreparationsRequested';

