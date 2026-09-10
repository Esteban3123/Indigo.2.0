CREATE TABLE [dbo].[HCFICHA450] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [TIPOINGRESO]         INT           NULL,
    [CLASIFCASO]          INT           NULL,
    [NUMLESIONES]         VARCHAR (50)  NULL,
    [BACILOS]             BIT           NULL,
    [RESULTADO]           VARCHAR (50)  NULL,
    [BIOPSIA]             BIT           NULL,
    [RESULHISPA]          INT           NULL,
    [MAXIGRADO]           INT           NULL,
    [PRESEREACCION]       INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA450] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA450_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA450_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA450] NOCHECK CONSTRAINT [CK_HCFICHA450_JSON];




GO
ALTER TABLE [dbo].[HCFICHA450] NOCHECK CONSTRAINT [CK_HCFICHA450_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena datos adicionales y dinámicos de la ficha en formato JSON validado; permite extensión de columnas sin alterar estructura (VARCHAR MAX, isJson constraint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación de lepra; NULL indica primera versión; controla auditoría y cambios evolutivos del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de reacción adversa leprótica: 1=Reacción tipo uno (leprorreaacción I), 2=Reacción tipo dos (eritema nodoso leproso/ENL), 3=Sin reacción; Bit/INT para evaluación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'PRESEREACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Presenta reacción?   1 = tipo uno    2 =  tipo dos  3= ninguno ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'PRESEREACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'PRESEREACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Máximo grado de discapacidad evaluada en paciente con lepra: 1=Grado cero (sin discapacidad), 2=Grado uno (discapacidad sensorial/motora leve), 3=Grado tres (discapacidad severa); escala OMS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'MAXIGRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Máximo grado de discapacidad evaluado  1 = grado cero   2 = grado uno   3 =  grado 3  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'MAXIGRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'MAXIGRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de histopatología/biopsia en diagnóstico de lepra: 1=Indeterminada, 2=Tuberculoide, 3=Dimorfa/Borderline, 4=Lepromatosa, 5=Neural, 6=Otro diagnóstico; clasificación OMS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'RESULHISPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Resultado de la histopatología  1 =  Indeterminada    2 = Tuberculoide    3 =Dimorfa (Borderline)     4 =Lepromatosa    5 =Neural     6 =  Otro Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'RESULHISPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'RESULHISPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano de realización de biopsia (BIT); 1=Sí se realizó biopsia, 0=No realizada; permite correlacionar con resultado histopatológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'BIOPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Biopsia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'BIOPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'BIOPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen complementario o prueba diagnóstica realizada (VARCHAR 50); puede referir baciloscopia, biopsia o cultivo según contexto clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el  Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Baciloscopia o índice bacilar: 1=Positivo (presencia de bacilos Mycobacterium leprae), 0=Negativo (ausencia de bacilos); BIT booleano para clasificación paucibacilar/multibacilar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'BACILOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Baciloscopia   1 = si   0=  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'BACILOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'BACILOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de lesiones de piel identificadas en examen clínico inicial; VARCHAR 50 permite rango o descripción; criterio diagnóstico OMS para lepra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'NUMLESIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Número de lesiones identificadas, examen clínico inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'NUMLESIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'NUMLESIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación clínica operativa del caso de lepra: 1=Paucibacilar (lesiones ≤5, baciloscopia negativa), 2=Multibacilar (lesiones >5 o baciloscopia positiva); determina esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Clasificación clínica del caso    1 = Paucibacilar    2 = Multibacilar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso/casuística del paciente con lepra: 1=Caso nuevo (nunca tratado), 2=Recidiva (reaparición tras cura), 3=Retratamiento por pérdida al seguimiento; INT para control epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'TIPOINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarada el tipo de ingreso 1 = Nuevo      2 = Recidiva    3 =  Retratamiento despues de la pérdida al seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'TIPOINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'TIPOINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico principal (CHAR 4); referencia a código CIE-10 para lepra (A90-A92) u otro diagnóstico relacionado; clave de clasificación nosológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de ficha de notificación obligatoria de lepra (INT, FK a HCFICHANOTIFICACION); vincula con evento de salud pública reportado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK IDENTITY INT); consecutivo auto-incrementado que identifica registro de forma inequívoca en tabla de seguimiento clínico de lepra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica 450 del sistema de vigilancia epidemiológica para notificación de lepra (enfermedad de Hansen). Registra los datos del caso: diagnóstico, tipo de ingreso, clasificación, resultados de baciloscopía, biopsia e histopatología, grado de afectación y reacciones, integrando también la información en formato JSON para trazabilidad del formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450';
