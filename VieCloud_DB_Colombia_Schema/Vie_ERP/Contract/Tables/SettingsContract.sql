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
    [SiifaEnabled]                      BIT          CONSTRAINT [DF_SettingsContract_SiifaEnabled] DEFAULT ((0)) NOT NULL,
    [SiifaStartDate]                    DATETIME     NULL,
    CONSTRAINT [PK_SettingsContract] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingsContract_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que habilita la gestion del reporte del Sistema de Informacion Financiera y Asistencial - SIIFA, Resolucion 1962 de 2025. Por defecto 0 (no aplica)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'SiifaEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME, nullable) a partir de la cual aplica la gestion del reporte SIIFA. Solo se registra cuando SiifaEnabled esta activo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'SiifaStartDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que habilita/restringe solicitud de cotización para productos/insumos intrahospitalarios según contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteIntrahospitalProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite identifica si se Solicitan Cotización Productos Intrahospitalarios de manera restrictiva', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteIntrahospitalProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteIntrahospitalProducts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que habilita/restringe solicitud de cotización para productos/insumos ambulatorios según contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteOutpatientProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite identifica si se Solicitan Cotización Productos Ambulatorios de manera restrictiva', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteOutpatientProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteOutpatientProducts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que habilita/restringe solicitud de cotización para servicios intrahospitalarios/internación según contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteIntrahospitalServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite identifica si se Solicitan Cotización Servicios Intrahospitalarios de manera restrictiva', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteIntrahospitalServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteIntrahospitalServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que habilita/restringe solicitud de cotización para servicios ambulatorios según contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteOutpatientServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite identifica si se Solicitan Cotización Servicios Ambulatorios de manera restrictiva', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteOutpatientServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'RequestQuoteOutpatientServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo/versión para control de concurrencia en SQL Server, generada automáticamente en cada cambio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó última modificación en la configuración (VARCHAR 20, nullable)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de configuración del contrato (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de configuración (VARCHAR 20, por defecto 999)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que controla si se debe mostrar descripciones relacionadas en formulario de CUPS (procedimientos/servicios según clasificación nacional)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CUPSWithRelatedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CUPS con descripción relacionada, permite saber si en el formulario de CUPS, se debe o no mostrar la sección de descripciones relacionadas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CUPSWithRelatedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'CUPSWithRelatedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa/centro de atención asociado al contrato, referencia a tabla Common.OperatingUnit (FK)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de configuración de contrato, clave primaria (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración general de contratos por unidad operativa. Define parámetros de comportamiento contractual como el uso de descripciones relacionadas con códigos CUPS y la obligatoriedad de solicitar cotización previa para servicios y productos, tanto ambulatorios como intrahospitalarios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingsContract';
