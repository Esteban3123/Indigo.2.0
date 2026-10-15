CREATE TABLE [Common].[CompanyIndigo] (
    [Id]                  TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [VerificationNumber]  CHAR (1)      NULL,
    [CompanyCode]         CHAR (2)      NOT NULL,
    [CompanyName]         VARCHAR (90)  NOT NULL,
    [City]                VARCHAR (50)  NULL,
    [RepresentationLegal] VARCHAR (50)  NULL,
    [Telephone]           VARCHAR (100) NULL,
    [Address]             VARCHAR (100) NULL,
    [KeyCode]             CHAR (20)     NOT NULL,
    [CompanyNit]          CHAR (15)     NOT NULL,
    [IdentificationType]  CHAR (1)      NULL,
    [Identification]      CHAR (10)     NULL,
    [ProductionCompany]   BIT           NOT NULL,
    [State]               BIT           NOT NULL,
    [Synchronized]        CHAR (1)      NOT NULL,
    [LyncIntegration]     BIT           NULL,
    CONSTRAINT [PK_CompanyIndigo__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificacion del Prestador  1: CC  2: Nit', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'IdentificationType';

