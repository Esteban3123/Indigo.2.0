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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHSOLDIET].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHSOLDIET].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de autorización de la solicitud de dieta (DATETIME). Se registra cuando el responsable aprueba la dieta solicitada. Null si aún no ha sido autorizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'FECAUTDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de la autorizacion de las Dietas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'FECAUTDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'FECAUTDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de autorización (BIT: TRUE/FALSE). Marca si la solicitud de dieta fue aprobada. Requerido para impresión si los parámetros exigen autorización previa. Bloquea emisión hasta TRUE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'AUTSOLDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo definido en parametros, Identifica si la solicitud ya fue aprobada     Nota: Si en parametros se define Requerir Autorizacion en las Solicitudes de Dieta, no se podran imprimir las autorizaciones hasta que este campo este en TRUE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'AUTSOLDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'AUTSOLDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación de la solicitud de dieta (DATETIME). Registro del momento en que se genera la orden de alimentación para el paciente o reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'FECSOLDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de la Solicitud de las Dietas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'FECSOLDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'FECSOLDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas o restricciones dietéticas especiales (VARCHAR 40). Observaciones generales sobre la dieta: alergias, preferencias, contraindicaciones nutricionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion general a la Dieta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de dieta (CHAR 3, FK→CHTIPDIET). Clasificación de régimen: blanda, líquida, diabética, hiposódica, etc. Define el plan nutricional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Dieta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de comida (CHAR 1). Momento del día: 1=Desayuno, 2=Almuerzo, 3=Cena, 4=Complemento mañana, 5=Complemento tarde, 6=Otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODTIPCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Comida:  1: Desayuno  2: Almuerzo  3: Cena  4: Complemento mañana  5: Complemento tarde  6: Otro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODTIPCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODTIPCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cama del paciente (INT, FK→CHCAMASHO). Identificador de ubicación física. NULL para solicitudes de médicos, personal de apoyo o reservas. Solo con reservas que cumplen criterio horario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cama    Nota: Este campo quedara NULL si la solicitud es para un Medico, Personal de Apoyo o Reserva. Solo se podrán realizar solicitudes de Dietas con reservas que cumplan con el criterio de hora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del proveedor de servicios dietéticos (CHAR 15, FK→INPROVEED). Identificación de la entidad responsable de preparar/suministrar la dieta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud solicitante (VARCHAR 25 PII enmascarado, FK→INPROFSAL). Médico, nutricionista o personal que ordena la dieta. Identificación ofuscada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25 PII enmascarado, FK→INPACIENT). Cédula, documento de identificación o ID único del paciente. Dato sensible, enmascarado en consultas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario (CHAR 10, FK→ADINGRESO). Identificador de atención/episodio del paciente. NULL para reservas o solicitudes no asociadas a internación activa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso    Nota: Este campo quedara NULL si la solicitud es para un Medico, Personal de Apoyo o Reserva. Solo se podrán realizar solicitudes de Dietas con reservas que cumplan con el criterio de hora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único de solicitud de dieta (CHAR 10, PK). Identificador secuencial de la orden (ej: 00000007). Clave primaria de registro dietético.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CONSOLDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de Solicitudes de Dieta.    Nota: Codigo 00000007', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CONSOLDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET', @level2type = N'COLUMN', @level2name = N'CONSOLDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de dieta para pacientes hospitalizados. Registra cada pedido de alimentación o régimen dietario indicado por un profesional de la salud durante un ingreso, incluyendo el tipo de dieta, el proveedor, la cama asignada y si fue autorizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHSOLDIET';
