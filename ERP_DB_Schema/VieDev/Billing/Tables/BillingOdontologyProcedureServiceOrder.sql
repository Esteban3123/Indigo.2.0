CREATE TABLE [Billing].[BillingOdontologyProcedureServiceOrder] (
    [Id]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber] VARCHAR (20) NOT NULL,
    [Folio]           VARCHAR (10) NOT NULL,
    [ProcedureCode]   INT          NOT NULL,
    [ServiceOrderId]  INT          NOT NULL,
    CONSTRAINT [PK_BillingOdontologyProcedureServiceOrder] PRIMARY KEY CLUSTERED ([Id] ASC)
);

