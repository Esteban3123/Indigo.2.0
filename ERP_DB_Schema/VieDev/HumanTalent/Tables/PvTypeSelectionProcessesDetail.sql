CREATE TABLE [HumanTalent].[PvTypeSelectionProcessesDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TypeSelectionProcessesId] INT             NOT NULL,
    [SelectionActivitiesId]    INT             NOT NULL,
    [Head]                     INT             NOT NULL,
    [Obligatory]               TINYINT         NOT NULL,
    [CreationUser]             VARCHAR (20)    NOT NULL,
    [CreationDate]             DATETIME        NOT NULL,
    [ModificationUser]         VARCHAR (20)    NULL,
    [ModificationDate]         DATETIME        NULL,
    [Weight]                   DECIMAL (18, 2) CONSTRAINT [DF__PvTypeSel__Weigh__77A62AB8] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PvTypeSelectionProcessesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PvTypeSelectionProcessesDetail_SelectionActivities] FOREIGN KEY ([SelectionActivitiesId]) REFERENCES [HumanTalent].[SelectionActivities] ([Id]),
    CONSTRAINT [FK_PvTypeSelectionProcessesDetail_TypeSelectionProcesses] FOREIGN KEY ([TypeSelectionProcessesId]) REFERENCES [HumanTalent].[TypeSelectionProcesses] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PvTypeSelectionProcessesDetail] NOCHECK CONSTRAINT [FK_PvTypeSelectionProcessesDetail_SelectionActivities];


GO
ALTER TABLE [HumanTalent].[PvTypeSelectionProcessesDetail] NOCHECK CONSTRAINT [FK_PvTypeSelectionProcessesDetail_TypeSelectionProcesses];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Feacha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de selección única, opciones por defecto SI/NO, para determinar si es obligatorio que se ejecuten las actividades seleccionadas dentro del proceso creado o no: 0- no obligatorio, 1- Obligatorio', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Obligatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para determinar el orden en que se deben ejecutar las actividades seleccionadas dentro del proceso creado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Head';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de las actividades de seleccion ()', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'SelectionActivitiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'TypeSelectionProcessesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Id';

