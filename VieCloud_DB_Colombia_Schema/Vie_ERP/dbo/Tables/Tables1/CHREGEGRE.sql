CREATE TABLE [dbo].[CHREGEGRE] (
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ESTPACEGR]    INT                                                                              NOT NULL,
    [FECMUEPAC]    DATETIME                                                                         NULL,
    [CODCAUMUE]    CHAR (3)                                                                         NULL,
    [NUMCERDEF]    CHAR (40) MASKED WITH (FUNCTION = 'partial(0, "DeathCertficate_Ofuscado", 0)')   NULL,
    [CODUSUARI]    CHAR (20)                                                                        NULL,
    [DEPMUNCOD]    CHAR (5)                                                                         NULL,
    [AIPSREMIS]    VARCHAR (MAX)                                                                    NULL,
    [IOBSERVAC]    VARCHAR (MAX)                                                                    NULL,
    [FECALTPAC]    DATETIME                                                                         NULL,
    [FECEGRESO]    DATETIME                                                                         NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [INDAUDFOR]    NUMERIC (18)                                                                     NULL,
    [TRANSPASIST]  TINYINT                                                                          NULL,
    [OTROTRANSP]   NCHAR (30)                                                                       NULL,
    [MEDIOTRANS]   TINYINT                                                                          NULL,
    [EMPRETRANS]   NCHAR (30)                                                                       NULL,
    [OTRAEMPTRA]   NCHAR (30)                                                                       NULL,
    [NOMRECIBE]    NCHAR (30)                                                                       NULL,
    [CARGO]        TINYINT                                                                          NULL,
    [OTROCARGO]    NCHAR (30)                                                                       NULL,
    [FECHATRANS]   DATETIME                                                                         NULL,
    [FECHASISTEMA] DATETIME                                                                         NULL,
    CONSTRAINT [PK_CHREGEGRE] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC),
    CONSTRAINT [FK_CHREGEGRE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHREGEGRE_CHREGEGRE] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[CHREGEGRE] ([NUMINGRES]),
    CONSTRAINT [FK_CHREGEGRE_INCAUMUER1] FOREIGN KEY ([CODCAUMUE]) REFERENCES [dbo].[INCAUMUER] ([CODCAUMUE]),
    CONSTRAINT [FK_CHREGEGRE_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]),
    CONSTRAINT [FK_CHREGEGRE_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHREGEGRE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHREGEGRE].[NUMCERDEF]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHREGEGRE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [_dta_index_CHREGEGRE_6_1670205646__K2_K1_11]
    ON [dbo].[CHREGEGRE]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([FECALTPAC]);


GO
ALTER INDEX [_dta_index_CHREGEGRE_6_1670205646__K2_K1_11]
    ON [dbo].[CHREGEGRE] DISABLE;




GO
CREATE NONCLUSTERED INDEX [_dta_index_CHREGEGRE_6_27915221__K2_K12]
    ON [dbo].[CHREGEGRE]([IPCODPACI] ASC, [FECEGRESO] ASC);


GO
ALTER INDEX [_dta_index_CHREGEGRE_6_27915221__K2_K12]
    ON [dbo].[CHREGEGRE] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro en el sistema (timestamp auditoría del egreso), tipo DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECHASISTEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECHASISTEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECHASISTEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del traslado o transporte asistencial del paciente (datos de translado), tipo DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECHATRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de translado (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECHATRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECHATRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otro cargo o profesional no listado en el translado (texto libre, máx 30 caracteres)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'OTROCARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro cargo (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'OTROCARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'OTROCARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo/profesional responsable del translado: 1=Auxiliar, 2=Enfermero(a), 3=Médico General, 4=Médico Especialista, 5=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo (Datos de translado)    Auxiliar 1  Enfermero(a) 2  Médico General 3  Médico Especialista 4  Otro 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la persona que recibe al paciente en destino (máx 30 caracteres)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'NOMRECIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de quien recibe (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'NOMRECIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'NOMRECIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de otra empresa transportadora asistencial si no está en catálogo (máx 30 caracteres)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'OTRAEMPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otra empresa transportadora  (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'OTRAEMPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'OTRAEMPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Empresa de transporte asistencial responsable del traslado del paciente (máx 30 caracteres)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'EMPRETRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Empresa de transporte (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'EMPRETRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'EMPRETRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de transporte/medio: 1=Terrestre (ambulancia/vehículo), 2=Marítimo/Fluvial, 3=Aéreo (helicóptero/avión)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'MEDIOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medio de transporte (Datos de translado)    Terrestre 1  Maritimo y/o fluial 2  Aéreo 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'MEDIOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'MEDIOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otro tipo de transporte asistencial no estándar (máx 30 caracteres)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'OTROTRANSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro transporte de asistencia (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'OTROTRANSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'OTROTRANSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de transporte asistencial: 1=Básico (sin equipamiento), 2=Medicalizado (equipos/personal), 3=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'TRANSPASIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Transporte de Asisitencia (Datos de translado)    Basico 1  Medicalizado 2  Otro 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'TRANSPASIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'TRANSPASIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador/bandera reservada para auditoría y fines de control normativo (numeric 18), tipo auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, enfermero) que autoriza el egreso del paciente (PII ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional que realiza el egreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora efectiva del egreso del paciente de la unidad/institución (tipo DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Egreso del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del alta médica/clínica del paciente (autorización para egreso), tipo DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECALTPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Alta del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECALTPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECALTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones administrativas y clínicas generales del egreso (texto libre, campo VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de uso administrativo para especificar observaciones generales del Egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación/código de la IPS (institución prestadora) a donde se remite el paciente (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica la IPS a donde Remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del departamento y municipio de la IPS destino a donde remite (FK INMUNICIP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el codigo del Municipio a la IPS donde remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login del usuario del sistema que crea/registra el egreso (máx 20 caracteres)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que Crea el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de certificado de defunción oficial (PII ofuscado, si paciente fallece)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del certificado de defuncion del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la causa de muerte según clasificación (FK INCAUMUER, solo si ESTPACEGRE=3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de muerte del paciente (tipo DATETIME, NULL si no aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECMUEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de muerte del paciente, solo si aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECMUEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'FECMUEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Paciente al Egreso   1: Mejor  2: Igual o Peor  3: Fallecido  4: Remitido  5: Hospitalizacion en Casa  6: Egreso creado por cunas de observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'ESTPACEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente en el sistema (documento, cédula, identificación PII ofuscado, FK INPACIENT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente (identificador único de atención, FK ADINGRESO, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de egresos hospitalarios: guarda la información del alta, fallecimiento o salida del paciente de una admisión, incluyendo estado al egreso, causa de muerte, certificado de defunción, transporte utilizado y datos del profesional que autoriza el egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE';
