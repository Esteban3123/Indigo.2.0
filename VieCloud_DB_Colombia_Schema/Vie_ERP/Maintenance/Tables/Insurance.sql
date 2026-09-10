CREATE TABLE [Maintenance].[Insurance] (
    [Id]        TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit]       VARCHAR (15) NOT NULL,
    [PersonId]  INT          NOT NULL,
    [Name]      VARCHAR (60) NOT NULL,
    [WebSite]   VARCHAR (80) NULL,
    [IdCity]    INT          NOT NULL,
    [State]     BIT          NOT NULL,
    [TimeStamp] ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Insurance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Insurance_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Insurance_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [UQ_Insurance__PersonId] UNIQUE NONCLUSTERED ([PersonId] ASC)
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_Insurance__State]
    ON [Maintenance].[Insurance]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (TIMESTAMP) para control de concurrencia y auditoría. Versión de fila SQL Server para gestionar bloqueos optimistas en actualización simultánea.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para controlar la concurrencia.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la aseguradora (BIT: 1=Activo, 0=Inactivo). Indicador de vigencia o habilitación del contrato/proveedor en el sistema.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ciudad (FK a Common.City) donde radica la aseguradora. Ubicación geográfica del domicilio principal o sede del tercero asegurador.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la ciudad relacionada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL o página web de la aseguradora (VARCHAR 80, opcional). Portal de consulta, afiliación o servicios en línea del asegurador.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'WebSite';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pagina web de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'WebSite';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'WebSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Denominación o razón social de la aseguradora (VARCHAR 60). Nombre comercial del plan o entidad aseguradora, searchable por nombre de contrato o EPS.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la persona (FK a Common.Person) vinculada al proveedor asegurador. Clave única que relaciona la aseguradora con su registro maestro de entidad.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la persona relacionada al proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT del tercero asegurador (VARCHAR 15). Número de Identificación Tributaria, documento único de la entidad aseguradora para propósitos fiscales y de facturación RIPS.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del tercero.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (TINYINT) de la aseguradora en el sistema ERP/EHR. Clave primaria de la tabla Maintenance.Insurance.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de aseguradora', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maestro de aseguradoras, EPS, ARS o entidades de seguros médicos. Contiene la información básica de cada aseguradora con la que opera la institución: NIT, nombre, ciudad sede y estado de vigencia del convenio o contrato.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance';
