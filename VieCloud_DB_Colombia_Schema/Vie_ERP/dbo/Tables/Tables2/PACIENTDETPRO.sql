CREATE TABLE [dbo].[PACIENTDETPRO] (
    [ID]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDINPACIENTPYP]   INT           NOT NULL,
    [IDPROGRAMA]       INT           NOT NULL,
    [FECHAINSCR]       DATE          NULL,
    [FECHULTIMATEN]    DATE          NULL,
    [JUSTIINSCRIPCION] VARCHAR (MAX) NULL,
    [ESTADO]           BIT           CONSTRAINT [DF_PACIENTDETPRO_ESTADO] DEFAULT ((1)) NULL,
    [JUSTIRETIRO]      VARCHAR (MAX) NULL,
    [CITAREPROGRA]     INT           NULL,
    [JUSTICITAREPRO]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_PACIENTDETPRO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PACIENTDETPRO_HCPROGRAPYP] FOREIGN KEY ([IDPROGRAMA]) REFERENCES [dbo].[RIAS] ([ID]),
    CONSTRAINT [FK_PACIENTDETPRO_INPACIENTPYP] FOREIGN KEY ([IDINPACIENTPYP]) REFERENCES [dbo].[INPACIENTPYP] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de la cita reprogramada; motivo, razón o explicación de por qué se reprogramó la cita del paciente en el programa (VARCHAR MAX, texto libre)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTICITAREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación cita reprogramada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTICITAREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTICITAREPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cita reprogramada; indicador numérico de si la cita fue reprogramada o número de reprogramaciones en el programa (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'CITAREPROGRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cita reprogramada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'CITAREPROGRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'CITAREPROGRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación del retiro del paciente; motivo, causa o explicación del abandono, desvinculación o salida del programa (VARCHAR MAX, texto libre)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTIRETIRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación del retiro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTIRETIRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTIRETIRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de inscripción en programa: 1=Activo en el programa, 0=No activo; indica si el paciente está vinculado activamente (BIT, default=1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Activo en el programa 0-No activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de la inscripción; motivo, criterio clínico o administrativo por el cual el paciente fue inscrito en el programa (VARCHAR MAX, texto libre)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTIINSCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de la inscripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTIINSCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTIINSCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha última atención, entrega o contacto; último registro de asistencia o seguimiento del paciente en el programa (DATE, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'FECHULTIMATEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha última entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'FECHULTIMATEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'FECHULTIMATEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inscripción; fecha de afiliación o vinculación inicial del paciente al programa (DATE, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'FECHAINSCR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inscripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'FECHAINSCR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'FECHAINSCR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del programa; clave foránea a tabla RIAS (programa de salud, RIAS, atención integral); INT NOT NULL, FK', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'IDPROGRAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla RIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'IDPROGRAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'IDPROGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ingreso de paciente; clave foránea a tabla INPACIENTPYP (ingreso o atención del paciente); INT NOT NULL, FK', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'IDINPACIENTPYP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla INPACIENTPYP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'IDINPACIENTPYP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'IDINPACIENTPYP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo; clave primaria de la relación paciente-programa (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de programas de Promoción y Prevención (PyP) a los que está inscrito cada paciente. Registra el estado de participación, fechas clave, justificaciones de ingreso o retiro, y gestión de citas reprogramadas dentro del programa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO';
