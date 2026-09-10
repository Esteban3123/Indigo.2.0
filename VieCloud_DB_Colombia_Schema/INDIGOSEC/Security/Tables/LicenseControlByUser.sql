CREATE TABLE [Security].[LicenseControlByUser] (
    [Id]            INT NOT NULL,
    [SuscriptionId] INT NOT NULL,
    [TenantUserId]  INT NOT NULL,
    CONSTRAINT [PK_LicenseControlByUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LicenseControlByUser_Suscriptions] FOREIGN KEY ([SuscriptionId]) REFERENCES [Security].[Suscriptions] ([Id]),
    CONSTRAINT [FK_LicenseControlByUser_TenantUsers] FOREIGN KEY ([TenantUserId]) REFERENCES [Security].[TenantUsers] ([Id])
);

