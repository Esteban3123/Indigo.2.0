CREATE TABLE [Payroll].[MinimumSalary] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]               INT          NOT NULL,
    [LegalMinimumSalary] NUMERIC (18) NOT NULL,
    [TransportHelpValue] NUMERIC (18) NOT NULL,
    [State]              BIT          NOT NULL,
    [CreationUser]       VARCHAR (20) NOT NULL,
    [CreationDate]       DATETIME     NOT NULL,
    [ModificationUser]   VARCHAR (20) NULL,
    [ModificationDate]   DATETIME     NULL,
    CONSTRAINT [PK_MinimumSlary] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_MinimumSalary_Year] UNIQUE NONCLUSTERED ([Year] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'MinimumSalary', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'MinimumSalary', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'MinimumSalary', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'MinimumSalary', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1-Activo, 2-Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'MinimumSalary', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Auxilio de Transporte de la vigencia correspondiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'MinimumSalary', @level2type = N'COLUMN', @level2name = N'TransportHelpValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del salario mínimo de la vigencia correspondiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'MinimumSalary', @level2type = N'COLUMN', @level2name = N'LegalMinimumSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año de la vigencia correspondiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'MinimumSalary', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'MinimumSalary', @level2type = N'COLUMN', @level2name = N'Id';

