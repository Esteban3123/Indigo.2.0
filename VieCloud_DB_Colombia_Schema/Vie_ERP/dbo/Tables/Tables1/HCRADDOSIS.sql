CREATE TABLE [dbo].[HCRADDOSIS] (
    [ID]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADESQUEMAS]   INT             NOT NULL,
    [FECHAREGISTRO]     DATETIME        NOT NULL,
    [USUARIOREGISTRO]   CHAR (20)       NOT NULL,
    [DOSISTUMOR1]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR2]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR3]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR4]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR5]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR6]       DECIMAL (18, 2) NULL,
    [TOTALDOSIS]        DECIMAL (18, 2) NULL,
    [CODESPECI]         CHAR (3)        NULL,
    [TIEMPOUNIMONITOR]  INT             NULL,
    [OBSERVACION]       VARCHAR (MAX)   NULL,
    [TIPOBRAQUITERAPIA] INT             NULL,
    [VOLUTRATAMIEN]     INT             NULL,
    [IDAGEQUIPTRA]      INT             NULL,
    [DOSISPRESCRITA]    INT             NULL,
    [IDCITA]            INT             NOT NULL,
    [CODCENATEDOSIS]    CHAR (10)       NULL,
    CONSTRAINT [PK_HCRADDOSIS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADDOSIS_ADCENATEN] FOREIGN KEY ([CODCENATEDOSIS]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADDOSIS_AGEQUIPTRA] FOREIGN KEY ([IDAGEQUIPTRA]) REFERENCES [dbo].[AGEQUIPTRA] ([ID]),
    CONSTRAINT [FK_HCRADDOSIS_HCRADTUMORES] FOREIGN KEY ([IDHCRADESQUEMAS]) REFERENCES [dbo].[HCRADESQUEMAS] ([ID]),
    CONSTRAINT [FK_HCRADDOSIS_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO




GO



GO


GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (unidad funcional, sede) donde fue aplicada la dosis de radioterapia. FK a ADCENATEN. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'CODCENATEDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de Atención en el cual fue aplicada la dosis de Radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'CODCENATEDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'CODCENATEDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cita médica de atención/consulta asociada al registro de dosis de radioterapia. FK a AGASICITA. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cita medica con la que se registro la dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prescripción de dosis en braquiterapia: 1=Isocentro, 2=Piel, 3=Isocentro y piel, 4=Puntos A, 5=Mucosa vaginal, 6=Mucosa esofágica. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISPRESCRITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Isocentro  2 - Piel  3  - Isocentro y piel  4  - Puntos A  5 - Mucosa vaginal  6 - Mucosa esofágica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISPRESCRITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISPRESCRITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del equipo de tratamiento (acelerador lineal, fuente de braquiterapia) utilizado para aplicar la radioterapia. FK a AGEQUIPTRA. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Equipo de Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen de tratamiento: 1=Primario solo, 2=Ninguno. Indica si la dosis se aplica solo al tumor primario u otro esquema. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'VOLUTRATAMIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Primario Solo  2 - Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'VOLUTRATAMIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'VOLUTRATAMIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de técnica de braquiterapia aplicada: 1=Intracavitaria, 2=Intraluminal, 3=Intersticial. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TIPOBRAQUITERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Intracavitaria  2 - Intraluminal  3 - Intersticial  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TIPOBRAQUITERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TIPOBRAQUITERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas, incidencias o comentarios adicionales sobre el registro de dosis de radioterapia/braquiterapia. VARCHAR(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo total de exposición a radiación en unidades de monitor (minutos equivalentes). Parámetro de control de dosis. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TIEMPOUNIMONITOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo en Minutos Monitor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TIEMPOUNIMONITOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TIEMPOUNIMONITOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica (oncología, radioterapia, etc.) asociada al tratamiento. FK a INESPECIA. CHAR(3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis total acumulada (Gy, cGy) calculada como suma de dosis aplicadas a todos los tumores tratados. DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TOTALDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de Dosis que es la sumatoria de todos los tumores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TOTALDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TOTALDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de radiación aplicada al tumor sexto en el esquema de tratamiento. DECIMAL(18,2). NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Aplicada Tumor 6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de radiación aplicada al tumor quinto en el esquema de tratamiento. DECIMAL(18,2). NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Aplicada Tumor 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de radiación aplicada al tumor cuarto en el esquema de tratamiento. DECIMAL(18,2). NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Aplicada Tumor 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de radiación aplicada al tumor tercero en el esquema de tratamiento. DECIMAL(18,2). NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Aplicada Tumor 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de radiación aplicada al tumor segundo en el esquema de tratamiento. DECIMAL(18,2). NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Aplicada Tumor 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de radiación aplicada al primer tumor en el esquema de tratamiento. DECIMAL(18,2). NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Aplicada Tumor 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (profesional de la salud, técnico) que registró el evento de aplicación de dosis. CHAR(20). Audit trail.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró la administración de la dosis de radioterapia. DATETIME. Audit trail.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema de tratamiento de radioterapia/braquiterapia al cual pertenece este registro de dosis. FK a HCRADESQUEMAS. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Esquema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de aplicación de dosis. IDENTITY(1,1). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de dosis aplicadas en sesiones de radioterapia y braquiterapia oncológica. Almacena las dosis por tumor, la dosis total prescrita, el tipo de tratamiento y el equipo utilizado por cita, dentro de un esquema de tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS';
