CREATE TABLE [Inventory].[PharmaceuticalForm] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)  NOT NULL,
    [Name]                         VARCHAR (100) NOT NULL,
    [CrystalMedicalForm]           CHAR (3)      NOT NULL,
    [Status]                       BIT           NOT NULL,
    [CreationUser]                 VARCHAR (20)  CONSTRAINT [DF_PharmaceuticalForm_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                 DATETIME      CONSTRAINT [DF_PharmaceuticalForm_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]             VARCHAR (20)  NULL,
    [ModificationDate]             DATETIME      NULL,
    [RequireStability]             BIT           NULL,
    [PharmaceuticalFormGroupingId] INT           NULL,
    CONSTRAINT [PK_PharmaceuticalForm__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalForm_PharmaceuticalFormGrouping] FOREIGN KEY ([PharmaceuticalFormGroupingId]) REFERENCES [Inventory].[PharmaceuticalFormGrouping] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalForm] NOCHECK CONSTRAINT [FK_PharmaceuticalForm_PharmaceuticalFormGrouping];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PharmaceuticalForm__Code]
    ON [Inventory].[PharmaceuticalForm]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de agrupador de forma farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormGroupingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si requiere o no estabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'RequireStability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'forma medica que se obtiene de la tabla IHFORMEDI de crystal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'CrystalMedicalForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la forma farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la forma farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalForm', @level2type = N'COLUMN', @level2name = N'Id';

