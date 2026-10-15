CREATE TABLE [Treasury].[Refunds] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)    NOT NULL,
    [IdCashRegister]      INT             NOT NULL,
    [FilingUnitId]        INT             NOT NULL,
    [InitialDate]         DATETIME        NULL,
    [FinalDate]           DATETIME        NOT NULL,
    [Value]               DECIMAL (18, 2) NOT NULL,
    [Detail]              VARCHAR (MAX)   NULL,
    [Refunded]            BIT             CONSTRAINT [DF_Refunds_Refunded] DEFAULT ((0)) NOT NULL,
    [BalanceCashRegister] DECIMAL (18, 2) CONSTRAINT [DF_Refunds_BalanceCashRegister] DEFAULT ((0.0)) NOT NULL,
    [Status]              TINYINT         NOT NULL,
    [CreationUser]        VARCHAR (20)    NOT NULL,
    [CreationDate]        DATETIME        NOT NULL,
    [ModificationUser]    VARCHAR (20)    NULL,
    [ModificationDate]    DATETIME        NULL,
    [TimeStamp]           ROWVERSION      NOT NULL,
    [ConfirmationUser]    VARCHAR (20)    NULL,
    [ConfirmationDate]    DATETIME        NULL,
    [AnnulmentUser]       VARCHAR (20)    NULL,
    [AnnulmentDate]       DATETIME        NULL,
    CONSTRAINT [PK_Refunds__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Refunds_CashRegisters] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_Refunds_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id])
);


GO
ALTER TABLE [Treasury].[Refunds] NOCHECK CONSTRAINT [FK_Refunds_CashRegisters];


GO
ALTER TABLE [Treasury].[Refunds] NOCHECK CONSTRAINT [FK_Refunds_FilingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del reembolso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Refunds', @level2type = N'COLUMN', @level2name = N'Detail';

