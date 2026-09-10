CREATE TABLE [dbo].[HCIMARADIO] (
    [ID]          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]      VARCHAR (3)     NOT NULL,
    [NOMBREFOTO]  VARCHAR (200)   NOT NULL,
    [ESTADO]      INT             NOT NULL,
    [FOTO]        VARBINARY (MAX) NULL,
    [FECREGISTRO] DATETIME        NOT NULL,
    [USUCREACION] CHAR (20)       NOT NULL,
    [TIPO]        INT             NULL,
    CONSTRAINT [PK_HCIMARADIO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCIMARADIO]
    ON [dbo].[HCIMARADIO]([CODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de procedimiento de imagen radiológica: 1=Radioterapia, 2=Informe de otros procedimientos (visualización en dashboard médicos). INT, clasificador de modalidad terapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo: 1.Radioterapia, 2.Informe otros procedimientos (dashboar médicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de imagen radiológica. CHAR(20), auditoria de creación, profesional de salud que ingresó la información.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la imagen radiológica en el sistema. DATETIME, marca temporal de ingreso del procedimiento de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Archivo binario de la imagen radiológica o radioterapia (DICOM, JPG, PNG). VARBINARY(MAX), datos PII/Identification_Ofuscado, evidencia digital del procedimiento de radiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'FOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Foto De Radioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'FOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'FOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de disponibilidad de la foto radiológica: 1=Sí (activa/válida), 2=No (inactiva/descartada). INT, indicador booleano de vigencia del archivo de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Foto 1 sI 2 No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo o identificador del archivo de imagen radiológica. VARCHAR(200), referencia legible de la foto/estudio de radioterapia o procedimiento radiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'NOMBREFOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Foto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'NOMBREFOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'NOMBREFOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la imagen radiológica asignado por el sistema. VARCHAR(3), identificador corto del estudio de imagen para búsqueda rápida en consultas clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo autoincrementable) del registro de imagen radiológica. INT IDENTITY, clave primaria de la tabla HCIMARADIO, referencia interna para relaciones con historias clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de imágenes radiológicas asociadas a la historia clínica. Almacena archivos de imágenes (radiografías, ecografías u otros estudios de imagen) junto con su clasificación, estado y datos de auditoría de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO';
