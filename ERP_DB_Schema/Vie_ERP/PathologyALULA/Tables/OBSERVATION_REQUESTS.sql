CREATE TABLE [PathologyALULA].[OBSERVATION_REQUESTS] (
    [id]                                      VARCHAR (16)   NOT NULL,
    [patient_id]                              VARCHAR (16)   NOT NULL,
    [type_identification]                     VARCHAR (2)    NOT NULL,
    [identification]                          VARCHAR (20)   NOT NULL,
    [placer_order_number]                     INT            NOT NULL,
    [filler_order_number]                     VARCHAR (100)  NULL,
    [priority]                                INT            NOT NULL,
    [requested_date]                          DATETIME       NOT NULL,
    [observation_date]                        VARCHAR (500)  NULL,
    [observation_end_date]                    DATETIME       NULL,
    [danger_code]                             VARCHAR (150)  NULL,
    [relevant_clinical_information]           VARCHAR (2000) NULL,
    [ordering_provider]                       VARCHAR (150)  NULL,
    [result_status_change_date]               DATETIME       NULL,
    [result_status]                           VARCHAR (3)    NULL,
    [result_copies_to]                        VARCHAR (150)  NULL,
    [reasons_for_study]                       VARCHAR (150)  NULL,
    [principal_result_interpreter]            VARCHAR (150)  NULL,
    [procedure_code]                          VARCHAR (20)   NOT NULL,
    [filler_supplemental_service_information] VARCHAR (150)  NULL,
    [result_handling]                         VARCHAR (150)  NULL,
    [case_id]                                 VARCHAR (100)  NULL,
    [complexity]                              VARCHAR (150)  NULL,
    [case_type]                               VARCHAR (150)  NULL,
    [result_destination]                      VARCHAR (150)  NULL,
    [email_destination]                       VARCHAR (150)  NULL,
    [authorization_number]                    VARCHAR (150)  NULL,
    [referring_service]                       VARCHAR (150)  NULL,
    [synchronization]                         BIT            NULL,
    CONSTRAINT [PK_OBSERVATION_REQUESTS] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_OBSERVATION_REQUESTS_INTEGRATION_PATIENTS] FOREIGN KEY ([patient_id]) REFERENCES [PathologyALULA].[INTEGRATION_PATIENTS] ([id]),
    CONSTRAINT [FK_OBSERVATION_REQUESTS_PATHOLOGYINTEGRATION] FOREIGN KEY ([placer_order_number]) REFERENCES [PathologyALULA].[INTEGRATIONCONTROL] ([id])
);


GO
ALTER TABLE [PathologyALULA].[OBSERVATION_REQUESTS] NOCHECK CONSTRAINT [FK_OBSERVATION_REQUESTS_INTEGRATION_PATIENTS];


GO
ALTER TABLE [PathologyALULA].[OBSERVATION_REQUESTS] NOCHECK CONSTRAINT [FK_OBSERVATION_REQUESTS_PATHOLOGYINTEGRATION];


GO
CREATE NONCLUSTERED INDEX [IX_OBSERVATION_REQUESTS_identification]
    ON [PathologyALULA].[OBSERVATION_REQUESTS]([identification] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bandera:  0 - No ledio    1 - Leido', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'synchronization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio remitente  (responsable Alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'referring_service';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de autorizacion (responsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'authorization_number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo para envio de  resultados  (responsanle alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'email_destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Destino de resultados  (responsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prefijo del ID del caso  (responsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'case_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complejidad del caso  (responsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'complexity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del caso dentro de  ALULA  (reponsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'case_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Medio de entrega de  resultados ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_handling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Destino de solicitud  remitida', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'filler_supplemental_service_information';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código CUPs de procedimiento ordenado', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'procedure_code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación intérprete', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'principal_result_interpreter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Razones para el estudio emitidas por médico remitente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'reasons_for_study';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contacto de envío de resultados', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_copies_to';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la orden', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de cambio deestado', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_status_change_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio remitente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'ordering_provider';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información clínica relevante asociada a la orden', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'relevant_clinical_information';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicador de caso crítico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'danger_code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final de proceso de diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'observation_end_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio de proceso de diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'observation_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de solicitud', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'requested_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad de la orden[1:high, 2:medium, 3:low]', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del caso para el sistema ALULA', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'filler_order_number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la orden para el sistema HIS', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'placer_order_number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'identification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento  CC -  Cédula de Ciudadanía  CE  -  Cédula de Extranjería  TI   -  Tarjeta de Identidad  RC  -  Registro Civil  PA  -  Pasaporte  AS  -  Adulto Sin Identificación  MS  -  Menor Sin Identificación  NU  -  Número único de identificación personal  CN  -  Certificado de Nacido Vivo  CD  -  Carnet Diplomático (Aplica para extranjeros)  SC  -  Salvoconducto (Aplica para extranjeros)  PE  -  Permiso especial de Permanencia (Aplica para extranjeros)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'type_identification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único del paciente asociado a la orden, llave foránea a la tabla INTEGRATION_PATIENTS', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'patient_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único de la orden', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'id';

