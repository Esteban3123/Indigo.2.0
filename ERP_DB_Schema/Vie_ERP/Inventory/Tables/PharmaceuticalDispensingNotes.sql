CREATE TABLE [Inventory].[PharmaceuticalDispensingNotes] (
    [Id]                            INT           IDENTITY (1, 1) NOT NULL,
    [PharmaceuticalRequestDetailId] INT           NOT NULL,
    [Note]                          VARCHAR (500) NOT NULL,
    [CreationUser]                  VARCHAR (50)  NOT NULL,
    [CreationDate]                  DATETIME      NOT NULL,
    [ATCId]                         INT           CONSTRAINT [DF__Pharmaceu__ATCId__6A37EC58] DEFAULT ((1)) NOT NULL,
    [AdmissionNumber]               CHAR(10)           CONSTRAINT [DF__Pharmaceu__Admis__6B2C1091] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingNotes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalDispensingNotes_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingNotes_PharmaceuticalRequestDetailId] FOREIGN KEY ([PharmaceuticalRequestDetailId]) REFERENCES [dbo].[HCFARMEPD] ([ID])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingNotes] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingNotes_ATC];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingNotes] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingNotes_PharmaceuticalRequestDetailId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de admision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingNotes', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento homologado desde el EHR ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingNotes', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingNotes', @level2type = N'COLUMN', @level2name = N'Note';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro de la tabla HCFARMAPD', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingNotes', @level2type = N'COLUMN', @level2name = N'PharmaceuticalRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingNotes', @level2type = N'COLUMN', @level2name = N'Id';

