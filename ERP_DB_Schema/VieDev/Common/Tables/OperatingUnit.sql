CREATE TABLE [Common].[OperatingUnit] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdUnit]           INT           NULL,
    [UnitName]         VARCHAR (100) NOT NULL,
    [UnitCode]         VARCHAR (5)   NULL,
    [IPSCode]          VARCHAR (20)  NULL,
    [Address]          VARCHAR (100) NULL,
    [Phone]            VARCHAR (20)  NULL,
    [Email]            VARCHAR (100) NULL,
    [EmailAudit]       VARCHAR (100) NULL,
    [IdCity]           INT           NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_OperatingUnit_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_OperatingUnit_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_OperatingUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OperatingUnit_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_OperatingUnit_OperatingUnit] FOREIGN KEY ([IdUnit]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Common].[OperatingUnit] NOCHECK CONSTRAINT [FK_OperatingUnit_City];


GO
ALTER TABLE [Common].[OperatingUnit] NOCHECK CONSTRAINT [FK_OperatingUnit_OperatingUnit];

