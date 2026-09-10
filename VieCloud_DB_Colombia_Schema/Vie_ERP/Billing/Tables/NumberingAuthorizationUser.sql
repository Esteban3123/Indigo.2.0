CREATE TABLE [Billing].[NumberingAuthorizationUser] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NumberingAuthorizationId] INT          NOT NULL,
    [UserId]                   INT          NOT NULL,
    [UserCode]                 VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NumberingAuthorizationUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NumberingAuthorizationId] FOREIGN KEY ([NumberingAuthorizationId]) REFERENCES [Billing].[NumberingAuthorization] ([Id]),
    CONSTRAINT [FK_NumberingAuthorizationUser_NumberingAuthorization] FOREIGN KEY ([NumberingAuthorizationId]) REFERENCES [Billing].[NumberingAuthorization] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario de seguridad; identificador alfanumérico (VARCHAR 50) que vincula el usuario del sistema con permisos de numeración y facturación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del usuario de seguridad autorizado; clave foránea que referencia al usuario con permisos para gestionar autorizaciones de numeración de documentos facturación/RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la autorización de numeración otorgada; clave foránea que referencia la autorización de consecutivos de facturas/documentos asociada al usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'NumberingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la autorizacion que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'NumberingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'NumberingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de asignación de autorización de numeración a usuario; vincula permisos de usuario con autorizaciones de numeración de facturación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de usuarios autorizados para usar una numeración de facturación específica. Controla qué usuarios tienen permiso de facturar bajo determinada resolución o prefijo de numeración.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser';
