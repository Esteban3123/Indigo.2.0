CREATE TABLE [dbo].[HemodynamicPatientMonitoringD] (
    [Id]                                INT                                                                           IDENTITY (1, 1) NOT NULL,
    [IdHemodynamicPatientMonitoringC]   INT                                                                           NOT NULL,
    [IdHemodynamicMonitoringC]          INT                                                                           NOT NULL,
    [IdHemodynamicMonitoringVariablesC] INT                                                                           NOT NULL,
    [IdHemodynamicMonitoringVariablesD] INT                                                                           NULL,
    [Value]                             VARCHAR (MAX)                                                                 NULL,
    [CODPROSAL]                         CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI]                         CHAR (3)                                                                      NOT NULL,
    [MonitoringDate]                    DATETIME                                                                      NOT NULL,
    [CreationDate]                      DATETIME                                                                      NOT NULL,
    [ModificationDate]                  DATETIME                                                                      NULL,
    CONSTRAINT [PK_HemodynamicPatientMonitoringD] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HemodynamicPatientMonitoringD].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de monitoreo hemodinámico. DATETIME, auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de monitoreo hemodinámico del paciente. DATETIME, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó la toma del monitoreo hemodinámico (presión, frecuencia cardíaca, saturación). DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'MonitoringDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha del monitoreo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'MonitoringDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'MonitoringDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tres dígitos de la especialidad médica que realiza el monitoreo hemodinámico. CHAR(3), referencia a especialidades (cardiología, UCI, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, enfermero) que registra el monitoreo hemodinámico. CHAR(10), PII ofuscado. FK a profesionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico o alfanumérico del parámetro hemodinámico monitoreado (presión sistólica/diastólica, frecuencia cardíaca, gasto cardíaco, etc.). VARCHAR MAX.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el valor del monitoreo hemodinámico del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la variable hemodinamica secundaria (detalle D) asociada al registro. INT NULL, FK a definiciones de variables D.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guarda el id de las variables del monitoreo hemodinamico D', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la variable hemodinamica principal (detalle C) del monitoreo realizado. INT, FK a variables de monitoreo C.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guarda el id de las variables del monitoreo hemodinamico C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del evento de monitoreo hemodinámico C del cual depende este detalle. INT, FK a monitoreo principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guarda el id del monitoreo hemodinamico C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del monitoreo hemodinámico C del paciente al cual pertenece este registro detallado. INT, FK a monitoreo del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicPatientMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guarda el id del monitoreo hemodinamico C del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicPatientMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicPatientMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo (IDENTITY) del registro detallado de monitoreo hemodinámico del paciente. INT PRIMARY KEY.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros detallados de monitoreo hemodinámico de pacientes: guarda los valores medidos de cada variable hemodinámica (presión arterial, frecuencia cardíaca, gasto cardíaco, etc.) durante el seguimiento clínico, asociando cada medición al profesional de salud, la especialidad y la fecha en que fue tomada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD';
