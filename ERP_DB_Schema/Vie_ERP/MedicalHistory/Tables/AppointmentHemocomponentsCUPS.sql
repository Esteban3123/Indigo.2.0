CREATE TABLE [MedicalHistory].[AppointmentHemocomponentsCUPS] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAppointmentHemocomponents] INT           NOT NULL,
    [CUP]                         INT           NOT NULL,
    [NameCUP]                     VARCHAR (100) NOT NULL,
    [TypeLoad]                    TINYINT       NOT NULL,
    [IdRelatedDescription]        INT           NULL,
    CONSTRAINT [PK_AppointmentHemocomponentsCUPS] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el Id descripción relacionada identificación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda tipo de cargar', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'TypeLoad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre del CUP', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'NameCUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda cup hemocomponentes', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'CUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Id Cita Hemocomponentes', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdAppointmentHemocomponents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'Id';

