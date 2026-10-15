CREATE TABLE [Contract].[Groupers] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)    NOT NULL,
    [Description]          VARCHAR (100)   NOT NULL,
    [ParentId]             INT             NULL,
    [UserNumber]           INT             CONSTRAINT [DF_Groupers_UserNumber] DEFAULT ((0)) NOT NULL,
    [UserMin]              INT             CONSTRAINT [DF_Groupers_UserMin] DEFAULT ((0)) NOT NULL,
    [UserMax]              INT             CONSTRAINT [DF_Groupers_UserMax] DEFAULT ((0)) NOT NULL,
    [ProjectCME]           NUMERIC (18)    CONSTRAINT [DF_Groupers_ProjectCME] DEFAULT ((0)) NOT NULL,
    [Frequence]            NUMERIC (18, 8) CONSTRAINT [DF_Groupers_Frequence] DEFAULT ((0)) NOT NULL,
    [TotalContract]        NUMERIC (18)    CONSTRAINT [DF_Groupers_TotalContract] DEFAULT ((0)) NOT NULL,
    [UserValue]            NUMERIC (18)    CONSTRAINT [DF_Groupers_UserValue] DEFAULT ((0)) NOT NULL,
    [MinimunRange]         INT             CONSTRAINT [DF_Groupers_MinimunRange] DEFAULT ((0)) NOT NULL,
    [MaximunRange]         INT             CONSTRAINT [DF_Groupers_MaximunRange] DEFAULT ((0)) NOT NULL,
    [MeasurementUnit]      TINYINT         CONSTRAINT [DF_Groupers_MeasurementUnit] DEFAULT ((0)) NOT NULL,
    [WarningFor]           INT             CONSTRAINT [DF_Groupers_WarningFor] DEFAULT ((0)) NOT NULL,
    [WarningMessage]       VARCHAR (500)   NULL,
    [MaximunRangeRestrict] BIT             CONSTRAINT [DF_Groupers_MaximunRangeRestrict] DEFAULT ((0)) NOT NULL,
    [RestrictMessage]      VARCHAR (500)   NULL,
    [Status]               BIT             NOT NULL,
    [CreationUser]         VARCHAR (20)    NOT NULL,
    [CreationDate]         DATETIME        NOT NULL,
    [ModificationUser]     VARCHAR (20)    NULL,
    [ModificationDate]     DATETIME        NULL,
    [TimeStamp]            ROWVERSION      NOT NULL,
    [Version]              INT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Groupers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groupers_Groupers] FOREIGN KEY ([ParentId]) REFERENCES [Contract].[Groupers] ([Id])
);


GO
ALTER TABLE [Contract].[Groupers] NOCHECK CONSTRAINT [FK_Groupers_Groupers];

