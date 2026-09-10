CREATE TABLE [Audit].[Audit] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Action]      TINYINT       NOT NULL,
    [Entity]      VARCHAR (50)  NOT NULL,
    [EntityKey]   INT           NOT NULL,
    [IdAudit]     INT           NULL,
    [Date]        DATETIME      NOT NULL,
    [Users]       VARCHAR (50)  NOT NULL,
    [Form]        VARCHAR (50)  NOT NULL,
    [UserWindows] VARCHAR (250) NOT NULL,
    [Workstation] VARCHAR (100) NOT NULL,
    [Application] VARCHAR (100) NULL,
    [Company]     VARCHAR (2)   CONSTRAINT [DF_Audit_Company] DEFAULT ((1)) NOT NULL,
    [IsParent]    BIT           NOT NULL,
    CONSTRAINT [PK_Audit_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Audit_Id_Audit_IdAudit] FOREIGN KEY ([IdAudit]) REFERENCES [Audit].[Audit] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Audit_Company]
    ON [Audit].[Audit]([Company] ASC);

