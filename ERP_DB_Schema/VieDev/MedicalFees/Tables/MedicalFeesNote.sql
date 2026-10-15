CREATE TABLE [MedicalFees].[MedicalFeesNote] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber]  CHAR (10)     NOT NULL,
    [InvoiceId]        INT           NOT NULL,
    [Note]             VARCHAR (MAX) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_MedicalFeesNote] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFeesNote_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id])
);


GO
ALTER TABLE [MedicalFees].[MedicalFeesNote] NOCHECK CONSTRAINT [FK_MedicalFeesNote_Invoice];

