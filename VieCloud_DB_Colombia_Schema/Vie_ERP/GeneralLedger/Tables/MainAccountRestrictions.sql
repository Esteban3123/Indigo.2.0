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



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_MainAccountRestrictions_MainAccountId_ItemType_CostCenterId_RestrictionType]
    ON [GeneralLedger].[MainAccountRestrictions]([MainAccountId] ASC, [ItemType] ASC, [CostCenterId] ASC, [RestrictionType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cobertura de la restricción: 0=Restricción específica (solo centro/tercero indicado), 1=Restricción aplica a todos los centros de costo o todos los terceros según ItemType', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'AllItems';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'       0. No se seleccionaron todos los centros de costo o todos los terceros     1. se seleccionaron todos los centros de costo o todos los terceros    ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'AllItems';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'AllItems';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de restricción contable: 0=Sin restricción, 1=Habilita/Permite productos/movimientos, 2=Restringe/Bloquea productos/movimientos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'RestrictionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de restriccion que tiene la condicion  1 - Habilita los productos  2 - Restringe los productos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'RestrictionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'RestrictionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del tercero/acreedor/proveedor/cliente (ThirdParty) asociado a la restricción; nulo si ItemType=1 (centro de costo)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del centro de costo (CostCenter) asociado a la restricción; nulo si ItemType=2 (tercero)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de elemento restringido: 1=Centro de Costo, 2=Tercero (proveedor, acreedor, cliente)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: centro de costo, 2: tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la cuenta contable principal (MainAccounts), referencia a la estructura de cuentas del libro mayor', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' ID de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) de la restricción de cuenta contable principal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Restricciones o limitaciones configuradas sobre las cuentas contables principales del libro mayor general. Controla qué centros de costo o terceros pueden (o no) registrar movimientos en cada cuenta, así como el tipo de restricción aplicada.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccountRestrictions';
