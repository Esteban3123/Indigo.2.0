CREATE TABLE [dbo].[SOLIEMAIL] (
    [Autonumerico]   TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Email]          VARCHAR (80) NOT NULL,
    [Codigo]         NUMERIC (18) NOT NULL,
    [EmailPrincipal] BIT          NOT NULL,
    CONSTRAINT [PK_SOLEMAIL] PRIMARY KEY CLUSTERED ([Autonumerico] ASC),
    CONSTRAINT [FK_SOLEMAIL_SOLPROVE] FOREIGN KEY ([Codigo]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si este email es el contacto principal o preferente del proveedor para notificaciones y solicitudes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'EmailPrincipal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene si el email es proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'EmailPrincipal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'EmailPrincipal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador numérico (NUMERIC 18) que referencia la clave primaria del proveedor en la tabla SOLPROVEE (FK PROVAUTO); vincula el email al proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico (VARCHAR 80) del proveedor; campo de contacto principal para comunicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (TINYINT IDENTITY) que genera automáticamente el sistema para cada registro de email en la tabla SOLIEMAIL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Autonumerico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de correos electrónicos asociados a una entidad (paciente, médico u otro registro), indicando cuál es el email principal de contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL';
