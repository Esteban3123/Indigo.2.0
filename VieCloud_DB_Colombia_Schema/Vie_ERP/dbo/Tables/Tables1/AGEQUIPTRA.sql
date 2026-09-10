CREATE TABLE [dbo].[AGEQUIPTRA] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODEQUIPO] VARCHAR (15)  NOT NULL,
    [CODCENATE] CHAR (10)     NOT NULL,
    [TIPOEQUIP] TINYINT       NOT NULL,
    [ESTEQUIP]  BIT           NOT NULL,
    [DESCREQUI] VARCHAR (100) NOT NULL,
    [CODINVI]   VARCHAR (20)  NULL,
    [CONDESPE]  BIT           NULL,
    [OBSERVAC]  VARCHAR (500) NULL,
    [COLOR]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_AGEQUIPTRA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGEQUIPTRA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [IX_AGEQUIPTRA] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [CODEQUIPO] ASC)
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color del equipo de tratamiento registrado cuando el equipo está marcado con condiciones especiales (CONDESPE=1); atributo visual para identificación de equipos de quimioterapia, radioterapia o diálisis con requerimientos especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'COLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color del equipo de tratamiento que se registra si el equipo es marcado como condiciones especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'COLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales del equipo de tratamiento para pacientes con condiciones especiales; notas sobre restricciones, contraindicaciones, mantenimiento o protocolos específicos aplicables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'OBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion que se registra si el equipo es para pacientes con condiciones especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'OBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'OBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (1=Sí, 0=No) que determina si el equipo aplica exclusivamente a pacientes con tratamientos o condiciones especiales; controla visibilidad/disponibilidad del equipo en protocolos específicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CONDESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condicion Especial: Determina si el equipo estrictamente Aplica a pacientes con tratamientos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CONDESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CONDESPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de registro INVIMA (Instituto Nacional de Vigilancia de Medicamentos y Alimentos) del equipo médico de tratamiento; identificación regulatoria requerida para quimioterapia, radioterapia y diálisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODINVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Invima del equipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODINVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODINVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del equipo de tratamiento; nombre comercial, modelo, características técnicas y funcionalidad (ej: acelerador lineal, bomba de quimio, máquina de diálisis).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'DESCREQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'descripcion del equipo de  tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'DESCREQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'DESCREQUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo del equipo de tratamiento: 1=ACTIVO (disponible para uso en atención), 0=INACTIVO (fuera de servicio, mantenimiento o descontinuado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'ESTEQUIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del equipo d etratamiento  1-> ACTIVO   0 ->INACTIVO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'ESTEQUIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'ESTEQUIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de equipo de tratamiento especializado: 1=Quimioterapia, 2=Radioterapia, 3=Diálisis; categorización para gestión de inventario y asignación a unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'TIPOEQUIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Equipo 1 -> Quimio 2-> Radio 3-> Dialisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'TIPOEQUIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'TIPOEQUIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del centro/unidad de atención (FK a ADCENATEN) donde se localiza y opera el equipo; establece relación entre equipo y centro hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de relacion con centros de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del equipo de tratamiento dentro del centro de atención (quimio, radio, diálisis); clave para rastreabilidad y mantenimiento preventivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODEQUIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de equipo de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODEQUIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODEQUIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico, PK) de cada registro en la tabla de equipos de tratamiento; generado automáticamente por SQL Server para integridad referencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de registro de equipos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Equipos o recursos físicos (camillas, sillas de ruedas, vehículos de traslado, etc.) disponibles para agendamiento y traslado de pacientes, asociados a un centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA';
