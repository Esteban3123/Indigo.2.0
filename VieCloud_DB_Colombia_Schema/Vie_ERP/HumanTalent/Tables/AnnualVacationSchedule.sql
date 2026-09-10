CREATE TABLE [HumanTalent].[AnnualVacationSchedule] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]            INT           NOT NULL,
    [InitialContractNumber] INT           NOT NULL,
    [ContractId]            INT           NOT NULL,
    [VacationPeriodId]      INT           NOT NULL,
    [VacationInitialDate]   DATE          NOT NULL,
    [VacationEndDate]       DATE          NOT NULL,
    [IncorporationDate]     DATE          NOT NULL,
    [RequestDays]           TINYINT       NOT NULL,
    [Comments]              VARCHAR (500) NOT NULL,
    [Status]                TINYINT       NOT NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    CONSTRAINT [PK_AnnualVacationSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualVacationSchedule_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_AnnualVacationSchedule_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_AnnualVacationSchedule_VacationPeriod] FOREIGN KEY ([VacationPeriodId]) REFERENCES [Payroll].[VacationPeriod] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Modificación. Timestamp DATETIME que registra cuándo se actualizó por última vez el registro de programación de vacaciones del empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Modificación. VARCHAR(20) que identifica al usuario del sistema que realizó la última actualización del cronograma vacacional.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Creación. Timestamp DATETIME que registra cuándo se creó el registro de solicitud o programación de vacaciones en el sistema.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creación. VARCHAR(20) que identifica al usuario del sistema que originalmente creó el registro de programación vacacional.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Programación de Vacaciones. TINYINT: 1=Programada (aprobada/vigente), 2=Cancelada (revocada/anulada). Controla el estado del cronograma.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  1 - Programada  2 - Cancelada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios. VARCHAR(500) para notas, observaciones o detalles adicionales sobre la solicitud, aprobación o cancelación de vacaciones.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días Solicitados. TINYINT que especifica la cantidad de días de vacaciones solicitados por el empleado en este período.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'RequestDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias Solicitados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'RequestDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'RequestDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Incorporación. DATE que marca cuándo el empleado retorna a labores después de terminar el período de vacaciones programado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'IncorporationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Incoporación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'IncorporationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'IncorporationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Fin de Vacaciones. DATE que define el último día del período de descanso vacacional del empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Fin de Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Inicio de Vacaciones. DATE que define el primer día del período de descanso vacacional programado del empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio de Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del Período de Vacaciones. INT FK a [Payroll].[VacationPeriod], referencia el período anual o definido para el cual se programa el descanso.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationPeriodId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del período de vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationPeriodId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationPeriodId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Contrato. INT FK a [Payroll].[Contract], referencia el contrato laboral activo del empleado al momento de la programación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Contrato Inicial. INT que guarda el número de contrato con el que inició la relación laboral del empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Contrato Inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del Empleado. INT FK a [Payroll].[Employee], referencia única al empleado titular de la programación de vacaciones.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Autonumérico. INT IDENTITY(1,1), clave primaria única que identifica cada registro de programación de vacaciones en la tabla.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cronograma anual de vacaciones del personal: registra las solicitudes y períodos de vacaciones programados para cada empleado, incluyendo fechas, días solicitados, contrato asociado y estado de aprobación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule';
