CREATE TABLE [Contract].[SettingsContract] (
    [Id]                                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                   INT          NOT NULL,
    [CUPSWithRelatedDescription]        BIT          CONSTRAINT [DF_SettingsContract_CUPSWithRelatedDescription] DEFAULT ((0)) NOT NULL,
    [CreationUser]                      VARCHAR (20) CONSTRAINT [DF_SettingsContract_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                      DATETIME     CONSTRAINT [DF_SettingsContract_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                  VARCHAR (20) NULL,
    [ModificationDate]                  DATETIME     NULL,
    [TimeStamp]                         ROWVERSION   NOT NULL,
    [RequestQuoteOutpatientServices]    BIT          CONSTRAINT [DF_SettingsContract_RequestQuoteOutpatientServices] DEFAULT ((0)) NOT NULL,
    [RequestQuoteIntrahospitalServices] BIT          CONSTRAINT [DF_SettingsContract_RequestQuoteIntrahospitalServices] DEFAULT ((0)) NOT NULL,
    [RequestQuoteOutpatientProducts]    BIT          CONSTRAINT [DF_SettingsContract_RequestQuoteOutpatientProducts] DEFAULT ((0)) NOT NULL,
    [RequestQuoteIntrahospitalProducts] BIT          CONSTRAINT [DF_SettingsContract_RequestQuoteIntrahospitalProducts] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SettingsContract] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingsContract_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Contract].[SettingsContract] NOCHECK CONSTRAINT [FK_SettingsContract_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite identifica si se Solicitan Cotización Productos Intrahospitalarios de manera restrictiva', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteIntrahospitalProducts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite identifica si se Solicitan Cotización Productos Ambulatorios de manera restrictiva', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteOutpatientProducts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite identifica si se Solicitan Cotización Servicios Intrahospitalarios de manera restrictiva', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteIntrahospitalServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite identifica si se Solicitan Cotización Servicios Ambulatorios de manera restrictiva', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteOutpatientServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CUPS con descripción relacionada, permite saber si en el formulario de CUPS, se debe o no mostrar la sección de descripciones relacionadas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CUPSWithRelatedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'Id';

