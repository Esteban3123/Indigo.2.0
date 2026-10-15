CREATE TABLE [HumanTalent].[PVEndowmentsParameters_Dates] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EndowmentsParametersId] INT          NOT NULL,
    [CutOffDate]             DATETIME     NOT NULL,
    [CreationUser]           VARCHAR (50) NOT NULL,
    [CreationDate]           DATETIME     NOT NULL,
    [ModificationUser]       VARCHAR (50) NULL,
    [ModificationDate]       DATETIME     NULL,
    CONSTRAINT [PK_PVEndowmentsParameters_Dates] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVEndowmentsParameters_Dates_EndowmentsParameters] FOREIGN KEY ([EndowmentsParametersId]) REFERENCES [HumanTalent].[EndowmentsParameters] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVEndowmentsParameters_Dates] NOCHECK CONSTRAINT [FK_PVEndowmentsParameters_Dates_EndowmentsParameters];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsParameters_Dates', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsParameters_Dates', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsParameters_Dates', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsParameters_Dates', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de corte', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsParameters_Dates', @level2type = N'COLUMN', @level2name = N'CutOffDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de parametros de dotaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsParameters_Dates', @level2type = N'COLUMN', @level2name = N'EndowmentsParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsParameters_Dates', @level2type = N'COLUMN', @level2name = N'Id';

