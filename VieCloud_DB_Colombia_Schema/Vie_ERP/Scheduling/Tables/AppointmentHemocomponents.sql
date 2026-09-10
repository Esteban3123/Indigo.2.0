CREATE TABLE [Scheduling].[AppointmentHemocomponents] (
    [Id]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAGASICITA]      INT            NOT NULL,
    [IdBloodComponent] INT            NOT NULL,
    [RequestType]      TINYINT        NOT NULL,
    [Quantity]         INT            NOT NULL,
    [VolumenComponent] DECIMAL (4, 1) NOT NULL,
    CONSTRAINT [PK_AppointmentHemocomponents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_HCCOMSAN_id] FOREIGN KEY ([IdBloodComponent]) REFERENCES [dbo].[HCCOMSAN] ([ID])
);


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total solicitado del componente sanguíneo en mililitros (ml). Decimal con precisión 4,1 para componentes fraccionados o derivados sanguíneos.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'VolumenComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen solicitado del componente sanguineo (ml)', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'VolumenComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'VolumenComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades solicitadas del componente sanguíneo. Número de bolsas, concentrados o dosis requeridas para el procedimiento o transfusión.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad solicitada del componente sanguineo', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de solicitud de transfusión: 1=Solicitud de transfusión; 2=Reserva y transfusión. Indica si es solo solicitud o incluye reserva de inventario para hemoterapia.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'RequestType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Solicitud: 1 - Solicitud de transfusión  2- Reserva y transfusión', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'RequestType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del componente sanguíneo solicitado (FK → HCCOMSAN.ID). Referencia el tipo de hemocomponente: glóbulos rojos, plasma, plaquetas, crioprecipitado, etc.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdBloodComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del Componente Sanguineo ---> HCCOMSAN', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdBloodComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdBloodComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cita médica asociada (FK → AGASICITA.CODAUTONU). Vincula el componente sanguíneo solicitado a una atención, ingreso o consulta específica.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdAGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cita asociada ---> AGASICITA (CODAUTONU)', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdAGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'IdAGASICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la asignación de componente sanguíneo a cita. Clave primaria de la tabla AppointmentHemocomponents.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de hemocomponentes (sangre, plasma, plaquetas, etc.) solicitados o asociados a una cita de hemoterapia. Guarda qué componente sanguíneo se requiere, el tipo de solicitud y la cantidad y volumen necesarios para cada turno o cita agendada.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponents';
