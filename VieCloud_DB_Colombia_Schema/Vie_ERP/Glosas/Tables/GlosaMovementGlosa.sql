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
   AFTER INSERT
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (entidad, asegurador, IPS) causante o responsable de la glosa; relación FK a Common.ThirdParty', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero causante de la glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1=Sí es normativo, 0=No es normativo; cumplimiento de normas regulatorias en la glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IsNormative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es normativo | 1 = Si | 0 = No|', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IsNormative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IsNormative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal DATETIME de auditoría: instante de creación, registro o última modificación del movimiento de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación FK hacia cabecera de conciliación (Glosas.ConciliationC)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ConciliationCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de relacion con la cabecera de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ConciliationCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ConciliationCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK al concepto de aceptación jerárquica para etapa de conciliación (Glosas.GlosasResponseHierarchy)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion del Concepto de Aceptación Jerarquíca para Conciliación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK al concepto de aceptación jerárquica para etapa de reiteración (Glosas.GlosasResponseHierarchy)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion del Concepto de Aceptación Jerarquíca para Reiteración', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK al concepto de aceptación jerárquica para glosa inicial (Glosas.GlosasResponseHierarchy)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion del Concepto de Aceptación Jerarquíca    para glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK a concepto de evaluación aplicado en momento de reiteración (Common.ConceptGlosas)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdReiterationEvaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion de concepto de evaluacion en el momento de la reiteración', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdReiterationEvaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdReiterationEvaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR del concepto de evaluación de glosa (sinónimo: código de respuesta, resultado evaluativo)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosaEvaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo concepto de evaluacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosaEvaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosaEvaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK a concepto de evaluación en glosa (Common.ConceptGlosas)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdGlosaEvaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion de concepto de evaluacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdGlosaEvaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdGlosaEvaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación textual plana de respuesta a reiteración; complemento descriptivo sin formato HTML', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationReiterationText';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de respuesta de la reiteracion texto', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationReiterationText';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationReiterationText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación en formato HTML de respuesta a reiteración; permite formato enriquecido y narrativa detallada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de respuesta de la reiteracion HTML', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación textual plana de respuesta a glosa inicial; complemento descriptivo sin formato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationGlosaText';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Jsutificacion de respuesta de la glosa texto', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationGlosaText';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationGlosaText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación en formato HTML de respuesta a glosa; permite formato enriquecido y narrativa detallada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de respuesta de la glosa HTML', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'JustificationGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado temporal anterior (TINYINT): refleja estado previo de la factura antes del movimiento de glosa actual', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TempState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado temporal - anterior al proceso en que se encontraba la factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TempState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TempState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto de glosa (CHAR): E=Específico, D=Detallado; clasifica nivel de especificidad del reclamo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TypeConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de concepto 1- Especifico 2- Detallado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TypeConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TypeConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1=Glosa principal, 0=Glosa secundaria o complementaria; jerarquía en el detalle', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'MainGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0 - NO  principal  1 - Principal', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'MainGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'MainGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de conciliación; momento en que se registró la resolución conciliada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón, fundamento o argumento de la conciliación; motivo de la resolución en tercera instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'razon de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de reiteración; momento en que se registró la impugnación o reiteración de la glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón, fundamento o argumento de la reiteración; motivo del recurso de impugnación a la decisión', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razon de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de glosa; momento en que se registró inicialmente el reclamo administrativo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleDateGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón, fundamento o argumento de la glosa inicial; motivo del cuestionamiento al detalle facturado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'razon de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'RationaleGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del movimiento de glosa (TINYINT): 1=Pendiente Evaluar Glosa, 2=Glosa Evaluada, 3=Pendiente Evaluar Reiteración, 4=Reiteración Evaluada, 5=Pendiente Conciliar, 6=Conciliado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Pendiente Evaluar Glosa   2 - Glosa Evaluada   3 - Pendiente Evaluar Reiteracion   4 - Reiteracion Evaluada   5 - Pendiente Conciliar   6 - Conciliado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY de transferencia legal; monto sujeto a traspaso legal o fondo de contingencia (default=0)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de transferencia legal', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY pendiente de conciliación; saldo aún no resuelto entre IPS y EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValuePendingConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor pendiente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValuePendingConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValuePendingConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY de pagos realizados; monto efectivamente pagado a cuenta del glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValuePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY aceptado por EAPB en conciliación; monto reconocido por asegurador en tercera instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor aceptado EAPB en conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY aceptado por IPS en conciliación; monto reconocido por prestador en tercera instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor aceptado IPS en conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY aceptado en segunda instancia; monto reconocido por autoridad de revisión ante reiteración', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor aceptado segunda instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY de saldo en reiteración: diferencia (Pendiente - Reiterado); acepto indirecto por EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueReiterationBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'saldo diferencia valor pendiente - valor reiterado = valor Aceptado Indirectamente por la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueReiterationBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueReiterationBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY reiterado; monto impugnado nuevamente en recurso posterior a rechazo inicial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueReiterated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor reiterado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueReiterated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueReiterated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY aceptado en primera instancia; monto reconocido por responsable inicial de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor aceptado primera instancia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY glosado; monto total del detalle cuestionado o rechazado en la factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor glosado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ValueGlosado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK al profesional o entidad responsable de la reiteración (Glosas.Responsible)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleReiterationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id relacion responsable de reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleReiterationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleReiterationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK al profesional o entidad responsable de evaluar la glosa (Glosas.Responsible)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id relacion responsable de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR del concepto de glosa; identificador único del motivo de rechazo o cuestionamiento', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo concepto de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK al concepto de glosa (Common.ConceptGlosas); clasificación de tipo de reclamo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion concepto de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'CodeGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número VARCHAR de factura; identificador único de la factura objeto de la glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK a detalle de factura Quirúrgica/Procedimiento (Glosas.GlosaInvoiceDetailQX)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceDetailIdQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion detalles de factura QX', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceDetailIdQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceDetailIdQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación FK a detalle de factura general (Glosas.GlosaInvoiceDetail); vínculo al servicio glosado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion detalles de facturas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) de cada movimiento o transacción en la tabla de glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de movimientos de glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos individuales de glosa asociados a ítems de factura. Registra el ciclo completo de una glosa: valores glosados, aceptados, reiterados y conciliados en cada instancia (primera instancia, reiteración, conciliación IPS y EAPB), junto con los justificativos y responsables de cada etapa del proceso de auditoría de cuentas médicas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa';
