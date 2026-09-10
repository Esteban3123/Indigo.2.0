CREATE TABLE [dbo].[HCMOAFOLI] (
    [CODMOANFO] CHAR (3)      NOT NULL,
    [DESMOANFO] VARCHAR (200) NOT NULL,
    [ESTMOANFO] BIT           NOT NULL,
    CONSTRAINT [PK_HCMOAFOLI] PRIMARY KEY CLUSTERED ([CODMOANFO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del motivo de anulación de folios (BIT: 1=activo, 0=inactivo). Controla si el motivo está disponible para registrar anulaciones en facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'ESTMOANFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Motivo Anulacion de Folios ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'ESTMOANFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'ESTMOANFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del motivo de anulación de folios. Texto que explica la razón por la cual se anuló un folio/comprobante (ej: error de impresión, datos incorrectos, documento perdido). Visible en reportes de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'DESMOANFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Motivo Anulacion de Folios ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'DESMOANFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'DESMOANFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del motivo de anulación de folios (PK, CHAR 3). Referencia única para clasificar y auditar anulaciones de comprobantes, facturas y documentos en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'CODMOANFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Motivo Anulacion de Folios ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'CODMOANFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'CODMOANFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de modalidades de atención o tipos de folio utilizados en la historia clínica. Permite clasificar y parametrizar las diferentes formas en que se registra o agrupa la atención médica dentro del expediente clínico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI';
