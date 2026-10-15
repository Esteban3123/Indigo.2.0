CREATE TABLE [dbo].[HCJUNOPOD] (
    [CODTIPPLAN] CHAR (5)   NOT NULL,
    [CODPRODUC]  CHAR (20)  NOT NULL,
    [CODPRODUH]  CHAR (20)  NOT NULL,
    [DIASTRATA]  INT        NOT NULL,
    [MEDNOMEJO]  BIT        NOT NULL,
    [MEDREADVE]  BIT        NOT NULL,
    [MEDINTOLE]  BIT        NOT NULL,
    [MEDNOINDI]  BIT        NOT NULL,
    [MEDOBSERV]  CHAR (200) NULL,
    CONSTRAINT [PK_HCJUNOPOD_1] PRIMARY KEY CLUSTERED ([CODTIPPLAN] ASC, [CODPRODUC] ASC, [CODPRODUH] ASC),
    CONSTRAINT [FK_HCJUNOPOD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCJUNOPOD_IHLISTPRO1] FOREIGN KEY ([CODPRODUH]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCJUNOPOD] NOCHECK CONSTRAINT [FK_HCJUNOPOD_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCJUNOPOD] NOCHECK CONSTRAINT [FK_HCJUNOPOD_IHLISTPRO1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento No Indicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDNOINDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Intolerancia al Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDINTOLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento Produjo Reaccion Adversa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDREADVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Con el Medicamento No Mejora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDNOMEJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'DIASTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Medicamento NO POS Homologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODPRODUH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Medicamento NO POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de Plantillas Cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODTIPPLAN';

