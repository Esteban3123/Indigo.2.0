CREATE TABLE [Security].[Suscriptions] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [TenantId]         SMALLINT         NOT NULL,
    [ProductCatalogId] SMALLINT         NOT NULL,
    [SuscriptionID]    UNIQUEIDENTIFIER NOT NULL,
    [TypeOfLicensing]  TINYINT          NOT NULL,
    [NumberOfUsers]    SMALLINT         NULL,
    [NumberOfDevices]  SMALLINT         NULL,
    [BTIuV1Id]         SMALLINT         NULL,
    [BTIuV2Id]         SMALLINT         NULL,
    CONSTRAINT [PK_Suscriptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Suscriptions_ProductCatalog] FOREIGN KEY ([ProductCatalogId]) REFERENCES [Security].[ProductCatalog] ([Id]),
    CONSTRAINT [FK_Suscriptions_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [Security].[Tenant] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de licenciamiento: 1|Por Usuario, 2|Por Maquina, 3|Por BTIu V1, 4|Por BTIu V2', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Suscriptions', @level2type = N'COLUMN', @level2name = N'TypeOfLicensing';

