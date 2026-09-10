CREATE TABLE [dbo].[AMBORDIMA] (
    [IPCODPACI]                   VARCHAR (25)   NOT NULL,
    [NUMINGRES]                   CHAR (10)      NOT NULL,
    [CODCENATE]                   CHAR (10)      NOT NULL,
    [UFUCODIGO]                   CHAR (10)      NOT NULL,
    [CODPROSAL]                   CHAR (20)      NULL,
    [FECORDMED]                   DATETIME       NOT NULL,
    [CODSERIPS]                   CHAR (20)      NOT NULL,
    [CANSERIPS]                   INT            NOT NULL,
    [ESTSERIPS]                   CHAR (1)       NULL,
    [INDAUDFOR]                   NUMERIC (18)   NOT NULL,
    [ESTALEIMG]                   BIT            NOT NULL,
    [FECRECEXA]                   DATETIME       NULL,
    [NOMARCIMG]                   CHAR (250)     NULL,
    [CONCURRE]                    ROWVERSION     NULL,
    [USURECEXA]                   CHAR (20)      NULL,
    [REALINOTIF]                  BIT            NOT NULL,
    [NOMRESULT]                   VARCHAR (100)  NULL,
    [SERTRANSC]                   BIT            NULL,
    [SERVALMED]                   BIT            NULL,
    [CODUSUTRA]                   CHAR (20)      NULL,
    [CODPROVAL]                   CHAR (20)      NULL,
    [FECTRASER]                   DATETIME       NULL,
    [FECVALSER]                   DATETIME       NULL,
    [ESTTRASER]                   BIT            NULL,
    [CODENTIDA]                   CHAR (9)       NULL,
    [CODCONTRA]                   CHAR (6)       NULL,
    [CODPANATE]                   CHAR (2)       NULL,
    [IPFECHACO]                   DATETIME       NOT NULL,
    [NUMCONCIT]                   CHAR (20)      NULL,
    [OBSERVACI]                   VARCHAR (2000) NULL,
    [USUOCUREG]                   CHAR (20)      NULL,
    [OBSERVSER]                   VARCHAR (2000) NULL,
    [LECTURA]                     BIT            NULL,
    [MEDREALEC]                   CHAR (20)      NULL,
    [FECHLECT]                    DATETIME       NULL,
    [GENSERVICEORDER]             INT            NULL,
    [AUTO]                        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIENEGRABACION]              BIT            NULL,
    [SERREAINT]                   BIT            NULL,
    [FECHARESCLA]                 DATETIME       NULL,
    [CLASIFICABIRADS]             TINYINT        NULL,
    [GENCAREGROUP]                INT            NULL,
    [GENCONENTITY]                INT            NULL,
    [GENINVOICE]                  VARCHAR (20)   NULL,
    [GENINVOICEID]                INT            NULL,
    [INTERPRET]                   VARCHAR (4000) NULL,
    [CODPROINT]                   CHAR (20)      NULL,
    [NUMFOLINT]                   CHAR (10)      NULL,
    [ESPEREALIZA]                 CHAR (3)       NULL,
    [REGASITENCIA]                BIT            NULL,
    [IDDESCRIPCIONRELACIONADA]    NCHAR (10)     NULL,
    [RelativeURI]                 VARCHAR (500)  NULL,
    [IdServerOrderDetail]         INT            NULL,
    [ExamProfessionalCode]        CHAR (20)      NULL,
    [ExamSpecialtyCode]           CHAR (3)       NULL,
    [ExamTakenDate]               DATETIME       NULL,
    [SendToInterface]             INT            NULL,
    [SendToInterfaceProfessional] CHAR (20)      NULL,
    [SendToInterfaceDate]         DATETIME       NULL,
    [CodeCareCenterProcess]       CHAR (10)      NULL,
    [ExamCompletedDate]           DATETIME       NULL,
    [AttendingProfessionalCode]   CHAR (20)      NULL,
    [AttendingSpecialty]          CHAR (3)       NULL,
    [LATERALIDAD]                 INT            NULL,
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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDIMA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDIMA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO

CREATE NONCLUSTERED INDEX [IX_AMBORDIMA_CODSERIPS_CANSERIPS_CODCENATE_CODPROSAL_CONCURRE_ESTALEIMG_ESTSERIPS_FECORDMED]
    ON [dbo].[AMBORDIMA]([CODSERIPS] ASC)
    INCLUDE([CANSERIPS], [CODCENATE], [CODPROSAL], [CONCURRE], [ESTALEIMG], [NUMINGRES], [OBSERVACI], [OBSERVSER], [ESTSERIPS], [FECORDMED], [FECRECEXA], [IDDESCRIPCIONRELACIONADA], [IPCODPACI], [NUMCONCIT]);


GO
CREATE NONCLUSTERED INDEX [IDX_OrdenesMedicas]
    ON [dbo].[AMBORDIMA]([CODCENATE] ASC, [CODSERIPS] ASC)
    INCLUDE([CANSERIPS], [CODPROSAL], [CONCURRE], [ESTALEIMG], [OBSERVACI], [OBSERVSER], [ESTSERIPS], [FECORDMED], [FECRECEXA], [IPCODPACI], [NUMINGRES]);


GO
ALTER INDEX [IDX_OrdenesMedicas]
    ON [dbo].[AMBORDIMA] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IDX_ControlServiciosAmbulatorios]
    ON [dbo].[AMBORDIMA]([CODCENATE] ASC, [SERTRANSC] ASC, [SERVALMED] ASC, [ESTTRASER] ASC, [TIENEGRABACION] ASC)
    INCLUDE([CANSERIPS], [ESTALEIMG], [ESTSERIPS], [FECORDMED], [IPCODPACI], [NUMINGRES], [OBSERVACI], [CODPROSAL], [CODSERIPS], [CONCURRE]);


GO
ALTER INDEX [IDX_ControlServiciosAmbulatorios]
    ON [dbo].[AMBORDIMA] DISABLE;




GO
CREATE NONCLUSTERED INDEX [_dta_index_AMBORDIMA_6_2011922289__K3_K6_K2_K7_K1_K5_K28_K12]
    ON [dbo].[AMBORDIMA]([CODCENATE] ASC, [FECORDMED] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC, [IPCODPACI] ASC, [CODPROSAL] ASC, [IPFECHACO] ASC, [FECRECEXA] ASC);

GO
CREATE NONCLUSTERED INDEX IX_AMBORDIMA_Estado_Centro
ON dbo.AMBORDIMA (ESTSERIPS, CODCENATE)
INCLUDE (IPCODPACI, NUMINGRES, CODSERIPS, FECORDMED, 
         CODPROSAL, OBSERVACI, FECRECEXA, OBSERVSER, 
         LATERALIDAD, ESTALEIMG, CONCURRE, CANSERIPS, 
         NUMCONCIT, IDDESCRIPCIONRELACIONADA);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad (VARCHAR 3) del profesional de salud registrado como asistente en AttendingProfessionalCode. Identifica la rama médica (medicina general, radiología, cardiología, etc.) del profesional que atiende el examen imagenológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AttendingSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la especialidad del profesional registrado como profesional que asiste y guardado el la columna AttendingProfessionalCode', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AttendingSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AttendingSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (CHAR 20) del profesional de salud registrado como asistente o responsable clínico del examen. Referencia a INPROFSAL.CODPROSAL. Vincula el examen con el médico tratante o especialista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AttendingProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del profesional el cual fue registrado como profesional que asiste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AttendingProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AttendingProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) registrada en el dashboard de imagenología tecnólogo al momento de iniciar el procesamiento y captura del examen. Marca el inicio del flujo de atención en la modalidad imagenológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamTakenDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha que se registra como fecha de inicio del procesamiento en el dashboard de imagenologia tecnologo al momento de tomar el examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamTakenDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamTakenDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad (CHAR 3) seleccionada por el tecnólogo al tomar el examen en dashboard de imagenología solicitada. Determina la rama médica del servicio (radiología, ecografía, tomografía, resonancia, mamografía, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamSpecialtyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo de la especialidad seleccionada del profesional seleccionado al momento de tomar el examen en el dashboar de imagenologia tecnologo en examenes solicitados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamSpecialtyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamSpecialtyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional (CHAR 20) seleccionado al momento de tomar el examen en dashboard de imagenología de examenes solicitados. Referencia a INPROFSAL.CODPROSAL. Identifica al tecnólogo o profesional que realiza la captura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del profesional seleccionado al momento de tomar el examen en el dashboar de imagenologia tecnologo en examenes solicitados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del detalle o línea específica de la orden de servicios. Permite rastrear cada servicio imagenológico dentro de una orden maestro (GENSERVICEORDER).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el detalle Id de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta web o física relativa (VARCHAR 500) que identifica la ubicación del recurso imagenológico en el servidor. Ejemplo: /study/7/4. Se combina con URL base de contenedores para acceso completo a imagen, reporte o archivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'RelativeURI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'La URI Relativa se usa para identificar la ruta web o fisica en la que se encuentra el recurso, en este caso la imagen.    Ejemplo: URIRealtiva de imagen en indira: /study/7/4  La URL Base del recurso se guarda en la tabla de contenedores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'RelativeURI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'RelativeURI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (NCHAR 10) de descripción de relación con VIE ERP, vinculación a contract.CUPSEntityContractDescriptions. Conecta el servicio imagenológico con configuraciones de contrato y beneficio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que indica si el paciente completó el registro de asistencia cuando esta característica está implementada. 1=asistencia registrada, 0=pendiente. Controla trazabilidad de presentación a cita imagenológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'REGASITENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si esta implementada la carateristica de registro de asistencia, este campo determina si el paciente ya realizo el registro de asistencia o no.  true  false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'REGASITENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'REGASITENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad (CHAR 3) que realizó el procedimiento. Identifica la rama médica ejecutora del examen imagenológico (radiología, ecografía, tomografía, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especialidad Realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (CHAR 10) asociado a la interpretación radiológica. Referencia interna para vinculación de reportes e interpretaciones de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número Folio que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional médico (CHAR 20) que realiza la interpretación radiológica del examen. Referencia a INPROFSAL.CODPROSAL. Identifica el radiólogo o especialista intérprete.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del profesional que realiza la interpretación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la interpretación radiológica (VARCHAR 4000) redactada por el médico especialista. Contiene hallazgos, diagnósticos imagenológicos y recomendaciones clínicas del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Interpretación del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la factura generada en Indigo Vie Cloud para el servicio imagenológico. Vincula el examen con documento de cobro y gestión financiera/RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura (VARCHAR 20) generada en Indigo Vie Cloud para facturación del servicio. Identificador de documento de cobro y trazabilidad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Numero de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENINVOICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la Entidad Administradora de Salud (EAS/EAPB) del contrato. Se completa cuando el grupo de atención seleccionado es EAPB sin contrato definido. Vincula con entidad responsable de pago.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la Entidad Administradora de salud del contrato, este campo solo se llena si el Grupo de atencion que seleccione es de EAPB Sin Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del grupo de atención en VIE. Identifica el agrupador de servicios relacionados o ciclo de atención dentro del ecosistema facturación/RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del grupo de atencion de la base de datos de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación BIRADS (TINYINT): 0=requiere nuevo estudio, 1=negativo, 2=hallazgos benignos, 3=probablemente benigno, 4=anormalidad sospechosa, 5=altamente sospechoso malignidad, 6=malignidad confirmada. Usado en mamografía y exámenes de mama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CLASIFICABIRADS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la calificación BIRADS:  1- BIRADS 0: Necesidad de Nuevo Estudio Imagenológico o Mamograma previo para evaluación  2- BIRADS 1: Negativo  3- BIRADS 2: Hallazgos Benignos  4- BIRADS 3: Probablemente Benigno  5- BIRADS 4: Anormalidad Sospechosa  6- BIRADS 5: Altamente Sospechoso de Malignidad  7- BIRADS 6: Malignidad por Biopsia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CLASIFICABIRADS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CLASIFICABIRADS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del resultado de clasificación BIRADS o fecha de realización de mamografía. Marca el momento del análisis imagenológico de mama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECHARESCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Resultado de Clasificación, Fecha Mamografía  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECHARESCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECHARESCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el servicio se integra con interfaz RIS externa. 1=realiza interfaz, 0=no realiza. Controla enrutamiento a Indira o sistema externo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Realiza interfáz 1:si 0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERREAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica presencia de grabación de audio/video del procedimiento imagenológico. 1=tiene grabación, 0=sin grabación. Documentación multimedia del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'TIENEGRABACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene grabacion 1:si 0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'TIENEGRABACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'TIENEGRABACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (INT IDENTITY) clave primaria clustered de la tabla AMBORDIMA. Generador secuencial único para cada registro de orden imagenológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la orden de servicio maestra en la cual se incluyó el servicio imagenológico. Se completa cuando la orden se genera desde Control de Cuenta. Agrupa servicios relacionados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Imagenologia, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en la cual el médico o tecnólogo realizó la lectura/análisis del examen imagenológico. Marca validación de captura y procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECHLECT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que se realizó la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECHLECT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECHLECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del médico o profesional (CHAR 20) que realiza la lectura del examen imagenológico. Referencia a INPROFSAL.CODPROSAL. Identifica intérprete responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'MEDREALEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medico que realiza la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'MEDREALEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'MEDREALEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano (BIT) que contiene indicador de si existe grabación de audio/voz del médico durante la lectura interpretativa. Documentación de análisis verbal del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'LECTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que contiene la grabación hecha por el médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'LECTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'LECTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones (VARCHAR 2000) con notas adicionales sobre el servicio imagenológico, hallazgos secundarios, recomendaciones o aclaraciones técnicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'OBSERVSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'OBSERVSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'OBSERVSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (CHAR 20) que ocupa o realiza el registro del examen imagenológico en el sistema. Identifica responsable de captura inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'USUOCUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que ocupa el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'USUOCUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'USUOCUREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones generales (VARCHAR 2000) sobre la orden imagenológica, contexto clínico, antecedentes o datos relevantes para interpretación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo de cita (CHAR 20) registrado desde VIE al facturar la cita imagenológica diagnóstica. Referencia a AGASISITA. Vincula orden con agenda de atención y cita programa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Consecutivo Cita en Interfaz    Campo que se registra desde vie cuando se factura la cita de imagenes Dx que va a contener el ID de la cita es decir de la tabla AGASISITA.    Por medio de este campo voy a tener la relacion de la orden con la cita que esta asociada.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la consulta o atención clínica en la cual se solicitó el examen imagenológico. Marca momento de solicitud en contexto clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IPFECHACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IPFECHACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IPFECHACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de beneficios (CHAR 2) del paciente en el momento de la atención. Identifica cobertura, límites y autorizaciones para servicios imagenológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato (CHAR 6) entre paciente/entidad y prestador. Determina términos facturación, copago, cobertura de imagenología y reportería RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad aseguradora o EAS (CHAR 9) a la cual pertenece el paciente. Vincula examen con responsable de pago y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de transcripción (BIT) del servicio imagenológico. Indica si se realizó transcripción de resultados desde formato de captura a formato definitivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTTRASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Transcripcion del Servicio de Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTTRASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTTRASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de validación de la transcripción del servicio imagenológico. Marca aprobación de exactitud de datos transcriptos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECVALSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Validacion de la Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECVALSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECVALSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de transcripción de la imagen y resultados imagenológicos. Marca momento de conversión de datos capturados a formato final.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECTRASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Transcripcion del la Imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECTRASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECTRASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (CHAR 20) que valida la transcripción de resultados imagenológicos. Referencia a INPROFSAL.CODPROSAL. Control de calidad de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional que valida la Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (CHAR 20) responsable de realizar la transcripción de datos y resultados del examen imagenológico. Identifica operador de digitación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODUSUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Transcribe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODUSUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODUSUTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que indica si la transcripción del examen imagenológico ya fue validada por profesional autorizado. 1=validada, 0=pendiente validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERVALMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si la Transcripcion Fue ya Validada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERVALMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERVALMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que indica si el servicio imagenológico ya fue procesado (lectura realizada por tecnólogo o radiólogo). 1=procesado, 0=pendiente procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERTRANSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si el Servicio ya se le realizo la Lectura por el Tecnologo o Radiologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERTRANSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SERTRANSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo (VARCHAR 100) que contiene resultado o reporte final del examen imagenológico. Referencia a archivo físico o documentación generada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NOMRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NOMRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NOMRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que especifica si ya se realizó sincronización del examen con sistemas externos (RISTRACAB, RISTRADED, interfaz RIS). 1=sincronizado, 0=pendiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'REALINOTIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'especifica si ya realizo la sincronizacion con ristracab y ristraded', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'REALINOTIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'REALINOTIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (CHAR 20) que recibió o registró el examen imagenológico en el sistema. Identifica operador de ingreso inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'USURECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp (TIMESTAMP) de concurrencia optimista para control de conflictos en edición simultánea de registro imagenológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo adjunto (CHAR 250) que contiene imagen, estudio o documentación imagenológica. Referencia a archivo físico de examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NOMARCIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NOMARCIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NOMARCIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación o recepción del registro del examen imagenológico en el sistema. Marca ingreso inicial al EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECRECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECRECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECRECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de alerta (BIT) sobre el examen imagenológico. 1=alerta activa (hallazgo crítico, requiere seguimiento), 0=sin alerta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTALEIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTALEIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTALEIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18) asociado al examen imagenológico. Trazabilidad de validación, glosa o revisión de congruencia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio RIPS (CHAR 1): 1=solicitado, 2=muestra recolectada, 3=resultado entregado, 4=examen interpretado, 5=remitido, 6=anulado, 7=extramural, 8=realizando. Ciclo de vida de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural 8: Realizando', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades (INT) del servicio imagenológico CUPS solicitado. Número de exámenes del mismo tipo en la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS (CHAR 20) del procedimiento imagenológico solicitado. Referencia a INCUPSIPS.CODSERIPS. Identifica modalidad (radiología, ecografía, tomografía, resonancia, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de solicitud de la orden imagenológica. Marca momento clínico de indicación del examen por médico tratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (CHAR 20, PII) que solicitó el servicio imagenológico. Referencia a INPROFSAL.CODPROSAL. Identificador del médico ordenante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (CHAR 10) donde se solicita o realiza el examen imagenológico. Referencia a INUNIFUNC. Identifica departamento, área o servicio ejecutor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10) donde se realiza el examen imagenológico. Referencia a ADCENATEN. Identifica sede, clínica o hospital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso (CHAR 10) del paciente en el cual se solicita el examen imagenológico. Agrupa servicios dentro de un episodio de atención hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII, ofuscado en RIPS) asociado al examen imagenológico. Referencia a INPACIENT.IPCODPACI. Identificación única del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional (CHAR 20) que determina si el registro imagenológico se envía a interfaz RIS Indira. Identifica responsable de decisión de enrutamiento según modalidad (requiere enrutamiento manual, procesamiento automático en interfaz, o procesamiento en VieCloud).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SendToInterfaceProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'19-05-2025
 


Se crea campo para indentificar el profesional que determino la interfaz  de Indira o no, esto por el tema de ecografias que algunas no se procesan directamente desde la interfaz y por ende no debe llegar el registro a Indira.


', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SendToInterfaceProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SendToInterfaceProfessional';








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en la cual se determina y registra la decisión de envío a interfaz RIS Indira. Marca momento de evaluación de configuración de modalidad y enrutamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SendToInterfaceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'19-05-2025
 


Se crea campo para indentificar la fecha en la cual se determino la interfaz  de Indira o no, esto por el tema de ecografias que algunas no se procesan directamente desde la interfaz y por ende no debe llegar el registro a Indira.


', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SendToInterfaceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SendToInterfaceDate';








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enrutamiento (INT): 0=en espera de ser ruteado, 1=enviar a interfaz Indira (procesamiento en RIS externo), 2=no enviar a interfaz (procesamiento directo en VieCloud). Controla flujo imagenológico según modalidad configurada (requiere enrutamiento manual, procesamiento automático interfaz, o procesamiento VieCloud).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SendToInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'19-05-2025
 


Se crea campo para indentificar si el registro o servicio se envia a la interfaz de Indira o no, esto por el tema de ecografias que algunas no se procesan directamente desde la interfaz y por ende no debe llegar el registro a Indira.

Indira debe hacer lectura de este campo para identificar los registros que se deben listar en sus dashboard.

0- En espera de ser enrutado
1-  Se envia a la interfaz  de Indira

2 - No se envia a la interfaz de Indira se queda en VIE 


Resuemen: 

Si la interfaz RIS Indira está activa, se debe validar la modalidad del servicio (según el CUPS de la orden) y actuar de acuerdo con su configuración:

Si la modalidad está configurada como “Requiere enrutamiento”, el servicio debe visualizarse en el formulario “Enrutador de imágenes intrahospitalarias”, desde donde se realizará el enrutamiento manual correspondiente.

Si la modalidad está configurada como “Procesamiento en interfaz”, el servicio debe enviarse automáticamente a la interfaz RIS Indira activa.

Si la modalidad está configurada como “Procesamiento en VieCloud”, el servicio debe visualizarse directamente en el Dashboard de Imagenología Tecnólogo, sin pasar por la interfaz.





', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SendToInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'SendToInterface';








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de lateralidad (INT): 0=no aplica, 1=izquierda, 2=derecha, 3=bilateral, 4=multilateral. Especifica ubicación anatómica del examen imagenológico, usado en radiología, mamografía, ecografía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de Lateralidad:   0 - No aplica  1 - Izquierda  2 - Derecha  3 - Bilateral  4 -Multilateral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de imágenes y exámenes diagnósticos en consulta ambulatoria: registra cada servicio ordenado por un profesional de la salud para un paciente en un ingreso específico, incluyendo estado del examen, resultados, lecturas radiológicas, clasificación BI-RADS, facturación y trazabilidad del proceso desde la orden hasta la entrega del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o proceso asistencial donde se ejecuta el examen (centro de procesamiento interno).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CodeCareCenterProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'CodeCareCenterProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el examen o imagen diagnóstica quedó completamente finalizado y cerrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamCompletedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDIMA', @level2type = N'COLUMN', @level2name = N'ExamCompletedDate';
