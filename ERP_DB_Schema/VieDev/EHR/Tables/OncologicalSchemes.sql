CREATE TABLE [EHR].[OncologicalSchemes] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [Description]            VARCHAR (250) NOT NULL,
    [Cycles]                 INT           NOT NULL,
    [State]                  BIT           NOT NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    [ServiceIPS]             CHAR (20)     NULL,
    [DurationAdministration] INT           NULL,
    [Observation]            VARCHAR (MAX) NULL,
    [TypeScheme]             INT           NULL,
    CONSTRAINT [PK_OncologicalSchemes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologicalSchemes_INCUPSIPS] FOREIGN KEY ([ServiceIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [EHR].[OncologicalSchemes] NOCHECK CONSTRAINT [FK_OncologicalSchemes_INCUPSIPS];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_OncologicalSchemes]
    ON [EHR].[OncologicalSchemes]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo de esquemas  1 = Quimioterapia    2 =Enfermedades Huérfanas     3 =Otros esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'TypeScheme';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la observación', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la duración administración esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'DurationAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Servicio de administración del esquema:', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'ServiceIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Marca de tiempo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de modificación', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario quien modifico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario quien lo creo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado  1 = Activo  0 =Inactivo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Número de ciclos de los esquemas oncologicos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'Cycles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la descripción esquemas oncologicos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo esquemas oncologicos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemes', @level2type = N'COLUMN', @level2name = N'Id';

