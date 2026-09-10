CREATE TABLE [dbo].[HCPLAINFD] (
    [CODPLAINF] NUMERIC (18)    NOT NULL,
    [CODPROMED] CHAR (20)       NOT NULL,
    [CANELEMED] NUMERIC (18, 2) NOT NULL,
    [CODUNIMED] VARCHAR (20)    NOT NULL,
    CONSTRAINT [PK_HCPLAINFD] PRIMARY KEY CLUSTERED ([CODPLAINF] ASC, [CODPROMED] ASC),
    CONSTRAINT [FK_HCPLAINFD_HCPLAINFC] FOREIGN KEY ([CODPLAINF]) REFERENCES [dbo].[HCPLAINFC] ([CODPLAINF]),
    CONSTRAINT [FK_HCPLAINFD_INUNIMEDI] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida del medicamento en volumen (mL, L, etc.); FK a INUNIMEDI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida del medicamento estipulada en volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de medicamento a mezclar en volumen (NUMERIC 18,2); dosis estipulada para infusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CANELEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de producto a mezclar se almacena la cantidad estipulada en volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CANELEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CANELEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/medicamento integrante de la mezcla infusional; componente farmacológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODPROMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto que hace parte de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODPROMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODPROMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de plantilla de infusiones; identificador único de mezcla medicamentosa (FK a HCPLAINFC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODPLAINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la plantilla de infusiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODPLAINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODPLAINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de elementos o medicamentos incluidos en un plan de informes o plan de atención clínica. Registra cada ítem con su código de producto/medicamento, la cantidad indicada y la unidad de medida correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD';
