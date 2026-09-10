CREATE TABLE [dbo].[BillableNursingActivityServices] (
    [Id]                               INT       IDENTITY (1, 1) NOT NULL,
    [CODACTENF]                        CHAR (3)  NOT NULL,
    [CODSERIPS]                        CHAR (20) NULL,
    [CUPSEntityId]                     INT       NULL,
    [CUPSEntityContractDescriptionsId] INT       NULL,
    CONSTRAINT [PK_BillableNursingActivityServices] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillableNursingActivityServices_ContractDescriptions] FOREIGN KEY ([CUPSEntityContractDescriptionsId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_BillableNursingActivityServices_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_BillableNursingActivityServices_HCACTENFE] FOREIGN KEY ([CODACTENF]) REFERENCES [dbo].[HCACTENFE] ([CODACTENF]),
    CONSTRAINT [FK_BillableNursingActivityServices_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la descripción del servicio CUPS vinculada al contrato; referencia a ContractDescriptions para detalles de cobertura y facturación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripcion relacionada del cups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio CUPS (Código Único de Procedimientos en Salud); enlace a catálogo de servicios facturables de enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio CUPS en formato RIPS (Registro Individual de Prestación de Servicios); clave para facturación y reportes de salud; máximo 20 caracteres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del servicio CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de actividad de enfermería (3 dígitos); clasificación de procedimientos de cuidado y atención de enfermería; referencia a tabla HCACTENFE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CODACTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la actividad de enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CODACTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CODACTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY); clave primaria de la tabla de servicios facturables de enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo autoincrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las actividades de enfermería facturables con sus códigos de servicio CUPS/IPS correspondientes, permitiendo asociar cada actividad de enfermería a un contrato o entidad específica para efectos de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices';
