CREATE TABLE [dbo].[HVPAQUETE] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPACSINCRO] INT                                                                              NOT NULL,
    [NUMEFOLIO]   NCHAR (10)                                                                       NULL,
    [CODAUTONU]   INT                                                                              NULL,
    [AUTOLABOR]   INT                                                                              NULL,
    [NUMMUESTRA]  INT                                                                              NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HVPAQUETE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HVPAQUETE_HVSINCRO] FOREIGN KEY ([IDPACSINCRO]) REFERENCES [dbo].[HVSINCRO] ([ID]),
    CONSTRAINT [FK_HVPAQUETE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HVPAQUETE] NOCHECK CONSTRAINT [FK_HVPAQUETE_HVSINCRO];


GO
ALTER TABLE [dbo].[HVPAQUETE] NOCHECK CONSTRAINT [FK_HVPAQUETE_INPACIENT];

