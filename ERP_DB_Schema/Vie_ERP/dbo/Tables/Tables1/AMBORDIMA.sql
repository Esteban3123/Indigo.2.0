CREATE TABLE [dbo].[AMBORDIMA] (
    [IPCODPACI]                 VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                 CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                 CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                 CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                 CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [FECORDMED]                 DATETIME                                                                         NOT NULL,
    [CODSERIPS]                 CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                 INT                                                                              NOT NULL,
    [ESTSERIPS]                 CHAR (1)                                                                         NULL,
    [INDAUDFOR]                 NUMERIC (18)                                                                     NOT NULL,
    [ESTALEIMG]                 BIT                                                                              NOT NULL,
    [FECRECEXA]                 DATETIME                                                                         NULL,
    [NOMARCIMG]                 CHAR (250)                                                                       NULL,
    [CONCURRE]                  ROWVERSION                                                                       NULL,
    [USURECEXA]                 CHAR (20)                                                                        NULL,
    [REALINOTIF]                BIT                                                                              NOT NULL,
    [NOMRESULT]                 VARCHAR (100)                                                                    NULL,
    [SERTRANSC]                 BIT                                                                              NULL,
    [SERVALMED]                 BIT                                                                              NULL,
    [CODUSUTRA]                 CHAR (20)                                                                        NULL,
    [CODPROVAL]                 CHAR (20)                                                                        NULL,
    [FECTRASER]                 DATETIME                                                                         NULL,
    [FECVALSER]                 DATETIME                                                                         NULL,
    [ESTTRASER]                 BIT                                                                              NULL,
    [CODENTIDA]                 CHAR (9)                                                                         NULL,
    [CODCONTRA]                 CHAR (6)                                                                         NULL,
    [CODPANATE]                 CHAR (2)                                                                         NULL,
    [IPFECHACO]                 DATETIME                                                                         NOT NULL,
    [NUMCONCIT]                 CHAR (20)                                                                        NULL,
    [OBSERVACI]                 VARCHAR (2000)                                                                   NULL,
    [USUOCUREG]                 CHAR (20)                                                                        NULL,
    [OBSERVSER]                 VARCHAR (2000)                                                                   NULL,
    [LECTURA]                   BIT                                                                              NULL,
    [MEDREALEC]                 CHAR (20)                                                                        NULL,
    [FECHLECT]                  DATETIME                                                                         NULL,
    [GENSERVICEORDER]           INT                                                                              NULL,
    [AUTO]                      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIENEGRABACION]            BIT                                                                              NULL,
    [SERREAINT]                 BIT                                                                              NULL,
    [FECHARESCLA]               DATETIME                                                                         NULL,
    [CLASIFICABIRADS]           TINYINT                                                                          NULL,
    [GENCAREGROUP]              INT                                                                              NULL,
    [GENCONENTITY]              INT                                                                              NULL,
    [GENINVOICE]                VARCHAR (20)                                                                     NULL,
    [GENINVOICEID]              INT                                                                              NULL,
    [INTERPRET]                 VARCHAR (4000)                                                                   NULL,
    [CODPROINT]                 CHAR (20)                                                                        NULL,
    [NUMFOLINT]                 CHAR (10)                                                                        NULL,
    [ESPEREALIZA]               CHAR (3)                                                                         NULL,
    [REGASITENCIA]              BIT                                                                              NULL,
    [IDDESCRIPCIONRELACIONADA]  NCHAR (10)                                                                       NULL,
    [RelativeURI]               VARCHAR (500)                                                                    NULL,
    [IdServerOrderDetail]       INT                                                                              NULL,
    [ExamProfessionalCode]      CHAR (20)                                                                        NULL,
    [ExamSpecialtyCode]         CHAR (3)                                                                         NULL,
    [ExamTakenDate]             DATETIME                                                                         NULL,
    [CodeCareCenterProcess]     CHAR (10)                                                                        NULL,
    [ExamCompletedDate]         DATETIME                                                                         NULL,
    [AttendingProfessionalCode] CHAR (20)                                                                        NULL,
    [AttendingSpecialty]        CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_AMBORDIMA] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_AMBORDIMA_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDIMA_CodeCareCenterProcess] FOREIGN KEY ([CodeCareCenterProcess]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDIMA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDIMA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AMBORDIMA_INPROFSAL1] FOREIGN KEY ([CODPROVAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AMBORDIMA_INPROFSAL3] FOREIGN KEY ([MEDREALEC]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AMBORDIMA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[AMBORDIMA] NOCHECK CONSTRAINT [FK_AMBORDIMA_ADcenaten];


GO
ALTER TABLE [dbo].[AMBORDIMA] NOCHECK CONSTRAINT [FK_AMBORDIMA_INCUPSIPS];


GO
ALTER TABLE [dbo].[AMBORDIMA] NOCHECK CONSTRAINT [FK_AMBORDIMA_INPACIENT];


GO
ALTER TABLE [dbo].[AMBORDIMA] NOCHECK CONSTRAINT [FK_AMBORDIMA_INPROFSAL1];


GO
ALTER TABLE [dbo].[AMBORDIMA] NOCHECK CONSTRAINT [FK_AMBORDIMA_INPROFSAL3];


GO
ALTER TABLE [dbo].[AMBORDIMA] NOCHECK CONSTRAINT [FK_AMBORDIMA_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_AMBORDIMA_CODSERIPS_CANSERIPS_CODCENATE_CODPROSAL_CONCURRE_ESTALEIMG_ESTSERIPS_FECORDMED]
    ON [dbo].[AMBORDIMA]([CODSERIPS] ASC)
    INCLUDE([CANSERIPS], [CODCENATE], [CODPROSAL], [CONCURRE], [ESTALEIMG], [NUMINGRES], [OBSERVACI], [OBSERVSER], [ESTSERIPS], [FECORDMED], [FECRECEXA], [IDDESCRIPCIONRELACIONADA], [IPCODPACI], [NUMCONCIT]);


GO
CREATE NONCLUSTERED INDEX [IDX_OrdenesMedicas]
    ON [dbo].[AMBORDIMA]([CODCENATE] ASC, [CODSERIPS] ASC)
    INCLUDE([CANSERIPS], [CODPROSAL], [CONCURRE], [ESTALEIMG], [OBSERVSER], [ESTSERIPS], [FECORDMED], [FECRECEXA], [IPCODPACI], [NUMINGRES], [OBSERVACI]);


GO
CREATE NONCLUSTERED INDEX [IDX_ControlServiciosAmbulatorios]
    ON [dbo].[AMBORDIMA]([CODCENATE] ASC, [SERTRANSC] ASC, [SERVALMED] ASC, [ESTTRASER] ASC, [TIENEGRABACION] ASC)
    INCLUDE([CANSERIPS], [IPCODPACI], [NUMINGRES], [OBSERVACI], [CODPROSAL], [CODSERIPS], [CONCURRE], [ESTALEIMG], [ESTSERIPS], [FECORDMED]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_AMBORDIMA_6_2011922289__K3_K6_K2_K7_K1_K5_K28_K12]
    ON [dbo].[AMBORDIMA]([CODCENATE] ASC, [FECORDMED] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC, [IPCODPACI] ASC, [CODPROSAL] ASC, [IPFECHACO] ASC, [FECRECEXA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la especialidad del profesional registrado como profesional que asiste y guardado el la columna AttendingProfessionalCode', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AttendingSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del profesional el cual fue registrado como profesional que asiste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AttendingProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha que se registra como fecha de inicio del procesamiento en el dashboard de imagenologia tecnologo al momento de tomar el examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamTakenDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo de la especialidad seleccionada del profesional seleccionado al momento de tomar el examen en el dashboar de imagenologia tecnologo en examenes solicitados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamSpecialtyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del profesional seleccionado al momento de tomar el examen en el dashboar de imagenologia tecnologo en examenes solicitados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el detalle Id de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La URI Relativa se usa para identificar la ruta web o fisica en la que se encuentra el recurso, en este caso la imagen.    Ejemplo: URIRealtiva de imagen en indira: /study/7/4  La URL Base del recurso se guarda en la tabla de contenedores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'RelativeURI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si esta implementada la carateristica de registro de asistencia, este campo determina si el paciente ya realizo el registro de asistencia o no.  true  false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'REGASITENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especialidad Realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número Folio que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del profesional que realiza la interpretación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Interpretación del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Numero de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENINVOICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la Entidad Administradora de salud del contrato, este campo solo se llena si el Grupo de atencion que seleccione es de EAPB Sin Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del grupo de atencion de la base de datos de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la calificación BIRADS:  1- BIRADS 0: Necesidad de Nuevo Estudio Imagenológico o Mamograma previo para evaluación  2- BIRADS 1: Negativo  3- BIRADS 2: Hallazgos Benignos  4- BIRADS 3: Probablemente Benigno  5- BIRADS 4: Anormalidad Sospechosa  6- BIRADS 5: Altamente Sospechoso de Malignidad  7- BIRADS 6: Malignidad por Biopsia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CLASIFICABIRADS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Resultado de Clasificación, Fecha Mamografía  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECHARESCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Realiza interfáz 1:si 0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERREAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene grabacion 1:si 0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'TIENEGRABACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Imagenologia, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que se realizó la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECHLECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medico que realiza la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'MEDREALEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que contiene la grabación hecha por el médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'LECTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'OBSERVSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que ocupa el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'USUOCUREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Consecutivo Cita en Interfaz    Campo que se registra desde vie cuando se factura la cita de imagenes Dx que va a contener el ID de la cita es decir de la tabla AGASISITA.    Por medio de este campo voy a tener la relacion de la orden con la cita que esta asociada.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IPFECHACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Transcripcion del Servicio de Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTTRASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Validacion de la Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECVALSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Transcripcion del la Imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECTRASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional que valida la Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que Transcribe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODUSUTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si la Transcripcion Fue ya Validada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERVALMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si el Servicio ya se le realizo la Lectura por el Tecnologo o Radiologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERTRANSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NOMRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'especifica si ya realizo la sincronizacion con ristracab y ristraded', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'REALINOTIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'USURECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NOMARCIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECRECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTALEIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural 8: Realizando', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

