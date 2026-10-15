CREATE TABLE [dbo].[AGBLOQMED] (
    [AUTONUBLO] INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECINIBLO] DATETIME                                                                      NOT NULL,
    [FECFINBLO] DATETIME                                                                      NOT NULL,
    [ESTADOBLO] BIT                                                                           NOT NULL,
    [TIPCAUBLO] CHAR (20)                                                                     NOT NULL,
    [OBSERVBLO] NVARCHAR (100)                                                                NULL,
    [ACTIVOBLO] BIT                                                                           NOT NULL,
    [CODCENATE] CHAR (10)                                                                     NOT NULL,
    CONSTRAINT [PK_AGBLOQMED] PRIMARY KEY CLUSTERED ([AUTONUBLO] ASC),
    CONSTRAINT [FK_AGBLOQMED_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGBLOQMED_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGBLOQMED] NOCHECK CONSTRAINT [FK_AGBLOQMED_ADCENATEN];


GO
ALTER TABLE [dbo].[AGBLOQMED] NOCHECK CONSTRAINT [FK_AGBLOQMED_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro Activo del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'ACTIVOBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'OBSERVBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Causa del Bloqueo del Medico    0: Hora Administrativa  1: Vacaciones  2: Permisos  3: Incapacidad  4: Calamidad Domestica  5: Compensatorios  6: Otros  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'TIPCAUBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado(Bloqueado-Desbloqueado)  del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'ESTADOBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'FECFINBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'FECINIBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Del profesional al que se le hace el Bloqueo Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'AUTONUBLO';

