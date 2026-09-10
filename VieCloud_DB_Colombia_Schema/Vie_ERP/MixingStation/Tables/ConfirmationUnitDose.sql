CREATE TABLE [MixingStation].[ConfirmationUnitDose] (
    [Id]                                     INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ServiceCode]                            VARCHAR (20)     NOT NULL,
    [ServiceName]                            VARCHAR (300)    NOT NULL,
    [CareCenterCode]                         VARCHAR (20)     NOT NULL,
    [Dosage]                                 DECIMAL (18, 2)  NOT NULL,
    [Quantity]                               INT              NOT NULL,
    [Source]                                 TINYINT          NOT NULL,
    [PersonalizedMasterPreparation]          BIT              NOT NULL,
    [PackageId]                              INT              NOT NULL,
    [PersonalizedMasterPreparationPackageId] INT              NULL,
    [ProductionLineId]                       INT              NOT NULL,
    [CMConfigurationId]                      INT              NOT NULL,
    [Status]                                 TINYINT          NOT NULL,
    [CreationUser]                           VARCHAR (20)     NOT NULL,
    [CreationDate]                           DATETIME         NOT NULL,
    [ModificationUser]                       VARCHAR (20)     NULL,
    [ModificationDate]                       DATETIME         NULL,
    [TimeStamp]                              ROWVERSION       NOT NULL,
    [RequestMixingStationDetailId]           INT              NULL,
    [GroupingCodeDose]                       UNIQUEIDENTIFIER NULL,
    [KeyView]                                VARCHAR (100)    DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_ConfirmationUnitDose] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConfirmationUnitDose_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_ConfirmationUnitDose_Package] FOREIGN KEY ([PackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_ConfirmationUnitDose_PackagePackagePersonalized] FOREIGN KEY ([PersonalizedMasterPreparationPackageId]) REFERENCES [MixingStation].[PackagePersonalized] ([Id]),
    CONSTRAINT [FK_ConfirmationUnitDose_ProductionLine] FOREIGN KEY ([ProductionLineId]) REFERENCES [MixingStation].[ProductionLine] ([Id]),
    CONSTRAINT [FK_ConfirmationUnitDose_RequestMixingStationDetail] FOREIGN KEY ([RequestMixingStationDetailId]) REFERENCES [MixingStation].[RequestMixingStationDetail] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (VARCHAR 100) de la vista de confirmación de dosis unitaria; clave para búsqueda y visualización del registro en la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'KeyView';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la vista de confirmación de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'KeyView';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'KeyView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GUID (UNIQUEIDENTIFIER) que agrupa paquetes generados por el EHR en tabla HCFARMEPD; permite rastrear dosis asociadas en solicitudes de mezcla central.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agrupador de Paquetes que genera el EHR en la tabla HCFARMEPD', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT que referencia RequestMixingStationDetail; indica si el registro está en proceso de solicitud de mezcla central y permite auditar trazabilidad de la orden.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la solicitud central de mezclas, permite saber si el registro está en proceso de solicitud central de mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP binario que captura el instante exacto de creación, modificación o cambio de estado del registro en la estación de mezcla; control de versión automático.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME nullable; fecha y hora en que se modificó por última vez el registro de confirmación de dosis unitaria en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) nullable; usuario del sistema (profesional de salud, farmacéutico, técnico) que realizó la última modificación del registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME; fecha y hora de creación del registro de confirmación de dosis unitaria en el sistema de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20); usuario del sistema que creó el registro de confirmación de dosis unitaria en estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT; estado actual del registro (ej: pendiente, confirmado, rechazado, en proceso) en la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT; identificador de la central de mezcla (Mixing Station Configuration) a la cual se asigna y procesa el registro de confirmación de dosis.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la central de mezcla a la cual se le realiza el registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT; identificador de la línea de producción asignada cuando se vincula un paquete; trazabilidad de manufactura en estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la línea de producción, se asigna cuando se asocia un paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT nullable a PackagePersonalized; identificador del paquete cuando se realiza preparación magistral personalizada (receta magistral).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparationPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete para cuando se realiza una preparación magistral personalizada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparationPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparationPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT; identificador del paquete (sugerido si preparación magistral=False, asociado si=True); vínculo a inventario y configuración farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete, se llama paquete sugerido cuando preparación magistral esta en False y se llama paquete asociado cuando preparación magistral esta en True', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT; indicador (0=paquete sugerido estándar, 1=preparación magistral personalizada) que determina tipo de paquete y protocolo de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Preparación magistral personalizada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT; origen de la solicitud (1=Orden Médica, 2=Solicitud Dosis Unitaria desde Centro de Atención Externo); traza procedencia de la prescripción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Source';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen:   1. Orden Médica  2. Solicitud Dosis Unitaria Centro Atención Externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Source';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT; cantidad total de unidades de dosis a preparar en la estación de mezcla; control de producción y dispensación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2); dosis unitaria en gramos, miligramos, unidades internacionales u otra unidad de medida farmacéutica; precisión de concentración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Dosage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Dosage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Dosage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20); código del centro de atención, unidad funcional o sede donde se requiere la dosis unitaria; identificación de entidad prestadora.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código centro atención', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(300); nombre descriptivo del servicio (ej: Farmacología, Oncología, Urgencias) donde se administrará la dosis unitaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ServiceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del servicio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ServiceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ServiceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20); código estándar del servicio clínico o unidad funcional; facilita búsqueda y auditoría de dosis por área de atención.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del servicio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY PK; identificador único autoincremental del registro de confirmación de dosis unitaria en la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de confirmación de dosis unitarias en la estación de mezclas (farmacia). Guarda la información de cada dosis preparada o por preparar, incluyendo el servicio solicitante, la cantidad, el tipo de preparación y el estado del proceso de dispensación unitaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose';
