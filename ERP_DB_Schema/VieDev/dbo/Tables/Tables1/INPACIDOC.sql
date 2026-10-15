CREATE TABLE [dbo].[INPACIDOC] (
    [CODDOCALM]   NUMERIC (10)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DESDOCALM]   CHAR (200)                                                                       NOT NULL,
    [CODCENATE]   CHAR (10)                                                                        NOT NULL,
    [FECREGDOC]   DATETIME                                                                         NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NULL,
    [NOMANTARC]   CHAR (250)                                                                       NOT NULL,
    [EXTEARCHI]   CHAR (5)                                                                         NOT NULL,
    [CODUSUARI]   CHAR (20)                                                                        NOT NULL,
    [TIPARCPAC]   CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR]   NUMERIC (18)                                                                     NOT NULL,
    [TIPODOCUM]   VARCHAR (3)                                                                      NULL,
    [Observation] VARCHAR (500)                                                                    NULL,
    [SupportDate] DATETIME                                                                         NULL,
    CONSTRAINT [PK_INPACIDOC] PRIMARY KEY CLUSTERED ([CODDOCALM] ASC),
    CONSTRAINT [FK_INPACIDOC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INPACIDOC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[INPACIDOC] NOCHECK CONSTRAINT [FK_INPACIDOC_ADINGRESO];


GO
ALTER TABLE [dbo].[INPACIDOC] NOCHECK CONSTRAINT [FK_INPACIDOC_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha hora soporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'SupportDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dato diligenciado en el campo Observaciones, en el formulario emergente al momento de cargar el archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el tipo de archivo guardado  1: Paciente  2: Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'TIPARCPAC';

