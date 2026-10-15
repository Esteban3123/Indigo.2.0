CREATE TABLE [Treasury].[DischargeBillBudget] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DischargeBillId]    INT             NOT NULL,
    [ObligationDetailId] INT             NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_DischargeBillBudget__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DischargeBillBudget_DischargeBill] FOREIGN KEY ([DischargeBillId]) REFERENCES [Treasury].[DischargeBill] ([Id]),
    CONSTRAINT [FK_DischargeBillBudget_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id])
);


GO
ALTER TABLE [Treasury].[DischargeBillBudget] NOCHECK CONSTRAINT [FK_DischargeBillBudget_DischargeBill];


GO
ALTER TABLE [Treasury].[DischargeBillBudget] NOCHECK CONSTRAINT [FK_DischargeBillBudget_ObligationDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor con el cual se pagará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBillBudget', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de la obligacion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBillBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de pago de factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBillBudget', @level2type = N'COLUMN', @level2name = N'DischargeBillId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBillBudget', @level2type = N'COLUMN', @level2name = N'Id';

