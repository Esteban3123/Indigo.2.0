CREATE TABLE [dbo].[INDIAGNOS] (
    [CODDIAGNO]        CHAR (4)     NOT NULL,
    [NOMDIAGNO]        CHAR (350)   NULL,
    [UNIEDADMI]        INT          NOT NULL,
    [EDADMINIM]        NUMERIC (3)  NOT NULL,
    [UNIEDADMA]        INT          NOT NULL,
    [EDADMAXIM]        NUMERIC (3)  NOT NULL,
    [APLICAMAS]        INT          NOT NULL,
    [APLICAFEM]        INT          NOT NULL,
    [EXIGENOTI]        BIT          NOT NULL,
    [CODICIE10]        CHAR (4)     NULL,
    [INDAUDFOR]        NUMERIC (18) NOT NULL,
    [EGREDIAGNO]       BIT          NOT NULL,
    [DIAGNOANT]        BIT          NULL,
    [ESTADO]           BIT          NULL,
    [DIASINCAMAX]      NUMERIC (3)  NULL,
    [DiagnosticType]   INT          DEFAULT ('1') NOT NULL,
    [DiagnosticFather] VARCHAR (20) NULL,
    CONSTRAINT [PK_INDIAGNOS] PRIMARY KEY CLUSTERED ([CODDIAGNO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INDIAGNOS_CODDIAGNO_NOMDIAGNO]
    ON [dbo].[INDIAGNOS]([CODDIAGNO] ASC, [NOMDIAGNO] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INDIAGNOS_8_1803153469__K1_2]
    ON [dbo].[INDIAGNOS]([CODDIAGNO] ASC)
    INCLUDE([NOMDIAGNO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de incapacidad maxima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DIASINCAMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diagnostico antecedente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DIAGNOANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No permitir egreso como diagnostico principal 1: No lo permite , 0: Si lo permite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EGREDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CIE10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODICIE10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige Notificacion Obligatoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EXIGENOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica al Sexo Femenino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'APLICAFEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica al Sexo Masculino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'APLICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Minima en la que aplica el Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Minima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Minima en la que aplica el Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EDADMINIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Minima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'NOMDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';

