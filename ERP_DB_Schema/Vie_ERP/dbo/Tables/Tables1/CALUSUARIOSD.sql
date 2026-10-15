CREATE TABLE [dbo].[CALUSUARIOSD] (
    [ID]                    INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRELACALCOMITESAREAS] INT       NOT NULL,
    [CODIGOUSUARIO]         CHAR (20) NOT NULL,
    [LIDER]                 BIT       NOT NULL,
    CONSTRAINT [PK_CALUSUARIOSD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALUSUARIOSD_CALCOMITESAREAS] FOREIGN KEY ([IDRELACALCOMITESAREAS]) REFERENCES [dbo].[CALCOMITESAREAS] ([ID])
);


GO
ALTER TABLE [dbo].[CALUSUARIOSD] NOCHECK CONSTRAINT [FK_CALUSUARIOSD_CALCOMITESAREAS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El registo es True Es seleccionado como LIder ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'LIDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'CODIGOUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cual relaciona con ID de la tabla CALCOMITESAREAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'IDRELACALCOMITESAREAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'ID';

