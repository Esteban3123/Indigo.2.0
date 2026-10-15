CREATE TABLE [Common].[Attachment] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FormId]           INT             NOT NULL,
    [EntityName]       VARCHAR (250)   NOT NULL,
    [EntityId]         INT             NOT NULL,
    [Name]             VARCHAR (250)   NOT NULL,
    [Extension]        VARCHAR (10)    NOT NULL,
    [Description]      VARCHAR (MAX)   NOT NULL,
    [FileAttached]     VARBINARY (MAX) NOT NULL,
    [CreationUser]     VARCHAR (20)    NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    [EntityDetailId]   INT             NULL,
    [EntityDetailName] VARCHAR (250)   NULL,
    CONSTRAINT [PK_Common_Attachment] PRIMARY KEY CLUSTERED ([Id] ASC)
);

