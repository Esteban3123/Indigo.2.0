CREATE TABLE [dbo].[HCPCECONTROL] (
    [ID]                    INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]             CHAR (10)                                                                     NOT NULL,
    [FECHACREA]             DATETIME                                                                      NOT NULL,
    [CODUSUCREA]            CHAR (20)                                                                     NOT NULL,
    [CODPLANCENF]           VARCHAR (3)                                                                   NULL,
    [OBSERVACION]           VARCHAR (500)                                                                 NULL,
    [UFUCODIGO]             CHAR (10)                                                                     NOT NULL,
    [CODPROSAL]             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE]             CHAR (10)                                                                     NOT NULL,
    [ESTADO]                INT                                                                           NULL,
    [CODPROSALFINALIZO]     CHAR (10)                                                                     NULL,
    [FECHAFINALIZO]         DATETIME                                                                      NULL,
    [CODPROSALSUSPENDIO]    CHAR (10)                                                                     NULL,
    [FECHASUSPENDIO]        DATETIME                                                                      NULL,
    [CODPROSALCAMBIOESTADO] CHAR (20)                                                                     NULL,
    [FECHACAMBIOESTADO]     DATETIME                                                                      NULL,
    CONSTRAINT [PK_HCPCECONTROL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPCECONTROL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPCECONTROL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPCECONTROL_ADINGRESO2] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPCECONTROL_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPCECONTROL_INPROFSAL2] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPCECONTROL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPCECONTROL_INUNIFUNC2] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPCECONTROL].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_HCPCECONTROL_ESTADO]
    ON [dbo].[HCPCECONTROL]([ESTADO] ASC)
    INCLUDE([CODPLANCENF], [CODPROSAL]);


GO
CREATE NONCLUSTERED INDEX [IX_HCPCECONTROL_NUMINGRES_ESTADO_CODPLANCENF]
    ON [dbo].[HCPCECONTROL]([NUMINGRES] ASC, [ESTADO] ASC, [CODPLANCENF] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del cambio de estado del control de plan de cuidado enfermero (DATETIME). Registra cuándo se modificó el estado (activo, suspendido, finalizado, anulado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHACAMBIOESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha cambio de estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHACAMBIOESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHACAMBIOESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (enfermero/médico) que realizó el cambio de estado del plan. VARCHAR(20), referencia a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALCAMBIOESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código usuario profesional cambio estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALCAMBIOESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALCAMBIOESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de suspensión del plan de cuidado enfermero (DATETIME). Registra cuándo se pausó o interrumpió temporalmente la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHASUSPENDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha de suspención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHASUSPENDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHASUSPENDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que suspendió el plan de cuidado. CHAR(10), referencia a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALSUSPENDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código usuario profesional suspendido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALSUSPENDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALSUSPENDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización del plan de cuidado enfermero (DATETIME). Registra cuándo se completó o cerró la intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha de finalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que finalizó el plan de cuidado. CHAR(10), referencia a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALFINALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional quien finalizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALFINALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALFINALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del plan de cuidado enfermero (INT): 1=Activo, 2=Suspendido, 3=Finalizado, 4=Anulado por continuidad del plan. Indica el ciclo de vida del control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Activo  2 - Suspendido  3 - Finalizado  4 - Anulado por continuidad del plan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se ejecuta el plan de cuidado (CHAR(10)). Referencia FK a ADCENATEN. Identifica sede, clínica o institución prestadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CODCENATE Codigo del Centro de Atencion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud responsable del plan de cuidado (enfermero/médico). CHAR(20), PII Identification_Ofuscado, FK a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se registra el plan de cuidado (CHAR(10)). FK a INUNIFUNC. Identifica departamento, piso, servicio o área clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anotaciones, notas y hallazgos clínicos sobre el plan de cuidado enfermero (VARCHAR(500)). Campo libre para comentarios del profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del plan de cuidado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de cuidado enfermero asociado (VARCHAR(3)). Identifica el tipo o template de intervención estandarizada en cuidados de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del plan de cuidado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario del sistema que creó el registro de control (CHAR(20)). Referencia al profesional o administrativo que inició el plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de control en el sistema (DATETIME). Marca temporal de inicio del seguimiento del plan de cuidado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de la creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso del paciente (CHAR(10)). FK a ADINGRESO. Vincula el plan de cuidado a la atención, consulta o hospitalización específica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de control del plan de cuidado enfermero (INT IDENTITY). Clave primaria de la tabla HCPCECONTROL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de control de planes de cuidado de enfermería por ingreso hospitalario. Permite hacer seguimiento del estado de cada plan (creación, suspensión y finalización), identificando los profesionales de salud involucrados y la unidad funcional responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL';
