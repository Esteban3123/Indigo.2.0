CREATE TABLE [Taxes].[LowTaxLiquidation] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Consecutive]      VARCHAR (20)    NOT NULL,
    [DocumentDate]     DATETIME        NOT NULL,
    [Type]             TINYINT         NOT NULL,
    [ThirdPartyId]     INT             NOT NULL,
    [Detail]           VARCHAR (2000)  NULL,
    [TotalToPay]       NUMERIC (18, 2) NOT NULL,
    [UVTValue]         NUMERIC (18, 2) NOT NULL,
    [InvoiceNumber]    VARCHAR (100)   NULL,
    [Status]           TINYINT         NOT NULL,
    [CreationUser]     VARCHAR (20)    NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    [ModificationUser] VARCHAR (20)    NULL,
    [ModificationDate] DATETIME        NULL,
    [ConfirmationUser] VARCHAR (20)    NULL,
    [ConfirmationDate] DATETIME        NULL,
    [OperatingUnitId]  INT             NULL,
    CONSTRAINT [PK_LowTaxLiquidation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LowerTaxLiquidation_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_LowTaxLiquidation_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Taxes].[LowTaxLiquidation] NOCHECK CONSTRAINT [FK_LowerTaxLiquidation_ThirdParty];


GO
ALTER TABLE [Taxes].[LowTaxLiquidation] NOCHECK CONSTRAINT [FK_LowTaxLiquidation_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de confirmación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (registrado = 1,confirmado = 2,anulado = 3,reversado = 4)', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del UVT al momento de realizar el documento', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'UVTValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total a pagar', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'TotalToPay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo detalle', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Liquidación de Impuesto menor:  1. Impuesto Publicidad Exterior Visual y avisos.  2. Impuesto Publicidad visual Móvil.  3. Impuesto de Circulación y Tránsito.  4. Impuesto de Azar y Juegos Permitidos.  5. Impuesto de Espectáculos Públicos.  6. Impuesto de Delineación urbana.  7. Impuesto por Ocupación de Vías, plazas y lugares públicos.  8.Impuesto de Registro de Patentes, Marcas y Herretes  9.Impuesto de Degüello de Ganado.  10.Impuesto Sobretasa a la Gasolina a Motor.  11.Participación en Plusvalía en el Municipio de Neiva.  ', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del documento', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidation', @level2type = N'COLUMN', @level2name = N'Id';

