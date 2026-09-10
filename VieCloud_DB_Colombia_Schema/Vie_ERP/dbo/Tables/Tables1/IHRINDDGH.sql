CREATE TABLE [dbo].[IHRINDDGH] (
    [IPRCODIGO] CHAR (20) NOT NULL,
    [CODPRODUC] CHAR (20) NOT NULL,
    [PROCONPRE] BIT       NOT NULL,
    CONSTRAINT [PK_IHRINDDGH] PRIMARY KEY CLUSTERED ([IPRCODIGO] ASC, [CODPRODUC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (bit) que señala si el producto está sujeto a control de precios; controla regulación de tarifas y márgenes comerciales. Tipo: BIT (0=sin control, 1=con control).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'PROCONPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el Producto Maneja Control de Precios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'PROCONPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'PROCONPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del producto en el sistema Indigo Vie Cloud; identificador maestro para medicamentos, dispositivos o insumos. Tipo: CHAR(20), clave primaria compuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Producto en Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUM (Código Único de Medicamento) asignado por Dirección General de Hipratecarios (DGH); vincula producto Indigo con registro regulatorio nacional. Tipo: CHAR(20), clave primaria compuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUM en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre indicadores de salud (o ítems de historia clínica) y productos o servicios, indicando si el producto/servicio tiene precio de contrato o precontrato asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH';
