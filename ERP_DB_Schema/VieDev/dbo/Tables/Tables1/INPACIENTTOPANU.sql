CREATE TABLE [dbo].[INPACIENTTOPANU] (
    [Id]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ANIO]      INT                                                                              NOT NULL,
    [PAGADOCMO] NUMERIC (18)                                                                     NOT NULL,
    [PAGADOCOP] NUMERIC (18)                                                                     NOT NULL,
    [PAGADOCRE] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_INPACIENTTOPANU] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_INPACIENTTOPANU_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[INPACIENTTOPANU] NOCHECK CONSTRAINT [FK_INPACIENTTOPANU_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENTTOPANU_IPCODPACI_ANIO]
    ON [dbo].[INPACIENTTOPANU]([IPCODPACI] ASC, [ANIO] ASC)
    INCLUDE([PAGADOCMO], [PAGADOCOP], [PAGADOCRE]);

