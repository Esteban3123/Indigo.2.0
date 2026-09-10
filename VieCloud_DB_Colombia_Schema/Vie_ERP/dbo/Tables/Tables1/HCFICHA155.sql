CREATE TABLE [dbo].[HCFICHA155] (
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [TIPOCANCER]          INT           NULL,
    [FECHAPROCEDI]        DATETIME      NULL,
    [FECHARESULT1]        DATETIME      NULL,
    [RESULBIOPSIA]        INT           NULL,
    [GRADOHISPA1]         INT           NULL,
    [FECHATOMAMUES]       DATE          NULL,
    [FECHARESULT2]        DATE          NULL,
    [BIOEXOTRANSF]        BIT           NULL,
    [RESULBIOPSIAEXO]     INT           NULL,
    [GRADOHISPA2]         INT           NULL,
    [BIOPSIAENDOCER]      BIT           NULL,
    [ADENOCARCINOMA]      INT           NULL,
    [GRADOHISPA3]         INT           NULL,
    [TRATAINITUMOR]       BIT           NULL,
    [TRADIOTERAPIA]       BIT           NULL,
    [TQUIRURGICO]         BIT           NULL,
    [TQUIMIOTERAPIA]      BIT           NULL,
    [THORMONTERAPIA]      BIT           NULL,
    [TCUIDADOSPALI]       BIT           NULL,
    [TINMUNOTERAPIA]      BIT           NULL,
    [FECHAINITRATA]       DATE          NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_HCFICHA115] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA155_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA115_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA155] NOCHECK CONSTRAINT [CK_HCFICHA155_JSON];




GO
ALTER TABLE [dbo].[HCFICHA155] NOCHECK CONSTRAINT [CK_HCFICHA155_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de registro (INT IDENTITY). Clave primaria para unicidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento de datos adicionales en formato JSON validado (VARCHAR MAX, isjson()=1). Flexibilidad de columnas dinámicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación oncológica. Nulo = primera versión. Rastrea cambios de estructura (V:03 desde 2023-04-01).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 o estándar (CHAR 4). Clasificación de enfermedad oncológica diagnosticada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del tratamiento oncológico (DATE). Marca comienzo de intervención terapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHAINITRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicio tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHAINITRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHAINITRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de inmunoterapia como tipo de tratamiento (BIT: True=Sí, Null=No/Obsoleto). Deprecated desde V:03 (26/10/2023), almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TINMUNOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Tipos de tratamiento    si selecionan   Inmunoterapia= True   si no NULL      campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TINMUNOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TINMUNOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cuidados paliativos como tipo de tratamiento (BIT: True=Sí, Null=No/Obsoleto). Deprecated desde V:03 (26/10/2023), almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TCUIDADOSPALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Tipos de tratamiento    si selecionan   Cuidados paliativos= True   si no NULL      campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TCUIDADOSPALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TCUIDADOSPALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de hormonoterapia como tipo de tratamiento (BIT: True=Sí, Null=No/Obsoleto). Deprecated desde V:03 (26/10/2023), almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'THORMONTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Tipos de tratamiento    si selecionan   Hormonterapia= True   si no NULL      campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'THORMONTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'THORMONTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de quimioterapia sistémica como tipo de tratamiento (BIT: True=Sí, Null=No/Obsoleto). Deprecated desde V:03 (26/10/2023), almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TQUIMIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Tipos de tratamiento    si selecionan   Quimioterapia= True   si no NULL      campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TQUIMIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TQUIMIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tratamiento quirúrgico/resección tumoral (BIT: True=Sí, Null=No/Obsoleto). Deprecated desde V:03 (26/10/2023), almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TQUIRURGICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Tipos de tratamiento    si selecionan   Quirúrgico= True   si no NULL    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TQUIRURGICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TQUIRURGICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de radioterapia como tipo de tratamiento (BIT: True=Sí, Null=No/Obsoleto). Deprecated desde V:03 (26/10/2023), almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TRADIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de tratamiento  si selecionan   Radioterapia = True   si no NULL    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TRADIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TRADIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de tratamiento inicial del tumor (BIT: True=Sí iniciado, False/Null=No). Indica inicio de terapia oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TRATAINITUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tratamiento inicial del tumor  True = SiFalse = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TRATAINITUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TRATAINITUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado histopatológico del tejido tumoral (INT: 1=In-situ, 2=Infiltrante, 3=No indicado). Deprecated desde V:03, almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grado histopatológico  1=In-situ  2=Infiltrante  3=No indicado    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023   cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado biopsia endocervical para adenocarcinoma (INT: 1=Positivo, 2=Negativo). Deprecated desde V:03, almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'ADENOCARCINOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado biopsia endocervix:  Adenocarcinoma:  1= Positivo  2=Negativo  campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023   cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'ADENOCARCINOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'ADENOCARCINOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Realización de biopsia endocervical/canal cervical (BIT: True=Sí realizada, False/Null=No). Deprecated desde V:03, almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'BIOPSIAENDOCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Biopsia endocervix  True = Si  False = NO  campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023   cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'BIOPSIAENDOCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'BIOPSIAENDOCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado histopatológico avanzado (INT: 1=In-situ, 2=Infiltrante, 3=No indicado, 4=Invasor Figo IA-IB2, 5=Invasor Figo ≥IB3). Clasificación FIGO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grado histopatológico  1=In-situ  2=Infiltrante  3=No indicado  4=Invasor / Infiltrante (Figo IA o IB2)  5=Invasor / Infiltrante (Figo >= IB3) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado biopsia exocervical y zona de transformación (INT: 1=LEI AG NCIII/In situ, 2=Carcinoma escamocelular, 3=Adenocarcinoma/mixtos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'RESULBIOPSIAEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado biopsia exocervix  1= LEI AG NCIII / In situ  2= Carcinoma escamocelular  3=Adenocarcinoma o mixtos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'RESULBIOPSIAEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'RESULBIOPSIAEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Realización de biopsia exocervical y zona de transformación (BIT: True=Sí realizada, False/Null=No). Deprecated desde V:03, almacena nulos por PBI 12155.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'BIOEXOTRANSF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Biopsia exocervix y zona de transformación  True = Si   Fasle = No     campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'BIOEXOTRANSF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'BIOEXOTRANSF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado/reporte de segunda biopsia o muestra (DATE). Seguimiento de proceso diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHARESULT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHARESULT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHARESULT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma/recolección de muestra tisular (DATE). Inicio del proceso de análisis patológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha toma de muestra ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado histopatológico inicial (INT: 1=In-situ, 2=Infiltrante, 3=No indicado). Clasificación del tumor primario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grado histopatológico   1=In-situ  2=Infiltrante  3=No indicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de biopsia inicial (INT: 1=Carcinoma ductal, 2=Carcinoma lobulillar). Tipo histológico de CA mama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'RESULBIOPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado biopsia  1=Carcioma ductal    2=Carcioma lobulillar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'RESULBIOPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'RESULBIOPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado/reporte de primera biopsia o examen (DATE). Confirmación diagnóstica inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHARESULT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHARESULT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHARESULT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del procedimiento de biopsia, punción o intervención diagnóstica (DATETIME). Evento clínico oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHAPROCEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHAPROCEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHAPROCEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cáncer notificado (INT: 1=CA Mama, 2=CA Cuello uterino, 3=Ambos). Clasificación por sitio anatomopatológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cancer  1: CA Mama  2: CA Cuello uterino  3: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a HCFICHANOTIFICACION.ID (INT NOT NULL). Referencia de ficha madre de notificación oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación de cáncer (ficha 155 del SIVIGILA), donde se registran los datos clínicos del paciente con diagnóstico de cáncer: tipo de cáncer, resultados de biopsias, grado histopatológico, fechas de procedimientos y tratamientos recibidos (radioterapia, quimioterapia, cirugía, hormonoterapia, inmunoterapia, cuidados paliativos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155';
