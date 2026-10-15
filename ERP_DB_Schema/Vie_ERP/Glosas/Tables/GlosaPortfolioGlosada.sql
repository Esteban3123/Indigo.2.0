CREATE TABLE [Glosas].[GlosaPortfolioGlosada] (
    [Id]                                 INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceNumber]                      VARCHAR (50)                                                                     NOT NULL,
    [InvoiceValueEntity]                 MONEY                                                                            NOT NULL,
    [InvoiceValuePacient]                MONEY                                                                            NOT NULL,
    [BalanceInvoice]                     MONEY                                                                            NOT NULL,
    [ValueGlosado]                       MONEY                                                                            NULL,
    [ValueAcceptedFirstInstance]         MONEY                                                                            NULL,
    [ValueReiterated]                    MONEY                                                                            NULL,
    [ValueReiterationBalance]            MONEY                                                                            NULL,
    [ValueAcceptedSecondInstance]        MONEY                                                                            NULL,
    [ValueAcceptedIPSconciliation]       MONEY                                                                            NULL,
    [ValueAcceptedEAPBconciliation]      MONEY                                                                            NULL,
    [ValuePayments]                      MONEY                                                                            CONSTRAINT [DF_GlosaPortfolioGlosada_ValuePayments] DEFAULT ((0)) NULL,
    [BalanceGlosa]                       MONEY                                                                            NULL,
    [LegalTransferValue]                 MONEY                                                                            NULL,
    [BalanceLegal]                       MONEY                                                                            NULL,
    [ContractName]                       VARCHAR (200)                                                                    NULL,
    [ContractCode]                       VARCHAR (20)                                                                     NULL,
    [PlanCode]                           VARCHAR (15)                                                                     NULL,
    [UserNameInvoice]                    VARCHAR (200)                                                                    NULL,
    [IngressNumber]                      VARCHAR (15)                                                                     NULL,
    [IngressDate]                        DATETIME                                                                         NULL,
    [PatientName]                        VARCHAR (200) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NULL,
    [PatientCode]                        VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [RadicatedDate]                      DATETIME                                                                         NULL,
    [RadicatedNumber]                    VARCHAR (50)                                                                     NULL,
    [InvoiceDate]                        DATETIME                                                                         NOT NULL,
    [AccountantAccountCustomers]         VARCHAR (30)                                                                     NULL,
    [State]                              TINYINT                                                                          NOT NULL,
    [PortfolioAge]                       INT                                                                              NULL,
    [Nit]                                VARCHAR (15)                                                                     NOT NULL,
    [TempState]                          TINYINT                                                                          NOT NULL,
    [EvaluationDateGlosa]                DATETIME                                                                         NULL,
    [CoordinationDateGlosa]              DATETIME                                                                         NULL,
    [EvaluationDateReiteration]          DATETIME                                                                         NULL,
    [CoordinationDateReiteration]        DATETIME                                                                         NULL,
    [ResponsibleEvaluationGlosa]         INT                                                                              NULL,
    [ResponsibleCoordinationGlosa]       INT                                                                              NULL,
    [ResponsibleEvaluationReiteration]   INT                                                                              NULL,
    [ResponsibleCoordinationReiteration] INT                                                                              NULL,
    [TimeStamp]                          ROWVERSION                                                                       NOT NULL,
    [StatusTotal]                        TINYINT                                                                          CONSTRAINT [DF_GlosaPortfolioGlosada_StatusTotal] DEFAULT ((0)) NOT NULL,
    [ImportunityCauseId]                 INT                                                                              NULL,
    CONSTRAINT [PK_GlosaPortfolioGlosada__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaPortfolioGlosada_ImportunityCause] FOREIGN KEY ([ImportunityCauseId]) REFERENCES [Glosas].[ImportunityCauses] ([Id]),
    CONSTRAINT [FK_GlosaPortfolioGlosada_Responsible] FOREIGN KEY ([ResponsibleCoordinationGlosa]) REFERENCES [Glosas].[Responsible] ([Id]),
    CONSTRAINT [FK_GlosaPortfolioGlosada_Responsible1] FOREIGN KEY ([ResponsibleCoordinationReiteration]) REFERENCES [Glosas].[Responsible] ([Id]),
    CONSTRAINT [FK_GlosaPortfolioGlosada_Responsible2] FOREIGN KEY ([ResponsibleEvaluationGlosa]) REFERENCES [Glosas].[Responsible] ([Id]),
    CONSTRAINT [FK_GlosaPortfolioGlosada_Responsible3] FOREIGN KEY ([ResponsibleEvaluationReiteration]) REFERENCES [Glosas].[Responsible] ([Id]),
    CONSTRAINT [UQ_GlosaPortfolioGlosada__InvoiceNumber] UNIQUE NONCLUSTERED ([InvoiceNumber] ASC)
);


GO
ALTER TABLE [Glosas].[GlosaPortfolioGlosada] NOCHECK CONSTRAINT [FK_GlosaPortfolioGlosada_ImportunityCause];


GO
ALTER TABLE [Glosas].[GlosaPortfolioGlosada] NOCHECK CONSTRAINT [FK_GlosaPortfolioGlosada_Responsible];


GO
ALTER TABLE [Glosas].[GlosaPortfolioGlosada] NOCHECK CONSTRAINT [FK_GlosaPortfolioGlosada_Responsible1];


GO
ALTER TABLE [Glosas].[GlosaPortfolioGlosada] NOCHECK CONSTRAINT [FK_GlosaPortfolioGlosada_Responsible2];


GO
ALTER TABLE [Glosas].[GlosaPortfolioGlosada] NOCHECK CONSTRAINT [FK_GlosaPortfolioGlosada_Responsible3];


GO
CREATE NONCLUSTERED INDEX [IX_GPG_ById]
    ON [Glosas].[GlosaPortfolioGlosada]([Id] ASC)
    INCLUDE([InvoiceDate], [RadicatedNumber], [RadicatedDate], [IngressNumber], [IngressDate], [PatientCode]);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaPortfolioGlosada__ResponsibleEvaluationReiteration]
    ON [Glosas].[GlosaPortfolioGlosada]([ResponsibleEvaluationReiteration] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_GlosaPortfolioGlosada_Responsible2]
    ON [Glosas].[GlosaPortfolioGlosada]([ResponsibleEvaluationGlosa] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaPortfolioGlosada__ResponsibleCoordinationReiteration]
    ON [Glosas].[GlosaPortfolioGlosada]([ResponsibleCoordinationReiteration] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaPortfolioGlosada__ResponsibleCoordinationGlosa]
    ON [Glosas].[GlosaPortfolioGlosada]([ResponsibleCoordinationGlosa] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-06-19
-- Description:	Se valida el valor glosado no supere el valor facturado del detalle
-- =============================================
CREATE TRIGGER [Glosas].[tgg_GlosaPortfolioGlosada_ValidateValueGlosado]
   ON  [Glosas].[GlosaPortfolioGlosada]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS
	(
		SELECT 1
		FROM INSERTED gpg
		LEFT JOIN
		(
			SELECT InvoiceNumber, SUM(ValueGlosado) ValueGlosado
			FROM Glosas.GlosaMovementGlosa
			WHERE MainGlosa = 1
			GROUP BY InvoiceNumber
		) gmg ON gpg.InvoiceNumber = gmg.InvoiceNumber
		WHERE ROUND(ISNULL(gpg.ValueGlosado, 0), 2) <> ROUND(ISNULL(gmg.ValueGlosado, 0), 2)
	)
	BEGIN
		THROW 51000, 'GlosaPortfolioGlosada: Error generado por control desde trigger. Existen facturas con valor glosado diferente al de sus detalles', 1
	END
END
GO

CREATE INDEX IX_GlosaPortfolioGlosada_Invoice_RadicatedDate
    ON Glosas.GlosaPortfolioGlosada (InvoiceNumber, RadicatedDate)
    INCLUDE (ValueGlosado, EvaluationDateGlosa, ValueAcceptedFirstInstance,
             EvaluationDateReiteration, ValueAcceptedSecondInstance, State);
GO
-- =============================================
-- Author:		Rafael patiño
-- Create date: 24/03/2014
-- Description:	Trigger para actualizar estado en Dinamica cuando se relice la confirmacion de factura en conciliacion es decir estado en genesis
-- este como 8 'Conciliado
-- =============================================
CREATE TRIGGER [Glosas].[UpdateStateERp]
ON [Glosas].[GlosaPortfolioGlosada]
AFTER  UPDATE 
AS

DECLARE @invoiceNumber AS VARCHAR(50)  = (SELECT invoiceNumber FROM INSERTED) 
Declare @StateGenesis as varchar(2) = (SELECT state FROM INSERTED)

if @StateGenesis = '8' begin
  --estado 4 = tramitada contestada
  UPDATE .crcarter set CEMESTADO = 4 where CEMNUMFAC = @invoiceNumber
 end
GO
DISABLE TRIGGER [Glosas].[UpdateStateERp]
    ON [Glosas].[GlosaPortfolioGlosada];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la causa de inoportunidad', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ImportunityCauseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de control ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'StatusTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de coordinacion de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleCoordinationReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de evaluacion reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleEvaluationReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de coordinacion glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleCoordinationGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de evaluacion glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleEvaluationGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de coordinacion de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'CoordinationDateReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de evaluacion de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'EvaluationDateReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de coordinacion de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'CoordinationDateGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de evaluacion de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'EvaluationDateGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado temporal', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'TempState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nit tercero', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad de cartera', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PortfolioAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Pendiente Confirmado Recepción Glosa  2- Pendiente Evaluacion Glosa   3- Pendiente envio de oficio   4- Pendiente confirmar reiteracion   5- Pendiente evaluacion reiteracion   6- Pendiente conciliacion   7- Pendiente de confirmar factura conciliacion   8- Conciliación   9- Conciliacion parcilal     11- Glosa con Respuesta   12- Reiteracion con Respuesta  13- Pendiente confirmar Pago Parcial   14- Confirmado Pago Parcial  15- Traslado a Cobro Juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuenta contable de cliente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre de paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Ingreso', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'IngressDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'IngressNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de facturacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de plan', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre de contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'saldo cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceLegal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'saldo de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValuePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor aceptado EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor aceptado IPS conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor aceptado segunda instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'diferencia entre el valor pendiente  y el valor reiterado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueReiterationBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor reiterado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueReiterated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor aceptado primera instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor glosado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueGlosado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'saldo de la factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor pagado por el paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor pagado entidad', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumero de saldo de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'Id';

