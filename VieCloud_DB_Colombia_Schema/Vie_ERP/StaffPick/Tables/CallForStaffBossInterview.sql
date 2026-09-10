CREATE TABLE [StaffPick].[CallForStaffBossInterview] (
    [Id]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCallForStaff] INT NOT NULL,
    [IdEmployee]     INT NOT NULL,
    CONSTRAINT [PK_CallForStaffBossInterview] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CallForStaffBossInterview_CallForStaff] FOREIGN KEY ([IdCallForStaff]) REFERENCES [StaffPick].[CallForStaff] ([Id]),
    CONSTRAINT [FK_CallForStaffBossInterview_Employee] FOREIGN KEY ([IdEmployee]) REFERENCES [Payroll].[Employee] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado entrevistador o jefe directo (FK a Payroll.Employee). Clave única del profesional de salud o administrativo que realiza la entrevista en el proceso de selección de personal.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'IdEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id empleado', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'IdEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'IdEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la convocatoria o llamada de personal (FK a StaffPick.CallForStaff). Referencia a la vacante, posición abierta o requerimiento de contratación vinculado a esta entrevista con jefe.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'IdCallForStaff';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de llamada para el personal', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'IdCallForStaff';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'IdCallForStaff';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario autoincremental de la tabla. Clave única que registra cada instancia de entrevista jefe-candidato en el proceso de selección de personal.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de las convocatorias de personal en las que un jefe o supervisor participa como entrevistador. Relaciona cada convocatoria de selección con el empleado responsable de conducir la entrevista.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaffBossInterview';
