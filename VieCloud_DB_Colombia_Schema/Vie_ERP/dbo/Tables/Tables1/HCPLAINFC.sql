CREATE TABLE [dbo].[HCPLAINFC] (
    [CODPLAINF] NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DESPLAINF] CHAR (200)   NOT NULL,
    [CODCENATE] CHAR (10)    NOT NULL,
    [UFUCODIGO] CHAR (10)    NOT NULL,
    [CODPRODIL] CHAR (20)    NOT NULL,
    [CANELESOL] INT          NOT NULL,
    [CODUNIDIL] VARCHAR (20) NOT NULL,
    [UNIINFMEZ] CHAR (10)    NOT NULL,
    CONSTRAINT [PK_HCPLAINFC] PRIMARY KEY CLUSTERED ([CODPLAINF] ASC),
    CONSTRAINT [FK_HCPLAINFC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPLAINFC_IHLISTPRO] FOREIGN KEY ([CODPRODIL]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCPLAINFC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPLAINFC_INUNIMEDI] FOREIGN KEY ([CODUNIDIL]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de infusión de la mezcla (CHAR 10); especifica la velocidad o concentración de administración: mgr/kg/min, mEq/min, cc/min, UI/min o ml/hr.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'UNIINFMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de la Infusion de la Mezcla:  mgr/Kg/min  mEq/min  cc/min  UI/min', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'UNIINFMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'UNIINFMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida del diluyente (VARCHAR 20, FK→INUNIMEDI); identifica si se mide en ml, L, gr, mg, mEq u otra unidad estándar del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODUNIDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida del diluyente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODUNIDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODUNIDIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de solución en volumen base (INT); especifica el mililitro total o dosis target a la cual se debe llevar la preparación de la mezcla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CANELESOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de solucion a la que se debe llevar la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CANELESOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CANELESOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto diluyente (CHAR 20, FK→IHLISTPRO); referencia el medicamento, solución o sustancia química usada para diluir o preparar la mezcla infusional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODPRODIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto que se especifico como diluyente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODPRODIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODPRODIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (CHAR 10, FK→INUNIFUNC); especifica el servicio clínico (urgencias, UCI, hospitalización, ambulatorio) que usa esta plantilla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK→ADCENATEN); identifica la unidad de salud, clínica, hospital o sitio donde se aplica esta plantilla de infusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (CHAR 200) de la plantilla de infusiones; detalla nombre, propósito clínico y composición de la mezcla farmacológica o nutriente a infundir.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'DESPLAINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la plantilla de infusiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'DESPLAINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'DESPLAINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único (NUMERIC) de la plantilla de infusiones; clave primaria que genera automáticamente cada registro de configuración de mezcla intravenosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODPLAINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la plantilla de infusiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODPLAINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC', @level2type = N'COLUMN', @level2name = N'CODPLAINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Planes de infusión configurados en historia clínica: define los productos diluidos, cantidades, unidades y mezclas utilizadas en protocolos de infusión para cada unidad funcional y centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFC';
