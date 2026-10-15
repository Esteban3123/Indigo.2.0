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
ALTER TABLE [dbo].[BillableNursingActivityServices] NOCHECK CONSTRAINT [FK_BillableNursingActivityServices_ContractDescriptions];


GO
ALTER TABLE [dbo].[BillableNursingActivityServices] NOCHECK CONSTRAINT [FK_BillableNursingActivityServices_CUPSEntity];


GO
ALTER TABLE [dbo].[BillableNursingActivityServices] NOCHECK CONSTRAINT [FK_BillableNursingActivityServices_HCACTENFE];


GO
ALTER TABLE [dbo].[BillableNursingActivityServices] NOCHECK CONSTRAINT [FK_BillableNursingActivityServices_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la descripcion relacionada del cups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del servicio CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la actividad de enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'CODACTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo autoincrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BillableNursingActivityServices', @level2type = N'COLUMN', @level2name = N'Id';

