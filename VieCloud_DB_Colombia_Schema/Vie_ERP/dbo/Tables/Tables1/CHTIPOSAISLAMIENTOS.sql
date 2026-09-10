CREATE TABLE [dbo].[CHTIPOSAISLAMIENTOS] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]              VARCHAR (3)   NOT NULL,
    [Nombre]              VARCHAR (100) NOT NULL,
    [Color]               VARCHAR (50)  NOT NULL,
    [Estado]              BIT           NOT NULL,
    [FechaCreacion]       DATETIME      NOT NULL,
    [UsuarioCreacion]     CHAR (20)     NOT NULL,
    [FechaModificacion]   DATETIME      NULL,
    [UsuarioModificacion] CHAR (20)     NULL,
    CONSTRAINT [PK_CHTIPOSAISLAMIENTOS] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/identificación, CHAR 20) que realizó la última modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quein modifico el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro (DATETIME, NULL si no fue editado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'FechaModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha modificacion del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'FechaModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'FechaModificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/identificación, CHAR 20) que originó la creación del tipo de aislamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien creo el registo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del tipo de aislamiento (DATETIME NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'FechaCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'FechaCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'FechaCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de vigencia del tipo de aislamiento: True = Activo/Disponible, False = Inactivo/Deshabilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'True = Activo  False = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Estado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color de visualización asignado al tipo de aislamiento (VARCHAR 50) para interfaz de usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del tipo de aislamiento (VARCHAR 100), ej: aislamiento respiratorio, contacto, gotitas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o Descripcion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 3) que identifica el tipo de aislamiento de forma única y breve', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la tabla de tipos de aislamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de aislamiento clínico utilizados en hospitalización. Registra las categorías de aislamiento que se pueden asignar a un paciente (por ejemplo: aislamiento de contacto, respiratorio, estricto), incluyendo su color de identificación visual y estado activo/inactivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOSAISLAMIENTOS';
