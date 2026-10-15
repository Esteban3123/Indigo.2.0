CREATE TABLE [HumanTalent].[ContractsChangeExam] (
    [Id]                                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]                         INT           NOT NULL,
    [ScheduleCompliance]                 TINYINT       NOT NULL,
    [StandardMonitoring]                 TINYINT       NOT NULL,
    [GoalsAchievement]                   TINYINT       NOT NULL,
    [EndowmentsUse]                      TINYINT       NOT NULL,
    [EmployeesRelations]                 TINYINT       NOT NULL,
    [EmployeesPerformance]               TINYINT       NOT NULL,
    [BossApproval]                       TINYINT       NOT NULL,
    [BossApprovalReason]                 VARCHAR (300) NOT NULL,
    [BossObservation]                    VARCHAR (300) NOT NULL,
    [EmployeesDisciplinaryProcess]       TINYINT       NULL,
    [EmployeesDisciplinaryProcessReason] VARCHAR (300) NULL,
    [EmployeesDisciplinarySanctions]     TINYINT       NULL,
    [EmployeesDisciplinarySanctionsType] VARCHAR (300) NULL,
    [HumanResourcesApproval]             TINYINT       NULL,
    [HumanResourcesApprovalReason]       VARCHAR (300) NULL,
    [CreationUser]                       VARCHAR (50)  NOT NULL,
    [CreationDate]                       DATETIME      NOT NULL,
    [ModificationUser]                   VARCHAR (50)  NULL,
    [ModificationDate]                   DATETIME      NULL,
    [State]                              TINYINT       NULL,
    CONSTRAINT [PK_ContractsChangeExam] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractsChangeExam_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [HumanTalent].[ContractsChangeExam] NOCHECK CONSTRAINT [FK_ContractsChangeExam_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo de estado: 1-Segmento del jefe hecho, 2-Segmento de recursos humanos hecho', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de aprobación de Recursos Humanos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'HumanResourcesApprovalReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para indicar si el encargado de recursos humanos aprueba la modificación de contrato con las opciones: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'HumanResourcesApproval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de sanciones disciplinarias de los empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinarySanctionsType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para indicar si el empleado tiene sanciones disciplinarias con las opciones: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinarySanctions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo del proceso disciplinario de los empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinaryProcessReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para indicar si el empleado se encuentra en procesos disciplinarios con las opciones: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinaryProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la observación del jefe hacia el empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que indica la razón por la que fue aprobada o denegada por parte del jefe', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossApprovalReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para indicar si el jefe aprueba la modificación de contrato con las opciones: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossApproval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para calificar el desempeño del empleado con las opciones: 1-Excelente, 2-Sobresaliente, 3-Aceptable, 4-Insuficiente', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesPerformance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para calificar los tipos de relaciones del empleado con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesRelations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para calificar el uso de dotación y elementos de protección personal con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EndowmentsUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para calificar el cumplimiento de objetivos y actividades con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'GoalsAchievement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para calificar el seguimiento de las normas y procedimientos con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'StandardMonitoring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para calificar el cumplimiento de horarios con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ScheduleCompliance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del  empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'Id';

