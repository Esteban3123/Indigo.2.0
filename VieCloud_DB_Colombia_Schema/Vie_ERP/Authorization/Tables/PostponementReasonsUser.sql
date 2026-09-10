CREATE TABLE [Authorization].[PostponementReasonsUser] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PostponementReasonsId] INT          NOT NULL,
    [UserId]                INT          NOT NULL,
    [UserCode]              VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PostponementReasonsUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PostponementReasonsUser_PostponementReasons] FOREIGN KEY ([PostponementReasonsId]) REFERENCES [Authorization].[PostponementReasons] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario de seguridad (VARCHAR 50); identificador textual del profesional o administrativo con permisos de aplazamiento/postergación de atenciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del usuario de seguridad que tiene permiso asignado; referencia al profesional de la salud o administrativo autorizado para gestionar postergaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK) de la razón/motivo de postergación o aplazamiento; enlaza a la tabla PostponementReasons que define las causas válidas de cancelación/demora de atenciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cancelación que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la autorización de usuario; clave primaria que relaciona un usuario específico con una razón de postergación permitida.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la relación entre los motivos de aplazamiento de autorizaciones y los usuarios responsables de aplicarlos. Permite saber qué usuario y con qué código gestionó cada razón de aplazamiento en el proceso de autorización de servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser';
