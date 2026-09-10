CREATE TABLE [dbo].[MenuNuevo] (
    [producto]         INT           NULL,
    [modulo]           INT           NULL,
    [titulo]           INT           NULL,
    [nombreformulario] VARCHAR (100) NULL,
    [idformulario]     INT           NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de configuración del menú de navegación de la aplicación, que asocia formularios con su ubicación jerárquica mediante producto, módulo y título. Permite identificar cada formulario por su nombre y un identificador numérico, estructurando así el árbol de acceso a las funcionalidades del sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MenuNuevo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MenuNuevo';
GO
