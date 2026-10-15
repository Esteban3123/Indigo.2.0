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
ALTER TABLE [StaffPick].[PersonalRequisition] NOCHECK CONSTRAINT [FK_PersonalRequisition_BranchOffice];


GO
ALTER TABLE [StaffPick].[PersonalRequisition] NOCHECK CONSTRAINT [FK_PersonalRequisition_CostCenter];


GO
ALTER TABLE [StaffPick].[PersonalRequisition] NOCHECK CONSTRAINT [FK_PersonalRequisition_FunctionalUnit];


GO
ALTER TABLE [StaffPick].[PersonalRequisition] NOCHECK CONSTRAINT [FK_PersonalRequisition_OrganizationChartPosition];


GO
ALTER TABLE [StaffPick].[PersonalRequisition] NOCHECK CONSTRAINT [FK_PersonalRequisition_OrganizationChartPosition1];


GO
ALTER TABLE [StaffPick].[PersonalRequisition] NOCHECK CONSTRAINT [FK_PersonalRequisition_Position];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (1. Pendiente Aprobacion, 2.Aprobado, 3.Rechazado)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Salida Fin de Semana (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'EndTimeWeekend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicio Fines de Semana (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'StarTimeWeekend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Salida Entre Semana (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'EndTimeWeek';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Ingreso Entre Semana (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'StarTimeWeek';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Posición Jefe Inmediato (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionImmediateBossId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Centro de Costo (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Unidad Funcional (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Sucursal (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cargo (traer los parametrizados en la posición del organigrama)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Posición de Talento Humano', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solicitud de fecha', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'DateRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Requisición de Personal (Dejar vacío por ahora)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Id';

