CREATE TABLE [MixingStation].[RequestUnitDoseExternalCareCenter] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20) NOT NULL,
    [CMConfigurationId]         INT          NOT NULL,
    [ExternalCareCenterId]      INT          NOT NULL,
    [RequestType]               TINYINT      NOT NULL,
    [DocumentDate]              DATETIME     NOT NULL,
    [Status]                    TINYINT      NOT NULL,
    [CreationUser]              VARCHAR (20) NOT NULL,
    [CreationDate]              DATETIME     NOT NULL,
    [ModificationUser]          VARCHAR (20) NULL,
    [ModificationDate]          DATETIME     NULL,
    [ConfirmationUser]          VARCHAR (20) NULL,
    [ConfirmationDate]          DATETIME     NULL,
    [AnnulmentUser]             VARCHAR (20) NULL,
    [AnnulmentDate]             DATETIME     NULL,
    [TimeStamp]                 ROWVERSION   NOT NULL,
    [ContractExternalClientsId] INT          NOT NULL,
    CONSTRAINT [PK_RequestUnitDoseExternalCareCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenter_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenter_ContractExternalClients] FOREIGN KEY ([ContractExternalClientsId]) REFERENCES [MixingStation].[ContractExternalClients] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenter_ExternalCareCenter] FOREIGN KEY ([ExternalCareCenterId]) REFERENCES [MixingStation].[ExternalCareCenter] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenter] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenter_CMConfiguration];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenter] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenter_ExternalCareCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del contrato asociado al centro de atención externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estados:  1. Registrado  2. Confirmado  3. Anulado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de documento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo solicitud:  1. Solicitud de dosis personalizada  2. Solicitud de dosis estándar  3. Solicitud de Nutrición', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro atención externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la central de mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Id';

