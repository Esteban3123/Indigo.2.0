CREATE TABLE [Portfolio].[Detallado1] (
    [AccountReceivableId] NVARCHAR (255) NULL,
    [MainAccountId]       NVARCHAR (255) NULL,
    [ThirdPartyId]        NVARCHAR (255) NULL,
    [CostCenterId]        NVARCHAR (255) NULL,
    [Value]               FLOAT (53)     NULL,
    [Balance]             FLOAT (53)     NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el saldo general.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado1', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del detalle.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado1', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del centro de costos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado1', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero asociado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado1', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta principal.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado1', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado1', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';

