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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de libro contable  1 - Colgap  2 - NIIF  3 - Cuentas Estados Financieros', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'LegalBook', @level2type = N'COLUMN', @level2name = N'TypeBook';

