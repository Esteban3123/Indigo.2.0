CREATE TABLE [MixingStation].[ExternalCareCenterUsers] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ExternalCareCenterId] INT          NOT NULL,
    [UserId]               INT          NOT NULL,
    [UserCode]             VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ExternalCareCenterUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExternalCareCenterUsers_ExternalCareCenter] FOREIGN KEY ([ExternalCareCenterId]) REFERENCES [MixingStation].[ExternalCareCenter] ([Id])
);


GO
ALTER TABLE [MixingStation].[ExternalCareCenterUsers] NOCHECK CONSTRAINT [FK_ExternalCareCenterUsers_ExternalCareCenter];

