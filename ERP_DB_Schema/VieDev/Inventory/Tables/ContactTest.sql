CREATE TABLE [Inventory].[ContactTest] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (25)  NOT NULL,
    [Name]             VARCHAR (150) NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [CreationUser]     CHAR (20)     NOT NULL,
    [ModificationDate] DATETIME      NULL,
    [ModificationUser] CHAR (20)     NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_AdmissionType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

