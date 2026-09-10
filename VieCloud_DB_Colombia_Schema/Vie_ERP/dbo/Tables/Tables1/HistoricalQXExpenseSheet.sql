CREATE TABLE [dbo].[HistoricalQXExpenseSheet] (
    [ID]            INT                                                                           IDENTITY (1, 1) NOT NULL,
    [IDHCHOGASTOQX] INT                                                                           NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [DATE]          DATETIME                                                                      NOT NULL,
    [CODCENATE]     CHAR (10)                                                                     NULL,
    [UFUCODIGO]     CHAR (10)                                                                     NULL,
    [TYPE]          INT                                                                           NOT NULL,
    CONSTRAINT [PK__Historic__3214EC27A2AF087E] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CODCENATE_HQES] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CODPROSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HistoricalQXExpenseSheet] FOREIGN KEY ([IDHCHOGASTOQX]) REFERENCES [dbo].[HCHOJAGASTOQX] ([ID]),
    CONSTRAINT [FK_UFUCODIGO] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HistoricalQXExpenseSheet] NOCHECK CONSTRAINT [FK_HistoricalQXExpenseSheet];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HistoricalQXExpenseSheet].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro de hoja de gasto quirúrgico: 1=Guardado (borrador), 2=Confirmado (finalizado). Indica estado del documento de gasto QX.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'TYPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de registro:   1. Guardado   2. Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'TYPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio, área, departamento) donde se registra el gasto quirúrgico. FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, sede o institución donde se originó el gasto QX. FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro o creación del movimiento en la hoja de gasto quirúrgico (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'DATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'DATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, cirujano, anestesiólogo) asociado al gasto QX. PII ofuscado. FK a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la hoja de gasto quirúrgico cabecera en HCHOJAGASTOQX. Vincula este registro histórico al documento principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'IDHCHOGASTOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna con llave foránea a la tabla cabecera HCHOJAGASTOQX que guarda el id de la hoja de gasto qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'IDHCHOGASTOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'IDHCHOGASTOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria autonumérica (INT IDENTITY), identificación única e irrepetible del registro histórico en HistoricalQXExpenseSheet.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'llave primaria autonumérica, identificación única del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de hojas de gastos quirúrgicos: registra el seguimiento histórico de los cambios o versiones de las hojas de gastos asociadas a procedimientos quirúrgicos, indicando el profesional responsable, el centro de atención, la unidad funcional y el tipo de movimiento o evento registrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet';
