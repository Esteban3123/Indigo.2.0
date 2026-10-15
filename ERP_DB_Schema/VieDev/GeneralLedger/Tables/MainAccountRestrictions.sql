CREATE TABLE [GeneralLedger].[MainAccountRestrictions] (
    [Id]              INT     IDENTITY (1, 1) NOT NULL,
    [MainAccountId]   INT     NOT NULL,
    [ItemType]        TINYINT NOT NULL,
    [CostCenterId]    INT     NULL,
    [ThirdPartyId]    INT     NULL,
    [RestrictionType] TINYINT CONSTRAINT [DF__MainAccou__Restr__42F40205] DEFAULT ((0)) NOT NULL,
    [AllItems]        BIT     CONSTRAINT [DF__MainAccou__AllIt__44DC4A77] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MainAccountRestrictions__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MainAccountRestrictions_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_MainAccountRestrictions_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_MainAccountRestrictions_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[MainAccountRestrictions] NOCHECK CONSTRAINT [FK_MainAccountRestrictions_CostCenter];


GO
ALTER TABLE [GeneralLedger].[MainAccountRestrictions] NOCHECK CONSTRAINT [FK_MainAccountRestrictions_MainAccounts];


GO
ALTER TABLE [GeneralLedger].[MainAccountRestrictions] NOCHECK CONSTRAINT [FK_MainAccountRestrictions_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_MainAccountRestrictions_MainAccountId_ItemType_CostCenterId_RestrictionType]
    ON [GeneralLedger].[MainAccountRestrictions]([MainAccountId] ASC, [ItemType] ASC, [CostCenterId] ASC, [RestrictionType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '       0. No se seleccionaron todos los centros de costo o todos los terceros     1. se seleccionaron todos los centros de costo o todos los terceros    ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'AllItems';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de restriccion que tiene la condicion  1 - Habilita los productos  2 - Restringe los productos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'RestrictionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: centro de costo, 2: tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' ID de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'Id';

