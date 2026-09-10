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
ADD SENSITIVITY CLASSIFICATION TO
    [Portfolio].[RadicateInvoiceD].[IngressNumber]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Portfolio].[RadicateInvoiceD].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Portfolio].[RadicateInvoiceD].[PatientName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (SQL Server). Registra el instante exacto de creación, modificación o actualización del detalle de factura/cuenta de cobro. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de confirmación del detalle de factura: 1=Sin Confirmar, 2=Confirmado, 4=Anulado. Controla el ciclo de radicación y facturación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. Sin Confirmar  2. Confirmado  4. Anulado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto o motivo de la devolución de factura. Descripción textual del porqué se revierte o devuelve la factura radicada (máx 500 caracteres).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ConceptDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto Devolución', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ConceptDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ConceptDevolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tipo de radicación: 1=Factura por devolución/reversión, 0=Factura radicada por primera vez. Controla si es reradicación o radicación original.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'Devolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - factura Por Devolucion     0 - factura radicada por primera ves', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'Devolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'Devolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en MONEY de nota débito asociada al detalle de factura. Créditos adicionales al paciente o entidad. Por defecto 0.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'DebitNoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de notas débito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'DebitNoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'DebitNoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en MONEY de nota crédito asociada al detalle de factura. Descuentos o ajustes al paciente o entidad. Por defecto 0.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'CreditNoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de notas crédito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'CreditNoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'CreditNoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de emisión/creación de la factura. Marca el momento en que se genera la cuenta de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(20) de identificación del contrato con la entidad aseguradora o pagador. Referencia contractual.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ContractEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo contrato', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ContractEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ContractEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(20) del plan de beneficios asociado. Identifica cobertura, póliza o régimen del afiliado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Plan de beneficios', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(20) único del contrato entre la institución y la entidad. Vinculación contractual de facturación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de contrato', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente VARCHAR(200), enmascarado como PII (Name_Ofuscado). Identidad del afiliado o usuario.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del paciente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente VARCHAR(25), enmascarado como PII (Identification_Ofuscado). Equivalente a cédula, documento o identificación única del paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable VARCHAR(15) de clientes en el Plan de Cuentas. Referencia para registro contable de cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable de clientes', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME del ingreso o admisión del paciente en la institución. Inicio del episodio de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'IngressDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ingreso', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'IngressDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'IngressDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso VARCHAR(15), enmascarado como PII (Value_Ofuscado). Identificador único del episodio/atención del paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'IngressNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'IngressNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'IngressNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o nombre de usuario VARCHAR(200) que radica o genera la factura. Responsable de la radicación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo usuario de facturacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente en MONEY del detalle de factura. Cantidad aún no pagada por entidad o paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'saldo de factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en MONEY cobrado al paciente. Copago, cuota moderadora u otro cargo directo al afiliado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor cobrado al paciente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en MONEY cobrado a la entidad aseguradora o pagador. Monto facturado a EAPB, EPS, municipio o tercero.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor cobrado a la entidad', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de radicación oficial ante la entidad. Momento en que se presenta formalmente la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado VARCHAR(50) asignado por la entidad. Código de presentación/confirmación de factura radicada.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura VARCHAR(50) único en el sistema. Identificador principal del documento de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento facturado TINYINT: 1=Factura EAPB con Contrato, 2=EAPB sin Contrato, 3=Particular, 4=Capitada, 5=Control Capitación, 6=Básica, 7=Venta de Productos. Clasifica naturaleza de la facturación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceDocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de Documentos  1= Factura EAPB con Contrato  2= Factura EAPB Sin Contrato  3= Factura Particular  4= Factura Capitada   5= Control de Capitacion  6= Factura Basica  7= Factura de Venta de Productos  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceDocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'InvoiceDocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign Key INT hacia [Glosas].[GlosasParametersInterface]. Vínculo con parámetros de interfaz para glosas y ajustes. Permite trazabilidad de disputas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion con parametros de Interfaz', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign Key INT hacia [Portfolio].[RadicateInvoiceC]. Relación de cabecera/maestro de la cuenta de cobro. Vincula detalle a documento radicado principal.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicateInvoiceCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion  de cabecera de cuentas de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicateInvoiceCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'RadicateInvoiceCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico INT (IDENTITY) del detalle de factura. Primary Key de la fila en RadicateInvoiceD. Clave única del renglón de la cuenta de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico detalle de factura de cuenta de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de facturas radicadas ante entidades pagadoras (EPS, aseguradoras, empresas). Registra cada factura presentada al cobro con sus valores, estado, notas crédito/débito, devoluciones y los datos del paciente e ingreso asociados.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceD';
