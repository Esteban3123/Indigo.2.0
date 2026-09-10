CREATE TABLE [Common].[ThirdPartyFiscalResponsibility] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ThirdPartyId]           INT NOT NULL,
    [FiscalResponsibilityId] INT NOT NULL,
    CONSTRAINT [PK_ThirdPartyFiscalResponsibility] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdPartyFiscalResponsibility_FiscalResponsibility] FOREIGN KEY ([FiscalResponsibilityId]) REFERENCES [Common].[FiscalResponsibility] ([Id]),
    CONSTRAINT [FK_ThirdPartyFiscalResponsibility_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ThirdPartyFiscalResponsibility_ThirdPartyId]
    ON [Common].[ThirdPartyFiscalResponsibility]([ThirdPartyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la responsabilidad fiscal asignada al tercero. Referencia a [Common].[FiscalResponsibility]. Clasifica obligaciones tributarias, retenciones, declaraciones RIPS y regímenes de facturación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'FiscalResponsibilityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la responsabilidad fiscal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'FiscalResponsibilityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'FiscalResponsibilityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del tercero (proveedor, asegurador, entidad, contratista, centro de atención). Referencia a [Common].[ThirdParty]. Vincula responsabilidades fiscales a la entidad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asociación entre tercero y responsabilidad fiscal. Clave primaria de la tabla de relación (INT IDENTITY).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre terceros (personas naturales o jurídicas) y sus responsabilidades fiscales tributarias, como régimen común, simplificado, gran contribuyente, autorretenedor, entre otros. Permite identificar las obligaciones fiscales asociadas a cada tercero para efectos de facturación y cumplimiento tributario.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility';
