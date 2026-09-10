CREATE TABLE [dbo].[ODONTOMOTIVOSNOCUMPLITRATA] (
    [ID]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]        VARCHAR (3)  NOT NULL,
    [NOMBRE]        VARCHAR (50) NOT NULL,
    [ESTADO]        BIT          NOT NULL,
    [FECHACREACION] DATETIME     NOT NULL,
    [USUARIOCREA]   CHAR (20)    NOT NULL,
    [FECHAMODIFI]   DATETIME     NULL,
    [USUMODIFICA]   CHAR (20)    NULL,
    CONSTRAINT [PK_ODONTOMOTIVOSNOCUMPLITRATA] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realiza la última modificación del registro (auditoría, VARCHAR(20), nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario quien lo modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro (DATETIME, nullable, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que crea el registro inicial (auditoría, CHAR(20), requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario quien  lo crea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro (DATETIME, requerido, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del motivo de no cumplimiento (BIT: 1=Activo, 0=Inactivo, booleano)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estado  1 =Activo     0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del motivo de no cumplimiento odontológico (VARCHAR(50))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el nombre o la descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de tres dígitos para motivos de no tratamiento/no cumplimiento odontológico (VARCHAR(3), PK lógica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código Motivos No Tratamientos Odontologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de la tabla (INT IDENTITY, PK, no replicable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de motivos por los cuales un paciente no cumple o abandona el tratamiento odontológico. Permite registrar y clasificar las razones de incumplimiento en los planes de tratamiento dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA';
