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
ALTER TABLE [MixingStation].[ConfirmationUnitDose] NOCHECK CONSTRAINT [FK_ConfirmationUnitDose_CMConfiguration];


GO
ALTER TABLE [MixingStation].[ConfirmationUnitDose] NOCHECK CONSTRAINT [FK_ConfirmationUnitDose_Package];


GO
ALTER TABLE [MixingStation].[ConfirmationUnitDose] NOCHECK CONSTRAINT [FK_ConfirmationUnitDose_ProductionLine];


GO
ALTER TABLE [MixingStation].[ConfirmationUnitDose] NOCHECK CONSTRAINT [FK_ConfirmationUnitDose_RequestMixingStationDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la vista de confirmación de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'KeyView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agrupador de Paquetes que genera el EHR en la tabla HCFARMEPD', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la solicitud central de mezclas, permite saber si el registro está en proceso de solicitud central de mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la central de mezcla a la cual se le realiza el registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la línea de producción, se asigna cuando se asocia un paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete para cuando se realiza una preparación magistral personalizada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparationPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete, se llama paquete sugerido cuando preparación magistral esta en False y se llama paquete asociado cuando preparación magistral esta en True', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Preparación magistral personalizada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen:   1. Orden Médica  2. Solicitud Dosis Unitaria Centro Atención Externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Dosage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código centro atención', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del servicio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ServiceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del servicio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDose', @level2type = N'COLUMN', @level2name = N'Id';

