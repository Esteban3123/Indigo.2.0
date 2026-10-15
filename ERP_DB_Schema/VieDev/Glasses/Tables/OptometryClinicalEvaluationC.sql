CREATE TABLE [Glasses].[OptometryClinicalEvaluationC] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA] INT          NOT NULL,
    [IPCODPACI]   VARCHAR (25) NOT NULL,
    [NUMINGRES]   CHAR (10)    NOT NULL,
    [CODCENATE]   CHAR (10)    NOT NULL,
    [UFUCODIGO]   CHAR (10)    NOT NULL,
    [CODPROSAL]   CHAR (10)    NOT NULL,
    [NUMEFOLIO]   CHAR (10)    NOT NULL,
    [OrderDate]   DATETIME     NOT NULL,
    CONSTRAINT [PK_ClinicalEvaluationOptometry_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro de la valoración', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationC', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional de la salud de la valoración', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional donde se hizo la valoración', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro atencion donde se hizo la valoración', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de identificacion del paciente (es el antiguo, pero funcional)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con el tablero de folios HCHISPACA', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';

