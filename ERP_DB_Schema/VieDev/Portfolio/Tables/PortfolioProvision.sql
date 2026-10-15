CREATE TABLE [Portfolio].[PortfolioProvision] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20)  NOT NULL,
    [DocumentDate]       DATE          NOT NULL,
    [CourtDate]          DATE          NOT NULL,
    [DocumentType]       TINYINT       NOT NULL,
    [Description]        VARCHAR (500) NULL,
    [OperatingUnitId]    INT           NOT NULL,
    [Status]             TINYINT       NOT NULL,
    [CreationUser]       VARCHAR (20)  CONSTRAINT [DF_PortfolioProvision_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_PortfolioProvision_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    [ConfirmationUser]   VARCHAR (20)  NULL,
    [ConfirmationDate]   DATETIME      NULL,
    [AnnulmentUser]      VARCHAR (20)  NULL,
    [AnnulmentDate]      DATETIME      NULL,
    [TimeStamp]          ROWVERSION    NOT NULL,
    [ApplyDeterioration] TINYINT       NULL,
    CONSTRAINT [PK_PortfolioProvision] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioProvision_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioProvision] NOCHECK CONSTRAINT [FK_PortfolioProvision_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo se habilita si el DocumentType es igual a 2 (deterioro)  1 - Deterioro a facturación en cartera  2 - Deterioro a facturación en glosa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvision', @level2type = N'COLUMN', @level2name = N'ApplyDeterioration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvision', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el esta del documento  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvision', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de documento o proceso  1 - Provision  2 - Deterioro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvision', @level2type = N'COLUMN', @level2name = N'DocumentType';

