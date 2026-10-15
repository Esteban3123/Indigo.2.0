CREATE TABLE [Billing].[ProductAndServiceFeeUser] (
    [Id]                     INT          IDENTITY (1, 1) NOT NULL,
    [ProductAndServiceFeeId] INT          NOT NULL,
    [UserId]                 INT          NOT NULL,
    [UserCode]               VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ProductAndServiceFeeUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductAndServiceFeeUser_ProductAndServiceFee] FOREIGN KEY ([ProductAndServiceFeeId]) REFERENCES [Billing].[ProductAndServiceFee] ([Id])
);


GO
ALTER TABLE [Billing].[ProductAndServiceFeeUser] NOCHECK CONSTRAINT [FK_ProductAndServiceFeeUser_ProductAndServiceFee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tarifa de servicios y productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'ProductAndServiceFeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de usuarios por almacen', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductAndServiceFeeUser', @level2type = N'COLUMN', @level2name = N'Id';

