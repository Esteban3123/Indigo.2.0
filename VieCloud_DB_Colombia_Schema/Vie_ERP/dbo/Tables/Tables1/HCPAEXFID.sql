CREATE TABLE [dbo].[HCPAEXFID] (
    [CODCONCEC] INT       NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    CONSTRAINT [PK_HCPAEXFID] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [UFUCODIGO] ASC),
    CONSTRAINT [FK_HCPAEXFID_HCPAEXFIC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCPAEXFIC] ([AUTO]),
    CONSTRAINT [FK_HCPAEXFID_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional (centro de atención, servicio o departamento médico). Identificador que relaciona el examen con la unidad donde se realiza. Tipo: CHAR(10). Foreign Key a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código autoincremental de la cabecera del concepto de examen. Identificador único que vincula el detalle con el registro principal de examen. Tipo: INT. Foreign Key a HCPAEXFIC (tabla cabecera).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Autonumerico de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de exclusiones o filtros de identificación para la historia clínica: asocia un concepto o categoría de exclusión con una unidad funcional, permitiendo controlar qué registros o conceptos se excluyen en determinadas unidades de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID';
