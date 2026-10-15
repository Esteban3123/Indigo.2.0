CREATE TABLE [Security].[AuthorizationToken] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [CompanyCode]        CHAR (3)      NOT NULL,
    [AuthorizationToken] VARCHAR (100) NOT NULL,
    [TypeToken]          INT           NOT NULL,
    CONSTRAINT [PK_AuthorizationToken] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - V-Twin     ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'TypeToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Token que tomamos de Azure para la comunicación.    ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'AuthorizationToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la empresa ejemplo  999  036  040  ...', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'CompanyCode';

