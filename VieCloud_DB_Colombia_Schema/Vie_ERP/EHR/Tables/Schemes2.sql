CREATE TABLE [EHR].[Schemes2] (
    [Code]                   VARCHAR (20)  NOT NULL,
    [Description]            VARCHAR (250) NOT NULL,
    [Cycles]                 INT           NOT NULL,
    [State]                  BIT           NOT NULL,
    [ServiceIPS]             CHAR (20)     NOT NULL,
    [DurationAdministration] INT           NOT NULL,
    [TypeScheme]             INT           NOT NULL,
    [RestDay]                INT           NULL,
    [Observation]            VARCHAR (MAX) NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que almacena esquemas de tratamiento (probablemente oncológicos o de infusión) definidos en el módulo EHR, identificados por un código y descripción. Cada esquema registra el número de ciclos, duración de administración, días de descanso, tipo de esquema y el servicio IPS asociado. Incluye campos de auditoría de creación y modificación, así como un estado activo/inactivo.', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'TABLE', @level1name=N'Schemes2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'TABLE', @level1name=N'Schemes2';
GO
