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
ALTER TABLE [HumanTalent].[HiringDocumentsTransfer] NOCHECK CONSTRAINT [FK_HiringDocumentsTransfer_Employee];


GO
ALTER TABLE [HumanTalent].[HiringDocumentsTransfer] NOCHECK CONSTRAINT [FK_HiringDocumentsTransfer_Employee1];


GO
ALTER TABLE [HumanTalent].[HiringDocumentsTransfer] NOCHECK CONSTRAINT [FK_HiringDocumentsTransfer_HiringFormsTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de realización de la prueba: 0-Transferida 1-Realizada(Evaluador) 2-Terminada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que define si la transferencia es para un empleado regular o si para un jefe inmediato: 1-Empleado, 2-Jefe inmediato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'TransfersType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado al que se le está transfieriendo la prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'EmployeeToTransfer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para definir si es la prueba de cambio de cargo: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'ContractsChangeTemplate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para definir si es la prueba de periodo de prueba: 0-No, 1-Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'TestPeriodTemplate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla de evaluacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'HiringFormTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Empleado al que se le hará la prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringDocumentsTransfer', @level2type = N'COLUMN', @level2name = N'Id';

