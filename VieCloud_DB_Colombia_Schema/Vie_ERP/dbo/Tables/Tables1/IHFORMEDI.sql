CREATE TABLE [dbo].[IHFORMEDI] (
    [CODFORMED]            VARCHAR (20) NOT NULL,
    [DESFORMED]            CHAR (100)   NOT NULL,
    [CODHOMHV]             VARCHAR (20) NULL,
    [RequireStability]     BIT          NULL,
    [AllowExtramural]      BIT          CONSTRAINT [DF_IHFORMEDI_AllowExtramural] DEFAULT ((0)) NOT NULL,
    [AutomaticCalculation] BIT          CONSTRAINT [DF_IHFORMEDI_AutomaticCalculation] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_IHFORMEDI] PRIMARY KEY CLUSTERED ([CODFORMED] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=calcula automáticamente cantidades en prescripción médica para medicamentos extramurales (adicionales), 0=cálculo manual. Default=0.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'AutomaticCalculation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'05-07-2023 me indica si calcula de forma automatica las cantidades en la prescripcion medica, por ahora solo para medicamentos extramurales que son medicamentos adicionales.   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'AutomaticCalculation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'AutomaticCalculation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=forma médica se lista en creación de productos adicionales en historia clínica extramural por prescripción médica, 0=no permitido. Default=0.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'AllowExtramural';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'05-07-2023 me indica si la forma medica se lista en la creaciòn de un producto adicional en la  una hc extramural por parte del medico. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'AllowExtramural';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'AllowExtramural';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=medicamento requiere cadena de frío/estabilidad (conservación especial), 0=no requiere. Crítico para almacenamiento y transporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'RequireStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el medicamento requiere o no estabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'RequireStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'RequireStability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código homólogo o equivalente de la vía de administración para interoperabilidad con Smart Health u otros sistemas externos. VARCHAR(20), FK opcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo homologo de la via de administración que pasa a smarth health', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la forma de presentación o vía de administración del medicamento. Ej: tableta, cápsula, solución inyectable, crema. CHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'DESFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'DESFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'DESFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la forma de presentación del medicamento (vía de administración): oral, inyectable, tópica, inhalada, etc. PK VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de formas de administración de medicamentos (oral, intravenosa, intramuscular, etc.), indicando si requieren estabilidad, si permiten atención extramural y si el cálculo de dosis es automático.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI';
