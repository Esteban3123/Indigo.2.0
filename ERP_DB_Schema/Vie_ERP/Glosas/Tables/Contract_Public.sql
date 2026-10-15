CREATE TABLE [Glosas].[Contract_Public] (
    [ContractCode]       VARCHAR (10) NOT NULL,
    [PlanCode]           VARCHAR (10) NOT NULL,
    [AccountNotRadicate] VARCHAR (20) NULL,
    [AccountRadicate]    VARCHAR (20) NULL,
    CONSTRAINT [PK_Contract_Public] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [PlanCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuenta factura radicadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'AccountRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta de facturas sin radicar', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'AccountNotRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo plan de beneficios', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'ContractCode';

