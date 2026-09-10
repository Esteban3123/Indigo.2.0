CREATE TABLE [dbo].[HCREGIONESNOQX] (
    [ID]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOHCORPNOQ]   INT           NOT NULL,
    [NOMREGTRATA]    VARCHAR (100) NOT NULL,
    [DOSTOTAL]       INT           NOT NULL,
    [UNMEDDOSISTO]   INT           NOT NULL,
    [DOSTOTALAPROB]  INT           NOT NULL,
    [DOSFRACC]       INT           NOT NULL,
    [UNMEDDOSFRAC]   INT           NOT NULL,
    [NUMEROFRACCION] INT           NOT NULL,
    [CODDIAGNO]      CHAR (4)      NULL,
    CONSTRAINT [PK_HCREGIONESNOQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREGIONESNOQX_HCREGIONESNOQX] FOREIGN KEY ([AUTOHCORPNOQ]) REFERENCES [dbo].[HCORDPRON] ([AUTO]),
    CONSTRAINT [FK_HCREGIONESNOQX_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico de la orden de prescripción no quirúrgica (CIE-10), FK a INDIAGNOS, PII sensible, máx 4 caracteres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo diagnostico Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la fracción o dosis dentro del tratamiento prescrito, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'NUMEROFRACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Fracción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'NUMEROFRACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'NUMEROFRACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la dosis fraccionada (mg, ml, UI, etc.), INT referencia a catálogo de unidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'UNMEDDOSFRAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medidad de Dosis Fracción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'UNMEDDOSFRAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'UNMEDDOSFRAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de la fracción individual del tratamiento, cantidad numérica en la unidad especificada, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSFRACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis fraccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSFRACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSFRACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis total aprobada acumulada para el tratamiento completo según orden clínica, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSTOTALAPROB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis total aprobado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSTOTALAPROB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSTOTALAPROB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la dosis total del tratamiento (mg, ml, UI, etc.), INT referencia a catálogo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'UNMEDDOSISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de la Dosis Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'UNMEDDOSISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'UNMEDDOSISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis total prescrita del medicamento o tratamiento para toda la región tratada, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción de la región anatómica tratada (ej: cabeza, tórax, abdomen), VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'NOMREGTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Region de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'NOMREGTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'NOMREGTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de prescripción no quirúrgica (FK a HCORDPRON.AUTO), INT, relación folio principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORPNOQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORPNOQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORPNOQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (IDENTITY) de cada registro de región tratada en la tabla, INT PK', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Regiones de tratamiento no quirúrgico (radioterapia u oncología) asociadas a una historia clínica. Registra las zonas del cuerpo irradiadas o tratadas, con sus dosis totales, fraccionadas y aprobadas, unidades de medida y diagnóstico relacionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX';
