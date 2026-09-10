CREATE TABLE [Security].[UsersApiClient] (
    [Id]                 INT          IDENTITY (1, 1) NOT NULL,
    [ClientID]           VARCHAR (50) NOT NULL,
    [ClientSecret]       VARCHAR (50) NOT NULL,
    [Type]               TINYINT      NOT NULL,
    [Container]          VARCHAR (50) NULL,
    [State]              BIT          NOT NULL,
    [ProductDescription] VARCHAR (70) NULL,
    [ExpirationMinutes]  INT          NULL,
    CONSTRAINT [PK_UsersApiClient] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que permite registrar los clientes (ClientId, ClientSecret, Container, State), que utilizaran la seguridad por Json Web Tokens (JWT). Permitiendo consumir las APIs que realicen en Indigo Technologies.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ClientId del cliente', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClient', @level2type = N'COLUMN', @level2name = N'ClientID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave secreta del cliente', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClient', @level2type = N'COLUMN', @level2name = N'ClientSecret';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de Usuarios de API:
0 => Factoring
1 => Aplicaciones Nativas (Ej: vie cloud platform, Indira, Vie Verify etc)', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClient', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Container de la BD donde puede acceder el ClientId', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClient', @level2type = N'COLUMN', @level2name = N'Container';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del producto', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClient', @level2type = N'COLUMN', @level2name = N'ProductDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'tiempo en minutos de expiración del token', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UsersApiClient', @level2type = N'COLUMN', @level2name = N'ExpirationMinutes';

