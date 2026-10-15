CREATE TABLE [Security].[TenantContainerInt] (
    [Id]          BIGINT   NOT NULL,
    [TenantId]    SMALLINT NOT NULL,
    [ContainerId] INT      NOT NULL,
    [Principal]   BIT      NOT NULL,
    [State]       BIT      NOT NULL
);

