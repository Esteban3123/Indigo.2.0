CREATE TABLE [Admissions].[Adstratification] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             INT           NOT NULL,
    [Description]      VARCHAR (100) NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [CreationUser]     CHAR (20)     NOT NULL,
    [ModificationDate] DATETIME      NULL,
    [ModificationUSER] CHAR (20)     NULL,
    CONSTRAINT [PK_Adstratification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Adstratification_CREATIONUSER] FOREIGN KEY ([CreationUser]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_Adstratification_MODIFICATIONUSER] FOREIGN KEY ([ModificationUSER]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Catálogo de niveles de estratificación utilizados en el módulo de admisiones, donde cada registro define un código numérico y su descripción asociada (hasta 100 caracteres). Permite activar o desactivar cada nivel mediante un indicador de estado. Registra el usuario y fecha de creación y última modificación, referenciando el sistema de seguridad para auditoría.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'TABLE', @level1name=N'Adstratification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'TABLE', @level1name=N'Adstratification';
GO
