CREATE TABLE [dbo].[HCCODAZUD] (
    [CODCONSEC]   NUMERIC (18)  NOT NULL,
    [CODPRODUC]   CHAR (20)     NOT NULL,
    [CANPEDPRO]   INT           NOT NULL,
    [TIPPRODUC]   CHAR (1)      NOT NULL,
    [CANENTPRO]   INT           NULL,
    [Indications] VARCHAR (200) NULL,
    CONSTRAINT [PK_HCCODAZUD] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCCODAZUD_HCCODAZUC] FOREIGN KEY ([CODCONSEC]) REFERENCES [dbo].[HCCODAZUC] ([CODCONSEC]),
    CONSTRAINT [FK_HCCODAZUD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones clínicas, notas y observaciones para prescripción de medicamento desde módulo de urgencia/emergencia. Justificación diagnóstica o instructivo de uso (VARCHAR 200, texto libre, búsqueda por síntoma, diagnóstico, instrucción).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'Indications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de Indicaciones a la hora de prescribir un medicamento desde la opcion de emergencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'Indications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'Indications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad entregada o utilizada del producto, unidades realmente dispensadas/consumidas. Puede diferir de cantidad pedida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CANENTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Utilizada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CANENTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CANENTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de producto: 1=Medicamentos, 2=Materiales e insumos. Clasificación que determina si el artículo es fármaco o suministro médico-quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Producto  1: Medicamentos  2: Materiales e Insumos  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pedida del producto, unidades solicitadas en la prescripción de medicamento, material o insumo durante atención de urgencia/emergencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto (medicamento, material o insumo). Identificador único que referencia la tabla IHLISTPRO. Búsqueda por: código de medicamento, insumo, material médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo, número único que identifica el registro de prescripción o solicitud en urgencias/emergencia. Llave primaria que referencia a HCCODAZUC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de productos o insumos asociados a órdenes o solicitudes clínicas (azul/urgente), donde se controla la cantidad pedida y la cantidad entregada de cada producto, junto con las indicaciones de uso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD';
