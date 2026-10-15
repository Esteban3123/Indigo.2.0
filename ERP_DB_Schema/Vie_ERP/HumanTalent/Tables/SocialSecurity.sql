CREATE TABLE [HumanTalent].[SocialSecurity] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateId]           INT          NOT NULL,
    [PromotingEntityId]     INT          NULL,
    [Pensioner]             INT          NULL,
    [PensionaryTypeId]      INT          NULL,
    [AdminFundId]           INT          NULL,
    [OcoupationalHazardsId] INT          NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    CONSTRAINT [PK__SocialSe__3214EC074F5541C7] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_AdminFundSS] FOREIGN KEY ([AdminFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [fk_CandidateIdSS] FOREIGN KEY ([CandidateId]) REFERENCES [HumanTalent].[Candidate] ([Id]),
    CONSTRAINT [fk_OcoupationalHazardsSS] FOREIGN KEY ([OcoupationalHazardsId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [fk_PensionaryTypeSS] FOREIGN KEY ([PensionaryTypeId]) REFERENCES [Payroll].[PensionaryType] ([Id]),
    CONSTRAINT [fk_PromotingEntitySS] FOREIGN KEY ([PromotingEntityId]) REFERENCES [Payroll].[Fund] ([Id])
);


GO
ALTER TABLE [HumanTalent].[SocialSecurity] NOCHECK CONSTRAINT [fk_AdminFundSS];


GO
ALTER TABLE [HumanTalent].[SocialSecurity] NOCHECK CONSTRAINT [fk_CandidateIdSS];


GO
ALTER TABLE [HumanTalent].[SocialSecurity] NOCHECK CONSTRAINT [fk_OcoupationalHazardsSS];


GO
ALTER TABLE [HumanTalent].[SocialSecurity] NOCHECK CONSTRAINT [fk_PensionaryTypeSS];


GO
ALTER TABLE [HumanTalent].[SocialSecurity] NOCHECK CONSTRAINT [fk_PromotingEntitySS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de riesgos ocupacionales', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'OcoupationalHazardsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del  fondo del administrador', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'AdminFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del  de  tipo pensionario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'PensionaryTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pensionista', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'Pensioner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la promocion  de la entifdad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'PromotingEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SocialSecurity', @level2type = N'COLUMN', @level2name = N'Id';

