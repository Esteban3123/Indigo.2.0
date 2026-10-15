CREATE TABLE [Billing].[LogsControlOutpatientServices] (
    [Id]                           INT          IDENTITY (1, 1) NOT NULL,
    [ControlOutPatientServicesXml] XML          NOT NULL,
    [CreationUser]                 VARCHAR (50) NOT NULL,
    [CreationDate]                 DATETIME     NOT NULL,
    [Timestamp]                    ROWVERSION   NOT NULL,
    CONSTRAINT [PK_LogsControlOutpatientServices__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LogsControlOutpatientServices', @level2type = N'COLUMN', @level2name = N'Timestamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LogsControlOutpatientServices', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LogsControlOutpatientServices', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Xml con los datos que generarón desde presentación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LogsControlOutpatientServices', @level2type = N'COLUMN', @level2name = N'ControlOutPatientServicesXml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LogsControlOutpatientServices', @level2type = N'COLUMN', @level2name = N'Id';

