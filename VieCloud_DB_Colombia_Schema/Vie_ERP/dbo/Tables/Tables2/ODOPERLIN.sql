CREATE TABLE [dbo].[ODOPERLIN] (
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
    CONSTRAINT [PK_ODOPERIPAI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODOPERIPAI_ODOPERICTR] FOREIGN KEY ([IDCONTROL]) REFERENCES [dbo].[ODOPERCTR] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea mucogingival en mm (DECIMAL 18.1), superficie lingual; distancia entre encía adherida y móvil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'LMG3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda LMG3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'LMG3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'LMG3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea mucogingival en mm (DECIMAL 18.1), superficie vestibular; distancia entre encía adherida y móvil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'LMG2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda LMG2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'LMG2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'LMG2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea mucogingival en mm (DECIMAL 18.1), superficie palatina; distancia entre encía adherida y móvil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'LMG1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda LMG1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'LMG1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'LMG1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de furcación (CHAR 1): 0=Sin furcación, 1=Furca inicial, 2=Furca abierta, 3=Furca completa; indica afectación de zona interradicular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'FURCACION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Furcacion    1: Furca Inicial  2: Furca abierta  3: Furca Completa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'FURCACION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'FURCACION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado al sondaje (BIT: 0=No, 1=Sí), superficie lingual; signo inflamatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SANGRADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SANGRADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SANGRADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado al sondaje (BIT: 0=No, 1=Sí), superficie vestibular; signo inflamatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SANGRADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SANGRADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SANGRADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado al sondaje (BIT: 0=No, 1=Sí), superficie palatina; signo inflamatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SANGRADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SANGRADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SANGRADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de inserción clínica en mm (DECIMAL 18.1), superficie lingual del diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'INSERCION3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'INSERCION3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'INSERCION3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de inserción clínica en mm (DECIMAL 18.1), superficie vestibular del diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'INSERCION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'INSERCION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'INSERCION2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de inserción clínica en mm (DECIMAL 18.1), superficie palatina del diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'INSERCION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'INSERCION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'INSERCION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad del surco/bolsa periodontal en mm (DECIMAL 18.1), superficie lingual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SURCO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SURCO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SURCO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad del surco/bolsa periodontal en mm (DECIMAL 18.1), superficie vestibular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SURCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SURCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SURCO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad del surco/bolsa periodontal en mm (DECIMAL 18.1), superficie palatina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SURCO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SURCO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'SURCO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medida en milímetros (DECIMAL 18.1) del margen gingival superficie lingual del diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'MARGEN3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Margen3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'MARGEN3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'MARGEN3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medida en milímetros (DECIMAL 18.1) del margen gingival superficie vestibular/bucal del diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'MARGEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda margen2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'MARGEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'MARGEN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medida en milímetros (DECIMAL 18.1) del margen gingival superficie palatina/palatal del diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'MARGEN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda margen1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'MARGEN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'MARGEN1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del diente (TINYINT, 1-32) evaluado en el periodontograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK → ODOPERCTR.ID) que vincula el registro al control/examen periodontal del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de control de periodontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la línea operatoria en periodontograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de mediciones periodontales por diente en el odontograma periodontal del paciente. Guarda los valores clínicos de sondaje, márgenes gingivales, niveles de inserción, sangrado al sondaje y furcación para cada diente evaluado en una consulta odontológica de periodoncia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERLIN';
