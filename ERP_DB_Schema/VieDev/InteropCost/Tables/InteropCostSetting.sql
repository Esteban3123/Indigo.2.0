CREATE TABLE [InteropCost].[InteropCostSetting] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                   INT          NOT NULL,
    [Month]                  INT          NOT NULL,
    [AccountingCosts]        BIT          CONSTRAINT [DF_InteropCostSetting_AccountingCosts] DEFAULT ((0)) NOT NULL,
    [JournalVoucherTypeId]   INT          NULL,
    [JournalVoucherTypeCode] VARCHAR (20) NOT NULL,
    [CostEstimateLabor]      TINYINT      CONSTRAINT [DF_InteropCostSetting_CostEstimateLabor] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_InteropCostSetting] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va a realizar la estimacion de costos para la Mano de Obra  1 - Total Devengados  2 - Total Devengados Mas Carga patronal', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'CostEstimateLabor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo comprobante contable    Se llena siempre y cuando AccountingCosts esté en True, sino va NULL', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';

