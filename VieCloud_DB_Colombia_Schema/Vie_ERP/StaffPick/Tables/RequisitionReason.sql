CREATE TABLE [StaffPick].[RequisitionReason] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (80) NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUSer] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_RequisitionReason] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__Requisit__A25C5AA75759B039] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME, nula si sin cambios) del motivo de requisición', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el registro (VARCHAR 20, nulo si sin cambios), identificador del profesional que realizó el último cambio', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'ModificationUSer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'ModificationUSer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'ModificationUSer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME) del motivo de requisición en el sistema', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (VARCHAR 20), identificador del profesional o sistema que ingresó el motivo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT): 1=Activo, 0=Inactivo; indica si el motivo de requisición está disponible', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 80) del motivo de requisición, razón por la cual se genera una solicitud o pedido', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Motivos de Requisición', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del motivo de requisición, identificador alfanumérico, clave única no agrupada', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Motivos de Requisición', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del motivo de requisición, clave primaria agrupada', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Motivos de Requisición', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de motivos o razones de requisición de personal (staff picking). Registra los conceptos por los cuales se solicita cobertura o asignación de personal, con su estado activo/inactivo y trazabilidad de auditoría.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'RequisitionReason';
