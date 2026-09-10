CREATE TABLE [dbo].[HCMOTNOREPC] (
    [ID]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]      VARCHAR (3)    NOT NULL,
    [DESCRIPCION] VARCHAR (1000) NOT NULL,
    [USUCREACION] CHAR (20)      NOT NULL,
    [FECCREACION] DATETIME       NOT NULL,
    [USUMODIFICA] CHAR (20)      NULL,
    [FECMODIFICA] DATETIME       NULL,
    [ESTADO]      BIT            NOT NULL,
    CONSTRAINT [PK_HCMOTNOREPC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCMOTNOREPC_HCMOTNOREPC] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCMOTNOREPC] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del motivo de no realización de procedimiento (BIT: 1=Activo, 0=Inactivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro del motivo de no realización (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'FECMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'FECMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'FECMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del motivo de no realización de procedimiento (CHAR 20, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del motivo de no realización (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'FECCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'FECCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'FECCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del motivo de no realización de procedimiento (CHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del motivo por el cual no se realizó el procedimiento, examen, intervención o atención (VARCHAR 1000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de 3 caracteres que identifica el tipo de motivo de no realización (VARCHAR 3, clave de negocio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del motivo de no realización de procedimiento (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de motivos por los cuales no se reporta una historia clínica o un evento clínico. Permite clasificar las razones de no reporte en procesos de auditoría y gestión de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC';
