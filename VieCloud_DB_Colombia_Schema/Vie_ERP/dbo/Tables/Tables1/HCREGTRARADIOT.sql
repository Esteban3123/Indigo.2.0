CREATE TABLE [dbo].[HCREGTRARADIOT] (
    [Id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]      VARCHAR (3)   NOT NULL,
    [Descripcion] VARCHAR (200) NOT NULL,
    [Estado]      INT           NOT NULL,
    [UsuCreacion] CHAR (20)     NOT NULL,
    [FecCreacion] DATETIME      NOT NULL,
    [UsuModifica] CHAR (20)     NULL,
    [FecModifica] DATETIME      NULL,
    CONSTRAINT [PK_HCREGTRARADIOT] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de región de tratamiento radioterápico (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'FecModifica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'FecModifica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'FecModifica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro de región de tratamiento radioterápico (CHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'UsuModifica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'UsuModifica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'UsuModifica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de región de tratamiento radioterápico (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'FecCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'FecCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'FecCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de región de tratamiento radioterápico (CHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'UsuCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'UsuCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'UsuCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo de la región de tratamiento en radioterapia, oncología (INT: 0=inactivo, 1=activo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Region Tratamiento Radioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Estado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la región anatómica para tratamiento radioterápico, oncología (ej: tórax, pelvis, cabeza) (VARCHAR 200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Descripcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Region Tratamiento Radioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Descripcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Descripcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único de la región de tratamiento radioterápico, oncología (VARCHAR 3, PK referencial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Region Tratamiento Radioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de la región de tratamiento radioterápico (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de trayectorias o trayectos de radioterapia utilizados en historia clínica. Registra los códigos y descripciones de las diferentes vías o rutas de aplicación de tratamientos de radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT';
