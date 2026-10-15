CREATE TABLE [Glosas].[AccountSettingsNET_PrivateMethod] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT           NULL,
    [Denomination]          VARCHAR (100) NOT NULL,
    [InvoiceNotRadicate]    VARCHAR (20)  NULL,
    [InvoiceRadicate]       VARCHAR (20)  NULL,
    [RectifiableGlosa]      VARCHAR (5)   NULL,
    [LegalProcess]          VARCHAR (20)  NULL,
    [Conciliation]          VARCHAR (5)   NULL,
    CONSTRAINT [PK_AccountSettingsNET_PrivateMethod] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountSettingsNET_PrivateMethod_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[AccountSettingsNET_PrivateMethod] NOCHECK CONSTRAINT [FK_AccountSettingsNET_PrivateMethod_GlosasParametersInterface];

