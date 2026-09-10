CREATE TABLE [HumanTalent].[TestPeriodExam] (
    [Id]                             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]                     INT           NOT NULL,
    [EmployeesLearning]              TINYINT       NOT NULL,
    [EmployeesLearningReason]        VARCHAR (300) NOT NULL,
    [EmployeesAptitudes]             TINYINT       NOT NULL,
    [EmployeesAptitudesReason]       VARCHAR (300) NOT NULL,
    [Reccomendations]                VARCHAR (300) NOT NULL,
    [OrganizationsInformation]       TINYINT       NULL,
    [OrganizationsInformationReason] VARCHAR (300) NULL,
    [PerformanceInformation]         VARCHAR (300) NULL,
    [EmployeesApproval]              TINYINT       NULL,
    [CreationUser]                   VARCHAR (50)  NOT NULL,
    [CreationDate]                   DATETIME      NOT NULL,
    [ModificationUser]               VARCHAR (50)  NULL,
    [ModificationDate]               DATETIME      NULL,
    [EmployeesApprovalReason]        VARCHAR (300) NULL,
    CONSTRAINT [PK_TestPeriodExam] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TestPeriodExam_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o motivo de la aprobación/desaprobación del empleado en el período de prueba; texto descriptivo (VARCHAR 300) que explica la decisión del evaluador respecto al acuerdo del trabajador con las anotaciones de desempeño', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesApprovalReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razón  de la aprobación de los empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesApprovalReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesApprovalReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de evaluación del período de prueba; DATETIME que rastrea cuándo se actualizó la información del examen', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación del sistema que realizó la última actualización del registro; VARCHAR(50) para auditoría de cambios en la evaluación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de examen del período de prueba; DATETIME que marca el inicio de la evaluación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación del sistema que creó el registro de evaluación; VARCHAR(50) para trazabilidad de quién registró la evaluación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta binaria (0=No, 1=Sí) del empleado: ¿está de acuerdo con las anotaciones sobre su desempeño evidenciadas por el evaluador?; TINYINT nullable que refleja conformidad del trabajador', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesApproval';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para dar respuesta a la pregunta "¿Esta de acuerdo con las anotaciones que respecto a su desempeño ha evidenciado el evaluador?” Con las opciones de respuesta:  0-No  1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesApproval';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesApproval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información o eventos relevantes que hayan impactado el desempeño del empleado durante el período de prueba; VARCHAR(300) para documentar circunstancias que afectaron la adaptación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'PerformanceInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para dar respuesta a la pregunta de información que haya afectado al empleado en su periodo de prueba  ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'PerformanceInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'PerformanceInformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o detalle adicional sobre la respuesta a inducción organizacional; VARCHAR(300) nullable que complementa la evaluación de orientación inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'OrganizationsInformationReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información de las organizaciones Razón', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'OrganizationsInformationReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'OrganizationsInformationReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta binaria (0=No, 1=Sí) del empleado: ¿recibió información clara y suficiente sobre la organización, funciones, jefe inmediato y horario laboral al ingresar?; TINYINT nullable', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'OrganizationsInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para dar respuesta a la pregunta "Al momento de ingresar a la empresa y ubicarse en su puesto de trabajo, recibió información clara y suficiente sobre la organización, sus funciones, jefe inmediato y horario laboral” Con las opciones de respuesta:  0-No  1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'OrganizationsInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'OrganizationsInformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recomendaciones dirigidas al empleado para mejorar su desempeño, adaptación o continuidad en el cargo; VARCHAR(300) con sugerencias del evaluador', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'Reccomendations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para ingresar las recomendaciones dirigidas al empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'Reccomendations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'Reccomendations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o explicación de la evaluación de actitudes y habilidades del colaborador; VARCHAR(300) que fundamenta la clasificación de aptitudes demostradas', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesAptitudesReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razon de la actitud de los empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesAptitudesReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesAptitudesReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación de aptitudes/actitudes del colaborador (1=Cumple expectativas aprueba, 2=Aprueba pero requiere mejora, 3=No se adapta rechaza período); TINYINT para resultado de adaptación laboral', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesAptitudes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para elegir las las actitudes y habilidades del (a) colaborador (a) demostradas durante el periodo. Las opciones son:  1. Cumple con las expectativas del cargo y aprueba el periodo de prueba. “  2. Aprueba el periodo de prueba, pero debe continuar con un plan de mejoramiento que contribuya a su adaptación al cargo.   3. No se evidenció su adaptación al puesto de trabajo, por ende, no aprueba el periodo de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesAptitudes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesAptitudes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o explicación sobre la capacidad de aprendizaje observada en el empleado; VARCHAR(300) que detalla el análisis de facilidad y disposición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesLearningReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar la razon de la pregunta sobre el aprendizaje del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesLearningReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesLearningReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta binaria (0=No, 1=Sí) del evaluador: ¿se observa facilidad, capacidad y disposición del empleado para aprender funciones y procesos?; TINYINT evaluativo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesLearning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para dar respuesta a la pregunta ¿Durante este periodo, se observa facilidad, capacidad y disposición por parte de la persona para aprender las funciones de su cargo y la forma como se desarrollan en la empresa?” Con las opciones de respuesta:  0-No  1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesLearning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesLearning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del empleado en la nómina; INT FK hacia [Payroll].[Employee].[Id] que vincula la evaluación al trabajador', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de examen del período de prueba; INT IDENTITY(1,1) que identifica cada evaluación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados de la evaluación de periodo de prueba de los empleados, incluyendo calificaciones de aprendizaje, aptitudes, información organizacional y aprobación o no aprobación del periodo de prueba con sus respectivas justificaciones y recomendaciones.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam';
