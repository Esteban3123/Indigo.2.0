CREATE TABLE [Maintenance].[SupplierMaintenance] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit]                 VARCHAR (15)  NOT NULL,
    [CompleteName]        VARCHAR (100) NOT NULL,
    [Names]               VARCHAR (50)  NOT NULL,
    [LastNames]           VARCHAR (50)  NULL,
    [TimeLimitDays]       INT           NOT NULL,
    [CodeCMMS]            VARCHAR (20)  NULL,
    [WebSite]             VARCHAR (80)  NULL,
    [IdCity]              INT           NOT NULL,
    [PermanentRetention]  BIT           NOT NULL,
    [NotIva]              BIT           NOT NULL,
    [Manufacturer]        BIT           NOT NULL,
    [Seller]              BIT           NOT NULL,
    [ResponsibleWarranty] BIT           NOT NULL,
    [Status]              BIT           NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    CONSTRAINT [PK_SupplierMaintenance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Supplier_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP SQL Server). Registra automáticamente el instante exacto de creación, actualización o modificación del registro del proveedor para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro del proveedor (DATETIME). Permite rastrear cuándo se actualizaron datos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro (VARCHAR 20). Identifica quién actualizó la información del proveedor en el sistema.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro del proveedor (DATETIME). Marca el momento de registro en el sistema.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del proveedor (VARCHAR 20). Identifica quién registró al proveedor en el sistema.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado o vigencia del proveedor fabricante (BIT: 1=Activo, 0=Inactivo). Indica si el fabricante está habilitado para operaciones.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del fabricante', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de responsabilidad por garantía (BIT: 1=Responsable, 0=No responsable). Especifica si el proveedor asume responsabilidad de garantía de equipos o servicios.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ResponsibleWarranty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'especifica si es responsable de la garantia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ResponsibleWarranty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ResponsibleWarranty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de rol de vendedor (BIT: 1=Es vendedor, 0=No es vendedor). Especifica si el proveedor actúa como distribuidor o vendedor de productos/servicios.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Seller';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el proveedor es vendedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Seller';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Seller';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de rol de fabricante (BIT: 1=Es fabricante, 0=No es fabricante). Especifica si el proveedor es fabricante de equipos o componentes.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Manufacturer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es un fabricante.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Manufacturer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Manufacturer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de exclusión de IVA (BIT: 1=Excluido de IVA, 0=Incluye IVA). Especifica si las transacciones del proveedor están exentas de impuesto al valor agregado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'NotIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No incluye iva', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'NotIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'NotIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de retención permanente (BIT: 1=Con retención, 0=Sin retención). Especifica si se aplica retención fiscal permanente a pagos del proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'PermanentRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Retencion permanente', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'PermanentRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'PermanentRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ciudad (FK a Common.City). Referencia la ciudad de ubicación o domicilio del proveedor para localización geográfica.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la ciudad relacionada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección web o URL del proveedor (VARCHAR 80). Página web, portal o enlace digital de contacto del proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'WebSite';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pagina web del proveedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'WebSite';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'WebSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación en sistema CMMS (VARCHAR 20). Código de integración con sistema de Gestión de Mantenimiento Computerizado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CodeCMMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CMMS', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CodeCMMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CodeCMMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo de entrega en días (INT). Número de días límite para cumplimiento de entregas o servicios pactados con el proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'TimeLimitDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias de plazo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'TimeLimitDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'TimeLimitDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apellidos del representante legal o contacto (VARCHAR 50). Apellido(s) de la persona física responsable cuando el proveedor es persona natural.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'LastNames';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apellidos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'LastNames';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'LastNames';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombres del representante legal o contacto (VARCHAR 50). Nombre(s) de la persona física responsable cuando el proveedor es persona natural.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Names';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombres', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Names';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Names';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del proveedor (VARCHAR 100). Razón social, nombre legal o denominación completa de la empresa o persona del proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CompleteName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Completo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CompleteName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CompleteName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Identificación Tributaria del proveedor (VARCHAR 15, PII-Ofuscado). NIT, CUIT, RUT o equivalente - identificación fiscal única del proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del Proveedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY). Clave primaria única que identifica cada registro de proveedor en mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maestro de proveedores para mantenimiento. Guarda los datos de contacto, clasificación y condiciones comerciales de cada proveedor (fabricante, vendedor, responsable de garantía) utilizado en la gestión de mantenimiento de equipos e infraestructura.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance';
