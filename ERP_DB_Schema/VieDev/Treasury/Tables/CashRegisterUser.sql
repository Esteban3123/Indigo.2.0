CREATE TABLE [Treasury].[CashRegisterUser] (
    [Id]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashRegister] INT NOT NULL,
    [IdUser]         INT NOT NULL,
    CONSTRAINT [PK_CashRegisterUser__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashRegisterUser_CashRegister] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id])
);


GO
ALTER TABLE [Treasury].[CashRegisterUser] NOCHECK CONSTRAINT [FK_CashRegisterUser_CashRegister];

