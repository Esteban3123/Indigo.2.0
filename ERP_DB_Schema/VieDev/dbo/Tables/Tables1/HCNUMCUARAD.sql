CREATE TABLE [dbo].[HCNUMCUARAD] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMCUADRO]   VARCHAR (15)                                                                     NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECREGISTRO] DATETIME                                                                         NOT NULL,
    [USUCREACION] CHAR (20)                                                                        NOT NULL,
    [PACIEFOTO]   VARBINARY (MAX)                                                                  NULL,
    [FECHACUADRO] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_HCNUMCUARAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUMCUARAD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCNUMCUARAD] NOCHECK CONSTRAINT [FK_HCNUMCUARAD_INPACIENT];

