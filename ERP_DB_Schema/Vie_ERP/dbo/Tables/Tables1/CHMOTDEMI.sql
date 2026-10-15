CREATE TABLE [dbo].[CHMOTDEMI] (
    [CODMOTDEI] CHAR (2)     NOT NULL,
    [DESMOTDEI] CHAR (250)   NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHMOTDEMI] PRIMARY KEY CLUSTERED ([CODMOTDEI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de Demanda Insatisfecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'DESMOTDEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Motivo de Demanda Insatisfecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'CODMOTDEI';

