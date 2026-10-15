CREATE TABLE [dbo].[INPLANVAC] (
    [IDPLAVACU]    CHAR (4)     NOT NULL,
    [EDAREGVAC]    CHAR (80)    NOT NULL,
    [TIPVACUNA]    CHAR (120)   NOT NULL,
    [NUMDOSISV]    CHAR (60)    NOT NULL,
    [ESTPLAVAC]    CHAR (1)     NOT NULL,
    [INDAUDFOR]    NUMERIC (18) NOT NULL,
    [NUMPOSGRU]    SMALLINT     NOT NULL,
    [ESTRUCNUEVA]  BIT          CONSTRAINT [DF_INPLANVAC_ESTRUCNUEVA] DEFAULT ((0)) NOT NULL,
    [ESTADOVACUNA] BIT          NULL,
    CONSTRAINT [PK_INPLANVAC] PRIMARY KEY CLUSTERED ([IDPLAVACU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda estado de vacuna  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTADOVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CAmpo Auditoria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Plan de Vacunacion  A: Anulado  C: Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTPLAVAC';

