CREATE TABLE [dbo].[INDIAGNOSactualizar] (
    [Codigo]                       NVARCHAR (255) NULL,
    [Nombre]                       NVARCHAR (255) NULL,
    [EdadIni]                      FLOAT (53)     NULL,
    [UnidadEdadIni]                NVARCHAR (255) NULL,
    [EdadFin]                      FLOAT (53)     NULL,
    [UnidadEdadfin]                NVARCHAR (255) NULL,
    [Masculino]                    NVARCHAR (255) NULL,
    [Femenino]                     NVARCHAR (255) NULL,
    [Estado]                       NVARCHAR (255) NULL,
    [ExigeNotiObligatoria(SiNo)]   NVARCHAR (255) NULL,
    [CodigoFichaNotificación]      NVARCHAR (255) NULL,
    [DiagnosticoAntecedente(SiNo)] NVARCHAR (255) NULL,
    [EgresarDxPrincipal]           NVARCHAR (255) NULL,
    [Tipificacion(4505)]           NVARCHAR (255) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de carga/actualización masiva de diagnósticos médicos (códigos CIE u otro catálogo), que almacena atributos como rango de edad aplicable, sexo permitido, estado, obligatoriedad de notificación epidemiológica, ficha de notificación asociada, uso como antecedente y tipificación. Su estructura sin claves ni índices sugiere que es una tabla temporal o de importación usada para sincronizar el catálogo maestro de diagnósticos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'INDIAGNOSactualizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'INDIAGNOSactualizar';
GO
