CREATE TABLE [MedicalHistory].[ResultGroupsHistoryDynamicsIntra] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]     INT NOT NULL,
    [IdHistoryPages]  INT NOT NULL,
    [IdHistoryGroups] INT NOT NULL,
    [ValueOption]     INT NOT NULL,
    CONSTRAINT [PK_ResultGroupsHistoryDynamicsIntra] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opción marcada en grupo de resultados dinámicos: 1=Refiere (grupo visible al médico con variables diligenciadas), 2=No Registra/No Aplica/No Refiere (grupo visible pero sin datos ingresados por el profesional). Indica si el grupo de exámenes/procedimientos fue documentado o no en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'ValueOption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opcion marcada grupo:
1 - Refiere  (Se le visualaiza al medico y SI diligencia algo de las variables de este grupo )

2 - No Registra  ó No aplica ó No refiere (Cuando se le visualiza al medico y por x o y motivo el medico NO diligencia nada)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'ValueOption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'ValueOption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de opciones seleccionadas en grupos dinámicos de la historia clínica intrahospitalaria. Guarda qué valor eligió el profesional en cada grupo de campos dentro de las páginas dinámicas de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de opción seleccionada en el grupo dinámico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la historia clínica intrahospitalaria del paciente a la que pertenece este registro.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la página dinámica de la historia clínica donde se encuentra el grupo de campos.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryPages';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryPages';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de campos dinámicos dentro de la página de historia clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryGroups';
