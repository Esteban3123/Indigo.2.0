CREATE TABLE [Payroll].[ContractType] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (50) NOT NULL,
    [TestPeriod]       BIT          NOT NULL,
    [Undefined]        BIT          NOT NULL,
    [JobBondingTypeId] TINYINT      NOT NULL,
    [SalaryType]       TINYINT      NOT NULL,
    [AutoRenew]        BIT          NOT NULL,
    [ContractClass]    TINYINT      NOT NULL,
    [SeveranceType]    TINYINT      NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ContractType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractType_JobBondingType] FOREIGN KEY ([JobBondingTypeId]) REFERENCES [Payroll].[JobBondingType] ([Id])
);


GO
ALTER TABLE [Payroll].[ContractType] NOCHECK CONSTRAINT [FK_ContractType_JobBondingType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del contrato 1 si 0 no', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo o  regimen de cesantias  1 - N/A No aplica  2 - Tradicional   3 - Tradicional mensual  4 - Consignado   5 - Consignado mensual  6 - Ley 33', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'SeveranceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clases de contratos   1 - Otros   2 - Aprendizaje   3 - Laboral Fijo   4 - Laboral Indefinido   5 - Aprendizaje Practica  6 - Practicas Universitarias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'ContractClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene habilitada la renovacion automatica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'AutoRenew';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Salario: 1 - Fijo   2 - Integral 3 - Variable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'SalaryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo vinculacion Trabajador', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'JobBondingTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indefinido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'Undefined';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo de prueba ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'TestPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del tipo de contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del tipo de contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla tipos de contratos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'Id';

