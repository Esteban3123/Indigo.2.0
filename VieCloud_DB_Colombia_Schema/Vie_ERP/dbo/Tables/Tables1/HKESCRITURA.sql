CREATE TABLE [dbo].[HKESCRITURA] (
    [ID]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [numero_solicitud]        INT           NULL,
    [codigo_interno_servicio] INT           NULL,
    [cups]                    VARCHAR (20)  NULL,
    [fecha_validacion]        DATE          NULL,
    [hora_validacion]         TIME (7)      NULL,
    [resultados]              VARCHAR (MAX) NULL,
    [codigo_tecnologo]        VARCHAR (20)  NULL,
    [codigo_radiologo]        VARCHAR (20)  NULL,
    [registro_medico]         VARCHAR (20)  NULL,
    [nombre_medico]           VARCHAR (120) NULL,
    [doc_medico_remitente]    VARCHAR (20)  NULL,
    [nombre_medico_remitente] VARCHAR (120) NULL,
    [url_imagen]              VARCHAR (256) NULL,
    [via_ingreso]             INT           NULL,
    CONSTRAINT [PK_HKESCRITURA] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de ingreso del paciente (INT): 1=Ambulatorio, 2=Hospitalización, 3=Urgencias, 5=Cita prioritaria. Tipo de atención/acceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'via_ingreso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: 1=Ambulatorio, 2=Hospitalización, 3=Urgencias, 5=Cita prioritaria. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'via_ingreso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'via_ingreso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL/enlace de acceso a la imagen diagnóstica (VARCHAR 256). Ruta de almacenamiento de archivos de imagen radiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'url_imagen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Url de la Imagen ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'url_imagen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'url_imagen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del médico profesional que remite/ordena el estudio (VARCHAR 120). Médico solicitante, profesional que refiere.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'nombre_medico_remitente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del médico remitente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'nombre_medico_remitente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'nombre_medico_remitente';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento de identificación/cédula del médico remitente (VARCHAR 20). Identificación PII del profesional solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'doc_medico_remitente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Documento del médico remitente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'doc_medico_remitente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'doc_medico_remitente';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del médico validador o profesional tratante (VARCHAR 120). Médico que interpreta/valida el resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'nombre_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'nombre_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'nombre_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro profesional/licencia del médico en la entidad (VARCHAR 20). Identificación profesional, credencial sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'registro_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'registro_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'registro_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del radiólogo/especialista en imagenología (VARCHAR 20). Usuario profesional que valida/interpreta imágenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_radiologo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_radiologo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_radiologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del tecnólogo/técnico en radiología (VARCHAR 20). Usuario que adquiere/procesa imágenes diagnósticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_tecnologo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_tecnologo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_tecnologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de hallazgos y conclusiones del examen validado (VARCHAR MAX). Informe radiológico, interpretación de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'resultados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exámen validado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'resultados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'resultados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de validación/firma del estudio (TIME HH:MM:SS). Momento exacto de aprobación del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'hora_validacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de envio  HH:MM:SS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'hora_validacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'hora_validacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de validación/firma del estudio (DATE YYYY-MM-DD). Día de aprobación del resultado, timestamp de envío.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'fecha_validacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio YYYY-MM-DD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'fecha_validacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'fecha_validacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimiento en salud (VARCHAR 20). Clasificación normativa de procedimiento/servicio sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Códificacion única de procedimientos en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del servicio/departamento de origen (INT). Referencia a unidad funcional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código interno del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único identificador de solicitud de estudio (INT). Llave de referencia para rastrear orden radiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador y llave primaria, código único de solictud.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY). Clave primaria de la tabla de escritura de resultados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autoincrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de escrituras e informes de estudios de imágenes diagnósticas (radiología, ecografía, etc.), incluyendo resultados, profesionales intervinientes y enlace a la imagen digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA';
