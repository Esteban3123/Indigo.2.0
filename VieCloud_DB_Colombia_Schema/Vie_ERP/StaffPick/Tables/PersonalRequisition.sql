CREATE TABLE [StaffPick].[PersonalRequisition] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)  NULL,
    [DateRequest]                 DATETIME      NOT NULL,
    [OrganizationChartPositionId] INT           NOT NULL,
    [PositionId]                  INT           NOT NULL,
    [BranchOfficeId]              INT           NOT NULL,
    [FunctionalUnitId]            INT           NOT NULL,
    [CostCenterId]                INT           NOT NULL,
    [PositionImmediateBossId]     INT           NULL,
    [StarTimeWeek]                TIME (7)      NOT NULL,
    [EndTimeWeek]                 TIME (7)      NOT NULL,
    [StarTimeWeekend]             TIME (7)      NULL,
    [EndTimeWeekend]              TIME (7)      NULL,
    [Observation]                 VARCHAR (500) NULL,
    [Status]                      TINYINT       NOT NULL,
    [CreationUser]                VARCHAR (20)  NOT NULL,
    [CreationDate]                DATETIME      NOT NULL,
    [ModificationUser]            VARCHAR (20)  NULL,
    [ModificationDate]            DATETIME      NULL,
    CONSTRAINT [PK_PersonalRequisition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonalRequisition_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_PersonalRequisition_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PersonalRequisition_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_PersonalRequisition_OrganizationChartPosition] FOREIGN KEY ([OrganizationChartPositionId]) REFERENCES [HumanTalent].[OrganizationChartPosition] ([Id]),
    CONSTRAINT [FK_PersonalRequisition_OrganizationChartPosition1] FOREIGN KEY ([PositionImmediateBossId]) REFERENCES [HumanTalent].[OrganizationChartPosition] ([Id]),
    CONSTRAINT [FK_PersonalRequisition_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [UQ__Personal__A25C5AA777603657] UNIQUE NONCLUSTERED ([Code] ASC)
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de requisición de personal (DATETIME, auditoria)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación de la requisición (VARCHAR 20, auditoria, PII)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de requisición de personal (DATETIME, auditoria)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó la requisición de personal (VARCHAR 20, auditoria, PII)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la requisición: 1=Pendiente Aprobación, 2=Aprobado, 3=Rechazado (TINYINT, flujo aprobatorio)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (1. Pendiente Aprobacion, 2.Aprobado, 3.Rechazado)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y notas adicionales sobre la requisición de personal (VARCHAR 500, libre texto)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de salida/término laboral en fines de semana, parametrizado en posición del organigrama (TIME)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'EndTimeWeekend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Salida Fin de Semana (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'EndTimeWeekend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'EndTimeWeekend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio/ingreso laboral en fines de semana, parametrizado en posición del organigrama (TIME)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'StarTimeWeekend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicio Fines de Semana (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'StarTimeWeekend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'StarTimeWeekend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de salida/término laboral entre semana, parametrizado en posición del organigrama (TIME)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'EndTimeWeek';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Salida Entre Semana (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'EndTimeWeek';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'EndTimeWeek';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio/ingreso laboral entre semana, parametrizado en posición del organigrama (TIME)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'StarTimeWeek';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Ingreso Entre Semana (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'StarTimeWeek';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'StarTimeWeek';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la posición del jefe inmediato en el organigrama (INT, FK → OrganizationChartPosition)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionImmediateBossId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Posición Jefe Inmediato (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionImmediateBossId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionImmediateBossId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del centro de costo asociado a la requisición (INT, FK → Payroll.CostCenter, gestión financiera)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Centro de Costo (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la unidad funcional donde se requiere personal (INT, FK → Payroll.FunctionalUnit, estructura organizacional)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Unidad Funcional (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la sucursal o sede donde se requiere el personal (INT, FK → Payroll.BranchOffice, ubicación)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Sucursal (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del cargo o posición a ser cubierto por la requisición (INT, FK → Payroll.Position)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cargo (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la posición en Talento Humano/organigrama (INT, FK → HumanTalent.OrganizationChartPosition, estructura)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Posición de Talento Humano', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de solicitud/creación de la requisición de personal (DATETIME)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'DateRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Solicitud de fecha', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'DateRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'DateRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la requisición de personal, autogenerado (VARCHAR 20, UNIQUE, actualmente vacío)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Requisición de Personal (Dejar vacío por ahora)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único de la requisición de personal (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requisiciones de personal: solicitudes formales para cubrir una vacante o nueva posición dentro de la organización, incluyendo el cargo solicitado, la sede, la unidad funcional, el centro de costo, los horarios requeridos y el estado del proceso de selección.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition';
