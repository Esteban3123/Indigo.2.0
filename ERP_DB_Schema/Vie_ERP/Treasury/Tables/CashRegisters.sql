CREATE TABLE [Treasury].[CashRegisters] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)    NOT NULL,
    [Name]                   VARCHAR (100)   NOT NULL,
    [Type]                   TINYINT         NOT NULL,
    [FilingUnitId]           INT             NULL,
    [Prefix]                 VARCHAR (4)     NOT NULL,
    [InitialBalance]         DECIMAL (18, 2) NOT NULL,
    [InitialDate]            DATETIME        NOT NULL,
    [RefundDate]             DATETIME        NULL,
    [AmountMax]              DECIMAL (18)    NOT NULL,
    [AmountMin]              DECIMAL (18)    NOT NULL,
    [CurrentBalance]         DECIMAL (18, 2) NOT NULL,
    [IdMainAccount]          INT             NOT NULL,
    [IdCostCenter]           INT             NULL,
    [ThirdPartyId]           INT             NULL,
    [IsMovement]             BIT             NOT NULL,
    [Status]                 BIT             CONSTRAINT [DF_CashRegister_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]           VARCHAR (20)    NOT NULL,
    [CreationDate]           DATETIME        NOT NULL,
    [ModificationUser]       VARCHAR (20)    NULL,
    [ModificationDate]       DATETIME        NULL,
    [TimeStamp]              ROWVERSION      NOT NULL,
    [CurrencyId]             INT             NULL,
    [BalanceLastRevaluation] DECIMAL (18, 2) CONSTRAINT [DF__CashRegis__Balan__1CA37094] DEFAULT ((0)) NOT NULL,
    [PeriodLastRevaluation]  INT             CONSTRAINT [DF__CashRegis__Perio__1D9794CD] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CashRegisters__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashRegister_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CashRegister_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CashRegisters_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CashRegisters_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_CashRegisters_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegister_CostCenter];


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegister_MainAccount];


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegisters_Currency];


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegisters_FilingUnit];


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegisters_ThirdParty];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CashRegisters__Code]
    ON [Treasury].[CashRegisters]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para almacenar el ultimo periodo que fue revalorizada la caja, el formato es YYYYmm  ejemplo: 202301', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'PeriodLastRevaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo de la última revalorización.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'BalanceLastRevaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo que indica que la caja ya tiene movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'IsMovement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tercero (aparece si la cuenta contable maneja tercero)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantia disponible', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'CurrentBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantia minima', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'AmountMin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantia maxima', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'AmountMax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de reembolso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'RefundDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo inicial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'InitialBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el prefijo que se deberia usar cuando se realiza un recibo de caja o un egreso con la caja especifica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de radicacion, esta solo se solicita si la caja es Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de caja: 1-Menor, 2-Mayor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'Id';

