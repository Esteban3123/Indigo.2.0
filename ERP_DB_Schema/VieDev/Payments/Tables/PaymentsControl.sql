CREATE TABLE [Payments].[PaymentsControl] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentNumber] VARCHAR (100) NOT NULL,
    [DocumentType]   INT           NOT NULL,
    [DocumentUser]   VARCHAR (50)  NOT NULL,
    [DocumentDate]   DATETIME      NOT NULL,
    [Timestamp]      ROWVERSION    NOT NULL,
    CONSTRAINT [PK_PaymentsControl__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_PaymentsControl__DocumentType__DocumentNumber] UNIQUE NONCLUSTERED ([DocumentType] ASC, [DocumentNumber] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del documento   Cuenta por pagar = 1  Notas = 2  Anticipos = 3  Traslado = 4  Saldo Inicial = 5', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsControl', @level2type = N'COLUMN', @level2name = N'DocumentType';

