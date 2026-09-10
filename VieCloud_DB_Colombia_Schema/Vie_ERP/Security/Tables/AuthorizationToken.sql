CREATE TABLE [Security].[AuthorizationToken] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [CompanyCode]        CHAR (3)      NOT NULL,
    [AuthorizationToken] VARCHAR (100) NOT NULL,
    [TypeToken]          INT           NOT NULL,
    CONSTRAINT [PK_AuthorizationToken] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de token de autorización (INT). Valor 1 = V-Twin. Clasifica la categoría o versión del mecanismo de autenticación utilizado en la comunicación con Azure.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'TypeToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - V-Twin     ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'TypeToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'TypeToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token de autorización alfanumérico (VARCHAR 100) obtenido de Azure AD/Microsoft Identity Platform. Credencial de acceso seguro para comunicación y autenticación entre sistemas. PII - Ofuscado en reportes.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'AuthorizationToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Token que tomamos de Azure para la comunicación.    ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'AuthorizationToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'AuthorizationToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de empresa (CHAR 3). Identificador único de la entidad clínica o prestador (ej: 999, 036, 040). Clave de vinculación a contrato y centro de atención.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'CompanyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la empresa ejemplo  999  036  040  ...', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'CompanyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'CompanyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de tokens de autorización de seguridad por empresa, usados para autenticar o autorizar accesos al sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del token de autorización (clave primaria autonumérica).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'AuthorizationToken', @level2type = N'COLUMN', @level2name = N'Id';
