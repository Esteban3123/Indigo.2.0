CREATE TABLE [dbo].[HCONCOTOPO] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]     INT           NULL,
    [CODTOPOGRA]  VARCHAR (15)  NOT NULL,
    [DESCRIPCION] VARCHAR (100) NULL,
    CONSTRAINT [PK_HCONCOTOPOG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCONCOTOPOG_HCONCOTOPOG] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[HCONCOTOPO] ([ID])
);


GO
ALTER TABLE [dbo].[HCONCOTOPO] NOCHECK CONSTRAINT [FK_HCONCOTOPOG_HCONCOTOPOG];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion topografico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del registro topografico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'CODTOPOGRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con el campo ID de la presente tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'ID';

