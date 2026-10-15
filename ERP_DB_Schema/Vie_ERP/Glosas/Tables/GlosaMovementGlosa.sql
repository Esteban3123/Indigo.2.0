CREATE TABLE [Glosas].[GlosaMovementGlosa] (
    [Id]                              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceDetailId]                 INT           NOT NULL,
    [InvoiceDetailIdQX]               INT           NULL,
    [InvoiceNumber]                   VARCHAR (50)  NULL,
    [CodeGlosaId]                     INT           NOT NULL,
    [CodeGlosa]                       VARCHAR (50)  NOT NULL,
    [ResponsibleId]                   INT           NOT NULL,
    [ResponsibleReiterationId]        INT           NULL,
    [ValueGlosado]                    MONEY         NOT NULL,
    [ValueAcceptedFirstInstance]      MONEY         NULL,
    [ValueReiterated]                 MONEY         NULL,
    [ValueReiterationBalance]         MONEY         NULL,
    [ValueAcceptedSecondInstance]     MONEY         NULL,
    [ValueAcceptedIPSconciliation]    MONEY         NULL,
    [ValueAcceptedEAPBconciliation]   MONEY         NULL,
    [ValuePayments]                   MONEY         NULL,
    [ValuePendingConciliation]        MONEY         NULL,
    [LegalTransferValue]              MONEY         CONSTRAINT [DF__GlosaMove__Legal__358361A0] DEFAULT ((0)) NULL,
    [State]                           TINYINT       NULL,
    [RationaleGlosa]                  VARCHAR (MAX) NULL,
    [RationaleDateGlosa]              DATETIME      NULL,
    [RationaleReiteration]            VARCHAR (MAX) NULL,
    [RationaleDateReiteration]        DATETIME      NULL,
    [RationaleConciliation]           VARCHAR (MAX) NULL,
    [RationaleDateConciliation]       DATETIME      NULL,
    [MainGlosa]                       BIT           NULL,
    [TypeConcept]                     CHAR (1)      NULL,
    [TempState]                       TINYINT       NULL,
    [JustificationGlosa]              VARCHAR (MAX) NULL,
    [JustificationGlosaText]          VARCHAR (MAX) NULL,
    [JustificationReiteration]        VARCHAR (MAX) NULL,
    [JustificationReiterationText]    VARCHAR (MAX) NULL,
    [IdGlosaEvaluation]               INT           NULL,
    [CodeGlosaEvaluation]             VARCHAR (50)  NULL,
    [IdReiterationEvaluation]         INT           NULL,
    [IdResponseHierarchyGlosa]        INT           NULL,
    [IdResponseHierarchyReiteration]  INT           NULL,
    [IdResponseHierarchyConciliation] INT           NULL,
    [ConciliationCId]                 INT           NULL,
    [TimeStamp]                       ROWVERSION    NOT NULL,
    [IsNormative]                     BIT           CONSTRAINT [DF_GlosaMovementGlosa_IsNormative] DEFAULT ((1)) NOT NULL,
    [ResponsibleThirdPartyId]         INT           NULL,
    CONSTRAINT [PK_GlosaMovementGlosa__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMovementGlosa_ConceptEvaluation] FOREIGN KEY ([IdGlosaEvaluation]) REFERENCES [Common].[ConceptGlosas] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_ConceptGlosas] FOREIGN KEY ([CodeGlosaId]) REFERENCES [Common].[ConceptGlosas] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_ConceptGlosasReiteration] FOREIGN KEY ([IdReiterationEvaluation]) REFERENCES [Common].[ConceptGlosas] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_ConciliationC] FOREIGN KEY ([ConciliationCId]) REFERENCES [Glosas].[ConciliationC] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_GlosaInvoiceDetail] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [Glosas].[GlosaInvoiceDetail] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_GlosaInvoiceDetailQX] FOREIGN KEY ([InvoiceDetailIdQX]) REFERENCES [Glosas].[GlosaInvoiceDetailQX] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_GlosasResponseHierarchy] FOREIGN KEY ([IdResponseHierarchyGlosa]) REFERENCES [Glosas].[GlosasResponseHierarchy] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_GlosasResponseHierarchyConciliation] FOREIGN KEY ([IdResponseHierarchyConciliation]) REFERENCES [Glosas].[GlosasResponseHierarchy] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_GlosasResponseHierarchyReiteration] FOREIGN KEY ([IdResponseHierarchyReiteration]) REFERENCES [Glosas].[GlosasResponseHierarchy] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_Responsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [Glosas].[Responsible] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_ResponsibleReiteration] FOREIGN KEY ([ResponsibleReiterationId]) REFERENCES [Glosas].[Responsible] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosa_ResponsibleThirdPartyId] FOREIGN KEY ([ResponsibleThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_ConceptEvaluation];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_ConceptGlosas];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_ConceptGlosasReiteration];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_ConciliationC];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_GlosaInvoiceDetail];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_GlosaInvoiceDetailQX];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_GlosasResponseHierarchy];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_GlosasResponseHierarchyConciliation];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_GlosasResponseHierarchyReiteration];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_Responsible];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_ResponsibleReiteration];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosa] NOCHECK CONSTRAINT [FK_GlosaMovementGlosa_ResponsibleThirdPartyId];


GO
CREATE NONCLUSTERED INDEX [IX_GlosaMovementGlosa__InvoiceDetailIdQX__ValueAcceptedIPSconciliation__INC__InvoiceDetailId__InvoiceNumber__State]
    ON [Glosas].[GlosaMovementGlosa]([InvoiceDetailIdQX] ASC, [ValueAcceptedIPSconciliation] ASC)
    INCLUDE([InvoiceDetailId], [InvoiceNumber], [State]);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaMovementGlosa__InvoiceNumber__INC__ResponsibleId__ResponsibleReiterationId__State]
    ON [Glosas].[GlosaMovementGlosa]([InvoiceNumber] ASC)
    INCLUDE([ResponsibleId], [ResponsibleReiterationId], [State]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_GlosaMovementGlosa__InvoiceDetailId__InvoiceDetailIdQX__CodeGlosa]
    ON [Glosas].[GlosaMovementGlosa]([InvoiceDetailId] ASC, [InvoiceDetailIdQX] ASC, [CodeGlosa] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-06-19
-- Description:	Se valida el valor glosado no supere el valor facturado del detalle
-- =============================================
CREATE TRIGGER [Glosas].[tgg_GlosaMovementGlosa_ValidateValueGlosado]
   ON  [Glosas].[GlosaMovementGlosa]
   AFTER INSERT,  UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	IF UPDATE (ValueGlosado) OR NOT EXISTS (SELECT 1 FROM DELETED)
    BEGIN
		IF EXISTS
		(
			SELECT 1
			FROM INSERTED i
			JOIN Glosas.GlosaInvoiceDetail gid ON i.InvoiceDetailId = gid.Id
			JOIN 
			(
				SELECT gmg.InvoiceDetailId, SUM(gmg.ValueGlosado) ValueGlosado
				FROM Glosas.GlosaMovementGlosa gmg
				WHERE gmg.MainGlosa = 1 AND gmg.InvoiceDetailIdQX IS NULL
				GROUP BY gmg.InvoiceDetailId
			) gmg ON gid.Id = gmg.InvoiceDetailId
			WHERE gid.InvoicedValue < gmg.ValueGlosado
		)
		BEGIN
			THROW 51000, 'GlosaMovementGlosa: Error generado por control desde trigger. El valor glosado no puede superar el valor facturado del detalle', 1
		END

		IF EXISTS
		(
			SELECT 1
			FROM INSERTED i
			JOIN Glosas.GlosaInvoiceDetailQX gidqx ON i.InvoiceDetailIdQX = gidqx.Id
			JOIN 
			(
				SELECT gmg.InvoiceDetailIdQX, SUM(gmg.ValueGlosado) ValueGlosado
				FROM Glosas.GlosaMovementGlosa gmg
				WHERE gmg.MainGlosa = 1 AND gmg.InvoiceDetailIdQX IS NOT NULL
				GROUP BY gmg.InvoiceDetailIdQX
			) gmg ON gidqx.Id = gmg.InvoiceDetailIdQX
			WHERE gidqx.InvoicedValue < gmg.ValueGlosado
		)
		BEGIN
			THROW 51000, 'GlosaMovementGlosa: Error generado por control desde trigger. El valor glosado no puede superar el valor facturado del detalle Quirurgico', 1
		END
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero causante de la glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es normativo | 1 = Si | 0 = No|', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IsNormative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de relacion con la cabecera de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ConciliationCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion del Concepto de Aceptación Jerarquíca para Conciliación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion del Concepto de Aceptación Jerarquíca para Reiteración', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion del Concepto de Aceptación Jerarquíca    para glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion de concepto de evaluacion en el momento de la reiteración', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdReiterationEvaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo concepto de evaluacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosaEvaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion de concepto de evaluacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdGlosaEvaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de respuesta de la reiteracion texto', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationReiterationText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de respuesta de la reiteracion HTML', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Jsutificacion de respuesta de la glosa texto', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationGlosaText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de respuesta de la glosa HTML', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado temporal - anterior al proceso en que se encontraba la factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TempState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de concepto 1- Especifico 2- Detallado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TypeConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 - NO  principal  1 - Principal', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'MainGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'razon de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Razon de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'razon de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Pendiente Evaluar Glosa   2 - Glosa Evaluada   3 - Pendiente Evaluar Reiteracion   4 - Reiteracion Evaluada   5 - Pendiente Conciliar   6 - Conciliado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de transferencia legal', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor pendiente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValuePendingConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValuePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor aceptado EAPB en conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor aceptado IPS en conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor aceptado segunda instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'saldo diferencia valor pendiente - valor reiterado = valor Aceptado Indirectamente por la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueReiterationBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor reiterado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueReiterated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor aceptado primera instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor glosado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueGlosado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id relacion responsable de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleReiterationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id relacion responsable de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo concepto de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion concepto de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion detalles de factura QX', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceDetailIdQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion detalles de facturas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de movimientos de glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'Id';

