CREATE TABLE [Payments].[LoadMassiveAccountPayableDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoadMassiveAccountPayableId] INT             NOT NULL,
    [AccountPayableConceptId]     INT             NOT NULL,
    [ThirdPartyId]                INT             NOT NULL,
    [CostCenterId]                INT             NULL,
    [Detail]                      VARCHAR (500)   NULL,
    [Nature]                      TINYINT         NOT NULL,
    [Value]                       DECIMAL (18, 2) NOT NULL,
    [RetentionConceptId]          INT             NULL,
    [BaseValue]                   DECIMAL (18, 2) CONSTRAINT [DF_LoadMassiveAccountPayableDetail_BaseValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Payments_LoadMassiveAccountPayableDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_AccountPayableConcept] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_LoadMassiveAccountPayable] FOREIGN KEY ([LoadMassiveAccountPayableId]) REFERENCES [Payments].[LoadMassiveAccountPayable] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_RetentionConcept] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_AccountPayableConcept];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_CostCenter];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_LoadMassiveAccountPayable];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_RetentionConcept];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor base', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza (Débito = 1, Crédito = 2)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Concepto de cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta por pagar cargada masivamente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'LoadMassiveAccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Id';

