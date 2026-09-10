CREATE TABLE [dbo].[HCPRESCRAEXT] (
    [ID]                         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPRESCRA]                INT NOT NULL,
    [NUMEROAPLICACIONINICIAL]    INT NOT NULL,
    [NUMEROAPLICACIONREPOSICION] INT NOT NULL,
    [CANTIDADREPOSICION]         INT NOT NULL,
    CONSTRAINT [PK_HCPRESCRAEXT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPRESCRAEXT_HCPRESCRA] FOREIGN KEY ([IDHCPRESCRA]) REFERENCES [dbo].[HCPRESCRA] ([ID])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCPRESCRAEXT]
    ON [dbo].[HCPRESCRAEXT]([IDHCPRESCRA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades en reposición (INT) - Volumen/cantidad total de medicamento dispensado en cada evento de reposición o reabastecimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda cantidad  reposición medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de aplicaciones en reposición (INT) - Cantidad de dosis adicionales o reposiciones autorizadas del medicamento recetado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de aplicaciónes  en reposición de medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de aplicación inicial (INT) - Cantidad de dosis o administraciones iniciales prescritas del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de aplicacion inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la prescripción de medicamentos (INT, FK → HCPRESCRA.ID) - Referencia a la receta/orden farmacéutica parent', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'IDHCPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Id de presquisión de medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'IDHCPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'IDHCPRESCRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) - Consecutivo/secuencia de registro de la tabla HCPRESCRAEXT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de reposiciones de prescripciones médicas externas. Guarda el detalle de cada solicitud de reposición de medicamentos o insumos prescritos, indicando el número de aplicación inicial y la cantidad que se repone.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT';
