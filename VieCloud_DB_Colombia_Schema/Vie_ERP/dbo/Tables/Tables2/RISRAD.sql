CREATE TABLE [dbo].[RISRAD] (
    [FLUTIEMP]      INT            NULL,
    [NUMEXPO]       INT            NULL,
    [DISTAFUEDECT]  VARCHAR (16)   NULL,
    [DISTAFUEPAC]   VARCHAR (16)   NULL,
    [DOSISENTRA]    INT            NULL,
    [DOSISENTRAMGY] VARCHAR (16)   NULL,
    [AREAEXP]       INT            NULL,
    [AREDOSPROD]    VARCHAR (16)   NULL,
    [COMDOSIS]      VARCHAR (1024) NULL,
    [RISORDENAUTO]  VARCHAR (20)   NOT NULL,
    [AUTO]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TAMPLACA]      VARCHAR (20)   NULL,
    [NUMPLACAS]     INT            NULL,
    [NUMDISPARO]    INT            NULL,
    [NUMPROCED]     INT            NULL,
    [PLACASBUENAS]  INT            NULL,
    [PLACASMALAS]   INT            NULL,
    [SOBREEXPO]     BIT            NULL,
    [MOTSOBREEXPO]  VARCHAR (200)  NULL,
    [PARTECUERPO]   VARCHAR (20)   NULL,
    [OBSERVAC]      VARCHAR (200)  NULL,
    CONSTRAINT [PK_RISRAD] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas del examen radiológico: notas adicionales, hallazgos relevantes o condiciones especiales registradas durante el procedimiento de imagen (VARCHAR 200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'OBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'observaciones ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'OBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'OBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parte del cuerpo o localización anatómica donde se realizó la radiografía: tórax, abdomen, extremidades, cráneo, pelvis, columna, etc. (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PARTECUERPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'parte del cuerpo en la que se tomo el examen ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PARTECUERPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PARTECUERPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo o código de justificación de la sobreexposición radiológica: razones clínicas por las que se excedió la dosis estándar (VARCHAR 200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'MOTSOBREEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del motivo de sobreexposicion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'MOTSOBREEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'MOTSOBREEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sobreexposición: si la dosis de radiación excedió los límites recomendados durante el procedimiento (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'SOBREEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'sobreexposicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'SOBREEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'SOBREEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de placas radiográficas rechazadas o defectuosas que no cumplieron criterios de calidad técnica (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PLACASMALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de placas malas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PLACASMALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PLACASMALAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de placas radiográficas aceptadas que cumplieron con estándares de calidad diagnóstica (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PLACASBUENAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de placas buenas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PLACASBUENAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PLACASBUENAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de procedimientos radiológicos realizados en esta orden o sesión (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de disparos o exposiciones individuales de radiación durante el procedimiento (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMDISPARO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de disparos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMDISPARO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMDISPARO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de placas radiográficas capturadas en el examen de imagen (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMPLACAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de placas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMPLACAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMPLACAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tamaño o formato estándar de las placas radiográficas utilizadas: 18x24, 20x25, digital, etc. (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'TAMPLACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tamaño de las placas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'TAMPLACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'TAMPLACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (PRIMARY KEY) de cada registro en la tabla RISRAD (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios sobre dosis de radiación: condiciones especiales, protecciones o consideraciones dosiméricas durante el procedimiento DICOM (0040,0310) (VARCHAR 1024)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'COMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comments on Radiation Dose:  User-defined comments on any special conditions related to radiation dose encountered during this Performed Procedure Step.  DICOM TAG (0040,0310)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'COMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'COMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto área-dosis acumulado: exposición radiológica total del paciente en dGy·cm² incluyendo fluoroscopia DICOM (0018,115E) (VARCHAR 16)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AREDOSPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Image and Fluoroscopy Area Dose Product:  Total area-dose-product to which the patient was exposed, accumulated over the complete Performed Procedure Step and measured in dGy*cm*cm, including fluoroscopy.  DICOM TAG (0018,115E)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AREDOSPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AREDOSPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área expuesta: dimensión típica en mm del campo irradiado en el detector (rectangular o circular) DICOM (0040,0303) (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AREAEXP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exposed Area:  Typical dimension of the exposed area at the detector plane. If Rectangular: row dimension followed by column; if Round: diameter. Measured in mm.  DICOM TAG (0040,0303)  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AREAEXP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AREAEXP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de entrada en mGy: promedio de dosis absorbida en la superficie del paciente durante el procedimiento DICOM (0040,8302) (VARCHAR 16)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DOSISENTRAMGY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entrance Dose in mGy:  Average entrance dose value measured in mGy at the surface of the patient­ during this Performed Procedure Step.  DICOM TAG (0040,8302)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DOSISENTRAMGY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DOSISENTRAMGY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de entrada en dGy: promedio de dosis absorbida en la superficie del paciente durante la exposición DICOM (0040,0302) (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DOSISENTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entrance Dose:   Average entrance dose value measured in dGy at the surface of the patient­ during this Performed Procedure Step.  DICOM TAG (0040,0302)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DOSISENTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DOSISENTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distancia fuente-paciente: distancia en mm desde la fuente de rayos X hasta la piel del paciente más cercana DICOM (0040,0306) (VARCHAR 16)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DISTAFUEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distance Source to Entrance:   Distance in mm from the source to the surface of the patient closest to the source during this Performed Procedure Step.  DICOM TAG (0040,0306)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DISTAFUEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DISTAFUEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distancia fuente-detector: distancia en mm desde la fuente de radiación al centro del detector de imagen DICOM (0018,1110) (VARCHAR 16)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DISTAFUEDECT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distance Source to Detector:  Distance in mm from the source to detector center.  DICOM TAG (0018,1110)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DISTAFUEDECT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DISTAFUEDECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de exposiciones: cantidad de veces que se activó la radiación incluyendo digitales y analógicas DICOM (0040,0301) (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Number of Exposures:  Total number of exposures made during this Performed Procedure Step. The number includes non-digital and digital expo­sures.   DICOM TAG (0040,0301)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo total de fluoroscopia: duración en segundos de la exposición a rayos X durante fluoroscopia (tiempo de pedal) DICOM (0040,0300) (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'FLUTIEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Time of Fluoroscopy:  Total duration of X-Ray exposure during fluoroscopy in seconds (pedal time) during this Performed Procedure Step.  DICOM TAG (0040,0300)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'FLUTIEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'FLUTIEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de exposición radiológica por procedimiento de imagen: captura los parámetros técnicos de cada disparo radiológico (dosis, tiempo de exposición, área, placas utilizadas, parte del cuerpo) para control de calidad, dosimetría y trazabilidad de estudios de radiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (clave foránea) que vincula este registro de exposición radiológica con la orden de radiología correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'RISORDENAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'RISORDENAUTO';
