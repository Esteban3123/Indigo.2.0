CREATE TABLE [Maintenance].[Location] (
    [Id]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdParent]       INT          NULL,
    [CityId]         INT          NULL,
    [Code]           VARCHAR (20) NOT NULL,
    [Name]           VARCHAR (50) NOT NULL,
    [IdLocationType] SMALLINT     NOT NULL,
    [RiskLevel]      CHAR (1)     NULL,
    [TimeStamp]      ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Location__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Location_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Location_Location] FOREIGN KEY ([IdParent]) REFERENCES [Maintenance].[Location] ([Id]),
    CONSTRAINT [FK_Location_LocationType] FOREIGN KEY ([IdLocationType]) REFERENCES [Maintenance].[LocationType] ([Id])
);


GO
ALTER TABLE [Maintenance].[Location] NOCHECK CONSTRAINT [FK_Location_City];


GO
ALTER TABLE [Maintenance].[Location] NOCHECK CONSTRAINT [FK_Location_Location];


GO
ALTER TABLE [Maintenance].[Location] NOCHECK CONSTRAINT [FK_Location_LocationType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna para el control de concurrencia de la aplicacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de riesgo de la ubicacion   1-Alto  2-Medio  3-Bajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'RiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el tipo de ubicacion   1- Sede o Sucursal  2-Torre  3-Piso  4-Area  5-Habitacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'IdLocationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Ubicacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la ubicacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la ciudad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro padre.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'IdParent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de ubicaciones.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Id';

