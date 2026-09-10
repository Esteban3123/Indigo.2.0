CREATE TABLE [Obstetrics].[ContraceptiveAdvice] (
    [Id]                              INT            IDENTITY (1, 1) NOT NULL,
    [IdPerinatalMaternalAssessmentC]  INT            NOT NULL,
    [ContraceptiveAdvice]             BIT            NOT NULL,
    [PostObstetricEventContraceptive] BIT            NULL,
    [ContraceptiveMethod]             VARCHAR (2)    NULL,
    [Observations]                    VARCHAR (1000) NULL,
    CONSTRAINT [PK_ContraceptiveAdvice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContraceptiveAdvice_PerinatalMaternalAssessmentC] FOREIGN KEY ([IdPerinatalMaternalAssessmentC]) REFERENCES [Obstetrics].[PerinatalMaternalAssessmentC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas, comentarios adicionales o justificaciones sobre la asesoría anticonceptiva, contraindicaciones observadas o recomendaciones especiales post evento obstétrico. Texto libre hasta 1000 caracteres.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método anticonceptivo elegido por la madre post evento obstétrico. Códigos: 1=Anillo vaginal, 2=Coito interrumpido, 3=Condón, 4=Diafragma, 5=DIU, 6=Espermicidas, 7=Esterilización quirúrgica, 8=Implante subdérmico, 9=Inyectables, 10=Anticonceptivo oral, 11=Ritmo/Ogino, 12=Parches. VARCHAR(2), nullable.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'ContraceptiveMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Método anticonceptivo elegido
1 = Anillo vaginal,  
2 = Coito interrumpido,
3 = Condon,
4 = Diafragma,
5 = DIU,
6 = Espermicidas,
7 = Esterilización QX,
8 = Implante subdermico,
9 = Inyectables,
10 = Anticonceptivo oral,
11 = Ritmo,
12 = Parches', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'ContraceptiveMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'ContraceptiveMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿Se prescribió método anticonceptivo en el período posparto inmediato? 1=Sí (prescrito), 0=No (no indicado). Nullable, permite registrar casos sin decisión contraceptiva.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'PostObstetricEventContraceptive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anticonceptivo para el post evento obstétrico        1 = Sí,   0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'PostObstetricEventContraceptive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'PostObstetricEventContraceptive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿Se brindó asesoría/consejería anticonceptiva a la madre en el post evento obstétrico? 1=Sí (educación impartida), 0=No (no realizada). Requerido, vinculado a evaluación perinatal materna.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'ContraceptiveAdvice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Asesoría anticonceptiva post evento obstétrico       1 = Sí,    0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'ContraceptiveAdvice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'ContraceptiveAdvice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de asesoría anticonceptiva y métodos contraceptivos post evento obstétrico (parto, aborto, cesárea). Almacena decisiones de planificación familiar y evaluación de paraclínicos maternos en período posparto.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena los datos de asesoría anticonceptiva post evento obstétrico de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Asesoría anticonceptiva post evento obstétrico de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de consejería anticonceptiva.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la valoración materna perinatal a la que pertenece este consejo anticonceptivo.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
