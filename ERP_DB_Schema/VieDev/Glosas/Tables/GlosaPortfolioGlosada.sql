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
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-06-19
-- Description:	Se valida el valor glosado no supere el valor facturado del detalle
-- =============================================
CREATE TRIGGER [Glosas].[tgg_GlosaPortfolioGlosada_ValidateValueGlosado]
   ON  [Glosas].[GlosaPortfolioGlosada]
   AFTER INSERT
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Pendiente Confirmado Recepción Glosa  2- Pendiente Evaluacion Glosa   3- Pendiente envio de oficio   4- Pendiente confirmar reiteracion   5- Pendiente evaluacion reiteracion   6- Pendiente conciliacion   7- Pendiente de confirmar factura conciliacion   8- Conciliación   9- Conciliacion parcilal     11- Glosa con Respuesta   12- Reiteracion con Respuesta  13- Pendiente confirmar Pago Parcial   14- Confirmado Pago Parcial  15- Traslado a Cobro Juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'State';

