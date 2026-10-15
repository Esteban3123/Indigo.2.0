CREATE TABLE [Glasses].[ClinicalEvaluationOptometry] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA] INT          NULL,
    [IdPatient]   INT          NULL,
    [NUMINGRES]   CHAR (10)    NOT NULL,
    [CODCENATE]   CHAR (10)    NOT NULL,
    [UFUCODIGO]   CHAR (10)    NOT NULL,
    [CODPROSAL]   CHAR (10)    NOT NULL,
    [NUMEFOLIO]   CHAR (10)    NULL,
    [OrderDate]   DATETIME     NOT NULL,
    [IPCODPACI]   VARCHAR (25) NULL,
    CONSTRAINT [PK_ClinicalEvaluationOptometry] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de pedido', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del folio', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la tabla INPACIENT', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IdPatient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de HCHISPACA', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'ClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Id';

