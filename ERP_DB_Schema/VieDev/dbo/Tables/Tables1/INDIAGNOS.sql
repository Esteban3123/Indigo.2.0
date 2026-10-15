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
CREATE NONCLUSTERED INDEX [_dta_index_INDIAGNOS_8_1803153469__K1_2]
    ON [dbo].[INDIAGNOS]([CODDIAGNO] ASC)
    INCLUDE([NOMDIAGNO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Minima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Minima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';

