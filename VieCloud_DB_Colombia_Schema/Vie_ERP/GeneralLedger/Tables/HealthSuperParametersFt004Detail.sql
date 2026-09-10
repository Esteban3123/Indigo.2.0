CREATE TABLE [GeneralLedger].[HealthSuperParametersFt004Detail] (
    [Id]                           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HealthSuperParametersFt004Id] INT NOT NULL,
    [JournalVouchersId]            INT NOT NULL,
    CONSTRAINT [PK_HealthSuperParametersFt004Detail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthSuperParametersFt004Detail_HealthSuperParametersFt004] FOREIGN KEY ([HealthSuperParametersFt004Id]) REFERENCES [GeneralLedger].[HealthSuperParametersFt004] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt004Detail_JournalVouchers] FOREIGN KEY ([JournalVouchersId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla JournalVouchers (Comprobante de Diario), vinculación a asiento contable de movimientos de débito/crédito en contabilidad general del ERP, tipo INT (FK)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'JournalVouchersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla journal voucher', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'JournalVouchersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'JournalVouchersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del encabezado HealthSuperParametersFt004 (Parámetro Maestro de Salud FT004), vinculación a la configuración del detalle de formato o estructura contable, tipo INT (FK)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersFt004Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersFt004Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersFt004Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único primario del detalle del registro HealthSuperParametersFt004Detail, clave principal autoincrementada que relaciona formato maestro con comprobante diario, tipo INT IDENTITY', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del formato ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la relación entre los parámetros del formulario FT004 de la Superintendencia de Salud y los comprobantes contables (vouchers) asociados. Vincula cada configuración de reporte FT004 con sus respectivos asientos o movimientos del libro mayor.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail';
