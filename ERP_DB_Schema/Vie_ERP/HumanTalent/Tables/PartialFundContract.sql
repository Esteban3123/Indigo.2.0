CREATE TABLE [HumanTalent].[PartialFundContract] (
    [Id]                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FundId]                     INT          NOT NULL,
    [ContractId]                 INT          NOT NULL,
    [FundType]                   TINYINT      NOT NULL,
    [InitialDate]                DATE         NOT NULL,
    [EndingDate]                 DATE         NULL,
    [MembershipNumber]           VARCHAR (15) NOT NULL,
    [VoluntaryContribution]      BIT          NOT NULL,
    [VoluntaryContributionValue] NUMERIC (18) NULL,
    [State]                      BIT          NOT NULL,
    [CreationUser]               VARCHAR (20) CONSTRAINT [DF_FundContract_CreationUser] DEFAULT ((999)) NULL,
    [CreationDate]               DATETIME     CONSTRAINT [DF_FundContract_CreationDate] DEFAULT ([Common].[getdate]()) NULL,
    [ModificationUser]           VARCHAR (20) NULL,
    [ModificationDate]           DATETIME     NULL,
    CONSTRAINT [PK_FundContract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FundContract_Fund] FOREIGN KEY ([FundId]) REFERENCES [Payroll].[Fund] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PartialFundContract] NOCHECK CONSTRAINT [FK_FundContract_Fund];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de contribucion voluntaria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'VoluntaryContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contribucion voluntaria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'VoluntaryContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de afiliacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'MembershipNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de desafiliacion del fondo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'EndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de afiliacion al fondo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de fondo   1 - Salud   2 - Pension   3 - Cesantias   4 - Riesgo    5 - Caja Compensacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'FundType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del fondo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'FundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla Fondos del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialFundContract', @level2type = N'COLUMN', @level2name = N'Id';

