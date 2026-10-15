CREATE TABLE [dbo].[CONCPRIAL] (
    [IDCONCURS] TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMBRCONC] VARCHAR (250) NOT NULL,
    [FECINICON] DATETIME      NOT NULL,
    [FECLIMCON] DATETIME      NOT NULL,
    [FECPRECON] DATETIME      NOT NULL,
    [OBJETCONC] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_CONCPRIAL] PRIMARY KEY CLUSTERED ([IDCONCURS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objetivos del concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'OBJETCONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Entrega Premiación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECPRECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final Del Concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECLIMCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial Del Concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECINICON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Del concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'NOMBRCONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'IDCONCURS';

