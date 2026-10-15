CREATE TABLE [dbo].[AGAGEMEDE] (
    [CODAUTONU] INT      NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    CONSTRAINT [PK_AGAGEMEDE] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC, [CODACTMED] ASC),
    CONSTRAINT [FK_AGAGEMEDE2_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED])
);


GO
ALTER TABLE [dbo].[AGAGEMEDE] NOCHECK CONSTRAINT [FK_AGAGEMEDE2_AGACTIMED];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Actividad medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Autonumerico de la tabla AGAGEMEDC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';

