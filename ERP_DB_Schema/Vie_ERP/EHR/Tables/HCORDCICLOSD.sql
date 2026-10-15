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
ALTER TABLE [EHR].[HCORDCICLOSD] NOCHECK CONSTRAINT [FK_HCORDCICLOSD_HCORDQUIMIO];


GO
CREATE NONCLUSTERED INDEX [IX_HCORDCICLOSD_IDHCORDCICLOS_ADMISTRADIACASA_CICLO_DIA_ESTADODIA_FECHAADMINISTRACION]
    ON [EHR].[HCORDCICLOSD]([IDHCORDCICLOS] ASC)
    INCLUDE([ADMISTRADIACASA], [CICLO], [DIA], [ESTADODIA], [FECHAADMINISTRACION]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDCICLOSD_CICLO_DIA_IDHCORDCICLOS_ADMISTRADIACASA_ESTADODIA_FECHAADMINISTRACION]
    ON [EHR].[HCORDCICLOSD]([CICLO] ASC, [DIA] ASC, [IDHCORDCICLOS] ASC)
    INCLUDE([ADMISTRADIACASA], [ESTADODIA], [FECHAADMINISTRACION]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificacion del registro (ciclo) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que realiza la modificacion del registro (ciclo) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Administración  , esto indica la fecha real en la que se aplico el primer medicamento de ese dia para el ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'USUARIOADMINISTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Administracion, esto indica la fecha real en la que se aplico el primer medicamento de ese dia para el ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'FECHAADMINISTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de programacion en la cual Indica la fecha en la cual estaban programados los medicamentos de ese dia. .  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'FECHAPROGRAMACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'columna que debe indicar si para ese día, todos los medicamentos son domiciliarios o no.    Este campo Identifica si la aplicacion del Dia es en su totalidad en casa.    True -> Todos los medicamentos del Dia son en casa  False -> Casa y Clinica (Mixtos)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ADMISTRADIACASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correspone al esado del Dia en el ciclo.  1 - Dia Sin Cumplir  2 - Dia cumplido  3 - Queda en 3 cuando hacemos el empalme de Informacion al arrancar el software de Indigo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ESTADODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dia del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera de CICLOS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSD', @level2type = N'COLUMN', @level2name = N'ID';

