CREATE TABLE [GeneralLedger].[MainAccountClasses] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NULL,
    [Nature]           TINYINT       NULL,
    [Type]             TINYINT       NULL,
    [Patrimony]        BIT           CONSTRAINT [DF_AccountClass_Patrimony] DEFAULT ((0)) NULL,
    [Status]           BIT           CONSTRAINT [DF_AccountClass_State] DEFAULT ((1)) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_MainAccountClasses__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

