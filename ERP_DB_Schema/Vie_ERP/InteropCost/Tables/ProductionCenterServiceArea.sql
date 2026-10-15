CREATE TABLE [InteropCost].[ProductionCenterServiceArea] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionCenterId] INT          NOT NULL,
    [ServiceAreaId]      INT          NOT NULL,
    [ServiceAreaCode]    VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ProductionCenterServiceArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionCenterServiceArea_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[ProductionCenterServiceArea] NOCHECK CONSTRAINT [FK_ProductionCenterServiceArea_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del area de servicio con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la area del servicio del erp con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ServiceAreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion del area de servicio', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'Id';

