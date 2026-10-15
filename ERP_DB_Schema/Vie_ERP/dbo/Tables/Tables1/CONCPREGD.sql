CREATE TABLE [dbo].[CONCPREGD] (
    [IDCONCRES] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONCPRE] TINYINT       NOT NULL,
    [CONCRESPT] VARCHAR (250) NOT NULL,
    [CONESTADO] BIT           NOT NULL,
    [CONCOMENT] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_CONCPREGD] PRIMARY KEY CLUSTERED ([IDCONCRES] ASC),
    CONSTRAINT [FK_CONCPREGD_CONCPREGC] FOREIGN KEY ([IDCONCPRE]) REFERENCES [dbo].[CONCPREGC] ([IDCONCPRE])
);


GO
ALTER TABLE [dbo].[CONCPREGD] NOCHECK CONSTRAINT [FK_CONCPREGD_CONCPREGC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'comentario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONCOMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Texto de la respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONCRESPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación Pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico Respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'IDCONCRES';

