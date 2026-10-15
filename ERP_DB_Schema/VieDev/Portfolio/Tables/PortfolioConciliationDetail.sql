CREATE TABLE [Portfolio].[PortfolioConciliationDetail] (
    [Id]                              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioConciliationId]         INT             NOT NULL,
    [InvoiceId]                       INT             NOT NULL,
    [AccountReceivableDate]           DATETIME        NOT NULL,
    [RadicatedNumber]                 INT             NOT NULL,
    [RadicatedDate]                   DATETIME        NOT NULL,
    [Balance]                         DECIMAL (20, 2) NOT NULL,
    [ValueGlosado]                    DECIMAL (20, 2) NOT NULL,
    [ValueEntity]                     DECIMAL (20, 2) NOT NULL,
    [PortfolioStatus]                 TINYINT         NOT NULL,
    [ValueAcceptedFirstInstance]      DECIMAL (20, 2) NOT NULL,
    [ValueAcceptedSecondInstance]     DECIMAL (20, 2) NOT NULL,
    [ValueGlosadoConciliation]        DECIMAL (20, 2) NOT NULL,
    [BalanceConciliation]             DECIMAL (20, 2) NOT NULL,
    [StateConciliation]               BIT             NOT NULL,
    [StatePortfolioConciliation]      TINYINT         NOT NULL,
    [Comment]                         VARCHAR (100)   NULL,
    [PortfolioConciliationConceptsId] INT             NULL,
    [PortfolioDifferenceConciliation] DECIMAL (20, 2) NULL,
    CONSTRAINT [PK_PortfolioConciliationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioConciliationDetail_ConciliationConcepts] FOREIGN KEY ([PortfolioConciliationConceptsId]) REFERENCES [Portfolio].[PortfolioConciliationConcepts] ([Id]),
    CONSTRAINT [FK_PortfolioConciliationDetail_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_PortfolioConciliationDetail_PortfolioConciliationId] FOREIGN KEY ([PortfolioConciliationId]) REFERENCES [Portfolio].[PortfolioConciliation] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioConciliationDetail] NOCHECK CONSTRAINT [FK_PortfolioConciliationDetail_ConciliationConcepts];


GO
ALTER TABLE [Portfolio].[PortfolioConciliationDetail] NOCHECK CONSTRAINT [FK_PortfolioConciliationDetail_Invoice];


GO
ALTER TABLE [Portfolio].[PortfolioConciliationDetail] NOCHECK CONSTRAINT [FK_PortfolioConciliationDetail_PortfolioConciliationId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diferencia saldo factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioDifferenceConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la relacion con la tabla PortfolioConciliationConcepts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioConciliationConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '      1 - Sin Radicar      2 - Radicada sin Confirmar      3 - Radicada Entidad      7 - Certificada Parcial      8 - Certificada Total      14 - Devolución Factura      15 - Cuenta de Dificil Recaudo      16 - Cobro Jurídico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'StatePortfolioConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' 0- sin conciliar                   1- conciliada', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'StateConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo de la factura que tiene pore lado del cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor glosado del cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosadoConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor aceptado segunda instancia', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor asignado en la primera instancia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado de cartera registrado en la entidad', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor factura entidad', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor glosado al corte', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'saldo al corte de la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la cuenta por Cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de relacion con saldos de facturas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de relacion con la cabecera de conciliacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioConciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de detalle de conciliacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Id';

