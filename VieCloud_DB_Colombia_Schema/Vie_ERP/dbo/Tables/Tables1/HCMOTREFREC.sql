CREATE TABLE [dbo].[HCMOTREFREC] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]      VARCHAR (3)   NOT NULL,
    [DESCRIPCION] VARCHAR (200) NOT NULL,
    [ESTADO]      BIT           NOT NULL,
    [USUCREACION] CHAR (20)     NOT NULL,
    [USUMODIFICA] CHAR (20)     NULL,
    [FECCREACION] DATETIME      NOT NULL,
    [FECMODIFICA] DATETIME      NULL,
    CONSTRAINT [PK_HCMOTREFREC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de motivo de referencia (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'FECMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'FECMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'FECMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de motivo de referencia (DATETIME, obligatorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'FECCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'FECCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'FECCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del motivo de referencia (CHAR 20, nullable, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del motivo de referencia (CHAR 20, obligatorio, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del motivo de referencia: 1=Activo, 0=Inactivo (BIT, obligatorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del motivo de referencia o remisión del paciente a otra unidad funcional (VARCHAR 200, obligatorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de 3 dígitos que identifica el motivo de referencia (VARCHAR 3, obligatorio, clave única)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de cada motivo de referencia (INT IDENTITY, PK, obligatorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla maestra de motivos de referencia y contrarreferencia de pacientes. Registra los códigos y descripciones de las razones por las cuales un paciente es remitido a otra institución o devuelto al prestador de origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTREFREC';
