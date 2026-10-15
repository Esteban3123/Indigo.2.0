CREATE TABLE [Maintenance].[PhoneMaintenance] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierMaintenanceId] INT          NOT NULL,
    [Phone]                 VARCHAR (15) NULL,
    [State]                 BIT          NULL,
    [Synchronized]          CHAR (1)     NULL,
    [PhoneType]             SMALLINT     NULL,
    CONSTRAINT [PK_PhoneMaintenance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhoneMaintenance_PhoneMaintenance] FOREIGN KEY ([SupplierMaintenanceId]) REFERENCES [Maintenance].[SupplierMaintenance] ([Id])
);


GO
ALTER TABLE [Maintenance].[PhoneMaintenance] NOCHECK CONSTRAINT [FK_PhoneMaintenance_PhoneMaintenance];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de telefono  1-Movil  2-Fijo  3-Fax  4-Oficina  5-PIN', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'PhoneType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'sincronizado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado | 1 = Activo | 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Mantenimiento de proveedores', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Id';

