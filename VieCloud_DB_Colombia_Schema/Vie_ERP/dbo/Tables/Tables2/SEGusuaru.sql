CREATE TABLE [dbo].[SEGusuaru] (
    [CODUSUARI]  CHAR (20)     NOT NULL,
    [NOMUSUARI]  CHAR (60)     NOT NULL,
    [PASSUSUAR]  CHAR (50)     NOT NULL,
    [CODUSUDGH]  CHAR (50)     NULL,
    [USUACTIVO]  BIT           NOT NULL,
    [CODIGOROL]  CHAR (4)      NOT NULL,
    [CODGRUPOU]  CHAR (3)      NOT NULL,
    [USUEMAILC]  CHAR (60)     NULL,
    [USUEMAILE]  CHAR (60)     NULL,
    [DESCARUSU]  VARCHAR (150) NULL,
    [TIPPERUSU]  CHAR (1)      NOT NULL,
    [PERASISTE]  CHAR (1)      NULL,
    [CODCENATE]  CHAR (10)     NULL,
    [UFUCODIGO]  CHAR (10)     NULL,
    [SOLCAMCON]  BIT           NOT NULL,
    [DIACAMCON]  INT           NOT NULL,
    [FECULTCAM]  DATETIME      NOT NULL,
    [FECADCUE]   DATETIME      NULL,
    [USUADMINI]  BIT           NULL,
    [TOKEN]      CHAR (100)    NULL,
    [EXPIRATION] DATETIME      NULL,
    CONSTRAINT [PK_SEGusuaru] PRIMARY KEY CLUSTERED ([CODUSUARI] ASC),
    CONSTRAINT [FK_SEGusuaru_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_SEGusuaru_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_SEGusuaru_SEGgruusu] FOREIGN KEY ([CODGRUPOU]) REFERENCES [dbo].[SEGgruusu] ([codgrupou]),
    CONSTRAINT [FK_SEGusuaru_SEGrolesu] FOREIGN KEY ([CODIGOROL]) REFERENCES [dbo].[SEGrolesu] ([codigorol])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de expiración del token de autenticación (DATETIME). Define cuándo caduca la sesión o credencial del usuario en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'EXPIRATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de expiración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'EXPIRATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'EXPIRATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token de autenticación (CHAR 100). Identificador único generado para sesiones de usuario, integración API o acceso temporal al sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'TOKEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el token', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'TOKEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'TOKEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de usuario administrador (BIT). 1=Sí es administrador del sistema, 0/NULL=No. Controla acceso a configuración global y gestión de usuarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario administrador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de caducidad o vencimiento de la cuenta de usuario (DATETIME). Determina cuándo la credencial del usuario expira automáticamente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'FECADCUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Caducidad de la Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'FECADCUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'FECADCUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último cambio de contraseña (DATETIME). Rastrea cuándo el usuario cambió su contraseña por última vez.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'FECULTCAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Ultimo Cambio de Contraseña', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'FECULTCAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'FECULTCAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciclo en días para obligar cambio de contraseña (INT). Número de días permitidos antes de requerir renovación de contraseña por política de seguridad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'DIACAMCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciclo en dias para el cambio de contraseña', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'DIACAMCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'DIACAMCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitar cambio de contraseña en próximo inicio de sesión (BIT). 1=Fuerza cambio obligatorio al siguiente login, 0=No requiere cambio inmediato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'SOLCAMCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Solicitar cambio de contraseña en el proximo inicio de sesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'SOLCAMCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'SOLCAMCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional (CHAR 10, FK→INUNIFUNC). Identifica a qué unidad funcional o departamento pertenece el usuario (médico, enfermería, laboratorio, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (CHAR 10, FK→ADCENATEN). Identifica la institución, clínica, hospital o sede donde labora el usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil asistencial del usuario (CHAR 1). Define dashboard y acceso: 1=Médico, 2=Enfermería, 3=Interconsultas, 4=Terapias, 5=Laboratorio, 6=Imagenología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'PERASISTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil Asistencial:  1= DashBoard Medico  2= DashBoard Enfermeria  3= DashBoard Interconsultas  4= DashBoard Terapias  5= DashBoard Laboratorio  6= DashBoard Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'PERASISTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'PERASISTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de perfil de usuario (CHAR 1). 1=Administrativo (gestión de configuración, usuarios, nómina), 2=Asistencial (atención clínica, pacientes, diagnósticos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'TIPPERUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el Tipo de Perfil:  1= Administrativo  2= Asistencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'TIPPERUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'TIPPERUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo o descripción del puesto del usuario (VARCHAR 150). Especifica la función, profesión o rol laboral (médico, enfermero, auxiliar, administrativo, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'DESCARUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'DESCARUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'DESCARUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico corporativo del usuario (CHAR 60, PII Identification_Ofuscado). Email para comunicaciones institucionales y notificaciones del EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUEMAILE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo Electronico del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUEMAILE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUEMAILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SIP usuario o correo institucional secundario (CHAR 60, PII Identification_Ofuscado). Identificador de comunicación alternativa en el sistema de información de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUEMAILC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'SIP Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUEMAILC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUEMAILC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de usuario (CHAR 3, FK→SEGgruusu). Agrupa usuarios por familia de permisos, roles o secciones del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODGRUPOU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODGRUPOU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODGRUPOU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del rol de usuario (CHAR 4, FK→SEGrolesu). Rol que define permisos, accesos y funcionalidades disponibles en el ERP/EHR (médico, farmacéutico, contador, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODIGOROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODIGOROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODIGOROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo del usuario (BIT). 1=Usuario activo con acceso al sistema, 0=Usuario inactivo/deshabilitado sin permisos de login.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Activo 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario en sistema DGH externo (CHAR 50). Identificador del usuario en sistema integrado DGH para sincronización de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODUSUDGH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODUSUDGH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODUSUDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña del usuario (CHAR 50, PII Password_Ofuscado). Credencial encriptada para autenticación. Nunca exponible en reportes o búsquedas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'PASSUSUAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contraseña del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'PASSUSUAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'PASSUSUAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario (CHAR 60, PII Identification_Ofuscado). Identidad humana del profesional de salud, administrativo o técnico en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'NOMUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'NOMUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'NOMUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (CHAR 20, PK). Identificador único y principal del usuario, equivalente a login, ID de usuario o código de empleado en Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de usuarios del sistema de seguridad: guarda las credenciales, roles, permisos y datos de acceso de cada usuario que opera en Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru';
