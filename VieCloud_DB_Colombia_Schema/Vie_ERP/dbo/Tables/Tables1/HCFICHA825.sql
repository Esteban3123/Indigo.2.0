CREATE TABLE [dbo].[HCFICHA825] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT          NOT NULL,
    [CODDIAGNO]           CHAR (4)     NULL,
    [CONDICION]           BIT          NULL,
    [TIPOTUBER]           BIT          NULL,
    [LOCALTUBER]          INT          NULL,
    [SEGUANTECE]          BIT          NULL,
    [PREVTRAT]            INT          NULL,
    [PACTRASAL]           BIT          NULL,
    [OCUPACION]           VARCHAR (50) NULL,
    [PACCUENDIA]          BIT          NULL,
    [PESOACT]             VARCHAR (50) NULL,
    [TALLAACT]            VARCHAR (50) NULL,
    [IMC]                 VARCHAR (50) NULL,
    [BACILOS]             BIT          NULL,
    [RESULBACI]           BIT          NULL,
    [CULTIVO]             BIT          NULL,
    [RESUCULT]            BIT          NULL,
    [PRUEMOL]             BIT          NULL,
    [RESULPRUEMOL]        BIT          NULL,
    [NOMESPIDENT]         INT          NULL,
    [HISPATO]             BIT          NULL,
    [RESUHISPAT]          BIT          NULL,
    [RESPRUSENS]          BIT          NULL,
    [CUADCLI]             BIT          NULL,
    [NEXOEPI]             BIT          NULL,
    [RADIOL]              BIT          NULL,
    [ADA]                 BIT          NULL,
    [TUBERCULINA]         BIT          NULL,
    [COOMOR]              INT          NULL,
    [MONORES]             BIT          NULL,
    [MDR]                 BIT          NULL,
    [POLIRES]             BIT          NULL,
    [XDR]                 BIT          NULL,
    [RESRIFAMP]           BIT          NULL,
    [RESPREXDR]           BIT          NULL,
    [ESTREP1]             INT          NULL,
    [ESTREP2]             INT          NULL,
    [ISONIA1]             INT          NULL,
    [ISONIA2]             INT          NULL,
    [ISONIA3]             INT          NULL,
    [ETAMBU1]             INT          NULL,
    [ETAMBU2]             INT          NULL,
    [PIRAZI1]             INT          NULL,
    [PIRAZI2]             INT          NULL,
    [RIFAMP1]             INT          NULL,
    [RIFAMP2]             INT          NULL,
    [QUINDO1]             INT          NULL,
    [QUINDO2]             INT          NULL,
    [INYECT1]             INT          NULL,
    [INYECT2]             INT          NULL,
    [FECHACONFIR]         DATE         NULL,
    CONSTRAINT [PK_HCFICHA825] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA825_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación diagnóstica de tuberculosis (DATE). Marca el momento en que se confirma el caso de TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de confirmación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resistencia a fármacos inyectables en perfil pre-XDR (INT: 1=Sensible, 2=Resistente). Obsoleto desde v03 2023-04-01, almacena nulos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'INYECT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Resistencia a pre XDR - I: Inyectables  almacena  1=Sensible  2=Resistente    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'INYECT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'INYECT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad a fármacos inyectables en perfil XDR (INT: 1=Sensible). Obsoleto desde v03 2023-04-01, almacena nulos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'INYECT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> XDR - I: Inyectables  almacena  1=Sensible    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'INYECT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'INYECT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resistencia a quinolonas en perfil pre-XDR (INT: 1=Sensible, 2=Resistente). Obsoleto desde v03 2023-04-01, almacena nulos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'QUINDO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Resistencia a pre XDR - Q: Quindonas  almacena  1=Sensible  2=Resistente    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'QUINDO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'QUINDO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad a quinolonas en perfil XDR (INT: 1=Sensible). Obsoleto desde v03 2023-04-01, almacena nulos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'QUINDO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> XDR - Q: Quindonas  almacena  1=Sensible    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'QUINDO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'QUINDO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resistencia a rifampicina - segunda línea (INT: 2=Resistente). Prueba de sensibilidad a fármacos anti-TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RIFAMP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> R: Rifampicina2 :      2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RIFAMP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RIFAMP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad a rifampicina - primera línea (INT: 1=Sensible, 2=Resistente, 3=No realizado). Fármaco clave TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RIFAMP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> R: Rifampicina    almacena  1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RIFAMP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RIFAMP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resistencia a pirazinamida - segunda línea (INT: 2=Resistente). Prueba de sensibilidad a fármacos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PIRAZI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Z : Pirazinamida2       2=Resistente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PIRAZI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PIRAZI2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad a pirazinamida - primera línea (INT: 1=Sensible, 2=Resistente, 3=No realizado). Fármaco TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PIRAZI1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Z : Pirazinamida1        1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PIRAZI1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PIRAZI1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resistencia a etambutol - segunda línea (INT: 2=Resistente). Prueba de sensibilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ETAMBU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Etambul2         2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ETAMBU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ETAMBU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad a etambutol - primera línea (INT: 1=Sensible, 2=Resistente, 3=No realizado). Fármaco TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ETAMBU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> E: Etambutol         almacena  1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ETAMBU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ETAMBU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad a isoniazida - tercer nivel (INT: 1=Sensible, 2=Resistente). Prueba de sensibilidad a fármacos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Isoniazida3          almacena  1=Sensible  2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resistencia a isoniazida - segundo nivel (INT: 2=Resistente). Prueba de sensibilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Isoniazida2       almacena   2=Resistente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad a isoniazida - primer nivel (INT: 1=Sensible, 2=Resistente, 3=No realizado). Fármaco TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Isoniazida - S: Estreptomicina  almacena  1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resistencia a estreptomicina en polirresistencia (INT: 1=Sensible, 2=Resistente, 3=No realizado). Obsoleto desde v03 2023-04-01.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ESTREP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Polirresistente - S: Estreptomicina  almacena  1=Sensible  2=Resistente  3=No realizado    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ESTREP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ESTREP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad a estreptomicina en monorresistencia (INT: 1=Sensible, 2=Resistente, 3=No realizado). Obsoleto desde v03 2023-04-01.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ESTREP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Monorresistencia - S: Estreptomicina  almacena  1=Sensible  2=Resistente  3=No realizado    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ESTREP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ESTREP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de resistencia a pre-XDR (BIT: 1=Sí/true, 0=No/false). Patrón de resistencia extendida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESPREXDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Resistencia a pre XDR  1 = true   o = false ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESPREXDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESPREXDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de resistencia a rifampicina/RR (BIT: 1=Sí/true, 0=No/false). Critério TB resistente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESRIFAMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  RR (Resistencia a rifampicina)  1 = true   0 = false ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESRIFAMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESRIFAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tuberculosis XDR extensamente resistente (BIT: 1=Sí/true, 0=No/false). Patrón máximo resistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'XDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  XDR  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'XDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'XDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de polirresistencia TB (BIT: 1=Sí/true, 0=No/false). Resistencia a múltiples fármacos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'POLIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Polirresistente   1= true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'POLIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'POLIRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tuberculosis MDR multidrogoresistente (BIT: 1=Sí/true, 0=No/false). Resistente a isoniazida y rifampicina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'MDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  MDR   1 = true    0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'MDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'MDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de monorresistencia TB (BIT: 1=Sí/true, 0=No/false). Resistencia a un solo fármaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'MONORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Monorresistencia  1 = true    0 =false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'MONORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'MONORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comorbilidades/condiciones especiales manejo TB (INT: 1=Diabetes, 2=Silicosis, 3=Renal, 4=EPOC, 5=Hepática, 6=Cáncer, 7=Artritis Reum., 8=Desnutrición). Obsoleto desde v03 2023-04-01.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'COOMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Coomorbilidades - condiciones especiales para el manejo  almacena     1=Diabetes  2=Silicosis  3=Enfermedad renal  4=EPOC  5=Enfermedad hepática  6=Cáncer  7=Artritis reumatoide  8=Desnutrición    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'COOMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'COOMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado prueba tuberculínica/intradérmica (BIT: 1=Positivo/Sí, 0=Negativo/No). Diagnóstico TB latente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TUBERCULINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Tuberculina  1 = Si     0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TUBERCULINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TUBERCULINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de adenopatía en evaluación TB (BIT: 1=Presente/Sí, 0=Ausente/No). Signo clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ADA  1 =Si      0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de hallazgo radiológico compatible TB (BIT: 1=Presente/Sí, 0=Ausente/No). Imagenología torácica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RADIOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Radiológico  1 = Si   0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RADIOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RADIOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de nexo epidemiológico TB (BIT: 1=Presente/Sí, 0=Ausente/No). Contacto o exposición TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'NEXOEPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Nexo epidemiológico   =Si   0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'NEXOEPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'NEXOEPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cuadro clínico compatible TB (BIT: 1=Presente/Sí, 0=Ausente/No). Síntomas respiratorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CUADCLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el  Cuadro clínico   1 =Si  0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CUADCLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CUADCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado prueba sensibilidad/PSF a fármacos anti-TB (BIT: 1=Positivo/true, 0=Negativo/false). Obsoleto desde v03 2023-04-01.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESPRUSENS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Resultado prueba de sensibilidad a fármacos (PSF)  almacena 1=true 0=false    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESPRUSENS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESPRUSENS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado histopatología/biopsia (BIT: 1=Positivo, 0=Negativo). Confirmación anátomo-patológica TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESUHISPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Resultado hispatología   1 =  Positivo    0= Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESUHISPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESUHISPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estudio histopatológico realizado (BIT: 1=Sí, 0=No). Biopsia para diagnóstico TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'HISPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Hispatología   1 =  Si   0=  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'HISPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'HISPATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de paciente TB por categoría tratamiento (INT: 1=Recaída, 2=Fracaso, 3=Recuperado/Pérdida seguimiento, 4=Otros previos, 5=1ra línea, 6=2da línea). Especie/categoría identificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'NOMESPIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Especie Identificada:   1 = Tras Recaída   2 = Tras Fracaso    3 = Paciente Recuperado tras Pérdida al Seguimiento    4 = Otros Pacientes Previamente Tratados    5 = Tratamiento con Medicamento de 1ra Línea  6 =  Tratamiento con Medicamento de 2da Línea    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'NOMESPIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'NOMESPIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado prueba molecular/PCR TB (BIT: 1=Positivo, 0=Negativo). Confirmación molecular M. tuberculosis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESULPRUEMOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Resultado prueba molecular para confirmación del caso  1 =Positivo     0=Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESULPRUEMOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESULPRUEMOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de prueba molecular realizada (BIT: 1=Sí, 0=No). Test PCR/GeneXpert diagnóstico TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PRUEMOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Prueba molecular   1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PRUEMOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PRUEMOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cultivo de M. tuberculosis (INT: 1=Positivo, 2=Negativo, 3=En proceso). Confirmación microbiana TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESUCULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Resultado cultivo  1 =  Positivo  2  = Negativo  3 =  en proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESUCULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESUCULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cultivo TB solicitado/realizado (BIT: 1=Sí, 0=No). Aislamiento M. tuberculosis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CULTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el  Cultivo  1 =Si      0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CULTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CULTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado baciloscopia/microscopía (BIT: 1=Positivo, 0=Negativo). Búsqueda BAAR muestras respiratorias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESULBACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Resultado baciloscopia    1 = Positivo     0 =Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESULBACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESULBACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de baciloscopia realizada (BIT: 1=Sí, 0=No). Examen de frotis BAAR/TBC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'BACILOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Baciloscopia   1 = Si      0  = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'BACILOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'BACILOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de masa corporal calculado (VARCHAR 50, Kg/m²). Evaluación nutricional TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'IMC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  IMC (Índice masa corporal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'IMC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'IMC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla actual del paciente (VARCHAR 50, metros). Antropometría TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TALLAACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Talla actual (Mts)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TALLAACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TALLAACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso actual del paciente (VARCHAR 50, kilogramos). Antropometría TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Peso actual (Kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PESOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador VIH/SIDA coinfección TB (BIT: 1=Sí/true, 0=No/false). Diagnóstico VIH confirmado. Obsoleto desde v03 2023-04-01.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PACCUENDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo --> Paciente cuenta con diagnostico confirmado de vih ?   almacena 1=true 0=false    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PACCUENDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PACCUENDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ocupación/profesión del paciente TB (VARCHAR 50). Campo laboral exposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'OCUPACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la ocuopación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'OCUPACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'OCUPACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador trabajador de salud (BIT: 1=Sí, 0=No). Personal sanitario/salud con TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PACTRASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ¿El paciente es trabajador de la salud?   1 =  Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PACTRASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PACTRASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría paciente según tratamiento previo (INT: 1=Recaída, 2=Fracaso, 3=Recuperado/Pérdida, 4=Otros previos, 5=1ra línea, 6=2da línea). Historial TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PREVTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Previamente tratado  1 = tras recaída     2 =  tras fracaso   3 =Paciente Recuperado tras Pérdida al Seguimiento     4 =Otros Pacientes Previamente Tratados  5 = Tratamiento con Medicamento de 1ra Línea   6 = Tratamiento con Medicamento de 2da Línea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PREVTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PREVTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación según antecedente tratamiento TB (BIT: 1=Nuevo, 0=Previamente tratado). Historial previo TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'SEGUANTECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Según antecedente de tratamiento  1 = Nuevo    0=Previamente Tratado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'SEGUANTECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'SEGUANTECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización tuberculosis extrapulmonar (INT: 1=Pleural, 2=Meníngea, 3=Peritoneal, 4=Ganglionar, 5=Renal, 6=Intestinal, 7=Osteoarticular, 8=Genitourinaria, 9=Pericárdica, 10=Cutánea, 11=Otra). Sitio afectación TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'LOCALTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Localización de la tuberculosis extrapulmonar  1 =Pleural   2 =Meningea   3 =Peritoneal  4 = Ganglionar   5 =Renal  6 =Intestinal  7 =Osteoarticular  8 =Genitourinaria   9 = Pericárdica  10 =Cutánea  11 =Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'LOCALTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'LOCALTUBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo tuberculosis clasificación (BIT: 1=Pulmonar, 0=Extrapulmonar). Localización TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TIPOTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el   Tipo de tuberculosis  1  = Pulmonar   0 = Extrapulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TIPOTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TIPOTUBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición sensibilidad fármaco (BIT: 1=Sensible, 0=Resistente). Estado resistencia TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CONDICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la condicion  1 = Sensible      0 = Resistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CONDICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CONDICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico TB CIE-10 (CHAR 4). Identificador diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID ficha notificación TB (INT, FK→HCFICHANOTIFICACION). Referencia evento notificable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único registro HCFICHA825 (INT IDENTITY, PK). Consecutivo tabla TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación de tuberculosis (programa de control de TB). Registra el diagnóstico, tipo y localización de la tuberculosis, antecedentes, resultados de pruebas bacteriológicas y moleculares, comorbilidades, perfil de resistencia a antibióticos y esquema de tratamiento farmacológico del paciente notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825';
