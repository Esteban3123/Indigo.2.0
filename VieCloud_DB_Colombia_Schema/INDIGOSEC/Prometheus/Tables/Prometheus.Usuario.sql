CREATE TABLE [Prometheus].[Prometheus.Usuario] (
    [id]     INT          IDENTITY (1, 1) NOT NULL,
    [nombre] VARCHAR (30) NULL,
    [edad]   INT          NULL,
    [sueldo] FLOAT (53)   NULL,
    CONSTRAINT [PK_Usario_Nom] PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([nombre] ASC)
);

