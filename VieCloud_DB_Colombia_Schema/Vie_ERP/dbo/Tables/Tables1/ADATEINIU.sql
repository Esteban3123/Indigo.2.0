CREATE TABLE [dbo].[ADATEINIU] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINFORM] CHAR (4)                                                                         NOT NULL,
    [FECINFORM] DATETIME                                                                         NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [CODENTIDA] CHAR (9)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODORIATE] CHAR (2)                                                                         NOT NULL,
    [FECINGURG] DATETIME                                                                         NOT NULL,
    [CODCLATRI] CHAR (1)                                                                         NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [MOTCONATE] CHAR (1000)                                                                      NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [CODDESPAC] CHAR (1)                                                                         NOT NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [ESTTRANSA] CHAR (1)                                                                         NOT NULL,
    [DESFINENV] CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [AuthorizationEventId]          INT            NULL,
    CONSTRAINT [PK_ADATEINIU] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADATEINIU_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADATEINIU_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADATEINIU_INENTIDAD] FOREIGN KEY ([CODENTIDA]) REFERENCES [dbo].[INENTIDAD] ([CODENTIDA]),
    CONSTRAINT [FK_ADATEINIU_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADATEINIU].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_ADATEINIU_FECINFORM]
    ON [dbo].[ADATEINIU]([FECINFORM] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_AIU]
    ON [dbo].[ADATEINIU]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador numérico del registro auditado en el proceso de atención de urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino de envío final del registro: 1=EAPB (Entidad Administradora de Planes de Beneficios), 2=DTL (Dirección Territorial Local), 3=DTD (Dirección Territorial Departamental)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'DESFINENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el destino de envio final:  1: a la EAPB  2: a la DTL  3: a la DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'DESFINENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'DESFINENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la transacción de envío: 1=En Proceso, 2=Envío 1 Fallido, 3=Envío 2 Fallido, 4=Envío 3 Fallido, 5=Envío DTL Fallido, 6=Envío DTD Fallido, 7=Envío Satisfactorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Transaccion:  1: En Proceso de Envio  2: Envio 1 Fallido  3: Envio 2 Fallido  4: Envio 3 Fallido  5: Envio DTL Fallido  6: Envio DTD Fallido  7: Envio Satisfactorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que reporta la atención de urgencias, profesional o personal administrativo responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que reporta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del destino del paciente tras el alta: 1=Domicilio, 2=Observación, 3=Internación, 4=Remisión, 5=Contrarremisión, 6=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODDESPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Destino del Paciente - Homologado con Destino de Historias  1: Domicilio          2: Observación     3: Internación      4: Remisión          5: Contrarremisión  6: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODDESPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODDESPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de historia clínica, identificador del documento clínico del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio de Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de consulta en urgencias, primeros 200 caracteres del relato de consulta del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'MOTCONATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de Consulta - Se Extrae los primeros 200 Caracteres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'MOTCONATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'MOTCONATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso a urgencias, identificador del evento de atención (ej: 00000001)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso    Nota: Codigo 00000001', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de nivel de triage en urgencias: 1=Rojo (emergencia), 2=Amarillo (urgencia), 3=Verde (no urgencia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCLATRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Nivel de TRIAGE  1: Rojo   2: Amarillo  3: Verde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCLATRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCLATRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de atención en urgencias, timestamp del ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'FECINGURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'FECINGURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'FECINGURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código origen de la atención homologado: 01=Accidente trabajo, 02=Accidente tránsito, 06=Catastrófico, 13=Enfermedad general, 14=Profesional, 16=Trabajo+Tránsito, 17=Catastrófico+Trabajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODORIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Origen de la Atencion - Homologado al momento de crear el Ingreso.  01: Accidente de trabajo              02: Accidente de tránsito             06: Evento catastrófico           13: Enfermedad general                14: Enfermedad profesional             16: Accidente de trabajo y Accidente de Transito        17 = Evento catastrófico y Accidente de trabajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODORIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODORIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, cédula/documento/identificación enmascarado (PII - Identification_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad aseguradora (EAPB, EPS), referencia a tabla INENTIDAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención en urgencias, referencia a tabla ADCENATEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la atención inicial de urgencias, timestamp de creación del informe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'FECINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Atencion Inicial de Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'FECINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'FECINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la atención inicial de urgencias, identificador correlativo del informe de 4 dígitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Atencion Inicial de Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno auto-generado de la tabla ADATEINIU, clave primaria numérica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno de Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de informes de atención inicial de urgencias (ATEI/NUIU). Guarda los datos de cada informe generado por la institución para reportar ingresos urgentes al ente regulador, incluyendo identificación del paciente, fecha de ingreso, clasificación de triage y estado de envío del reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento de autorización con estado Autorizado registrado para esta urgencia. Actualizado automáticamente en la misma transacción al guardar el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'AuthorizationEventId';
