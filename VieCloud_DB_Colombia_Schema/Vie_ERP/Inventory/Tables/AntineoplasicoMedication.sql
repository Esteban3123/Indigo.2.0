CREATE TABLE [Inventory].[AntineoplasicoMedication] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AtcId]            INT          NOT NULL,
    [ClassificationId] TINYINT      NOT NULL,
    [StateAPM]         TINYINT      NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [InactivateUser]   VARCHAR (20) NULL,
    [InactivateDate]   DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_AntineoplasicoMedication_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AntineoplasicoMedication_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (SQL Server) que registra el instante exacto de creación, modificación o cambio de estado del medicamento antineoplásico. Usado para auditoría y control de versiones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME en que se inactivó o desactivó el registro del medicamento antineoplásico. NULL si aún está activo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'InactivateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inactiva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'InactivateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'InactivateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la inactivación o desactivación del medicamento antineoplásico. NULL si el registro sigue activo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'InactivateUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Inactiva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'InactivateUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'InactivateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de creación inicial del registro del medicamento antineoplásico en el inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) responsable de la creación del registro del medicamento antineoplásico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del medicamento antineoplásico (TINYINT): 1=Activo, 2=Inactivo. Indica si el fármaco está disponible o descontinuado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'StateAPM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el estado de registro: 1:Activo, 2:Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'StateAPM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'StateAPM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación CAC del medicamento antineoplásico (TINYINT 1-33): Bleomicina, Cisplatino, Doxorubicina, Metotrexato, Vincristina, Rituximab, Trastuzumab, corticoides (Prednisona, Dexametasona) u otra clasificación de quimioterapia/fármaco oncológico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'ClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación CAC, 1:Bleomicina, 2:Busulfano, 3:Capecitabina, 4:Carboplatino, 5:Ciclofosfamida, 6:Ciclosporina, 7:Cisplatino, 8:Citarabina, 9:Clorambucilo, 10:Dacarbazina, 11:Doxorubicina, 12:Etopósido, 13:Fluorouracilo, 14:Gemcitabina, 15:Imatinib, 16:Interferón Alfa, 17:Melfalan, 18:Mercaptopurina, 19:Metotrexato, 20:Paclitaxel, 21:Pegfilgrastim, 22:Procarbazina, 23:Rituximab, 24:Tamoxifeno, 25:Tioguanina, 26:Trastuzumab, 27:Vinblastina, 28:Vincristina, 29:Prednisona, 30:Prednisolona, 31:Metilprednisolona, 32:Dexametasona, 33:Otra clasificación de antineoplásico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'ClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'ClassificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) que referencia la tabla Inventory.ATC. Código de Clasificación Anatómica Terapéutica Química del medicamento antineoplásico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia a la tabla Inventory.ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de medicamento antineoplásico. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maestro de medicamentos antineoplásicos (quimioterapia y oncológicos) disponibles en el inventario, clasificados por código ATC y tipo de clasificación, con control de estado activo/inactivo y auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication';
