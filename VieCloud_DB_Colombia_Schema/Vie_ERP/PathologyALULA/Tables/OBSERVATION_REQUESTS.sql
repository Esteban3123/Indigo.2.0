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



GO



GO
CREATE NONCLUSTERED INDEX [IX_OBSERVATION_REQUESTS_identification]
    ON [PathologyALULA].[OBSERVATION_REQUESTS]([identification] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de sincronización: 0=No leído, 1=Leído. Indica si la orden ha sido procesada en el sistema ALULA.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'synchronization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bandera:  0 - No ledio    1 - Leido', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'synchronization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'synchronization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio remitente responsable en ALULA. Unidad funcional que solicita el estudio (laboratorio, patología, imagen).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'referring_service';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio remitente  (responsable Alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'referring_service';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'referring_service';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización asignado por ALULA. Referencia para validación de cobertura y aprobación de estudio.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'authorization_number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de autorizacion (responsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'authorization_number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'authorization_number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico de destino para envío de resultados. Responsable ALULA para notificación.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'email_destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo para envio de  resultados  (responsanle alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'email_destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'email_destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino de entrega de resultados. Centro de atención o unidad responsable ALULA para recepción.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Destino de resultados  (responsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo del identificador del caso en ALULA. Clasificación de tipo de caso para categorización.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'case_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prefijo del ID del caso  (responsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'case_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'case_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de complejidad del caso en ALULA. Indica grado de dificultad diagnóstica o recursos requeridos.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'complexity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complejidad del caso  (responsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'complexity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'complexity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del caso dentro del sistema ALULA. Referencia para seguimiento en patología.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'case_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del caso dentro de  ALULA  (reponsable alula)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'case_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'case_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medio de entrega de resultados. Forma de transmisión: correo, fax, sistema, impreso, etc.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_handling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Medio de entrega de  resultados ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_handling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_handling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información complementaria del servicio. Destino de solicitud remitida y detalles adicionales ALULA.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'filler_supplemental_service_information';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Destino de solicitud  remitida', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'filler_supplemental_service_information';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'filler_supplemental_service_information';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS del procedimiento ordenado. Identificación estándar de examen, laboratorio o diagnóstico solicitado.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'procedure_code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código CUPs de procedimiento ordenado', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'procedure_code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'procedure_code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del profesional intérprete principal. Médico patólogo o especialista responsable del diagnóstico.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'principal_result_interpreter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación intérprete', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'principal_result_interpreter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'principal_result_interpreter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razones clínicas para el estudio emitidas por médico remitente. Motivo, síntomas o diagnóstico presuntivo.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'reasons_for_study';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razones para el estudio emitidas por médico remitente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'reasons_for_study';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'reasons_for_study';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contacto o profesional para envío de copias de resultados. Destinatario adicional de la información diagnóstica.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_copies_to';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contacto de envío de resultados', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_copies_to';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_copies_to';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la orden. Código HL7 (P=Pending, C=Complete, F=Failed, X=Cancelled, etc.).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la orden', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de cambio de estado de la orden. Último cambio de estatus en el proceso diagnóstico.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_status_change_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de cambio deestado', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_status_change_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'result_status_change_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio remitente. Médico o unidad funcional que genera la solicitud de estudio.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'ordering_provider';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio remitente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'ordering_provider';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'ordering_provider';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información clínica relevante asociada a la orden. Antecedentes, síntomas, contexto clínico para interpretación.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'relevant_clinical_information';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información clínica relevante asociada a la orden', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'relevant_clinical_information';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'relevant_clinical_information';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de caso crítico. Código de alerta para hallazgos graves o de urgencia diagnóstica.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'danger_code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicador de caso crítico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'danger_code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'danger_code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del proceso de diagnóstico. Fecha de conclusión del estudio o análisis patológico.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'observation_end_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de proceso de diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'observation_end_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'observation_end_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del proceso de diagnóstico. Inicio de toma de muestra, examen o evaluación.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'observation_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de proceso de diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'observation_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'observation_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de solicitud de la orden. Timestamp de generación de la orden en el sistema.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'requested_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de solicitud', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'requested_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'requested_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad de la orden. Escala numérica: 1=Alta/Urgente, 2=Media/Rutina, 3=Baja/Programada.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'priority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad de la orden[1:high, 2:medium, 3:low]', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'priority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del caso para el sistema ALULA. Número de seguimiento en patología/laboratorio.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'filler_order_number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del caso para el sistema ALULA', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'filler_order_number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'filler_order_number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden para el sistema HIS. Número único de la solicitud en historia clínica electrónica.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'placer_order_number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la orden para el sistema HIS', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'placer_order_number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'placer_order_number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del paciente. Cédula, pasaporte u documento según tipo_identificacion (PII_Identificacion_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'identification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identificación. CC (Cédula Ciudadanía), CE (Extranjería), TI (Tarjeta Identidad), RC (Registro Civil), PA (Pasaporte), AS (Adulto Sin ID), MS (Menor Sin ID), NU (Número único), CN (Nacido Vivo), CD (Diplomático), SC (Salvoconducto), PE (Permiso Permanencia).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'type_identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Documento  CC -  Cédula de Ciudadanía  CE  -  Cédula de Extranjería  TI   -  Tarjeta de Identidad  RC  -  Registro Civil  PA  -  Pasaporte  AS  -  Adulto Sin Identificación  MS  -  Menor Sin Identificación  NU  -  Número único de identificación personal  CN  -  Certificado de Nacido Vivo  CD  -  Carnet Diplomático (Aplica para extranjeros)  SC  -  Salvoconducto (Aplica para extranjeros)  PE  -  Permiso especial de Permanencia (Aplica para extranjeros)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'type_identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'type_identification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del paciente asociado a la orden. FK a tabla INTEGRATION_PATIENTS para vinculación de historia clínica.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'patient_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del paciente asociado a la orden, llave foránea a la tabla INTEGRATION_PATIENTS', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'patient_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'patient_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la orden de observación. PK de la solicitud de estudio patológico o laboratorio.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único de la orden', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS', @level2type = N'COLUMN', @level2name = N'id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de observación o pedidos de exámenes de patología (laboratorio, histopatología u otros estudios diagnósticos). Registra cada orden médica enviada al sistema de patología ALULA, incluyendo el paciente, el procedimiento solicitado, el estado del resultado y la información clínica relevante.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'OBSERVATION_REQUESTS';
