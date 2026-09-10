CREATE TABLE [dbo].[HKLECTURA] (
    [tipo_doc]                 VARCHAR (2)                                                            NULL,
    [documento]                VARCHAR (20)                                                           NULL,
    [primer_nombre]            VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)') NULL,
    [segundo_nombre]           VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)') NULL,
    [primer_apellido]          VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)') NULL,
    [segundo_apellido]         VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)') NULL,
    [telefono]                 VARCHAR (50)                                                           NULL,
    [celular]                  VARCHAR (50)                                                           NULL,
    [direccion]                VARCHAR (256) MASKED WITH (FUNCTION = 'default()')                     NULL,
    [correo]                   VARCHAR (40)                                                           NULL,
    [sexo]                     VARCHAR (1)                                                            NULL,
    [rh_sanguineo]             VARCHAR (4)                                                            NULL,
    [Fecha_nacimiento]         DATE                                                                   NULL,
    [zona_residencial]         VARCHAR (1)                                                            NULL,
    [codigo_departamento]      VARCHAR (2)                                                            NULL,
    [nombre_departamento]      VARCHAR (100)                                                          NULL,
    [codigo_municipio]         VARCHAR (5)                                                            NULL,
    [nombre_municipio]         VARCHAR (100)                                                          NULL,
    [cups]                     VARCHAR (20)                                                           NULL,
    [nombre_cups]              VARCHAR (256)                                                          NULL,
    [modalidad]                VARCHAR (10)                                                           NULL,
    [codigo_interno_servicio]  INT                                                                    NULL,
    [centro_costo_solicitante] INT                                                                    NULL,
    [centro_costo_responde]    INT                                                                    NULL,
    [codigo_medico]            VARCHAR (50)                                                           NULL,
    [nombre_medico]            VARCHAR (120)                                                          NULL,
    [especialidad_solicitante] VARCHAR (150)                                                          NULL,
    [via_ingreso]              INT                                                                    NULL,
    [campo_urgencia]           CHAR (1)                                                               NULL,
    [codigo_cie_10]            VARCHAR (7)                                                            NULL,
    [descripción_cie_10]       VARCHAR (200)                                                          NULL,
    [nombre_eps]               VARCHAR (256)                                                          NULL,
    [nit_eps]                  VARCHAR (11)                                                           NULL,
    [fecha_solicitud]          DATE                                                                   NULL,
    [hora_solicitud]           TIME (7)                                                               NULL,
    [observacion]              VARCHAR (MAX)                                                          NULL,
    [justificacion]            VARCHAR (4000)                                                         NULL,
    [lectura]                  BIT                                                                    NULL,
    [prioridad]                VARCHAR (1)                                                            NULL,
    [estado_his]               VARCHAR (2)                                                            NULL,
    [cancelacion_his]          VARCHAR (1)                                                            NULL,
    [numero_solicitud]         INT                                                                    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [auto_imagen_his]          INT                                                                    NOT NULL,
    [url_imagen]               VARCHAR (256)                                                          NULL,
    CONSTRAINT [PK_HKLECTURA] PRIMARY KEY CLUSTERED ([numero_solicitud] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HKLECTURA].[primer_nombre]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HKLECTURA].[segundo_nombre]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HKLECTURA].[primer_apellido]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HKLECTURA].[segundo_apellido]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HKLECTURA].[direccion]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(256). URL completa de la imagen almacenada en repositorio; enlace de acceso a imagen diagnóstica digitalizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'url_imagen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URL de la imagen ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'url_imagen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'url_imagen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador/autonumérico de imagen en HIS; clave foránea que guarda URL completa de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'auto_imagen_his';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuarda   URL completa imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'auto_imagen_his';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'auto_imagen_his';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY. Identificador único y llave primaria de solicitud de orden; código secuencial de procedimiento/examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador y llave primaria, código único de solictud.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(1). Bandera booleana (0=activo, 1=cancelado); indica si HIS envió cancelación de orden marcando con ''''t'''' o similar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'cancelacion_his';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bandera por defecto 0  que nos informa si el HIS nos envia una cancelación marcando el campo con t  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'cancelacion_his';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'cancelacion_his';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(2). Estado sincronizado con RIS: RC=Registro recibido, PR=En proceso RIS, LE=Lectura registrada, RE=Validado, CA=Cancelado RIS, CH=Cancelación HIS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'estado_his';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se cambia e nombre del campo de estado por estado_his y los estados también cambian y seran los siguientes: RC= Registro recibido por el RIS, PR= En proceso marcado por el RIS, LE= Cuando el registro contenga una lectura, RE=El registro ha sido validado, CA=Cuando el RIS cancele la orden, CH=Cuando el HIS envie una cancelación al RIS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'estado_his';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'estado_his';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(1). Nivel de urgencia de orden: 1=Media, 2=Alta, 3=Baja; priorización de examen/procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'prioridad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1=Media, 2=Alta, 3=Baja.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'prioridad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'prioridad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de lectura (false=no leído, true=leído); valida que IMEXHS procesó el registro de orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'lectura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para validar que IMEXHS leyo el registro, valor por defecto f y cuando sgrel RIS cambia a t.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'lectura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'lectura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(4000). Justificación clínica de la orden/solicitud de procedimiento; motivo y razonamiento médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'justificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de la orden  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'justificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'justificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Observaciones adicionales de la orden; notas clínicas y aclaraciones de solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obsrevaciones de la orden  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'observacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIME. Hora de creación de la orden (HH:MM:SS); timestamp horario de generación de solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'hora_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de solicitud de la orden HH:MM:SS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'hora_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'hora_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de creación de la orden (YYYY-MM-DD); timestamp de generación de solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'fecha_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de solicitud de la orden YYYY-MM-DD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'fecha_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'fecha_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). NIT del convenio/aseguradora; identificador fiscal del plan de salud contratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nit_eps';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del convenio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nit_eps';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nit_eps';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(256). Nombre del convenio/aseguradora del paciente; entidad de salud contratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_eps';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del convenio al que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_eps';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_eps';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(200). Nombre/descripción del código diagnóstico CIE-10; diagnóstico clínico codificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'descripción_cie_10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del CIE10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'descripción_cie_10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'descripción_cie_10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(7). Código diagnóstico CIE-10 OMS; clasificación internacional de enfermedad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_cie_10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código CIE10  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_cie_10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_cie_10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(1). Bandera urgencia (N=no urgente, S=sí urgente); marca si orden es de emergencia/urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'campo_urgencia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'N=no, S=si  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'campo_urgencia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'campo_urgencia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Tipo de ingreso: 1=Ambulatorio, 2=Hospitalización, 3=Urgencias, 5=Cita prioritaria; forma de atención paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'via_ingreso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: 1=Ambulatorio, 2=Hospitalización, 3=Urgencias, 5=Cita prioritaria.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'via_ingreso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'via_ingreso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(150). Especialidad médica que solicita procedimiento; área clínica generadora de orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'especialidad_solicitante';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: Medeicina general=1  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'especialidad_solicitante';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'especialidad_solicitante';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(120). Nombre del profesional de salud que genera la orden; médico solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quien genera la orden  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50). Código/registro del profesional de salud solicitante; identificador del médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo_medico  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_medico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Centro de costo/unidad funcional que ejecuta el procedimiento; área que atiende la solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'centro_costo_responde';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Área del hospital que responde la solicitud del procedimiento  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'centro_costo_responde';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'centro_costo_responde';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Centro de costo/unidad funcional que solicita el procedimiento; área que genera la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'centro_costo_solicitante';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Área del hospital que hace la solicitud del procedimiento.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'centro_costo_solicitante';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'centro_costo_solicitante';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Código interno del servicio/unidad funcional; identificador local del departamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código interno del servicio.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(10). Modalidad/tipo de estudio radiológico: RX=Radiografía, DX=Densitometría, MG=Mamografía, CT=TAC, RM=Resonancia, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'modalidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: RX, DX, MG, CT, RM, etc.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'modalidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'modalidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(256). Nombre del procedimiento/estudio CUPS; denominación del servicio codificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del estudio, se cambio tipo de campo de text a varchar  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Código CUPS (Codificación Única de Procedimientos en Salud) Colombia; código único de procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Códificacion única de procedimientos en salud.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Nombre del municipio de residencia del paciente; ciudad/localidad geográfica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_municipio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: Medellín  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_municipio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_municipio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(5). Código DANE del municipio; identificador administrativo municipal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_municipio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: 001  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_municipio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_municipio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Nombre del departamento de residencia del paciente; región geográfica colombiana.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_departamento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: Antioquia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_departamento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_departamento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(2). Código DANE del departamento; identificador administrativo regional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_departamento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: 05  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_departamento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_departamento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(1). Zona de residencia (U=Urbana, R=Rural); clasificación geográfica de domicilio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'zona_residencial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Urbana=U, Rural=R  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'zona_residencial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'zona_residencial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de nacimiento del paciente (YYYY-MM-DD); data para cálculo de edad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'Fecha_nacimiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'YYYY-MM-DD  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'Fecha_nacimiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'Fecha_nacimiento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(4). Tipo de sangre/factor Rh: O-, O+, A-, A+, B-, B+, AB-, AB+; grupo sanguíneo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'rh_sanguineo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ejemplo: O-, A-, AB+, AB-, A+, etc.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'rh_sanguineo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'rh_sanguineo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(1). Sexo biológico (M=Hombre/Masculino, F=Mujer/Femenino); género del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'sexo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hombre=M, Mujer=F  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'sexo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'sexo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(40). Correo electrónico del paciente; email de contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'correo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'correo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'correo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(256) MASKED default(). Dirección de residencia del paciente; domicilio (PII ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'direccion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'direccion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'direccion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50). Número de teléfono celular del paciente; móvil de contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'celular';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de celular  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'celular';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'celular';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50). Número de teléfono fijo del paciente; línea telefónica de contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'telefono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono fijo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'telefono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'telefono';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(30) MASKED Name_Ofuscado. Segundo apellido del paciente; apellido materno (PII ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'segundo_apellido';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apellido del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'segundo_apellido';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'segundo_apellido';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(30) MASKED Name_Ofuscado. Primer apellido del paciente; apellido paterno (PII ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'primer_apellido';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apellido del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'primer_apellido';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'primer_apellido';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(30) MASKED Name_Ofuscado. Segundo nombre del paciente; nombre adicional (PII ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'segundo_nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'segundo_nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'segundo_nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(30) MASKED Name_Ofuscado. Primer nombre del paciente; nombre de pila (PII ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'primer_nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'primer_nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'primer_nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Número de documento de identificación del paciente; cédula/identificación (PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'documento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del documento  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'documento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'documento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(2). Tipo de documento: CC=Cédula ciudadanía, TI=Tarjeta identidad, CE=Cédula extranjería, PA=Pasaporte, RC=Registro civil; clase de identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'tipo_doc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CC= Cédula de ciudadanía, TI= Tarjeta de identidad, CE= Cédula de extranjería, PA= Pasaporte, RC= Registro civil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'tipo_doc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'tipo_doc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de solicitudes de lectura de imágenes diagnósticas (radiografías, ecografías, tomografías, etc.). Contiene los datos del paciente, el servicio solicitado (CUPS), el médico solicitante, el diagnóstico CIE-10, la EPS, el estado de la lectura y el enlace a la imagen, permitiendo el seguimiento del proceso de interpretación radiológica o de imágenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA';
