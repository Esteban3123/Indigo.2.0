CREATE TABLE [dbo].[HCHEMOMED] (
    [CONSECUTI] NUMERIC (18)    NOT NULL,
    [CODPRODUC] CHAR (20)       NOT NULL,
    [DOSISPROD] NUMERIC (18, 2) NULL,
    [CODUNIMED] CHAR (3)        NULL,
    [CODVIAADM] CHAR (3)        NOT NULL,
    [OBSERVMED] CHAR (400)      NULL,
    CONSTRAINT [PK_HCHEMOMED] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC, [CODPRODUC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o comentarios clínicos sobre el medicamento; instrucciones especiales, indicaciones, restricciones o datos relevantes del medicamento en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'OBSERVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'OBSERVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'OBSERVMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la vía de administración común (ej: intravenosa IV, oral, intramuscular IM); ruta o método de aplicación del medicamento al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida (ej: mg, ml, gramos, unidades); clasificador que especifica la magnitud en que se expresa la dosis del producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del producto (valor numérico con decimales); cantidad prescrita del medicamento expresada en unidades de medida específicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del producto químico/medicamento; identificador único del fármaco, droga oncológica o sustancia utilizada en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único de la tabla HCHEMOMED; identificador numérico de registro de medicamento en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de medicamentos ordenados en la historia clínica: registra cada producto medicamentoso prescrito, su dosis, unidad de medida, vía de administración y observaciones clínicas asociadas a la orden médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMOMED';
