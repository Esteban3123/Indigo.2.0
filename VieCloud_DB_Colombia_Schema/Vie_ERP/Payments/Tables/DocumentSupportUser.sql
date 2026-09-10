CREATE TABLE [Payments].[DocumentSupportUser] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentSupportId] INT          NOT NULL,
    [UserId]            INT          NOT NULL,
    [UserCode]          VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_DocumentSupportUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentSupportId] FOREIGN KEY ([DocumentSupportId]) REFERENCES [Payments].[DocumentSupport] ([Id]),
    CONSTRAINT [FK_DocumentSupportUser_DocumentSupport] FOREIGN KEY ([DocumentSupportId]) REFERENCES [Payments].[DocumentSupport] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario de seguridad; identificador único del profesional o administrador con acceso al sistema de pagos y soportes documentales (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que tiene permisos asignados; clave foránea al usuario de seguridad autorizado para gestionar documentos de soporte de pago (INT)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la autorización/soporte documental (factura, glosa, reclamación, contrato) a la cual se está otorgando permisos de acceso a usuarios; referencia FK a Payments.DocumentSupport (INT)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'DocumentSupportId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la autorizacion que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'DocumentSupportId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'DocumentSupportId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asignación de permisos; relación entre usuario de seguridad y autorización/soporte documental (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los documentos soporte de pagos con los usuarios responsables de gestionarlos, registrando qué usuario tiene asignado o acceso a cada documento soporte en el módulo de pagos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser';
