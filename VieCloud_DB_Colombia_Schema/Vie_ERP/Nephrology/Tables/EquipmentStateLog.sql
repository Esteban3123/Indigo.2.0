CREATE TABLE [Nephrology].[EquipmentStateLog] (
    [Id]           INT       IDENTITY (1, 1) NOT NULL,
    [IdAGEQUIPTRA] INT       NOT NULL,
    [State]        INT       NOT NULL,
    [StartedAt]    DATETIME  NOT NULL,
    [EndedAt]      DATETIME  NULL,
    [CodeUser]     CHAR (20) NOT NULL,
    CONSTRAINT [PK_EquipmentStateLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_EquipmentStateLog_IdAgequiptra_State_EndedAt]
    ON [Nephrology].[EquipmentStateLog]([IdAGEQUIPTRA] ASC, [State] ASC, [EndedAt] DESC)
    INCLUDE([CodeUser], [StartedAt]);


GO
CREATE NONCLUSTERED INDEX [IX_EquipmentStateLog_IdAgequiptra_EndedAt_StartedAt]
    ON [Nephrology].[EquipmentStateLog]([IdAGEQUIPTRA] ASC, [EndedAt] DESC, [StartedAt] DESC)
    INCLUDE([State], [CodeUser]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario (profesional sanitario, técnico de mantenimiento) que realizó el movimiento o cambio de estado del equipo de diálisis.', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'CodeUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario del movimiento', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'CodeUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'CodeUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización del estado o gestión de desinfección del equipo; NULL si el estado sigue activo.', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'EndedAt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Fin, termino a las: ', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'EndedAt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'EndedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del cambio de estado o ciclo de desinfección del equipo de diálisis.', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'StartedAt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio, empezo a las: ', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'StartedAt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'StartedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del equipo de diálisis: (1) Ocupado/En uso, (2) En desinfección - Limpieza, (3) En desinfección - Verificación técnica, (4) Disponible para tratamiento.', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'''''1. Equipos ocupado ''''
''''2. Equipos en desinfección - En limpieza''''
''''3. Equipos en desinfección - En verificación técnica''''
''''4. Equipos disponibles''''    
', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que vincula a la tabla maestra de creación y gestión de equipos de tratamiento renal (diálisis).', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'IdAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla maestra de creacion de equipos de tratamiento', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'IdAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'IdAGEQUIPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria, identity auto-incremental) del registro de movimiento de estado del equipo.', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla, el identity', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de gestión de ciclo de vida y desinfección de equipos de diálisis. Auditoría de estados y transiciones de máquinas de tratamiento renal (equipos ocupados, en limpieza, en verificación técnica, disponibles) con trazabilidad de usuario y timestamps.', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla la cual guarda todo el tramite de Gestión de Desinfección de Equipos de Dialización, es decir equipos de dialisis que se crean en equipos de tratamiento.
', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nephrology', @level1type = N'TABLE', @level1name = N'EquipmentStateLog';

