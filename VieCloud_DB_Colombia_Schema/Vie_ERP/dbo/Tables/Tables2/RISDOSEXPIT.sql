CREATE TABLE [dbo].[RISDOSEXPIT] (
    [MODRAD]     VARCHAR (16)   NULL,
    [KVP]        VARCHAR (16)   NULL,
    [XRAYTUCUA]  VARCHAR (16)   NULL,
    [TIEMPEXP]   VARCHAR (12)   NULL,
    [TIPOFILT]   VARCHAR (16)   NULL,
    [MATFILT]    VARCHAR (16)   NULL,
    [COMDOSIS]   VARCHAR (1042) NULL,
    [AUTO]       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RISRADAUTO] INT            NOT NULL,
    CONSTRAINT [PK_RISDOSEXPIT] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del registro padre en tabla RISRAD; referencia a estudio radiológico, examen radiológico, acto radiológico o procedimiento de imagen diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'RISRADAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AUTO de la tabla RISRAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'RISRADAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'RISRADAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico, clave primaria de la tabla; incremento automático de secuencia de exposición radiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios sobre dosis de radiación, condiciones especiales o notas sobre exposición radiológica durante el episodio; campo de texto libre para observaciones clínicas; DICOM TAG (0040,0310)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'COMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comments on Radiation Dose:  User-defined comments on any special conditions related to radiation dose encountered during during the episode described by this Exposure Dose Sequence Item.  DICOM TAG (0040,0310)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'COMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'COMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Material del filtro absorbente de rayos X utilizado; puede ser múltiple (aluminio, cobre, molibdeno); especificación técnica de atenuación de radiación; DICOM TAG (0018,7050)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'MATFILT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Filter Material:  The X-Ray absorbing material used in the filter. May be multi-valued. See Section C.8.7.10 and Section C.8.15.3.9 (for enhanced CT) for Defined Terms.  DICOM TAG (0018,7050)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'MATFILT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'MATFILT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de filtro en haz de rayos X (cuñas, filtros especiales); clasificación del dispositivo de modulación radiológica; DICOM TAG (0018,1160)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'TIPOFILT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Filter Type:  Type of filter(s) inserted into the X-Ray beam (e.g., wedges). See Section C.8.7.10 and Section C.8.15.3.9 (for enhanced CT) for Defined Terms.  DICOM TAG (0018,1160)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'TIPOFILT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'TIPOFILT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de exposición a rayos X o fluoroscopia en milisegundos (ms); promedio en modo fluoroscopia continua; parámetro temporal de irradiación; DICOM TAG (0018,1150)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'TIEMPEXP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exposure Time:  Time of x-ray exposure or fluoroscopy in msec.  DICOM TAG (0018,1150)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'TIEMPEXP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'TIEMPEXP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Corriente del tubo de rayos X en microamperios (µA); intensidad promedio en modo fluoroscopia; parámetro de producción de radiación; DICOM TAG (0018,8151)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'XRAYTUCUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'X-Ray Tube Current in µA:  X-Ray Tube Current in µA. An average in the case of fluoroscopy (continuous radiation mode).  DICOM TAG (0018,8151)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'XRAYTUCUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'XRAYTUCUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Voltaje pico (kilovoltaje) del generador de rayos X; promedio en fluoroscopia continua; energía de penetración radiológica; DICOM TAG (0018,0060)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'KVP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'KVP:   Peak kilo voltage output of the x-ray generator used. An average in the case of fluoroscopy (continuous radiation mode).  DICOM TAG (0018,0060)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'KVP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'KVP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de radiación: CONTINUOUS (continuo) o PULSED (pulsado); tipo de exposición radiológica utilizada; DICOM TAG (0018,115A)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'MODRAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Radiation Mode:   Specifies X-Ray radiation mode.  Enumerated Values:    - CONTINUOUS  - PULSED  DICOM TAG (0018,115A)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'MODRAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'MODRAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros técnicos de exposición radiológica asociados a estudios de imágenes diagnósticas (rayos X). Guarda los valores de dosis, kilovoltaje, tiempo de exposición y filtros utilizados durante la adquisición de una imagen radiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT';
