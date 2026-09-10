CREATE TABLE [dbo].[Productos] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]        NVARCHAR (100)  NOT NULL,
    [Descripcion]   NVARCHAR (500)  NULL,
    [Precio]        DECIMAL (18, 2) NOT NULL,
    [Stock]         INT             DEFAULT ((0)) NOT NULL,
    [FechaCreacion] DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [Activo]        BIT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Productos_Precio] CHECK ([Precio]>=(0)),
    CONSTRAINT [CK_Productos_Stock] CHECK ([Stock]>=(0))
);


GO
CREATE NONCLUSTERED INDEX [IX_Productos_FechaCreacion]
    ON [dbo].[Productos]([FechaCreacion] DESC);


GO
CREATE NONCLUSTERED INDEX [IX_Productos_Activo]
    ON [dbo].[Productos]([Activo] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Productos_Nombre]
    ON [dbo].[Productos]([Nombre] ASC);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Catálogo de productos del sistema, donde cada registro almacena nombre, descripción opcional, precio (≥ 0) y cantidad en stock (≥ 0). Incluye control de vigencia mediante el campo `Activo` y registro automático de fecha de creación. Los índices sobre nombre, estado activo y fecha de creación sugieren búsquedas y listados frecuentes por esos criterios.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Productos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Productos';
GO
