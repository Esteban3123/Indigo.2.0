CREATE TABLE [dbo].[COPLANBEF] (
    [CODPLANBE] CHAR (2)        NOT NULL,
    [DESPLANBE] CHAR (40)       NOT NULL,
    [CODCONFAC] CHAR (4)        NULL,
    [INDAUDFOR] NUMERIC (18, 9) NOT NULL,
    CONSTRAINT [PK_COPLANBEF] PRIMARY KEY CLUSTERED ([CODPLANBE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de Auditoría INDIGO. Parámetro numérico para trazabilidad y control de auditoría en sistema ERP Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auditoria INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Concepto de Facturación (FK opcional). Referencia a concepto de facturación; campo no vinculado actualmente en arquitectura de contratos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'CODCONFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Concepto de Facturacion - Campo Opcional no esta definido en la arquitectura del modulo de contratos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'CODCONFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'CODCONFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del Plan de Beneficios. Nombre o detalle del plan de cobertura que define beneficiarios, prestaciones y alcance de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'DESPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'DESPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'DESPLANBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Plan de Beneficios (PK). Identificador único de 2 caracteres que clasifica planes de cobertura, beneficios y prestaciones en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'CODPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'CODPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'CODPLANBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Planes de beneficios o coberturas contratadas con aseguradoras o pagadores. Permite clasificar qué servicios y prestaciones cubre cada plan para la facturación y autorización de atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF';
