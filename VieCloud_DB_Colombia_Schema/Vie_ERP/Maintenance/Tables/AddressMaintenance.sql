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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sincronización (CHAR(1)): marca si la dirección ha sido sincronizada con sistemas externos o ERP; valores típicos S/N o 1/0 para control de replicación de datos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sincronizado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la dirección (BIT): 1 = Activo/Vigente, 0 = Inactivo/Eliminado; controla si la dirección del proveedor está habilitada para operaciones', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado | 1 = Activo | 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección física o de correspondencia del proveedor (VARCHAR 100): calle, número, apartamento, localidad; usada para envíos, facturación y contacto', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Addresss';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Addresss';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Addresss';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la relación con mantenimiento de proveedores (INT, FK): enlace a SupplierMaintenance.Id para asociar direcciones a cada proveedor registrado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Mantenimiento de proveedores', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY 1,1): clave primaria de cada registro de dirección en el mantenimiento de proveedores', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de direcciones asociadas a proveedores o terceros en mantenimiento. Guarda las direcciones físicas vinculadas a cada proveedor, con su estado de vigencia y si han sido sincronizadas con otros sistemas.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AddressMaintenance';
