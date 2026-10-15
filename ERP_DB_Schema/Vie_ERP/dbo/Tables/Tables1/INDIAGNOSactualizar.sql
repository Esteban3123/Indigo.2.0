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

