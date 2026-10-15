CREATE TABLE [Maintenance].[LocationType] (
    [Id]            SMALLINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [OrderLocation] INT           NOT NULL,
    [State]         BIT           NOT NULL,
    CONSTRAINT [PK_LocationType__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del tipo de ubicacion  1-Activo   0-Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'LocationType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación del pedido', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'LocationType', @level2type = N'COLUMN', @level2name = N'OrderLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la ubicacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'LocationType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del tipo de ubicaciones', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'LocationType', @level2type = N'COLUMN', @level2name = N'Id';

