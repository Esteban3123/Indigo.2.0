CREATE TABLE [dbo].[HCPARALERNOT] (
    [CODCONSEC]           TINYINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]           CHAR (10)   NOT NULL,
    [TIPSECALER]          CHAR (1)    NOT NULL,
    [ENVMAILUS]           BIT         NOT NULL,
    [ENVSMSUSU]           BIT         NOT NULL,
    [NOMCOMUSU]           CHAR (100)  NOT NULL,
    [NOMCORSMS]           NCHAR (10)  NOT NULL,
    [NUMTELMOV]           CHAR (10)   NOT NULL,
    [CORELEUSU]           NCHAR (50)  NOT NULL,
    [MENADICOR]           NCHAR (250) NOT NULL,
    [USUARIOCREACION]     CHAR (20)   NULL,
    [FECHACREACION]       DATETIME    NULL,
    [USUARIOMODIFICACION] CHAR (20)   NULL,
    [FECHAMODIFICACION]   DATETIME    NULL,
    CONSTRAINT [PK_HCPARALERNOT_1] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCPARALERNOT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARALERNOT_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARALERNOT_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de configuración de notificaciones; DATETIME; auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que realizó la última modificación del registro; FK a SEGusuaru; auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de configuración de notificaciones; DATETIME; auditoría de origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que creó el registro de configuración de notificaciones; FK a SEGusuaru; auditoría de origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje adicional personalizado para el envío de correos electrónicos en cada notificación de alertas; NVARCHAR(250); contenido PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'MENADICOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje Adicional para el envio de Correo Electronico en Cada Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'MENADICOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'MENADICOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico del usuario; NVARCHAR(50); contacto electrónico, PII_Ofuscado, notificaciones por email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CORELEUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo Electronico Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CORELEUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CORELEUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono móvil para envío de SMS y notificaciones; CHAR(10); contacto telefónico, PII_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NUMTELMOV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico Movil para el envio de SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NUMTELMOV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NUMTELMOV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre corto o alias del remitente para el envío de SMS; NCHAR(10); identificador de origen en notificaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NOMCORSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Corto para el Envio de SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NOMCORSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NOMCORSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario que recibe las notificaciones; CHAR(100); referencia personal, PII_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NOMCOMUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Completo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NOMCOMUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NOMCOMUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (0/1) que especifica si está habilitado el envío de notificaciones por SMS; BIT; canal de comunicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'ENVSMSUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se realiza el envio de SMS como Medio de Comunicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'ENVSMSUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'ENVSMSUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (0/1) que especifica si está habilitado el envío de notificaciones por correo electrónico; BIT; canal de comunicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'ENVMAILUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se realiza el envio de Correos Electronicos como Medio de Comunicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'ENVMAILUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'ENVMAILUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de sección o departamento para alertas: L=Laboratorio, I=Imagenología, C=Interconsulta; CHAR(1); clasificación de alertas clínicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'TIPSECALER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Seccion de Alertas  1: Laboratorio  2: Imagenologia  3: Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'TIPSECALER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'TIPSECALER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del centro de atención (sede, clínica, hospital); CHAR(10); FK a ADCENATEN; unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo único del registro de configuración de notificaciones; TINYINT IDENTITY; clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de alertas y notificaciones por centro de atención: define a qué usuarios enviar alertas (por correo electrónico o SMS), con sus datos de contacto y el mensaje adicional a incluir en la notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT';
