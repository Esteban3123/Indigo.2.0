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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion/Justificacion de la devolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'OBSEDEVOLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se realiza la devolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECDEVOLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de devolucion selecionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'MOTDEVOLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que realiza la devolucion del estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUDEVOLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora final de la Cita Médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora inicial de la Cita Médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se creo el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación/Observacion de la remisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'JUSREMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de remision seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CODCENLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación de la anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'JUSANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de anulación seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'MOTANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que anuló', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUANULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que remitió el estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUREMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que realizó el estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que agendó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUAGENDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de cuando se anuló el estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se remite el estudio, cuando se pone en estado 7-Remitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se finaliza el estudio, cuando se pone en estado 4-Realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREALIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se inicia el estudio, cuando se pone en estado 3-Realizando', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREALIZANDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se agendó, Si viene de una cita de agendamiento debe ser la misma fecha de creación de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECAGENDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla AGASICITA (Cuando se crea una cita desde agendamiento también se crea un registro en esta tabla con el IDCITA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla AMBORDIMA (si es un registro ambulatorio)    Cuando se factura un procedimiento de imagenología desde CSA, se crea un registro en la tabla AMBORDIMA y si el registro quedó asociado a una cita de agendamiento (AMBORDIMA.NUMCONCIT) se debe relacionar el ID del registro de AMBORDIMA con el registro de IndiraRIS.Ordenes que tienen el mismo consecutivo de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDAMBORDIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla HCORDIMAG (si es un registro intrahospitalario)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDHCORDIMAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado del estudio  1. Solicitado - Tecnólogo  2. Agendado - Tecnólogo  3. Realizando - Tecnólogo  4. Realizado – Sin imagen (sin lectura) - Tecnólogo  5. Realizado – Con imagen (sin lectura) – Tecnólogo  6. Remitido – Tecnólogo  7. Con lectura – Radiólogo  8. Anulado – Tecnólogo – agendamiento  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo autonumerico de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'AUTO';

