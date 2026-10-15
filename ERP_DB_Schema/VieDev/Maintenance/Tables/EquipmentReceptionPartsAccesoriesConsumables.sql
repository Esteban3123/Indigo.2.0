CREATE TABLE [Maintenance].[EquipmentReceptionPartsAccesoriesConsumables] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPartsAccesoriesConsumables] INT           NOT NULL,
    [IdEquipmentReception]         INT           NOT NULL,
    [Name]                         VARCHAR (50)  NULL,
    [Comment]                      VARCHAR (250) NULL,
    [RegisterInvima]               VARCHAR (20)  NULL,
    [LifeTime]                     CHAR (1)      NULL,
    [MeasurementUnit]              CHAR (1)      NULL,
    [SupplierReference]            VARCHAR (20)  NULL,
    [InstallationDate]             DATETIME      NULL,
    [CalculatedDate]               DATETIME      NULL,
    CONSTRAINT [PK_EquipmentReceptionPartsAccesoriesConsumables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentReceptionPartsAccesoriesConsumables_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id]),
    CONSTRAINT [FK_EquipmentReceptionPartsAccesoriesConsumables_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([IdPartsAccesoriesConsumables]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentReceptionPartsAccesoriesConsumables] NOCHECK CONSTRAINT [FK_EquipmentReceptionPartsAccesoriesConsumables_EquipmentRegistration];


GO
ALTER TABLE [Maintenance].[EquipmentReceptionPartsAccesoriesConsumables] NOCHECK CONSTRAINT [FK_EquipmentReceptionPartsAccesoriesConsumables_FixedAssetPartsAccesoriesConsumables];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha estimada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'CalculatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de instalación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia del proveedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'SupplierReference';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'vida util', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro invima', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'RegisterInvima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID recepción de equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Piezas Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'IdPartsAccesoriesConsumables';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Id';

