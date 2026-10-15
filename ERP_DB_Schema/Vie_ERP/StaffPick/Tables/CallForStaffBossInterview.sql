CREATE TABLE [StaffPick].[CallForStaffBossInterview] (
    [Id]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCallForStaff] INT NOT NULL,
    [IdEmployee]     INT NOT NULL,
    CONSTRAINT [PK_CallForStaffBossInterview] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CallForStaffBossInterview_CallForStaff] FOREIGN KEY ([IdCallForStaff]) REFERENCES [StaffPick].[CallForStaff] ([Id]),
    CONSTRAINT [FK_CallForStaffBossInterview_Employee] FOREIGN KEY ([IdEmployee]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [StaffPick].[CallForStaffBossInterview] NOCHECK CONSTRAINT [FK_CallForStaffBossInterview_CallForStaff];


GO
ALTER TABLE [StaffPick].[CallForStaffBossInterview] NOCHECK CONSTRAINT [FK_CallForStaffBossInterview_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id empleado', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'IdEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de llamada para el personal', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'IdCallForStaff';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'Id';

