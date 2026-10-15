CREATE TABLE [Scheduling].[AppointmentHemocomponents] (
    [Id]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAGASICITA]      INT            NOT NULL,
    [IdBloodComponent] INT            NOT NULL,
    [RequestType]      TINYINT        NOT NULL,
    [Quantity]         INT            NOT NULL,
    [VolumenComponent] DECIMAL (4, 1) NOT NULL,
    CONSTRAINT [PK_AppointmentHemocomponents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_Agasicita_idAgacisita] FOREIGN KEY ([IdAGASICITA]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [Fk_HCCOMSAN_id] FOREIGN KEY ([IdBloodComponent]) REFERENCES [dbo].[HCCOMSAN] ([ID])
);


GO
ALTER TABLE [Scheduling].[AppointmentHemocomponents] NOCHECK CONSTRAINT [Fk_Agasicita_idAgacisita];


GO
ALTER TABLE [Scheduling].[AppointmentHemocomponents] NOCHECK CONSTRAINT [Fk_HCCOMSAN_id];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen solicitado del componente sanguineo (ml)', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'VolumenComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad solicitada del componente sanguineo', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Solicitud: 1 - Solicitud de transfusión  2- Reserva y transfusión', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del Componente Sanguineo ---> HCCOMSAN', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdBloodComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cita asociada ---> AGASICITA (CODAUTONU)', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdAGASICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Id';

