CREATE TABLE [Glosas].[ConciliationC] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConciliationConsecutive] NUMERIC (18)  NOT NULL,
    [Nit]                     VARCHAR (15)  NOT NULL,
    [NitName]                 VARCHAR (150) NOT NULL,
    [ConciliationDate]        DATETIME      NOT NULL,
    [DocumentDate]            DATETIME      NOT NULL,
    [DocumentNumber]          VARCHAR (50)  NOT NULL,
    [Comment]                 VARCHAR (500) NULL,
    [State]                   CHAR (1)      NOT NULL,
    [ConfirmDate]             DATETIME      NULL,
    [ConfirmUser]             INT           NULL,
    [RadicatedUser]           INT           NULL,
    [CreationUser]            VARCHAR (20)  CONSTRAINT [DF_ConciliationC_CreationUser] DEFAULT ((9999)) NOT NULL,
    [CreationDate]            DATETIME      CONSTRAINT [DF_ConciliationC_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ConciliationC] PRIMARY KEY CLUSTERED ([Id] ASC)
);

