CREATE TABLE [Taxes].[TaxesMagneticMediaDetail] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MagneticMediaId]      INT            NOT NULL,
    [NameOrBusinessName]   NVARCHAR (200) NOT NULL,
    [IdentificationNumber] NVARCHAR (20)  NOT NULL,
    [ValueOperation]       NUMERIC (18)   NOT NULL,
    [TaxWithheld]          NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_MagneticMediaDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MagneticMediaDetail_MagnetiMedia] FOREIGN KEY ([MagneticMediaId]) REFERENCES [Taxes].[TaxesMagneticMedia] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesMagneticMediaDetail] NOCHECK CONSTRAINT [FK_MagneticMediaDetail_MagnetiMedia];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'TaxWithheld';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Valor de Operación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'ValueOperation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de identificación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre o nombre comercial', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de medios magnéticos', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'MagneticMediaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'Id';

