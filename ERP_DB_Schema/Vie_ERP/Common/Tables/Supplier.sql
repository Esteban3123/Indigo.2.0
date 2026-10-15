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
ALTER TABLE [Common].[Supplier] NOCHECK CONSTRAINT [FK_Manufacturer_City];


GO
ALTER TABLE [Common].[Supplier] NOCHECK CONSTRAINT [FK_Supplier_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_Supplier__Status]
    ON [Common].[Supplier]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0= Costo promedio,1: Lista de costos ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryCosting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marcar proveedor a exponer', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ExposeFactoring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autoretenedor a titulo de ICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'SelfWithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autoretenedor a titulo de Renta', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'SelfWithholding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vendedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Seller';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fabricante', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Manufacturer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pertenece a cedula tributaria: dividendos y participaciones', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'DividendsAndParticipations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pertenece a cedula tributaria: renta no laboral', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'NotLaborRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pertenece a cedula tributaria: renta capital', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CapitalRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pertenece a cedula tributaria: pensiones', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Pensions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pertenece a cedula tributaria: renta de trabajo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'WorkRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna para controlar la concurrencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del fabricante', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la categoria tributaria    1 - Ninguna  2 - Empleado   3 - Trabajador por cuenta propia   4 - Otros contribuyentes   ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TaxCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si al momento de hacer una dispersion de fondos va a tomar la cuenta bancaria que tenga el mismo banco o si va tomar la cuenta bancaria que coloquen como Default', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'PrioritizeBankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el proveedor es declarante de impuestos    Si es declarante de impuestos, cuando se le vaya hacer una retencion de categoria empleados se le van a calcular las retenciones 383 y 384 y se cobrara la mayor de las dos    y Si no es declarante solo se va a calcular y a cobrar la retencion 383', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IndependentEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el proveedor es declarante de impuestos    Si es declarante de impuestos, cuando se le vaya hacer una retencion de categoria empleados se le van a calcular las retenciones 383 y 384 y se cobrara la mayor de las dos    y Si no es declarante solo se va a calcular y a cobrar la retencion 383', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Declarant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No incluye iva', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'NotIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de plazo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TimeLimitDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Retencion permanente', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'PermanentRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del fabricante relacionado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdManufacturer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la ciudad relacionada', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pagina web del proveedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'WebSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CMMS', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'CodeCMMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del fabricante', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del proveedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de fabricantes', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Id';

