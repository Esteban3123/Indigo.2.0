CREATE TABLE [Security].[TenantDomains] (
    [Id]         TINYINT       IDENTITY (1, 1) NOT NULL,
    [TenantId]   SMALLINT      NOT NULL,
    [DomainName] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TenantDomains] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenantDomains_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [Security].[Tenant] ([Id])
);

