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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0= Costo promedio,1: Lista de costos ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryCosting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la categoria tributaria    1 - Ninguna  2 - Empleado   3 - Trabajador por cuenta propia   4 - Otros contribuyentes   ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'TaxCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el proveedor es declarante de impuestos    Si es declarante de impuestos, cuando se le vaya hacer una retencion de categoria empleados se le van a calcular las retenciones 383 y 384 y se cobrara la mayor de las dos    y Si no es declarante solo se va a calcular y a cobrar la retencion 383', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'IndependentEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el proveedor es declarante de impuestos    Si es declarante de impuestos, cuando se le vaya hacer una retencion de categoria empleados se le van a calcular las retenciones 383 y 384 y se cobrara la mayor de las dos    y Si no es declarante solo se va a calcular y a cobrar la retencion 383', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Supplier', @level2type = N'COLUMN', @level2name = N'Declarant';

