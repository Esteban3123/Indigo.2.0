CREATE TABLE [dbo].[HCINIMAG] (
    [oid]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RISTRICAB_CODCONSEC] NVARCHAR (50)  NULL,
    [RISTRICAB_IPCODPACI] NVARCHAR (50)  NULL,
    [LECTURA]             NVARCHAR (MAX) NULL,
    [LINK_IMAGEN]         NVARCHAR (MAX) NULL,
    [ESTADO]              NVARCHAR (10)  NULL,
    CONSTRAINT [PK_HCINIMAG] PRIMARY KEY CLUSTERED ([oid] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Imágenes diagnósticas de historia clínica: almacena los registros de imágenes médicas (radiografías, ecografías, tomografías, etc.) asociadas a las atenciones de los pacientes, incluyendo la lectura o interpretación del radiólogo y el enlace al archivo de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de imagen diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'oid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'oid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo de la orden o solicitud de imagen diagnóstica a la que pertenece este resultado (código de cabecera de la solicitud de imágenes).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'RISTRICAB_CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'RISTRICAB_CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula o código del paciente al que pertenece la imagen diagnóstica, identificación del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'RISTRICAB_IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'RISTRICAB_IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de la lectura o interpretación médica de la imagen, informe del radiólogo o especialista que analizó el estudio de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'LECTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'LECTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enlace o ruta al archivo de la imagen médica (URL o path del archivo DICOM, JPG u otro formato de imagen diagnóstica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'LINK_IMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'LINK_IMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del registro de la imagen diagnóstica (por ejemplo: activo, inactivo, pendiente, entregado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINIMAG', @level2type = N'COLUMN', @level2name = N'ESTADO';
