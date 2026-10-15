CREATE TABLE [Payroll].[EmployeeType] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [Name]                 VARCHAR (150) NOT NULL,
    [State]                BIT           NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [EmployeeClass]        CHAR (2)      CONSTRAINT [DF_EmployeeType_EmployeeClass] DEFAULT ('01') NOT NULL,
    [ContributorSubtypeId] INT           NULL,
    CONSTRAINT [PK_EmployeeType__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_EmployeeType__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de subtipo de colaborador', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeType', @level2type = N'COLUMN', @level2name = N'ContributorSubtypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define la clase de empleado:   01 - Dependiente,   02 - Servicio Doméstico,   04 - Madre Comunitaria,   12 - Aprendices del SENA en etapa lectiva,   18 - Funcionarios públicos sin tope máximo de ibc,  19 - Aprendices del SENA en etapa productiva,  21- Estudiantes de postgrado en salud,  22 - Profesor de establecimiento particular,  23 - Estudiantes aportes solo riesgos laborales,  30 - Dependiente entidades o universidades públicas con régimen especial en salud,  31 - Cooperados o pre cooperativas de trabajo asociado,   47 - Trabajador dependiente de entidad beneficiaria del sistema general de participacinoes - aportes patronales,  51 - Trabajador de tiempo parcial,  54 - Pre pensionado de entidad en liquidación,   56 - Pre pensionado con aporte voluntario a salud,  58 - Estudiantes de prácticas laborales en el sector público', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeType', @level2type = N'COLUMN', @level2name = N'EmployeeClass';

