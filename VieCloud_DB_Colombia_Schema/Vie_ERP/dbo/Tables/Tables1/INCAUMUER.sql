CREATE TABLE [dbo].[INCAUMUER] (
    [CODCAUMUE] CHAR (3)     NOT NULL,
    [DESCAUMUE] CHAR (40)    NOT NULL,
    [EXICERDEF] BIT          NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [CAMUERASO] CHAR (1)     NULL,
    CONSTRAINT [PK_INCAUMUER] PRIMARY KEY CLUSTERED ([CODCAUMUE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa de muerte asociada/secundaria (Char 1). Clasificador que indica si la causa registrada es primaria, secundaria o contribuyente al evento de defunción, según protocolos RIPS o certificado de defunción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'CAMUERASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Causa de muerte asociada  - ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'CAMUERASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'CAMUERASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría o campo reservado (Numeric 18). Marcador para trazabilidad, auditoría forense y cumplimiento normativo en registro de causas de fallecimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: exigir certificado de defunción (Bit). Bandera que determina si la causa de muerte requiere obligatoriamente documento/certificado de defunción firmado por profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'EXICERDEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exigir certificado de defuncion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'EXICERDEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'EXICERDEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la causa de muerte (Char 40). Denominación clínica del diagnóstico o causa que originó el fallecimiento del paciente, utilizado en reportes de mortalidad y certificados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'DESCAUMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'DESCAUMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'DESCAUMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de causa de muerte (Char 3). Identificador único de la causa/diagnóstico de defunción registrado en el sistema ERP. Clave primaria para clasificación de mortalidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de causas de muerte registradas en el sistema. Permite clasificar y codificar el motivo de fallecimiento de un paciente para efectos clínicos, estadísticos y de certificación de defunción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER';
