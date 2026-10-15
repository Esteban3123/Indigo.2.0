CREATE TABLE [AccountManagement].[AutomaticEntryDistribution] (
    [Id]                     INT       IDENTITY (1, 1) NOT NULL,
    [AssignedUserId]         INT       NOT NULL,
    [AdmissionNumber]        CHAR (10) NOT NULL,
    [RevenueControlDetailId] INT       NULL,
    [AssignmentDate]         DATETIME  NOT NULL,
    [EntryType]              TINYINT   NULL,
    CONSTRAINT [PK_AutomaticEntryDistribution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AutomaticEntryDistribution_ADINGRESO] FOREIGN KEY ([AdmissionNumber]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_AutomaticEntryDistribution_UsersAssignment] FOREIGN KEY ([AssignedUserId]) REFERENCES [AccountManagement].[UsersAssignment] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Ingreso. 1 - Ambulatorio, 2 - Hospitalario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la asignación del ingreso al facturador', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AssignmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del folio - Llave foranea con RevenueControlDetail', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso, proveniente de ADINGRESO', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario asignado - Llave foranea con UsersAssignment que define el facturador al que se le asignó el ingreso', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AssignedUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'Id';

