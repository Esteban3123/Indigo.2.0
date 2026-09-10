CREATE TABLE [Common].[PromptPaymentDiscount] (
    [Id]           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RangeName]    VARCHAR (30)   NOT NULL,
    [InitialRank]  INT            NOT NULL,
    [EndRank]      INT            NOT NULL,
    [DiscountRate] DECIMAL (5, 2) NOT NULL,
    [SupplierId]   INT            NOT NULL,
    CONSTRAINT [PK__PromptPa__3214EC0719BE6D50] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PromptPaymentDiscount_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor/supplier relacionado; clave foránea a tabla Common.Supplier; vinculación con entidad de suministro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion tabla supplier', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento por pronto pago; valor decimal 0.00-999.99; tasa de bonificación aplicable al rango', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'DiscountRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'DiscountRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'DiscountRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rango final del intervalo; límite superior del monto o cantidad; valor entero que define fin del tramo aplicable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'EndRank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango final', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'EndRank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'EndRank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rango inicial del intervalo; límite inferior del monto o cantidad; valor entero que define inicio del tramo aplicable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'InitialRank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango Inicial', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'InitialRank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'InitialRank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del rango; etiqueta/denominación del tramo (ej: ''''Hasta 100 UVR'''', ''''De 100 a 500''''); texto VARCHAR(30)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'RangeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Rango', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'RangeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'RangeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la política de descuento; clave primaria autoincrementable; INT IDENTITY(1,1)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuentos por pronto pago definidos por rangos de posición o clasificación de proveedores. Permite configurar tasas de descuento diferenciadas según el rango en que se ubique el proveedor, para incentivar el pago anticipado de facturas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount';
