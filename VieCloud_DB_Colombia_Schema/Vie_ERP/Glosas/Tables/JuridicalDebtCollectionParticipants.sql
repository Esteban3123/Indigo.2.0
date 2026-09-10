CREATE TABLE [Glosas].[JuridicalDebtCollectionParticipants] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConciliationCId] INT           NOT NULL,
    [Type]            CHAR (1)      NOT NULL,
    [FullName]        VARCHAR (200) NOT NULL,
    [Position]        VARCHAR (200) NOT NULL,
    [TimeStamp]       ROWVERSION    NOT NULL,
    CONSTRAINT [PK_JuridicalDebtCollectionParticipants] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_JuridicalDebtCollectionParticipants_ConciliationC] FOREIGN KEY ([ConciliationCId]) REFERENCES [Glosas].[ConciliationC] ([Id])
);


GO
ALTER TABLE [Glosas].[JuridicalDebtCollectionParticipants] NOCHECK CONSTRAINT [FK_JuridicalDebtCollectionParticipants_ConciliationC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP SQL Server) del evento de participante en conciliación. Registra el instante exacto de creación, modificación o actualización del registro del participante en el proceso de cobro judicial de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo, rol o puesto del participante en la conciliación de glosas. Ej: abogado, gestor de cobro, representante legal, coordinador de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo del participante', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Position';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del participante involucrado en la conciliación o cobro judicial de glosas. Profesional de la salud, gestor, abogado o representante externo.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'FullName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del participante', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'FullName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'FullName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de participante: 1=IPS (Institución Prestadora de Servicios), 2=Externo (tercero, abogado, gestor, acreedor). Determina si es participante interno o externo en cobro de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - IPS, 2 - Externo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que vincula al registro de cabecera de conciliación en tabla [Glosas].[ConciliationC]. Código de relación que agrupa participantes en un mismo proceso de conciliación o cobro judicial.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'ConciliationCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de relacion con la cabecera de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'ConciliationCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'ConciliationCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del participante en conciliación. Consecutivo autoincremental que distingue cada participante del proceso de cobro judicial de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de los participantes de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Participantes de un proceso de cobro jurídico en conciliación de glosas. Registra las personas involucradas (representantes, apoderados, testigos u otros roles) en cada conciliación judicial o extrajudicial de deudas por glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants';
