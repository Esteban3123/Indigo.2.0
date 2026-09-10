CREATE TABLE [dbo].[HCRADORDEN] (
    [ID]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCORDPRON]              INT                                                                              NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODDIAGNO]                CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [FECHAORDEN]               DATETIME                                                                         NOT NULL,
    [ESTADO]                   TINYINT                                                                          NOT NULL,
    [FECHAREGISTRO]            DATETIME                                                                         NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [CODESPECI]                CHAR (3)                                                                         NOT NULL,
    [FECHAPROSIMULA]           DATETIME                                                                         NULL,
    [USUARIOPROSIMULA]         CHAR (20)                                                                        NULL,
    [FECHASIMULA]              DATETIME                                                                         NULL,
    [USUARIOSIMULA]            CHAR (20)                                                                        NULL,
    [FECHAPLANEA]              DATETIME                                                                         NULL,
    [USUARIOPLANEA]            CHAR (20)                                                                        NULL,
    [NOREFIEREFECHAPLANEA]     BIT                                                                              NULL,
    [USUARIONOREFIEREPLANEA]   CHAR (20)                                                                        NULL,
    [CITABRAQUIASIGNADA]       BIT                                                                              NULL,
    [IDHCMOANULB]              CHAR (4)                                                                         NULL,
    [USUARIOANULACION]         VARCHAR (20)                                                                     NULL,
    [FECHAANULACION]           DATETIME                                                                         NULL,
    [OBSERVANULACION]          VARCHAR (MAX)                                                                    NULL,
    [CODCENATEULTIMO]          CHAR (10)                                                                        NULL,
    [FECHACONTORNEO]           DATETIME                                                                         NULL,
    [USUARIOCONTORNEO]         CHAR (20)                                                                        NULL,
    [NOREFIEREFECHACONTOR]     BIT                                                                              NULL,
    [USUARIONOREFIERECONTOR]   CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCRADORDEN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADORDEN_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADORDEN_ADCENATEN1] FOREIGN KEY ([CODCENATEULTIMO]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADORDEN_HCMOANULB] FOREIGN KEY ([IDHCMOANULB]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCRADORDEN_HCORDPRON] FOREIGN KEY ([IDHCORDPRON]) REFERENCES [dbo].[HCORDPRON] ([AUTO]),
    CONSTRAINT [FK_HCRADORDEN_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCRADORDEN_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCRADORDEN_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCRADORDEN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCRADORDEN_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_HCORDPRON];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCRADORDEN].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCRADORDEN].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCRADORDEN].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO



GO



GO



GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_HCORDPRON];


GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registra la no referencia o rechazo de la fecha de contorneo en radioterapia. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIONOREFIERECONTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de No refiere Contorneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIONOREFIERECONTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIONOREFIERECONTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si se rechaza o no aplica la fecha de contorneo en el ciclo de radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'NOREFIEREFECHACONTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No refiere fecha de contorneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'NOREFIEREFECHACONTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'NOREFIEREFECHACONTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que ejecuta el contorneo (delineación de volúmenes objetivo) en radioterapia. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOCONTORNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Contorneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOCONTORNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOCONTORNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realiza el contorneo (delineación de órganos y volúmenes) en radioterapia. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHACONTORNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de contorneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHACONTORNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHACONTORNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del último centro de atención donde se ejecutó la fase más reciente de la orden (simulación, planeación, contorneo o aplicación). Referencia FK a ADCENATEN. Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODCENATEULTIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta columna Identifica el Ultimo centro de atencion en el cual el paciente a estado, por ejemplo:   La simulacion la hacen en el Centro A  La planeacion la hacen en el Centro B  La aplicacion de la radio la hacen en el Centro C    En esta columna se almacena el Ultimo utilizado.   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODCENATEULTIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODCENATEULTIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas y justificación detallada del motivo de anulación de la orden de radioterapia. Tipo: VARCHAR(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se anula la orden de procedimiento radioncológico. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que ejecuta la anulación de la orden. Tipo: VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que hace la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de anulación (FK a HCMOANULB). Clasifica la razón: administrativo, clínico, paciente no asiste, etc. Tipo: CHAR(4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo anulacion, campo relacionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si la primera cita de braquiterapia ya fue asignada a la orden. Solo aplica para órdenes de braquiterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CITABRAQUIASIGNADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para las ordenes de Braquiterapia, indica si ya se asigno la primer cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CITABRAQUIASIGNADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CITABRAQUIASIGNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registra la no referencia o rechazo de la fecha de planeación en radioterapia. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIONOREFIEREPLANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de No refiere Planeación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIONOREFIEREPLANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIONOREFIEREPLANEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si se rechaza o no aplica la fecha de planeación en el ciclo de radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'NOREFIEREFECHAPLANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No refiere fecha de planeacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'NOREFIEREFECHAPLANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'NOREFIEREFECHAPLANEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que ejecuta la planeación (cálculo de dosis y optimización de haces) en radioterapia. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOPLANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Planeación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOPLANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOPLANEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realiza la planeación (tratamiento dosimétrico) en radioterapia. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAPLANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de planeación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAPLANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAPLANEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que ejecuta la simulación real en el equipo de radioterapia. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOSIMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Real Simulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOSIMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOSIMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora real en que se ejecuta la simulación radioncológica. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHASIMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Real de la Simulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHASIMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHASIMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que programa o agenda la simulación como actividad futura. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOPROSIMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario programo la Simulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOPROSIMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOPROSIMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora programada para la simulación en radioterapia. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAPROSIMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que seleccionan como programacion de la Simulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAPROSIMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAPROSIMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica (radioncología, braquiterapia, etc.). FK a INESPECIA. Tipo: CHAR(3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción de contrato CUPS (relación con VIE ERP, tabla contract.CUPSEntityContractDescriptions). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro en la base de datos. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico del estado de la orden (1=Solicitado, 2=Simulación Programada, 3=Planeación Programada, 4=Planeación Confirmada, 5=Finalizado, 6=Anulado, 7=Completado, 8=Braquiterapia Iniciada). Tipo: TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Solicitado  2 - Simulacion Programada (aplica solo para radioterapias)  3 - Planeacion Programada  (aplica solo para radioterapias)  4 - Planeacion Confirmada   (aplica solo para radioterapias)  5 - Finalizado    6 - Anulado   7 - Completado   8 - Braquiterapia Iniciada ( cuando se ha aplicado la primer dosis desde  dashboard de Braquiterapia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de emisión de la orden de radioterapia por el médico. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS que identifica el servicio radioncológico (ej: 420001, braquiterapia). FK a INCUPSIPS. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 del padecimiento que motiva la radioterapia. FK a INDIAGNOS. Tipo: CHAR(4). PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico/oncólogo) que emite la orden. FK a INPROFSAL. Tipo: VARCHAR(25). PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente (cédula, documento, equivalente a número de ingreso). FK a INPACIENT. Tipo: VARCHAR(25). PII ofuscado con Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se origina la orden. FK a ADCENATEN. Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo centro atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de procedimiento no quirúrgico asociada (FK a HCORDPRON.AUTO). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion con ordenes procedimientos NO QX  (HCORDPRON)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumerado de la orden de radioterapia/braquiterapia. Primary Key. Tipo: INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de radiología (o procedimientos de imagen/diagnóstico) generadas en la historia clínica. Registra cada orden médica con su paciente, profesional, diagnóstico, servicio solicitado, estados de planeación, simulación y anulación, permitiendo el seguimiento del ciclo de vida de la orden desde su creación hasta su ejecución o cancelación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN';
