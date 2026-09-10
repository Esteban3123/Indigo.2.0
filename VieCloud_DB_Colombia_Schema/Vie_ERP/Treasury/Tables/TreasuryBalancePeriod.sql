CREATE TABLE [Treasury].[TreasuryBalancePeriod] (
    [Id]      INT             IDENTITY (1, 1) NOT NULL,
    [Periodo] DATETIME        NULL,
    [Balance] DECIMAL (18, 2) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo monetario en decimal (18,2) de la tesorería para el período especificado; representa disponibilidad de efectivo, caja o recursos financieros en la entidad de salud.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Saldo ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de tiempo (DATETIME) al que corresponde el registro de balance de tesorería; identifica mes, trimestre o fecha de corte para análisis financiero y flujo de caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Periodo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Periodo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Periodo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de cada registro de balance de tesorería; clave primaria para auditoría y trazabilidad de saldos históricos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el balance o saldo de tesorería por período de tiempo, permitiendo consultar el estado financiero acumulado en cada corte contable o período.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalancePeriod';
