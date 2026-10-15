CREATE TABLE [Inventory].[Diagnostic] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)  NOT NULL,
    [Name]                      VARCHAR (100) NOT NULL,
    [MinimumAge]                NUMERIC (3)   NOT NULL,
    [MinimumAgeMeasurementUnit] TINYINT       NOT NULL,
    [MaximumAge]                NUMERIC (3)   NOT NULL,
    [MaximumAgeMeasurementUnit] TINYINT       NOT NULL,
    [AppliesToMale]             BIT           NOT NULL,
    [AppliesToFemale]           BIT           NOT NULL,
    [RequiresNotification]      BIT           NOT NULL,
    [CodeCIE10]                 VARCHAR (4)   NOT NULL,
    [NotAllowMainDiagnosis]     BIT           NOT NULL,
    [Status]                    BIT           NOT NULL,
    [CreationUser]              VARCHAR (20)  CONSTRAINT [DF_Diagnostic_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]              DATETIME      CONSTRAINT [DF_Diagnostic_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]          VARCHAR (20)  NULL,
    [ModificationDate]          DATETIME      NULL,
    [TimeStamp]                 ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Diagnostic__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Diagnostic__Code]
    ON [Inventory].[Diagnostic]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No permitir egreso como diagnostico principal   1 - No lo permite   0 - Si lo permite', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'NotAllowMainDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CIE10 (Clasificación internacional de enfermedades, décima versión)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CodeCIE10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige Notificacion Obligatoria, este campo se utiliza mas que todo para reportes donde se tiene que notificar al estado ciertos diagnosticos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'RequiresNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica al Sexo Femenino', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'AppliesToFemale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica al Sexo Masculino', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'AppliesToMale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Mixima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MaximumAgeMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Mixima en la que aplica el Diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Minima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MinimumAgeMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Minima en la que aplica el Diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Id';

