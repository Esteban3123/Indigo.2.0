CREATE TABLE [dbo].[RSGRUPOACT] (
    [ID]        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    [IDHCGRUPO] INT      NOT NULL,
    CONSTRAINT [PK_HCGRUPOACT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSGRUPOACT_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_RSGRUPOACT_RSGRUPO] FOREIGN KEY ([IDHCGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID])
);


GO
ALTER TABLE [dbo].[RSGRUPOACT] NOCHECK CONSTRAINT [FK_RSGRUPOACT_AGACTIMED];


GO
ALTER TABLE [dbo].[RSGRUPOACT] NOCHECK CONSTRAINT [FK_RSGRUPOACT_RSGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del Grupo de Revisión por sistema de la HC dinámica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'IDHCGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'ID';

