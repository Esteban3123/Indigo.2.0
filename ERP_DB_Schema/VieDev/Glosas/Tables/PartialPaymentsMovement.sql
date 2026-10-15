CREATE TABLE [Glosas].[PartialPaymentsMovement] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PartialPaymentsDId]   INT          NOT NULL,
    [GlosaMovementGlosaId] INT          NOT NULL,
    [ValueEAPB]            MONEY        NULL,
    [CreationUser]         VARCHAR (20) NOT NULL,
    [CreationDate]         DATETIME     NOT NULL,
    [TimeStamp]            ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Value] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConciliationPartialPayments_GlosaMovementGlosa] FOREIGN KEY ([GlosaMovementGlosaId]) REFERENCES [Glosas].[GlosaMovementGlosa] ([Id]),
    CONSTRAINT [FK_PartialPaymentsMovement_PartialPaymentsD] FOREIGN KEY ([PartialPaymentsDId]) REFERENCES [Glosas].[PartialPaymentsD] ([Id])
);


GO
ALTER TABLE [Glosas].[PartialPaymentsMovement] NOCHECK CONSTRAINT [FK_ConciliationPartialPayments_GlosaMovementGlosa];


GO
ALTER TABLE [Glosas].[PartialPaymentsMovement] NOCHECK CONSTRAINT [FK_PartialPaymentsMovement_PartialPaymentsD];

