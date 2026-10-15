CREATE TABLE [Glosas].[GlosasParametersInterface] (
    [Id]                                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContainerName]                        VARCHAR (50)  NOT NULL,
    [CompanyName]                          VARCHAR (100) NOT NULL,
    [Interface]                            BIT           NOT NULL,
    [AccountingMethod]                     INT           NOT NULL,
    [AffectsService]                       BIT           NOT NULL,
    [AccountantAccountGeneralAcceptanceC]  VARCHAR (15)  NULL,
    [AccountantAccountPreviousAcceptanceC] VARCHAR (15)  NULL,
    [DebitAccount]                         VARCHAR (15)  NULL,
    [AcceptanceObjectionsConcept]          VARCHAR (15)  NULL,
    [AcceptanceObjectionsConceptPast]      VARCHAR (15)  NULL,
    [AcceptancReiterationsConcept]         VARCHAR (15)  NULL,
    [DateConfiguration]                    DATETIME      CONSTRAINT [DF_GlosasParametersInterface_DateConfiguration] DEFAULT ([Common].[getdate]()) NOT NULL,
    [RadicateCodeNoteAccounting]           VARCHAR (10)  NULL,
    [DevolutionCodeNoteAccounting]         VARCHAR (10)  NULL,
    [DevolutionInjustificate]              INT           NULL,
    [CreationUser]                         VARCHAR (20)  CONSTRAINT [DF_GlosasParametersInterface_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                         DATETIME      CONSTRAINT [DF_GlosasParametersInterface_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                     VARCHAR (20)  NULL,
    [ModificationDate]                     DATETIME      NULL,
    [TimeStamp]                            ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosasParametersInterface__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1) liberar factura del radicado  2) mantener factura en el radicado  3) definido por el usuario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';

