CREATE TABLE [dbo].[HCFICHA430] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [RECITRAT]            BIT           NULL,
    [TRATLOCAL]           BIT           NULL,
    [PESOACT]             VARCHAR (50)  NULL,
    [MEDIFORM]            INT           NULL,
    [OTROCUAL]            VARCHAR (50)  NULL,
    [NUMCAPS]             VARCHAR (50)  NULL,
    [DIASTRAT]            VARCHAR (50)  NULL,
    [TOTACAPS]            VARCHAR (50)  NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHARECE]           DATE          NULL,
    [MUESTRA]             INT           NULL,
    [PRUEBA]              INT           NULL,
    [AGENTE]              INT           NULL,
    [RESULTADO]           INT           NULL,
    [FECHARESUL]          DATE          NULL,
    [VALOR]               VARCHAR (50)  NULL,
    [MUCOSA]              INT           NULL,
    [RINORREA]            BIT           NULL,
    [EPISTAXIS]           BIT           NULL,
    [OBSTRNASAL]          BIT           NULL,
    [DISFONIA]            BIT           NULL,
    [DISFAGIA]            BIT           NULL,
    [HIPEREMIA]           BIT           NULL,
    [ULCERACION]          BIT           NULL,
    [PERFORACION]         BIT           NULL,
    [DESTRUCCION]         BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA430] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA430_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA430_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA430] NOCHECK CONSTRAINT [CK_HCFICHA430_JSON];




GO
ALTER TABLE [dbo].[HCFICHA430] NOCHECK CONSTRAINT [CK_HCFICHA430_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento de nuevas columnas y datos adicionales en formato JSON validado (ISJSON); extensión flexible para evolución de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación (VARCHAR 20); valor nulo indica primera versión; rastreo de cambios y auditoría desde V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de signo/síntoma: destrucción de tejido; 1=Sí, 0=No; utilizado en notificación de lesiones cutáneas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DESTRUCCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada  DESTRUCCION  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DESTRUCCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DESTRUCCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de signo/síntoma: perforación de mucosa; 1=Sí, 0=No; válido para notificación epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'PERFORACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada  PERFORACION  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'PERFORACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'PERFORACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de signo/síntoma: úlcera, ulceración de mucosa; 1=Sí, 0=No; marca lesión ulcerativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'ULCERACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada  ULCERACION  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'ULCERACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'ULCERACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de signo/síntoma: hiperemia (enrojecimiento); 1=Sí, 0=No; evaluación de inflamación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'HIPEREMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada  HIPEREMIA  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'HIPEREMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'HIPEREMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de signo/síntoma: disfagia (dificultad para tragar); 1=Sí, 0=No; síntoma de afección orofaríngea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada  DISFAGIA  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DISFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de signo/síntoma: disfonía (voz ronca/alterada); 1=Sí, 0=No; síntoma de afección laríngea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DISFONIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada DISFONIA  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DISFONIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DISFONIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de signo/síntoma: obstrucción nasal; 1=Sí, 0=No; síntoma de afección vías aéreas superiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'OBSTRNASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada OBSTRNASAL  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'OBSTRNASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'OBSTRNASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de signo/síntoma: epistaxis (sangrado nasal); 1=Sí, 0=No; hemorragia de mucosa nasal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'EPISTAXIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada EPISTAXIS  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'EPISTAXIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'EPISTAXIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de signo/síntoma: rinorrea (flujo nasal); 1=Sí, 0=No; secreción de cavidad nasal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'RINORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada RINORREA  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'RINORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'RINORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica de mucosa afectada (INT): 1=Nasal, 2=Cavidad Oral, 3=Labios, 4=Faringe, 5=Laringe, 6=Párpados, 7=Genitales; desde V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'MUCOSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mucosa Afectada :    1 = Nasal    2 = Cavidad Oral    3 = Labios    4 = Faringe    5 = Laringe    6 = Párpados    7 = Genitales ---> item nuevo desde version ''''V01_2020-03-06'''' ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'MUCOSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'MUCOSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico del resultado de laboratorio (VARCHAR 50); dato cuantitativo de examen (títulos, concentraciones, recuentos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el valor (Datos laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de reporte de resultado de laboratorio (DATE); fecha en que se comunica el resultado al notificador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del resultado de laboratorio (INT): 1=Positivo, 2=Negativo, 3=Compatible, 4=No Compatible; validación diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el resultado 1 = Positivo  2 = Negativo  3=Compatible    4 = No Compatible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del agente etiológico identificado (INT): 1=Leishmania; referencia para notificación de enfermedades infecciosas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el agente  1= Leishmania', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de prueba/técnica realizada (INT): 1=Estudio Directo, 2=Título IFI, 3=Aspirado Bazo, 4=Aspirado Médula, 5=Prueba Montenegro; método diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Prueba  1 = Estudio Directo   2 = Titulo IFI  3= Aspirado Bazo   4= Aspirado Médula   5= Prueba Montenegro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de muestra biológica (INT): 1=Sangre total, 2=Tejido, 3=Linfa; origen del espécimen para laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Muestra  1 =sangre total   2 = Tejido  3 = Linfa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de muestra en laboratorio (DATE); trazabilidad de cadena de custodia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la  Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma/recolección de muestra (DATE); fecha de origen del espécimen biológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la  Fecha toma de examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de cápsulas o ampollas dispensadas (VARCHAR 50); cantidad total de medicamento en presentación farmacéutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'TOTACAPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Total de cápsulas o ampollas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'TOTACAPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'TOTACAPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días de duración del tratamiento (VARCHAR 50); período completo de medicación prescrita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DIASTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Días de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DIASTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'DIASTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cápsulas o volumen diario a aplicar (VARCHAR 50); posología: dosis diaria del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'NUMCAPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el  Número de capsulas o volumen diario a aplicar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'NUMCAPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'NUMCAPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otra opción seleccionada (VARCHAR 50); especificación textual cuando medicamento/tratamiento es ''''Otro''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'OTROCUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Otro ¿Cúal?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'OTROCUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'OTROCUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento formulado actualmente (INT): 1=N-Metil Glucamina (Glucantime), 2=Estibogluconato Sodio, 3=Isotianato Pentamidina, 4=Anfotericina B, 5=Otro, 6=Miltefosina, 7=Pentamidina, 8=Sin tratamiento; desde V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'MEDIFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento Formulado Actualmente :    1 = N- Metil Glucamina (Glucantime)   2 = Estibogluconato de Sodio   3 = Isotianato de Pentamidina   4 = Anfotericina B   5 = Otro   6 = Miltefosina   7 = Pentamidina   8 = Sin tratamiento ---> item nuevo desde version ''''V01_2020-03-06'''' ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'MEDIFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'MEDIFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso actual del paciente en kilogramos (VARCHAR 50); dato antropométrico para ajuste de dosis farmacológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Peso actual del paciente (Kgs)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'PESOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tratamiento local tópico (BIT): 1=Crioterapia, 2=Termoterapia; terapia complementaria de lesiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'TRATLOCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Tratamiento local   1 = Crioterapia     2 = Termoterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'TRATLOCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'TRATLOCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de tratamiento anterior previo: 1=Sí recibió, 0=No recibió; historial terapéutico del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'RECITRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda ¿Recibió tratamiento anterior?    1 = si    0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'RECITRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'RECITRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CHAR 4); clasificación nosológica de la enfermedad notificada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la ficha de notificación epidemiológica relacionada (FK); vinculación a cabecera HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (INT IDENTITY); clave primaria de la tabla HCFICHA430', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica 430 de notificación epidemiológica para leishmaniasis u otras enfermedades con compromiso cutáneo-mucoso. Registra el diagnóstico, esquema de tratamiento, resultados de laboratorio (toma de muestra, pruebas diagnósticas, agente etiológico) y manifestaciones clínicas en mucosas (rinorrea, epistaxis, obstrucción nasal, disfonía, úlceras, perforaciones, destrucción de tejido).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA430';
