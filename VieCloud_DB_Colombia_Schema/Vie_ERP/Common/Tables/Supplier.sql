CREATE TABLE [Common].[Supplier] (
    [Id]                          INT                                                                         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')       NOT NULL,
    [IdThirdParty]                INT                                                                         NOT NULL,
    [Name]                        VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Supplier_Ofuscado", 0)') NOT NULL,
    [CodeCMMS]                    VARCHAR (20)                                                                NULL,
    [WebSite]                     VARCHAR (80)                                                                NULL,
    [IdCity]                      INT                                                                         NOT NULL,
    [IdManufacturer]              INT                                                                         NULL,
    [PermanentRetention]          BIT                                                                         NOT NULL,
    [TimeLimitDays]               INT                                                                         NOT NULL,
    [NotIva]                      BIT                                                                         NOT NULL,
    [Declarant]                   BIT                                                                         NOT NULL,
    [IndependentEmployee]         BIT                                                                         NOT NULL,
    [PrioritizeBankAccount]       BIT                                                                         NOT NULL,
    [TaxCategory]                 TINYINT                                                                     NOT NULL,
    [Status]                      BIT                                                                         NOT NULL,
    [CreationUser]                VARCHAR (20)                                                                NOT NULL,
    [CreationDate]                DATETIME                                                                    NOT NULL,
    [ModificationUser]            VARCHAR (20)                                                                NULL,
    [ModificationDate]            DATETIME                                                                    NULL,
    [TimeStamp]                   ROWVERSION                                                                  NOT NULL,
    [WorkRent]                    BIT                                                                         NOT NULL,
    [Pensions]                    BIT                                                                         NOT NULL,
    [CapitalRent]                 BIT                                                                         NOT NULL,
    [NotLaborRent]                BIT                                                                         NOT NULL,
    [DividendsAndParticipations]  BIT                                                                         NOT NULL,
    [Manufacturer]                BIT                                                                         NOT NULL,
    [Seller]                      BIT                                                                         NOT NULL,
    [SelfWithholding]             BIT                                                                         CONSTRAINT [DF_Supplier_SelfWithholding] DEFAULT ((0)) NOT NULL,
    [SelfWithholdingICA]          BIT                                                                         CONSTRAINT [DF_Supplier_SelfWithholdingICA] DEFAULT ((0)) NOT NULL,
    [ExposeFactoring]             BIT                                                                         CONSTRAINT [DF__Supplier__Expose__57395B26] DEFAULT ((1)) NOT NULL,
    [ConsignmentInventoryCosting] TINYINT                                                                     CONSTRAINT [DF__Supplier__Consig__09916777] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Supplier__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Manufacturer_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Supplier_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_Supplier__IdThirdParty] UNIQUE NONCLUSTERED ([IdThirdParty] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Supplier].[Code]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Supplier].[Name]
    WITH (LABEL = 'Confidential', INFORMATION_TYPE = 'Name');


GO
CREATE NONCLUSTERED INDEX [IX_Supplier__Status]
    ON [Common].[Supplier]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de valoración de inventario en consignación: 0=Costo promedio, 1=Lista de costos. TINYINT, afecta cálculo de costos de mercancía.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryCosting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0= Costo promedio,1: Lista de costos ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryCosting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryCosting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el proveedor está expuesto o disponible para operaciones de factoring. BIT (booleano), marca visibilidad para cesión de cartera.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ExposeFactoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marcar proveedor a exponer', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ExposeFactoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ExposeFactoring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autoretenedor a título de ICA (Impuesto de Industria y Comercio). BIT booleano, determina retención en la fuente por gravamen local.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'SelfWithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autoretenedor a titulo de ICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'SelfWithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'SelfWithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autoretenedor a título de Renta (IRCA, retención en la fuente). BIT booleano, calcula retención sobre ingresos laborales y no laborales.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'SelfWithholding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autoretenedor a titulo de Renta', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'SelfWithholding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'SelfWithholding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rol de proveedor como vendedor/distribuidor. BIT booleano, indica si realiza ventas de bienes o servicios a la organización.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Seller';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vendedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Seller';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Seller';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rol de proveedor como fabricante/productor. BIT booleano, identifica si es productor directo de bienes o componentes.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Manufacturer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fabricante', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Manufacturer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Manufacturer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula tributaria: dividendos y participaciones. BIT booleano, clasifica ingresos por distribución de utilidades o participaciones accionarias.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'DividendsAndParticipations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pertenece a cedula tributaria: dividendos y participaciones', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'DividendsAndParticipations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'DividendsAndParticipations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula tributaria: renta no laboral. BIT booleano, agrupa ingresos por arrendamientos, intereses, comisiones fuera de relación laboral.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'NotLaborRent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pertenece a cedula tributaria: renta no laboral', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'NotLaborRent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'NotLaborRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula tributaria: renta capital. BIT booleano, clasifica ingresos por ganancia en venta de activos, valorización, intereses financieros.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CapitalRent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pertenece a cedula tributaria: renta capital', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CapitalRent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CapitalRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula tributaria: pensiones. BIT booleano, identifica ingresos por pensión, jubilación o prestaciones de retiro.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Pensions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pertenece a cedula tributaria: pensiones', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Pensions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Pensions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula tributaria: renta de trabajo. BIT booleano, agrupa ingresos derivados de relación laboral, honorarios profesionales.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'WorkRent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pertenece a cedula tributaria: renta de trabajo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'WorkRent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'WorkRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de concurrencia y versionado de registros. TIMESTAMP SQL Server, marca cambios para evitar conflictos en actualizaciones simultáneas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para controlar la concurrencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro proveedor. DATETIME, auditoria de cambios en datos maestros.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro. VARCHAR(20), trazabilidad de cambios en el proveedor.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro proveedor. DATETIME, auditoria de alta en sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro proveedor. VARCHAR(20), trazabilidad de origen del dato maestro.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del proveedor o fabricante. BIT booleano, indica habilitación para operaciones.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del fabricante', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría tributaria del proveedor. TINYINT: 1=Ninguna, 2=Empleado, 3=Trabajador por cuenta propia, 4=Otros contribuyentes. Determina esquema de retenciones.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TaxCategory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la categoria tributaria    1 - Ninguna  2 - Empleado   3 - Trabajador por cuenta propia   4 - Otros contribuyentes   ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TaxCategory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TaxCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad en selección de cuenta bancaria para dispersión de fondos. BIT booleano, si es true toma cuenta del mismo banco, si false usa cuenta Default.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'PrioritizeBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si al momento de hacer una dispersion de fondos va a tomar la cuenta bancaria que tenga el mismo banco o si va tomar la cuenta bancaria que coloquen como Default', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'PrioritizeBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'PrioritizeBankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si proveedor es trabajador independiente/por cuenta propia. BIT booleano, afecta cálculo de retenciones 383 y 384 en la fuente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IndependentEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el proveedor es declarante de impuestos    Si es declarante de impuestos, cuando se le vaya hacer una retencion de categoria empleados se le van a calcular las retenciones 383 y 384 y se cobrara la mayor de las dos    y Si no es declarante solo se va a calcular y a cobrar la retencion 383', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IndependentEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IndependentEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si proveedor es declarante de impuestos. BIT booleano, en retenciones de empleados calcula máximo entre 383/384 si es true, solo 383 si false.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Declarant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el proveedor es declarante de impuestos    Si es declarante de impuestos, cuando se le vaya hacer una retencion de categoria empleados se le van a calcular las retenciones 383 y 384 y se cobrara la mayor de las dos    y Si no es declarante solo se va a calcular y a cobrar la retencion 383', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Declarant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Declarant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exclusión de IVA en operaciones con proveedor. BIT booleano, indica no aplica impuesto al valor agregado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'NotIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No incluye iva', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'NotIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'NotIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días de plazo de pago acordados con proveedor. INT, define términos de crédito en órdenes de compra y facturas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TimeLimitDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias de plazo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TimeLimitDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TimeLimitDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag de retención permanente en la fuente aplicable al proveedor. BIT booleano, marca obligación de retener en todas las transacciones.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'PermanentRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Retencion permanente', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'PermanentRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'PermanentRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del fabricante relacionado. INT, referencia a registro padre en tabla Supplier si este es distribuidor/reventa.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdManufacturer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del fabricante relacionado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdManufacturer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdManufacturer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de ciudad ubicación proveedor. INT, referencia a [Common].[City] para localización geográfica y tributaria.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la ciudad relacionada', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL o página web del proveedor. VARCHAR(80), contacto digital y validación de tercero.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'WebSite';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pagina web del proveedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'WebSite';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'WebSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código asignado en sistema CMMS (Computerized Maintenance Management System). VARCHAR(20), integración con gestión de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CodeCMMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CMMS', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CodeCMMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CodeCMMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón social o nombre comercial del proveedor. VARCHAR(100), MASKED=Supplier_Ofuscado (PII), descripción del fabricante o distribuidor.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del fabricante', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del tercero (Supplier). INT, referencia a [Common].[ThirdParty], UNIQUE, vinculación a datos maestros de persona/empresa.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del proveedor, generalmente NIT o documento. VARCHAR(25), MASKED=Nit_Ofuscado (PII identificación), clave de búsqueda.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del proveedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la tabla Supplier. INT IDENTITY(1,1), clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de fabricantes', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proveedores y terceros con quienes la organización tiene relaciones comerciales de compra o suministro. Contiene datos fiscales, tributarios y operativos de cada proveedor, incluyendo retenciones, categoría tributaria, ciudad, fabricante asociado y condiciones de pago.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier';
