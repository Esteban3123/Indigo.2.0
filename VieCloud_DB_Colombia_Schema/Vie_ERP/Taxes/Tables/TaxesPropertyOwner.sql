CREATE TABLE [Taxes].[TaxesPropertyOwner] (
    [Id]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesPropertyId] INT            NOT NULL,
    [ThirdPartyId]    INT            NOT NULL,
    [OrderOwner]      INT            NOT NULL,
    [Percentage]      NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_TaxesPropertyOwner] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesPropertyOwner_TaxesProperty] FOREIGN KEY ([TaxesPropertyId]) REFERENCES [Taxes].[TaxesProperty] ([Id]),
    CONSTRAINT [FK_TaxesPropertyOwner_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_TaxesPropertyOwner__TaxesPropertyId__ThirdPartyId] UNIQUE NONCLUSTERED ([TaxesPropertyId] ASC, [ThirdPartyId] ASC)
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_TaxesPropertyOwner__ThirdPartyId]
    ON [Taxes].[TaxesPropertyOwner]([ThirdPartyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesPropertyOwner__TaxesPropertyId]
    ON [Taxes].[TaxesPropertyOwner]([TaxesPropertyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de propiedad o participación del predio; valor numérico de 0 a 100 con dos decimales que indica la cuota de participación del propietario en el inmueble o propiedad', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de propiedad del predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden o secuencia del propietario; número que define la jerarquía o prioridad del propietario dentro de los múltiples dueños de un mismo predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'OrderOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden del propietario', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'OrderOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'OrderOwner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del Tercero/Propietario (FK a Common.ThirdParty); referencia a la persona natural o jurídica propietaria del predio, puede ser persona, empresa, entidad', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Propietario', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del predio o propiedad inmueble (FK a Taxes.TaxesProperty); referencia al inmueble o activo fijo sujeto a tributación catastral o impuesto predial', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario autoincremental (INT IDENTITY); clave única de la relación entre propietario y predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de los propietarios o titulares asociados a un predio o bien inmueble tributario, indicando el porcentaje de participación o dominio que cada tercero tiene sobre dicho predio, así como el orden de prelación entre los copropietarios.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner';
