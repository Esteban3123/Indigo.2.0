CREATE TABLE [Common].[Country] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (2)  NOT NULL,
    [Name]                VARCHAR (80) NOT NULL,
    [Nationality]         VARCHAR (50) NULL,
    [State]               BIT          NOT NULL,
    [CreationUser]        VARCHAR (20) CONSTRAINT [DF_Country_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME     CONSTRAINT [DF_Country_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    [TimeStamp]           ROWVERSION   NOT NULL,
    [StandardCode]        VARCHAR (3)  CONSTRAINT [DF_Country_StandardCode] DEFAULT ((0)) NOT NULL,
    [StandardCodeNumeric] VARCHAR (3)  NULL,
    CONSTRAINT [PK_Country__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Country__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Me identifica el codigo estandar ISO 3166-1 (numérico) de los paises de acuerdo a la nacionalidad para los pacientes extranjeros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'StandardCodeNumeric';

