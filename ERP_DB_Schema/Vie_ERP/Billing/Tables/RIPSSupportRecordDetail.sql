CREATE TABLE [Billing].[RIPSSupportRecordDetail] (
    [Id]                                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RIPSSupportRecordId]                   INT           NOT NULL,
    [InitialDate]                           DATETIME      NOT NULL,
    [FinalDate]                             DATETIME      NOT NULL,
    [FunctionalUnitId]                      INT           NOT NULL,
    [TypeStay]                              INT           NOT NULL,
    [CalculatedDaysStay]                    INT           NULL,
    [PerformsHealthProfessionalThirdPartyId] INT          NULL,
    [PerformsProfessionalSpecialty]         CHAR (3)      NULL,
    [MainDiagnosis]                         CHAR (4)      NOT NULL,
    [RelatedDiagnosis]                      CHAR (4)      NOT NULL,
    [DischargeCondition]                    CHAR (2)      NOT NULL,
    [DiagnosisCauseDeath]                   CHAR (4)      NULL,
    [CreationUser]                          VARCHAR (20)  CONSTRAINT [DF_RIPSSupportRecordDetail_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                          DATETIME      CONSTRAINT [DF_RIPSSupportRecordDetail_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                      VARCHAR (20)  NULL,
    [ModificationDate]                      DATETIME      NULL,
    [StayCUPSEntityContractDescriptionId]   INT           NULL,
    [StayCUPSEntityId]                      [int]         NULL,
    CONSTRAINT [PK_RIPSSupportRecordDetail_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RIPSSupportRecordDetail_RIPSSupportRecord] FOREIGN KEY ([RIPSSupportRecordId]) REFERENCES [Billing].[RIPSSupportRecord] ([Id]),
    CONSTRAINT [FK_RIPSSupportRecordDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_RIPSSupportRecordDetail_ThirdParty] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Billing].[RIPSSupportRecordDetail] NOCHECK CONSTRAINT [FK_RIPSSupportRecordDetail_RIPSSupportRecord];


GO
ALTER TABLE [Billing].[RIPSSupportRecordDetail] NOCHECK CONSTRAINT [FK_RIPSSupportRecordDetail_FunctionalUnit];


GO
ALTER TABLE [Billing].[RIPSSupportRecordDetail] NOCHECK CONSTRAINT [FK_RIPSSupportRecordDetail_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_RIPSSupportRecordDetail_RIPSSupportRecordId]
    ON [Billing].[RIPSSupportRecordDetail]([RIPSSupportRecordId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que guarda el detalle del registro de soporte para el archivo RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la causa de muerte del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'DiagnosisCauseDeath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de alta de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'DischargeCondition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico relacionado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'RelatedDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico principal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'MainDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especialidad del profesional de la salud que realiza la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del profesional de la salud que realiza la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días calculados de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CalculatedDaysStay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de estancia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'TypeStay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional asociada a la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización de la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'FinalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de soporte asociado al detalle.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'RIPSSupportRecordId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del contrato de entidad CUPs asociado a la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'StayCUPSEntityContractDescriptionId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad CUPs asociada a la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'StayCUPSEntityId';