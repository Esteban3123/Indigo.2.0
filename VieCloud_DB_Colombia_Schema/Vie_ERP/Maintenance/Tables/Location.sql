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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (TIMESTAMP) para control de concurrencia optimista en actualizaciones simultáneas de la ubicación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para el control de concurrencia de la aplicacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de riesgo operacional o de bioseguridad (CHAR 1): 1=Alto, 2=Medio, 3=Bajo; aplica a áreas de riesgo en centros de atención.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'RiskLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de riesgo de la ubicacion   1-Alto  2-Medio  3-Bajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'RiskLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'RiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ubicación (FK a Maintenance.LocationType): 1=Sede/Sucursal, 2=Torre, 3=Piso, 4=Área/Unidad funcional, 5=Habitación/Consultorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'IdLocationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el tipo de ubicacion   1- Sede o Sucursal  2-Torre  3-Piso  4-Area  5-Habitacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'IdLocationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'IdLocationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción legible (VARCHAR 50) de la ubicación: razón social de sede/sucursal, identificación de torre, piso, área funcional o número de habitación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Ubicacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único o identificador textual (VARCHAR 20) de la ubicación, empleado para búsquedas rápidas y reportes operacionales.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la ubicacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ciudad (FK a Common.City) donde se ubica la sede, sucursal u oficina del centro de atención.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'CityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la ciudad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'CityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro padre (FK autorreferencial a Location.Id) para jerarquías de ubicaciones: sede → torre → piso → área → habitación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'IdParent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro padre.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'IdParent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'IdParent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico, INT IDENTITY) de la ubicación o lugar físico del centro de atención, sede, sucursal, torre, piso, área o habitación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de ubicaciones.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maestro de ubicaciones o sedes del sistema, organizadas de forma jerárquica (país, departamento, municipio, sede, piso, habitación, etc.). Permite identificar dónde se presta la atención médica o administrativa dentro de la red de centros.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Location';
