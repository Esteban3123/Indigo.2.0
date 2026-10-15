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
ALTER TABLE [HumanTalent].[TestPeriodExam] NOCHECK CONSTRAINT [FK_TestPeriodExam_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón  de la aprobación de los empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesApprovalReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para dar respuesta a la pregunta "¿Esta de acuerdo con las anotaciones que respecto a su desempeño ha evidenciado el evaluador?” Con las opciones de respuesta:  0-No  1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesApproval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para dar respuesta a la pregunta de información que haya afectado al empleado en su periodo de prueba  ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'PerformanceInformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información de las organizaciones Razón', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'OrganizationsInformationReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para dar respuesta a la pregunta "Al momento de ingresar a la empresa y ubicarse en su puesto de trabajo, recibió información clara y suficiente sobre la organización, sus funciones, jefe inmediato y horario laboral” Con las opciones de respuesta:  0-No  1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'OrganizationsInformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para ingresar las recomendaciones dirigidas al empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'Reccomendations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razon de la actitud de los empleados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesAptitudesReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para elegir las las actitudes y habilidades del (a) colaborador (a) demostradas durante el periodo. Las opciones son:  1. Cumple con las expectativas del cargo y aprueba el periodo de prueba. “  2. Aprueba el periodo de prueba, pero debe continuar con un plan de mejoramiento que contribuya a su adaptación al cargo.   3. No se evidenció su adaptación al puesto de trabajo, por ende, no aprueba el periodo de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesAptitudes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para almacenar la razon de la pregunta sobre el aprendizaje del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesLearningReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para dar respuesta a la pregunta ¿Durante este periodo, se observa facilidad, capacidad y disposición por parte de la persona para aprender las funciones de su cargo y la forma como se desarrollan en la empresa?” Con las opciones de respuesta:  0-No  1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeesLearning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TestPeriodExam', @level2type = N'COLUMN', @level2name = N'Id';

