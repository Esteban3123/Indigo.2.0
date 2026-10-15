CREATE TABLE [Glosas].[GlosaDevolutionsReceptionD] (
    [Id]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaDevolutionsReceptionCId] INT                                                                              NOT NULL,
    [GlosasParametersInterfaceId]  INT                                                                              NULL,
    [InvoiceNumber]                VARCHAR (50)                                                                     NOT NULL,
    [InvoiceDate]                  DATETIME                                                                         NOT NULL,
    [RadicatedNumber]              VARCHAR (50)                                                                     NULL,
    [RadicatedDate]                DATETIME                                                                         NULL,
    [BalanceInvoice]               MONEY                                                                            NULL,
    [PatientCode]                  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PatientName]                  VARCHAR (200) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NOT NULL,
    [Ingress]                      VARCHAR (15)                                                                     NOT NULL,
    [UserNameInvoice]              VARCHAR (200)                                                                    NOT NULL,
    [Comment]                      VARCHAR (500)                                                                    NULL,
    [PlanCode]                     VARCHAR (20)                                                                     NULL,
    [ContractCode]                 VARCHAR (20)                                                                     NOT NULL,
    [ContractName]                 VARCHAR (200)                                                                    NOT NULL,
    [State]                        CHAR (1)                                                                         NOT NULL,
    [TimeStamp]                    ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_GlosaDevolutionsReceptionD__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosaDevolutionsReceptionC] FOREIGN KEY ([GlosaDevolutionsReceptionCId]) REFERENCES [Glosas].[GlosaDevolutionsReceptionC] ([Id]),
    CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosasParametersInterface] FOREIGN KEY ([GlosasParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaDevolutionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosaDevolutionsReceptionC];


GO
ALTER TABLE [Glosas].[GlosaDevolutionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosasParametersInterface];

