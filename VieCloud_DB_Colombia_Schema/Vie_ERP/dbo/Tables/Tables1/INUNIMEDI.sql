CREATE TABLE [dbo].[INUNIMEDI] (
    [CODUNIMED] VARCHAR (20)  NOT NULL,
    [DESUNIMED] VARCHAR (100) NOT NULL,
    [ABRUNIMED] CHAR (10)     NULL,
    [TIPUNIDAD] CHAR (1)      NULL,
    [INDAUDFOR] NUMERIC (18)  NOT NULL,
    [CODHOMHV]  VARCHAR (20)  NULL,
    CONSTRAINT [PK_INUNIMEDI] PRIMARY KEY CLUSTERED ([CODUNIMED] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de homologación Health Vault (VARCHAR 20), identificador de integración con Smart Health para sincronización de unidades de medida en intercambio de datos clínicos interoperables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo que pasa a health vault en la integración con smarth health', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y folio (NUMERIC 18), código secuencial de control y trazabilidad para registro de cambios, auditoría de transacciones y cumplimiento normativo RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad de medida (CHAR 1): 1=Peso, 2=Volumen, 3=Unidad de Administración; clasifica la categoría física o administrativa del medicamento, procedimiento o material.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'TIPUNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Peso 2- Volumen 3- Unidad Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'TIPUNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'TIPUNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviatura de la unidad de medida (CHAR 10), símbolo corto usado en prescripciones, recetas, exámenes, laboratorio e inventario (ej: mg, ml, UI, comp).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'ABRUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abreviatura Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'ABRUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'ABRUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la unidad de medida, nombre completo (VARCHAR 100) que especifica el tipo de unidad: peso, volumen, dosis, cantidad o administración (ej: miligramos, mililitros, unidades, tabletas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'DESUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'DESUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'DESUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida, identificador único (VARCHAR 20) que clasifica y categoriza unidades de administración farmacéutica, laboratorial o de inventario en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de unidades de medida utilizadas en medicamentos e insumos médicos (por ejemplo: tableta, ampolla, mililitro, gramo). Permite estandarizar las unidades en recetas, dispensación y control de inventario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI';
