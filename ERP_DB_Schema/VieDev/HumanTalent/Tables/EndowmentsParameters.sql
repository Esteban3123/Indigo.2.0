CREATE TABLE [HumanTalent].[EndowmentsParameters] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GarmentsType]     TINYINT      NOT NULL,
    [MinSalary]        INT          NOT NULL,
    [Returnable]       TINYINT      NOT NULL,
    [Anticipated]      TINYINT      NOT NULL,
    [MinAntiquity]     INT          NOT NULL,
    [CreationUser]     VARCHAR (50) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (50) NULL,
    [ModificationDate] DATETIME     NULL,
    [MinimumSalaryId]  INT          NULL,
    CONSTRAINT [PK_EndowmentsParameters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EndowmentsParameters_MinimumSalary] FOREIGN KEY ([MinimumSalaryId]) REFERENCES [Payroll].[MinimumSalary] ([Id])
);


GO
ALTER TABLE [HumanTalent].[EndowmentsParameters] NOCHECK CONSTRAINT [FK_EndowmentsParameters_MinimumSalary];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del salario mínimo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinimumSalaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para el minimo de antigüedad(meses)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinAntiquity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de selección por si la prenda se puede entregar con anticipación: 0=No, 1=Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Anticipated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de selección por si la prenda es de tipo devolutivo: 0=No, 1=Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Returnable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario minimo legal mensual vigente', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de prenda: 1=Vestido, 2=Calzado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'GarmentsType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Id';

