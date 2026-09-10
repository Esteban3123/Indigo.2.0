CREATE TABLE [dbo].[HCFICHA800] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT          NOT NULL,
    [CODDIAGNO]           CHAR (4)     NULL,
    [NOMMADRE]            VARCHAR (50) NULL,
    [TIPOID]              VARCHAR (50) NULL,
    [NUMIDENT]            VARCHAR (50) NULL,
    [CASOIDENT]           INT          NULL,
    [DOSISAPLI]           INT          NULL,
    [TIPOVACUNA]          INT          NULL,
    [FECHAULTDOS1]        DATE         NULL,
    [FECHAULTDOS2]        DATE         NULL,
    [ANTEMATEVAC]         BIT          NULL,
    [INFERESPI]           BIT          NULL,
    [ETAPAENFE]           INT          NULL,
    [TOS]                 BIT          NULL,
    [DURATOS]             VARCHAR (50) NULL,
    [TOSPARO]             BIT          NULL,
    [ESTRIDOR]            BIT          NULL,
    [APNEA]               BIT          NULL,
    [CIANOSIS]            BIT          NULL,
    [VOMITO]              BIT          NULL,
    [COMPLICA]            BIT          NULL,
    [TIPOCOMPLI]          INT          NULL,
    [TRATAANTIBIO]        BIT          NULL,
    [TIPOANTIBIO]         VARCHAR (50) NULL,
    [DURATRATA]           VARCHAR (50) NULL,
    [FECHAINVEST]         DATE         NULL,
    [FECHATOMA1]          DATE         NULL,
    [FECHATOMA2]          DATE         NULL,
    [FECHARECE1]          DATE         NULL,
    [FECHARECE2]          DATE         NULL,
    [FECHARESUL1]         DATE         NULL,
    [FECHARESUL2]         DATE         NULL,
    [MUESTRA1]            INT          NULL,
    [MUESTRA2]            INT          NULL,
    [PRUEBA1]             INT          NULL,
    [PRUEBA2]             INT          NULL,
    [AGENTE1]             INT          NULL,
    [AGENTE2]             INT          NULL,
    [RESULTADO1]          INT          NULL,
    [RESULTADO2]          INT          NULL,
    [INVESTCAMP]          BIT          NULL,
    [VERSION]             VARCHAR (20) NULL,
    CONSTRAINT [PK_HCFICHA800] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA800_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación epidemiológica; NULL=primera versión, VARCHAR(20), control de cambios y actualizaciones de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Investigación de campo efectiva (seguimiento epidemiológico): 1=Sí, 0=No; BIT, vigilancia activa de contactos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'INVESTCAMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Investigación de campo efectiva (SEGUIMIENTO, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'INVESTCAMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'INVESTCAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de laboratorio segunda muestra (1=POSITIVO, 2=NEGATIVO, 3=NO PROCESADO, 4=INADECUADO, 5=BORDERLINE); INT, confirmación diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado (Datos de Laboratorio, (1. POSITIVO, 2. NEGATIVO, 3. NO PROCESADO, 4. INADECUADO, 5. BORDELINE))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de laboratorio primera muestra (1=POSITIVO, 2=NEGATIVO, 3=NO PROCESADO, 4=INADECUADO, 5=BORDERLINE); INT, diagnóstico confirmatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'RESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado (Datos de Laboratorio, (1. POSITIVO, 2. NEGATIVO, 3. NO PROCESADO, 4. INADECUADO, 5. BORDELINE))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'RESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'RESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente causal identificado segunda muestra (1=BORDETELLA PERTUSSIS, 2=BORDETELLA PARAPERTUSSIS, 3=BORDETELLA SPP, 4=BORDETELLA HOLMESII); INT, microorganismo patógeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'AGENTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente (DATOS DE LABORATORIO, (1. BORDETELLA PERTUSSIS, 2. BORDETELLA PARAPERTUSSIS, 3. BORDETELLA SPP, 4. BORDETELLA HOLMESII))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'AGENTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'AGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente causal identificado primera muestra (1=BORDETELLA PERTUSSIS, 2=BORDETELLA PARAPERTUSSIS, 3=BORDETELLA SPP, 4=BORDETELLA HOLMESII); INT, microorganismo etiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'AGENTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente (DATOS DE LABORATORIO, (1. BORDETELLA PERTUSSIS, 2. BORDETELLA PARAPERTUSSIS, 3. BORDETELLA SPP, 4. BORDETELLA HOLMESII))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'AGENTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'AGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba segunda muestra (1=CULTIVO, 2=PATOLOGÍA, 3=PCR, 4=IgG); INT, metodología diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'PRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba (Datos de Laboratorio, (1. CULTIVO, 2. PATOLOGÍA, 3. PCR, 4. IgG))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'PRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'PRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba primera muestra (1=CULTIVO, 2=PATOLOGÍA, 3=PCR, 4=IgG); INT, técnica de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'PRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba (Datos de Laboratorio, (1. CULTIVO, 2. PATOLOGÍA, 3. PCR, 4. IgG))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'PRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'PRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de muestra segunda toma (1=HISOPADO NASOFARINGEO, 2=ASPIRADO NASOFARINGEO, 3=SUERO, 4=TEJIDO, 5=LAVADO BRONQUIAL); INT, espécimen biológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'MUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra (Datos de Laboratorio, (1. HISOPADO NASOFARINGEO, 2. ASPIRADO NASOFARINGEO, 3. SUERO, 4. TEJIDO, 5. LAVADO BRONQUIAL))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'MUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'MUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de muestra primera toma (1=HISOPADO NASOFARINGEO, 2=ASPIRADO NASOFARINGEO, 3=SUERO, 4=TEJIDO, 5=LAVADO BRONQUIAL); INT, especímen clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'MUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra (Datos de Laboratorio, (1. HISOPADO NASOFARINGEO, 2. ASPIRADO NASOFARINGEO, 3. SUERO, 4. TEJIDO, 5. LAVADO BRONQUIAL))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'MUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'MUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado segunda muestra (laboratorio); DATE, entrega de informe diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARESUL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado (Datos Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARESUL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARESUL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado primera muestra (laboratorio); DATE, reporte de análisis clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARESUL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado (Datos Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARESUL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARESUL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción segunda muestra (laboratorio); DATE, ingreso de espécimen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARECE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción (Datos Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARECE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARECE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción primera muestra (laboratorio); DATE, ingreso de muestra biológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARECE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción (Datos Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARECE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHARECE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma segunda muestra (laboratorio); DATE, recolección de especímen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la toma (Datos Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma primera muestra (laboratorio); DATE, recolección de espécimen clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la toma (Datos Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de investigación de campo (seguimiento); DATE, actividades epidemiológicas de rastreo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHAINVEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de investigación de campo (Seguimiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHAINVEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHAINVEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración del tratamiento antibiótico en días; VARCHAR(50), periodo de farmacoterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'DURATRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración del tratamiento "días" (Tratamiento Específico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'DURATRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'DURATRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de antibiótico administrado; VARCHAR(50), fármaco antimicrobiano específico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Antibiótico (Tratamiento Específico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOANTIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tratamiento antibiótico recibido (1=Sí, 0=No); BIT, terapia antimicrobiana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TRATAANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tratamiento antibiótico (Tratamiento Específico, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TRATAANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TRATAANTIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de complicación clínica (1=Convulsiones, 2=Atelectasia, 3=Neumotórax, 4=Neumonía, 5=Otro); INT, complicación secundaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de complicaciones (DATOS CLÍNICOS, (1. Convulsiones, 2. Atelectasia, 3. Neumotórax, 4. Neumonía, 5. Otro))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicaciones clínicas presentes (1=Sí, 0=No); BIT, eventos adversos del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'COMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones (DATOS CLÍNICOS, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'COMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'COMPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vómito postusivo (después de la tos); BIT, síntoma clínico, manifestación gastrointestinal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vómito postusivo (DATOS CLÍNICOS, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cianosis (coloración azulada de piel/mucosas); BIT, signo de hipoxia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'CIANOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cianosis (DATOS CLÍNICOS, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'CIANOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'CIANOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apnea (pausas respiratorias); BIT, evento respiratorio, signo crítico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'APNEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apnea (DATOS CLÍNICOS, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'APNEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'APNEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estridor (ruido respiratorio anormal); BIT, síntoma de obstrucción de vía aérea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ESTRIDOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ESTRIDOR (DATOS CLÍNICOS, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ESTRIDOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ESTRIDOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tos paroxística (accesos de tos severa); BIT, manifestación clínica cardinal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TOSPARO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tos paroxística (DATOS CLÍNICOS, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TOSPARO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TOSPARO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la tos en días; VARCHAR(50), período sintomático respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'DURATOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración de la tos "días" (DATOS CLÍNICOS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'DURATOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'DURATOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tos presente (síntoma respiratorio); BIT, manifestación clínica primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tos (DATOS CLÍNICOS, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etapa clínica de enfermedad (1=Catarral, 2=Espasmódica, 3=Convaleciente); INT, estadio evolutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ETAPAENFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Etapa de la enfermedad (DATOS CLÍNICOS, (1. Catarral, 2. Espasmódica, 3. Convaleciente))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ETAPAENFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ETAPAENFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infección respiratoria concomitante (1=Sí, 2=No); INT, coinfección respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'INFERESPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Infección respiratoria (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'INFERESPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'INFERESPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de vacunación materna Tdap (1=Sí, 2=No); BIT, inmunización prenatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ANTEMATEVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedente materno de vacunación Tadp (ANTECEDENTES, (1. Sí, 2 No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ANTEMATEVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ANTEMATEVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última dosis segunda vacuna antipertussis; DATE, historial de inmunización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado (DATOS DE LABORATORIO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última dosis primera vacuna antipertussis; DATE, registro de vacunación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado (DATOS DE LABORATORIO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vacuna antipertussis (1=DPT, 2=Pentavalente, 3=Tdap); INT, esquema de inmunización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de vacuna (ANTECEDENTES, (1. DPT, 2. Pentavalente, 3. Tadp))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de vacuna antipertussis aplicadas (1=Ninguna, 2=Una, 3=Dos, 4=Tres, 5=Primer refuerzo, 6=Segundo refuerzo); INT, cobertura vacunal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis aplicadas de vacuna antipertussis (ANTECEDENTES, (1. Ninguna, 2. Una, 3. Dos, 4. Tres, 5. Primer refuerzo, 6. Segundo refuerzo))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caso identificado por (1=Consulta externa, 2=Urgencias, 3=Hospitalización, 4=Búsqueda comunitaria); INT, puerta de entrada diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'CASOIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso identificado por (ANTECEDENTES, (1. Consulta externa, 2. Urgencias, 3. Hospitalización, 4. Búsquedad comunitaria))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'CASOIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'CASOIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del paciente (cédula, pasaporte, registro civil); VARCHAR(50), PII - Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de identificación (INFORMACIÓN GENERAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación (1=RC, 2=TI, 3=CC, 4=CE, 5=PA, 6=MS, 7=AS, 8=PE, 9=PT); VARCHAR(50), documento de identidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de identificación (Informacipón general, (1. RC - Registro Civil, 2. TI - Tarjeta de Identidad, 3. CC - Cédula de Ciudadanía, 4. CE - Cédula de Extranjería, 5. PA - Pasaporte, 6. MS - Menor Sin Identificación, 7. AS - Adulto Sin Identificación, 8. PE - Permiso Especial de Permanencia, 9. PT - Permiso Temporal de Permanencia))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la madre del paciente; VARCHAR(50), información de familiar responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'NOMMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la madre (Información general)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'NOMMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'NOMMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CIE-10); CHAR(4), clasificación diagnóstica pertussis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación epidemiológica (FK→HCFICHANOTIFICACION.ID); INT, relación padre con evento notificable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo del registro; INT IDENTITY, clave primaria de auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica para casos de tosferina (código 800 del SIVIGILA). Registra los datos clínicos, antecedentes de vacunación, síntomas, complicaciones, tratamiento antibiótico y resultados de laboratorio del paciente notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA800';
