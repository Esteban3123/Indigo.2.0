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
CREATE NONCLUSTERED INDEX [IX_TrazabilidadPreTriage_CODCONCEC_IPCODPACI]
    ON [Admissions].[TrazabilidadPreTriage]([CODCONCEC] ASC, [IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha registro del sistema', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Incial cuando se empezo a diligenciar el PreTriage', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la observación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Priorización: Corresponderá a un campo de tipo lista de selección única con las siguientes opciones:    1 - Urgente, color rojo.  2 - Prioritario, color amarillo.  3 - Ninguna, color verde.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Prioritization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de población', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'PopulationGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla (cm)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso (kg)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saturación de oxígeno (%)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'OxygenSaturation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura (°C)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Temperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia respiratoria (rpm)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'RespiratoryFrequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia cardiaca (ppm)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'HeartRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tensión arterial (mmHg)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'BloodPressure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de conciencia  0 - Alerta  1 - Confuso  2 - Comtoso  3 - Estuporoso  4 - Obnubilado  5 - Otro  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'StateConsciousness';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de edad  1 - Años  2 -  Meses  3 - Dias', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'TypeAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Age';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entidad administradora', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'AttentionCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla ADCONTURG  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TrazabilidadPreTriage', @level2type = N'COLUMN', @level2name = N'Id';

