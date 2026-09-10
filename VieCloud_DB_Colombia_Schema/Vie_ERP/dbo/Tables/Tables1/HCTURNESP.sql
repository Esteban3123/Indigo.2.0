CREATE TABLE [dbo].[HCTURNESP] (
    [CONSECUTI] INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI] CHAR (3)                                                                      NOT NULL,
    [FECHAINIC] DATETIME                                                                      NOT NULL,
    [FECHAFINA] DATETIME                                                                      NOT NULL,
    [CODCENATE] CHAR (10)                                                                     NOT NULL,
    [OBSERVACI] CHAR (500)                                                                    NULL,
    [CONFREPET] NVARCHAR (MAX)                                                                NULL,
    [CODUSUARI] CHAR (20)                                                                     CONSTRAINT [DF_HCTURNESP_CODUSUARI] DEFAULT ('Codigo de usuario') NOT NULL,
    [TODOELDIA] BIT                                                                           NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCTURNESP_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCTURNESP_HCTURNESP] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCTURNESP] ([CONSECUTI]),
    CONSTRAINT [FK_HCTURNESP_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCTURNESP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCTURNESP].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_HCTURNESP]
    ON [dbo].[HCTURNESP]([CODCENATE] ASC, [CODESPECI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de jornada completa (1=sí, 0=no). Determina si el turno o cita abarca todo el día o está limitado a horario específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'TODOELDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Todo el dia 1:si  0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'TODOELDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'TODOELDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario Indigo Crystal (CHAR 20). Identificador del operador que registra o modifica el turno en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario Indigo Crystal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de recurrencia del turno. Define patrón de repetición (diaria, semanal, mensual) y fechas de vigencia de la cita recurrente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CONFREPET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información de recurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CONFREPET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CONFREPET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales sobre el turno o cita. Notas clínicas, restricciones especiales o indicaciones relevantes para el profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (FK a ADCENATEN). Identifica la unidad funcional, sede o institución donde se realiza la consulta o procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final del turno. Marca el término de la disponibilidad, cita o jornada del profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'FECHAFINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'FECHAFINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'FECHAFINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial del turno. Marca el inicio de la disponibilidad, cita o jornada del profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'FECHAINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'FECHAINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'FECHAINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad (FK a INESPECIA). Identifica el área de medicina (cardiología, pediatría, urgencias, etc.) asociada al turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (FK a INPROFSAL, PII-Identification_Ofuscado). Identificación del médico, enfermero o profesional asignado al turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno (PK, IDENTITY). Identificador único e incremental del registro de turno en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Turnos o franjas horarias de disponibilidad asignadas a profesionales de la salud por especialidad y centro de atención. Permite configurar los bloques de tiempo en que un profesional atiende, incluyendo si cubre todo el día, para facilitar el agendamiento de citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP';
