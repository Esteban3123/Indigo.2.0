CREATE TABLE [Portfolio].[RadicateInvoiceD] (
    [Id]                          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RadicateInvoiceCId]          INT                                                                              NOT NULL,
    [GlosasParametersInterfaceId] INT                                                                              NULL,
    [InvoiceDocumentType]         TINYINT                                                                          NULL,
    [InvoiceNumber]               VARCHAR (50)                                                                     NOT NULL,
    [RadicatedNumber]             VARCHAR (50)                                                                     NULL,
    [RadicatedDate]               DATETIME                                                                         NULL,
    [InvoiceValueEntity]          MONEY                                                                            NULL,
    [InvoiceValuePacient]         MONEY                                                                            NULL,
    [BalanceInvoice]              MONEY                                                                            NOT NULL,
    [UserNameInvoice]             VARCHAR (200)                                                                    NULL,
    [IngressNumber]               VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Value_Ofuscado", 0)')          NOT NULL,
    [IngressDate]                 DATETIME                                                                         NULL,
    [AccountantAccountCustomers]  VARCHAR (15)                                                                     NOT NULL,
    [PatientCode]                 VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PatientName]                 VARCHAR (200) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NULL,
    [ContractCode]                VARCHAR (20)                                                                     NULL,
    [PlanCode]                    VARCHAR (20)                                                                     NULL,
    [ContractEntity]              VARCHAR (20)                                                                     NULL,
    [InvoiceDate]                 DATETIME                                                                         NOT NULL,
    [CreditNoteValue]             MONEY                                                                            CONSTRAINT [DF_RadicateInvoiceD_CreditNoteValue] DEFAULT ((0)) NULL,
    [DebitNoteValue]              MONEY                                                                            CONSTRAINT [DF_RadicateInvoiceD_DebitNoteValue] DEFAULT ((0)) NULL,
    [Devolution]                  BIT                                                                              NULL,
    [ConceptDevolution]           VARCHAR (500)                                                                    NULL,
    [State]                       CHAR (1)                                                                         NULL,
    [TimeStamp]                   ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_RadicateInvoiceD__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RadicateInvoiceD_GlosasParametersInterface] FOREIGN KEY ([GlosasParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id]),
    CONSTRAINT [FK_RadicateInvoiceD_RadicateInvoiceC] FOREIGN KEY ([RadicateInvoiceCId]) REFERENCES [Portfolio].[RadicateInvoiceC] ([Id])
);


GO
ALTER TABLE [Portfolio].[RadicateInvoiceD] NOCHECK CONSTRAINT [FK_RadicateInvoiceD_GlosasParametersInterface];


GO
ALTER TABLE [Portfolio].[RadicateInvoiceD] NOCHECK CONSTRAINT [FK_RadicateInvoiceD_RadicateInvoiceC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Sin Confirmar  2. Confirmado  4. Anulado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de Documentos  1= Factura EAPB con Contrato  2= Factura EAPB Sin Contrato  3= Factura Particular  4= Factura Capitada   5= Control de Capitacion  6= Factura Basica  7= Factura de Venta de Productos  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceDocumentType';

