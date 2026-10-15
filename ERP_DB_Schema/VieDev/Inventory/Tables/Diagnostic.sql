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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No permitir egreso como diagnostico principal   1 - No lo permite   0 - Si lo permite', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'NotAllowMainDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Mixima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MaximumAgeMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Minima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MinimumAgeMeasurementUnit';

