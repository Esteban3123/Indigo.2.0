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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del examen de cambio de contrato (TINYINT): 1=Evaluación del jefe completada, 2=Evaluación de Recursos Humanos completada. Indica etapa de aprobación en flujo de cambio contractual.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo de estado: 1-Segmento del jefe hecho, 2-Segmento de recursos humanos hecho', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de última modificación del registro de examen de contrato. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 50) que realizó la última modificación del examen de cambio de contrato. Trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación inicial del registro de examen de cambio de contrato. Auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 50) que creó el registro de examen de cambio de contrato. Trazabilidad de origen.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo textual (VARCHAR 300) de la decisión de Recursos Humanos sobre aprobación/rechazo de cambio contractual. Justificación de gestión humana.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'HumanResourcesApprovalReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de aprobación de Recursos Humanos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'HumanResourcesApprovalReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'HumanResourcesApprovalReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aprobación de Recursos Humanos (TINYINT nullable): 0=No aprueba, 1=Aprueba el cambio de contrato. Validación final por gestión humana.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'HumanResourcesApproval';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para indicar si el encargado de recursos humanos aprueba la modificación de contrato con las opciones: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'HumanResourcesApproval';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'HumanResourcesApproval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo/clasificación (VARCHAR 300) de sanciones disciplinarias aplicadas al empleado. Ej: amonestación, suspensión, otros.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinarySanctionsType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de sanciones disciplinarias de los empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinarySanctionsType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinarySanctionsType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (TINYINT nullable): 0=Sin sanciones disciplinarias, 1=Con sanciones disciplinarias vigentes. Estado disciplinario.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinarySanctions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para indicar si el empleado tiene sanciones disciplinarias con las opciones: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinarySanctions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinarySanctions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo (VARCHAR 300) por el cual el empleado se encuentra en proceso disciplinario. Justificación de procedimiento disciplinario.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinaryProcessReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo del proceso disciplinario de los empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinaryProcessReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinaryProcessReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (TINYINT nullable): 0=Sin proceso disciplinario, 1=En proceso disciplinario activo. Estado disciplinario del empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinaryProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para indicar si el empleado se encuentra en procesos disciplinarios con las opciones: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinaryProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesDisciplinaryProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios (VARCHAR 300) del supervisor/jefe directo sobre desempeño y comportamiento del empleado. Evaluación cualitativa.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la observación del jefe hacia el empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación (VARCHAR 300) de la decisión del jefe sobre aprobación/rechazo del cambio de contrato. Fundamentación supervisora.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossApprovalReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que indica la razón por la que fue aprobada o denegada por parte del jefe', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossApprovalReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossApprovalReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aprobación del jefe directo (TINYINT): 0=No aprueba, 1=Aprueba el cambio de contrato. Validación supervisora obligatoria.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossApproval';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para indicar si el jefe aprueba la modificación de contrato con las opciones: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossApproval';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'BossApproval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación (TINYINT) del desempeño laboral: 1=Excelente, 2=Sobresaliente, 3=Aceptable, 4=Insuficiente. Evaluación de productividad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesPerformance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para calificar el desempeño del empleado con las opciones: 1-Excelente, 2-Sobresaliente, 3-Aceptable, 4-Insuficiente', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesPerformance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesPerformance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación (TINYINT) de relaciones interpersonales del empleado (1-5): 1=Óptima, 5=Deficiente. Evaluación de convivencia laboral.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesRelations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para calificar los tipos de relaciones del empleado con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesRelations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeesRelations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación (TINYINT) de uso correcto de dotación y elementos de protección personal (1-5): 1=Óptimo, 5=Deficiente. Cumplimiento de seguridad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EndowmentsUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para calificar el uso de dotación y elementos de protección personal con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EndowmentsUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EndowmentsUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación (TINYINT) de cumplimiento de objetivos y actividades asignadas (1-5): 1=Óptimo, 5=Deficiente. Evaluación de metas.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'GoalsAchievement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para calificar el cumplimiento de objetivos y actividades con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'GoalsAchievement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'GoalsAchievement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación (TINYINT) de seguimiento y cumplimiento de normas y procedimientos (1-5): 1=Óptimo, 5=Deficiente. Adherencia normativa.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'StandardMonitoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para calificar el seguimiento de las normas y procedimientos con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'StandardMonitoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'StandardMonitoring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación (TINYINT) de cumplimiento de horarios y asistencia (1-5): 1=Óptimo, 5=Deficiente. Puntualidad y asistencia.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ScheduleCompliance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para calificar el cumplimiento de horarios con las opciones: 1,2,3,4,5', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ScheduleCompliance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'ScheduleCompliance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del empleado evaluado. Referencia a [Payroll].[Employee].[Id]. Código único del personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del  empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de examen de cambio de contrato. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluaciones de cambio de contrato (examen de período) para empleados del área de Talento Humano. Registra los criterios de desempeño, aprobación del jefe y de Recursos Humanos, así como procesos disciplinarios, para decidir la renovación o modificación del contrato laboral.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsChangeExam';
