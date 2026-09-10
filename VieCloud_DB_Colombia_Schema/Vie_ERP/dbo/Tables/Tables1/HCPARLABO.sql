CREATE TABLE [dbo].[HCPARLABO] (
    [CODCENATE]           CHAR (10)     NOT NULL,
    [INTLABOAC]           BIT           NOT NULL,
    [URISERVIC]           VARCHAR (250) NULL,
    [PASSINTER]           VARCHAR (150) NULL,
    [EXAENUF]             BIT           CONSTRAINT [DF_HCPARLABO_EXAENUF] DEFAULT ((0)) NOT NULL,
    [TIPOPROVEEDOR]       INT           NULL,
    [USUARIOAUTHBASICA]   VARCHAR (150) NULL,
    [MENSAJERECIBIDO]     VARCHAR (250) NULL,
    [USUARIOCREACION]     CHAR (20)     NULL,
    [FECHACREACION]       DATETIME      NULL,
    [USUARIOMODIFICACION] CHAR (20)     NULL,
    [FECHAMODIFICACION]   DATETIME      NULL,
    CONSTRAINT [PK_HCPARLABO] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_HCPARLABO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARLABO_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARLABO_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de configuración de interfaz con laboratorio (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que realizó la última modificación del registro; FK a SEGusuaru.CODUSUARI (auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de configuración de interfaz con laboratorio (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que creó el registro; FK a SEGusuaru.CODUSUARI (auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de confirmación recibido de la interfaz con laboratorio (ej: ''''Solicitud procesada'''', ''''Trama XML registrada'''', ''''MENSAJE RECIBIDO'''', ''''Ingreso de orden''''); indica estado de integración exitosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'MENSAJERECIBIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje de confirmacion de recepcion exitosa por parte de la interfaz    EJ:  Solicitud procesada  Trama XML registrada  MENSAJE RECIBIDO  Ingreso de órden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'MENSAJERECIBIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'MENSAJERECIBIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuario para autenticación HTTP Basic (HTTPS) en la conexión con el proveedor de laboratorio; credencial de interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'USUARIOAUTHBASICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de autenticacion basica via https', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'USUARIOAUTHBASICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'USUARIOAUTHBASICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del proveedor de laboratorio: 0=Ninguno, 1=Enterprise, 2=Athenea, 3=Annarlab, 4=Comprolab, 5=Nobilis, 6=Medife (INT, catálogo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'TIPOPROVEEDOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ninguno = 0  Enterprise = 1  Athenea = 2  Annarlab = 3  COmprolab = 4  Nobilis = 5  Medife = 6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'TIPOPROVEEDOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'TIPOPROVEEDOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que define si el centro de atención realiza servicios de laboratorio en unidades funcionales (UF) propias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'EXAENUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si se realizan servicios en unidades funcionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'EXAENUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'EXAENUF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave o contraseña compartida entre ambos extremos de la interfaz para garantizar la seguridad en la comunicación con laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'PASSINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clave compartida en ambos extremos para la seguridad de la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'PASSINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'PASSINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL o dirección del servicio web del proveedor de laboratorio para establecer la interfaz de integración (VARCHAR 250)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'URISERVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion de los servicios Web para poder realizar la interfaz con laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'URISERVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'URISERVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si el centro de atención está activo para realizar interfaz bidireccional con el proveedor de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'INTLABOAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'INTLABOAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'INTLABOAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del centro de atención; FK a ADCENATEN.CODCENATE; clave primaria (PK_HCPARLABO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración para la integración del módulo de laboratorio clínico por centro de atención. Guarda la URL del servicio externo, credenciales de autenticación y opciones de conexión para el intercambio electrónico de resultados de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO';
