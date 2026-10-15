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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el Volumen a transfundir (ml):', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'VolumenComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda cantidad numero de unidades ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo de compones   1 = Solicitud de transfusión  2 =Reserva y transfusión ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'TypeBloodComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id transfusion componentes sanguineos', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdBloodComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de las  citas ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdAGASICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Id';

