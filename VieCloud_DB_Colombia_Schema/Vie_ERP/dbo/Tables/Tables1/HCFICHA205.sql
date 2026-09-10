CREATE TABLE [dbo].[HCFICHA205] (
    [ID]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION]  INT           NOT NULL,
    [SEMANASEMBARAZO]      INT           NULL,
    [CODDIAGNO]            CHAR (4)      NULL,
    [CLASIFCASO]           BIT           NULL,
    [REACTIVACION]         BIT           NULL,
    [FIEBRE]               BIT           NULL,
    [DISNEA]               BIT           NULL,
    [EDEMAFACIAL]          BIT           NULL,
    [EDEMAMIEMINF]         BIT           NULL,
    [DERRAPERICAR]         BIT           NULL,
    [HEPATOESPLENOMEGALIA] BIT           NULL,
    [ADENOPATIAS]          BIT           NULL,
    [ROMANA]               BIT           NULL,
    [CHAGOMA]              BIT           NULL,
    [FALLACARDIA]          BIT           NULL,
    [DISFAGIA]             BIT           NULL,
    [DOLORTORAX]           BIT           NULL,
    [BRADICARDIA]          BIT           NULL,
    [ARRITMIACARDIA]       BIT           NULL,
    [MICROMETODO]          INT           NULL,
    [GOTAGRUESA]           INT           NULL,
    [MICROHEMATO]          INT           NULL,
    [STROUT]               INT           NULL,
    [ELISA]                INT           NULL,
    [ELISACLIA]            INT           NULL,
    [IFI]                  INT           NULL,
    [INMUNOBIOT]           INT           NULL,
    [VIATRANSMI]           INT           NULL,
    [VERSION]              VARCHAR (20)  NULL,
    [JSON]                 VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA205] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA205_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA205_HCFICHANOTIFICACION1] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA205] NOCHECK CONSTRAINT [CK_HCFICHA205_JSON];




GO
ALTER TABLE [dbo].[HCFICHA205] NOCHECK CONSTRAINT [CK_HCFICHA205_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento JSON con columnas adicionales de la ficha; VARCHAR(MAX) con validación ISJSON para almacenar datos estructurados complementarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación; NULL indica primera versión, incrementa en reactivaciones o correcciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de transmisión de enfermedad (INT): 1=Vectorial, 2=Transfusional, 3=Congénita, 4=Oral, 5=Trasplante, 6=Accidente laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'VIATRANSMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posible vía de transmisión  1=Vectorial  2=Transfuncional  3=Congénita  4=Vía oral  5=Transplante  6=Accidente de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'VIATRANSMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'VIATRANSMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado prueba Inmunobiot T.cruzi (INT): 1=Positivo, 2=Negativo, 3=No realizado; serotipificación complementaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'INMUNOBIOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmunobiot  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'INMUNOBIOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'INMUNOBIOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado IFI IgG T.cruzi (INT): 1=Positivo, 2=Negativo, 3=No realizado; inmunofluorescencia indirecta para confirmación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'IFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IFI IgG T.cruzi  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'IFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'IFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado ELISA/CLIA con antígenos recombinantes o péptidos sintéticos (INT): 1=Positivo, 2=Negativo, 3=No realizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ELISACLIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ELISA/CLIA Ag recombinantes/péptidos sintéticos  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ELISACLIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ELISACLIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado ELISA antígenos totales convencionales (INT): 1=Positivo, 2=Negativo, 3=No realizado; prueba serológica inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ELISA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ELISA Ag totales (convencional)  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ELISA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ELISA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado Strout o concentración hemática (INT): 1=Positivo, 2=Negativo, 3=No realizado; detección parasitemia aguda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'STROUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Strout  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'STROUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'STROUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado Microhematocrito/examen de sangre (INT): 1=Positivo, 2=Negativo, 3=No realizado; método concentración para fase aguda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'MICROHEMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microhematocrito / examen  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'MICROHEMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'MICROHEMATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado Gota gruesa o frotis de sangre (INT): 1=Positivo, 2=Negativo, 3=No realizado; visualización directa parásitos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'GOTAGRUESA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gota gruesa / frotis de sangre  1 = Positivo2= Negativo  3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'GOTAGRUESA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'GOTAGRUESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado Micrométodo de concentración (INT): 1=Positivo, 2=Negativo, 3=No realizado; técnica microscopía luz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'MICROMETODO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Micrométodo  1=Positive  2=Negativo  3=No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'MICROMETODO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'MICROMETODO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Arritmia cardiaca detectada (BIT); manifestación clínica en paciente crónico con cardiopatía chagásica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ARRITMIACARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   ARRITMIACARDIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ARRITMIACARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ARRITMIACARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bradicardia o frecuencia cardíaca baja (BIT); manifestación clínica en paciente crónico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   BRADICARDIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dolor torácico o precordial (BIT); manifestación clínica en paciente crónico; síntoma cardiopulmonar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DOLORTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   DISFAGIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DOLORTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DOLORTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dificultad para deglutir (BIT); manifestación clínica en paciente crónico; afecta esófago/tracto digestivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   DISFAGIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DISFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Insuficiencia o falla cardíaca (BIT); manifestación clínica en paciente crónico; compromiso miocárdico severo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'FALLACARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   FALLACARDIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'FALLACARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'FALLACARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chagoma o lesión de entrada (BIT); manifestación clínica aguda en lugar inoculación; inflamación nodular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CHAGOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     CHAGOMA= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CHAGOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CHAGOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signo de Romana u oftalmitis (BIT); manifestación clínica aguda; edema periocular específico fase inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ROMANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     ROMANA= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ROMANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ROMANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adenopatías o inflamación de ganglios (BIT); manifestación clínica aguda; linfadenopatía generalizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ADENOPATIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     ADENOPATIAS= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ADENOPATIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ADENOPATIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hepatoesplenomegalia u órganos aumentados (BIT); manifestación clínica aguda; hepatomegalia+esplenomegalia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'HEPATOESPLENOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     HEPATOESPLENOMEGALIA= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'HEPATOESPLENOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'HEPATOESPLENOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Derrame pericárdico o acumulación fluido (BIT); manifestación clínica aguda; complicación cardíaca severa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DERRAPERICAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     DERRAPERICAR= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DERRAPERICAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DERRAPERICAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edema en miembros inferiores (BIT); manifestación clínica aguda; inflamación extremidades inferiores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'EDEMAMIEMINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     EDEMAMIEMINF= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'EDEMAMIEMINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'EDEMAMIEMINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edema facial o hinchazón cara (BIT); manifestación clínica aguda; tumefacción rostro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'EDEMAFACIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     EDEMAFACIAL= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'EDEMAFACIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'EDEMAFACIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disnea o dificultad respiratoria (BIT); manifestación clínica aguda; compromiso pulmonar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DISNEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     DISNEA= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DISNEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DISNEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fiebre o temperatura elevada (BIT); manifestación clínica aguda; síntoma inicial enfermedad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona   Fiebre = True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reactivación de infección crónica latente (BIT): True=Sí/reactivación, False o NULL=No; especialmente en inmunodeprimidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'REACTIVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reactivación  True = Si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'REACTIVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'REACTIVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación epidemiológica del caso (BIT): True=Agudo, False=Crónico; determina perfil clínico y laboratorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación del caso  True = Agudo   False = Crónico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (CHAR 4); diagnóstico principal de la notificación; ejemplo B57.0-B57.9.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semana de gestación del paciente (INT); información obstétrica relevante para riesgo transmisión congénita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'SEMANASEMBARAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semana de embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'SEMANASEMBARAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'SEMANASEMBARAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación (INT); llave foránea a tabla HCFICHANOTIFICACION; vincula datos epidemiológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ficha clínica 205 (INT IDENTITY); clave primaria de registro; generado automáticamente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica de notificación de Enfermedad de Chagas (evento 205). Registra los síntomas clínicos, resultados de pruebas diagnósticas de laboratorio, vía de transmisión y clasificación del caso para pacientes notificados con sospecha o confirmación de Chagas, incluyendo datos de embarazo cuando aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205';
