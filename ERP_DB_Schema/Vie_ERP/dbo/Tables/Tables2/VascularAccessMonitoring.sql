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
ALTER TABLE [dbo].[VascularAccessMonitoring] NOCHECK CONSTRAINT [FK_VascularAccessMonitoring_ADCENATEN];


GO
ALTER TABLE [dbo].[VascularAccessMonitoring] NOCHECK CONSTRAINT [FK_VascularAccessMonitoring_CONSECUTI];


GO
ALTER TABLE [dbo].[VascularAccessMonitoring] NOCHECK CONSTRAINT [FK_VascularAccessMonitoring_INPROFSAL];


GO
ALTER TABLE [dbo].[VascularAccessMonitoring] NOCHECK CONSTRAINT [FK_VascularAccessMonitoring_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del profesional de la salud que ordenó seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad funcional de donde se ordena seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de atención de donde se ordena seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterio de seguimiento de acceso vascular: 1. Indicación de medicamentos intravenosos. 2. Hidratación intravenosa. 3. Soporte metabólico. 4. Líquidos endovenosos a mantenimiento. 5. Necesidad de emergencia de administración intravenosa. 6. Otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringContinuationCriteria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora en que se realiza seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'MonitoringDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo padre (CONSECUTI) de tabla de acceso vascular ó venopunción (HCCTRVENP).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id único del seguimiento de acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VascularAccessMonitoring', @level2type = N'COLUMN', @level2name = N'Id';

