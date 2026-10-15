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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo o  regimen de cesantias  1 - N/A No aplica  2 - Tradicional   3 - Tradicional mensual  4 - Consignado   5 - Consignado mensual  6 - Ley 33', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'SeveranceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clases de contratos   1 - Otros   2 - Aprendizaje   3 - Laboral Fijo   4 - Laboral Indefinido   5 - Aprendizaje Practica  6 - Practicas Universitarias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractType', @level2type = N'COLUMN', @level2name = N'ContractClass';

