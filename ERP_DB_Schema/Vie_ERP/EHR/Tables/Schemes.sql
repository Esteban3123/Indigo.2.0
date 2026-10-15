CREATE TABLE [EHR].[Schemes] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
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
    [TimeStamp]              ROWVERSION    NOT NULL,
    CONSTRAINT [PK_SchemesDrug] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Schemes_INCUPSIPS] FOREIGN KEY ([ServiceIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [EHR].[Schemes] NOCHECK CONSTRAINT [FK_Schemes_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la marca del tiempo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Día de descanso (tiempo de descanso entre ciclos) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'RestDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Esquema  1 - Quimioterapia  2 - Enfermedades Huérfanas  3 - Otros', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'TypeScheme';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración de la administración en meses', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'DurationAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'ServiceIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado  1 - Activo  2 - Inactivo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ciclos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'Cycles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'Schemes', @level2type = N'COLUMN', @level2name = N'Id';

