CREATE TABLE [Admissions].[TrazabilidadPreTriage] (
    [Id]                   INT                                                                       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]            CHAR (20)                                                                 NOT NULL,
    [IPCODPACI]            VARCHAR (25)                                                              NOT NULL,
    [AttentionCenter]      CHAR (10)                                                                 NOT NULL,
    [FunctionalUnit]       CHAR (10)                                                                 NOT NULL,
    [GENCAREGROUP]         INT                                                                       NOT NULL,
    [GENCONENTITY]         INT                                                                       NOT NULL,
    [Age]                  INT                                                                       NOT NULL,
    [TypeAge]              INT                                                                       NOT NULL,
    [StateConsciousness]   INT                                                                       NOT NULL,
    [BloodPressure]        VARCHAR (7) MASKED WITH (FUNCTION = 'partial(0, "Pressure_Ofuscado", 0)') NOT NULL,
    [HeartRate]            INT                                                                       NOT NULL,
    [RespiratoryFrequency] INT                                                                       NOT NULL,
    [Temperature]          NUMERIC (18, 1)                                                           NOT NULL,
    [OxygenSaturation]     INT                                                                       NOT NULL,
    [Weight]               NUMERIC (18, 3)                                                           NOT NULL,
    [Size]                 INT                                                                       NOT NULL,
    [PopulationGroup]      INT                                                                       NOT NULL,
    [Prioritization]       INT                                                                       NOT NULL,
    [Observation]          VARCHAR (500)                                                             NULL,
    [InitialDate]          DATETIME                                                                  NOT NULL,
    [RegistrationDate]     DATETIME                                                                  NOT NULL,
    CONSTRAINT [PK_TrazabilidadPreTriage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CnetroAtencion_ADCENATEN] FOREIGN KEY ([AttentionCenter]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ConsecutivoUrgencias_ADCONTURG] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADCONTURG] ([CODCONCEC]),
    CONSTRAINT [FK_Entidad_Contract_HealthAdministrator] FOREIGN KEY ([GENCONENTITY]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_GrupoAtencion_Contract_CareGroup] FOREIGN KEY ([GENCAREGROUP]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_Paciente_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_UnidadFuncional_INUNIFUNC] FOREIGN KEY ([FunctionalUnit]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Admissions].[TrazabilidadPreTriage].[BloodPressure]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Admissions].[TrazabilidadPreTriage].[HeartRate]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Admissions].[TrazabilidadPreTriage].[RespiratoryFrequency]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Admissions].[TrazabilidadPreTriage].[Temperature]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Admissions].[TrazabilidadPreTriage].[OxygenSaturation]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');




GO
CREATE NONCLUSTERED INDEX [IX_TrazabilidadPreTriage_CODCONCEC_IPCODPACI]
    ON [Admissions].[TrazabilidadPreTriage]([CODCONCEC] ASC, [IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro en el sistema del formulario PreTriage. Tipo: DATETIME. Marca cuándo se guardó el documento en la base de datos.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha registro del sistema', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial cuando comenzó la diligenciación del PreTriage. Tipo: DATETIME. Diferencia el inicio del triaje del momento de registro final.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Incial cuando se empezo a diligenciar el PreTriage', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones libres (varchar 500). Notas clínicas, hallazgos o comentarios del PreTriage que no se capturan en otros campos.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la observación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de priorización de urgencia: 1=Urgente (rojo), 2=Prioritario (amarillo), 3=Ninguna (verde). Tipo: INT. Guía la atención según severidad.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Prioritization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Priorización: Corresponderá a un campo de tipo lista de selección única con las siguientes opciones:    1 - Urgente, color rojo.  2 - Prioritario, color amarillo.  3 - Ninguna, color verde.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Prioritization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Prioritization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de población: grupo etario, vulnerabilidad o categoría demográfica del paciente. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'PopulationGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de población', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'PopulationGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'PopulationGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla o estatura en centímetros (cm). Tipo: INT. Medida antropométrica para valoración del estado físico.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Size';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla (cm)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Size';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso en kilogramos (kg). Tipo: NUMERIC(18,3). Medida antropométrica clave para cálculos de dosis y evaluación clínica.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso (kg)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saturación de oxígeno en sangre (%). Tipo: INT. Signo vital esencial; valores normales >95%. Indica oxigenación pulmonar.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'OxygenSaturation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saturación de oxígeno (%)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'OxygenSaturation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'OxygenSaturation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura corporal en grados Celsius (°C). Tipo: NUMERIC(18,1). Signo vital para detectar fiebre o hipotermia.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Temperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura (°C)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Temperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Temperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia respiratoria en respiraciones por minuto (rpm). Tipo: INT. Signo vital; rango normal 12-20 rpm en adultos.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'RespiratoryFrequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia respiratoria (rpm)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'RespiratoryFrequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'RespiratoryFrequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia cardiaca o pulso en pulsaciones por minuto (ppm). Tipo: INT. Signo vital; rango normal 60-100 ppm en reposo.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'HeartRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia cardiaca (ppm)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'HeartRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'HeartRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tensión arterial o presión sanguínea en milímetros de mercurio (mmHg). Tipo: VARCHAR(7) MASKED. PII ofuscado. Formato: sistólica/diastólica.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'BloodPressure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tensión arterial (mmHg)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'BloodPressure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'BloodPressure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de conciencia del paciente: 0=Alerta, 1=Confuso, 2=Comatoso, 3=Estuporoso, 4=Obnubilado, 5=Otro. Tipo: INT. Evalúa nivel de consciencia.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'StateConsciousness';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de conciencia  0 - Alerta  1 - Confuso  2 - Comtoso  3 - Estuporoso  4 - Obnubilado  5 - Otro  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'StateConsciousness';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'StateConsciousness';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad de edad: 1=Años, 2=Meses, 3=Días. Tipo: INT. Especifica la unidad en que se registra el campo Age.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'TypeAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de edad  1 - Años  2 -  Meses  3 - Dias', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'TypeAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'TypeAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del paciente en la unidad indicada por TypeAge (años, meses o días). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Age';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Age';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Age';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad administradora de salud (EPS, aseguradora). Tipo: INT. FK a Contract.HealthAdministrator.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entidad administradora', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención o línea asistencial. Tipo: INT. FK a Contract.CareGroup.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio, área, departamento). Tipo: CHAR(10). Ubica dónde se realizó el PreTriage.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (sede, clínica, hospital). Tipo: CHAR(10). FK a ADCENATEN. Ubicación física de la atención.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'AttentionCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'AttentionCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'AttentionCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente, equivalente a cédula, documento de identidad o número de identificación. Tipo: VARCHAR(25). PII. FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de urgencia o atención. Tipo: CHAR(20). FK a ADCONTURG. Enlaza con el registro de consulta de urgencia asociada.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla ADCONTURG  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (Primary Key). Tipo: INT IDENTITY(1,1). Número secuencial autoincrementado de cada registro PreTriage.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de trazabilidad del pre-triage de pacientes en urgencias: captura los signos vitales, datos clínicos iniciales y la priorización asignada a cada paciente antes de la clasificación formal de triage, permitiendo hacer seguimiento del proceso de atención temprana.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage';
