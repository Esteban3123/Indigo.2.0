CREATE TABLE [Treasury].[CashRegisterUser] (
    [Id]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashRegister] INT NOT NULL,
    [IdUser]         INT NOT NULL,
    CONSTRAINT [PK_CashRegisterUser__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashRegisterUser_CashRegister] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id])
);


GO
ALTER TABLE [Treasury].[CashRegisterUser] NOCHECK CONSTRAINT [FK_CashRegisterUser_CashRegister];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CashRegisterUser__IdCashRegister__IdUser]
    ON [Treasury].[CashRegisterUser]([IdCashRegister] ASC, [IdUser] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario a autorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'IdUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la caja a autorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'IdCashRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'Id';

