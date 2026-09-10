CREATE TABLE [dbo].[HCINFCONCEXT] (
    [ID]                         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCINFCONC]                INT NOT NULL,
    [NUMEROAPLICACIONINICIAL]    INT NOT NULL,
    [NUMEROAPLICACIONREPOSICION] INT NOT NULL,
    [CANTIDADREPOSICION]         INT NOT NULL,
    CONSTRAINT [PK_HCINFLIQAEXT] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCINFCONCEXT]
    ON [dbo].[HCINFCONCEXT]([IDHCINFCONC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades en reposición o reabastecimiento de medicamento/insumo. Tipo: INT. Dominio: cantidad, stock, inventario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda cantidad de reposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la aplicación de reposición del medicamento o insumo. Tipo: INT. Sinónimos: folio de reposición, número de evento de reabastecimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número aplicación reposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la aplicación inicial del medicamento o insumo al paciente. Tipo: INT. Sinónimos: folio inicial, número de evento de dispensación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número aplicación inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador que referencia la tabla HCINFCONC (FK). Vincula el registro de reposición a la historia clínica, informe o concentrado de medicamentos/insumos. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'IDHCINFCONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HCINFCONC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'IDHCINFCONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'IDHCINFCONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo autogenerado (IDENTITY). Clave primaria de la tabla HCINFCONCEXT. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de extensiones o reposiciones de concentrados en historia clínica: guarda el detalle de cada reposición asociada a una indicación de concentrado (por ejemplo, hemocomponentes o soluciones), incluyendo el número de aplicación inicial, el número de aplicación de reposición y la cantidad repuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT';
