CREATE TABLE [dbo].[Proveedor] (
    [IdProveedor]   INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]        VARCHAR (150) NOT NULL,
    [Estado]        BIT           NOT NULL,
    [FechaCreacion] DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProveedor] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla maestra que registra los proveedores del sistema, almacenando su nombre, estado activo/inactivo y fecha de registro. Sirve como catálogo base para relacionar proveedores con otros procesos del dominio (compras, insumos u otros).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Proveedor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Proveedor';
GO
