CREATE TABLE [MixingStation].[CMConfiguration] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MixingStationType] INT          NOT NULL,
    [Name]              VARCHAR (40) NOT NULL,
    [Code]              VARCHAR (20) NOT NULL,
    [State]             BIT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    [TimeStamp]         ROWVERSION   NOT NULL,
    [ManageMS]          INT          NULL,
    [Id_depmuncod]      CHAR (20)    NULL,
    [Prefix]            VARCHAR (4)  CONSTRAINT [DF_CMConfiguration_Prefix] DEFAULT ('CCMM') NOT NULL,
    [IdDirector]        INT          CONSTRAINT [DF__CMConfigu__IdDir__3FA1C8B7] DEFAULT ((1)) NOT NULL,
    [IdDirectorSp]      INT          CONSTRAINT [DF__CMConfigu__IdDir__4A1F572A] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_CMConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el IDDirectorSp', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfiguration', @level2type = N'COLUMN', @level2name = N'IdDirectorSp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el id de director', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfiguration', @level2type = N'COLUMN', @level2name = N'IdDirector';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el id del departamento o municipio ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfiguration', @level2type = N'COLUMN', @level2name = N'Id_depmuncod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Estado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfiguration', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfiguration', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Nombre', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfiguration', @level2type = N'COLUMN', @level2name = N'Name';

