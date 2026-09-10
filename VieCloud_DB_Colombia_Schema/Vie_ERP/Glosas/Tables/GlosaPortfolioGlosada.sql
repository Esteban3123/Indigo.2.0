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
ADD SENSITIVITY CLASSIFICATION TO
    [Glosas].[GlosaPortfolioGlosada].[PatientName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Glosas].[GlosaPortfolioGlosada].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la causa de inoportunidad o inoportun llegada de la glosa; referencia a [Glosas].[ImportunityCauses].', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ImportunityCauseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la causa de inoportunidad', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ImportunityCauseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ImportunityCauseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de control general (TINYINT, default 0); indica estatus consolidado del trámite de glosa, reconciliación o pago.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'StatusTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de control ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'StatusTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'StatusTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) de auditoría; registra instante exacto de creación, modificación o evento determinado en el registro.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del profesional responsable de coordinación en la reiteración de glosa; referencia a [Glosas].[Responsible].', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleCoordinationReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable de coordinacion de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleCoordinationReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleCoordinationReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del evaluador responsable de la reiteración de glosa; referencia a [Glosas].[Responsible].', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleEvaluationReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable de evaluacion reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleEvaluationReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleEvaluationReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del coordinador responsable de la glosa inicial; referencia a [Glosas].[Responsible].', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleCoordinationGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable de coordinacion glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleCoordinationGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleCoordinationGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del evaluador responsable de la glosa; referencia a [Glosas].[Responsible].', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleEvaluationGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable de evaluacion glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleEvaluationGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ResponsibleEvaluationGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de coordinación registrada en la reiteración de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'CoordinationDateReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de coordinacion de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'CoordinationDateReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'CoordinationDateReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de evaluación de la reiteración de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'EvaluationDateReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de evaluacion de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'EvaluationDateReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'EvaluationDateReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de coordinación de la glosa inicial.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'CoordinationDateGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de coordinacion de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'CoordinationDateGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'CoordinationDateGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de evaluación de la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'EvaluationDateGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de evaluacion de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'EvaluationDateGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'EvaluationDateGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado temporal (TINYINT); código transitorio que modela flujo de proceso durante evaluación o coordinación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'TempState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'estado temporal', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'TempState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'TempState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT o identificación (VARCHAR 15) del tercero, acreedor o entidad involucrada en la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nit tercero', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad de cartera (INT); días transcurridos desde radicación o evento clave hasta la fecha actual.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PortfolioAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad de cartera', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PortfolioAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PortfolioAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (TINYINT) del glosa: 1=Pendiente Confirmado Recepción; 2=Pendiente Evaluación; 3=Pendiente Oficio; 4=Pendiente Confirmar Reiteración; 5=Pendiente Evaluación Reiteración; 6=Pendiente Conciliación; 7=Pendiente Confirmar Factura Conciliación; 8=Conciliación Confirmada; 9=Conciliación Parcial; 11=Glosa con Respuesta; 12=Reiteración con Respuesta; 13=Pendiente Confirmar Pago Parcial; 14=Confirmado Pago Parcial; 15=Traslado a Cobro Jurídico.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Pendiente Confirmado Recepción Glosa  2- Pendiente Evaluacion Glosa   3- Pendiente envio de oficio   4- Pendiente confirmar reiteracion   5- Pendiente evaluacion reiteracion   6- Pendiente conciliacion   7- Pendiente de confirmar factura conciliacion   8- Conciliación   9- Conciliacion parcilal     11- Glosa con Respuesta   12- Reiteracion con Respuesta  13- Pendiente confirmar Pago Parcial   14- Confirmado Pago Parcial  15- Traslado a Cobro Juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (VARCHAR 30) del cliente o tercero asignada en módulo contable del ERP.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuenta contable de cliente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de emisión o generación de la factura.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado (VARCHAR 50); código único de entrada o registro administrativo de la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de radicación oficial de la glosa ante entidad correspondiente.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación (VARCHAR 20, MASKED) del paciente; cédula, documento o equivalente identificación PII ofuscada.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre (VARCHAR 200, MASKED) del paciente; dato sensible ofuscado en búsquedas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre de paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de ingreso o admisión del paciente a la atención/hospitalization.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'IngressDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Ingreso', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'IngressDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'IngressDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (VARCHAR 15); identificador del episodio de atención, admisión o prestación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'IngressNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'IngressNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'IngressNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 200) responsable de facturación o emisión de la factura.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de facturacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de plan (VARCHAR 15); identifica plan de beneficios, cobertura o producto EAPB/asegurador.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de plan', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato (VARCHAR 20); identifica acuerdo, convenio o vinculación comercial.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre (VARCHAR 200) del contrato, acuerdo o convenio de servicios.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre de contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente (MONEY) derivado a cobro jurídico; diferencia aún en litigio o gestión legal.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceLegal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'saldo cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceLegal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceLegal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY) transferido a cobro jurídico; monto trasladado para gestión legal de recuperación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente (MONEY) de glosa; diferencia entre valor glosado y valores aceptados/pagados.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'saldo de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY, default 0) de pagos parciales o abonos aplicados a la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValuePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY) aceptado por EAPB en conciliación; monto conciliado con asegurador/plan.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor aceptado EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY) aceptado por IPS en conciliación; monto aceptado por prestador de servicios.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor aceptado IPS conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY) aceptado en segunda instancia; resolución favorable tras apelación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor aceptado segunda instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diferencia (MONEY) entre valor reiterado y pendiente; saldo restante en reiteración.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueReiterationBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'diferencia entre el valor pendiente  y el valor reiterado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueReiterationBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueReiterationBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY) reiterado; monto insistido en segunda o mayor solicitud de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueReiterated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor reiterado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueReiterated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueReiterated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY) aceptado en primera instancia; monto reconocido tras evaluación inicial.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor aceptado primera instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY) glosado; monto total cuestionado o rechazado en la factura.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor glosado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'ValueGlosado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo de factura (MONEY); diferencia entre valor total y pagos o deducciones aplicadas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'saldo de la factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY) pagado o asumido por el paciente; copago, cuota moderadora o similar.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor pagado por el paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (MONEY) pagado o asumido por entidad (EAPB, IPS, asegurador, etc.)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor pagado entidad', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura (VARCHAR 50, UNIQUE); identificador único de documento de cobro.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK, IDENTITY); autonúmero secuencial del registro de saldo de glosa en portfolio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumero de saldo de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_GlosaPortfolioGlosada_Invoice_RadicatedDate]
    ON [Glosas].[GlosaPortfolioGlosada]([InvoiceNumber] ASC, [RadicatedDate] ASC)
    INCLUDE([ValueGlosado], [EvaluationDateGlosa], [ValueAcceptedFirstInstance], [EvaluationDateReiteration], [ValueAcceptedSecondInstance], [State]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cartera de facturas glosadas por aseguradores (EPS/EAPB). Registra el seguimiento financiero de cada factura en proceso de glosa: valores cobrados, glosados, aceptados en primera y segunda instancia, conciliados y pagados, junto con los responsables y fechas de cada etapa del proceso de glosa y reiteración.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaPortfolioGlosada';
