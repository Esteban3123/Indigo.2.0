CREATE TABLE [dbo].[HCONCOMORFO] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]     INT           NULL,
    [CODMORFOLO]  VARCHAR (10)  NOT NULL,
    [DESCRIPCION] VARCHAR (100) NULL,
    CONSTRAINT [PK_HCONCOMORFO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCONCOMORFO_HCONCOMORFO] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[HCONCOMORFO] ([ID])
);


GO
ALTER TABLE [dbo].[HCONCOMORFO] NOCHECK CONSTRAINT [FK_HCONCOMORFO_HCONCOMORFO];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCONCOMORFO]
    ON [dbo].[HCONCOMORFO]([CODMORFOLO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion morfologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo registro morfologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'CODMORFOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion con la presente tabla - estructura jerarquíca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'ID';

