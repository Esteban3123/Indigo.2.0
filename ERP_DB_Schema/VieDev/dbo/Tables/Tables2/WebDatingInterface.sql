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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en la que se modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que crea el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Url servidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'UrlServerWebDating';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Topic', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Topic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Endpoint', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'ServiceEndpoint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica el proveedor de la interfaz:  1 - Vie Bookings', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'InterfaceProvider';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la interfaz está activa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'Interface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface', @level2type = N'COLUMN', @level2name = N'CenterOfAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que almacena la parametrización de la interfaz de citas web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebDatingInterface';

