CREATE TABLE [GeneralLedger].[HealthSuperParametersFt004Detail] (
    [Id]                           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HealthSuperParametersFt004Id] INT NOT NULL,
    [JournalVouchersId]            INT NOT NULL,
    CONSTRAINT [PK_HealthSuperParametersFt004Detail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthSuperParametersFt004Detail_HealthSuperParametersFt004] FOREIGN KEY ([HealthSuperParametersFt004Id]) REFERENCES [GeneralLedger].[HealthSuperParametersFt004] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt004Detail_JournalVouchers] FOREIGN KEY ([JournalVouchersId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt004Detail] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt004Detail_HealthSuperParametersFt004];


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt004Detail] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt004Detail_JournalVouchers];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla journal voucher', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'JournalVouchersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersFt004Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del formato ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004Detail', @level2type = N'COLUMN', @level2name = N'Id';

