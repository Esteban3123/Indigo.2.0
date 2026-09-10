CREATE TABLE [dbo].[CHREGDEIN] (
    [DEMCONSEC] CHAR (10)    NOT NULL,
    [CODMOTDEI] CHAR (2)     NOT NULL,
    [CODCENATE] CHAR (10)    NOT NULL,
    [UFUCODIGO] CHAR (10)    NOT NULL,
    [DEMFECHAI] DATETIME     NOT NULL,
    [DEMOBSERV] CHAR (250)   NULL,
    [DEMANULAD] BIT          NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHREGDEIN] PRIMARY KEY CLUSTERED ([DEMCONSEC] ASC),
    CONSTRAINT [FK_CHREGDEIN_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHREGDEIN_CHMOTDEMI] FOREIGN KEY ([CODMOTDEI]) REFERENCES [dbo].[CHMOTDEMI] ([CODMOTDEI]),
    CONSTRAINT [FK_CHREGDEIN_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18). Identificador único del registro de auditoría asociado a la demanda insatisfecha para trazabilidad y control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro anulado (BIT, 0=activo, 1=anulado). Indicador booleano que marca si la demanda insatisfecha ha sido anulada o invalidada en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMANULAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMANULAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMANULAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de la demanda insatisfecha (VARCHAR 250). Notas, comentarios o detalles adicionales que justifican o describen el motivo de la demanda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de demanda insatisfecha (DATETIME). Fecha y hora en que se registró o identificó la demanda, solicitud o necesidad insatisfecha del paciente/atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMFECHAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Demanda Insatisfecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMFECHAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMFECHAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (CHAR 10, FK→INUNIFUNC). Identificador del área, departamento o servicio de salud donde se originó la demanda insatisfecha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK→ADCENATEN). Identificador de la IPS, clínica, hospital o punto de prestación donde se registra la demanda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de demanda insatisfecha (CHAR 2, FK→CHMOTDEMI). Clasificación o categoría que especifica la razón, tipo o causa de la demanda insatisfecha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'CODMOTDEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Motivo de Demanda Insatisfecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'CODMOTDEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'CODMOTDEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de demandas insatisfechas (CHAR 10, PK). Número secuencial único que identifica cada registro de demanda insatisfecha en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de Demandas Insatisfechas:  Codigo de Consecutivo 00000009  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de desingresos o egresos de pacientes: guarda el historial de salidas, altas o retiros de pacientes de una unidad funcional o centro de atención, incluyendo el motivo del desingreso, observaciones y si el registro fue anulado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN';
