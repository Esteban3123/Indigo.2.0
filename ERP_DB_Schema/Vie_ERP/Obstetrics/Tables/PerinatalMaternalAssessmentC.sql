CREATE TABLE [Obstetrics].[PerinatalMaternalAssessmentC] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA] INT          NOT NULL,
    [IPCODPACI]   VARCHAR (25) NOT NULL,
    [NUMINGRES]   CHAR (10)    NOT NULL,
    [CODCENATE]   CHAR (10)    NOT NULL,
    [UFUCODIGO]   CHAR (10)    NOT NULL,
    [CODPROSAL]   CHAR (20)    NOT NULL,
    [NUMEFOLIO]   CHAR (10)    NOT NULL,
    [FECREGISTRO] DATETIME     NOT NULL,
    CONSTRAINT [PK_PerinatalMaternalAssessmentC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PerinatalMaternalAssessmentC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PerinatalMaternalAssessmentC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PerinatalMaternalAssessmentC_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_PerinatalMaternalAssessmentC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_PerinatalMaternalAssessmentC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_PerinatalMaternalAssessmentC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [Obstetrics].[PerinatalMaternalAssessmentC] NOCHECK CONSTRAINT [FK_PerinatalMaternalAssessmentC_ADCENATEN];


GO
ALTER TABLE [Obstetrics].[PerinatalMaternalAssessmentC] NOCHECK CONSTRAINT [FK_PerinatalMaternalAssessmentC_ADINGRESO];


GO
ALTER TABLE [Obstetrics].[PerinatalMaternalAssessmentC] NOCHECK CONSTRAINT [FK_PerinatalMaternalAssessmentC_HCHISPACA];


GO
ALTER TABLE [Obstetrics].[PerinatalMaternalAssessmentC] NOCHECK CONSTRAINT [FK_PerinatalMaternalAssessmentC_INPACIENT];


GO
ALTER TABLE [Obstetrics].[PerinatalMaternalAssessmentC] NOCHECK CONSTRAINT [FK_PerinatalMaternalAssessmentC_INPROFSAL];


GO
ALTER TABLE [Obstetrics].[PerinatalMaternalAssessmentC] NOCHECK CONSTRAINT [FK_PerinatalMaternalAssessmentC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la cabecera del control de usuario "valoración materno perinatal"', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessmentC';

