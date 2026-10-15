CREATE TABLE [dbo].[CONCPREGC] (
    [IDCONCPRE] TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONCPREGU] VARCHAR (380) NOT NULL,
    [CONTIPRES] TINYINT       NOT NULL,
    [COESPECIA] BIT           NOT NULL,
    [CONMEDICO] BIT           NOT NULL,
    [COENFERME] BIT           NOT NULL,
    [CONCAPOYO] BIT           NOT NULL,
    CONSTRAINT [PK_CONCPREGC] PRIMARY KEY CLUSTERED ([IDCONCPRE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pregunta Para Apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONCAPOYO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pregunta Para Emfermer@', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'COENFERME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pregunta Para Médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pregunta Para Especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'COESPECIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de respuesta 0-Selección Multiple, 1-Selección Única', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONTIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TextoPregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONCPREGU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';

