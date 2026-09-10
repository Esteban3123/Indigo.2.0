CREATE TABLE [Inventory].[RequestParamAuthUser] (
    [Id]                            INT IDENTITY (1, 1) NOT NULL,
    [RequestParamId]                INT NOT NULL,
    [WarehouseId]                   INT NULL,
    [FunctionalUnitId]              INT NULL,
    [UserPrincipalWarehouseId]      INT NULL,
    [UserAlternateWarehouseId]      INT NULL,
    [UserPrincipalFunctionalUnitId] INT NULL,
    [UserAlternateFunctionalUnitId] INT NULL,
    CONSTRAINT [PK_RequestParamAuthUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestParamAuthUser_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_RequestParamAuthUser_RequestParam] FOREIGN KEY ([RequestParamId]) REFERENCES [Inventory].[RequestParam] ([Id]),
    CONSTRAINT [FK_RequestParamAuthUser_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la unidad funcional o centro de atención asociado al parámetro de solicitud, referencia a Payroll.FunctionalUnit.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del almacén o depósito de inventario vinculado al parámetro de solicitud, referencia a Inventory.Warehouse.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacén', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del parámetro de solicitud de inventario que define reglas de comportamiento, referencia a Inventory.RequestParam.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'RequestParamId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del parámetro de solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'RequestParamId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'RequestParamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de autorización que relaciona almacenes y unidades funcionales (centros de atención, servicios) con parámetros de solicitudes de inventario, gestiona accesos de usuarios a almacenes y unidades funcionales primarias y alternas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla que relaciona los almacenes y unidades funcionales con los parámetros de solicitudes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de parámetros de autorización por usuario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bodega o almacén principal asignado al usuario para autorizar solicitudes de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'UserPrincipalWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'UserPrincipalWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bodega o almacén alternativo asignado al usuario, utilizado cuando no aplica la bodega principal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'UserAlternateWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'UserAlternateWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional principal asociada al usuario para la autorización de solicitudes de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'UserPrincipalFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'UserPrincipalFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional alternativa del usuario, usada como segunda opción en la autorización de solicitudes de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'UserAlternateFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'UserAlternateFunctionalUnitId';
