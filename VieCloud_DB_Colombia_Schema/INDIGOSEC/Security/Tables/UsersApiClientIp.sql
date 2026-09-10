CREATE TABLE [Security].[UsersApiClientIp] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [IdUsersApiClient] INT          NOT NULL,
    [AddressIp]        VARCHAR (15) NOT NULL,
    CONSTRAINT [PK_UsersApiClientIp] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersApiClient_IP] FOREIGN KEY ([IdUsersApiClient]) REFERENCES [Security].[UsersApiClient] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla para registrar las diferentes direcciones IP permitidas a un ClienteId', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClientIp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de las IP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClientIp', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del ClientId', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClientIp', @level2type = N'COLUMN', @level2name = N'IdUsersApiClient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección IP del ClientId', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClientIp', @level2type = N'COLUMN', @level2name = N'AddressIp';

