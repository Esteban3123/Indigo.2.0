CREATE TABLE [dbo].[HCFICHA850] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [MECAPROBA]           INT           NULL,
    [NOMMADRE]            VARCHAR (50)  NULL,
    [TIPOID]              VARCHAR (50)  NULL,
    [NUMIDENT]            VARCHAR (50)  NULL,
    [IDENTGEN]            INT           NULL,
    [DONOSANGRE]          BIT           NULL,
    [TIPOPRUEBA]          INT           NULL,
    [FECHARESUL]          DATE          NULL,
    [VALORCARGA]          VARCHAR (50)  NULL,
    [ESTADCLINI]          INT           NULL,
    [CANDIESO]            BIT           NULL,
    [CANDIVIA]            BIT           NULL,
    [TUBERPULM]           BIT           NULL,
    [CANCERCERV]          BIT           NULL,
    [TUBEREXTRA]          BIT           NULL,
    [COCCIDIO]            BIT           NULL,
    [CITOMEGA]            BIT           NULL,
    [RETINICITO]          BIT           NULL,
    [ENCEFALOVIH]         BIT           NULL,
    [OTRASMICRO]          BIT           NULL,
    [HISTOEXTRA]          BIT           NULL,
    [ISOSPOCRON]          BIT           NULL,
    [HERPESZOST]          BIT           NULL,
    [HISTODISEM]          BIT           NULL,
    [LINFBURKI]           BIT           NULL,
    [NEUMOPNEUMO]         BIT           NULL,
    [NEUMORECU]           BIT           NULL,
    [LINFOINMU]           BIT           NULL,
    [CRIPTOCRON]          BIT           NULL,
    [CRIPTOEXTRA]         BIT           NULL,
    [SARCOKAPOSI]         BIT           NULL,
    [SINDROEMAC]          BIT           NULL,
    [LEUCOMULTI]          BIT           NULL,
    [SEPTIRECUR]          BIT           NULL,
    [TOXOCERE]            BIT           NULL,
    [HEPATB]              BIT           NULL,
    [HEPATC]              BIT           NULL,
    [MENINGITIS]          BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA850] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA850_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA850_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA850] NOCHECK CONSTRAINT [CK_HCFICHA850_JSON];




GO
ALTER TABLE [dbo].[HCFICHA850] NOCHECK CONSTRAINT [CK_HCFICHA850_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON; almacena nuevas columnas y extensiones de la ficha de notificación de VIH/SIDA (VARCHAR MAX, validado con ISJSON)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación (VARCHAR 20); NULL indica primera versión; facilita seguimiento de cambios en el formulario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Meningitis (BIT); 1=Sí presente, 0=No presente; complicación oportunista en pacientes VIH/SIDA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'MENINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Meningitis      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'MENINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'MENINGITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Hepatitis C (BIT); 1=Sí presente, 0=No presente; coinfección viral en notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HEPATC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Hepatitis C      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HEPATC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HEPATC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Hepatitis B (BIT); 1=Sí presente, 0=No presente; coinfección viral en notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HEPATB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Hepatitis B      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HEPATB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HEPATB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Toxoplasmosis Cerebral (BIT); 1=Sí presente, 0=No presente; infección oportunista de SNC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TOXOCERE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Toxoplasmosis Cerebral    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TOXOCERE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TOXOCERE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Septicemia Recurrente por Salmonella (BIT); 1=Sí presente, 0=No presente; bacteremia recurrente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SEPTIRECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Septicemia Recurrente por Salmonella    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SEPTIRECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SEPTIRECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Leucoencefalopatía Multifocal Progresiva (BIT); 1=Sí presente, 0=No presente; infección de SNC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LEUCOMULTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Leucoencefalopatía Multifocal    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LEUCOMULTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LEUCOMULTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Síndrome de Emaciación (BIT); 1=Sí presente, 0=No presente; pérdida de peso progresiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SINDROEMAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Síndrome de Emaciación    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SINDROEMAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SINDROEMAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Sarcoma de Kaposi (BIT); 1=Sí presente, 0=No presente; neoplasia relacionada con SIDA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SARCOKAPOSI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Sarcoma de Kaposi    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SARCOKAPOSI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SARCOKAPOSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Criptococosis Extrapulmonar (BIT); 1=Sí presente, 0=No presente; micosis diseminada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CRIPTOEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Criptococosis Extrapulmonar    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CRIPTOEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CRIPTOEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Criptosporidiasis Crónica (BIT); 1=Sí presente, 0=No presente; diarrea oportunista persistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CRIPTOCRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Criptosporidiasis Crónica    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CRIPTOCRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CRIPTOCRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Linfoma Inmunoblástico (BIT); 1=Sí presente, 0=No presente; neoplasia maligna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LINFOINMU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Linfoma Inmunoblástico     1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LINFOINMU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LINFOINMU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Neumonía Recurrente (>2 episodios/año) (BIT); 1=Sí presente, 0=No presente; marcador de inmunosupresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NEUMORECU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Neumonía Recurrente (Más de 2 episodios en un año)     1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NEUMORECU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NEUMORECU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Neumonía por Pneumocystis jirovecii (BIT); 1=Sí presente, 0=No presente; infección pulmonar oportunista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NEUMOPNEUMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Neumonía por Pneumocystis      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NEUMOPNEUMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NEUMOPNEUMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Linfoma de Burkitt (BIT); 1=Sí presente, 0=No presente; neoplasia maligna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LINFBURKI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Linfoma de Burkitt      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LINFBURKI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LINFBURKI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Histoplasmosis Diseminada (BIT); 1=Sí presente, 0=No presente; micosis sistémica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HISTODISEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Histoplasmosis Diseminada      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HISTODISEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HISTODISEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Herpes Zóster en Múltiples Dermatomas (BIT); 1=Sí presente, 0=No presente; reactivación viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HERPESZOST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Herpes zoster en Multiples Dermatomas      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HERPESZOST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HERPESZOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Isosporidiasis Crónica (BIT); 1=Sí presente, 0=No presente; parasitosis intestinal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ISOSPOCRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  IsosporidiasisCrónica      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ISOSPOCRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ISOSPOCRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Histoplasmosis Extrapulmonar (BIT); 1=Sí presente, 0=No presente; micosis diseminada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HISTOEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Histoplasmosis Extrapulmonar      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HISTOEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HISTOEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Otras Microbacterias no tuberculosas (BIT); 1=Sí presente, 0=No presente; infecciones atípicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'OTRASMICRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Otras Microbactérias      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'OTRASMICRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'OTRASMICRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Encefalopatía por VIH (BIT); 1=Sí presente, 0=No presente; afectación neurológica directa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ENCEFALOVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Encefalopatía por VIH      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ENCEFALOVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ENCEFALOVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Retinitis por Citomegalovirus (BIT); 1=Sí presente, 0=No presente; complicación oftalmológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'RETINICITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona Retinitis por Citomegalovirus      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'RETINICITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'RETINICITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Citomegalovirosis (BIT); 1=Sí presente, 0=No presente; infección viral oportunista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CITOMEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona   Citomegalovirosis    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CITOMEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CITOMEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Coccidiodomicosis (BIT); 1=Sí presente, 0=No presente; micosis sistémica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'COCCIDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona   Coccidiodomicosis    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'COCCIDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'COCCIDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Tuberculosis Extrapulmonar (BIT); 1=Sí presente, 0=No presente; TB diseminada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TUBEREXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona   Tuberculosis Extrapulmonar    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TUBEREXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TUBEREXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Cáncer Cervical Invasivo (BIT); 1=Sí presente, 0=No presente; neoplasia VPH-asociada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANCERCERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona Cáncer Cervical Invasivo    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANCERCERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANCERCERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Tuberculosis Pulmonar (BIT); 1=Sí presente, 0=No presente; infección oportunista frecuente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TUBERPULM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona Tuberculosis Pulmonar    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TUBERPULM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TUBERPULM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Candidiasis de Vías Aéreas (BIT); 1=Sí presente, 0=No presente; infección fúngica oropharyngea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANDIVIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Candidiasis de las Vías Areas    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANDIVIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANDIVIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad asociada: Candidiasis Esofágica (BIT); 1=Sí presente, 0=No presente; infección fúngica gastrointestinal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANDIESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Enfermedades asociadas si, se selecciona  Candidiasis Esofágica    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANDIESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANDIESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado clínico del paciente (INT): 1=VIH asintomático, 2=SIDA, 3=Fallecido; categoría de progresión epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ESTADCLINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Estado clínico  1 = Vih  2 = Sida   3 = Muerto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ESTADCLINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ESTADCLINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de carga viral en número de copias de VIH/mL plasma (VARCHAR 50); resultado cuantitativo de carga viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'VALORCARGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Valor de la carga viral (N° de copias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'VALORCARGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'VALORCARGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado de prueba diagnóstica (DATE); marca temporalidad de confirmación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba de confirmación diagnóstica (INT): 1=Western Blot, 2=Carga Viral, 3=Prueba Rápida, 4=ELISA; método serológico/virológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TIPOPRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guara el Tipo de prueba de confirmación de diagnóstico  1 =Western Blot   2 = Carga Viral   3 = Prueba Rápida    4 =Elisa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TIPOPRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TIPOPRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Donó sangre en últimos 12 meses? (BIT); 1=Sí, 0=No; factor de transmisión potencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'DONOSANGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda ¿Donó sangre en los 12 meses anteriores?  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'DONOSANGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'DONOSANGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidad de género del paciente (INT); dato sociodemográfico sensible de la notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'IDENTGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Identidad de género', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'IDENTGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'IDENTGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del paciente (VARCHAR 50, PII-Ofuscado); cédula/documento único', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Número de identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identificación de la madre (VARCHAR 50): RC=Registro Civil, TI=Tarjeta Identidad, CC=Cédula Ciudadanía, CE=Cédula Extranjería, PA=Pasaporte, MS=Menor Sin Identificación, AS=Adulto Sin Identificación, PE=Permiso Especial, CN=Cédula Nit, PT=Permiso Temporal; aplica transmisión materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo ID (Documento Identificación) de la Madre --> desde versión ''''V01_2020-03-06'''' es con una enumeración:  1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE   9 = CN   10=PT  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la madre (VARCHAR 50); usado cuando hay transmisión materna del VIH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NOMMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Nombre de la madre (En caso de transmisión materna)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NOMMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NOMMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mecanismo probable de transmisión (INT): 1=Heterosexual, 2=Homosexual, 3=Bisexual, 4=Materno-infantil, 5=Transfusión Sanguínea, 6=Usuarios Drogas IV, 7=Accidente Laboral, 8=Trasplante Órganos, 9=Piercing, 10=Hemodiálisis, 11=Tatuajes, 12=Centro Estético, 13=Acupuntura; categorización epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'MECAPROBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Mecanismo probable de transmisión      1 = Heterosexual  2=Homosexual    3 = Bisexual     4 = Materno Infantil    5 = Transfusión Sanguínea     6 =Usuarios drogas IV     7 = Accidente de Trabajo    8 =Transplante de Órganos    9 = Piercing  10=Hemodiálisis    11 =Tatuajes 12 =Centro estético  13  =Acupuntura   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'MECAPROBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'MECAPROBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CHAR 4); identificador de patología registrada en notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la ficha de notificación (INT, FK); referencia principal a HCFICHANOTIFICACION; clave foránea para trazabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único/consecutivo de registro (INT, PK IDENTITY); clave primaria de la tabla HCFICHA850', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica para casos de VIH/SIDA (evento 850 del SIVIGILA). Registra el detalle clínico del paciente notificado: identificación, diagnóstico CIE-10, estado clínico, resultados de pruebas, carga viral y las enfermedades oportunistas o definitorias de SIDA presentadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850';
