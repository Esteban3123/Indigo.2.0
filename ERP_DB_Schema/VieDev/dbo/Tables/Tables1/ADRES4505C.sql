CREATE TABLE [dbo].[ADRES4505C] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSEC]      VARCHAR (10)  NOT NULL,
    [CONSECREPOR] VARCHAR (10)  NULL,
    [DESCRIPCI]   VARCHAR (100) NOT NULL,
    [EAPB]        CHAR (9)      NULL,
    [FECHINICIAL] DATETIME      NOT NULL,
    [FECHFINAL]   DATETIME      NOT NULL,
    [ESTADO]      TINYINT       NULL,
    [FECREGCRE]   DATETIME      NOT NULL,
    [CODUSUCRE]   CHAR (20)     NOT NULL,
    [FECREGMOD]   DATETIME      NULL,
    [CODUSUMOD]   CHAR (20)     NULL,
    [FECREGANU]   DATETIME      NULL,
    [CODUSUANU]   CHAR (20)     NULL,
    [FECCONFREG]  DATETIME      NULL,
    [CODUSUCONF]  CHAR (20)     NULL,
    [CODCENATE]   CHAR (10)     NULL,
    [IDEAPBVIE]   INT           NULL,
    CONSTRAINT [PK_ADRES4505C] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRES4505C_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADRES4505C_INENTADM] FOREIGN KEY ([EAPB]) REFERENCES [dbo].[INENTADM] ([CODENTADM])
);


GO
ALTER TABLE [dbo].[ADRES4505C] NOCHECK CONSTRAINT [FK_ADRES4505C_ADCENATEN];


GO
ALTER TABLE [dbo].[ADRES4505C] NOCHECK CONSTRAINT [FK_ADRES4505C_INENTADM];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento.  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo o codigo de archivo a reportar, solo pueden seleccionar estos tres.  01  02  03', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CONSECREPOR';

