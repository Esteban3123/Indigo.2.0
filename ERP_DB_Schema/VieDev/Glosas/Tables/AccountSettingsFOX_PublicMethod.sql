CREATE TABLE [Glosas].[AccountSettingsFOX_PublicMethod] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT           NULL,
    [PlanCode]              VARCHAR (20)  NOT NULL,
    [Denomination]          VARCHAR (100) NULL,
    [InvoiceRadicate]       VARCHAR (20)  NULL,
    CONSTRAINT [PK_AccountSettingsFOX_PublicMethod] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountSettingsFOX_PublicMethod_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[AccountSettingsFOX_PublicMethod] NOCHECK CONSTRAINT [FK_AccountSettingsFOX_PublicMethod_GlosasParametersInterface];

