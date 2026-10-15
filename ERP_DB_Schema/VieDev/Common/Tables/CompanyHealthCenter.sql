CREATE TABLE [Common].[CompanyHealthCenter] (
    [Id]             INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CompanyId]      INT     NOT NULL,
    [HealthCenterId] TINYINT NOT NULL,
    CONSTRAINT [PK_CompanyHealthCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompanyHealthCenter_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Common].[Company] ([Id]),
    CONSTRAINT [FK_CompanyHealthCenter_HealthCenter] FOREIGN KEY ([HealthCenterId]) REFERENCES [Common].[HealthCenter] ([Id])
);


GO
ALTER TABLE [Common].[CompanyHealthCenter] NOCHECK CONSTRAINT [FK_CompanyHealthCenter_Company];


GO
ALTER TABLE [Common].[CompanyHealthCenter] NOCHECK CONSTRAINT [FK_CompanyHealthCenter_HealthCenter];

