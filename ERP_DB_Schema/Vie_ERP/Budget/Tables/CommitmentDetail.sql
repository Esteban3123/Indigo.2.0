CREATE TABLE [Budget].[CommitmentDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CommitmentId]            INT          NOT NULL,
    [AvailabilityDetailId]    INT          NULL,
    [CategoryId]              INT          NOT NULL,
    [RevenueTypeId]           INT          NOT NULL,
    [ExpiredDate]             DATETIME     NOT NULL,
    [InitialValue]            NUMERIC (18) NOT NULL,
    [DebitModificationValue]  NUMERIC (18) NOT NULL,
    [CreditModificationValue] NUMERIC (18) NOT NULL,
    [TotalCommitment]         NUMERIC (18) NOT NULL,
    [ExecutedValue]           NUMERIC (18) NOT NULL,
    [Balance]                 NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CommitmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Budget_CommitmentDetail_ValidateValues] CHECK ((([InitialValue]-[DebitModificationValue])+[CreditModificationValue])=[TotalCommitment] AND ([TotalCommitment]-[ExecutedValue])=[Balance] AND NOT ([TotalCommitment]<(0) OR [ExecutedValue]<(0) OR [Balance]<(0))),
    CONSTRAINT [FK_CommitmentDetail_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_CommitmentDetail_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_CommitmentDetail_Commitment] FOREIGN KEY ([CommitmentId]) REFERENCES [Budget].[Commitment] ([Id]),
    CONSTRAINT [FK_CommitmentDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id])
);


GO
ALTER TABLE [Budget].[CommitmentDetail] NOCHECK CONSTRAINT [CK_Budget_CommitmentDetail_ValidateValues];


GO
ALTER TABLE [Budget].[CommitmentDetail] NOCHECK CONSTRAINT [FK_CommitmentDetail_AvailabilityDetail];


GO
ALTER TABLE [Budget].[CommitmentDetail] NOCHECK CONSTRAINT [FK_CommitmentDetail_Category];


GO
ALTER TABLE [Budget].[CommitmentDetail] NOCHECK CONSTRAINT [FK_CommitmentDetail_Commitment];


GO
ALTER TABLE [Budget].[CommitmentDetail] NOCHECK CONSTRAINT [FK_CommitmentDetail_RevenueType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Saldo del compromiso  TotalCommitment - ExecutedValue ', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor ejecutado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total del compromiso, el cual se calcula asi  InitialValue - DebitModificationValue + CreditModificationValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'TotalCommitment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor modificacion credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id detalle disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera compromiso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CommitmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del compromiso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'Id';

