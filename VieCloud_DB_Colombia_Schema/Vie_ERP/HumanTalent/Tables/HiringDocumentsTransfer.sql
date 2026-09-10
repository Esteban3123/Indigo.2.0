CREATE TABLE [HumanTalent].[HiringDocumentsTransfer] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]              INT          NOT NULL,
    [HiringFormTemplateId]    INT          NULL,
    [TestPeriodTemplate]      TINYINT      NULL,
    [ContractsChangeTemplate] TINYINT      NULL,
    [EmployeeToTransfer]      INT          NOT NULL,
    [TransfersType]           TINYINT      NULL,
    [CreationUser]            VARCHAR (50) NOT NULL,
    [CreationDate]            DATETIME     NOT NULL,
    [ModificationUser]        VARCHAR (50) NULL,
    [ModificationDate]        DATETIME     NULL,
    [State]                   INT          NULL,
    CONSTRAINT [PK_HiringDocumentsTransfer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HiringDocumentsTransfer_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_HiringDocumentsTransfer_Employee1] FOREIGN KEY ([EmployeeToTransfer]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_HiringDocumentsTransfer_HiringFormsTemplate] FOREIGN KEY ([HiringFormTemplateId]) REFERENCES [HumanTalent].[HiringFormsTemplate] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de realización de la transferencia de prueba/evaluación: 0=Transferida, 1=Realizada (por evaluador), 2=Terminada. Indica el ciclo de vida del proceso de evaluación del empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de realización de la prueba: 0-Transferida 1-Realizada(Evaluador) 2-Terminada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de transferencia de documentos/prueba. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro. Nombre o login del modificador. Tipo VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de transferencia de documentos de contratación. Tipo DATETIME. Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de transferencia. Nombre o login del creador. Tipo VARCHAR(50). Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de transferencia de prueba/evaluación: define si va dirigida a empleado regular (1) o jefe inmediato (2). Tipo TINYINT. Categoriza el destinatario.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'TransfersType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que define si la transferencia es para un empleado regular o si para un jefe inmediato: 1-Empleado, 2-Jefe inmediato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'TransfersType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'TransfersType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (Id) del empleado que recibe la transferencia de prueba/evaluación. Clave foránea a [Payroll].[Employee]. Destinatario del proceso evaluativo.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'EmployeeToTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empleado al que se le está transfieriendo la prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'EmployeeToTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'EmployeeToTransfer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (0=No, 1=Si) que marca si esta transferencia es una evaluación de cambio de cargo/puesto. Tipo TINYINT. Tipo TINYINT. Distingue evaluaciones por cambio contractual.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ContractsChangeTemplate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para definir si es la prueba de cambio de cargo: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ContractsChangeTemplate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ContractsChangeTemplate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (0=No, 1=Si) que marca si esta transferencia es una evaluación de período de prueba/inducción. Tipo TINYINT. Evaluaciones de ingreso o validación inicial.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'TestPeriodTemplate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para definir si es la prueba de periodo de prueba: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'TestPeriodTemplate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'TestPeriodTemplate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la plantilla/formulario de evaluación o contratación asociado. Clave foránea a [HumanTalent].[HiringFormsTemplate]. Referencia al modelo de prueba.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'HiringFormTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla de evaluacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'HiringFormTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'HiringFormTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (Id) del empleado evaluado o que será sometido a la prueba. Clave foránea a [Payroll].[Employee]. Empleado origen de la transferencia.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Empleado al que se le hará la prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del registro de transferencia de documentos de contratación. Clave primaria. Tipo INT. Identificación de la tabla HiringDocumentsTransfer.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de transferencias de documentos de contratación entre empleados en el módulo de Talento Humano. Guarda qué plantillas de formularios, períodos de prueba y contratos se trasladan de un empleado a otro, junto con el tipo de transferencia y la trazabilidad de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer';
