CREATE TABLE [Billing].[ProductAndServiceFeeUser] (
    [Id]                     INT          IDENTITY (1, 1) NOT NULL,
    [ProductAndServiceFeeId] INT          NOT NULL,
    [UserId]                 INT          NOT NULL,
    [UserCode]               VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ProductAndServiceFeeUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductAndServiceFeeUser_ProductAndServiceFee] FOREIGN KEY ([ProductAndServiceFeeId]) REFERENCES [Billing].[ProductAndServiceFee] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario, identificador alfanumérico (VARCHAR 20) asignado al profesional de la salud, administrativo o personal que gestiona tarifas de productos y servicios en el sistema de facturación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del usuario en el sistema, referencia interna que vincula al profesional o personal administrativo responsable de la tarifa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK) de la tarifa de productos y servicios asociada, referencia a Billing.ProductAndServiceFee que define precios, códigos y valores de facturación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'ProductAndServiceFeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tarifa de servicios y productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'ProductAndServiceFeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'ProductAndServiceFeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la relación usuario-tarifa en la tabla de asignación de tarifas de productos y servicios por usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de usuarios por almacen', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la asociación entre tarifas de productos y servicios y los usuarios habilitados para aplicarlas, permitiendo controlar qué usuario o profesional puede usar cada tarifa de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser';
