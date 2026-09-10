CREATE TABLE [MedicalHistory].[AppointmentHemocomponents] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAGASICITA]        INT            NOT NULL,
    [IdBloodComponent]   INT            NOT NULL,
    [TypeBloodComponent] TINYINT        NOT NULL,
    [Quantity]           INT            NOT NULL,
    [VolumenComponent]   DECIMAL (4, 1) NOT NULL,
    CONSTRAINT [PK_AppointmentHemocomponents] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total en mililitros (ml) del componente hemático a transfundir; cantidad líquida expresada en DECIMAL(4,1) para precisión en hemoterapia y control de transfusión.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'VolumenComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el Volumen a transfundir (ml):', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'VolumenComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'VolumenComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de unidades de hemocomponentes requeridas; número entero de bolsas, concentrados o dosis del componente sanguíneo a transfundir.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda cantidad numero de unidades ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de solicitud hemática (TINYINT): 1=Solicitud de transfusión (pedido), 2=Reserva y transfusión (confirmado y ejecutado); estado o clasificación del trámite transfusional.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'TypeBloodComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el tipo de compones   1 = Solicitud de transfusión  2 =Reserva y transfusión ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'TypeBloodComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'TypeBloodComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del componente sanguíneo (FK); referencia el tipo específico de transfusión o hemocomponente (glóbulos rojos, plasma, plaquetas, etc.) solicitado o reservado.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdBloodComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id transfusion componentes sanguineos', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdBloodComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdBloodComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cita médica (FK); vincula el componente hemático a una cita, atención u agendamiento específico del paciente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdAGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de las  citas ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdAGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdAGASICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY INT) de cada registro de componente hemático en la cita; consecutivo secuencial de la tabla AppointmentHemocomponents.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los hemocomponentes (sangre, plasma, plaquetas, etc.) solicitados o asignados en una cita o atención médica. Relaciona cada cita con el tipo, cantidad y volumen del componente sanguíneo requerido.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents';
