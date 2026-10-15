CREATE TABLE [dbo].[Proveedor] (
    [IdProveedor]   INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]        VARCHAR (150) NOT NULL,
    [Estado]        BIT           NOT NULL,
    [FechaCreacion] DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProveedor] ASC)
);

