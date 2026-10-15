CREATE TABLE [MedicalHistory].[ResultGroupsHistoryDynamicsIntra] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]     INT NOT NULL,
    [IdHistoryPages]  INT NOT NULL,
    [IdHistoryGroups] INT NOT NULL,
    [ValueOption]     INT NOT NULL,
    CONSTRAINT [PK_ResultGroupsHistoryDynamicsIntra] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opcion marcada grupo:
1 - Refiere  (Se le visualaiza al medico y SI diligencia algo de las variables de este grupo )

2 - No Registra  ó No aplica ó No refiere (Cuando se le visualiza al medico y por x o y motivo el medico NO diligencia nada)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultGroupsHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'ValueOption';

