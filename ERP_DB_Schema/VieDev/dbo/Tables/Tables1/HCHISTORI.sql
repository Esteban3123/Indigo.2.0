CREATE TABLE [dbo].[HCHISTORI] (
    [CODTIPHIS] CHAR (3)     NOT NULL,
    [DESTIPHIS] CHAR (60)    NOT NULL,
    [IDETIPHIS] CHAR (9)     NOT NULL,
    [FORTIPHIS] CHAR (1)     NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_HCTIPOHIS] PRIMARY KEY CLUSTERED ([CODTIPHIS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formato Tipo de Historia Clinica  I: Ingreso  E: Evolucion  O: Otros formatos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISTORI', @level2type = N'COLUMN', @level2name = N'FORTIPHIS';

