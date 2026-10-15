CREATE TABLE [Integrations].[Fomag_MedicalHistory] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Identificador] VARCHAR (50)    NOT NULL,
    [FechaEnvio]    DATETIME2 (7)   NOT NULL,
    [Estado]        INT             NOT NULL,
    [Resultado]     NVARCHAR (4000) NULL,
    CONSTRAINT [PK__Fomag_Me__3214EC0723DBF3D9] PRIMARY KEY CLUSTERED ([Id] ASC)
);

