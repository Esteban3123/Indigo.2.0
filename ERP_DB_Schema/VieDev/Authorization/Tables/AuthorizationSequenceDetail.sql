CREATE TABLE [Authorization].[AuthorizationSequenceDetail] (
    [Id]                       INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseAuthorizationC] INT    NOT NULL,
    [IdSequense]               INT    NOT NULL,
    [IdOperatingUnit]          INT    NULL,
    [Next]                     BIGINT CONSTRAINT [DF_AuthorizationSequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_AuthorizationSequenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationSequenceDetail_AuthorizationSequence] FOREIGN KEY ([IdSequenseAuthorizationC]) REFERENCES [Authorization].[AuthorizationSequence] ([Id]),
    CONSTRAINT [FK_AuthorizationSequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_AuthorizationSequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationSequenceDetail] NOCHECK CONSTRAINT [FK_AuthorizationSequenceDetail_AuthorizationSequence];


GO
ALTER TABLE [Authorization].[AuthorizationSequenceDetail] NOCHECK CONSTRAINT [FK_AuthorizationSequenceDetail_OperatingUnit];


GO
ALTER TABLE [Authorization].[AuthorizationSequenceDetail] NOCHECK CONSTRAINT [FK_AuthorizationSequenceDetail_Sequense];

