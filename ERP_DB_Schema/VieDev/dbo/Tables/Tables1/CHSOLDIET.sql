CREATE TABLE [dbo].[CHSOLDIET] (
    [CONSOLDIE] CHAR (10)                                                                        NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODPROVEE] CHAR (15)                                                                        NOT NULL,
    [CODICAMAS] INT                                                                              NULL,
    [CODTIPCOM] CHAR (1)                                                                         NOT NULL,
    [CODTIPDIE] CHAR (3)                                                                         NOT NULL,
    [OBSERVACI] CHAR (40)                                                                        NOT NULL,
    [FECSOLDIE] DATETIME                                                                         NOT NULL,
    [AUTSOLDIE] BIT                                                                              NULL,
    [FECAUTDIE] DATETIME                                                                         NULL,
    CONSTRAINT [PK_CHSOLDIET] PRIMARY KEY CLUSTERED ([CONSOLDIE] ASC),
    CONSTRAINT [FK_CHSOLDIET_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHSOLDIET_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHSOLDIET_CHCTRLCOM] FOREIGN KEY ([CODTIPCOM]) REFERENCES [dbo].[CHCTRLCOM] ([CODTIPDIE]),
    CONSTRAINT [FK_CHSOLDIET_CHTIPDIET] FOREIGN KEY ([CODTIPDIE]) REFERENCES [dbo].[CHTIPDIET] ([CODTIPDIE]) ON UPDATE CASCADE,
    CONSTRAINT [FK_CHSOLDIET_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHSOLDIET_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]) ON UPDATE CASCADE,
    CONSTRAINT [FK_CHSOLDIET_INPROVEED] FOREIGN KEY ([CODPROVEE]) REFERENCES [dbo].[INPROVEED] ([CODPROVEE]) ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[CHSOLDIET] NOCHECK CONSTRAINT [FK_CHSOLDIET_ADINGRESO];


GO
ALTER TABLE [dbo].[CHSOLDIET] NOCHECK CONSTRAINT [FK_CHSOLDIET_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHSOLDIET] NOCHECK CONSTRAINT [FK_CHSOLDIET_CHCTRLCOM];


GO
ALTER TABLE [dbo].[CHSOLDIET] NOCHECK CONSTRAINT [FK_CHSOLDIET_CHTIPDIET];


GO
ALTER TABLE [dbo].[CHSOLDIET] NOCHECK CONSTRAINT [FK_CHSOLDIET_INPacient];


GO
ALTER TABLE [dbo].[CHSOLDIET] NOCHECK CONSTRAINT [FK_CHSOLDIET_INPROFSAL];


GO
ALTER TABLE [dbo].[CHSOLDIET] NOCHECK CONSTRAINT [FK_CHSOLDIET_INPROVEED];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo definido en parametros, Identifica si la solicitud ya fue aprobada     Nota: Si en parametros se define Requerir Autorizacion en las Solicitudes de Dieta, no se podran imprimir las autorizaciones hasta que este campo este en TRUE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'AUTSOLDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Comida:  1: Desayuno  2: Almuerzo  3: Cena  4: Complemento mañana  5: Complemento tarde  6: Otro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODTIPCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cama    Nota: Este campo quedara NULL si la solicitud es para un Medico, Personal de Apoyo o Reserva. Solo se podrán realizar solicitudes de Dietas con reservas que cumplan con el criterio de hora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso    Nota: Este campo quedara NULL si la solicitud es para un Medico, Personal de Apoyo o Reserva. Solo se podrán realizar solicitudes de Dietas con reservas que cumplan con el criterio de hora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de Solicitudes de Dieta.    Nota: Codigo 00000007', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CONSOLDIE';

