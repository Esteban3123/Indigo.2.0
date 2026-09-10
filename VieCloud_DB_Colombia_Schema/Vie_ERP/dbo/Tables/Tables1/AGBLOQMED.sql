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
ALTER TABLE [dbo].[AGBLOQMED] NOCHECK CONSTRAINT [FK_AGBLOQMED_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGBLOQMED].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, unidad funcional o establecimiento de salud donde se aplica el bloqueo. FK a ADCENATEN. Sinónimos: sede, institución, clínica, hospital, punto de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano de si el registro de bloqueo médico está activo (1) o inactivo/eliminado (0). Controla la vigencia del bloqueo en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'ACTIVOBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Activo del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'ACTIVOBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'ACTIVOBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas, comentarios o detalles adicionales que explican o justifican el bloqueo del profesional de salud. Texto libre de observaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'OBSERVBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'OBSERVBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'OBSERVBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de la causa del bloqueo: 0=Hora Administrativa, 1=Vacaciones, 2=Permisos, 3=Incapacidad, 4=Calamidad Doméstica, 5=Compensatorios, 6=Otros. Define el motivo del impedimento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'TIPCAUBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Causa del Bloqueo del Medico    0: Hora Administrativa  1: Vacaciones  2: Permisos  3: Incapacidad  4: Calamidad Domestica  5: Compensatorios  6: Otros  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'TIPCAUBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'TIPCAUBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del bloqueo: 1=Bloqueado (impedido), 0=Desbloqueado (activo). Indica si el profesional está restringido o habilitado para atender.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'ESTADOBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado(Bloqueado-Desbloqueado)  del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'ESTADOBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'ESTADOBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final del bloqueo. DateTime. Marca cuándo vence o termina la restricción del profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'FECFINBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'FECFINBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'FECFINBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial del bloqueo. DateTime. Marca cuándo comienza la restricción o impedimento del profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'FECINIBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'FECINIBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'FECINIBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, especialista, etc.) bloqueado. CHAR(20), FK a INPROFSAL. PII ofuscado. Sinónimos: cédula, identificación del profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Del profesional al que se le hace el Bloqueo Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de bloqueo médico. Clave primaria clustered. Autoincrementable, no replicable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'AUTONUBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico del Bloqueo del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'AUTONUBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED', @level2type = N'COLUMN', @level2name = N'AUTONUBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de bloqueos de agenda de profesionales de la salud. Permite definir períodos en los que un profesional no está disponible para atención en un centro determinado, indicando el tipo de causa y el estado del bloqueo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQMED';
