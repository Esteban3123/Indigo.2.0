CREATE TABLE [Common].[Currency] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (3)  NOT NULL,
    [Name]             VARCHAR (80) NOT NULL,
    [State]            BIT          CONSTRAINT [DF_Currency_State] DEFAULT ((0)) NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    [RoundingType]     TINYINT      CONSTRAINT [DF__Currency__Roudin__1714B470] DEFAULT ((3)) NOT NULL,
    [CurrencyExchange] BIT          CONSTRAINT [DF__Currency__Curren__124FFF53] DEFAULT ((1)) NOT NULL,
    [RateVariation]    BIT          CONSTRAINT [DF__Currency__RateVa__1344238C] DEFAULT ((1)) NOT NULL,
    [Abbreviation]     VARCHAR (5)  NULL,
    [ISO4217Id]        INT          CONSTRAINT [DF__Currency__ISO421__541DC2B0] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Currency_ISO4217] FOREIGN KEY ([ISO4217Id]) REFERENCES [Common].[ISO4217] ([Id]),
    CONSTRAINT [IX_CurrencyISO4217] UNIQUE NONCLUSTERED ([ISO4217Id] ASC)
);


GO
ALTER TABLE [Common].[Currency] NOCHECK CONSTRAINT [FK_Currency_ISO4217];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la divisa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'ISO4217Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Abreviacion de la moneda', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'Abbreviation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ajuste por variacion de tasa de cambio automatica : 1 - Si, 2 - No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'RateVariation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa de cambio automatica : 1 - Si, 2 - No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'CurrencyExchange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' 1.  0,01  A Dos Decimales                   2.  0,1    A Un Decimal                   3.  1       Ninguno                   4.  10     A la Decena                   5.  100   A la Centena                   6. 1000  A la Milésima', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'RoundingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ModificationDate', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del concepto general  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Currency', @level2type = N'COLUMN', @level2name = N'Id';

