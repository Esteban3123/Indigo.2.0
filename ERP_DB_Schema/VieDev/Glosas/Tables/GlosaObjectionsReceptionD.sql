CREATE TABLE [Glosas].[GlosaObjectionsReceptionD] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaObjectionsReceptionCId] INT           NOT NULL,
    [PortfolioGlosaId]            INT           NOT NULL,
    [GlosasParametersInterfaceId] INT           NULL,
    [ObservationInvoiceCode]      VARCHAR (5)   NULL,
    [DocumentType]                CHAR (1)      NOT NULL,
    [InvoiceNumber]               VARCHAR (50)  NOT NULL,
    [Comment]                     VARCHAR (MAX) NULL,
    [State]                       CHAR (1)      NOT NULL,
    [TimeStamp]                   ROWVERSION    NOT NULL,
    [RadicateResponsibleId]       INT           NULL,
    [RadicatedDate]               DATETIME      NULL,
    [RadicatedReceiver]           VARCHAR (100) NULL,
    [RadicatedObservation]        VARCHAR (200) NULL,
    CONSTRAINT [PK_GlosaObjectionsReceptionD__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosaObjectionsReceptionC] FOREIGN KEY ([GlosaObjectionsReceptionCId]) REFERENCES [Glosas].[GlosaObjectionsReceptionC] ([Id]),
    CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosaPortfolioGlosada] FOREIGN KEY ([PortfolioGlosaId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id]),
    CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosasParametersInterface] FOREIGN KEY ([GlosasParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id]),
    CONSTRAINT [FK_GlosaObjectionsReceptionD_Responsible] FOREIGN KEY ([RadicateResponsibleId]) REFERENCES [Glosas].[Responsible] ([Id]),
    CONSTRAINT [UQ_GlosaObjectionsReceptionD__GlosaObjectionsReceptionCId__InvoiceNumber] UNIQUE NONCLUSTERED ([GlosaObjectionsReceptionCId] ASC, [InvoiceNumber] ASC)
);


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosaObjectionsReceptionC];


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosaPortfolioGlosada];


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosasParametersInterface];


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionD_Responsible];


GO
CREATE NONCLUSTERED INDEX [IX_GORD_ByReceptionC]
    ON [Glosas].[GlosaObjectionsReceptionD]([GlosaObjectionsReceptionCId] ASC)
    INCLUDE([PortfolioGlosaId], [Id]);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaObjectionsReceptionD_InvoiceNumber]
    ON [Glosas].[GlosaObjectionsReceptionD]([InvoiceNumber] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Sin Confirmar   2- Confirmada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'State';

