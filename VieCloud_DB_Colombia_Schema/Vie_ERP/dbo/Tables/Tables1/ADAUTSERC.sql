CREATE TABLE [dbo].[ADAUTSERC] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINFORM] CHAR (10)                                                                        NOT NULL,
    [FECINFORM] DATETIME                                                                         NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [CODENTIDA] CHAR (9)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODORIATE] CHAR (2)                                                                         NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NULL,
    [JUSCLISER] VARCHAR (MAX)                                                                    NOT NULL,
    [CODPRIATE] CHAR (1)                                                                         NOT NULL,
    [TIPSERSOL] CHAR (1)                                                                         NOT NULL,
    [UBICAPACI] CHAR (1)                                                                         NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [UFUDESCRI] CHAR (60)                                                                        NOT NULL,
    [NUMCAMHOS] CHAR (10)                                                                        NULL,
    [NOMGUIATE] CHAR (30)                                                                        NOT NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [ESTTRANSA] CHAR (1)                                                                         NOT NULL,
    [DESFINENV] CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_ADAUTSERC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADAUTSERC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADAUTSERC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADAUTSERC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ADAUTSERC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ADAUTSERC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_ADAUTSERC_SEGusuaru] FOREIGN KEY ([CODUSUARI]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[ADAUTSERC] NOCHECK CONSTRAINT [FK_ADAUTSERC_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADAUTSERC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADAUTSERC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERC_NUMINFORM]
    ON [dbo].[ADAUTSERC]([NUMINFORM] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERC_IPCODPACI_NUMINGRES]
    ON [dbo].[ADAUTSERC]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERC_FECINFORM]
    ON [dbo].[ADAUTSERC]([FECINFORM] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador numérico único para el registro de auditoría de servicios adicionales en el sistema ADAUTSERC. Tipo: NUMERIC(18).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino final del envío de autorización: 1=EAPB (Entidad Administradora de Planes de Beneficios), 2=DTL (Dirección Territorial Local), 3=DTD (Dirección Territorial Departamental). Tipo: CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'DESFINENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el destino de envio final:  1: a la EAPB  2: a la DTL  3: a la DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'DESFINENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'DESFINENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la transacción de envío: 1=En Proceso, 2=Envío 1 Fallido, 3=Envío 2 Fallido, 4=Envío 3 Fallido, 5=Envío DTL Fallido, 6=Envío DTD Fallido, 7=Envío Satisfactorio. Tipo: CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Transaccion:  1: En Proceso de Envio  2: Envio 1 Fallido  3: Envio 2 Fallido  4: Envio 3 Fallido  5: Envio DTL Fallido  6: Envio DTD Fallido  7: Envio Satisfactorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que solicita el servicio adicional. Identificación PII ofuscada. FK a INPROFSAL. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que reporta/registra la solicitud de servicios adicionales en el sistema. FK a SEGusuaru. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que reporta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificación de la guía de atención clínica aplicada en la solicitud de servicio. Tipo: CHAR(30).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NOMGUIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guia de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NOMGUIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NOMGUIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cama en la cual se ubica el paciente durante la solicitud de servicio adicional. Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Numero de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la unidad funcional donde se realiza la solicitud (primeros 30 caracteres). Ej: Urgencias, Hospitalización, Consulta Externa. Tipo: CHAR(60).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Unidad Funcional - Extrae los 30 Caracteres Iniciales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se realiza la solicitud de servicio. FK a INUNIFUNC. Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación del paciente: 1=Consulta Externa, 2=Urgencias, 3=Hospitalización. Homologado con tipo de unidad funcional. Tipo: CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UBICAPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicacion del Paciente - Homologado con Tipo de Unidad Funcional en donde se realiza la solicitud  1: Consulta externa   2: Urgencias  3: Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UBICAPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UBICAPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio solicitado: 1=Posterior a atención inicial de urgencias, 2=Servicios electivos. Tipo: CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'TIPSERSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Servicio de Solicitud  1: Posterior a la Atención Inicial de Urgencias  2: Servicios Electivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'TIPSERSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'TIPSERSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad de la atención solicitada: 1=Prioritaria, 2=No Prioritaria. Tipo: CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODPRIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Prioridad de la Atencion  1: Prioritaria  2: No Prioritaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODPRIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODPRIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica, diagnóstica y administrativa para la solicitud del servicio adicional. Soporte documentado. Tipo: VARCHAR(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'JUSCLISER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion Clinica para la solicitud del Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'JUSCLISER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'JUSCLISER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de historia clínica asociado a la solicitud de servicio. Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio de Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente a la institución. FK a ADINGRESO. Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código origen de la atención: 01=Accidente trabajo, 02=Accidente tránsito, 06=Evento catastrófico, 13=Enfermedad general, 14=Profesional, 16=Trabajo y tránsito, 17=Catástrofe y trabajo. Tipo: CHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODORIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Origen de la Atencion - Homologado al momento de crear el Ingreso.  01: Accidente de trabajo              02: Accidente de tránsito             06: Evento catastrófico           13: Enfermedad general                14: Enfermedad profesional             16: Accidente de trabajo y Accidente de Transito        17 = Evento catastrófico y Accidente de trabajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODORIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODORIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, pasaporte, documento). Identificación PII ofuscada. FK a INPACIENT. Tipo: VARCHAR(25).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad aseguradora o prestadora responsable. FK a INENTIDAD. Tipo: CHAR(9).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se realiza la solicitud. FK a ADCENATEN. Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro/solicitud del servicio adicional en el sistema. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'FECINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Solicitud de Servicios Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'FECINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'FECINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización para servicios adicionales a la atención inicial de urgencias. Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Autorizacion para Servicios Adicionales a la Atencion Inicial de Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno, identificador único de la tabla ADAUTSERC. Clave primaria. Tipo: NUMERIC(18) IDENTITY.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno de Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de autorización de servicios ante entidades aseguradoras (EPS/aseguradoras). Registra cada petición de autorización generada durante un ingreso del paciente, incluyendo el tipo de servicio solicitado, la justificación clínica, la prioridad, la ubicación del paciente y el profesional que la genera. Tabla en desuso por desarrollo de Dashboard Autorizacion Intrahospitalaria el 23/06/2026', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC';
