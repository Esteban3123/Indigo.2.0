CREATE TABLE [Billing].[SlipOut] (
    [Id]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]            VARCHAR (20) NOT NULL,
    [AdmissionNumber] CHAR (10)    NOT NULL,
    [DocumentDate]    DATE         NOT NULL,
    [CreationUser]    VARCHAR (20) NOT NULL,
    [CreationDate]    DATETIME     NOT NULL,
    CONSTRAINT [PK_SlipOut__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

