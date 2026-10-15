CREATE TABLE [Payroll].[TradeUnionEmployee] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TradeUnionId] INT          NOT NULL,
    [EmployeeId]   INT          NOT NULL,
    [CreationUser] VARCHAR (20) NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    [TimeStamp]    ROWVERSION   NOT NULL,
    CONSTRAINT [PK_TradeUnionEmployee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TradeUnionEmployee_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_TradeUnionEmployee_TradeUnion] FOREIGN KEY ([TradeUnionId]) REFERENCES [Payroll].[TradeUnion] ([Id])
);


GO
ALTER TABLE [Payroll].[TradeUnionEmployee] NOCHECK CONSTRAINT [FK_TradeUnionEmployee_Employee];


GO
ALTER TABLE [Payroll].[TradeUnionEmployee] NOCHECK CONSTRAINT [FK_TradeUnionEmployee_TradeUnion];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnionEmployee', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnionEmployee', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnionEmployee', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnionEmployee', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del sindicato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnionEmployee', @level2type = N'COLUMN', @level2name = N'TradeUnionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnionEmployee', @level2type = N'COLUMN', @level2name = N'Id';

