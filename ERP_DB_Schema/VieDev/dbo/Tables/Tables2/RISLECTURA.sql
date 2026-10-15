CREATE TABLE [dbo].[RISLECTURA] (
    [AUTO]          INT                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRISORDEN]    INT                NOT NULL,
    [ESTADO]        INT                NOT NULL,
    [FECREGISTRO]   DATETIMEOFFSET (7) NOT NULL,
    [USUCREAREG]    VARCHAR (20)       NOT NULL,
    [FECCONFIRMADA] DATETIMEOFFSET (7) NULL,
    [USUCONFIRMA]   VARCHAR (20)       NULL,
    [FECANULA]      DATETIMEOFFSET (7) NULL,
    [USUANULA]      VARCHAR (20)       NULL,
    [MOTANULA]      VARCHAR (50)       NULL,
    [JUSANULA]      NVARCHAR (200)     NULL,
    [FECTRASNCRIP]  DATETIMEOFFSET (7) NULL,
    [USUTRANSCRIP]  VARCHAR (20)       NULL,
    CONSTRAINT [PK_RISLECTURA] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la lectura  1. En transcripción: una transcripción no finalizada  2. Validar: solo una puede estar en este estado  3. Validada: solo una puede estar en este estado  4. muchas pueden estar en este estado. Solo puede ser anulada después de estar en estado 3-validada  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'ESTADO';

