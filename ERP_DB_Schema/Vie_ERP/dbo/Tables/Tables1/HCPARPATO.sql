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
ALTER TABLE [dbo].[HCPARPATO] NOCHECK CONSTRAINT [FK_HCPARPATO_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARPATO] NOCHECK CONSTRAINT [FK_HCPARPATO_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARPATO] NOCHECK CONSTRAINT [FK_HCPARPATO_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Url del servidor de la interfaz de patologias (APIS), esta url es la que permite abrir los reportes de patologias de coral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'UrlServerPathology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando el proveedor es Indigo-Coral entonces es cuando se solicita este dato de la URL Queue para el encolamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'NameQueuePathology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando el proveedor es Indigo-Coral entonces es cuando se solicita este dato de la URL Queue para el encolamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'UrlQueuePathology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Indigo - Coral  2 - ALULA  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'ProveedorInterfaz';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Integración con ALULA (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'INTERFAZALULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clave compartida en ambos extremos para la seguridad de la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'PASSINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion de los servicios Web para poder realizar la interfaz con patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'URISERVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'INTPATOAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPATO', @level2type = N'COLUMN', @level2name = N'CODCENATE';

