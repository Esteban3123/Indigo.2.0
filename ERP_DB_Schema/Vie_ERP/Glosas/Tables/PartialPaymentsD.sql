CREATE TABLE [Glosas].[PartialPaymentsD] (
    [Id]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PartialPaymentsCId]       INT                                                                              NOT NULL,
    [PortfolioGlosaId]         INT                                                                              NULL,
    [InvoiceNumber]            VARCHAR (50)                                                                     NOT NULL,
    [InvoiceDate]              DATETIME                                                                         NOT NULL,
    [RadicatedNumber]          VARCHAR (50)                                                                     NOT NULL,
    [RadicatedDate]            DATETIME                                                                         NOT NULL,
    [PatientCode]              VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [PatientName]              VARCHAR (200) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NULL,
    [ContractCode]             VARCHAR (15)                                                                     NOT NULL,
    [ValuePendingConciliation] MONEY                                                                            NULL,
    [ValuePayments]            MONEY                                                                            NULL,
    [State]                    CHAR (1)                                                                         NOT NULL,
    [TimeStamp]                ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_PartialPaymentsD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PartialPaymentsD_GlosaPortfolioGlosada] FOREIGN KEY ([PortfolioGlosaId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id]),
    CONSTRAINT [FK_PartialPaymentsD_PartialPaymentsC] FOREIGN KEY ([PartialPaymentsCId]) REFERENCES [Glosas].[PartialPaymentsC] ([Id])
);


GO
ALTER TABLE [Glosas].[PartialPaymentsD] NOCHECK CONSTRAINT [FK_PartialPaymentsD_GlosaPortfolioGlosada];


GO
ALTER TABLE [Glosas].[PartialPaymentsD] NOCHECK CONSTRAINT [FK_PartialPaymentsD_PartialPaymentsC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1 sin confirmar    2 confirmado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Pago parcial total Aplicado a la factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ValuePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Pendiente de Conciliacion al momento de realizar el pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ValuePendingConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Radicacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de radicacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion de Cartera de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion de Cabecera del pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PartialPaymentsCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de detalle de factura de pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'Id';

