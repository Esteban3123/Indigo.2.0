CREATE TABLE [Maintenance].[MaintenanceSequenceDetail] (
    [Id]                     INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseMaintenanceC] INT    NOT NULL,
    [IdSequense]             INT    NOT NULL,
    [IdOperatingUnit]        INT    NULL,
    [Next]                   BIGINT CONSTRAINT [DF_SequenseMaintenanceD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseMaintenanceD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseMaintenanceD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseMaintenanceD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseMaintenanceD_SequenseMaintenanceC] FOREIGN KEY ([IdSequenseMaintenanceC]) REFERENCES [Maintenance].[MaintenanceSequence] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenanceSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseMaintenanceD_OperatingUnit];


GO
ALTER TABLE [Maintenance].[MaintenanceSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseMaintenanceD_Sequense];


GO
ALTER TABLE [Maintenance].[MaintenanceSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseMaintenanceD_SequenseMaintenanceC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseMaintenanceC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

