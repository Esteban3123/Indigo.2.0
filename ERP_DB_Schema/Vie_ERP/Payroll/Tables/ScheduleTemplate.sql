CREATE TABLE [Payroll].[ScheduleTemplate] (
    [Id]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]    VARCHAR (3)  NOT NULL,
    [GroupId] INT          NOT NULL,
    [Letter]  VARCHAR (2)  NOT NULL,
    [Name]    VARCHAR (75) NULL,
    [State]   BIT          NOT NULL,
    CONSTRAINT [PK_ScheduleTemplate__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ShiftsTemplate_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleTemplate] NOCHECK CONSTRAINT [FK_ShiftsTemplate_Group];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ScheduleTemplate__Code]
    ON [Payroll].[ScheduleTemplate]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1 Activo, 0 Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplate', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la plantilla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplate', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Letra utilizada para identificar la plantilla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplate', @level2type = N'COLUMN', @level2name = N'Letter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplate', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de plantilla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplate', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplate', @level2type = N'COLUMN', @level2name = N'Id';

