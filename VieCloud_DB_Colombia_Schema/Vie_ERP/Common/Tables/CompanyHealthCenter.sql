CREATE TABLE [Common].[CompanyHealthCenter] (
    [Id]             INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CompanyId]      INT     NOT NULL,
    [HealthCenterId] TINYINT NOT NULL,
    CONSTRAINT [PK_CompanyHealthCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompanyHealthCenter_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Common].[Company] ([Id]),
    CONSTRAINT [FK_CompanyHealthCenter_HealthCenter] FOREIGN KEY ([HealthCenterId]) REFERENCES [Common].[HealthCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de atención, unidad funcional o sede donde se brinda atención médica. FK a [Common].[HealthCenter]. Tipo: TINYINT.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'HealthCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de atencion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'HealthCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'HealthCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la empresa, asegurador, ARL o prestador de servicios de salud. FK a [Common].[Company]. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la empresa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la relación empresa-centro de atención. Clave primaria clustered. Tipo: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de empresa centro de atencion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las empresas o compañías con los centros de atención médica habilitados para operar, definiendo qué sedes o centros de salud pertenecen a cada compañía dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyHealthCenter';
