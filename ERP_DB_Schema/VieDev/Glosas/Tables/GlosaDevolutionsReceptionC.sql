CREATE TABLE [Glosas].[GlosaDevolutionsReceptionC] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RadicatedConsecutive]       INT           NOT NULL,
    [DocumentNumber]             VARCHAR (30)  NOT NULL,
    [CustomerId]                 INT           NOT NULL,
    [RadicatedDate]              DATETIME      NOT NULL,
    [DocumentDate]               DATETIME      NOT NULL,
    [Comment]                    VARCHAR (250) NOT NULL,
    [State]                      CHAR (1)      NOT NULL,
    [ConfirmDate]                DATETIME      NULL,
    [ReceivesDevolution]         VARCHAR (50)  NULL,
    [ReceivesDevolutionPosition] VARCHAR (50)  NULL,
    [PersonSends]                VARCHAR (50)  NULL,
    [PersonSendsPosition]        VARCHAR (50)  NULL,
    [CreationUser]               VARCHAR (20)  CONSTRAINT [DF_GlosaDevolutionsReceptionC_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]               DATETIME      CONSTRAINT [DF_GlosaDevolutionsReceptionC_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosaDevolutionsReceptionC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaDevolutionsReceptionC_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaDevolutionsReceptionC] NOCHECK CONSTRAINT [FK_GlosaDevolutionsReceptionC_Customer];

