CREATE TABLE [Common].[ThirdParty] (
    [Id]                                  INT                                                                      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]                            INT                                                                      NOT NULL,
    [Nit]                                 VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')    NOT NULL,
    [DigitVerification]                   VARCHAR (1)                                                              CONSTRAINT [DF_ThirdParty_DigitVerification] DEFAULT ((1)) NULL,
    [Name]                                VARCHAR (300) MASKED WITH (FUNCTION = 'partial(0, "Third_Ofuscado", 0)') NOT NULL,
    [PersonType]                          TINYINT                                                                  NOT NULL,
    [RetentionType]                       TINYINT                                                                  NOT NULL,
    [ContributionType]                    TINYINT                                                                  NOT NULL,
    [StateEnterpriseType]                 TINYINT                                                                  CONSTRAINT [DF_ThirdParty_StateEnterpriseType] DEFAULT ((0)) NOT NULL,
    [IVARetentionAccountPayableConceptId] INT                                                                      NULL,
    [Ica]                                 BIT                                                                      NOT NULL,
    [IcaPercentage]                       NUMERIC (5, 3)                                                           NOT NULL,
    [IcaTop]                              BIT                                                                      NOT NULL,
    [IcaTopValue]                         NUMERIC (18, 2)                                                          NOT NULL,
    [EntityCode]                          VARCHAR (15)                                                             NULL,
    [EconomicActivityId]                  INT                                                                      NULL,
    [Class]                               TINYINT                                                                  NULL,
    [DigitalSignature]                    VARBINARY (MAX)                                                          NULL,
    [CodeCIIU]                            VARCHAR (10)                                                             NULL,
    [State]                               BIT                                                                      CONSTRAINT [DF_ThirdParty_State] DEFAULT ((0)) NOT NULL,
    [CreationDate]                        DATETIME                                                                 NOT NULL,
    [UserId]                              INT                                                                      NOT NULL,
    [HandlesBranchOffice]                 BIT                                                                      CONSTRAINT [DF_ThirdParty_HandlesBranchOffice] DEFAULT ((0)) NOT NULL,
    [CodeDivipola]                        VARCHAR (20)                                                             NULL,
    [IVARetentionConceptId]               INT                                                                      NULL,
    [ElectronicBiller]                    BIT                                                                      CONSTRAINT [DF__ThirdPart__Elect__722D4884] DEFAULT ((0)) NOT NULL,
    [CreationUser]                        VARCHAR (20)                                                             NULL,
    [ModificationUser]                    VARCHAR (20)                                                             NULL,
    [ModificationDate]                    DATETIME                                                                 NULL,
    CONSTRAINT [PK_ThirdParty__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdParty_AccountPayableConcepts] FOREIGN KEY ([IVARetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_ThirdParty_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_ThirdParty_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_ThirdParty_RetentionConcepts] FOREIGN KEY ([IVARetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Common].[ThirdParty] NOCHECK CONSTRAINT [FK_ThirdParty_AccountPayableConcepts];


GO
ALTER TABLE [Common].[ThirdParty] NOCHECK CONSTRAINT [FK_ThirdParty_EconomicActivity];


GO
ALTER TABLE [Common].[ThirdParty] NOCHECK CONSTRAINT [FK_ThirdParty_Person];


GO
ALTER TABLE [Common].[ThirdParty] NOCHECK CONSTRAINT [FK_ThirdParty_RetentionConcepts];


GO
CREATE NONCLUSTERED INDEX [nci_wi_ThirdParty_E0A415FF1E7850F34015A768B185D078]
    ON [Common].[ThirdParty]([PersonId] ASC)
    INCLUDE([Name], [Nit]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase del tercero, solo se solicita si es de tipo juridico  1 - Nacional  2 - Extranjero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Class';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Empesa estatal  0 - No Aplica  1 - Municipal  2 - Departamental  3 - Distrital', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'StateEnterpriseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de contribuyente 0 - No responsable de Iva, 1 - Responsables de Iva, 2 - Empresa estatal, 3 - Gran Contribuyente, 4 - Regimen Simple,  5- Exento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ContributionType';

