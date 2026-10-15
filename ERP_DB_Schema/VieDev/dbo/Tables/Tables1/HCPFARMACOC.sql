CREATE TABLE [dbo].[HCPFARMACOC] (
    [IDPERFIL]      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [FECHACREACION] DATETIME                                                                         NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    CONSTRAINT [PK_HCPFARMACOC] PRIMARY KEY CLUSTERED ([IDPERFIL] ASC),
    CONSTRAINT [FK_HCPFARMACOC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPFARMACOC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCPFARMACOC] NOCHECK CONSTRAINT [FK_HCPFARMACOC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPFARMACOC] NOCHECK CONSTRAINT [FK_HCPFARMACOC_INPACIENT];

