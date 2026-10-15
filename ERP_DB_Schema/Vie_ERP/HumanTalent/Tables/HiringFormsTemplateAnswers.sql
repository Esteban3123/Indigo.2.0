CREATE TABLE [HumanTalent].[HiringFormsTemplateAnswers] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]            INT             NOT NULL,
    [HiringFormsTemplateId] INT             NOT NULL,
    [ActivityResult]        DECIMAL (18, 2) NULL,
    [Annexed]               VARCHAR (100)   NULL,
    [CreationUser]          VARCHAR (20)    NOT NULL,
    [CreationDate]          DATETIME        NOT NULL,
    [ModificationUser]      VARCHAR (20)    NULL,
    [ModificationDate]      DATETIME        NULL,
    CONSTRAINT [PK_HiringFormsTemplateAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HiringFormsTemplateAnswers_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_HiringFormsTemplateAnswers_HiringFormsTemplate] FOREIGN KEY ([HiringFormsTemplateId]) REFERENCES [HumanTalent].[HiringFormsTemplate] ([Id])
);


GO
ALTER TABLE [HumanTalent].[HiringFormsTemplateAnswers] NOCHECK CONSTRAINT [FK_HiringFormsTemplateAnswers_Employee];


GO
ALTER TABLE [HumanTalent].[HiringFormsTemplateAnswers] NOCHECK CONSTRAINT [FK_HiringFormsTemplateAnswers_HiringFormsTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringFormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringFormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringFormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que realizo el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringFormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para almacenar la ubicación del archivo o anexo de cada actividad realizada ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringFormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Annexed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para almacenar el porcentaje total del resultado de la actividad o plantilla realizada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringFormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ActivityResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del formulario o plantilla que el encargado del proceso le diligencia al candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringFormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'HiringFormsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla donde se almacena el Id de candidato, el id del proceso de selección y el id de empleado si es candidato interno.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringFormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HiringFormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Id';

