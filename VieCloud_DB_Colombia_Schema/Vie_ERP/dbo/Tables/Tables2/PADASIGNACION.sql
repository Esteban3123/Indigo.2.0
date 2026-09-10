CREATE TABLE [dbo].[PADASIGNACION] (
    [ID]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PADCONTROLID]       INT           NULL,
    [TIPOATENCION]       TINYINT       NOT NULL,
    [AUTORIZACION]       VARCHAR (15)  NOT NULL,
    [NUMEROINGRESO]      CHAR (10)     NOT NULL,
    [ATENCIONAUTORIZA]   SMALLINT      CONSTRAINT [DF_PADASIGNACION_ATENCIONAUTORIZA] DEFAULT ((0)) NOT NULL,
    [ENFERMERIAAUTORIZA] SMALLINT      CONSTRAINT [DF_PADASIGNACION_ENFERMERIAAUTORIZA] DEFAULT ((0)) NOT NULL,
    [TERAPIAAUTORIZA]    SMALLINT      CONSTRAINT [DF_PADASIGNACION_TERAPIAAUTORIZA] DEFAULT ((0)) NOT NULL,
    [PSICOLOGIAAUTORIZA] SMALLINT      CONSTRAINT [DF_PADASIGNACION_PSICOLOGIAAUTORIZA] DEFAULT ((0)) NOT NULL,
    [ESTADO]             TINYINT       NOT NULL,
    [FECHAREGISTRO]      DATETIME      NOT NULL,
    [FECHAEGRESO]        DATETIME      NULL,
    [OBSERVACION]        VARCHAR (500) NULL,
    [ATENCIONREALIZA]    SMALLINT      CONSTRAINT [DF_PADASIGNACION_ATENCIONREALIZA] DEFAULT ((0)) NOT NULL,
    [ENFERMERIAREALIZA]  SMALLINT      CONSTRAINT [DF_PADASIGNACION_ENFERMERIAREALIZA] DEFAULT ((0)) NOT NULL,
    [TERAPIAREALIZA]     SMALLINT      CONSTRAINT [DF_PADASIGNACION_TERAPIAREALIZA] DEFAULT ((0)) NOT NULL,
    [PSICOLOGIAREALIZA]  SMALLINT      CONSTRAINT [DF_PADASIGNACION_PSICOLOGIAREALIZA] DEFAULT ((0)) NOT NULL,
    [MOTIVOEGRESO]       CHAR (4)      NULL,
    [USUARIOREGISTRA]    CHAR (20)     NOT NULL,
    [USUARIOEGRESO]      CHAR (20)     NULL,
    [NUMEROINGRESOORG]   CHAR (10)     NULL,
    [CODDIAGNO]          CHAR (4)      NULL,
    CONSTRAINT [PK_PADASIGNACION] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PADASIGNACION_ADINGRESO] FOREIGN KEY ([NUMEROINGRESO]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PADASIGNACION_HCMOANULB] FOREIGN KEY ([MOTIVOEGRESO]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_PADASIGNACION_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_PADASIGNACION_PADCONTROL] FOREIGN KEY ([PADCONTROLID]) REFERENCES [dbo].[PADCONTROL] ([ID])
);




GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_PADASIGNACION_MOTIVOEGRESO]
    ON [dbo].[PADASIGNACION]([MOTIVOEGRESO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PADASIGNACION_NUMEROINGRESO]
    ON [dbo].[PADASIGNACION]([NUMEROINGRESO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PADASIGNACION_PADCONTROLID]
    ON [dbo].[PADASIGNACION]([PADCONTROLID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10, referencia a tabla INDIAGNOS. Diagnóstico principal del paciente en la atención domiciliaria o hospitalización en casa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso en la solicitud de origen, registro administrativo del paciente en el sistema de admisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'NUMEROINGRESOORG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ingreso en la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'NUMEROINGRESOORG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'NUMEROINGRESOORG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/identificación) que realiza el egreso del programa PAD. Auditoría de quién registra la salida del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'USUARIOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien realiza el egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'USUARIOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'USUARIOEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/identificación) que realiza la asignación inicial al programa. Auditoría de quién crea el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien realiza la asignación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de motivo de egreso (tabla HCMOANULB). Razón por la cual el paciente se retira: mejoría, traslado, abandono, defunción, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'MOTIVOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de motivo de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'MOTIVOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'MOTIVOEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conteo SMALLINT de sesiones o atenciones de psicología efectivamente realizadas al momento del egreso. Métrica de cumplimiento PAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PSICOLOGIAREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de atenciones de psicología realizadas en el momento de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PSICOLOGIAREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PSICOLOGIAREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conteo SMALLINT de sesiones de terapia (física/ocupacional/del lenguaje) efectivamente realizadas al momento del egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TERAPIAREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de terapias realizadas en el momento de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TERAPIAREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TERAPIAREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conteo SMALLINT de atenciones de enfermería efectivamente realizadas al momento del egreso. Cuidados domiciliarios ejecutados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ENFERMERIAREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de atenciones de enfermeria realizadas en el momento de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ENFERMERIAREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ENFERMERIAREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conteo SMALLINT de atenciones médicas domiciliarias efectivamente realizadas al momento del egreso. Visitas del profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ATENCIONREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de atenciones médicas realizadas en el momento de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ATENCIONREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ATENCIONREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto libre (VARCHAR 500) con notas, hallazgos o comentarios registrados al momento del egreso del programa PAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se registra el egreso del paciente del programa de atención domiciliaria. NULL si aún activo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'FECHAEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'FECHAEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'FECHAEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se crea el registro de asignación al programa PAD. Auditoría de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado TINYINT del registro: 1=Asignada (activo en programa), 2=Egresado (salida registrada). Indica ciclo de vida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado, 1:Asignada, 2:Egresado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conteo SMALLINT de atenciones de psicología autorizadas por plan. Número máximo permitido al inicio de la asignación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PSICOLOGIAAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de atenciones de psicología autorizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PSICOLOGIAAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PSICOLOGIAAUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conteo SMALLINT de sesiones de terapia autorizadas por plan. Cuota aprobada para el período de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TERAPIAAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de terapias autorizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TERAPIAAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TERAPIAAUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conteo SMALLINT de atenciones de enfermería autorizadas por plan. Visitas enfermería permitidas en domicilio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ENFERMERIAAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de atenciones de enfermería autorizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ENFERMERIAAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ENFERMERIAAUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conteo SMALLINT de atenciones médicas autorizadas por plan. Número de consultas domiciliarias aprobadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ATENCIONAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de atenciones médicas autorizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ATENCIONAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ATENCIONAUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente (CHAR 10, FK a ADINGRESO). Identificador del acto administrativo de admisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'NUMEROINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'NUMEROINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'NUMEROINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización (VARCHAR 15) emitido por entidad pagadora. Referencia para facturación y RIPS del programa PAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'AUTORIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de autorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'AUTORIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'AUTORIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo TINYINT de modalidad: 1=Atención Domiciliaria, 2=Hospitalización en Casa. Define modelo de prestación del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de atencion, 1:Atencion domiciliaria, 2:Hospitalización en casa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de control PAD (INT, FK a PADCONTROL). Vínculo a seguimiento clínico y protocolos del programa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PADCONTROLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de control PAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PADCONTROLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PADCONTROLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY, clave primaria). Registro de asignación de paciente al programa PAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las asignaciones de pacientes en programas de atención domiciliaria o ambulatoria (PAD), incluyendo autorizaciones por área (atención médica, enfermería, terapia, psicología), sesiones autorizadas versus realizadas, estado del caso, fechas de registro y egreso, y motivo de salida del programa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION';
