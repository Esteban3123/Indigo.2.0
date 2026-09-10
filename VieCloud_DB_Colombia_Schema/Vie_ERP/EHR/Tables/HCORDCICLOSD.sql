CREATE TABLE [EHR].[HCORDCICLOSD] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCORDQUIMIO]            INT       NOT NULL,
    [IDHCORDCICLOS]            INT       NULL,
    [CICLO]                    INT       NOT NULL,
    [DIA]                      INT       NOT NULL,
    [ESTADODIA]                INT       NOT NULL,
    [ADMISTRADIACASA]          BIT       CONSTRAINT [DF_HCORDCICLOSD_DIAENCASA] DEFAULT ((0)) NOT NULL,
    [FECHAPROGRAMACION]        DATETIME  NULL,
    [FECHAADMINISTRACION]      DATETIME  NULL,
    [USUARIOADMINISTRACION]    CHAR (20) NULL,
    [ProfessionalModification] CHAR (20) NULL,
    [DateModification]         DATETIME  NULL,
    [State]                    INT       CONSTRAINT [DF_HCORDCICLOSD_State] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCORDCICLOSD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDCICLOSD_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID])
);






GO



GO
CREATE NONCLUSTERED INDEX [IX_HCORDCICLOSD_IDHCORDCICLOS_ADMISTRADIACASA_CICLO_DIA_ESTADODIA_FECHAADMINISTRACION]
    ON [EHR].[HCORDCICLOSD]([IDHCORDCICLOS] ASC)
    INCLUDE([ADMISTRADIACASA], [CICLO], [DIA], [ESTADODIA], [FECHAADMINISTRACION]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDCICLOSD_CICLO_DIA_IDHCORDCICLOS_ADMISTRADIACASA_ESTADODIA_FECHAADMINISTRACION]
    ON [EHR].[HCORDCICLOSD]([CICLO] ASC, [DIA] ASC, [IDHCORDCICLOS] ASC)
    INCLUDE([ADMISTRADIACASA], [ESTADODIA], [FECHAADMINISTRACION]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro en ciclo de quimioterapia (INT): 1=Con origen desde ordenamiento, 2=Adicionado, 3=Modificado, 4=Eliminado. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de última modificación del registro de día en ciclo. Timestamp de cambio.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion del registro (ciclo) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional sanitario (CHAR 20) que realizó la última modificación del día en ciclo de quimioterapia. Usuario responsable del cambio.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza la modificacion del registro (ciclo) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario administrativo (CHAR 20) que registró la administración real del primer medicamento del día. Indica quién aplicó la quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'USUARIOADMINISTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Administración  , esto indica la fecha real en la que se aplico el primer medicamento de ese dia para el ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'USUARIOADMINISTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'USUARIOADMINISTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora real (DATETIME) de administración del primer medicamento del día en ciclo de quimioterapia. Momento efectivo de aplicación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'FECHAADMINISTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Administracion, esto indica la fecha real en la que se aplico el primer medicamento de ese dia para el ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'FECHAADMINISTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'FECHAADMINISTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora programada (DATETIME) para administración de medicamentos del día en ciclo. Fecha planificada de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'FECHAPROGRAMACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de programacion en la cual Indica la fecha en la cual estaban programados los medicamentos de ese dia. .  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'FECHAPROGRAMACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'FECHAPROGRAMACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT): True=todos medicamentos domiciliarios, False=mixto (casa y clínica). Identifica si aplicación del día es íntegramente en domicilio.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ADMISTRADIACASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'columna que debe indicar si para ese día, todos los medicamentos son domiciliarios o no.    Este campo Identifica si la aplicacion del Dia es en su totalidad en casa.    True -> Todos los medicamentos del Dia son en casa  False -> Casa y Clinica (Mixtos)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ADMISTRADIACASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ADMISTRADIACASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del día en ciclo (INT): 1=Sin cumplir, 2=Cumplido, 3=Empalme al iniciar software. Progreso diario de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ESTADODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correspone al esado del Dia en el ciclo.  1 - Dia Sin Cumplir  2 - Dia cumplido  3 - Queda en 3 cuando hacemos el empalme de Informacion al arrancar el software de Indigo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ESTADODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ESTADODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del día secuencial dentro del ciclo de quimioterapia (INT). Orden cronológico de administración.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dia del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número identificador del ciclo de quimioterapia (INT). Secuencia de tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, nullable) hacia tabla cabecera HCORDCICLOS. Relación con estructura de ciclos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera de CICLOS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, NOT NULL) hacia tabla HCORDQUIMIO. Referencia a orden de quimioterapia del paciente.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY 1,1) del detalle de día en ciclo de quimioterapia. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ID';


GO
CREATE NONCLUSTERED INDEX [IX_HCORDCICLOSD_Quimio_Ciclo]
    ON [EHR].[HCORDCICLOSD]([IDHCORDQUIMIO] ASC, [CICLO] ASC, [ADMISTRADIACASA] ASC, [ESTADODIA] ASC)
    INCLUDE([DIA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de días por ciclo de una orden de quimioterapia. Registra cada día de administración dentro de un ciclo de tratamiento oncológico, indicando si fue administrado en casa o en el centro, las fechas programadas y reales de administración, y el estado del día.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD';
