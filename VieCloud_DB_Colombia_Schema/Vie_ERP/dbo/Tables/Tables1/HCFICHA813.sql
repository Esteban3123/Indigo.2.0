CREATE TABLE [dbo].[HCFICHA813] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CONDICION]           BIT           NULL,
    [TIPOTUBER]           BIT           NULL,
    [LOCALTUBER]          INT           NULL,
    [SEGUANTECE]          BIT           NULL,
    [PREVTRAT]            INT           NULL,
    [PACTRASAL]           BIT           NULL,
    [OCUPACION]           VARCHAR (50)  NULL,
    [PACCUENDIA]          BIT           NULL,
    [PESOACT]             VARCHAR (50)  NULL,
    [TALLAACT]            VARCHAR (50)  NULL,
    [IMC]                 VARCHAR (50)  NULL,
    [BACILOS]             BIT           NULL,
    [RESULBACI]           BIT           NULL,
    [CULTIVO]             BIT           NULL,
    [RESUCULT]            INT           NULL,
    [PRUEMOL]             BIT           NULL,
    [RESULPRUEMOL]        BIT           NULL,
    [NOMESPIDENT]         INT           NULL,
    [HISPATO]             BIT           NULL,
    [RESUHISPAT]          BIT           NULL,
    [RESPRUSENS]          BIT           NULL,
    [CUADCLI]             BIT           NULL,
    [NEXOEPI]             BIT           NULL,
    [RADIOL]              BIT           NULL,
    [ADA]                 BIT           NULL,
    [TUBERCULINA]         BIT           NULL,
    [COOMOR]              INT           NULL,
    [MONORES]             BIT           NULL,
    [MDR]                 BIT           NULL,
    [POLIRES]             BIT           NULL,
    [XDR]                 BIT           NULL,
    [RESRIFAMP]           BIT           NULL,
    [RESPREXDR]           BIT           NULL,
    [ESTREP1]             INT           NULL,
    [ESTREP2]             INT           NULL,
    [ISONIA1]             INT           NULL,
    [ISONIA2]             INT           NULL,
    [ISONIA3]             INT           NULL,
    [ETAMBU1]             INT           NULL,
    [ETAMBU2]             INT           NULL,
    [PIRAZI1]             INT           NULL,
    [PIRAZI2]             INT           NULL,
    [RIFAMP1]             INT           NULL,
    [RIFAMP2]             INT           NULL,
    [QUINDO1]             INT           NULL,
    [QUINDO2]             INT           NULL,
    [INYECT1]             INT           NULL,
    [INYECT2]             INT           NULL,
    [FECHACONFIR]         DATE          NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA813] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA813_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA813_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA813] NOCHECK CONSTRAINT [CK_HCFICHA813_JSON];




GO
ALTER TABLE [dbo].[HCFICHA813] NOCHECK CONSTRAINT [CK_HCFICHA813_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento JSON de campos nuevos por versionamiento; versiones antiguas nulo; desde V01_2020-03-06 incluye ENPROCESOCLASIFICA (booleano: En Proceso de Clasificación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-03-06 ) =    ENPROCESOCLASIFICA: booleano (En Proceso de Clasificación)   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación TB; nulo = primera versión; formatos: V01_2020-03-06, V03_2023-04-01, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación del caso de tuberculosis (diagnóstico confirmado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de confirmación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'XDR/pre-XDR - Resistencia a inyectables (amikacina, capreomicina, kanamicina): 1=Sensible, 2=Resistente; obsoleto desde V03_2023-04-01 (almacena nulos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'INYECT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Resistencia a pre XDR - I: Inyectables  almacena  1=Sensible  2=Resistente    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'INYECT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'INYECT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'XDR - Resistencia a inyectables: 1=Sensible; obsoleto desde V03_2023-04-01 (almacena nulos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'INYECT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> XDR - I: Inyectables  almacena  1=Sensible    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'INYECT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'INYECT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'pre-XDR - Resistencia a fluoroquinolonas (quinolonas): 1=Sensible, 2=Resistente; obsoleto desde V03_2023-04-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'QUINDO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Resistencia a pre XDR - Q: Quindonas  almacena  1=Sensible  2=Resistente    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'QUINDO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'QUINDO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'XDR - Resistencia a fluoroquinolonas: 1=Sensible; obsoleto desde V03_2023-04-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'QUINDO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> XDR - Q: Quindonas  almacena  1=Sensible    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'QUINDO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'QUINDO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'R - Rifampicina (resultado 2): 2=Resistente; prueba de sensibilidad a fármacos (PSF)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RIFAMP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> R: Rifampicina2 :      2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RIFAMP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RIFAMP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'R - Rifampicina (resultado 1): 2=Resistente; prueba de sensibilidad a fármacos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RIFAMP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> R: Rifampicina     2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RIFAMP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RIFAMP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Z - Pirazinamida (resultado 2): 2=Resistente; fármaco de primera línea TB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PIRAZI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Z : Pirazinamida2      2=Resistente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PIRAZI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PIRAZI2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Z - Pirazinamida (resultado 1): 1=Sensible, 2=Resistente, 3=No realizado; fármaco de primera línea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PIRAZI1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Z : Pirazinamida1        1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PIRAZI1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PIRAZI1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'E - Etambutol (resultado 2): 2=Resistente; fármaco de primera línea TB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ETAMBU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Etambul2         2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ETAMBU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ETAMBU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'E - Etambutol (resultado 1): 1=Sensible, 2=Resistente, 3=No realizado; fármaco de primera línea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ETAMBU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> E: Etambutol         almacena  1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ETAMBU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ETAMBU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'S - Isoniazida (resultado 3): 1=Sensible, 2=Resistente; fármaco de primera línea TB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ISONIA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Isoniazida3          almacena  1=Sensible  2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ISONIA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ISONIA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'S - Isoniazida (resultado 2): 2=Resistente; fármaco de primera línea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ISONIA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Isoniazida2       almacena   2=Resistente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ISONIA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ISONIA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'S - Isoniazida/Estreptomicina (resultado 1): 1=Sensible, 2=Resistente, 3=No realizado; prueba de sensibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ISONIA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Isoniazida - S: Estreptomicina  almacena  1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ISONIA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ISONIA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Polirresistencia - S - Estreptomicina (resultado 2): 1=Sensible, 2=Resistente, 3=No realizado; obsoleto V03_2023-04-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ESTREP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Polirresistente - S: Estreptomicina  almacena  1=Sensible  2=Resistente  3=No realizado    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ESTREP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ESTREP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monorresistencia - S - Estreptomicina (resultado 1): 1=Sensible, 2=Resistente, 3=No realizado; obsoleto V03_2023-04-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ESTREP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Monorresistencia - S: Estreptomicina  almacena  1=Sensible  2=Resistente  3=No realizado    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ESTREP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ESTREP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Resistencia a pre-XDR detectada; 1=Sí (verdadero), 0=No (falso); clasificación de TB resistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESPREXDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Resistencia a pre XDR  1 = true   o = false ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESPREXDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESPREXDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: RR (Resistencia a Rifampicina) confirmada; 1=Sí, 0=No; indicador clave de TB resistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESRIFAMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  RR (Resistencia a rifampicina)  1 = true   0 = false ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESRIFAMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESRIFAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Tuberculosis Extensivamente Resistente (XDR) confirmada; 1=Sí, 0=No; resistencia a múltiples fármacos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'XDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  XDR (Extensivamente resistente)  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'XDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'XDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Polirresistencia a TB detectada; 1=Sí (verdadero), 0=No (falso); múltiples resistencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'POLIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Polirresistente   1= true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'POLIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'POLIRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Tuberculosis Multirresistente (MDR) confirmada; 1=Sí, 0=No; resistencia a isoniazida y rifampicina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'MDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  MDR   1 = true    0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'MDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'MDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Monorresistencia a TB detectada; 1=Sí (verdadero), 0=No (falso); resistencia a un solo fármaco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'MONORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Monorresistencia  1 = true    0 =false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'MONORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'MONORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coomorbilidades/condiciones especiales que afectan manejo TB: 1=Diabetes, 2=Silicosis, 3=ERC, 4=EPOC, 5=Hepática, 6=Cáncer, 7=Artritis Reumatoide, 8=Desnutrición; obsoleto V03_2023-04-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'COOMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Coomorbilidades - condiciones especiales para el manejo  almacena     1=Diabetes  2=Silicosis  3=Enfermedad renal  4=EPOC  5=Enfermedad hepática  6=Cáncer  7=Artritis reumatoide  8=Desnutrición    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'COOMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'COOMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Prueba de tuberculina (Mantoux) realizada; 1=Sí, 0=No; prueba diagnóstica intradérmica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'TUBERCULINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Tuberculina  1 = Si     0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'TUBERCULINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'TUBERCULINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Adenopatía detectada (ganglios inflamados); 1=Sí, 0=No; hallazgo clínico físico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ADA  1 =Si      0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Hallazgo radiológico (radiografía de tórax) realizado; 1=Sí, 0=No; estudios de imagen diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RADIOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Radiológico  1 = Si   0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RADIOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RADIOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Nexo epidemiológico/contacto con TB confirmado; 1=Sí, 0=No; factor epidemiológico de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'NEXOEPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Nexo epidemiológico   =Si   0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'NEXOEPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'NEXOEPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Cuadro clínico compatible con TB documentado; 1=Sí, 0=No; signos y síntomas presentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CUADCLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el  Cuadro clínico   1 =Si  0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CUADCLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CUADCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Resultado de prueba de sensibilidad a fármacos (PSF) disponible; 1=Sí, 0=No; obsoleto V03_2023-04-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESPRUSENS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Resultado prueba de sensibilidad a fármacos (PSF)  almacena 1=true 0=false    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESPRUSENS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESPRUSENS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado histopatología (biopsia); 1=Positivo (granulomas/BAAR), 0=Negativo; confirmación diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESUHISPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Resultado hispatología   1 =  Postivo    0= Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESUHISPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESUHISPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Histopatología (biopsia) realizada; 1=Sí, 0=No; estudio de tejido para TB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'HISPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Hispatología   1 =  Si   0=  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'HISPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'HISPATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especie de Mycobacterium identificada: 1=M.tuberculosis, 2=M.bovis, 3=M.africanum, 4=M.microti, 5=M.canettii; cultivo/PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'NOMESPIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Especie Identificada:   1 = Mycobacterium tuberculosis    2 = Mycobacterium bovis    3 = Mycobacterium africanum    4 = Mycobacterium microti    5 = Mycobacterium canet tii     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'NOMESPIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'NOMESPIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado prueba molecular PCR (GeneXpert/similares) para confirmación TB; 1=Positivo, 0=Negativo; diagnóstico rápido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESULPRUEMOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Resultado prueba molecular para confirmación del caso  1 =Positivo     0=Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESULPRUEMOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESULPRUEMOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Prueba molecular (PCR/GeneXpert) para TB realizada; 1=Sí, 0=No; diagnóstico rápido de TB-RIF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PRUEMOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Prueba molecular   1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PRUEMOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PRUEMOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cultivo de Mycobacterium (gold estándar); 1=Positivo (TB confirmada), 2=Negativo, 3=En proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESUCULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Resultado cultivo  1 =  Positivo  2  = Negativo  3 =  en proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESUCULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESUCULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Cultivo de Mycobacterium realizado; 1=Sí, 0=No; prueba diagnóstica de referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CULTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el  Cultivo  1 =Si      0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CULTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CULTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado baciloscopia (frotis) microscópica; 1=Positivo (BAAR visible), 0=Negativo; prueba diagnóstica rápida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESULBACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Resultado baciloscopia    1 = Positivo     0 =Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESULBACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'RESULBACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: Baciloscopia (frotis microscópico) realizada; 1=Sí, 0=No; búsqueda de bacilos ácido-alcohol resistentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'BACILOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Baciloscopia   1 = Si      0  = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'BACILOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'BACILOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de Masa Corporal (peso/talla²) del paciente; numeral VARCHAR(50) en kg/m²; indicador nutricional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'IMC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  IMC (Índice masa corporal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'IMC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'IMC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla actual del paciente en metros (mts); numeral VARCHAR(50); medida antropométrica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'TALLAACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Talla actual (Mts)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'TALLAACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'TALLAACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso actual del paciente en kilogramos (Kg); numeral VARCHAR(50); medida antropométrica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Peso actual (Kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PESOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: ¿Paciente diagnosticado con VIH/SIDA?; 1=Sí, 0=No; comorbilidad inmunosupresora; obsoleto V03_2023-04-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PACCUENDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Paciente cuenta con diagnostico confirmado de vih ?   almacena 1=true 0=false    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PACCUENDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PACCUENDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ocupación/profesión del paciente; texto varchar(50); factor sociodemográfico y de riesgo ocupacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'OCUPACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la ocuopación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'OCUPACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'OCUPACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: ¿Paciente es trabajador de la salud?; 1=Sí (profesional sanitario), 0=No; factor de exposición ocupacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PACTRASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ¿El paciente es trabajador de la salud?   1 =  Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PACTRASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PACTRASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de paciente por historia previa de tratamiento TB: 1=Reingreso recaída, 2=Reingreso fracaso, 3=Recuperado pérdida seguimiento, 4=Otros previamente tratados, 5=TB sensible previo, 6=TB MDR/RR/XDR previo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PREVTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Previamente tratado  1 =Reingreso tras recaída     2 = Reingreso tras fracaso   3 =Recuperado tras pérdida al seguimiento     4 =  Otros casos previamente tratados   5 = Personas tratadas con tuberculosis sensible a los medicamentos  6 =Personas tratadas con tuberculosis con medicamentos de 2da línea (MDR, RR, XDR)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PREVTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'PREVTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación según antecedente de tratamiento TB: 1=Nuevo (sin tratamiento previo), 0=Previamente Tratado; categoría epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'SEGUANTECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Según antecedente de tratamiento  1 = Nuevo    0=Previamente Tratado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'SEGUANTECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'SEGUANTECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización de tuberculosis extrapulmonar: 1=Pleural, 2=Meníngea, 3=Peritoneal, 4=Ganglionar, 5=Renal, 6=Intestinal, 7=Osteoarticular, 8=Genitourinaria, 9=Pericárdica, 10=Cutánea, 11=Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'LOCALTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Localización de la tuberculosis extrapulmonar  1 =Pleural   2 =Meningea   3 =Peritoneal  4 = Ganglionar   5 =Renal  6 =Intestinal  7 =Osteoarticular  8 =Genitourinaria   9 = Pericárdica  10 =Cutánea  11 =Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'LOCALTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'LOCALTUBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tuberculosis del caso: 1=Pulmonar (TB-P, transmisible), 0=Extrapulmonar (TB-EP, no transmisible por aire)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'TIPOTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el   Tipo de tuberculosis  1  = Pulmonar   0 = Extrapulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'TIPOTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'TIPOTUBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de sensibilidad del paciente: 1=Sensible (sin resistencia a fármacos), 0=Resistente (MDR/RR/XDR confirmado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CONDICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la condicion  1 = Sensible      0 = Resistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CONDICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CONDICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 de tuberculosis (4 caracteres); ej: A15-A19 para TB; diagnóstico principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID/Llave foránea de la ficha de notificación origen (FK→HCFICHANOTIFICACION.ID); vincula datos clínicos TB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY); clave primaria secuencial de la tabla HCFICHA813 (ficha clínica TB)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación de tuberculosis (TB) del paciente: registra el diagnóstico, tipo y localización de la tuberculosis, antecedentes, resultados de pruebas diagnósticas (baciloscopia, cultivo, prueba molecular, histopatología), comorbilidades, perfil de resistencia a medicamentos antituberculosos (monoresistencia, MDR, XDR) y resultados de sensibilidad a fármacos de primera y segunda línea como estreptomicina, isoniazida, etambutol, pirazinamida, rifampicina, quinolonas e inyectables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA813';
