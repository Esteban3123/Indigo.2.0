CREATE TABLE [Glosas].[PartialPaymentsD] (
    [Id]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PartialPaymentsCId]       INT                                                                              NOT NULL,
    [PortfolioGlosaId]         INT                                                                              NULL,
    [InvoiceNumber]            VARCHAR (50)                                                                     NOT NULL,
    [InvoiceDate]              DATETIME                                                                         NOT NULL,
    [RadicatedNumber]          VARCHAR (50)                                                                     NOT NULL,
    [RadicatedDate]            DATETIME                                                                         NOT NULL,
    [PatientCode]              VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [PatientName]              VARCHAR (200) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NULL,
    [ContractCode]             VARCHAR (15)                                                                     NOT NULL,
    [ValuePendingConciliation] MONEY                                                                            NULL,
    [ValuePayments]            MONEY                                                                            NULL,
    [State]                    CHAR (1)                                                                         NOT NULL,
    [TimeStamp]                ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_PartialPaymentsD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PartialPaymentsD_GlosaPortfolioGlosada] FOREIGN KEY ([PortfolioGlosaId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id]),
    CONSTRAINT [FK_PartialPaymentsD_PartialPaymentsC] FOREIGN KEY ([PartialPaymentsCId]) REFERENCES [Glosas].[PartialPaymentsC] ([Id])
);


GO
ALTER TABLE [Glosas].[PartialPaymentsD] NOCHECK CONSTRAINT [FK_PartialPaymentsD_GlosaPortfolioGlosada];


GO
ALTER TABLE [Glosas].[PartialPaymentsD] NOCHECK CONSTRAINT [FK_PartialPaymentsD_PartialPaymentsC];

