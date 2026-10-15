CREATE TABLE [Taxes].[TaxesMagneticMedia] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NameOrBusinessName]   NVARCHAR (200) NOT NULL,
    [IdentificationNumber] NVARCHAR (20)  NOT NULL,
    [Year]                 INT            NOT NULL,
    [Date]                 DATE           NULL,
    [Bimester]             TINYINT        NULL,
    CONSTRAINT [PK_MagneticMedia] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Bimester';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de identificación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre o nombre comercial', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Id';

