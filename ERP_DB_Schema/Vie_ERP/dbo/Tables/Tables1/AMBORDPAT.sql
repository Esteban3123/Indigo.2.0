CREATE TABLE [dbo].[AMBORDPAT] (
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [FECORDMED]                DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                INT                                                                              NOT NULL,
    [ESTSERIPS]                CHAR (1)                                                                         NULL,
    [INDAUDFOR]                NUMERIC (18)                                                                     NOT NULL,
    [ESTALEPAT]                BIT                                                                              NOT NULL,
    [FECRECEXA]                DATETIME                                                                         NULL,
    [NOMARCPAT]                CHAR (250)                                                                       NULL,
    [CONCURRE]                 ROWVERSION                                                                       NULL,
    [USURECEXA]                CHAR (20)                                                                        NULL,
    [CODENTIDA]                CHAR (9)                                                                         NULL,
    [CODCONTRA]                CHAR (6)                                                                         NULL,
    [CODPANATE]                CHAR (2)                                                                         NULL,
    [IPFECHACO]                DATETIME                                                                         NOT NULL,
    [NUMCONCIT]                CHAR (20)                                                                        NULL,
    [OBSERVACI]                VARCHAR (2000)                                                                   NULL,
    [RESULTADO]                VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')        NULL,
    [FECHARESULT]              DATETIME                                                                         NULL,
    [BIOSENCER]                TINYINT MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [BIOSENBACAF]              TINYINT MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [FECSENCER]                DATETIME                                                                         NULL,
    [FECSENBACAF]              DATETIME                                                                         NULL,
    [PROFRESULTADO]            CHAR (10)                                                                        NULL,
    [GENSERVICEORDER]          INT                                                                              NULL,
    [AUTO]                     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENCAREGROUP]             INT                                                                              NULL,
    [GENCONENTITY]             INT                                                                              NULL,
    [GENINVOICE]               VARCHAR (20)                                                                     NULL,
    [GENINVOICEID]             INT                                                                              NULL,
    [FECRECEPMUES]             DATETIME                                                                         NULL,
    [CODSERIPSREAL]            CHAR (20)                                                                        NULL,
    [ESPEREALIZA]              CHAR (3)                                                                         NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [CAC23]                    DATE                                                                             NULL,
    [CAC24]                    DATE                                                                             NULL,
    [CAC27]                    INT                                                                              NULL,
    [CAC28]                    INT                                                                              NULL,
    [CAC139]                   INT                                                                              NULL,
    [CAC140]                   INT                                                                              NULL,
    [CAC141]                   INT                                                                              NULL,
    [SYNCMIRTH]                BIT                                                                              NULL,
    [Laterality]               INT                                                                              NULL,
    [Specimen]                 INT                                                                              NULL,
    [Technique]                INT                                                                              NULL,
    [CollectionMedium]         INT                                                                              NULL,
    CONSTRAINT [PK_AMBORDPAT] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_AMBORDPAT_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDPAT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_CollectionMedium] FOREIGN KEY ([CollectionMedium]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_Specimen] FOREIGN KEY ([Specimen]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_Technique] FOREIGN KEY ([Technique]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_AMBORDPAT_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDPAT_INCUPSIPS1] FOREIGN KEY ([CODSERIPSREAL]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDPAT_INESPECIA] FOREIGN KEY ([ESPEREALIZA]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AMBORDPAT_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AMBORDPAT_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_ADcenaten];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_ADINGRESO];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_CollectionMedium];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_Specimen];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_Technique];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INCUPSIPS];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INCUPSIPS1];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INESPECIA];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INPacient];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medio de recolección campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Medio de recolección    Relacion con la tabla:  ClinicalParameters.DefinitionSamples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CollectionMedium';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tecnica: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Tecnica    Relacion con la tabla:  ClinicalParameters.DefinitionSamples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Technique';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especimen: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Especimen     Relacion con la tabla:  ClinicalParameters.DefinitionSamples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Specimen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 = No aplica   1 = Izquierda   2 = Derecha   3 = Ambos       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Laterality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La patología cumple con lo criterios de calidad (Pregunta 141).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC141';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ganglios positivos. (Pregunta 140).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC140';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ganglios extraídos reportados en el informe. (Pregunta 139)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC139';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grado de diferenciación del tumor (pregunta 28 CAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Histologia del tumor (pregunta 27)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del informe - No se hace la pregunta si existe resultado en ALULA y el dato se saca de la tabla de integración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC24';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de recolección de muestra(s) (Pregunta 23).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especialidad Realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios Realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPSREAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción de muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECRECEPMUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Numero de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENINVOICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la Entidad Administradora de salud del contrato, este campo solo se llena si el Grupo de atencion que seleccione es de EAPB Sin Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del grupo de atencion de la base de datos de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Patologia, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que devuelve el resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'PROFRESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de resultado de Biopsia de seno por bacaf', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECSENBACAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de resultado de Biopsia Seno Cervical', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECSENCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'resultado de biopsia de seno por bacaf  1-> Benigna  2-> Atípica (Interdeterminada)  3-> Malignidad Sospechosa/Probable  4-> Maligna  5-> No Satisfactoria  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENBACAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de Biopsia cervical  1-> Negativo para Neoplasia  2-> Infección por VPH  3-> NIC de Bajo Grado - NIC I  4-> NIC de Alto Grado: NIC II - NIC III  5-> Neoplasia Micro infiltrante:   Escamocelular o Adenocarcinoma  6-> Neoplasia Infiltrante: Escamocelular o Adenocarcinoma  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de recepción del informe patológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECHARESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de patología que devuelve la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Consecutivo Cita en Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IPFECHACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del plan de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'USURECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concurrecia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECRECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESTALEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

