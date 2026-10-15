CREATE TABLE [dbo].[HCORDIALI] (
    [AUTO]                     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]                DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                INT                                                                              NOT NULL,
    [OBSSERIPS]                NCHAR (2000)                                                                     NULL,
    [PRISERIPS]                CHAR (1)                                                                         NOT NULL,
    [ESTSERIPS]                CHAR (1)                                                                         NULL,
    [CODDIAGNO]                CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [FECRECEXA]                DATETIME                                                                         NULL,
    [USURECEXA]                CHAR (20)                                                                        NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    CONSTRAINT [PK_HCORDIALI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDIALI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDIALI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCORDIALI] NOCHECK CONSTRAINT [FK_HCORDIALI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDIALI] NOCHECK CONSTRAINT [FK_HCORDIALI_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Servicio IPS  1-Solicitado  2-Programado  3-Anulado  4-Realizado  5-Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIALI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';

