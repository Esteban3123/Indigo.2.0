CREATE TABLE [Maintenance].[MaintenanceFailureRequestDetail] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceFailureRequestId] INT           NOT NULL,
    [TransactionClass]            TINYINT       NOT NULL,
    [PhysicalAssetId]             INT           NOT NULL,
    [PhysicalAssetPartsId]        INT           NULL,
    [Description]                 VARCHAR (500) NULL,
    CONSTRAINT [PK_MaintenanceFailureRequestDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenanceFailureRequestDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_MaintenanceFailureRequestDetail_FixedAssetPhysicalAssetParts] FOREIGN KEY ([PhysicalAssetPartsId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAssetParts] ([Id]),
    CONSTRAINT [FK_MaintenanceFailureRequestDetail_MaintenanceFailureRequest] FOREIGN KEY ([MaintenanceFailureRequestId]) REFERENCES [Maintenance].[MaintenanceFailureRequest] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenanceFailureRequestDetail] NOCHECK CONSTRAINT [FK_MaintenanceFailureRequestDetail_FixedAssetPhysicalAsset];


GO
ALTER TABLE [Maintenance].[MaintenanceFailureRequestDetail] NOCHECK CONSTRAINT [FK_MaintenanceFailureRequestDetail_FixedAssetPhysicalAssetParts];


GO
ALTER TABLE [Maintenance].[MaintenanceFailureRequestDetail] NOCHECK CONSTRAINT [FK_MaintenanceFailureRequestDetail_MaintenanceFailureRequest];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del detalle de la solicitud', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la parte, este solo se llena si la clase del detalle es Parte', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del Activo, este solo se llena si la clase del detalle es Activo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la clase del detalle a solicitar mantenimiento  1 - Activo  2 - Parte de Activo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'TransactionClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la solicitud de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'MaintenanceFailureRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';

