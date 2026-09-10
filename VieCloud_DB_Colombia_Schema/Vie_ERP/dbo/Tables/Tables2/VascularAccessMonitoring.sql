CREATE TABLE [dbo].[VascularAccessMonitoring] (
    [Id]                             NUMERIC (18)                                                                  IDENTITY (1, 1) NOT NULL,
    [CONSECUTI]                      NUMERIC (18)                                                                  NOT NULL,
    [MonitoringDate]                 DATETIME                                                                      NOT NULL,
    [MonitoringContinuationCriteria] INT                                                                           NOT NULL,
    [MonitoringObservation]          VARCHAR (300)                                                                 NULL,
    [CODCENATE]                      CHAR (10)                                                                     NOT NULL,
    [UFUCODIGO]                      CHAR (10)                                                                     NOT NULL,
    [CODPROSAL]                      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_VascularAccessMonitoring] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VascularAccessMonitoring_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_VascularAccessMonitoring_CONSECUTI] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCCTRVENP] ([CONSECUTI]),
    CONSTRAINT [FK_VascularAccessMonitoring_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_VascularAccessMonitoring_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[VascularAccessMonitoring].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, enfermero, especialista) que ordena o autoriza el seguimiento de acceso vascular; identificación PII ofuscada en consultas; referencia a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional de la salud que ordenó seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional (servicio, área clínica, departamento) desde donde se ordena el seguimiento de acceso vascular; referencia a tabla INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional de donde se ordena seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de atención (institución, clínica, hospital) desde donde se ordena seguimiento de acceso vascular; referencia a tabla ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de atención de donde se ordena seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación clínica o anotación del seguimiento de acceso vascular (punción, catéter, fístula); hallazgos, signos de infección, complicaciones o estado general del sitio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de continuidad del seguimiento de acceso vascular: 1-Medicamentos intravenosos; 2-Hidratación IV; 3-Soporte metabólico; 4-Líquidos endovenosos mantenimiento; 5-Emergencia administración IV; 6-Otros motivos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringContinuationCriteria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio de seguimiento de acceso vascular: 1. Indicación de medicamentos intravenosos. 2. Hidratación intravenosa. 3. Soporte metabólico. 4. Líquidos endovenosos a mantenimiento. 5. Necesidad de emergencia de administración intravenosa. 6. Otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringContinuationCriteria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringContinuationCriteria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se realiza evaluación clínica del acceso vascular (catéter, fístula, punción venosa).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se realiza seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo padre (CONSECUTI) de registro de acceso vascular o venopunción procedimiento en tabla HCCTRVENP; número secuencial único del ingreso/atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo padre (CONSECUTI) de tabla de acceso vascular ó venopunción (HCCTRVENP).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del evento de monitoreo de acceso vascular; clave primaria de VascularAccessMonitoring.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id único del seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de seguimiento y monitoreo del acceso vascular de los pacientes, incluyendo criterios de continuación, observaciones clínicas y el profesional responsable de cada evaluación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring';
