CREATE TABLE [Integrations].[Fomag_orderSync] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Identificador] VARCHAR (50)   NOT NULL,
    [FechaEnvio]    DATETIME2 (7)  NOT NULL,
    [Estado]        INT            NOT NULL,
    [Resultado]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK__Fomag_or__3214EC070BFAB258] PRIMARY KEY CLUSTERED ([Id] ASC)
);

