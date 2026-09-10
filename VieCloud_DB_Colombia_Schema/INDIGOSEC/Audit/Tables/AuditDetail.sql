CREATE TABLE [Audit].[AuditDetail] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [IdAudit]           INT           NOT NULL,
    [AssociatedTable]   VARCHAR (50)  NULL,
    [IdAuditAssociated] INT           NULL,
    [Property]          VARCHAR (100) NOT NULL,
    [PreviousValue]     VARCHAR (MAX) NULL,
    [NewValue]          VARCHAR (MAX) NULL,
    CONSTRAINT [PK_AuditDetail_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditDetail_IdAudit_Audit_Id] FOREIGN KEY ([IdAudit]) REFERENCES [Audit].[Audit] ([Id]),
    CONSTRAINT [FK_AuditDetail_IdAuditAssociated_Audit_Id] FOREIGN KEY ([IdAuditAssociated]) REFERENCES [Audit].[Audit] ([Id])
);

