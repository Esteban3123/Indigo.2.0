CREATE TABLE [dbo].[INPRODPAT] (
    [IPRCODIGO]  CHAR (20) NOT NULL,
    [CODDIAGNO]  CHAR (4)  NOT NULL,
    [MinimumAge] TINYINT   NULL,
    [MaximumAge] TINYINT   NULL,
    [AgeMeasure] TINYINT   NULL,
    [Id]         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_INPRODPAT] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_INPRODPAT_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[INPRODPAT] NOCHECK CONSTRAINT [FK_INPRODPAT_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la edad medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'AgeMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la edad maxima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la edad minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Diagnostico relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto de patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';

