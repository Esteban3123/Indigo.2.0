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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del almacén', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del parámetro de solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser', @level2type = N'COLUMN', @level2name = N'RequestParamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que relaciona los almacenes y unidades funcionales con los parámetros de solicitudes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamAuthUser';

