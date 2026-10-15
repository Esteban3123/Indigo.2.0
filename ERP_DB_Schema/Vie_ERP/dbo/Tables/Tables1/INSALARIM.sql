CREATE TABLE [dbo].[INSALARIM] (
    [ISALCODIG] CHAR (3)        NOT NULL,
    [ISALNOMBR] CHAR (50)       NOT NULL,
    [ISALVALOR] NUMERIC (18, 2) NOT NULL,
    [INDAUDFOR] NUMERIC (18)    NOT NULL,
    CONSTRAINT [PK_INSalamin] PRIMARY KEY CLUSTERED ([ISALCODIG] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Salario Mininmo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALVALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALNOMBR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Salario Minimo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALCODIG';

