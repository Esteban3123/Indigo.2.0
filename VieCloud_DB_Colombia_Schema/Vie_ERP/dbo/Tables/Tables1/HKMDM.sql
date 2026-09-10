CREATE TABLE [dbo].[HKMDM] (
    [ID]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [numero_solicitud]        INT             NULL,
    [codigo_interno_servicio] INT             NULL,
    [cups]                    VARCHAR (20)    NULL,
    [fecha_proceso]           DATETIME        NULL,
    [fecha_validacion]        DATE            NULL,
    [hora_validacion]         DATETIME        NULL,
    [codigo_tecnologo]        VARCHAR (20)    NULL,
    [registro_medico]         VARCHAR (150)   NULL,
    [nombre_medico]           VARCHAR (150)   NULL,
    [via_ingreso]             INT             NULL,
    [url_imagen]              VARCHAR (256)   NULL,
    [pdf]                     VARBINARY (MAX) NULL,
    [fecharegistro]           DATETIME        NULL,
    CONSTRAINT [PK_HKMDM] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación o inserción del registro en la tabla HKMDM; timestamp de ingreso al sistema de auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecharegistro';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecharegistro';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecharegistro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido binario (VARBINARY MAX) del informe o imagen en formato PDF; documento digital comprimido en base64 del resultado del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'pdf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagen en base64', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'pdf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'pdf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL o ruta de acceso a la imagen diagnóstica almacenada; enlace web o servidor DICOM para visualización del estudio radiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'url_imagen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URL de la imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'url_imagen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'url_imagen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso o modalidad de atención: 1=Ambulatorio, 2=Hospitalización, 3=Urgencias, 5=Cita prioritaria; código que clasifica el contexto clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'via_ingreso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: 1=Ambulatorio, 2=Hospitalización, 3=Urgencias, 5=Cita prioritaria. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'via_ingreso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'via_ingreso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del médico especialista, radiólogo o profesional de la salud que valida, autoriza o firma el informe de imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'nombre_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'nombre_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'nombre_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o registro profesional (cédula/matrícula) del médico radiólogo, especialista o profesional de la salud responsable del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'registro_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero registro medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'registro_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'registro_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del técnico radiólogo, tecnólogo en radiología o profesional que adquirió/procesó la imagen diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'codigo_tecnologo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo tecnologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'codigo_tecnologo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'codigo_tecnologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto (DATETIME) exactos de validación o aprobación de la imagen; timestamp de confirmación diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'hora_validacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'hora de validacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'hora_validacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'hora_validacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) en que se validó, aprobó o liberó el estudio de imagen por el profesional responsable; fecha de autorización del resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecha_validacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de validacion de la imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecha_validacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecha_validacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se procesó el estudio de imagen; marca temporal de ejecución del procedimiento diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecha_proceso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecha_proceso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecha_proceso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Clasificación Única de Procedimientos en Salud) del procedimiento diagnóstico realizado; código estándar RIPS para facturación y reportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno que identifica el servicio clínico o unidad funcional que solicita el estudio (radiología, ecografía, tomografía, resonancia, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo interno de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de solicitud único de la orden de estudio de imagen en VIE HIS; identificador de la orden de procedimiento radiológico, laboratorio o diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id unico de la orden de VIE HIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de la tabla HKMDM; clave primaria de registro de diagnóstico por imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de imágenes diagnósticas y documentos PDF asociados a solicitudes de servicios médicos, incluyendo datos del tecnólogo que realizó el proceso, el médico responsable y las fechas de validación. Usada para trazabilidad de resultados de imagenología (rayos X, ecografías, TAC, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM';
