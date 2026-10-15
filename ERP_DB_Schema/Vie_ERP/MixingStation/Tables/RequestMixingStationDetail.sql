CREATE TABLE [MixingStation].[RequestMixingStationDetail] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestMixingStationId]       INT           NOT NULL,
    [ATCId]                        INT           NULL,
    [PackageId]                    INT           NULL,
    [UnitDoseTypeId]               INT           NOT NULL,
    [Quantity]                     INT           NULL,
    [Status]                       TINYINT       NULL,
    [EntityId]                     INT           NOT NULL,
    [EntityName]                   VARCHAR (300) NOT NULL,
    [CareCenterCode]               VARCHAR (20)  NOT NULL,
    [Source]                       TINYINT       NOT NULL,
    [CampaignDetailId]             INT           NULL,
    [ProductionLineId]             INT           NULL,
    [PackagePersonalizedId]        INT           NULL,
    [LabelType]                    TINYINT       NULL,
    [SendTo]                       TINYINT       CONSTRAINT [DF_RequestMixingStationDetail_SendTo] DEFAULT ((0)) NOT NULL,
    [ConfirmationUser]             VARCHAR (20)  NULL,
    [ConfirmationDate]             DATETIME      NULL,
    [RequestPackageDetailStatusId] INT           NULL,
    CONSTRAINT [PK_RequestMixingStationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestMixingStationDetail_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_RequestMixingStationDetail_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_RequestMixingStationDetail_Package] FOREIGN KEY ([PackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_RequestMixingStationDetail_PackagePersonalized] FOREIGN KEY ([PackagePersonalizedId]) REFERENCES [MixingStation].[PackagePersonalized] ([Id]),
    CONSTRAINT [FK_RequestMixingStationDetail_ProductionLine] FOREIGN KEY ([ProductionLineId]) REFERENCES [MixingStation].[ProductionLine] ([Id]),
    CONSTRAINT [FK_RequestMixingStationDetail_RequestMixingStation] FOREIGN KEY ([RequestMixingStationId]) REFERENCES [MixingStation].[RequestMixingStation] ([Id]),
    CONSTRAINT [FK_RequestMixingStationDetail_RequestPackageDetailStatus] FOREIGN KEY ([RequestPackageDetailStatusId]) REFERENCES [MixingStation].[RequestPackageDetailStatus] ([Id]),
    CONSTRAINT [FK_RequestMixingStationDetail_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetail] NOCHECK CONSTRAINT [FK_RequestMixingStationDetail_ATC];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetail] NOCHECK CONSTRAINT [FK_RequestMixingStationDetail_CampaignDetail];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetail] NOCHECK CONSTRAINT [FK_RequestMixingStationDetail_Package];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetail] NOCHECK CONSTRAINT [FK_RequestMixingStationDetail_PackagePersonalized];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetail] NOCHECK CONSTRAINT [FK_RequestMixingStationDetail_ProductionLine];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetail] NOCHECK CONSTRAINT [FK_RequestMixingStationDetail_RequestMixingStation];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetail] NOCHECK CONSTRAINT [FK_RequestMixingStationDetail_RequestPackageDetailStatus];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetail] NOCHECK CONSTRAINT [FK_RequestMixingStationDetail_UnitDoseType];


GO
CREATE NONCLUSTERED INDEX [IDX_RequestMixingStationDetail_CampaignDetailId_ProductionLineId]
    ON [MixingStation].[RequestMixingStationDetail]([CampaignDetailId] ASC, [ProductionLineId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Asociacion del producto con readecuacion a una solicitud existente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se procesa y se confirma la Solicitud', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que procesa y confirma la Solicitud', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este Campo se inserta desde el Dasboard de Solicitudes de Central de Mezclas al Ser Procesada y se envia a la Solicitud a Campañas:     0 - No ha sido Procesada  1 - Procesada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'SendTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1- Bolsa  
2- Nutriciones parenterales  
3- Jeringa 10 CC  
4- Tableteria 4x4 cm 
5- Magistral', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'LabelType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es la relación con el Paquete personalizado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'PackagePersonalizedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la línea de producción, se asigna cuando se realiza el proceso de confirmación dosis unitarias, si viene desde los otros procesos va nula y se puede asignar en el modal para agregar la campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la campaña que me indica si esta solicitud esta dentro de una campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber desde donde viene el registro:  1. Orden Médica  2. Solicitud Externa Paciente  3. Solicitud Externa Maquila  4. Solicitud Inventario', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del centro de atención, cuando el registro viene desde la orden medica o solicitud inventario el código de centro de atención es de la tabla ADCENATEN, si el registro viene desde la solicitud externa maquila o paciente el código del centro de atención viene desde ExternalCareCenter', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad con la cual se realiza este registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad con la cual se realiza este registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:    1. En su Estado Inicial este en el Estado "Pendiente"  2. Confirmar se coloque "Confirmado"  3. Anular se coloque "Anulado"    Se asigna cuando el  proceso de solicitud no tiene pacientes, es decir cuando viene desde solicitud inventario o solicitud centro atención externo tipo maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad, se asigna cuando no tiene pacientes asignados', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete, se asigna cuando se realiza el proceso de solicitud desde confirmación dosis unitaria, solicitud inventario o solicitud centros de atención externo tipo maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'PackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento, se asigna cuando se realiza el proceso de solicitud desde solicitud inventario o solicitud centro atención externo tipo maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'RequestMixingStationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetail', @level2type = N'COLUMN', @level2name = N'Id';

