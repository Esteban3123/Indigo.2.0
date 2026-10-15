CREATE TABLE [Treasury].[TreasuryBalancePeriod] (
    [Id]      INT             IDENTITY (1, 1) NOT NULL,
    [Periodo] DATETIME        NULL,
    [Balance] DECIMAL (18, 2) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Saldo ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Periodo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Id';

