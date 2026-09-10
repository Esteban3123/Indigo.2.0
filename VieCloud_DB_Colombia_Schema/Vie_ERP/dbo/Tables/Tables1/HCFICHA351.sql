CREATE TABLE [dbo].[HCFICHA351] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [COMPLESERVI]         INT           NULL,
    [SERVIADMIN]          INT           NULL,
    [PROCEDMEDI]          INT           NULL,
    [TIPOPARTO]           BIT           NULL,
    [TIEMPODURA]          DATETIME      NULL,
    [TIEMPORUPT]          DATETIME      NULL,
    [FECHAPROCE]          DATE          NULL,
    [CODNOM]              VARCHAR (MAX) NULL,
    [PACIREQUI]           BIT           NULL,
    [DIABETES]            BIT           NULL,
    [INMUNOSUPRE]         BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [DESNUTRICION]        BIT           NULL,
    [PREECLAMPSIA]        BIT           NULL,
    [ANEMIA]              BIT           NULL,
    [CLASIFASA]           INT           NULL,
    [TIPOHERIDA]          INT           NULL,
    [DURAPROCE]           DATETIME      NULL,
    [SUPERPRIMA]          BIT           NULL,
    [SUPERSECUN]          BIT           NULL,
    [PROFUPRIMA]          BIT           NULL,
    [PROFUSECUN]          BIT           NULL,
    [ORGAESPAC]           BIT           NULL,
    [PROFILAXIS]          BIT           NULL,
    [TIEMPOADMIN]         INT           NULL,
    [LAPAROSCOPIA]        BIT           NULL,
    [FECHATOMA1]          DATE          NULL,
    [FECHATOMA2]          DATE          NULL,
    [FECHATOMA3]          DATE          NULL,
    [CODMUESTRA1]         INT           NULL,
    [CODMUESTRA2]         INT           NULL,
    [CODMUESTRA3]         INT           NULL,
    [CODPRUEBA1]          INT           NULL,
    [CODPRUEBA2]          INT           NULL,
    [CODPRUEBA3]          INT           NULL,
    [MICROORGA1]          VARCHAR (50)  NULL,
    [MICROORGA2]          VARCHAR (50)  NULL,
    [MICROORGA3]          VARCHAR (50)  NULL,
    [CUAL]                VARCHAR (50)  NULL,
    CONSTRAINT [PK_HCFICHA351] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA351_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación adicional o aclaración del procedimiento, diagnóstico o hallazgo clínico (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en tercera muestra de laboratorio, cultivo o análisis microbiológico (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'MICROORGA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'MICROORGA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'MICROORGA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en segunda muestra de laboratorio, cultivo o análisis microbiológico (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'MICROORGA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'MICROORGA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'MICROORGA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en primera muestra de laboratorio, cultivo o análisis microbiológico (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'MICROORGA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'MICROORGA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'MICROORGA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de prueba diagnóstica tercera muestra: 1=Cultivo, 2=Hemocultivo, 3=Biopsia, 4=Radiografía, 5=TAC, 6=RNM (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la prueba:   1= Cultivo   2= Hemocultivo   3= Biopsia   4= Radiografía   5= Tomografía axial computarizada   6= Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de prueba diagnóstica segunda muestra: 1=Cultivo, 2=Hemocultivo, 3=Biopsia, 4=Radiografía, 5=TAC, 6=RNM (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la prueba:   1= Cultivo   2= Hemocultivo   3= Biopsia   4= Radiografía   5= Tomografía axial computarizada   6= Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de prueba diagnóstica primera muestra: 1=Cultivo, 2=Hemocultivo, 3=Biopsia, 4=Radiografía, 5=TAC, 6=RNM (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la prueba:   1= Cultivo   2= Hemocultivo   3= Biopsia   4= Radiografía   5= Tomografía axial computarizada   6= Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tipo muestra tercera: 1=Sangre, 2=Tejido, 3=Líquidos estériles, 4=Secreciones (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la muestra:   1=Sangre   2=Tejido   3=Otros líquidos estériles   4=Secreciones ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tipo muestra segunda: 1=Sangre, 2=Tejido, 3=Líquidos estériles, 4=Secreciones (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la muestra:   1=Sangre   2=Tejido   3=Otros líquidos estériles   4=Secreciones ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tipo muestra primera: 1=Sangre, 2=Tejido, 3=Líquidos estériles, 4=Secreciones (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la muestra:   1=Sangre   2=Tejido   3=Otros líquidos estériles   4=Secreciones ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la tercera muestra para laboratorio o cultivo (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la segunda muestra para laboratorio o cultivo (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la primera muestra para laboratorio o cultivo (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento laparoscópico realizado: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'LAPAROSCOPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laparoscopia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'LAPAROSCOPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'LAPAROSCOPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento de administración antibiótica: 1=Antes, 2=Durante, 3=Después, 4=Ninguna (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIEMPOADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo en que se le administró el antibiótico:   1=Antes   2=Durante   3=Después   4=Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIEMPOADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIEMPOADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profilaxis antibiótica relacionada con procedimiento quirúrgico: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROFILAXIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profilaxis antibiótica relacionada con procedimiento:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROFILAXIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROFILAXIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órgano específico afectado o incluido en procedimiento: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'ORGAESPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orgaespac:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'ORGAESPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'ORGAESPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profusión secundaria o sangrado secundario: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROFUSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profusecun:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROFUSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROFUSECUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profusión primaria o sangrado primario: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROFUPRIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profuprima:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROFUPRIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROFUPRIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sobreinfección secundaria: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'SUPERSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Supersecun:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'SUPERSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'SUPERSECUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sobreinfección primaria: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'SUPERPRIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Superprima:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'SUPERPRIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'SUPERPRIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración del procedimiento quirúrgico o parto en minutos (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'DURAPROCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración del Procedimiento (minutos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'DURAPROCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'DURAPROCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación herida quirúrgica: 1=Limpia, 2=Limpia contaminada, 3=Contaminada, 4=Sucia (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Herida:   1= Limpia   2= Limpia contaminada   3= Herida contaminada   4= Herida sucia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación ASA riesgo anestésico: 1=ASA 1, 2=ASA 2, 3=ASA 3, 4=ASA 4, 5=ASA 5 (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CLASIFASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación ASA:   1=ASA 1   2=ASA 2   3=ASA 3   4=ASA 4   5=ASA 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CLASIFASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CLASIFASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de anemia en paciente: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'ANEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anemia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'ANEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'ANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de preeclampsia en embarazo: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Preclampsia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de desnutrición en paciente: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desnutricion:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de obesidad en paciente: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obesidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de inmunosupresión en paciente: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmunosupre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de diabetes en paciente: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diabetes:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paciente requirió reintervención o cirugía complementaria: True=Sí, False=No (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PACIREQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente requirió reintervención:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PACIREQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PACIREQUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código de la IPS donde se atendió parto, cesárea o procedimiento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODNOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Nombre de la IPS donde se antendio el parto ó césarea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODNOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODNOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del procedimiento quirúrgico, parto o cesárea realizado (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHAPROCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del procedimiento quirúrgico o parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHAPROCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'FECHAPROCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de ruptura de membranas en el parto en minutos (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIEMPORUPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de ruptura de membranas (minutos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIEMPORUPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIEMPORUPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de duración del trabajo de parto en minutos (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIEMPODURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de duración del trabajo de parto (minutos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIEMPODURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIEMPODURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de parto: True=Vaginal, False=Cesárea (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de parto:   True= Vaginal   False= Cesárea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento quirúrgico realizado: 1=Cesárea, 2=Herniorrafía, 3=Parto, 4=Revascularización miocárdica, 5=Colecistectomía (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROCEDMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seleccione el procedimiento médico quirúrgico realizado:   1= Cesárea   2= Herniorrafía   3= Parto   4= Revascularización miocárdica con incisión torácica y del sitio donante   5= Colecistectomía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROCEDMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'PROCEDMEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio de admisión: 1=Programado ambulatorio, 2=Urgencias, 3=Programado hospitalizado (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'SERVIADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio de admisión del procedimiento Qx o parto:   1= Programado ambulatorio   2= Urgencias   3= Programado hospitalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'SERVIADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'SERVIADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio complementario quirúrgico o de parto asociado (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'COMPLESERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio complementario quirúrgico o parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'COMPLESERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'COMPLESERVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 (CHAR 4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de ficha de notificación, relacionado con HCFICHANOTIFICACION (INT, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único de registro en HCFICHA351 (INT, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación de infecciones asociadas a la atención en salud (IAAS), específicamente para el registro de infecciones del sitio quirúrgico (ISQ). Contiene datos del procedimiento quirúrgico, factores de riesgo del paciente, clasificación de la herida, profilaxis antibiótica, y resultados microbiológicos de muestras tomadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA351';
