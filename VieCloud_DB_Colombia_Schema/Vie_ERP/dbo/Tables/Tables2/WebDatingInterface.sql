CREATE TABLE [dbo].[WebDatingInterface] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [CenterOfAttention]  CHAR (10)     NOT NULL,
    [Interface]          BIT           NOT NULL,
    [InterfaceProvider]  INT           NULL,
    [ServiceEndpoint]    VARCHAR (100) NULL,
    [Topic]              VARCHAR (100) NULL,
    [UrlServerWebDating] VARCHAR (300) NULL,
    [CreationUser]       CHAR (20)     NULL,
    [CreationDate]       DATETIME      NULL,
    [ModificationUser]   CHAR (20)     NULL,
    [ModificationDate]   DATETIME      NULL,
    CONSTRAINT [PK_WebDatingInterface] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de configuración de la interfaz web (DATETIME, auditoría).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/login que realizó la última modificación de los parámetros de la interfaz web de citas (CHAR 20, auditoría).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de configuración de la interfaz de citas web (DATETIME, auditoría).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/login que creó el registro de parametrización de la interfaz web de citas (CHAR 20, auditoría).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que crea el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL o dirección web del servidor que aloja la plataforma de citas web, endpoint base para acceso a la interfaz de agendamiento (VARCHAR 300).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'UrlServerWebDating';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Url servidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'UrlServerWebDating';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'UrlServerWebDating';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tema, canal o tópico de publicación/suscripción (ej: mensaje broker) para la interfaz de citas web (VARCHAR 100, integración).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Topic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Topic', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Topic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Topic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Punto de acceso (endpoint) o ruta del servicio web que expone la interfaz de citas, para integración con sistemas externos (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ServiceEndpoint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Endpoint', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ServiceEndpoint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ServiceEndpoint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor de la interfaz de citas: 1=Vie Bookings u otro integrador de agendamiento web (INT, FK referencia a catálogo de proveedores).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'InterfaceProvider';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el proveedor de la interfaz:  1 - Vie Bookings', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'InterfaceProvider';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'InterfaceProvider';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estado activo/inactivo (BIT: 1=habilitada, 0=deshabilitada) de la interfaz web de citas para el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Interface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la interfaz está activa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Interface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Interface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del centro de atención, unidad funcional o sede asociada a esta configuración de interfaz web (CHAR 10, FK a catálogo de centros).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CenterOfAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CenterOfAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CenterOfAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de parametrización y configuración de la interfaz web de citas/agendamiento. Almacena los datos de conexión, proveedor y estado activo de la plataforma de reserva de citas en línea por centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla que almacena la parametrización de la interfaz de citas web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de configuración de la interfaz de agendamiento web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Id';
