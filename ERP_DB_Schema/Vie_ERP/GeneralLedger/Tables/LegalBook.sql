CREATE TABLE [GeneralLedger].[LegalBook] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20)  NOT NULL,
    [Name]               VARCHAR (100) NOT NULL,
    [Description]        VARCHAR (MAX) NULL,
    [TypeBook]           TINYINT       NOT NULL,
    [OfficialBook]       BIT           NOT NULL,
    [Status]             BIT           NOT NULL,
    [CreationUser]       VARCHAR (20)  NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    [TimeStamp]          ROWVERSION    NOT NULL,
    [LastYearClose]      INT           CONSTRAINT [DF_LegalBook_LastYearClose] DEFAULT ((2015)) NOT NULL,
    [OfficialCurrencyId] INT           CONSTRAINT [DF__LegalBook__Offic__47B7EBCB] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LegalBook_Currency] FOREIGN KEY ([OfficialCurrencyId]) REFERENCES [Common].[Currency] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[LegalBook] NOCHECK CONSTRAINT [FK_LegalBook_Currency];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Moneda Oficial del libro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el ultimo año del cierre contable de el libro, este campo es validado cuando se crean comprobantes contables, es decir que la condicion es que el año de la fecha del comprobante sea mayor a el año del ultimo cierre', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'LastYearClose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del libro 1 -Activo 0  -Inactivo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es el libro oficial, solo puede haber un libro oficial', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'OfficialBook';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de libro contable  1 - Colgap  2 - NIIF  3 - Cuentas Estados Financieros', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'TypeBook';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del libro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del libro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del libro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'Id';

