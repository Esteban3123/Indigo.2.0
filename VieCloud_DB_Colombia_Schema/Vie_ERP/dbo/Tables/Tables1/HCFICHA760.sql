CREATE TABLE [dbo].[HCFICHA760] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT          NOT NULL,
    [CODDIAGNO]           CHAR (4)     NULL,
    [DOLORCUELLO]         INT          NULL,
    [DOLORGARGANTA]       INT          NULL,
    [IMPOABRIR]           INT          NULL,
    [DISFAGIA]            INT          NULL,
    [CONVULSIONES]        INT          NULL,
    [CONTRAMUSC]          INT          NULL,
    [RIGIDEZMUSC]         INT          NULL,
    [ESPASGENER]          INT          NULL,
    [RIGIDEZNUCA]         INT          NULL,
    [AFECTNERVIOS]        INT          NULL,
    [TRISMUS]             INT          NULL,
    [OPISTOTONOS]         INT          NULL,
    [FIEBRE]              INT          NULL,
    [OTRO]                VARCHAR (50) NULL,
    [VERSION]             VARCHAR (20) NULL,
    CONSTRAINT [PK_HCFICHA760] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA760_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación epidemiológica. Si es nulo = primera versión; VARCHAR(20) para control de cambios y auditoría de actualizaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otros hallazgos clínicos no listados: descripción de síntomas o signos adicionales. VARCHAR(50), datos clínicos complementarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro ¿ Cuál? (DATOS CLÍNICOS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Fiebre presente? Indicador binario ternario (1=Sí, 2=No, 3=Desconocido). INT, signo vital clínico de infección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Fiebre? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Opistótonos presente? Rigidez dorsal extrema con hiperextensión. INT ternario (1=Sí, 2=No, 3=Desconocido). Signo neurológico grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'OPISTOTONOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Opistótonos? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'OPISTOTONOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'OPISTOTONOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Trismus presente? Contractura del masetero, imposibilidad de abrir mandíbula. INT ternario (1=Sí, 2=No, 3=Desconocido). Dato clínico neurológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'TRISMUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Trismus? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'TRISMUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'TRISMUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Afectación de nervios craneales? Signos de parálisis o disfunción craneal. INT ternario (1=Sí, 2=No, 3=Desconocido). Complicación neurológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'AFECTNERVIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Afectación de nervios craneales? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'AFECTNERVIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'AFECTNERVIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Rigidez de nuca presente? Meningismo o resistencia cervical a flexión. INT ternario (1=Sí, 2=No, 3=Desconocido). Signo meníngeo clásico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'RIGIDEZNUCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Rigidez de nuca? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'RIGIDEZNUCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'RIGIDEZNUCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Espasmos musculares generalizados? Contracciones involuntarias sistémicas. INT ternario (1=Sí, 2=No, 3=Desconocido). Manifestación convulsiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'ESPASGENER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Espasmos generalizados? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'ESPASGENER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'ESPASGENER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Rigidez muscular abdominal? Contractura de musculatura del abdomen. INT ternario (1=Sí, 2=No, 3=Desconocido). Dato clínico neurológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'RIGIDEZMUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Rigidez en músculos abdominales? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'RIGIDEZMUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'RIGIDEZMUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Contracciones musculares involuntarias? Espasmos o mioclonías. INT ternario (1=Sí, 2=No, 3=Desconocido). Signo neuromuscular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CONTRAMUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Contracciones musculares? (DATOS CLÍNICOS,(1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CONTRAMUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CONTRAMUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Convulsiones o crisis convulsivas? Eventos seizure/convulsivos. INT ternario (1=Sí, 2=No, 3=Desconocido). Complicación neurológica grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Convulsiones? (DATOS CLÍNICOS, (1.Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Dificultad para tragar (disfagia)? Odinofagia o imposibilidad de deglución. INT ternario (1=Sí, 2=No, 3=Desconocido). Síntoma funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Disfagia? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DISFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Imposibilidad de abrir boca o hablar? Mutismo, afasia o trismus. INT ternario (1=Sí, 2=No, 3=Desconocido). Síntoma funcional grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'IMPOABRIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Imposibilidad para abrir la boca / hablar? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'IMPOABRIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'IMPOABRIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Dolor de garganta (faringitis)? Odinofagia o dolor orofaríngeo. INT ternario (1=Sí, 2=No, 3=Desconocido). Síntoma clínico inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DOLORGARGANTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Dolor de garganta? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DOLORGARGANTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DOLORGARGANTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Dolor cervical o cervicalgia? Rigidez o dolor del cuello. INT ternario (1=Sí, 2=No, 3=Desconocido). Síntoma clínico inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DOLORCUELLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Dolor del cuello? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconcodido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DOLORCUELLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DOLORCUELLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 u otro sistema de clasificación. CHAR(4), referencia a diagnóstico principal de la notificación epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK a tabla HCFICHANOTIFICACION (ID). INT NOT NULL. Clave foránea, vinculación con ficha de notificación epidemiológica padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único PK de la ficha 760 (formulario clínico). INT IDENTITY(1,1), llave primaria de registro clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica de notificación epidemiológica para enfermedades con síntomas neurológicos y musculares, como tétanos y meningitis. Registra los signos y síntomas presentes en el paciente al momento de la notificación obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760';
