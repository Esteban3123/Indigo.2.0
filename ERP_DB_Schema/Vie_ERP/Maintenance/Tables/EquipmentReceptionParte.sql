CREATE TABLE [Maintenance].[EquipmentReceptionParte] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentReception] INT           NOT NULL,
    [Idpart]               INT           NOT NULL,
    [NamePart]             VARCHAR (100) NOT NULL,
    [Name]                 VARCHAR (500) NULL,
    [Comment]              VARCHAR (250) NULL,
    [RegisterInvima]       VARCHAR (20)  NULL,
    [LifetimeTime]         CHAR (1)      NULL,
    [MeasurementUnit]      CHAR (1)      NULL,
    [SupplierReference]    VARCHAR (20)  NULL,
    [InstallationDate]     DATETIME      NULL,
    [CalculatedDate]       DATETIME      NULL,
    CONSTRAINT [PK_EquipmentReceptionParte__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentReceptionParte_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id]),
    CONSTRAINT [FK_ParteDetail_Part] FOREIGN KEY ([Idpart]) REFERENCES [Maintenance].[PartDetail] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentReceptionParte] NOCHECK CONSTRAINT [FK_EquipmentReceptionParte_EquipmentRegistration];


GO
ALTER TABLE [Maintenance].[EquipmentReceptionParte] NOCHECK CONSTRAINT [FK_ParteDetail_Part];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de calculacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'CalculatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de instalacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia del Proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'SupplierReference';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida de la vida Util 1-Año 2- Meses 3-Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de Vida Util del accesorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'LifetimeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro Invima ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'RegisterInvima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del detalle de la pieza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la pieza relacionada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'NamePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la pieza relacionadadel detalle de la pieza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Idpart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la recepcion del equipo relacionado ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'autonumerico del detalle de la pieza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Id';

