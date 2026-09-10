CREATE TABLE [dbo].[HCFICHA342] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NIVELEDUCA]          INT           NULL,
    [TRABURBAN]           BIT           NULL,
    [TRABRURAL]           BIT           NULL,
    [JOVENVULNE]          BIT           NULL,
    [JOVENVULURB]         BIT           NULL,
    [DISSISNERV]          BIT           NULL,
    [DISOJOS]             BIT           NULL,
    [DISOIDOS]            BIT           NULL,
    [DISDEMAS]            BIT           NULL,
    [DISVOZ]              BIT           NULL,
    [DISSISTEM]           BIT           NULL,
    [DISDIGEST]           BIT           NULL,
    [DISSISTGENI]         BIT           NULL,
    [DISMOVI]             BIT           NULL,
    [DISPIEL]             BIT           NULL,
    [DISOTRO]             BIT           NULL,
    [NODEFIN]             BIT           NULL,
    [FECHADIAG]           DATE          NULL,
    [PRUEBLAB]            VARCHAR (50)  NULL,
    [NOMBENFER]           VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA342] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA342_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA342_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA342] NOCHECK CONSTRAINT [CK_HCFICHA342_JSON];




GO
ALTER TABLE [dbo].[HCFICHA342] NOCHECK CONSTRAINT [CK_HCFICHA342_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales de la ficha en formato JSON estructurado; VARCHAR(MAX) con validación ISJSON; almacena campos enriquecidos o extensiones no normalizadas de la notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación; NULL indica primera versión; control de cambios y auditoría de modificaciones en registro de enfermedad notificable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la enfermedad diagnosticada; descripción legible del diagnóstico clínico; búsqueda por patología, dolencia, condición de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NOMBENFER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Enfermedad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NOMBENFER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NOMBENFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba confirmatoria de laboratorio (actualización: antes prueba de laboratorio); test diagnóstico, ensayo, análisis de laboratorio que valida el diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'PRUEBLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(ahora) ¿Cuál prueba confirmatoria? --- (antes) ¿Cuál prueba de laboratorio? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'PRUEBLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'PRUEBLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del diagnóstico clínico; día en que se confirma la enfermedad o condición en el paciente; referencia temporal del evento de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'FECHADIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'FECHADIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'FECHADIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = déficit definido / No (0) = sin déficit definido; identifica si discapacidad está claramente establecida en diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NODEFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nodefin:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NODEFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NODEFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = otra discapacidad no clasificada / No (0) = no aplica; captura deficiencias adicionales o atípicas no listadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disotro:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = discapacidad de piel / tegumentaria / dermatológica / No (0) = no aplica; deficiencia cutánea, dermis o integridad de piel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISPIEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dispiel:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISPIEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISPIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = discapacidad de movilidad / motricidad / movimiento / No (0) = no aplica; limitación física, motora, articular o de locomoción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISMOVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dismovi:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISMOVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISMOVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = discapacidad sistema genital / reproductivo / urogenital / No (0) = no aplica; deficiencia en función sexual o reproductiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISTGENI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dissistgeni:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISTGENI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISTGENI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = discapacidad digestiva / gastrointestinal / No (0) = no aplica; deficiencia en aparato digestivo, nutrición o asimilación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISDIGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disdigest:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISDIGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISDIGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = discapacidad sistema hematológico / inmunológico / metabólico / No (0) = no aplica; deficiencia hemato-inmuno-metabólica sistémica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISTEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dissistem:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISTEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISTEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = discapacidad de voz / fonación / comunicación oral / No (0) = no aplica; deficiencia en producción o calidad de voz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISVOZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disvoz:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISVOZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISVOZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = otra discapacidad (alias para deficiencias múltiples o adicionales) / No (0) = no aplica; captura deficiencias complementarias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISDEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disdemas:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISDEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISDEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = discapacidad auditiva / sordera / hipoacusia / audición / No (0) = no aplica; deficiencia en procesamiento de sonido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disoidos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = discapacidad visual / ceguera / baja visión / ojos / No (0) = no aplica; deficiencia en visión o percepción visual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOJOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disojos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOJOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOJOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = discapacidad sistema nervioso / neurológica / cognitiva / No (0) = no aplica; deficiencia neuronal, cerebral, medular o sensitiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISNERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dissisnerv:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISNERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISNERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = joven vulnerable zona urbana / No (0) = no aplica; identifica juventud en riesgo psicosocial o socioeconómico en ciudad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JOVENVULURB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Jovenvulurb:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JOVENVULURB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JOVENVULURB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = joven vulnerable (general) / No (0) = no aplica; población joven en situación de vulnerabilidad, riesgo o desprotección social', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JOVENVULNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Jovenvulne:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JOVENVULNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JOVENVULNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = trabaja en zona rural / actividad laboral rural / No (0) = no aplica; ocupación, labor o empleo en área campestre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'TRABRURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Trabrural:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'TRABRURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'TRABRURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: Si (1) = trabaja en zona urbana / actividad laboral urbana / No (0) = no aplica; ocupación, labor o empleo en área ciudad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'TRABURBAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Traburban:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'TRABURBAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'TRABURBAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel educativo alcanzado: 1=Preescolar, 2=Primaria, 3=Secundaria, 4=Media Académica, 5=Técnica, 6=Normalista, 7=Profesional Técnico, 8=Tecnológica, 9=Profesional, 10=Especialización, 11=Maestría, 12=Doctorado, 13=Ninguno; grado de instrucción, formación, escolaridad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NIVELEDUCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel educativo:  1=Preescolar   2=Básica Primaria   3=Básica Secundaria   4=Media Académica o Clásica   5=Media Técnica (Bachillerato Técnico)   6=Normalista   7=Técnica Profesional   8=Tecnológica   9=Profesional   10=Especialización   11=Maestría   12=Doctorado   13=Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NIVELEDUCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NIVELEDUCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CUPS, CIE-10 o nomenclatura nacional); clasificación de enfermedad, dolencia, patología o condición de salud notificable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación relacionada (FK a HCFICHANOTIFICACION.ID); referencia al evento de reporte de enfermedad de interés en salud pública', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY); clave primaria de registro de discapacidades, deficiencias y condiciones sociales en ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica 342 (evento de salud pública). Registra el diagnóstico CIE-10, nivel educativo del paciente, tipo de trabajador (urbano/rural), condición de joven vulnerable y los sistemas del cuerpo afectados por discapacidad (nervioso, ojos, oídos, voz, digestivo, genital, movilidad, piel, entre otros), junto con la fecha de diagnóstico, prueba de laboratorio y nombre de la enfermedad notificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342';
