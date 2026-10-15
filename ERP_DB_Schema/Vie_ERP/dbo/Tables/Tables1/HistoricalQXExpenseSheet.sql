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
ALTER TABLE [dbo].[HistoricalQXExpenseSheet] NOCHECK CONSTRAINT [FK_CODCENATE_HQES];


GO
ALTER TABLE [dbo].[HistoricalQXExpenseSheet] NOCHECK CONSTRAINT [FK_CODPROSAL];


GO
ALTER TABLE [dbo].[HistoricalQXExpenseSheet] NOCHECK CONSTRAINT [FK_HistoricalQXExpenseSheet];


GO
ALTER TABLE [dbo].[HistoricalQXExpenseSheet] NOCHECK CONSTRAINT [FK_UFUCODIGO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de registro:   1. Guardado   2. Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna con llave foránea a la tabla cabecera HCHOJAGASTOQX que guarda el id de la hoja de gasto qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'IDHCHOGASTOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'llave primaria autonumérica, identificación única del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HistoricalQXExpenseSheet', @level2type = N'COLUMN', @level2name = N'ID';

