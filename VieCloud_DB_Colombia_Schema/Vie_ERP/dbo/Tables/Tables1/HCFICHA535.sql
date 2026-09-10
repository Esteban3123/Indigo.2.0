CREATE TABLE [dbo].[HCFICHA535] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT          NOT NULL,
    [CODDIAGNO]           CHAR (4)     NULL,
    [TIPOAGENTE]          INT          NULL,
    [VACUANTIHIB]         INT          NULL,
    [VACUANTIMENINGO]     INT          NULL,
    [VACUANTINEUMO]       INT          NULL,
    [DOSIS1]              INT          NULL,
    [DOSIS2]              INT          NULL,
    [DOSIS3]              INT          NULL,
    [FECHAULTIDO1]        DATE         NULL,
    [FECHAULTIDO2]        DATE         NULL,
    [FECHAULTIDO3]        DATE         NULL,
    [FECHAULTIDO4]        DATE         NULL,
    [USOANTIBIO]          BIT          NULL,
    [CLASIFCASO]          INT          NULL,
    [FECHATOMA]           DATE         NULL,
    [FECHARECE]           DATE         NULL,
    [MUESTRA]             INT          NULL,
    [PRUEBA]              INT          NULL,
    [AGENTE]              INT          NULL,
    [RESULTADO]           INT          NULL,
    [FECHARESUL]          DATE         NULL,
    [VALOR]               VARCHAR (50) NULL,
    [VERSION]             VARCHAR (20) NULL,
    CONSTRAINT [PK_HCFICHA535] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA535_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico o texto del resultado de laboratorio; dato cuantitativo o cualitativo del examen (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el valor  (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado del examen de laboratorio; cuando se obtiene el resultado del cultivo, PCR o prueba diagnóstica (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha del  resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de la prueba diagnóstica: 1=Positivo, 2=Negativo; indica presencia o ausencia del agente (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda resultado  1 =Positivo    2 = Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente bacteriano aislado o identificado: 1=Otro, 2=Haemophilus influenzae, 3=Neisseria meningitidis, 4=Streptococcus pneumoniae, 5=Staphylococcus aureus, 6=Listeria monocytogenes, 7=E. coli, 8=Enterobacter cloacae, 10=Staphylococcus epidermidis, 11=Streptococcus beta hemolítico, 12=Streptococcus agalactiae, 13=Streptococcus pyogenes (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda agente  1 =Otro   2 =Haemophilus influenzae  3 = Neisseria meningitidis 4 =Streptococus pneumonie  5 =Staphylococcus aereus 6 =Listeria monocytogenes 7 =E. coli 8 =Enterobacter clocae  9 =Enterobacter clocae   10 =Staphylococcus epidermidis  11= Streptococcus beta hemiolitico 12 =Streptococcus agalactiae  13 =Streptococcus pyogenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba diagnóstica realizada: 1=Aislamiento, 2=Cultivo, 3=Coloración de Gram, 4=Antigenemia, 5=RT/PCR; método de laboratorio (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda prueba   1 =Aislamiento   2 =Cultivo  3 = Coloración de gram    4 = Antigenemia  5 = RT/PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de muestra biológica: 1=Sangre total, 2=Líquido cefalorraquídeo (LCR); origen del especimen (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda muestra 1 =sangre total    2 = LCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de la muestra en laboratorio; cuando llega el especimen al área de procesamiento (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma del examen; fecha de recolección de la muestra biológica del paciente (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Fecha toma de examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación clínica de enfermedad meningocócica: 1=Meningitis, 2=Meningitis con meningococemia, 3=Meningococemia sin meningitis (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la clasificación de caso enfermedad meningocócica    1 = Meningitis    2 = Meningitis con meningoccocemia    3 =  Meningoccocemia sin meningitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de uso de antibióticos en última semana: 1=Sí, 0=No; antecedente de tratamiento antimicrobiano previo (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'USOANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda  si uso antibioticos en la ultima semana    1 =  Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'USOANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'USOANTIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la cuarta dosis de vacuna; fecha de administración de dosis adicional (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha4  de ultima dosis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la tercera dosis de vacuna; fecha de administración de dosis tercera (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha3  de ultima dosis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la segunda dosis de vacuna; fecha de administración de dosis segunda (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha2  de ultima dosis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la primera dosis de vacuna; fecha de administración de dosis inicial (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha1  de ultima dosis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis tercera de vacuna: 1=Dosis 1, 2=Dosis 2, 3=Dosis 3; secuencia vacunal (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Dosis3   1 =1     2 =2      3 =3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis segunda de vacuna: 1=Dosis 1, 2=Dosis 2, 3=Dosis 3; secuencia vacunal (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Dosis2   1 =1     2 =2      3 =3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis primera de vacuna: 1=Dosis 1, 2=Dosis 2, 3=Dosis 3; secuencia vacunal (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Dosis1   1 =1     2 =2      3 =3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de vacunación antineumocócica: 1=Sí, 2=No, 3=Desconocido; vacuna contra Streptococcus pneumoniae (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTINEUMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda  Vacuna anti neumococo   1 = Si   2 =No   3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTINEUMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTINEUMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de vacunación antimeningocócica: 1=Sí, 2=No, 3=Desconocido; vacuna contra Neisseria meningitidis (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTIMENINGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda  Vacuna anti meningococo   1 = Si   2 =No   3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTIMENINGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTIMENINGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de vacunación anti-Hib: 1=Sí, 2=No, 3=Desconocido; vacuna contra Haemophilus influenzae tipo b (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTIHIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gurada  vacuna  anti - Hib   1  = Si  2 = No  3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTIHIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTIHIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de agente bacteriano causal: 1=Haemophilus influenzae (Hi), 2=Neisseria meningitidis (Meningococo), 3=Streptococcus pneumoniae (Neumococo), 4=Otros agentes bacterianos, 5=Agente sin determinar (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'TIPOAGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el tipo de agente bacteriano  1  =Haemophilus Influenzae (Hi)     2 =Neisseria meningitidis (Meningococo)      3 =Streptoccoccus pneumoniae (Neumococo)   4 =Otros agentes bacterianos  5 = Agente sin determinar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'TIPOAGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'TIPOAGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 o de clasificación de enfermedad meningocócica (CHAR 4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda  codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de ficha de notificación de enfermedad; referencia FK a tabla HCFICHANOTIFICACION (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo) de registro en tabla HCFICHA535; clave primaria clustered (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo  de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de la ficha 535 de notificación epidemiológica para casos de meningitis bacteriana (Sivigila). Guarda información sobre el diagnóstico, tipo de agente causante, historial de vacunación (antiHib, antimeningocócica, antineumocócica), uso de antibióticos, clasificación del caso y resultados de laboratorio de muestras tomadas al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del formulario o ficha 535 utilizada para el registro de la notificación epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VERSION';
