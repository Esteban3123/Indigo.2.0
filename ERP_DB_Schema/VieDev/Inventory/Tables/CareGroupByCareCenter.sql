CREATE TABLE [Inventory].[CareGroupByCareCenter] (
    [Id]             INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]    INT       NOT NULL,
    [CareCenterCode] CHAR (10) NOT NULL,
    CONSTRAINT [PK_CareGroupByCareCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

