CREATE TABLE [dbo].[IHPRODIAD] (
    [CODCONCEC]   INT           NOT NULL,
    [CODDIAGNO]   CHAR (4)      NOT NULL,
    [TIPDIAGNOS]  CHAR (1)      NOT NULL,
    [OBSERVACIO]  VARCHAR (MAX) NULL,
    [CODPRODUC]   CHAR (20)     NOT NULL,
    [CODPRODUCHC] CHAR (20)     NULL,
    CONSTRAINT [PK_IHPRODIAD] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODDIAGNO] ASC),
    CONSTRAINT [FK_IHPRODIAD_IHLISTPRO] FOREIGN KEY ([CODPRODUCHC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[IHPRODIAD] NOCHECK CONSTRAINT [FK_IHPRODIAD_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Diagnostico  1= Indicaciones  2= Contraindicaciones  3= Precauciones  4= Reacciones Adversas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'TIPDIAGNOS';

