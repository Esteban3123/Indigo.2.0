CREATE TABLE [SelfService].[PortalUserCompany] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [PortalUserId]           INT           NOT NULL,
    [CompanyName]            VARCHAR (100) NOT NULL,
    [TransactionalContainer] VARCHAR (30)  NOT NULL,
    [EmployeeId]             INT           NOT NULL,
    CONSTRAINT [PK_PortalUserCompany] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortalUserCompany_PortalUser] FOREIGN KEY ([PortalUserId]) REFERENCES [SelfService].[PortalUser] ([Id])
);

