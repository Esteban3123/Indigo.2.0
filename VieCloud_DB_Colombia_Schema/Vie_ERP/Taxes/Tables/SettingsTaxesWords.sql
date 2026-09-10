CREATE TABLE [Taxes].[SettingsTaxesWords] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingsTaxesId] INT           NOT NULL,
    [Word]            VARCHAR (200) NOT NULL,
    [ThirdPartyId]    INT           NOT NULL,
    CONSTRAINT [PK_SettingsTaxesWords] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingsTaxesWords_SettingsTaxes] FOREIGN KEY ([SettingsTaxesId]) REFERENCES [Taxes].[SettingsTaxes] ([Id]),
    CONSTRAINT [FK_SettingsTaxesWords_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO





GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor, acreedor, entidad externa). Clave foránea que referencia Common.ThirdParty. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Palabra clave o término de búsqueda asociado a la configuración fiscal. Cadena de texto VARCHAR(200) utilizada para identificar conceptos, códigos o descripciones en procesos impositivos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'Word';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Palabra', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'Word';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'Word';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración de impuestos. Clave foránea que referencia Taxes.SettingsTaxes. Tipo INT. Vincula palabras a ajustes fiscales específicos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'SettingsTaxesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id ajuste de impuesto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'SettingsTaxesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'SettingsTaxesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la tabla SettingsTaxesWords. Clave primaria de tipo INT. Generado automáticamente con incremento +1.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Palabras clave o términos configurados para reglas de impuestos, asociadas a un tercero específico. Permite definir vocabulario o etiquetas que determinan cómo se aplican las configuraciones tributarias según el tipo de tercero o proveedor.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords';
