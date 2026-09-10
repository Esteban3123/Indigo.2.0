CREATE TABLE [dbo].[RCTIPACCEVE] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]              VARCHAR (3)   NOT NULL,
    [Nombre]              VARCHAR (100) NOT NULL,
    [Estado]              BIT           NOT NULL,
    [FechaCreacion]       DATETIME      NOT NULL,
    [UsuarioCreacion]     CHAR (20)     NOT NULL,
    [FechaModificacion]   DATETIME      NULL,
    [UsuarioModificacion] CHAR (20)     NULL,
    CONSTRAINT [PK_RCTIPACCEVE] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (CHAR 20) que realizó la última modificación del registro; auditoría de cambios; nullable cuando no hay ediciones posteriores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario que modificó el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro; timestamp de auditoría; nullable si el registro nunca fue editado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'FechaModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'FechaModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'FechaModificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (CHAR 20) que creó originalmente el registro; auditoría de origen; requerido en toda creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creo el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro; timestamp de inicio; marca de auditoría obligatoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'FechaCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'FechaCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'FechaCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (BIT): 1=Activo, 0=Inactivo; indica si el tipo de acceso a evento está habilitado o deshabilitado en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro  1-> Activo  0-> Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Estado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) del tipo de acceso a evento; etiqueta legible para clasificación de accesos y eventos en la atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (VARCHAR 3) del tipo de acceso a evento; clave corta única para búsqueda y referencia rápida en RIPS y procesos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY); clave primaria del registro de tipos de acceso a evento; no replicable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de accidentes o eventos adversos utilizados en el módulo de registro de accidentes. Permite clasificar los diferentes tipos de eventos (laborales, de tránsito, en el hogar, etc.) que se registran en la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCTIPACCEVE';
