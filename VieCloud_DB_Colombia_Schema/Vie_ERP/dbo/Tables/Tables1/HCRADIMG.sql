CREATE TABLE [dbo].[HCRADIMG] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN] INT       NOT NULL,
    [IDHCIMARADIO] INT       NOT NULL,
    [CODCENATE]    CHAR (10) NOT NULL,
    [FECREGISTRO]  DATETIME  NOT NULL,
    [EXTARCHIVO]   CHAR (10) NOT NULL,
    [TIPO]         TINYINT   NOT NULL,
    CONSTRAINT [PK_HCRADIMG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADIMG_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADIMG_HCIMARADIO] FOREIGN KEY ([IDHCIMARADIO]) REFERENCES [dbo].[HCIMARADIO] ([ID]),
    CONSTRAINT [FK_HCRADIMG_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de imagen radiológica: 1=Imagen de radioterapia, 2=Imagen de braquiterapia; clasificación del procedimiento radiológico documentado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1  - Imagen de radioterapia  2  - Imagen de Braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión del archivo digital de imagen (ej: .dcm, .jpg, .png), formato técnico del archivo radiológico almacenado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'EXTARCHIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extensión del archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'EXTARCHIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'EXTARCHIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la imagen radiológica en el sistema, timestamp de ingreso del archivo de diagnóstico o radioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o unidad funcional (FK a ADCENATEN) donde se registró la imagen radiológica, identificación del sitio de captura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código centro atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la imagen radiológica capturada (FK a HCIMARADIO), referencia a la imagen de diagnóstico o tratamiento radiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'IDHCIMARADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Imagen, relación con la Tabla   HCIMARADIO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'IDHCIMARADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'IDHCIMARADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de radioterapia o braquiterapia (FK a HCRADORDEN), vinculación con la solicitud de procedimiento radiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Información de la radioterapia relación con la tabla HCRADINFOC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK), consecutivo autoincrementable de registro de imagen radiológica en historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Archivos adjuntos de imágenes radiológicas (radiografías, ecografías, tomografías, etc.) vinculados a órdenes de radiología e imágenes diagnósticas de la historia clínica. Registra qué archivo de imagen corresponde a cada orden de imagenología, en qué centro de atención se generó y el tipo y extensión del archivo almacenado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG';
