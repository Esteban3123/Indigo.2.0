CREATE TABLE [MixingStation].[LineClearanceCriteria] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [Name]                   VARCHAR (300) NOT NULL,
    [State]                  BIT           NOT NULL,
    [ValidCriteria]          BIT           NOT NULL,
    [SelectOptionToValidate] BIT           NULL,
    [ProductionChemist]      BIT           NOT NULL,
    [ProductionAssistant]    BIT           NOT NULL,
    [Creationuser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    CONSTRAINT [PK_LineClearanceCriteria] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Creationuser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Auxiliar de produccion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ProductionAssistant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quimico de produccion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ProductionChemist';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Selecciona la opcion a validar', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'SelectOptionToValidate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida criterio o no', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ValidCriteria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Estado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Nombre', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Id';

