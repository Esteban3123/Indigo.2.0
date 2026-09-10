CREATE TABLE [dbo].[RIASXPACIENTE] (
    [ID]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRIAS]            INT                                                                              NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHAINCRIPCION]   DATETIME                                                                         NULL,
    [ESTADO]            INT                                                                              NOT NULL,
    [ORIGENINSCRIPCION] INT                                                                              NOT NULL,
    [USUARIO]           CHAR (20)                                                                        NOT NULL,
    [JUSTIFICACIONEGRE] VARCHAR (MAX)                                                                    NULL,
    [USUARIOEGRE]       CHAR (20)                                                                        NULL,
    [CODMOTIVOEGRE]     CHAR (4)                                                                         NULL,
    [FECHAEGRE]         DATETIME                                                                         NULL,
    CONSTRAINT [PK_RIASXPACIENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RIASXPACIENTE_HCMOANULB] FOREIGN KEY ([CODMOTIVOEGRE]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_RIASXPACIENTE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_RIASXPACIENTE_RIAS] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RIASXPACIENTE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_RIASXPACIENTE_IPCODPACI]
    ON [dbo].[RIASXPACIENTE]([IPCODPACI] ASC);


GO
ALTER INDEX [IX_RIASXPACIENTE_IPCODPACI]
    ON [dbo].[RIASXPACIENTE] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de egreso del paciente de la RIAS (Ruta Integral de Atención en Salud), momento en que se da por finalizada la atención integral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de egreso de la rias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de egreso de la RIAS (ej: alta médica, remisión, abandono, fallecimiento), referencia a tabla HCMOANULB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'CODMOTIVOEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del motivo por el cual egresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'CODMOTIVOEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'CODMOTIVOEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario/profesional que registró el egreso del paciente de la RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'USUARIOEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo usuario que egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'USUARIOEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'USUARIOEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo con la razón, justificación clínica o administrativa del egreso de la RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razón por la cual se egresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario/profesional que realizó la inscripción inicial del paciente en la RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'USUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realizo el registro   (usuario que inscribio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'USUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'USUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen o proceso que generó la inscripción en RIAS: 1=Dashboard RIAS, 2=Orden de servicios, 3=Agendamiento, 4=Historia clínica/Formulario demanda inducida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ORIGENINSCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso que inscribio:  1 - Dashboard RIAS  2 - Orden de servicios  3 - Agendamiento  4 - Desde la Historia Clinica (Formulario demanda Inducida)  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ORIGENINSCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ORIGENINSCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del registro RIAS del paciente: 1=No inscrito, 2=Inscrito/Activo, 3=Egresado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-No Inscrito   2-Inscrito   3-Egresado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inscripción del paciente en la RIAS (inicio de la ruta integral de atención).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAINCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inscrición a la RIAS ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAINCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAINCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente (cédula, pasaporte, documento de identidad), enmascarada como PII. FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la RIAS (Ruta Integral de Atención en Salud) relacionada, FK a tabla RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de RIAS, es el id de la rias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (primary key) del registro en la tabla RIASXPACIENTE, autonumérico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inscripciones de pacientes a programas de RIAS (Rutas Integrales de Atención en Salud). Registra qué pacientes están o estuvieron enrolados en cada ruta, cuándo ingresaron, su estado actual y los datos de egreso cuando aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE';
