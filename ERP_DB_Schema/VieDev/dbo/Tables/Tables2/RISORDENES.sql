CREATE TABLE [dbo].[RISORDENES] (
    [AUTO]           INT                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ESTADO]         INT                NOT NULL,
    [IDHCORDIMAG]    INT                NULL,
    [IDAMBORDIMA]    INT                NULL,
    [NUMCONCIT]      INT                NULL,
    [FECAGENDO]      DATETIMEOFFSET (7) NULL,
    [FECREALIZANDO]  DATETIMEOFFSET (7) NULL,
    [FECREALIZADO]   DATETIMEOFFSET (7) NULL,
    [FECREMITE]      DATETIMEOFFSET (7) NULL,
    [FECANULADO]     DATETIMEOFFSET (7) NULL,
    [USUAGENDO]      VARCHAR (20)       NULL,
    [USUREALIZO]     VARCHAR (20)       NULL,
    [USUREMITE]      VARCHAR (20)       NULL,
    [USUANULO]       VARCHAR (20)       NULL,
    [MOTANULADO]     VARCHAR (50)       NULL,
    [JUSANULADO]     VARCHAR (50)       NULL,
    [CODCENLAB]      VARCHAR (50)       NULL,
    [JUSREMITE]      VARCHAR (50)       NULL,
    [CONCURREN]      ROWVERSION         NULL,
    [FECHCREA]       DATETIMEOFFSET (7) NOT NULL,
    [IDSALA]         INT                NULL,
    [FECHORAIN]      DATETIMEOFFSET (7) NULL,
    [FECHORAFI]      DATETIMEOFFSET (7) NULL,
    [USUDEVOLUCION]  VARCHAR (20)       NULL,
    [MOTDEVOLUCION]  VARCHAR (50)       NULL,
    [FECDEVOLUCION]  DATETIMEOFFSET (7) NULL,
    [OBSEDEVOLUCION] VARCHAR (50)       NULL,
    [CONSENTIMIENTO] INT                NULL,
    CONSTRAINT [PK_RISORDENES] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'especifica si acepto o no el consentimiento   1 - Si Acepto  2 - No Acepto  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CONSENTIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla AMBORDIMA (si es un registro ambulatorio)    Cuando se factura un procedimiento de imagenología desde CSA, se crea un registro en la tabla AMBORDIMA y si el registro quedó asociado a una cita de agendamiento (AMBORDIMA.NUMCONCIT) se debe relacionar el ID del registro de AMBORDIMA con el registro de IndiraRIS.Ordenes que tienen el mismo consecutivo de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDAMBORDIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado del estudio  1. Solicitado - Tecnólogo  2. Agendado - Tecnólogo  3. Realizando - Tecnólogo  4. Realizado – Sin imagen (sin lectura) - Tecnólogo  5. Realizado – Con imagen (sin lectura) – Tecnólogo  6. Remitido – Tecnólogo  7. Con lectura – Radiólogo  8. Anulado – Tecnólogo – agendamiento  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'ESTADO';

