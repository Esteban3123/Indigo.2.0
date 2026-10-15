CREATE TABLE [HumanTalent].[ContractProjections] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]          INT          NOT NULL,
    [ContractinitialDate] DATE         NOT NULL,
    [ContractEndingDate]  DATE         NOT NULL,
    [State]               TINYINT      NOT NULL,
    [CreationUser]        VARCHAR (20) NOT NULL,
    [CreationDate]        DATETIME     NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    CONSTRAINT [PK_ContractProjections] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractProjections_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [HumanTalent].[ContractProjections] NOCHECK CONSTRAINT [FK_ContractProjections_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de moficiacion del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractProjections', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractProjections', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractProjections', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractProjections', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1- Ejecutado, 2- No ejecutado y  3- Cancelado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractProjections', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractProjections', @level2type = N'COLUMN', @level2name = N'ContractEndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inical del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractProjections', @level2type = N'COLUMN', @level2name = N'ContractinitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractProjections', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractProjections', @level2type = N'COLUMN', @level2name = N'Id';

