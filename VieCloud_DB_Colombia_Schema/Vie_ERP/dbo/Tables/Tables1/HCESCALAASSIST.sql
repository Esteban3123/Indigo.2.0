CREATE TABLE [dbo].[HCESCALAASSIST] (
    [ID]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCESCALAID]     INT           NOT NULL,
    [CODIGO]         INT           NOT NULL,
    [RESULTADO]      INT           NOT NULL,
    [INTERPRETACION] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_HCESCALAASSIST] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALAASSIST_HCESCALAS] FOREIGN KEY ([HCESCALAID]) REFERENCES [dbo].[HCESCALAS] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo (VARCHAR 500) que interpreta y clasifica el puntaje obtenido en la sustancia; proporciona diagnóstico clínico o categorización del riesgo (ej: bajo riesgo, consumo moderado, dependencia).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'INTERPRETACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de acuerdo al puntaje de la sustancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'INTERPRETACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'INTERPRETACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje numérico (INT) asignado para cada sustancia según escala; refleja nivel o intensidad de consumo/riesgo evaluado en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje para cada sustancia. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico de la sustancia evaluada: 001=Tabaco, 002=Alcohol, 003=Cannabis, 004=Cocaína, 005=Anfetaminas, 006=Inhalantes, 007=Sedantes, 008=Alucinógenos, 009=Opiáceos, 010=Otras drogas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'001->Tabaco, 002->Alcohol, 003->Cannabis, 004->Cocaina, 005->Anfetaminas, 006->Inhalantes, 007->Sedantes, 008->Alucinogenos, 009->Opiaceos, 010->Otras Drogas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que relaciona con tabla HCESCALAS; identifica la escala de evaluación de consumo de sustancias psicoactivas a la que pertenece este registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'HCESCALAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera llamada HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'HCESCALAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'HCESCALAID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, consecutivo y clave primaria (INT IDENTITY) de cada registro de sustancia en la escala de asistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de las respuestas o ítems aplicados en escalas de valoración clínica tipo ASSIST (prueba de detección de consumo de alcohol, tabaco y sustancias). Cada fila representa un ítem respondido dentro de una evaluación, con su puntaje y la interpretación del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST';
