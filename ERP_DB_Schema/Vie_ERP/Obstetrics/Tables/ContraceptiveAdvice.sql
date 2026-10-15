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
ALTER TABLE [Obstetrics].[ContraceptiveAdvice] NOCHECK CONSTRAINT [FK_ContraceptiveAdvice_PerinatalMaternalAssessmentC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método anticonceptivo elegido
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anticonceptivo para el post evento obstétrico        1 = Sí,   0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'PostObstetricEventContraceptive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asesoría anticonceptiva post evento obstétrico       1 = Sí,    0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice', @level2type = N'COLUMN', @level2name = N'ContraceptiveAdvice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena los datos de asesoría anticonceptiva post evento obstétrico de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Asesoría anticonceptiva post evento obstétrico de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ContraceptiveAdvice';

