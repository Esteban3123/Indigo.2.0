CREATE TABLE [dbo].[ODOPERVES] (
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
    [LMG1]       DECIMAL (18, 1) NULL,
    [LMG2]       DECIMAL (18, 1) NULL,
    [LMG3]       DECIMAL (18, 1) NULL,
    CONSTRAINT [PK_ODOPERIVES] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea Marginal Gingival (LMG) superficie lingual del diente, medida en mm (DECIMAL 18,1), evaluación periodontal, odontología preventiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'LMG3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda LMG3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'LMG3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'LMG3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea Marginal Gingival (LMG) superficie vestibular del diente, medida en mm (DECIMAL 18,1), evaluación periodontal, odontología preventiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'LMG2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda LMG2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'LMG2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'LMG2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea Marginal Gingival (LMG) superficie palatina del diente, medida en mm (DECIMAL 18,1), evaluación periodontal, odontología preventiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'LMG1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda LMG1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'LMG1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'LMG1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación furcación radicular: 1=Furca inicial, 2=Furca abierta, 3=Furca completa (CHAR 1), diagnóstico periodontal, pérdida ósea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'FURCACION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Furcacion    1: Furca Inicial  2: Furca abierta  3: Furca Completa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'FURCACION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'FURCACION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado gingival superficie lingual (BIT), hallazgo clínico periodontal, inflamación, periodontitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SANGRADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SANGRADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SANGRADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado gingival superficie vestibular (BIT), hallazgo clínico periodontal, inflamación, periodontitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SANGRADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SANGRADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SANGRADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado gingival superficie palatina (BIT), hallazgo clínico periodontal, inflamación, periodontitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SANGRADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SANGRADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SANGRADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad inserción clínica (NIC) superficie lingual del diente, medida en mm (DECIMAL 18,1), evaluación periodontal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'INSERCION3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'INSERCION3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'INSERCION3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad inserción clínica (NIC) superficie vestibular del diente, medida en mm (DECIMAL 18,1), evaluación periodontal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'INSERCION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'INSERCION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'INSERCION2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad inserción clínica (NIC) superficie palatina del diente, medida en mm (DECIMAL 18,1), evaluación periodontal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'INSERCION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'INSERCION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'INSERCION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad de bolsa periodontal o surco gingival superficie lingual, medida en mm (DECIMAL 18,1), diagnóstico periodontitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SURCO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SURCO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SURCO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad de bolsa periodontal o surco gingival superficie vestibular, medida en mm (DECIMAL 18,1), diagnóstico periodontitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SURCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SURCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SURCO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad de bolsa periodontal o surco gingival superficie palatina, medida en mm (DECIMAL 18,1), diagnóstico periodontitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SURCO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SURCO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'SURCO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Altura margen gingival superficie lingual del diente, medida en mm (DECIMAL 18,1), evaluación retracción gingival, odontología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'MARGEN3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Margen3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'MARGEN3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'MARGEN3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Altura margen gingival superficie vestibular del diente, medida en mm (DECIMAL 18,1), evaluación retracción gingival, odontología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'MARGEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda margen2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'MARGEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'MARGEN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Altura margen gingival superficie palatina del diente, medida en mm (DECIMAL 18,1), evaluación retracción gingival, odontología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'MARGEN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda margen1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'MARGEN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'MARGEN1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador del diente según nomenclatura FDI (01-32), referencia dental, pieza dentaria (TINYINT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de control o evaluación periodontal, FK a tabla de control periodontograma, relación odontología preventiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de control de periodontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de examen odontológico vespertino (INT IDENTITY 1,1), clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de mediciones periodontales por superficie dental (vestibular/palatino/lingual) para cada diente evaluado en una historia clínica odontológica. Guarda los valores de profundidad de surco, margen gingival, inserción clínica, sangrado al sondaje y nivel de furcación, permitiendo el seguimiento del estado periodontal del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVES';
