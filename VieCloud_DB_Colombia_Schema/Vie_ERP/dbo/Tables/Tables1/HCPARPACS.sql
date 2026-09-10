CREATE TABLE [dbo].[HCPARPACS] (
    [CODCENATE]           CHAR (10)     NOT NULL,
    [INTPACSAC]           BIT           NOT NULL,
    [NOMSERPAC]           VARCHAR (256) NULL,
    [INTERFAZHK]          INT           NULL,
    [NOMSERPACEXT]        VARCHAR (256) NULL,
    [INTERFAZONCO]        BIT           NULL,
    [URLINTERFAZONCO]     VARCHAR (256) NULL,
    [CLAVECOMPARTIDAONCO] VARCHAR (256) NULL,
    [USUARIOCREACION]     CHAR (20)     NULL,
    [FECHACREACION]       DATETIME      NULL,
    [USUARIOMODIFICACION] CHAR (20)     NULL,
    [FECHAMODIFICACION]   DATETIME      NULL,
    [ServiceEndpoint]     VARCHAR (200) NULL,
    [Topic]               VARCHAR (200) NULL,
    CONSTRAINT [PK_HCPARPACS] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_HCPARPACS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARPACS_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARPACS_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tópico/tema del servicio de mensajería o integración, utilizado en la cola de eventos para imagenología PACS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'Topic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Topic del servico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'Topic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'Topic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Punto final (endpoint) del servicio web, URL o dirección de conexión para comunicación con sistemas de imagenología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'ServiceEndpoint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Endpoint del servicio ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'ServiceEndpoint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'ServiceEndpoint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de configuración PACS, tipo DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro (FK a SEGusuaru.CODUSUARI), auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de configuración PACS, tipo DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (FK a SEGusuaru.CODUSUARI), auditoría de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave o token compartido para autenticación en interfaz HL7 ORM con servidor PACS/RIS oncológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'CLAVECOMPARTIDAONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clave compartida interfaz de imagenes HL7 ORM ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'CLAVECOMPARTIDAONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'CLAVECOMPARTIDAONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del webservice para envío de tramas HL7 ORM hacia servidor PACS/RIS de oncología, integración de imágenes médicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'URLINTERFAZONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URL webservices envio trama interfaz con HL7 ORM - RIS PACS oncologos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'URLINTERFAZONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'URLINTERFAZONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el centro realiza interfaz HL7 ORM con servidor PACS/RIS oncológico, flag de habilitación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTERFAZONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si realiza interfaz con HL7 ORM - RIS PACS oncologos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTERFAZONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTERFAZONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servidor PACS externo (tercero) cuando se utiliza proveedor de imagenología contratado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'NOMSERPACEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del servidor PSC externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'NOMSERPACEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'NOMSERPACEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código que especifica el proveedor de imagenología integrado: 1=Hiruko, 2=Visual Médica, 3=Indira, 4=AQUILA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTERFAZHK';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el proveedor de imagenologia    1: Hiruko   2: Visual Medica   3: Indira       4: AQUILA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTERFAZHK';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTERFAZHK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificador del servidor PACS local, sistema de archivo y comunicación de imágenes médicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'NOMSERPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Servidor del PACS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'NOMSERPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'NOMSERPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el centro de atención realiza interfaz con servidor PACS, flag de integración activa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTPACSAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTPACSAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTPACSAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (FK a ADCENATEN.CODCENATE), identificador único de la unidad funcional de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de integración y conectividad por centro de atención: define si el centro tiene activa la interfaz con el sistema de historia clínica (PACS/HIS), las URLs y credenciales para la interfaz de oncología, y los endpoints de servicios externos para intercambio de datos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS';
