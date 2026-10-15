CREATE TABLE [Maintenance].[AddressMaintenance] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierMaintenanceId] INT           NOT NULL,
    [Addresss]              VARCHAR (100) NOT NULL,
    [State]                 BIT           NOT NULL,
    [Synchronized]          CHAR (1)      NOT NULL,
    CONSTRAINT [PK_AddressMaintenance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AddressMaintenance_SupplierMaintenance] FOREIGN KEY ([SupplierMaintenanceId]) REFERENCES [Maintenance].[SupplierMaintenance] ([Id])
);


GO
ALTER TABLE [Maintenance].[AddressMaintenance] NOCHECK CONSTRAINT [FK_AddressMaintenance_SupplierMaintenance];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sincronizado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado | 1 = Activo | 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Addresss';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Mantenimiento de proveedores', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Id';

