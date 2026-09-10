CREATE TABLE [dbo].[HCREFREDIAG] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [INDIAGNOSCOD] CHAR (4)      NOT NULL,
    [PRINCIPAL]    BIT           NOT NULL,
    [OBSERVACION]  VARCHAR (200) NULL,
    [HCREFRECEPID] INT           NOT NULL,
    CONSTRAINT [PK_HCREFREDIAG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFREDIAG_INDIAGNOS] FOREIGN KEY ([INDIAGNOSCOD]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la referencia médica o remisión asociada; clave foránea que vincula el diagnóstico a la recepción/atención de referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre para notas clínicas, comentarios adicionales o hallazgos relevantes sobre el diagnóstico referenciado; máximo 200 caracteres.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (SÍ/NO) que determina si este es el diagnóstico principal, primario o de mayor relevancia clínica en la referencia/atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el diagnostico es o no principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico de diagnóstico (4 caracteres); referencia a tabla INDIAGNOS que contiene el catálogo de diagnósticos, patologías o condiciones clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'INDIAGNOSCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'INDIAGNOSCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'INDIAGNOSCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de cada registro de diagnóstico en la referencia; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos asociados a una referencia o remisión clínica del paciente. Registra los códigos de diagnóstico (CIE-10) vinculados a cada referencia, indicando cuál es el diagnóstico principal y observaciones adicionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG';
