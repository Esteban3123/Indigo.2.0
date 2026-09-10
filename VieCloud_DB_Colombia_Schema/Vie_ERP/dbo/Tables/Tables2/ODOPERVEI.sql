CREATE TABLE [dbo].[ODOPERVEI] (
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
    CONSTRAINT [PK_ODOPERIVEI] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea Mucogingival (LMG) superficie lingual (dorsal/posterior) del diente. Decimal(18,1) en mm. Indicador periodontal de encía adherida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'LMG3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda LMG3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'LMG3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'LMG3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea Mucogingival (LMG) superficie vestibular (labial/externa) del diente. Decimal(18,1) en mm. Indicador periodontal de encía adherida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'LMG2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda LMG2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'LMG2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'LMG2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea Mucogingival (LMG) superficie palatina (paladar/interna superior) del diente. Decimal(18,1) en mm. Indicador periodontal de encía adherida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'LMG1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda LMG1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'LMG1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'LMG1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de Furcación del diente: 1=Furca Inicial (sonda entra), 2=Furca Abierta (sonda atraviesa parcial), 3=Furca Completa (sonda atraviesa total). Char(1). Examen periodontal molares.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'FURCACION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Furcación1    1: Furca Inicial  2: Furca abierta  3: Furca Completa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'FURCACION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'FURCACION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado gingival (BIT) en superficie lingual/dorsal del diente durante sondaje periodontal. Signo de inflamación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SANGRADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SANGRADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SANGRADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado gingival (BIT) en superficie vestibular/labial del diente durante sondaje periodontal. Signo de inflamación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SANGRADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SANGRADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SANGRADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sangrado gingival (BIT) en superficie palatina/interna superior del diente durante sondaje periodontal. Signo de inflamación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SANGRADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Sangrado1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SANGRADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SANGRADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de inserción (inserción del tejido conectivo) en superficie lingual del diente. Decimal(18,1) en mm. Medida periodontal de salud gingival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'INSERCION3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'INSERCION3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'INSERCION3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de inserción (inserción del tejido conectivo) en superficie vestibular del diente. Decimal(18,1) en mm. Medida periodontal de salud gingival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'INSERCION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'INSERCION2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'INSERCION2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de inserción (inserción del tejido conectivo) en superficie palatina del diente. Decimal(18,1) en mm. Medida periodontal de salud gingival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'INSERCION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Insercion1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'INSERCION1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'INSERCION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad de surco gingival (profundidad de sondaje) en superficie lingual del diente. Decimal(18,1) en mm. Indicador de salud periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SURCO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SURCO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SURCO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad de surco gingival (profundidad de sondaje) en superficie vestibular del diente. Decimal(18,1) en mm. Indicador de salud periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SURCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SURCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SURCO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad de surco gingival (profundidad de sondaje) en superficie palatina del diente. Decimal(18,1) en mm. Indicador de salud periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SURCO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Surco1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SURCO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'SURCO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Margen gingival (nivel del borde de la encía) en superficie lingual del diente. Decimal(18,1) en mm. Parámetro de salud gingival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'MARGEN3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Margen3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'MARGEN3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'MARGEN3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Margen gingival (nivel del borde de la encía) en superficie vestibular del diente. Decimal(18,1) en mm. Parámetro de salud gingival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'MARGEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda margen2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'MARGEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'MARGEN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Margen gingival (nivel del borde de la encía) en superficie palatina del diente. Decimal(18,1) en mm. Parámetro de salud gingival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'MARGEN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda margen1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'MARGEN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'MARGEN1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador del diente examinado. TinyInt (1-32). Sistema internacional de numeración dental para periodontograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a tabla control de periodontograma. Int. Relaciona datos periodontales con control de atención odontológica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de control de periodontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) autoincrementable. Int Identity(1,1). Clave primaria del registro de hallazgo periodontal por diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros del periodontograma (sondaje periodontal) por diente: almacena las mediciones de margen gingival, profundidad de surco, nivel de inserción, sangrado al sondaje, furcación y nivel de la línea mucogingival para cada cara del diente evaluado en un control odontológico periodontal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERVEI';
