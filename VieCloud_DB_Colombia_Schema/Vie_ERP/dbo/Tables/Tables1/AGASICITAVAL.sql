CREATE TABLE [dbo].[AGASICITAVAL] (
    [MES]                 NVARCHAR (255) NULL,
    [DIA]                 NVARCHAR (255) NULL,
    [HORA]                NVARCHAR (255) NULL,
    [CodEspe]             NVARCHAR (255) NULL,
    [Especialidad]        NVARCHAR (255) NULL,
    [Medico]              NVARCHAR (255) NULL,
    [HC]                  NVARCHAR (255) NULL,
    [Actividad]           NVARCHAR (255) NULL,
    [CUPS]                NVARCHAR (255) NULL,
    [ACTIVIDAD1]          NVARCHAR (255) NULL,
    [Fecha Inicial]       NVARCHAR (255) NULL,
    [Grupo]               NVARCHAR (255) NULL,
    [entidad]             NVARCHAR (255) NULL,
    [entidad Crystal]     NVARCHAR (255) NULL,
    [nombre]              NVARCHAR (255) NULL,
    [Telefono]            NVARCHAR (255) NULL,
    [direccion]           NVARCHAR (255) NULL,
    [sexo]                NVARCHAR (255) NULL,
    [Tipo_Identificacion] NVARCHAR (255) NULL,
    [FECHA_NACIMIENTO]    NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de citas médicas agendadas con validación, incluyendo datos del paciente, el profesional de salud, la especialidad, la actividad o procedimiento CUPS y la entidad aseguradora. Sirve para reportería y seguimiento de agenda médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes en que está programada la cita médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'MES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'MES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día del mes en que está programada la cita médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora programada para la cita médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'HORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'HORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno de la especialidad médica asociada a la cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'CodEspe';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'CodEspe';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la especialidad médica de la cita (ej: Medicina General, Ortopedia, Ginecología).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Especialidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Especialidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del médico o profesional de salud asignado a la cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de historia clínica del paciente, identificador del expediente médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'HC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'HC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la actividad o procedimiento médico programado en la cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Actividad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Actividad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS del procedimiento o servicio de salud programado en la cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción alternativa o complementaria de la actividad médica programada, detalle adicional del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio o fecha programada de la cita médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Fecha Inicial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Fecha Inicial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo poblacional o clasificación a la que pertenece el paciente (ej: grupo etario, grupo de riesgo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Grupo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Grupo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o nombre de la entidad aseguradora o pagadora del paciente (EPS, ARS, aseguradora).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'entidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'entidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad aseguradora formateado para reportes en Crystal Reports.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'entidad Crystal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'entidad Crystal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente agendado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono de contacto del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Telefono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Telefono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de residencia del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'direccion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'direccion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo biológico del paciente (masculino, femenino).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'sexo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'sexo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad del paciente (cédula, tarjeta de identidad, pasaporte, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Tipo_Identificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'Tipo_Identificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente, usada para calcular edad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'FECHA_NACIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITAVAL', @level2type = N'COLUMN', @level2name = N'FECHA_NACIMIENTO';
