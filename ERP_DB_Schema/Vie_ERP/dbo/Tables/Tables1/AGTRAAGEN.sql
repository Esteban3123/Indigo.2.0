CREATE TABLE [dbo].[AGTRAAGEN] (
    [AUTONUMER]                INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODAUTONU]                INT         NOT NULL,
    [CODCENATE]                CHAR (10)   NOT NULL,
    [FECHINORI]                DATETIME    NOT NULL,
    [FECHFIORI]                DATETIME    NOT NULL,
    [FECHINMOD]                DATETIME    NOT NULL,
    [FECHFIMOD]                DATETIME    NOT NULL,
    [CODUSUMOD]                CHAR (20)   NOT NULL,
    [FECREGSIS]                DATETIME    NOT NULL,
    [ConsultationCode]         VARCHAR (6) NULL,
    [ModifiedConsultationCode] VARCHAR (6) NULL,
    CONSTRAINT [PK_AGTRAAGEN] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGTRAAGEN_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[AGTRAAGEN] NOCHECK CONSTRAINT [FK_AGTRAAGEN_ADCENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del consultorio al cual fue modificada la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'ModifiedConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del consultorio original de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'ConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Registro del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que Modifico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora Final de la Agenda Despues de Ser Modificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHFIMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora Inicial de la Agenda Despues de Ser Modificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHINMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora Final Original de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHFIORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora Inicial Original de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHINORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la relacion con la Agenda Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'AUTONUMER';

