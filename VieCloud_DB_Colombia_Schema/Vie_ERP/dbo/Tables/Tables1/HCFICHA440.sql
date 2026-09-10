CREATE TABLE [dbo].[HCFICHA440] (
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
    [DIAGCONF]            INT           NULL,
    [FIEBRE]              BIT           NULL,
    [HEPATO]              BIT           NULL,
    [ESPLENO]             BIT           NULL,
    [ANEMIA]              BIT           NULL,
    [LEUCOCI]             BIT           NULL,
    [PLAQUET]             BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA440] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA440_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA440_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA440] NOCHECK CONSTRAINT [CK_HCFICHA440_JSON];




GO
ALTER TABLE [dbo].[HCFICHA440] NOCHECK CONSTRAINT [CK_HCFICHA440_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columnas adicionales en formato JSON (VARCHAR MAX), validadas con constraint ISJSON para almacenamiento flexible de datos estructurados de notificación epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación epidemiológica; NULL = primera versión; permite rastrear cambios y actualizaciones del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de signo/síntoma: plaquetopenia (bajo recuento de plaquetas); BIT (true/1=sí, false/0=no); componente de presentación clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PLAQUET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada PLAQUET   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PLAQUET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PLAQUET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de signo/síntoma: leucocitosis o leucopenia (alteración de glóbulos blancos); BIT (true/1=sí, false/0=no); hallazgo hematológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'LEUCOCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada  LEUCOCI   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'LEUCOCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'LEUCOCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de signo/síntoma: anemia (reducción de hemoglobina); BIT (true/1=sí, false/0=no); manifestación clínica común en leishmaniasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ANEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada ANEMIA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ANEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de signo/síntoma: esplenomegalia (bazo aumentado); BIT (true/1=sí, false/0=no); hallazgo físico relevante en diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ESPLENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada  ESPLENO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ESPLENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ESPLENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de signo/síntoma: hepatomegalia (hígado aumentado); BIT (true/1=sí, false/0=no); manifestación sistémica de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'HEPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada HEPATO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'HEPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'HEPATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de signo/síntoma: fiebre (elevación de temperatura); BIT (true/1=sí, false/0=no); síntoma cardinal en enfermedad infecciosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el tipo de signos y sintomas  es seleccionada FIEBRE    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Diagnóstico VIH confirmado? INT: 1=sí, 2=no, 3=desconocido; determina estado de comorbilidad en paciente notificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'DIAGCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ¿Tiene diagnóstico VIH confirmado?   1 = si     2 = no    3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'DIAGCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'DIAGCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico o alfanumérico del resultado de laboratorio (VARCHAR 50); ej: hematocrito %, hemoglobina g/dL, conteo de plaquetas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el valor (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado del examen de laboratorio (DATE); marca momento de obtención del diagnóstico confirmatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen: INT (1=positivo, 2=negativo, 3=compatible, 4=no compatible); clasifica estado diagnóstico del agente infeccioso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el resultado 1 = Positivo  2 = Negativo  3=Compatible    4 = No Compatible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente etiológico identificado; INT (1=Leishmania); especifica patógeno causal en ficha de notificación epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el agente  1= Leishmania', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba diagnóstica: INT (1=hematocrito, 2=hemoglobina, 3=plaquetas, 4=estudio directo, 5=título IFI, 6=aspirado bazo, 7=aspirado médula, 8=pruebas Montenegro, 9=albúmina); define método de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Guarda la Prueba  1 = Hematocrito   2 = Hemoglobina  3=Plaquetas   4= Estudio Directo  5= Titulo IFI  6 = Aspirado Bazo 7 = Aspirado Médula  8= Pruebas Montenegro  9 =  Albumina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de muestra biológica: INT (1=sangre total, 2=tejido, 3=linfa); origen del especimen para análisis diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Muestra  1 =sangre total   2 = Tejido  3 = Linfa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de la muestra en laboratorio (DATE); registra momento de ingreso del especimen para procesamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma o recolección del examen (DATE); establece temporalidad de obtención de muestra clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la  Fecha toma de examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de cápsulas o ampollas dispensadas (VARCHAR 50); cantidad agregada de unidades farmacéuticas entregadas en tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'TOTACAPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Total de cápsulas o ampollas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'TOTACAPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'TOTACAPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de tratamiento administrado (VARCHAR 50); duración total de la terapia farmacológica prescrita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'DIASTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Días de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'DIASTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'DIASTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cápsulas/ampollas o volumen diario a aplicar (VARCHAR 50); dosis diaria o cantidad por administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'NUMCAPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el  Número de capsulas o volumen diario a aplicar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'NUMCAPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'NUMCAPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de opción ''''otro'''' (VARCHAR 50); especificación libre de medicamento, muestra u otro parámetro no listado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'OTROCUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Otro ¿Cúal?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'OTROCUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'OTROCUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento formulado actualmente: INT (1=N-Metil Glucamina Glucantime, 2=Estibogluconato Sodio, 3=Isotianato Pentamidina, 4=Anfotericina B, 5=Otro, 6=Miltefosina, 7=Pentamidina, 8=Sin tratamiento); registra fármaco activo en esquema terapéutico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'MEDIFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento Formulado Actualmente :    1 = N- Metil Glucamina (Glucantime)   2 = Estibogluconato de Sodio   3 = Isotianato de Pentamidina   4 = Anfotericina B   5 = Otro   6 = Miltefosina   7 = Pentamidina   8 = Sin tratamiento ---> item nuevo desde version ''''V01_2020-03-06'''' ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'MEDIFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'MEDIFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso actual del paciente en kilogramos (VARCHAR 50); medida antropométrica para cálculo de dosis medicamentosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Peso actual del paciente (Kgs)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PESOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tratamiento local aplicado: BIT/INT (1=crioterapia, 2=termoterapia); indica intervención tópica complementaria en lesiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'TRATLOCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Tratamiento local   1 = Crioterapia     2 = Termoterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'TRATLOCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'TRATLOCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Recibió tratamiento anterior? BIT (1=sí, 0=no); identifica pacientes con antecedente de exposición farmacológica previa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'RECITRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda ¿Recibió tratamiento anterior?    1 = si    0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'RECITRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'RECITRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CHAR 4); referencia a clasificación de diagnóstico (ej: CIE-10) para la enfermedad notificada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación epidemiológica (FK a HCFICHANOTIFICACION.ID); enlace con registro principal de notificación de vigilancia en salud pública', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de la tabla HCFICHA440 (INT IDENTITY); clave primaria para registro individual de datos clínico-epidemiológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del formulario de notificación de malaria (ficha 440 del SIVIGILA), que almacena datos clínicos, de laboratorio y tratamiento para cada caso notificado: diagnóstico, medicamentos, dosis, muestras, pruebas diagnósticas, resultados parasitológicos y signos de alarma (fiebre, hepatomegalia, esplenomegalia, anemia, leucocitos, plaquetas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440';
