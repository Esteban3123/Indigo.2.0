CREATE TABLE [dbo].[HCFICHA345] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [PRESECARNE]          BIT           NULL,
    [NEUMOCOCO]           INT           NULL,
    [INFLUENZAESTACIO]    INT           NULL,
    [DOSIS1]              VARCHAR (50)  NULL,
    [DOSIS2]              VARCHAR (50)  NULL,
    [FECHAULTDOS1]        DATE          NULL,
    [FECHAULTDOS2]        DATE          NULL,
    [ASMA]                BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [TOS]                 BIT           NULL,
    [EPOC]                BIT           NULL,
    [INSUFIRENAL]         BIT           NULL,
    [FIEBRE]              BIT           NULL,
    [DIABETES]            BIT           NULL,
    [TOMAMEDICA]          BIT           NULL,
    [DOLORGARGANTA]       BIT           NULL,
    [VIH]                 BIT           NULL,
    [FUMADOR]             BIT           NULL,
    [RINORREA]            BIT           NULL,
    [ENFECARDIACA]        BIT           NULL,
    [OTROS]               BIT           NULL,
    [CONJUNTIVITIS]       BIT           NULL,
    [CANCER]              BIT           NULL,
    [CEFALEA]             BIT           NULL,
    [MALNUTRICION]        BIT           NULL,
    [DIFIRESPIRA]         BIT           NULL,
    [DIARREA]             BIT           NULL,
    [CUALESOTROS]         VARCHAR (50)  NULL,
    [SEMAGEST]            VARCHAR (50)  NULL,
    [TOMRADIOTORAX]       INT           NULL,
    [FECHATOMA1]          DATE          NULL,
    [FECHATOMA2]          DATE          NULL,
    [FECHATOMA3]          DATE          NULL,
    [HALLAZGOS]           INT           NULL,
    [USOANTIBIO]          BIT           NULL,
    [FECHAINICIO1]        DATE          NULL,
    [FECHAINICIO2]        DATE          NULL,
    [USOANTIVIRAL]        BIT           NULL,
    [SERVIHOSPITAL]       BIT           NULL,
    [FECHAINGRESO]        DATE          NULL,
    [DERRAPLEURAL]        BIT           NULL,
    [DERRAPERICAR]        BIT           NULL,
    [MIOCARDITIS]         BIT           NULL,
    [SEPTICEMIA]          BIT           NULL,
    [FALLARESPIR]         BIT           NULL,
    [OTRO]                BIT           NULL,
    [OTROSCUALES]         VARCHAR (50)  NULL,
    [DIAGNOINICI]         VARCHAR (50)  NULL,
    [DIAGNOEGRESO]        VARCHAR (50)  NULL,
    [FECHARECE1]          DATE          NULL,
    [FECHARECE2]          DATE          NULL,
    [FECHARECE3]          DATE          NULL,
    [FECHARECE4]          DATE          NULL,
    [MUESTRA1]            INT           NULL,
    [MUESTRA2]            INT           NULL,
    [PRUEBA1]             INT           NULL,
    [PRUEBA2]             INT           NULL,
    [AGENTE1]             INT           NULL,
    [AGENTE2]             INT           NULL,
    [RESULTADO1]          INT           NULL,
    [RESULTADO2]          INT           NULL,
    [VALORREGIS1]         VARCHAR (50)  NULL,
    [VALORREGIS2]         VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA345] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA345_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA345_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA345] NOCHECK CONSTRAINT [CK_HCFICHA345_JSON];




GO
ALTER TABLE [dbo].[HCFICHA345] NOCHECK CONSTRAINT [CK_HCFICHA345_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento JSON de campos nuevos/ampliados de Ficha de Enfermedad Similar a Influenza: COVID-19, dosis 3 vacuna, fecha última dosis 3, nombre vacuna, hipertensión arterial. Tipo: VARCHAR(MAX), validado con ISJSON().', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guardan los columna-dato nuevos de FichaEnfermedadSimilarInfluenza (COVID19, DOSIS3, FECHAULTDOS3, NOMVACU, Hipertension)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del esquema de la ficha de notificación. Tipo: VARCHAR(20). Permite rastrear cambios en estructura de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico/texto registrado en segundo resultado de laboratorio. Tipo: VARCHAR(50). Complementa RESULTADO2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VALORREGIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VALORREGIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VALORREGIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico/texto registrado en primer resultado de laboratorio. Tipo: VARCHAR(50). Complementa RESULTADO1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VALORREGIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VALORREGIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VALORREGIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo resultado de prueba respiratoria: 1=Positivo, 2=Negativo, 3=No Procesado, 4=Inadecuado, 5=Valor Registrado, 6=Contaminado hongos, 7=Muestra células insuficientes. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado2:   1=Positivo   2=Negativo   3=No Procesado   4=Inadecuado   5=Valor Registrado   6=Contaminado con hongos   7=Muestra escasa de células', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer resultado de prueba respiratoria: 1=Positivo, 2=Negativo, 3=No Procesado, 4=Inadecuado, 5=Valor Registrado, 6=Contaminado hongos, 7=Muestra células insuficientes. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado1:   1=Positivo   2=Negativo   3=No Procesado   4=Inadecuado   5=Valor Registrado   6=Contaminado con hongos   7=Muestra escasa de células', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo agente/patógeno detectado en muestra: virus respiratorios (influenza A/B/H1N1/H3N2, coronavirus, sincitial respiratorio, parainfluenza, adenovirus, etc.), bacterias (neumococo, Haemophilus), otros. Tipo: INT, catálogo 1-25.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'AGENTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente2:  1=Otro   2=Adenovirus   3=Virus sincitial respiratorio   4=Haemophilus influenzae   5=Streptococcus pneumoniae  6=Influenza A   7=Influenza B   8=Parainfluenza 1   9=Parainfluenza 2   10=Parainfluenza 3   11=Enterovirus   12=Influenza A(H1N1) pdm09   13=Influenza A no subtipificable   14=Bocavirus   15=Coronavirus   16=Metaneumovirus   17=Rinovirus   18=Virus respiratorios   19=Coronavirus causante del síndrome respiratorio de oriente medio (MERS - CoV)   20=Coronavirus subtipo 229e   21=Coronavirus subtipo HKU1   22=Coronavirus subtipo NL63   23=Coronavirus subtipo OC43   24=Influenza A(H3N2)   25=Parainfluenza tipo 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'AGENTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'AGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer agente/patógeno detectado en muestra: virus respiratorios (influenza A/B/H1N1/H3N2, coronavirus, sincitial respiratorio, parainfluenza, adenovirus, etc.), bacterias (neumococo, Haemophilus), otros. Tipo: INT, catálogo 1-25.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'AGENTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente1:   1=Otro   2=Adenovirus   3=Virus sincitial respiratorio   4=Haemophilus influenzae   5=Streptococcus pneumoniae  6=Influenza A   7=Influenza B   8=Parainfluenza 1   9=Parainfluenza 2   10=Parainfluenza 3   11=Enterovirus   12=Influenza A(H1N1) pdm09   13=Influenza A no subtipificable   14=Bocavirus   15=Coronavirus   16=Metaneumovirus   17=Rinovirus   18=Virus respiratorios   19=Coronavirus causante del síndrome respiratorio de oriente medio (MERS - CoV)   20=Coronavirus subtipo 229e   21=Coronavirus subtipo HKU1   22=Coronavirus subtipo NL63   23=Coronavirus subtipo OC43   24=Influenza A(H3N2)   25=Parainfluenza tipo 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'AGENTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'AGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de segunda prueba diagnóstica: 1=PCR, 2=Aislamiento viral, 3=Patología, 4=Inmunohistoquímica, 5=Inhibición hemaglutinación, 6=Cultivo, 7=IFI, 8=Hemocultivo. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:   1=PCR   2=Aislamiento viral   3=Patología   4=Inmunohistoquímica   5=Inhibición hemaglutinación   6=Cultivo   7=IFI   8=Hemocultivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de primera prueba diagnóstica: 1=PCR, 2=Aislamiento viral, 3=Patología, 4=Inmunohistoquímica, 5=Inhibición hemaglutinación, 6=Cultivo, 7=IFI, 8=Hemocultivo. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:   1=PCR   2=Aislamiento viral   3=Patología   4=Inmunohistoquímica   5=Inhibición hemaglutinación   6=Cultivo   7=IFI   8=Hemocultivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de segunda muestra biológica recolectada: 1=Sangre total, 2=Hisopado nasofaríngeo, 3=Tejido, 4=Aspirado nasofaríngeo, 5=Otros líquidos estériles, 6=Lavado bronquial. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra:   1=Sangre total   2=Hisopado nasofaríngeo   3=Tejido   4=Aspirado nasofaríngeo   5=Otros líquidos esteriles   6=Lavado bronquial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de primera muestra biológica recolectada: 1=Sangre total, 2=Hisopado nasofaríngeo, 3=Tejido, 4=Aspirado nasofaríngeo, 5=Otros líquidos estériles, 6=Lavado bronquial. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra:   1=Sangre total   2=Hisopado nasofaríngeo   3=Tejido   4=Aspirado nasofaríngeo   5=Otros líquidos esteriles   6=Lavado bronquial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta fecha de recepción de muestra en laboratorio. Tipo: DATE. Permite rastrear recolecciones múltiples.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera fecha de recepción de muestra en laboratorio. Tipo: DATE. Permite rastrear recolecciones múltiples.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda fecha de recepción de muestra en laboratorio. Tipo: DATE. Permite rastrear recolecciones múltiples.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera fecha de recepción de muestra en laboratorio. Tipo: DATE. Inicia cadena de diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHARECE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico principal al momento del egreso hospitalario, codificado en CIE-10. Tipo: VARCHAR(50). PII potencial según datos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIAGNOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnóstico de egreso CIE - 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIAGNOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIAGNOEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico inicial/presuntivo al ingreso, codificado en CIE-10. Tipo: VARCHAR(50). PII potencial según datos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIAGNOINICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnóstico inicial CIE - 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIAGNOINICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIAGNOINICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de otras comorbilidades/condiciones no enumeradas. Tipo: VARCHAR(50). Complementa campo OTROS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTROSCUALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros cuales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTROSCUALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTROSCUALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica presencia de otras comorbilidades o condiciones clínicas adicionales: True=Sí, False=No. Tipo: BIT. Bandera indicadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica complicación de insuficiencia/falla respiratoria aguda: True=Sí, False=No. Tipo: BIT. Complicación grave de infección respiratoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FALLARESPIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Falla respiratoria:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FALLARESPIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FALLARESPIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica presencia de infección sistémica/sepsis: True=Sí, False=No. Tipo: BIT. Complicación potencialmente mortal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SEPTICEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Septicemia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SEPTICEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SEPTICEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica inflamación del miocardio (músculo cardíaco): True=Sí, False=No. Tipo: BIT. Complicación cardiovascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MIOCARDITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Miocarditis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MIOCARDITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MIOCARDITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica derrame pericárdico (acumulación de líquido alrededor del corazón): True=Sí, False=No. Tipo: BIT. Complicación cardíaca.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DERRAPERICAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Derrapericar:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DERRAPERICAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DERRAPERICAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica derrame pleural (acumulación de líquido alrededor de pulmones): True=Sí, False=No. Tipo: BIT. Complicación respiratoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DERRAPLEURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'DERRAPLEURAL:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DERRAPLEURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DERRAPLEURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a hospitalización/servicio de atención. Tipo: DATE. Marca inicio de episodio intrahospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio hospitalario de ingreso: True=Hospitalización general, False=Unidad de Cuidados Intensivos (UCI). Tipo: BIT. Indica gravedad relativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SERVIHOSPITAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio en el que se hospitalizó:   Hospitalización general=Si   UCI=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SERVIHOSPITAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SERVIHOSPITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica administración de medicamentos antivirales (oseltamivir, zanamivir, etc.): True=Sí, False=No. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'USOANTIVIRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usó antivirales:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'USOANTIVIRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'USOANTIVIRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de segundo ciclo de tratamiento antibiótico o antiviral. Tipo: DATE. Permite auditar cambios terapéuticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAINICIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAINICIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAINICIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de primer ciclo de tratamiento antibiótico o antiviral. Tipo: DATE. Marca comienzo de intervención farmacológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAINICIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAINICIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAINICIO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica administración de medicamentos antibióticos: True=Sí, False=No. Tipo: BIT. Infecciones bacterianas concomitantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'USOANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usó antibióticos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'USOANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'USOANTIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador de hallazgos radiológicos/de imagen (radiografía de tórax, TC, etc.). Tipo: INT. Referencia a catálogo de patrones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'HALLAZGOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hallazgos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'HALLAZGOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'HALLAZGOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera fecha de toma/recolección de muestra biológica. Tipo: DATE. Permite seguimiento seriado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda fecha de toma/recolección de muestra biológica. Tipo: DATE. Permite seguimiento seriado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera fecha de toma/recolección de muestra biológica del paciente. Tipo: DATE. Marca inicio de diagnóstico microbiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se realizó radiografía de tórax: 1=Sí, 2=No. Tipo: INT. Complementa HALLAZGOS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOMRADIOTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si se tomó radiografía de tórax ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOMRADIOTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOMRADIOTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional en semanas (solo pacientes embarazadas). Tipo: VARCHAR(50). Campo clínico pediátrico/obstétrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SEMAGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semanas de gestacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SEMAGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SEMAGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de otros síntomas o hallazgos clínicos no enumerados. Tipo: VARCHAR(50). Complementa campos binarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CUALESOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuales otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CUALESOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CUALESOTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma gastrointestinal: deposiciones frecuentes/acuosas: True=Sí, False=No. Tipo: BIT. Manifestación sistémica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diarrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma de dificultad respiratoria/disnea: True=Sí, False=No. Tipo: BIT. Síntoma clave en infección respiratoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIFIRESPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Difirespira:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIFIRESPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIFIRESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico de malnutrición/desnutrición en evaluación: True=Sí, False=No. Tipo: BIT. Comorbilidad de riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MALNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Malnutricion:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MALNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MALNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma de dolor de cabeza: True=Sí, False=No. Tipo: BIT. Manifestación sistémica frecuente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cefalea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de malignidad/enfermedad neoplásica: True=Sí, False=No. Tipo: BIT. Comorbilidad de riesgo oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cancer:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma de inflamación ocular/conjuntivitis: True=Sí, False=No. Tipo: BIT. Manifestación sistémica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CONJUNTIVITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Conjuntivitis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CONJUNTIVITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CONJUNTIVITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de otras comorbilidades o condiciones clínicas: True=Sí, False=No. Tipo: BIT. Bandera para explorar OTROSCUALES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de enfermedad cardíaca/cardiopatía: True=Sí, False=No. Tipo: BIT. Comorbilidad de riesgo cardiovascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ENFECARDIACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfecardiaca:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ENFECARDIACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ENFECARDIACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma de secreción nasal: True=Sí, False=No. Tipo: BIT. Manifestación respiratoria alta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RINORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rinorrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RINORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RINORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de tabaquismo actual: True=Sí/fumador activo, False=No. Tipo: BIT. Factor de riesgo respiratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FUMADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fumador:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FUMADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FUMADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de infección por VIH/SIDA: True=Positivo, False=Negativo. Tipo: BIT. Inmunosupresión crítica, PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vih:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma de odinofagia/dolor de garganta: True=Sí, False=No. Tipo: BIT. Síntoma respiratorio superior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOLORGARGANTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dolor garganta:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOLORGARGANTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOLORGARGANTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si paciente recibe medicamentos de forma regular: True=Sí, False=No. Tipo: BIT. Comorbilidad implícita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOMAMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tomamedica:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOMAMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOMAMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de diabetes mellitus: True=Sí, False=No. Tipo: BIT. Comorbilidad de riesgo metabólico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diabetes:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma de elevación de temperatura corporal: True=Sí, False=No. Tipo: BIT. Signo cardinal infección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de insuficiencia renal crónica/enfermedad renal: True=Sí, False=No. Tipo: BIT. Comorbilidad de riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'INSUFIRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Insufirenal:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'INSUFIRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'INSUFIRENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de Enfermedad Pulmonar Obstructiva Crónica: True=Sí, False=No. Tipo: BIT. Comorbilidad respiratoria grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'EPOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Epoc:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'EPOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'EPOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma de tos: True=Sí, False=No. Tipo: BIT. Síntoma principal infección respiratoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico de sobrepeso/obesidad (IMC≥30): True=Sí, False=No. Tipo: BIT. Comorbilidad de riesgo metabólico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obesidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de asma bronquial: True=Sí, False=No. Tipo: BIT. Comorbilidad respiratoria crónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ASMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Asma:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ASMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ASMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última dosis aplicada del segundo esquema/refuerzo vacunal. Tipo: DATE. Rastreo inmunización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha ultima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última dosis aplicada del primer esquema vacunal (influenza estacional, neumococo). Tipo: DATE. Rastreo inmunización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha ultima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador/descripción de segunda dosis de vacuna aplicada. Tipo: VARCHAR(50). Ej: ''''Pfizer lote XYZ''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador/descripción de primera dosis de vacuna aplicada. Tipo: VARCHAR(50). Ej: ''''AstraZeneca lote ABC''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de vacunación contra influenza estacional: 1=Sí vacunado, 2=No vacunado, 3=Desconocido. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'INFLUENZAESTACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Influenza estacional:  1=Si   2=No   3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'INFLUENZAESTACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'INFLUENZAESTACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de vacunación contra Streptococcus pneumoniae (neumococo): 1=Sí vacunado, 2=No vacunado, 3=Desconocido. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'NEUMOCOCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Streptococcus Pneumoniae (neumococo):  1=Si   2=No   3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'NEUMOCOCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'NEUMOCOCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si paciente presentó/portaba carné/documento de vacunación: True=Sí, False=No. Tipo: BIT. Verificación de comprobante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRESECARNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentó Carné:  True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRESECARNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRESECARNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico en nomenclatura CIE-10 u otra clasificación clínica. Tipo: CHAR(4). Ej: ''''J189'''' (neumonía no especificada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ficha de notificación de enfermedad similar a influenza (COVID-19, influenza, etc.). Tipo: INT. FK a HCFICHANOTIFICACION.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de registro. Tipo: INT IDENTITY(1,1). Clave primaria, no reutilizable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica 345 para la notificación de casos de infección respiratoria aguda grave (IRAG) y eventos relacionados. Registra antecedentes de vacunación, comorbilidades, síntomas clínicos, manejo hospitalario, muestras de laboratorio y resultados por cada notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345';
