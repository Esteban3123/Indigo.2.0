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
ALTER TABLE [dbo].[CHREGDEIN] NOCHECK CONSTRAINT [FK_CHREGDEIN_ADcenaten];


GO
ALTER TABLE [dbo].[CHREGDEIN] NOCHECK CONSTRAINT [FK_CHREGDEIN_CHMOTDEMI];


GO
ALTER TABLE [dbo].[CHREGDEIN] NOCHECK CONSTRAINT [FK_CHREGDEIN_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMANULAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Demanda Insatisfecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMFECHAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Motivo de Demanda Insatisfecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'CODMOTDEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de Demandas Insatisfechas:  Codigo de Consecutivo 00000009  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDEIN', @level2type = N'COLUMN', @level2name = N'DEMCONSEC';

