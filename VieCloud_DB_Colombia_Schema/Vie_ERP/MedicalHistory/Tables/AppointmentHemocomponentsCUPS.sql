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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, nullable) de descripción relacionada o identificación adicional; referencia a contexto clínico, nota o glosa asociada a la transfusión.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el Id descripción relacionada identificación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de carga o forma de cargo (TINYINT): indica categoría de facturación, procedencia o clasificación del hemocomponente en la cita.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'TypeLoad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda tipo de cargar', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'TypeLoad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'TypeLoad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del hemocomponente o procedimiento de transfusión (VARCHAR 100); denominación del CUP (ej: Concentrado de Glóbulos Rojos, Plasma Fresco Congelado).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'NameCUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el nombre del CUP', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'NameCUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'NameCUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (INT) del hemocomponente (sangre, plaquetas, plasma, glóbulos rojos); código de procedimiento de transfusión según tarifa de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'CUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda cup hemocomponentes', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'CUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'CUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cita de hemocomponentes; referencia a la atención de transfusión sanguínea o hemoterapia asociada.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdAppointmentHemocomponents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Id Cita Hemocomponentes', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdAppointmentHemocomponents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdAppointmentHemocomponents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) de la relación entre cita de hemocomponentes y código CUPS; consecutivo secuencial de la tabla.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los códigos CUPS (procedimientos) asociados a una solicitud o cita de hemocomponentes (sangre, plasma, plaquetas u otros derivados), permitiendo identificar qué procedimientos se facturan o reportan en RIPS para cada administración de hemocomponentes.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS';
