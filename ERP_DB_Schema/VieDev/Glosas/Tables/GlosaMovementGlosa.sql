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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero causante de la glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'ResponsibleThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es normativo | 1 = Si | 0 = No|', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IsNormative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion del Concepto de Aceptación Jerarquíca    para glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'IdResponseHierarchyGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de concepto 1- Especifico 2- Detallado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'TypeConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Pendiente Evaluar Glosa   2 - Glosa Evaluada   3 - Pendiente Evaluar Reiteracion   4 - Reiteracion Evaluada   5 - Pendiente Conciliar   6 - Conciliado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosa', @level2type = N'COLUMN', @level2name = N'State';

