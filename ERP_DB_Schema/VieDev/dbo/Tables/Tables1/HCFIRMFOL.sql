CREATE TABLE [dbo].[HCFIRMFOL] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [CODPROCRE] CHAR (20)                                                                        NOT NULL,
    [CODPROFIR] CHAR (20)                                                                        NOT NULL,
    [FECFIRFOL] DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCFIRMFOL] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [NUMEFOLIO] ASC),
    CONSTRAINT [FK_HCFIRMFOL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCFIRMFOL] NOCHECK CONSTRAINT [FK_HCFIRMFOL_INPACIENT];

