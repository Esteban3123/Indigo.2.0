CREATE TABLE [dbo].[INDIAFEST] (
    [DIAFESTIV] DATETIME     NOT NULL,
    [DIADESCRI] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INDIAFES] PRIMARY KEY CLUSTERED ([DIAFESTIV] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Dia Festivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'DIADESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Dia Festivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'DIAFESTIV';

