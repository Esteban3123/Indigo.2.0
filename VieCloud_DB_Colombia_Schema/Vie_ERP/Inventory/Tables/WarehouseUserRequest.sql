CREATE TABLE [Inventory].[WarehouseUserRequest] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [WarehouseId] INT          NOT NULL,
    [UserId]      INT          NOT NULL,
    [UserCode]    VARCHAR (20) NOT NULL,
    CONSTRAINT [PK__Warehous__3214EC07A114346F] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WarehouseUserRequest_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de acceso o asignación de usuarios a bodegas de inventario. Relaciona cada usuario del sistema con la bodega a la que tiene permiso o fue asignado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de la solicitud de usuario-bodega.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la bodega o almacén al que se asocia el usuario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del usuario solicitante o asignado a la bodega.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario, login o identificador textual del funcionario asignado a la bodega.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseUserRequest', @level2type = N'COLUMN', @level2name = N'UserCode';
