CREATE TABLE [dbo].[ODOPERPAS] (
    [ID]         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROL]  INT             NOT NULL,
    [NUMDIENTE]  TINYINT         NOT NULL,
    [MARGEN1]    DECIMAL (18, 1) NULL,
    [MARGEN2]    DECIMAL (18, 1) NULL,
    [MARGEN3]    DECIMAL (18, 1) NULL,
    [SURCO1]     DECIMAL (18, 1) NULL,
    [SURCO2]     DECIMAL (18, 1) NULL,
    [SURCO3]     DECIMAL (18, 1) NULL,
    [INSERCION1] DECIMAL (18, 1) NULL,
    [INSERCION2] DECIMAL (18, 1) NULL,
    [INSERCION3] DECIMAL (18, 1) NULL,
    [SANGRADO1]  BIT             NULL,
    [SANGRADO2]  BIT             NULL,
    [SANGRADO3]  BIT             NULL,
    [FURCACION1] CHAR (1)        NULL,
    [FURCACION2] CHAR (1)        NULL,
    CONSTRAINT [PK_ODOPERIPAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODOPERIPAS_ODOPERICTR] FOREIGN KEY ([IDCONTROL]) REFERENCES [dbo].[ODOPERCTR] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de furcación (Furca Inicial=1, Furca Abierta=2, Furca Completa=3) en segundo sitio de medición; evaluación de bifurcación radicular en periodontograma, CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'FURCACION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Furcación2    1: Furca Inicial  2: Furca abierta  3: Furca Completa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'FURCACION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'FURCACION2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de furcación (Furca Inicial=1, Furca Abierta=2, Furca Completa=3) en primer sitio de medición; evaluación de bifurcación radicular en periodontograma, CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'FURCACION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Furcación1     1: Furca Inicial  2: Furca abierta  3: Furca Completa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'FURCACION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'FURCACION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado (BIT) en superficie lingual del diente; tercer sitio de evaluación en examen periodontal, señal de inflamación gingival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado (BIT) en superficie vestibular del diente; segundo sitio de evaluación en examen periodontal, señal de inflamación gingival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado (BIT) en superficie palatina del diente; primer sitio de evaluación en examen periodontal, señal de inflamación gingival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición de nivel de inserción clínica (mm, DECIMAL 18.1) en superficie lingual; tercer sitio en periodontograma, indicador de pérdida de inserción periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición de nivel de inserción clínica (mm, DECIMAL 18.1) en superficie vestibular; segundo sitio en periodontograma, indicador de pérdida de inserción periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición de nivel de inserción clínica (mm, DECIMAL 18.1) en superficie palatina; primer sitio en periodontograma, indicador de pérdida de inserción periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición de profundidad de surco/bolsa periodontal (mm, DECIMAL 18.1) en superficie lingual; tercer sitio de evaluación en diagnóstico de enfermedad periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición de profundidad de surco/bolsa periodontal (mm, DECIMAL 18.1) en superficie vestibular; segundo sitio de evaluación en diagnóstico de enfermedad periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición de profundidad de surco/bolsa periodontal (mm, DECIMAL 18.1) en superficie palatina; primer sitio de evaluación en diagnóstico de enfermedad periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición de recesión de margen gingival (mm, DECIMAL 18.1) en superficie lingual; tercer sitio en periodontograma, indicador de retracción de encía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Margen3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición de recesión de margen gingival (mm, DECIMAL 18.1) en superficie vestibular; segundo sitio en periodontograma, indicador de retracción de encía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda margen2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición de recesión de margen gingival (mm, DECIMAL 18.1) en superficie palatina; primer sitio en periodontograma, indicador de retracción de encía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda margen1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número identificador del diente evaluado (TINYINT 1-32); pieza dental en nomenclatura odontológica para referencia en periodontograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Numero del diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de control periodontal (FK a ODOPERCTR.ID); vinculación con examen periodontal y paciente, referencia de atención odontológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de control de periodontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY); clave primaria autoincrementada de registro de medición periodontal por diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de periodoncia por diente (periodontograma): guarda las mediciones clínicas periodontales de cada diente del paciente, incluyendo profundidad de surco, nivel de inserción, margen gingival, sangrado al sondaje y clasificación de furcación. Se usa en odontología para evaluar la salud periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS';
