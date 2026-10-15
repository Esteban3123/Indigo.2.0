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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de atencion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'HealthCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la empresa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de empresa centro de atencion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'Id';

