CREATE TABLE [rda].[rda_enviados] (
    [Id]         UNIQUEIDENTIFIER   DEFAULT (newid()) NOT NULL,
    [IngresoId]  VARCHAR (20)       NOT NULL,
    [TipoRda]    VARCHAR (50)       NOT NULL,
    [FolioUsado] VARCHAR (20)       NOT NULL,
    [FechaEnvio] DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NOT NULL,
    [MessageId]  VARCHAR (128)      NOT NULL,
    [HashDatos]  VARCHAR (256)      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

