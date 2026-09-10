CREATE TABLE [Taxes].[TaxesLiquidationDetail] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesLiquidationId] INT            NOT NULL,
    [TaxesPropertyId]    INT            NOT NULL,
    [Appraisal]          DECIMAL (18)   NOT NULL,
    [ThirdPartyId]       INT            NOT NULL,
    [RateOwner]          DECIMAL (5, 2) NOT NULL,
    [PercentageOwner]    DECIMAL (5, 2) NOT NULL,
    [TotalValueTax]      DECIMAL (18)   NOT NULL,
    [ValueOwner]         DECIMAL (18)   NOT NULL,
    [TaxesInvoiceId]     INT            NULL,
    CONSTRAINT [PK_TaxesLiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesLiquidationDetail_TaxesInvoice] FOREIGN KEY ([TaxesInvoiceId]) REFERENCES [Taxes].[TaxesInvoice] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetail_TaxesLiquidation] FOREIGN KEY ([TaxesLiquidationId]) REFERENCES [Taxes].[TaxesLiquidation] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetail_TaxesProperty] FOREIGN KEY ([TaxesPropertyId]) REFERENCES [Taxes].[TaxesProperty] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO





GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetail__TaxesLiquidationId__TaxesPropertyId__INC__Id]
    ON [Taxes].[TaxesLiquidationDetail]([TaxesLiquidationId] ASC, [TaxesPropertyId] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetail__TaxesLiquidationId__INC__Id__TaxesPropertyId__ThirdPartyId]
    ON [Taxes].[TaxesLiquidationDetail]([TaxesLiquidationId] ASC)
    INCLUDE([Id], [TaxesPropertyId], [ThirdPartyId]);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetail__TaxesInvoiceId__INC__TaxesLiquidationId__TaxesPropertyId]
    ON [Taxes].[TaxesLiquidationDetail]([TaxesInvoiceId] ASC)
    INCLUDE([TaxesLiquidationId], [TaxesPropertyId]);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetail__TaxesPropertyId__ThirdPartyId__INC__Id__TaxesInvoiceId]
    ON [Taxes].[TaxesLiquidationDetail]([TaxesPropertyId] ASC, [ThirdPartyId] ASC)
    INCLUDE([Id], [TaxesInvoiceId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de factura de impuestos (INT, nullable). Referencia a documento de cobro de impuesto predial generado. FK→[Taxes].[TaxesInvoice]', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de factura de impuestos', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesInvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor a pagar por propietario (DECIMAL 18,0). Monto específico del impuesto al predio que corresponde al dueño según su porcentaje de propiedad sobre el terreno', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValueOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor que le corresponde pagar propietario por el impuesto, este valor esta asociado al porcentaje del propietario sobre el terreno', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValueOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValueOwner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del impuesto predial (DECIMAL 18,0). Monto integral del gravamen al predio sin distinción de propietarios, base para prorrateo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor total del impuesto al predio sin importar el propietario o los propietarios que tenga', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de participación del propietario (DECIMAL 5,2). Participación accionaria o porcentual del dueño sobre la propiedad inmueble', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'PercentageOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de propietario', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'PercentageOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'PercentageOwner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifa o alícuota aplicable al propietario (DECIMAL 5,2). Tasa impositiva específica asignada al titular de la propiedad', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RateOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tarifa Propietario', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RateOwner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RateOwner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del tercero/propietario (INT). Referencia a persona natural o jurídica titular de derechos sobre el predio. FK→[Common].[ThirdParty]', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Avalúo catastral del predio (DECIMAL 18,0). Valor comercial estimado de la propiedad inmueble para fines tributarios', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Appraisal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Avaluo del Predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Appraisal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Appraisal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del gravamen predial (INT). Identificador del impuesto a la propiedad inmueble. FK→[Taxes].[TaxesProperty]', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID impuestos a la propiedad', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la liquidación de impuestos (INT). Referencia a proceso de cálculo y determinación del impuesto predial. FK→[Taxes].[TaxesLiquidation]', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de liquidación de impuestos', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY). Clave primaria de registro detalle en liquidación de impuestos prediales', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la liquidación de impuestos prediales: registra cada ítem de una liquidación, indicando el predio, el avalúo catastral, el tercero propietario, la tarifa y porcentaje de propiedad aplicados, el valor total del impuesto y el valor a cargo del propietario, con referencia opcional a la factura generada.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail';
