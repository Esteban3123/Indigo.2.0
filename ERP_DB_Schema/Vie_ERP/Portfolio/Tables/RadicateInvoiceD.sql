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
CREATE NONCLUSTERED INDEX [IX_RadicateInvoiceD__InvoiceNumber]
    ON [Portfolio].[RadicateInvoiceD]([InvoiceNumber] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RadicateInvoiceD_ValidateDuplicates]
    ON [Portfolio].[RadicateInvoiceD]([RadicateInvoiceCId] ASC, [InvoiceNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RadicateInvoiceD__RadicateInvoiceCId__INC__InvoiceNumber__State]
    ON [Portfolio].[RadicateInvoiceD]([RadicateInvoiceCId] ASC)
    INCLUDE([InvoiceNumber], [State]);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-03
-- Description:	Validar que no se eliminen detalles de facturas en estado distinto a registrado
-- =============================================
CREATE TRIGGER [Portfolio].[tgg_ValidateDeleted]
   ON [Portfolio].[RadicateInvoiceD]
   AFTER DELETE
AS 
BEGIN
	SET NOCOUNT ON;

    IF EXISTS
	(
		SELECT 1
		FROM Portfolio.RadicateInvoiceC ri
		JOIN DELETED rid ON ri.Id = rid.RadicateInvoiceCId
		WHERE ri.State <> '1'
	)
	BEGIN
		THROW 51000, 'Error generado por control de eliminacion desde trigger: No se puede eliminar un registro de un radicado en estado diferente al registrado', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Sin Confirmar  2. Confirmado  4. Anulado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto Devolución', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ConceptDevolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - factura Por Devolucion     0 - factura radicada por primera ves', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'Devolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de notas débito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'DebitNoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de notas crédito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'CreditNoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo contrato', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ContractEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Plan de beneficios', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de contrato', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del paciente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable de clientes', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de ingreso', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'IngressDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'IngressNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo usuario de facturacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'saldo de factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor cobrado al paciente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor cobrado a la entidad', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de Documentos  1= Factura EAPB con Contrato  2= Factura EAPB Sin Contrato  3= Factura Particular  4= Factura Capitada   5= Control de Capitacion  6= Factura Basica  7= Factura de Venta de Productos  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceDocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion con parametros de Interfaz', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion  de cabecera de cuentas de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicateInvoiceCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico detalle de factura de cuenta de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'Id';

