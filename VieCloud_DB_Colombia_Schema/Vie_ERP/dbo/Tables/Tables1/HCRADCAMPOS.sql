CREATE TABLE [dbo].[HCRADCAMPOS] (
    [ID]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADPLANTRATA] INT             NOT NULL,
    [NUMCAMPO]         VARCHAR (5)     NOT NULL,
    [NOMCAMPO]         VARCHAR (100)   NOT NULL,
    [Y1]               NUMERIC (18, 2) NULL,
    [Y2]               NUMERIC (18, 2) NULL,
    [X1]               NUMERIC (18, 2) NULL,
    [X2]               NUMERIC (18, 2) NULL,
    [ENERGIA]          VARCHAR (5)     NULL,
    [GANTRY]           NUMERIC (18, 2) NULL,
    [COLIMADOR]        NUMERIC (18, 2) NULL,
    [CAMILLA]          NUMERIC (18, 2) NULL,
    [ACCESORIOS]       VARCHAR (5)     NULL,
    [MLC]              NUMERIC (18, 2) NULL,
    [BOLUS]            NUMERIC (18, 2) NULL,
    [DFP]              NUMERIC (18, 2) NULL,
    [PROFUN]           NUMERIC (18, 2) NULL,
    [UM]               NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_HCRADCAMPOS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada X2 de posicionamiento geométrico del campo de radiación en plano (numeric 18,2, mm o cm)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'X2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'x2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'X2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'X2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada X1 de posicionamiento geométrico del campo de radiación en plano (numeric 18,2, mm o cm)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'X1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'x1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'X1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'X1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada Y2 de posicionamiento geométrico del campo de radiación en plano (numeric 18,2, mm o cm)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'Y2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'y2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'Y2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'Y2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada Y1 de posicionamiento geométrico del campo de radiación en plano (numeric 18,2, mm o cm)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'Y1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'y1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'Y1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'Y1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del campo de radiación, etiqueta del área o zona irradiada (VARCHAR 100, ej: ''''Pulmón derecho'''', ''''Próstata AP-PA'''')', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'NOMCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'NOMCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'NOMCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del campo de radiación, identificador dentro del plan (VARCHAR 5, ej: ''''001'''', ''''002'''')', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'NUMCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del Campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'NUMCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'NUMCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla HCRADPLANTRATA (FK), vincula campo a plan de tratamiento radiante específico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADPLANTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabl de Plan de Tratamientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADPLANTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADPLANTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK), consecutivo autoincrementable de registro de campo de radiación en plan de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campos de radioterapia asociados a un plan de tratamiento. Cada registro define los parámetros técnicos de un campo de irradiación (haz de radiación): geometría del colimador, ángulos del equipo, accesorios y dosis, usados por el servicio de radioterapia para ejecutar el plan de tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Energía del haz de radiación utilizada en el campo (por ejemplo, 6MV, 15MV), que determina la penetración de la radiación en el tejido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'ENERGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'ENERGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ángulo de rotación del gantry (brazo del acelerador lineal) en grados, que define la dirección de entrada del haz de radiación al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'GANTRY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'GANTRY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ángulo de rotación del colimador del equipo en grados, que orienta las láminas o mandíbulas que conforman el campo de irradiación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'COLIMADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'COLIMADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ángulo de rotación de la camilla (mesa de tratamiento) en grados, que ajusta la posición del paciente respecto al haz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'CAMILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'CAMILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador de accesorios físicos utilizados en el campo (bandejas, bloques, cuñas físicas u otros dispositivos de modificación del haz).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'ACCESORIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'ACCESORIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición o parámetro del colimador multiláminas (Multi-Leaf Collimator), dispositivo que conforma la forma irregular del campo de irradiación para proteger tejido sano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'MLC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'MLC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grosor o parámetro del bolo utilizado en el campo; material compensador colocado sobre el paciente para modificar la distribución de dosis superficial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'BOLUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'BOLUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distancia foco-piel (distancia desde la fuente de radiación hasta la superficie del paciente) en centímetros, parámetro geométrico clave para el cálculo de dosis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'DFP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'DFP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad de referencia en el tejido (en centímetros) a la que se prescribe o calcula la dosis del campo de irradiación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'PROFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'PROFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidades monitor (UM) prescritas para el campo; valor que controla el tiempo de irradiación del acelerador lineal para administrar la dosis planificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'UM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'UM';
