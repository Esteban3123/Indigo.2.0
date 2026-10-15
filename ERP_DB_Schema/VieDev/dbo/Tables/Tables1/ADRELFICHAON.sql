CREATE TABLE [dbo].[ADRELFICHAON] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [IDFICHA]       INT                                                                              NOT NULL,
    [ESTADO]        INT                                                                              NOT NULL,
    [USUCREACION]   CHAR (20)                                                                        NOT NULL,
    [FECHACREACION] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_ADRELFICHAON] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRELFICHAON_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADRELFICHAON_HCONCOFICH] FOREIGN KEY ([IDFICHA]) REFERENCES [dbo].[HCONCOFICH] ([ID]),
    CONSTRAINT [FK_ADRELFICHAON_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADRELFICHAON] NOCHECK CONSTRAINT [FK_ADRELFICHAON_ADINGRESO];


GO
ALTER TABLE [dbo].[ADRELFICHAON] NOCHECK CONSTRAINT [FK_ADRELFICHAON_HCONCOFICH];


GO
ALTER TABLE [dbo].[ADRELFICHAON] NOCHECK CONSTRAINT [FK_ADRELFICHAON_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Ficha   0 - No Esta Amarrada Con El Ingreso  1 - Amarrada Con El Ingreso  2 - Inativada La Ficha Oncologica.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRELFICHAON', @level2type = N'COLUMN', @level2name = N'ESTADO';

