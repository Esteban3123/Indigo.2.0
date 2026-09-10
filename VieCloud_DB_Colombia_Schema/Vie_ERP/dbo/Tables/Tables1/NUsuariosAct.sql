CREATE TABLE [dbo].[NUsuariosAct] (
    [codigo]    NVARCHAR (255) NULL,
    [TERPRINOM] NVARCHAR (255) NULL,
    [TERSEGNOM] NVARCHAR (255) NULL,
    [TERPRIAPE] NVARCHAR (255) NULL,
    [TERSEGAPE] NVARCHAR (255) NULL,
    [Completo]  NVARCHAR (255) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de usuarios activos que almacena información básica de identidad personal: código de usuario, primer y segundo nombre, primer y segundo apellido, y nombre completo. Funciona como un registro auxiliar o temporal de personal habilitado en el sistema, sin claves primarias ni foráneas definidas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NUsuariosAct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NUsuariosAct';
GO
