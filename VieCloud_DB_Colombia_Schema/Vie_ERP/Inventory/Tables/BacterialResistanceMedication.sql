CREATE TABLE [Inventory].[BacterialResistanceMedication] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AtcId]            INT           NOT NULL,
    [StartDate]        DATETIME      NOT NULL,
    [EndDate]          DATETIME      NOT NULL,
    [Observation]      VARCHAR (500) NULL,
    [StateBRM]         TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [InactivateUser]   VARCHAR (20)  NULL,
    [InactivateDate]   DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_BacterialResistanceMedication_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BacterialResistanceMedication_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de control concurrente (TIMESTAMP); rastrea automáticamente el instante exacto de creación, modificación o cambio de estado del registro para auditoría y sincronización', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se inactivó o desactivó el registro de resistencia bacteriana (DATETIME, opcional)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'InactivateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inactiva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'InactivateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'InactivateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que inactivó o desactivó el registro de resistencia bacteriana (VARCHAR 20, opcional)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'InactivateUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Inactiva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'InactivateUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'InactivateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación o actualización del registro de resistencia bacteriana (DATETIME, opcional)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó última modificación al registro de resistencia bacteriana (VARCHAR 20, opcional)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de creación del registro de resistencia bacteriana en el sistema (DATETIME, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (identificación, login o ID de profesional) que creó el registro de resistencia bacteriana (VARCHAR 20, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de resistencia bacteriana: 1=Activo, 2=Vigente, 3=Inactivo (TINYINT); indica disponibilidad o vigencia del control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'StateBRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el estado de registro: 1:Activo, 2:Vigente, 3:Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'StateBRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'StateBRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas, observaciones o comentarios adicionales sobre la resistencia bacteriana, diagnóstico microbiológico o restricciones de uso del medicamento (VARCHAR 500, opcional)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final de vigencia o aplicación del registro de resistencia bacteriana del medicamento (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de aplicación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial de vigencia o aplicación del registro de resistencia bacteriana del medicamento (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'StartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial de aplicación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'StartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia la tabla Inventory.ATC; identifica el código ATC (Anatomical Therapeutic Chemical) del medicamento asociado a resistencia bacteriana', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia a la tabla Inventory.ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de resistencia bacteriana a medicamento en el sistema de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los medicamentos con resistencia bacteriana asociados a un código ATC, incluyendo el período de vigencia, estado y datos de auditoría de creación, modificación e inactivación. Permite gestionar qué principios activos están clasificados como de resistencia bacteriana y durante qué fechas aplica esa restricción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'BacterialResistanceMedication';
