CREATE TABLE [dbo].[PostpartumCareFollowUp] (
    [ID]                 INT           IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]          VARCHAR (25)  NOT NULL,
    [NUMINGRES]          CHAR (10)     NOT NULL,
    [CODCENATE]          CHAR (10)     NOT NULL,
    [UFUCODIGO]          CHAR (10)     NOT NULL,
    [CODPROSAL]          CHAR (20)     NOT NULL,
    [FECREGSIS]          DATETIME      NOT NULL,
    [RegisterDate]       DATETIME      NOT NULL,
    [ProcedurePerformed] INT           NOT NULL,
    [ObstetricEvent]     VARCHAR (100) NULL,
    [ProcedureDate]      DATETIME      NOT NULL,
    [RedCode]            BIT           NULL,
    [RedCodeHour]        TIME (7)      NULL,
    [FollowUpMinute]     INT           NULL,
    [MomentOfAssessment] TINYINT       NULL,
    CONSTRAINT [PK_PostpartumCareFollowUp] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PostpartumCareFollowUp_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PostpartumCareFollowUp_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PostpartumCareFollowUp_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_PostpartumCareFollowUp_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_PostpartumCareFollowUp_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[PostpartumCareFollowUp].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[PostpartumCareFollowUp].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento de valoración posparto: 1=Inmediato, 2=Vigilancia. TINYINT, determina fase de evaluación clínica materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'MomentOfAssessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Momento de valoración
1 - Inmediato
2 - Vigilancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'MomentOfAssessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'MomentOfAssessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Minuto del seguimiento posparto. INT, registro temporal en minutos durante control postparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'FollowUpMinute';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Minuto del seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'FollowUpMinute';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'FollowUpMinute';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora del código rojo (emergencia obstétrica). TIME, timestamp de activación de alerta crítica materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RedCodeHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del código rojo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RedCodeHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RedCodeHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código rojo activado. BIT, indicador de emergencia obstétrica o complicación que requiere intervención urgente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RedCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código rojo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RedCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RedCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del procedimiento obstétrico realizado. DATETIME, marca momento de ejecución del acto quirúrgico o intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ProcedureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ProcedureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ProcedureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evento obstétrico reportado (campo Cuál del formulario). VARCHAR(100), descripción de complicación o hallazgo durante posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ObstetricEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evento obstétrico (Campo Cual del formulario)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ObstetricEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ObstetricEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento realizado. INT, identificador de intervención obstétrica ejecutada durante ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ProcedurePerformed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Procedimiento realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ProcedurePerformed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ProcedurePerformed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro del formulario. DATETIME, marca administrativo de cuando se documentó el seguimiento posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RegisterDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RegisterDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RegisterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de grabación en servidor. DATETIME, timestamp auditoría del sistema al persistir el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en la cual se guarda el registro (fecha servidor)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud. VARCHAR(25) PII enmascarado, identifica médico/enfermera que realizó valoración posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional. CHAR(10), area clínica (ginecoobstetricia, piso posparto, UCI materna) donde se registra seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención. CHAR(10), institución/hospital donde se atiende la paciente en posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso. CHAR(10), equivalente a admisión hospitalaria del parto, referencia a ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente. VARCHAR(25) PII enmascarado, cédula/documento de la madre en seguimiento posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la tabla. INT identity, clave primaria única del registro de seguimiento posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de seguimiento en la atención posparto: guarda los procedimientos realizados, eventos obstétricos y valoraciones de control tras el parto, asociados a cada paciente, ingreso y profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp';
