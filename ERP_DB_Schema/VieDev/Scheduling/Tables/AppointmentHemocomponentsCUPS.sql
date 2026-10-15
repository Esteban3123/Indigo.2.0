CREATE TABLE [Scheduling].[AppointmentHemocomponentsCUPS] (
    [Id]                          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAppointmentHemocomponents] INT       NOT NULL,
    [CODSERIPS]                   CHAR (20) NOT NULL,
    [TypeLoad]                    TINYINT   NOT NULL,
    [IdRelatedDescription]        INT       NULL,
    CONSTRAINT [PK_AppointmentHemocomponentsCUPS] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_AppointmentHemocomponents_id] FOREIGN KEY ([IdAppointmentHemocomponents]) REFERENCES [Scheduling].[AppointmentHemocomponents] ([Id]),
    CONSTRAINT [Fk_INCUPSIPS_CODSERIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [Scheduling].[AppointmentHemocomponentsCUPS] NOCHECK CONSTRAINT [Fk_AppointmentHemocomponents_id];


GO
ALTER TABLE [Scheduling].[AppointmentHemocomponentsCUPS] NOCHECK CONSTRAINT [Fk_INCUPSIPS_CODSERIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción relacionada del CUP asociado al componente sanguineo', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cargue: 1:Automatico, 2:Preguntar antes de cargar', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'TypeLoad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codserips del CUP asociado al componente sanguineo', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de los componentes sanguineos ---> AppointmentHemocomponents', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdAppointmentHemocomponents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'Id';

