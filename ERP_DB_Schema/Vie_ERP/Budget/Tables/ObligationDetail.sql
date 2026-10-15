CREATE TABLE [Budget].[ObligationDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ObligationId]            INT             NOT NULL,
    [CommitmentDetailId]      INT             NULL,
    [CategoryId]              INT             NOT NULL,
    [RevenueTypeId]           INT             NOT NULL,
    [ExpiredDate]             DATETIME        NOT NULL,
    [InitialValue]            NUMERIC (18, 2) NOT NULL,
    [DebitModificationValue]  NUMERIC (18, 2) NOT NULL,
    [CreditModificationValue] NUMERIC (18, 2) NOT NULL,
    [TotalObligation]         NUMERIC (18)    NOT NULL,
    [ExecutedValue]           NUMERIC (18, 2) NOT NULL,
    [Balance]                 NUMERIC (18)    NOT NULL,
    [EntityId]                INT             NULL,
    [EntityCode]              VARCHAR (20)    NULL,
    [EntityName]              VARCHAR (250)   NULL,
    CONSTRAINT [PK_ObligationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Budget_ObligationDetail_ValidateValues] CHECK ((([InitialValue]-[DebitModificationValue])+[CreditModificationValue])=[TotalObligation] AND ([TotalObligation]-[ExecutedValue])=[Balance] AND NOT ([TotalObligation]<(0) OR [ExecutedValue]<(0) OR [Balance]<(0))),
    CONSTRAINT [FK_ObligationDetail_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_ObligationDetail_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_ObligationDetail_Obligation] FOREIGN KEY ([ObligationId]) REFERENCES [Budget].[Obligation] ([Id]),
    CONSTRAINT [FK_ObligationDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id])
);


GO
ALTER TABLE [Budget].[ObligationDetail] NOCHECK CONSTRAINT [CK_Budget_ObligationDetail_ValidateValues];


GO
ALTER TABLE [Budget].[ObligationDetail] NOCHECK CONSTRAINT [FK_ObligationDetail_Category];


GO
ALTER TABLE [Budget].[ObligationDetail] NOCHECK CONSTRAINT [FK_ObligationDetail_CommitmentDetail];


GO
ALTER TABLE [Budget].[ObligationDetail] NOCHECK CONSTRAINT [FK_ObligationDetail_Obligation];


GO
ALTER TABLE [Budget].[ObligationDetail] NOCHECK CONSTRAINT [FK_ObligationDetail_RevenueType];


GO
CREATE NONCLUSTERED INDEX [IX_ObligationDetail__ObligationId]
    ON [Budget].[ObligationDetail]([ObligationId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento, este campo se llena cuando se genera por CxP', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento, este campo se llena cuando se genera por CxP', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el documento, este campo se llena cuando se genera por CxP', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Saldo de la Obligación  TotalObligation - ExecutedValue ', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Ejecutado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de la Obligación, el cual se calcula asi  InitialValue - DebitModificationValue + CreditModificationValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'TotalObligation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor modificacion credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de expiracion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo del gasto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'CategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del compromiso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera de la obligacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'ObligationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationDetail', @level2type = N'COLUMN', @level2name = N'Id';

