CREATE TABLE [dbo].[ODONTOPLANTRATAMIENTOPAC] (
    [ID]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                    CHAR (10)                                                                        NOT NULL,
    [CODUSUARICREACION]            CHAR (20)                                                                        NOT NULL,
    [FECHACREACION]                DATETIME                                                                         NOT NULL,
    [ESTADO]                       INT                                                                              NOT NULL,
    [IDODONTOMOTIVOSNOCUMPLITRATA] INT                                                                              NULL,
    [JUSTIFICACION]                VARCHAR (MAX)                                                                    NULL,
    CONSTRAINT [PK_ODONTOPLANTRATAMIENTOPAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_INPROFSAL] FOREIGN KEY ([CODUSUARICREACION]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_ODONTOMOTIVOSNOCUMPLITRATA] FOREIGN KEY ([IDODONTOMOTIVOSNOCUMPLITRATA]) REFERENCES [dbo].[ODONTOMOTIVOSNOCUMPLITRATA] ([ID])
);


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPAC] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_ADINGRESO];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPAC] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_INPACIENT];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPAC] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_INPROFSAL];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPAC] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_ODONTOMOTIVOSNOCUMPLITRATA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - En ejecución  2 - Cumplido  3 - No Cumplido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'ESTADO';

