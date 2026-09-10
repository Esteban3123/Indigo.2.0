CREATE TABLE [dbo].[HCPARPATO] (
    [CODCENATE]           CHAR (10)     NOT NULL,
    [INTPATOAC]           BIT           NOT NULL,
    [URISERVIC]           CHAR (50)     NULL,
    [PASSINTER]           CHAR (50)     NULL,
    [INTERFAZALULA]       BIT           NULL,
    [USUARIOCREACION]     CHAR (20)     NULL,
    [FECHACREACION]       DATETIME      NULL,
    [USUARIOMODIFICACION] CHAR (20)     NULL,
    [FECHAMODIFICACION]   DATETIME      NULL,
    [ProveedorInterfaz]   INT           NULL,
    [UrlQueuePathology]   VARCHAR (100) NULL,
    [NameQueuePathology]  VARCHAR (100) NULL,
    [UrlServerPathology]  VARCHAR (300) NULL,
    CONSTRAINT [PK_HCPARPATO] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_HCPARPATO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARPATO_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARPATO_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servidor de interfaz de patologías (API REST), permite abrir y consultar reportes de patologías desde Coral. VARCHAR(300), conexión externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'UrlServerPathology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Url del servidor de la interfaz de patologias (APIS), esta url es la que permite abrir los reportes de patologias de coral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'UrlServerPathology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'UrlServerPathology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la cola de mensajería para encolamiento cuando proveedor es Indigo-Coral. VARCHAR(100), parámetro de integración asincrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'NameQueuePathology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando el proveedor es Indigo-Coral entonces es cuando se solicita este dato de la URL Queue para el encolamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'NameQueuePathology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'NameQueuePathology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de la cola de mensajería (Queue) para encolamiento cuando proveedor es Indigo-Coral. VARCHAR(100), endpoint de integración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'UrlQueuePathology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando el proveedor es Indigo-Coral entonces es cuando se solicita este dato de la URL Queue para el encolamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'UrlQueuePathology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'UrlQueuePathology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proveedor de interfaz de patologías: 1=Indigo-Coral, 2=ALULA. INT, determina flujo de integración y validaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'ProveedorInterfaz';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Indigo - Coral  2 - ALULA  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'ProveedorInterfaz';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'ProveedorInterfaz';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de configuración de patologías. DATETIME, auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro. CHAR(20), FK a SEGusuaru, auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de configuración de patologías. DATETIME, auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de configuración. CHAR(20), FK a SEGusuaru, auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de integración activa con ALULA (verdadero/falso). BIT, habilita flujo de patologías vía ALULA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'INTERFAZALULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Integración con ALULA (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'INTERFAZALULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'INTERFAZALULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave/contraseña compartida para autenticación segura de la interfaz de patologías. CHAR(50), credencial encriptada PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'PASSINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clave compartida en ambos extremos para la seguridad de la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'PASSINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'PASSINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URI/dirección del servicio web para interfaz de patologías. CHAR(50), endpoint de conexión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'URISERVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion de los servicios Web para poder realizar la interfaz con patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'URISERVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'URISERVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de activación de interfaz de patologías. BIT, especifica si el centro realiza integración automática.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'INTPATOAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'INTPATOAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'INTPATOAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del centro de atención. CHAR(10), PK, FK a ADCENATEN, identifica unidad funcional/sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de la interfaz de patología por centro de atención: guarda los parámetros de conexión (URL, credenciales, colas de mensajería) necesarios para integrar el sistema con el laboratorio o servicio externo de patología, así como el estado de activación de dicha interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO';
