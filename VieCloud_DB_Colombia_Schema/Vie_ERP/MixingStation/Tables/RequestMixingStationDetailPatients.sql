CREATE TABLE [MixingStation].[RequestMixingStationDetailPatients] (
    [Id]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestMixingStationDetailId] INT                                                                              NOT NULL,
    [PatientCode]                  VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FunctionalUnitCode]           VARCHAR (20)                                                                     NULL,
    [Bed]                          VARCHAR (20)                                                                     NULL,
    [Quantity]                     INT                                                                              NOT NULL,
    [EntityId]                     INT                                                                              NOT NULL,
    [EntityName]                   VARCHAR (300)                                                                    NOT NULL,
    [Status]                       TINYINT                                                                          NOT NULL,
    [AdministrationRouteId]        INT                                                                              CONSTRAINT [DF_RequestMixingStationDetailPatients_AdministrationRouteId] DEFAULT ((1)) NULL,
    [CampaignDetailId]             INT                                                                              NULL,
    [AnnulmentUser]                VARCHAR (20)                                                                     NULL,
    [AnnulmentDate]                DATETIME                                                                         NULL,
    CONSTRAINT [PK_RequestMixingStationDetailPatients] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestMixingStationDetailPatients_AdministrationRoute] FOREIGN KEY ([AdministrationRouteId]) REFERENCES [Inventory].[AdministrationRoute] ([Id]),
    CONSTRAINT [FK_RequestMixingStationDetailPatients_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_RequestMixingStationDetailPatients_RequestMixingStationDetail] FOREIGN KEY ([RequestMixingStationDetailId]) REFERENCES [MixingStation].[RequestMixingStationDetail] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetailPatients] NOCHECK CONSTRAINT [FK_RequestMixingStationDetailPatients_CampaignDetail];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetailPatients] NOCHECK CONSTRAINT [FK_RequestMixingStationDetailPatients_RequestMixingStationDetail];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [MixingStation].[RequestMixingStationDetailPatients].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
ALTER TABLE [MixingStation].[RequestMixingStationDetailPatients] NOCHECK CONSTRAINT [FK_RequestMixingStationDetailPatients_CampaignDetail];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetailPatients] NOCHECK CONSTRAINT [FK_RequestMixingStationDetailPatients_RequestMixingStationDetail];


GO

ALTER TABLE [MixingStation].[RequestMixingStationDetailPatients] NOCHECK CONSTRAINT [FK_RequestMixingStationDetailPatients_CampaignDetail];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetailPatients] NOCHECK CONSTRAINT [FK_RequestMixingStationDetailPatients_RequestMixingStationDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación del registro de paciente en estación de mezcla; DATETIME, registra cuándo se canceló la solicitud', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la anulación; VARCHAR(20), identificación de quién canceló el registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de detalle de campaña asociado; INT FK a CampaignDetail, vincula a campañas de atención o vacunación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de vía de administración (oral, IV, IM, etc.); INT FK a AdministrationRoute, por defecto 1', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la vía de administración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Pendiente (inicial), 2=Confirmado (aprobado), 3=Anulado (cancelado); TINYINT', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro:    1. En su Estado Inicial este en el Estado "Pendiente"  2. Confirmar se coloque "Confirmado"  3. Anular se coloque "Anulado"', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad prestadora o centro de atención que realiza el registro; VARCHAR(300), puede ser hospital, clínica, EPS', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad con la cual se realiza este registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad prestadora o IPS asociada; INT, referencia a centro de atención o prestador', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad con la cual se realiza este registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de dosis, unidades o volumen a administrar al paciente; INT, número de unidades del medicamento/insumo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de cama del paciente en la unidad funcional; VARCHAR(20), ubicación física del paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Bed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cama del paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Bed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Bed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (interno/externo): urgencia, hospitalización, consulta externa, etc.; VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la unidad funcional, puede ser externo o interno', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del paciente; VARCHAR(20) PII MASKED, equivalente a cédula, documento, identificación única del paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle padre de solicitud a estación de mezcla; INT FK, vincula a RequestMixingStationDetail', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de paciente en solicitud de mezcla; INT IDENTITY, clave primaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pacientes asignados a cada ítem de una solicitud de mezcla en la estación de mezclas (farmacia). Registra qué paciente, en qué unidad y cama, recibe determinada cantidad de un preparado magistral o mezcla, junto con la vía de administración y el estado de la solicitud.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients';
