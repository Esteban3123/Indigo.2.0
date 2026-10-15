CREATE TABLE [Maintenance].[EquipmentReceptionConsumable] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdConsumable]         INT           NOT NULL,
    [IdEquipmentReception] INT           NOT NULL,
    [Name]                 VARCHAR (50)  NULL,
    [Comment]              VARCHAR (250) NULL,
    [RegisterInvima]       VARCHAR (20)  NULL,
    [LifetimeTime]         CHAR (1)      NULL,
    [MeasurementUnit]      CHAR (1)      NULL,
    [SupplierReference]    VARCHAR (20)  NULL,
    [InstallationDate]     DATETIME      NULL,
    [CalculatedDate]       DATETIME      NULL,
    CONSTRAINT [PK_EquipmentReceptionDConsumable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentReceptionConsumable_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id]),
    CONSTRAINT [FK_EquipmentReceptionDConsumable_ConsumableDetail] FOREIGN KEY ([IdConsumable]) REFERENCES [Maintenance].[Consumable] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentReceptionConsumable] NOCHECK CONSTRAINT [FK_EquipmentReceptionConsumable_EquipmentRegistration];


GO
ALTER TABLE [Maintenance].[EquipmentReceptionConsumable] NOCHECK CONSTRAINT [FK_EquipmentReceptionDConsumable_ConsumableDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de calculacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'CalculatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de instalacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia del Proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'SupplierReference';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida de la vida Util 1-Año 2- Meses 3-Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de Vida Util del accesorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'LifetimeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro Invima ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'RegisterInvima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del consumible', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro detalle de equipo relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del consumible relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'IdConsumable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de relacion entre  el registro del detalle del equipo  y los consumibles', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Id';

