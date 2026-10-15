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
ALTER TABLE [MixingStation].[RequestMixingStationDetailPatients] NOCHECK CONSTRAINT [FK_RequestMixingStationDetailPatients_AdministrationRoute];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetailPatients] NOCHECK CONSTRAINT [FK_RequestMixingStationDetailPatients_CampaignDetail];


GO
ALTER TABLE [MixingStation].[RequestMixingStationDetailPatients] NOCHECK CONSTRAINT [FK_RequestMixingStationDetailPatients_RequestMixingStationDetail];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [MixingStation].[RequestMixingStationDetailPatients].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la vía de administración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:    1. En su Estado Inicial este en el Estado "Pendiente"  2. Confirmar se coloque "Confirmado"  3. Anular se coloque "Anulado"', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad con la cual se realiza este registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad con la cual se realiza este registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cama del paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Bed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la unidad funcional, puede ser externo o interno', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestMixingStationDetailPatients', @level2type = N'COLUMN', @level2name = N'Id';

