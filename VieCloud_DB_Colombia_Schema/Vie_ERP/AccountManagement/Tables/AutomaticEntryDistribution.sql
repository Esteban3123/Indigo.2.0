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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso/atención: 1=Ambulatorio (consulta externa, urgencia sin hospitalización), 2=Hospitalario (internación, cama). TINYINT, nullable.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Ingreso. 1 - Ambulatorio, 2 - Hospitalario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de asignación automática del ingreso al facturador. DATETIME, registro de cuándo se distribuyó la atención para facturación.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AssignmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la asignación del ingreso al facturador', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AssignmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AssignmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del folio/detalle de control de ingresos. FK con RevenueControlDetail. INT, nullable. Vincula la distribución a línea de facturación.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del folio - Llave foranea con RevenueControlDetail', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del paciente. CHAR(10). FK con ADINGRESO.NUMINGRES. Identificador único de la atención a facturar.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del ingreso, proveniente de ADINGRESO', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del facturador asignado. FK con UsersAssignment.Id. INT. Define qué usuario de facturación recibió esta atención.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AssignedUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario asignado - Llave foranea con UsersAssignment que define el facturador al que se le asignó el ingreso', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AssignedUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'AssignedUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la distribución automática. INT, clave primaria. Rastrea cada asignación de ingreso a facturador.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de distribución automática de ingresos por admisión: guarda qué usuario fue asignado a cada ingreso/admisión, en qué fecha ocurrió la asignación automática y a qué detalle de control de ingresos corresponde, clasificando el tipo de entrada.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AutomaticEntryDistribution';
