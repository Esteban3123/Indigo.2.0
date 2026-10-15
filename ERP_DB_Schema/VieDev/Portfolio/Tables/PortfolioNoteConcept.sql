CREATE TABLE [Portfolio].[PortfolioNoteConcept] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NULL,
    [NoteType]         TINYINT       CONSTRAINT [DF_PortfolioNoteConcept_NoteType] DEFAULT ((2)) NOT NULL,
    [IdAccount]        INT           NULL,
    [Status]           BIT           CONSTRAINT [DF_PortfolioNoteConcept_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [AlternateCode]    VARCHAR (25)  NULL,
    [HandleTax]        BIT           CONSTRAINT [DF__Portfolio__Handl__7B234C8F] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PortfolioNoteConcept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteConcept_PUC] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioNoteConcept] NOCHECK CONSTRAINT [FK_PortfolioNoteConcept_PUC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el concepto maneja o no impuesto', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteConcept', @level2type = N'COLUMN', @level2name = N'HandleTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Alterno (COL) / Código Cabys (CR)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteConcept', @level2type = N'COLUMN', @level2name = N'AlternateCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de concepto de nota  1  - General  2 - Especifico    Cuando es general no se solicita la cuenta contable pero si se solicita cuando esten creando la nota    Cuando es especifico si se obliga a que seleccionen una cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteConcept', @level2type = N'COLUMN', @level2name = N'NoteType';

