CREATE TABLE [dbo].[INPRODPAT] (
    [IPRCODIGO]  CHAR (20) NOT NULL,
    [CODDIAGNO]  CHAR (4)  NOT NULL,
    [MinimumAge] TINYINT   NULL,
    [MaximumAge] TINYINT   NULL,
    [AgeMeasure] TINYINT   NULL,
    [Id]         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_INPRODPAT] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_INPRODPAT_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_INPRODPAT_ValidateUnique]
    ON [dbo].[INPRODPAT]([IPRCODIGO] ASC, [CODDIAGNO] ASC, [MinimumAge] ASC, [MaximumAge] ASC, [AgeMeasure] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de registro en tabla INPRODPAT, clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de edad (TINYINT): tipo de período (años, meses, días) para rango etario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'AgeMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la edad medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'AgeMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'AgeMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida (TINYINT) en rango etario aplicable al producto-patología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la edad maxima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima requerida (TINYINT) en rango etario aplicable al producto-patología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la edad minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CHAR 4, FK→INDIAGNOS) vinculado a producto de patología: enfermedad, condición, ICD-10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Diagnostico relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del producto de patologías (CHAR 20): asociación producto-diagnóstico para atención clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del producto de patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de restricciones por edad para productos o procedimientos asociados a diagnósticos (CIE-10). Indica el rango de edad mínimo y máximo en que un producto, medicamento o procedimiento es válido para un diagnóstico específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPRODPAT';
