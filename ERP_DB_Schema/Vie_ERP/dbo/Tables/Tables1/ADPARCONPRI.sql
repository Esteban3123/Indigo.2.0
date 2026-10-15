CREATE TABLE [dbo].[ADPARCONPRI] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]    CHAR (10) NOT NULL,
    [CODENTIDA]    CHAR (9)  NULL,
    [LUN]          BIT       NULL,
    [MAR]          BIT       NULL,
    [MIER]         BIT       NULL,
    [JUE]          BIT       NULL,
    [VIER]         BIT       NULL,
    [SAB]          BIT       NULL,
    [DOM]          BIT       NULL,
    [TRIAGECUATRO] INT       NULL,
    [TRIAGECINCO]  INT       NULL,
    [HORAS]        INT       NOT NULL,
    CONSTRAINT [PK_ADPARCONPRI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Centro Atencion] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[ADPARCONPRI] NOCHECK CONSTRAINT [Centro Atencion];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas de espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'HORAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Triage cinco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'TRIAGECINCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Teiage Cuatro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'TRIAGECUATRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Domingo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'DOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Sabado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'SAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Viernes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'VIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Jueves', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'JUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Miercoles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'MIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Martes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'MAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Lunes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'LUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'ID';

