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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha del monitoreo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'MonitoringDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el valor del monitoreo hemodinámico del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el id de las variables del monitoreo hemodinamico D', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el id de las variables del monitoreo hemodinamico C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el id del monitoreo hemodinamico C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el id del monitoreo hemodinamico C del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicPatientMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicPatientMonitoringD', @level2type = N'COLUMN', @level2name = N'Id';

